using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TaskScheduler;

namespace OutputFileList
{
    public class TaskSchedulerInfo : TaskBaseInfo
    {
        private const int TASK_FLAG_HIDDEN = (int)_TASK_ENUM_FLAGS.TASK_ENUM_HIDDEN;
        private readonly TaskScheduler.TaskScheduler taskService;
        private readonly ITaskFolder rootFolder;
        private readonly bool scoreFlag;

        public TaskSchedulerInfo()
        {
            scoreFlag = false;
            taskService = new();
            taskService.Connect();

            rootFolder = taskService.GetFolder("\\");

            TaskInfoData = new List<TaskData>();
        }

        public TaskSchedulerInfo(bool scoreFlag) : this()
        {
            this.scoreFlag = scoreFlag;
        }

        public static int GetCountTask()
        {
            TaskSchedulerInfo taskSchedulerInfo = new(true);
            taskSchedulerInfo.Execute();

            return taskSchedulerInfo.Count;
        }

        public void Execute()
        {
            EnumFolderTasks(rootFolder);
        }

        private void EnumFolderTasks(ITaskFolder tf)
        {
            foreach (IRegisteredTask task in tf.GetTasks(TASK_FLAG_HIDDEN))
            {
                ActionOnTask(task);
            }
                
            foreach (ITaskFolder sfld in tf.GetFolders(TASK_FLAG_HIDDEN))
            {
                EnumFolderTasks(sfld);
            }
        }

        private void ActionOnTask(IRegisteredTask rt)
        {
            Count++;

            if (!scoreFlag)
            {
                var task = Deserialize(rt.Xml);

                if (task.Actions.Exec is null)
                {
                    TaskInfoData.Add(
                        new TaskData
                        {
                            Path = rt.Path,
                            Name = rt.Name,
                            Type = TaskType.Scheduler,
                            IsSigned = false
                        });
                }
                else
                {
                    TaskInfoData.Add(
                        new TaskData
                        {
                            Path = task.Actions.Exec.Command,
                            Name = rt.Name,
                            Type = TaskType.Scheduler,
                            IsSigned = base.VerifyAuthenticodeSignature(task.Actions.Exec.Command)
                        });
                }
            }
        }

        public Task Deserialize(string xmlData)
        {
            XmlSerializer serializer = new(typeof(Task));
            using StreamReader reader = StringToStream(xmlData);
            return (Task)serializer.Deserialize(reader);
        }
    }
}