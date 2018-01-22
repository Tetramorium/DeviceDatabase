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
    /// Interaction logic for SolveCalamityView.xaml
    /// </summary>
    public partial class SolveCalamityView : Window
    {
        public Calamity Calamity { get; set; }

        public SolveCalamityView(Window _Owner, Calamity _Calamity)
        {
            this.Owner = _Owner;

            InitializeComponent();

            this.Calamity = _Calamity;

            this.dp_Date.SelectedDate = DateTime.Today;

            DateTime d = Calamity.Date;
            this.dp_Date.DisplayDateStart = d;

            this.DataContext = Calamity;
        }

        private void bt_Accept_Click(object sender, RoutedEventArgs e)
        {
            if (this.dp_Date.SelectedDate != null && this.dp_Date.SelectedDate >= this.Calamity.Date)
            {
                this.Calamity.IsSolvedDate = this.dp_Date.SelectedDate.Value;

                string solution = RichTextBoxController.ConvertFlowDocumentToString(rtb_Solution.Document);
                this.Calamity.IsSolvedSolution = solution;

                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Select a valid date");
            }
        }
    }
}
