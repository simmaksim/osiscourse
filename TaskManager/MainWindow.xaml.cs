using Microsoft.Win32;
using ProcessesInformation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SystemProcess systemProcess;
        private ThreadsInfo threadsInfo;
        private CurrentProcess currentProcess;
        private VideoCardInformation videoCard;
        private OSInformation os;
        private ProcessorInformation processor;
        private RAMInformation ram;
        private DiskInformation disc;
        private HDDInformation hdd;
        private ProcessorUsage usage;
        private System.Timers.Timer timer;
        private ServiceInformation service;
        private SoftwareInformation software;

        private SoftwareInfoWindow softInfoWindow;
        private bool isReady = false;

        private List<SystemProcess> processes;
        private List<ThreadsInfo> threads;
        private List<CurrentProcess> currentProcessInfo;
        private List<VideoCardInformation> videoCardInfo;
        private List<RAMInformation> ramInfo;
        private List<HDDInformation> hddInfo;
        private List<SoftwareInformation> softInfo;

        public MainWindow()
        {
            InitializeComponent();
            systemProcess = new SystemProcess();
            threadsInfo = new ThreadsInfo();
            currentProcess = new CurrentProcess();
            videoCard = new VideoCardInformation();
            os = new OSInformation();
            processor = new ProcessorInformation();
            ram = new RAMInformation();
            disc = new DiskInformation();
            hdd = new HDDInformation();
            usage = new ProcessorUsage();
            service = new ServiceInformation();
            software = new SoftwareInformation();

            processes = new List<SystemProcess>();
            currentProcessInfo = new List<CurrentProcess>();

            InformationAboutProcesses();

            UserNameLabel.Content = UserName.GetUserName().ToString();
            
            timer = new System.Timers.Timer(500);
            timer.Elapsed += new ElapsedEventHandler(UpdateProcessorUsage);
            timer.Start();
        }

        private void UpdateProcessorUsage(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
            {
                CPUProgressBar.Value = usage.GetCurrentValue();
                ProcessorCount.Text = ((int)usage.GetCurrentValue()).ToString() + "%";
                RAMCount.Text = ((int)usage.GetAvailableRAM()).ToString() + " MB";
            }));
        }

        private void InformationAboutProcesses()
        {
            ProcessesList.ItemsSource = null;
            ProcessesList.Items.Clear();
            ProcessesList.ItemsSource = systemProcess.GetProcesses();
        }

        private void VideoCard_Click(object sender, RoutedEventArgs e)
        {
            videoCardInfo = new List<VideoCardInformation>();
            videoCardInfo.AddRange(videoCard.GetVideoCardInfo()[0]);
            videoCardInfo.AddRange(videoCard.GetVideoCardInfo()[1]);
             
            var videoCardInfoWindow = new VideoCardInfo();
            videoCardInfoWindow.Show();
            videoCardInfoWindow.VideoCardInfoList.ItemsSource = videoCardInfo;
        }

        private void Update_OnClick(object sender, RoutedEventArgs e)
        {     
            InformationAboutProcesses();
        }

        private void Kill_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = ProcessesList.SelectedItem;
                var process = item as SystemProcess;
                systemProcess.KillProcessById(process.ProcessID);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Process to kill is not selected!");
            }
            catch
            {
                MessageBox.Show("Deny access!");
            }
            Thread.Sleep(500);
            InformationAboutProcesses();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = ProcessesList.SelectedItem;
                var process = item as SystemProcess;
                threads = threadsInfo.GetThreadsInformationById(process.ProcessID);
                currentProcessInfo = currentProcess.GetCurrentProcessInformationById(process.ProcessID);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Process to info is not selected!");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Deny access!");
                return;
            }
            var processInfoWindow = new ProcessInfoWindow();
            processInfoWindow.Show();
            processInfoWindow.ThreadsList.ItemsSource = threads;
            processInfoWindow.CurrentProcessList.ItemsSource = currentProcessInfo;
        }

        private void OS_Click(object sender, RoutedEventArgs e)
        {
            var osInfoWindow = new OSInfoWindow();
            osInfoWindow.Show();
            osInfoWindow.OSInfoList.ItemsSource = os.GetOSInfo();
        }

        private void Processor_Click(object sender, RoutedEventArgs e)
        {
            var processorInfoWindow = new ProcessorInfoWindow();
            processorInfoWindow.Show();
            processorInfoWindow.ProcessorInfoList.ItemsSource = processor.GetProcessorInfo();
        }

        private void RAM_Click(object sender, RoutedEventArgs e)
        {
            ramInfo = new List<RAMInformation>();
            ramInfo.AddRange(ram.GetRAMInfo()[0]);
            ramInfo.AddRange(ram.GetRAMInfo()[1]);

            var ramInfoWindow = new RAMInfoWindow();
            ramInfoWindow.Show();
            ramInfoWindow.RAMInfoList.ItemsSource = ramInfo;
        }

        private void Disk_Click(object sender, RoutedEventArgs e)
        {
            var discInfoWindow = new DiscInfoWindow();
            discInfoWindow.DiscInfoList.ItemsSource = disc.GetDrivesInfo();
            discInfoWindow.Show();
        }

        private void HDD_Click(object sender, RoutedEventArgs e)
        {
            var hddInfoWindow = new HDDInfoWindow();
            hddInfoWindow.Show();
            hddInfoWindow.HDDInfoList.ItemsSource = hdd.GetHDDInfo();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var regysterWindow = new RegisterWindow();
            regysterWindow.Show();
        }

        private void Service_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var serviceInfoWindow = new ServiceInfoWindow();
                serviceInfoWindow.Show();
                serviceInfoWindow.ServiceInfoList.ItemsSource = service.GetServiceInfo();
            }
            catch
            {
                MessageBox.Show("Can't download all services!");
            }
        }

        private void Software_Click(object sender, RoutedEventArgs e)
        {
            softInfoWindow = new SoftwareInfoWindow();
            softInfoWindow.Show();

            isReady = false;
            softInfo = software.GetSoftwareInfo(out isReady);

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(UpdateProgressBar);
            timer.Start();
        }

        private void UpdateProgressBar(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
            {
                if (isReady)
                {
                    softInfoWindow.SoftProgressBar.Value = 100;
                    softInfoWindow.SoftwareInfoList.ItemsSource = softInfo;
                }
            }));
        }
    }
}
