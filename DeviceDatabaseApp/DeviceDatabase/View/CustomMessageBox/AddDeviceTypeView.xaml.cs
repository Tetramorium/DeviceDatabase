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
    /// Interaction logic for AddDeviceTypeView.xaml
    /// </summary>
    public partial class AddDeviceTypeView : Window
    {
        //http://www.wpf-tutorial.com/dialogs/creating-a-custom-input-dialog/

        public DeviceType NewDeviceType { get; set; }

        public AddDeviceTypeView(Window _Owner)
        {
            InitializeComponent();

            this.Owner = _Owner;
        }

        public AddDeviceTypeView(Window _Owner, DeviceType _OldDeviceType)
        {
            InitializeComponent();

            this.Owner = _Owner;

            this.NewDeviceType = _OldDeviceType;
            this.bt_Accept.Click -= bt_AcceptAdd_Click;
            this.bt_Accept.Click += bt_AcceptEdit_Click;

            this.tb_DeviceTypeName.Text = _OldDeviceType.Name;
        }

        private bool IsValid()
        {
            if (this.tb_DeviceTypeName.Text.Trim() != "")
            {

                if (this.NewDeviceType == null)
                {
                    return IfAddEditValid();
                }
                else
                {
                    if (this.NewDeviceType.Name.ToLower() == this.tb_DeviceTypeName.Text.ToLower())
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
                MessageBox.Show("Device type name is required!");
            }

            return false;
        }

        private bool IfAddEditValid()
        {
            if (DatabaseController.CheckIfDeviceTypeIsUnique(this.tb_DeviceTypeName.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Device type name is not unique!");
            }
            return false;
        }

        private void bt_AcceptAdd_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                this.NewDeviceType = new DeviceType(this.tb_DeviceTypeName.Text);
                this.DialogResult = true;
            }
        }

        private void bt_AcceptEdit_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                this.NewDeviceType.Name = this.tb_DeviceTypeName.Text;
                this.DialogResult = true;
            }
        }
    }
}
