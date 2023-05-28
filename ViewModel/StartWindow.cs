using Hospital_Reservation_App.Model;
using Hospital_Reservation_App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net;
using System.Security.Principal;
using System.Threading;

namespace Hospital_Reservation_App.ViewModel
{
    public class StartWindow : ViewModelBase
    {
        private string _username;
        private SecureString _password;
        private string _errorMessage = "";
        private string _errorMessage2 = "";
        private bool _isViewVisible = true;

        public string username
        {
            get { return _username; }
            set
            {
                if (username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(username));
                }
            }
        }
        public SecureString password
        {
            get { return _password; }
            set
            {
                _password = value; 
                OnPropertyChanged(nameof(password));
            }
        }
        public string errorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(errorMessage));
                }

            }
        }
        public string errorMessage2
        {
            get { return errorMessage2; }
            set
            {
                if (_errorMessage2 != value)
                {
                    _errorMessage2 = value;
                    OnPropertyChanged(nameof(errorMessage2));
                }
            }
        }
        public bool isViewVisible
        {
            get { return _isViewVisible; }
            set
            {
                if (_isViewVisible != value)
                {
                    _isViewVisible = value;
                    OnPropertyChanged(nameof(isViewVisible));
                }
            }
        }

        private IUserRepository userRepository;

        public ICommand LoginCommand { get; }

        public StartWindow()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(username))
            {
                errorMessage = "Login nie może być pusty!";
                validData = false;
            }
            else if (password == null)
            {
                errorMessage = "Hasło nie może być puste!";
                validData = false;
            }
            else
            {
                errorMessage = "";
                validData = true;
            }
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            if (userRepository.AuthentificateUser(new NetworkCredential(username, password)))
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(username), null);
                isViewVisible = false;
            }
            else
            {
                errorMessage2 = "Ly login lub hasło"; 
            }
        }
    }
}
