using ProcessesInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Threading;

namespace TaskManager
{
    /// <summary>
    /// Логика взаимодействия для ServiceInfoWindow.xaml
    /// </summary>
    public partial class ServiceInfoWindow : Window
    {
        private ServiceInformation service;

        public ServiceInfoWindow()
        {
            InitializeComponent();

            service = new ServiceInformation();
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedIndex;

            try
            {
                service.ChangeStatus(item);
            }
            catch(IndexOutOfRangeException ex) { }

            UpdateServices();
        }

        private void UpdateServices()
        {
            Thread.Sleep(300);
            var serv = new ServiceInformation();
            ServiceInfoList.ItemsSource = null;
            ServiceInfoList.Items.Clear();
            ServiceInfoList.ItemsSource = serv.GetServiceInfo();
        }

    }
}
