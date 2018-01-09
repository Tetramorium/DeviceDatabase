using DeviceDatabase.Controller.Authentication;
using DeviceDatabase.Model.Authentication;
using DeviceDatabase.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DeviceDatabase
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Login loginWindow;

        protected override void OnStartup(StartupEventArgs e)
        {

            //Create a custom principal with an anonymous identity at startup
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);

            base.OnStartup(e);

            //Show the login view
            AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());

            viewModel.LoggingOut += ViewModel_LoggingOut;
            viewModel.LoggingIn += ViewModel_LoggingIn;

            loginWindow = new Login(viewModel);
            loginWindow.Show();

        }

        private void ViewModel_LoggingIn(object sender, EventArgs e)
        {
            loginWindow.Hide();
        }

        private void ViewModel_LoggingOut(object sender, EventArgs e)
        {
            loginWindow.Show();
        }
    }
}
