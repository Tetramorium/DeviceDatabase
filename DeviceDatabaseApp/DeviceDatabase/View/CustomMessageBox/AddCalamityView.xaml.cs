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

        public AddCalamityView(Window _Owner, Calamity _OldCalamity)
        {
            InitializeComponent();

            this.Owner = _Owner;

            this.NewCalamity = _OldCalamity;
            this.bt_Accept.Click -= bt_AcceptAdd_Click;
            this.bt_Accept.Click += bt_AcceptEdit_Click;

            this.tb_About.Text = _OldCalamity.About;
            this.dp_Date.SelectedDate = _OldCalamity.Date;
        }

        private void bt_AcceptAdd_Click(object sender, RoutedEventArgs e)
        {
            if(this.dp_Date.SelectedDate != null)
            {
                this.NewCalamity = new Calamity(this.tb_About.Text, this.dp_Date.SelectedDate.Value);
                this.DialogResult = true;
            } else
            {
                MessageBox.Show("Select a valid date");
            }
        }

        private void bt_AcceptEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.dp_Date.SelectedDate != null)
            {
                this.NewCalamity.About = this.tb_About.Text;
                this.NewCalamity.Date = this.dp_Date.SelectedDate.Value;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Select a valid date");
            }
        }
    }
}
