using DeviceDatabase.Controller;
using DeviceDatabase.Model;
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

namespace DeviceDatabase.View.CustomMessageBox
{
    /// <summary>
    /// Interaction logic for AddDeviceView.xaml
    /// </summary>
    public partial class AddDeviceView : Window
    {

        public Device NewDevice { get; set; }

        public AddDeviceView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.cb_DeviceTypes.ItemsSource = DatabaseController.GetDeviceTypes();
            this.cb_DeviceTypes.DisplayMemberPath = "Name";
        }

        private void bt_Accept_Click(object sender, RoutedEventArgs e)
        {
            if (this.tb_DeviceName.Text.Trim() != "")
            {
                DeviceType dt = (DeviceType)cb_DeviceTypes.SelectedItem;
                this.NewDevice = new Device(this.tb_DeviceName.Text, dt.DeviceTypeId, "1234asfw");
                this.DialogResult = true;
            }
        }
    }
}
