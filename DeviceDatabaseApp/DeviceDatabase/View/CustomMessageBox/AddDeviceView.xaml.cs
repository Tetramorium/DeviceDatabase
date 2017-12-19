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
        //http://www.wpf-tutorial.com/dialogs/creating-a-custom-input-dialog/

        public Device NewDevice { get; set; }

        public AddDeviceView(Window _Owner)
        {
            InitializeComponent();

            this.Owner = _Owner;
        }

        public AddDeviceView(Window _Owner, Device _OldDevice)
        {
            InitializeComponent();

            this.Owner = _Owner;
            this.Title = string.Format("Editing {0}", _OldDevice.Name);

            this.NewDevice = _OldDevice;
            this.bt_Accept.Click -= bt_AcceptAdd_Click;
            this.bt_Accept.Click += bt_AcceptEdit_Click;

            this.tb_DeviceName.Text = _OldDevice.Name;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<DeviceType> DeviceTypeList = DatabaseController.GetDeviceTypes();

            if (DeviceTypeList.Count() == 0)
            {
                MessageBox.Show("There are no Device Types yet");
                this.DialogResult = false;
            }

            this.cb_DeviceTypes.ItemsSource = DeviceTypeList;
            this.cb_DeviceTypes.DisplayMemberPath = "Name";

            if (NewDevice != null && NewDevice.DeviceType != null)
            {
                this.cb_DeviceTypes.SelectedItem = NewDevice.DeviceType;
            }

        }

        private bool IsValid()
        {
            if (this.cb_DeviceTypes.SelectedItem != null)
            {
                if (this.tb_DeviceName.Text.Trim() != "")
                {

                    if (this.NewDevice == null)
                    {
                        return IfAddEditValid();
                    }
                    else
                    {
                        if (NewDevice.Name.ToLower() == this.tb_DeviceName.Text.ToLower())
                        {
                            return true;
                        }
                        else
                        {
                            return IfAddEditValid();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Device name is required!");
                }
            }
            else
            {
                MessageBox.Show("Device Type is required!");
            }

            return false;
        }

        private bool IfAddEditValid()
        {
            if (DatabaseController.CheckIfDeviceIsUnique(this.tb_DeviceName.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Device name is not unique!");
            }
            return false;
        }

        private void bt_AcceptEdit_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                DeviceType dt = (DeviceType)cb_DeviceTypes.SelectedItem;
                // Set DeviceType to null to not cause duplicates
                this.NewDevice.DeviceType = dt;
                // Set the foreign key
                //this.NewDevice.DeviceTypeId = dt.DeviceTypeId;
                this.NewDevice.Name = this.tb_DeviceName.Text;
                this.DialogResult = true;
            }
        }


        private void bt_AcceptAdd_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                DeviceType dt = (DeviceType)cb_DeviceTypes.SelectedItem;
                this.NewDevice = new Device(this.tb_DeviceName.Text, dt.DeviceTypeId);
                this.DialogResult = true;
            }
        }
    }
}
