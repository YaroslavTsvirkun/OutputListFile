using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;


namespace OutputFileList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TaskData> List { get; }
        public MainWindow()
        {
            InitializeComponent();
            List = new ObservableCollection<TaskData>();
        }

        private void BtnGetDataClick(object sender, RoutedEventArgs e)
        {
            btnGetData.IsEnabled = false;
            lwListData.Items.Clear();

            if (rbRegistry.IsChecked == true)
            {

            }

            if (rbStartMenu.IsChecked == true)
            {
                
            }

            if (rbScheduler.IsChecked == true)
            {
                BackgroundWorker worker = new BackgroundWorker
                {
                    WorkerReportsProgress = true
                };
                worker.DoWork += DoWork;
                worker.ProgressChanged += ProgressChanged;

                worker.RunWorkerAsync();
            }

            btnGetData.IsEnabled = true;
        }

        void DoWork(object sender, DoWorkEventArgs e)
        {
            TaskSchedulerInfo taskSchedulerInfo = new();

            Thread thread = new(taskSchedulerInfo.Execute);
            thread.Start();

            for (int i = 0; i < TaskSchedulerInfo.GetCountTask() - 1; i++)
            {
                if (taskSchedulerInfo.TaskInfoData.Count == 0)
                {
                    System.Threading.Tasks.Task.Delay(2000).Wait();
                }
                else
                {
                    lwListData.Dispatcher.BeginInvoke(new Action(delegate ()
                    {
                        lwListData.Items.Add(taskSchedulerInfo.TaskInfoData[i]);
                        System.Threading.Tasks.Task.Delay(5).Wait();
                    }));
                    (sender as BackgroundWorker).ReportProgress(i);
                }
            }
        }

        void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbDownloadData.Value = e.ProgressPercentage;
        }
    }
}