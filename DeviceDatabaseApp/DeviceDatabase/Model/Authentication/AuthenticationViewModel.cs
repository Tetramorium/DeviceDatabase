using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using System.Security;
using DeviceDatabase.Controller.Authentication;
using System.Collections.Generic;
using DeviceDatabase.View;

namespace DeviceDatabase.Model.Authentication
{
    public interface IViewModel { }

    public class AuthenticationViewModel : IViewModel
    {
        private readonly IAuthenticationService _authenticationService;

        private MainWindow mainWindow;

        public event EventHandler LoggingOut;
        public event EventHandler LoggingIn;

        private readonly DelegateCommand _loginAdministratorCommand;
        private readonly DelegateCommand _loginManagerCommand;

        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _loginAdministratorCommand = new DelegateCommand(LoginAdministrator, CanLogin);
            _loginManagerCommand = new DelegateCommand(LoginManager, CanLogin);
        }

        public string AuthenticatedUser
        {
            get
            {
                if (IsAuthenticated)
                    return string.Format("Signed in as {0}. {1}",
                          Thread.CurrentPrincipal.Identity.Name,
                          Thread.CurrentPrincipal.IsInRole("Administrators") ? "You are an administrator!"
                              : "You are NOT a member of the administrators group.");

                return "Not authenticated!";
            }
        }

        public DelegateCommand LoginAdministratorCommand { get { return _loginAdministratorCommand; } }
        public DelegateCommand LoginManagerCommand { get { return _loginManagerCommand; } }

        private void LoginAdministrator(object parameter)
        {
            Login("Administrator", new List<string> { "Administrator" });
        }

        private void LoginManager(object parameter)
        {
            Login("Manager", new List<string> { "Manager" });
        }

        private void Login(string _Name, List<string> _Roles)
        {
            //Get the current principal object
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal == null)
                throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

            //Authenticate the user
            customPrincipal.Identity = new CustomIdentity(_Name, "", _Roles);

            mainWindow = new MainWindow();
            mainWindow.LoggingOut += MainWindow_LoggingOut;
            mainWindow.Show();

            _loginAdministratorCommand.RaiseCanExecuteChanged();
            _loginManagerCommand.RaiseCanExecuteChanged();

            this.LoggingIn?.Invoke(this, new EventArgs());
        }

        private void MainWindow_LoggingOut(object sender, EventArgs e)
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
                _loginAdministratorCommand.RaiseCanExecuteChanged();
                _loginManagerCommand.RaiseCanExecuteChanged();
                mainWindow.Close();
                this.LoggingOut?.Invoke(this, new EventArgs());
            }
        }

        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }

        private bool CanLogout(object parameter)
        {
            return IsAuthenticated;
        }

        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }
    }
}