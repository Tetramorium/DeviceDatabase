using DeviceDatabase.Controller;
using DeviceDatabase.Model;
using DeviceDatabase.View.CustomMessageBox;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeviceDatabase.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Device> d = DatabaseController.GetDevices();

            this.dg_DevicesList.ItemsSource = DatabaseController.GetDevices();
        }

        private void AddCalamity(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Click!");
        }

        private void DeleteDevice(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Click!");
        }

        private void EditDevice(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Click!");
        }

        private void AddDevice(object sender, RoutedEventArgs e)
        {
            AddDeviceView adv = new AddDeviceView();

            if (adv.ShowDialog() == true)
            {
                DatabaseController.AddDevice(adv.NewDevice);
                Update();
            }
        }

        private void tb_SearchDevice_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            if (tb_SearchDevice.Text.Trim() != "")
            {
                this.dg_DevicesList.ItemsSource = DatabaseController.SearchDevices(tb_SearchDevice.Text);
            }
            else
            {
                this.dg_DevicesList.ItemsSource = DatabaseController.GetDevices();
            }
        }
    }
}
