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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeviceDatabase.View.CustomMessageBox
{
    /// <summary>
    /// Interaction logic for CalamityView.xaml
    /// </summary>
    public partial class CalamityView : Window
    {
        //http://www.wpf-tutorial.com/dialogs/creating-a-custom-input-dialog/

        private Calamity calamity;

        public CalamityView(Window _Owner, Calamity _Calamity)
        {
            this.Owner = _Owner;
            InitializeComponent();

            this.calamity = _Calamity;

            //https://msdn.microsoft.com/en-us/library/system.string.isnullorempty(v=vs.110).aspx
            if (!string.IsNullOrEmpty(this.calamity.IsSolvedSolution))           
                this.rtb_Solution.Document = RichTextBoxController.ConvertStringToFlowDocument(this.calamity.IsSolvedSolution);

            if (!string.IsNullOrEmpty(this.calamity.About))
                this.rtb_About.Document = RichTextBoxController.ConvertStringToFlowDocument(this.calamity.About);
            //if (this.calamity.IsSolvedDate != null)
            //    this.lbl_Solved.Content = this.calamity.IsSolvedDate.Value.ToString("dd-MM-yyyy");


            DataContext = calamity;
        }
    }
}
