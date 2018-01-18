using DeviceDatabase.Controller;
using DeviceDatabase.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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
            this.Title = string.Format("Adding calamity for {0}", _DeviceName);
        }

        public AddCalamityView(Window _Owner, Calamity _OldCalamity)
        {
            InitializeComponent();

            this.Owner = _Owner;
            this.Title = string.Format("Editing calamity for {0}", _OldCalamity.Device.Name);

            this.NewCalamity = _OldCalamity;
            this.bt_Accept.Click -= bt_AcceptAdd_Click;
            this.bt_Accept.Click += bt_AcceptEdit_Click;

            if (!string.IsNullOrEmpty(_OldCalamity.About))
                this.rtb_Description.Document = XamlReader.Parse(_OldCalamity.About) as FlowDocument;

            this.dp_Date.SelectedDate = _OldCalamity.Date;
        }

        private void bt_AcceptAdd_Click(object sender, RoutedEventArgs e)
        {
            if(this.dp_Date.SelectedDate != null)
            {
                string about = RichTextBoxController.ConvertFlowDocumentToString(rtb_Description.Document);
                this.NewCalamity = new Calamity(about, this.dp_Date.SelectedDate.Value);
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
                string about = RichTextBoxController.ConvertFlowDocumentToString(rtb_Description.Document);
                this.NewCalamity.About = about;
                this.NewCalamity.Date = this.dp_Date.SelectedDate.Value;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Select a valid date");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = NewCalamity;
        }
    }
}
