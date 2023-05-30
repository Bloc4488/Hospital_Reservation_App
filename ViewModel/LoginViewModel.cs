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
using System.Text.RegularExpressions;

namespace Hospital_Reservation_App.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string _email;
        private string _firstname;
        private string _lastname;
        private SecureString _password;
        private SecureString _passwordRep;
        private SecureString _pesel;
        private string _errorMessageLogin;
        private string _errorMessageRegistration;
        private bool _isViewVisible = true;
        private bool _isRegViewVisible = false;

        public string email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(email));
                }
            }
        }
        public string firstname
        {
            get { return _firstname; }
            set
            {
                if (_firstname != value)
                {
                    _firstname = value;
                    OnPropertyChanged(nameof(firstname));
                }
            }
        }
        public string lastname
        {
            get { return _lastname; }
            set 
            {
                if (_lastname != value) 
                { 
                    _lastname = value;
                    OnPropertyChanged(nameof(lastname));
                } 
            }
        }
        public SecureString password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(password));
                }
            }
        }
        public SecureString passwordRep
        {
            get { return _passwordRep; }
            set
            {
                if (_passwordRep != value)
                {
                    _passwordRep = value;
                    OnPropertyChanged(nameof(passwordRep));
                }
            }
        }
        public SecureString pesel
        {
            get { return _pesel; }
            set
            {
                if (_pesel != value)
                _pesel = value;
                OnPropertyChanged(nameof(pesel));
            }
        }
        public string ErrorMessageLogin
        {
            get { return _errorMessageLogin; }
            set
            {
                if (_errorMessageLogin != value)
                {
                    _errorMessageLogin = value;
                    OnPropertyChanged(nameof(ErrorMessageLogin));
                }

            }
        }
        public string ErrorMessageRegistration
        {
            get { return _errorMessageRegistration; }
            set
            {
                if (_errorMessageRegistration != value)
                {
                    _errorMessageRegistration = value;
                    OnPropertyChanged(nameof(ErrorMessageRegistration));
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

        public bool isRegViewVisible
        {
            get { return _isRegViewVisible; }
            set
            {
                if (_isRegViewVisible != value)
                {
                    _isRegViewVisible = value;
                    OnPropertyChanged(nameof(isRegViewVisible));
                }
            }
        }

        private IUserRepository userRepository;

        public ICommand LoginCommand { get; }
        public ICommand RegistrationCommand { get; }

        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private bool CanExecuteRegistrationCommand(object obj)
        {
            bool validCreate;
            string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(firstname) || firstname.Length < 3)
            {
                ErrorMessageRegistration = "Imię musi być więcej 3 znaków!";
                validCreate = false;
            }
            else if (string.IsNullOrEmpty(lastname) || lastname.Length < 3)
            {
                ErrorMessageRegistration = "Nazwisko musi być więcej 3 znaków!";
                validCreate = false;
            }
            else if (string.IsNullOrEmpty(email) || !regex.Match(email).Success)
            {
                ErrorMessageRegistration = "Niepoprawna poczta!";
                validCreate = false;
            }
            
            return validCreate;
        }

        private void ExecuteRegistrationCommand(object obj)
        {

        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            if (string.IsNullOrWhiteSpace(email))
            {
                ErrorMessageLogin = "E-mail nie może być pusty!";
                validData = false;
            }
            else if (!regex.Match(email).Success)
            {
                ErrorMessageLogin = "Niepoprawny e-mail";
                validData = false;
            }
            else if (password == null)
            {
                ErrorMessageLogin = "Hasło nie może być puste!";
                validData = false;
            }
            else
            {
                ErrorMessageLogin = "";
                validData = true;
            }
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            if (userRepository.AuthentificateUser(new NetworkCredential(email, password)))
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(email), null);
                isViewVisible = false;
                ErrorMessageLogin = "";
            }
            else
            {
                ErrorMessageLogin = "Zły email lub hasło"; 
            }
        }
    }
}
