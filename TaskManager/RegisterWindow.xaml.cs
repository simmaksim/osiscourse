using Microsoft.Win32;
using ProcessesInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaskManager
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private RegisterInformation register = new RegisterInformation();
        private string value = string.Empty;
        private string expression = string.Empty;
        private string key = string.Empty;

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;

            if (radioButton.Name == "CurrentUser")
                register.BaseRegistryKey = Registry.CurrentUser;
            else if (radioButton.Name == "CurrentConfig")
                register.BaseRegistryKey = Registry.CurrentConfig;
            else if (radioButton.Name == "Users")
                register.BaseRegistryKey = Registry.Users;
            else if (radioButton.Name == "LocalMachine")
                register.BaseRegistryKey = Registry.LocalMachine;
            else
                register.BaseRegistryKey = Registry.ClassesRoot;
        }

        private void Expression_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            expression = textBox.Text;
        }

        private void Value_Changed(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            value = textBox.Text;
        }

        private void Path_Changed(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            register.SubKey = textBox.Text;
        }

        private void Key_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            key = textBox.Text;
        }

        private void ValueSearch_Click(object sender, RoutedEventArgs e)
        {
            if (register.SubKey == null)
                MessageBox.Show("Input a path for searching!");

            if (value == string.Empty)
                MessageBox.Show("Input a value for searching!");

            RegisterList.ItemsSource = null;
            RegisterList.Items.Clear();

            try
            {
                var info = register.Search(value);
                RegisterList.ItemsSource = info;

                if (info.Count == 0)
                    MessageBox.Show("Elements not found!");
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("Path not found!");
            }
        }

        private void ExpressionSearch_Click(object sender, RoutedEventArgs e)
        {
            if (register.SubKey == null)
                MessageBox.Show("Input a path for searching!");

            if (expression == string.Empty)
                MessageBox.Show("Input an regular expression for searching!");

            RegisterList.ItemsSource = null;
            RegisterList.Items.Clear();

            Func<List<string>> func = () =>
            {
                try
                {
                    var list = register.GetKeyPath(expression);
                    return list;
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show("Path not found!");
                    return null;
                }
            };

            var task = new Task<List<string>>(func);
            task.Start();

            RegisterList.ItemsSource = task.Result;
            if (task.Result != null &&  task.Result.Count == 0)
                MessageBox.Show("Elements not found!");
            task.Dispose();
        }

        private void WriteValue_Click(object sender, RoutedEventArgs e)
        {
            if (register.SubKey == null)
                MessageBox.Show("Input a path for searching!");

            if (value == string.Empty)
                MessageBox.Show("Input an value for searching!");

            Func<bool> func = () =>
            {
                try
                {
                    return register.WriteValue(register.SubKey, value);
                }
                catch
                {
                    MessageBox.Show("ERROR");
                    return false;
                }
            };

            var task = new Task<bool>(func);
            task.Start();

            if (task.Result)
                MessageBox.Show("Value is write!");
            task.Dispose();
        }

        private void DeleteValue_Click(object sender, RoutedEventArgs e)
        {
            if (register.SubKey == null)
                MessageBox.Show("Input a path for searching!");

            if (key == string.Empty)
                MessageBox.Show("Input an value for searching!");

            Func<bool> func = () =>
            {
                try
                {
                    return register.DeleteKey(key);
                }
                catch
                {
                    MessageBox.Show("ERROR");
                    return false;
                }
            };

            var task = new Task<bool>(func);
            task.Start();

            if (task.Result)
                MessageBox.Show("Key is delited!");
            task.Dispose();
        }

        private void DeleteTree_Click(object sender, RoutedEventArgs e)
        {
            if (register.SubKey == null)
                MessageBox.Show("Input a path for searching!");

            Func<bool> func = () =>
            {
                try
                {
                    return register.DeleteSubKeyTree();
                }
                catch
                {
                    MessageBox.Show("ERROR");
                    return false;
                }
            };

            var task = new Task<bool>(func);
            task.Start();

            if (task.Result)
                MessageBox.Show("Tree keys is delited!");
            task.Dispose();
        }

        private void ReadValue_Click(object sender, RoutedEventArgs e)
        {
            if (register.SubKey == null)
                MessageBox.Show("Input a path for searching!");

            if (key == string.Empty)
                MessageBox.Show("Input an value for searching!");

            Func<string> func = () =>
            {
                try
                {
                    return register.ReadValue(key);
                }
                catch
                {
                    MessageBox.Show("ERROR");
                    return string.Empty;
                }
            };

            var task = new Task<string>(func);
            task.Start();

            if (task.Result != string.Empty && task.Result != null)
                MessageBox.Show(task.Result.ToString());
            task.Dispose();
        }
    }
}
