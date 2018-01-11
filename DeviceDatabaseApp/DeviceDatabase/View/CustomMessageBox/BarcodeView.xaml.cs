using DeviceDatabase.Controller;
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
    /// Interaction logic for BarcodeView.xaml
    /// </summary>
    public partial class BarcodeView : Window
    {
        public BarcodeView(Window _Owner, BitmapImage _GeneratedBarcode, string _DeviceName)
        {
            InitializeComponent();

            this.Owner = _Owner;
            this.Title = string.Format("Editing {0}", _DeviceName);

            this.img_GeneratedBarcode.Width = _GeneratedBarcode.Width;
            this.img_GeneratedBarcode.Height = _GeneratedBarcode.Height;
            Console.WriteLine(_GeneratedBarcode.Width + " " + _GeneratedBarcode.Height);

            this.img_GeneratedBarcode.Source = _GeneratedBarcode;
        }

        private void bt_Save_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
