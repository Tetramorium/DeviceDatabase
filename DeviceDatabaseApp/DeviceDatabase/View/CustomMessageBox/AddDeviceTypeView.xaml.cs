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

        private void bt_Accept_Click(object sender, RoutedEventArgs e)
        {
            if (this.tb_DeviceTypeName.Text.Trim() != "")
            {
                if (DatabaseController.CheckIfDeviceTypeIsUnique(this.tb_DeviceTypeName.Text))
                {
                    this.NewDeviceType = new DeviceType(this.tb_DeviceTypeName.Text);
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Device type name is not unique!");
                }
            }
            else
            {
                MessageBox.Show("Device type name is required!");
            }
        }
    }
}
