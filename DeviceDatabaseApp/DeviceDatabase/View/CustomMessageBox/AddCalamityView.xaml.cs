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
    /// Interaction logic for AddCalamityView.xaml
    /// </summary>
    public partial class AddCalamityView : Window
    {
        public Calamity NewCalamity { get; set; }

        public AddCalamityView(Window _Owner, string _DeviceName)
        {
            InitializeComponent();

            this.dp_Date.SelectedDate = DateTime.Today;
            this.Owner = _Owner;
            this.Title = String.Format("Add calamity for {0}", _DeviceName);
        }

        private void bt_Accept_Click(object sender, RoutedEventArgs e)
        {
            if(this.dp_Date.SelectedDate != null)
            {
                NewCalamity = new Calamity(this.tb_About.Text, this.dp_Date.SelectedDate.Value);
                this.DialogResult = true;
            } else
            {
                MessageBox.Show("Select a valid date");
            }
        }
    }
}
