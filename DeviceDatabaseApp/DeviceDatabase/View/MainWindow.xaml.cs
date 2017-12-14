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
            Update();
            UpdateDeviceTypeList();
            UpdateCalamityList();
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
            AddDeviceView adv = new AddDeviceView(this);

            if (adv.ShowDialog() == true)
            {
                DatabaseController.AddDevice(adv.NewDevice);
                Update();
            }
        }

        private void AddDeviceType(object sender, RoutedEventArgs e)
        {
            AddDeviceTypeView advt = new AddDeviceTypeView(this);

            if (advt.ShowDialog() == true)
            {
                DatabaseController.AddDeviceType(advt.NewDeviceType);
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

        private void tb_SearchDeviceType_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDeviceTypeList();
        }

        private void UpdateCalamityList()
        {
            if (tb_SearchCalamity.Text.Trim() != "")
            {
                this.dg_CalamityList.ItemsSource = DatabaseController.SearchCalamities(tb_SearchCalamity.Text);
            }
            else
            {
                this.dg_CalamityList.ItemsSource = DatabaseController.GetCalamities();
            }
        }

        private void UpdateDeviceTypeList()
        {
            if (tb_SearchDeviceType.Text.Trim() != "")
            {
                this.dg_DeviceTypesList.ItemsSource = DatabaseController.SearchDeviceTypes(tb_SearchDeviceType.Text);
            }
            else
            {
                this.dg_DeviceTypesList.ItemsSource = DatabaseController.GetDeviceTypes();
            }
        }

        private void DeleteDeviceType(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Click!");
        }

        private void EditDeviceType(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Click!");
        }

        private void tb_SearchCalamity_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
