using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutputFileList
{
    public class TaskData
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public TaskType Type { get; set; }
        public bool IsSigned { get; set; }
    }
}