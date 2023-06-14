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
using Hospital_Reservation_App.View;

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
        private string _errorMessageLogin2;
        private string _errorMessageRegistration;
        
        private bool _isViewVisible = true;
        private bool _isRegViewVisible = false;

        private bool _checkBoxMaleChecked = true;
        private bool _checkBoxFemaleChecked;

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
        public string ErrorMessageLogin2
        {
            get { return _errorMessageLogin2; }
            set
            {
                if (_errorMessageLogin2 != value)
                {
                    _errorMessageLogin2 = value;
                    OnPropertyChanged(nameof(ErrorMessageLogin2));
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
        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set
            {
                if (_isViewVisible != value)
                {
                    _isViewVisible = value;
                    OnPropertyChanged(nameof(IsViewVisible));
                }
            }
        }

        public bool IsRegViewVisible
        {
            get { return _isRegViewVisible; }
            set
            {
                if (_isRegViewVisible != value)
                {
                    _isRegViewVisible = value;
                    OnPropertyChanged(nameof(IsRegViewVisible));
                }
            }
        }

        public bool CheckBoxMaleChecked
        {
            get { return _checkBoxMaleChecked; }
            set
            {
                _checkBoxMaleChecked = value;
                if (value)
                {
                    CheckBoxFemaleChecked = false;
                }
                OnPropertyChanged(nameof(CheckBoxMaleChecked));
            }
        }

        public bool CheckBoxFemaleChecked
        {
            get { return _checkBoxFemaleChecked; }
            set
            {
                _checkBoxFemaleChecked = value;
                if (value)
                {
                    CheckBoxMaleChecked = false;
                }
                OnPropertyChanged(nameof(CheckBoxFemaleChecked));
            }
        }

        private readonly IUserRepository userRepository;

        public ICommand LoginCommand { get; }
        public ICommand RegistrationCommand { get; }

        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RegistrationCommand = new ViewModelCommand(ExecuteRegistrationCommand, CanExecuteRegistrationCommand);
        }

        private void RaiseLoginCompleted()
        {
            MainWindowView main = new MainWindowView();
            main.Show();
            IsViewVisible = false;
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
            else if (email != null && userRepository.checkMail(email))
            {
                ErrorMessageRegistration = "Dana poczta jest juz zarejestrowana!";
                validCreate = false;
            }
            else if (pesel == null)
            {
                ErrorMessageRegistration = "PESEl nie może byc pusty";
                validCreate = false;
            }
            else if (pesel != null && !userRepository.checkPeselLength(pesel))
            {
                ErrorMessageRegistration = "PESEl musi być 11 znaków!";
                validCreate = false;
            }
            else if (userRepository.checkPeselUser(pesel))
            {
                ErrorMessageRegistration = "Taki PESEL jest już zarejestrowany!";
                validCreate = false;
            }
            else if (password == null || password.Length < 6 || passwordRep == null || passwordRep.Length < 6)
            {
                ErrorMessageRegistration = "Hasła powinny być więcej niż 6 znaków!";
                validCreate = false;
            }
            else
            {
                if (!userRepository.checkPassRepeat(password, passwordRep))
                {
                    ErrorMessageRegistration = "Niepoprawne drugie hasło!";
                    validCreate = false;
                }
                else
                {
                    ErrorMessageRegistration = "";
                    validCreate = true;
                }
            }
            return validCreate;
        }

        private void ExecuteRegistrationCommand(object obj)
        {
            UserModel user = new UserModel
            {
                firstName = firstname,
                lastName = lastname,
                email = email,
                PESEL = pesel,
                Password = password
            };
            if (CheckBoxMaleChecked)
                user.sex = "M";
            else if (CheckBoxFemaleChecked)
                user.sex = "F";
            userRepository.Add(user);
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
                RaiseLoginCompleted();
            }
            else
            {
                ErrorMessageLogin2 = "Zły email lub hasło"; 
            }
        }
    }
}
