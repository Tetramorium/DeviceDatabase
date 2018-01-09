using DeviceDatabase.Controller;
using DeviceDatabase.Model;
using DeviceDatabase.Model.Authentication;
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

namespace DeviceDatabase.View
{

    public interface IView
    {
        IViewModel ViewModel
        {
            get;
            set;
        }

        void Show();
    }

    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window, IView
    {
        public Login(AuthenticationViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseController.InitialSetup();
        }

        private void bt_AdministratorLogin_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow mw = new MainWindow();
            //mw.Show();
            //this.Close();
        }

        private void bt_ManagerLogin_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow mw = new MainWindow();
            //mw.Show();
            //this.Close();
        }

        public IViewModel ViewModel
        {
            get { return DataContext as IViewModel; }
            set { DataContext = value; }
        }
    }
}
