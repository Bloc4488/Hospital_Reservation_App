using Hospital_Reservation_App.Model;
using Hospital_Reservation_App.Repositories;
using Hospital_Reservation_App.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Hospital_Reservation_App.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private UserModel _currentAccount;
        private bool _checkBoxMaleChecked;
        private bool _checkBoxFemaleChecked;
        private SecureString _passwordOld;
        private SecureString _passwordNew;
        private SecureString _passwordRepNew;
        private SecureString _passwordCheck;
        private SecureString _passwordDeleteUser;

        private string _errorMessageChangeData;
        private string _errorMessageChangePassword;
        private string _errorMessageChangePassword2;
        private string _errorMessageDeleteReservations;
        private string _errorMessageDeleteUser;

        private System.Windows.Visibility _isChangePasswordVisible = System.Windows.Visibility.Collapsed;
        private System.Windows.Visibility _isChangeDataVisible = System.Windows.Visibility.Collapsed;
        private System.Windows.Visibility _isDeleteReservationsVisible = System.Windows.Visibility.Collapsed;
        private System.Windows.Visibility _isDeleteUserVisible = System.Windows.Visibility.Collapsed;

        public UserModel CurrentAccount
        {
            get { return _currentAccount; }
            set
            {
                _currentAccount = value;
                OnPropertyChanged(nameof(CurrentAccount));
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
        public SecureString passwordNew
        {
            get { return _passwordNew; }
            set
            {
                if (_passwordNew != value)
                {
                    _passwordNew = value;
                    OnPropertyChanged(nameof(passwordNew));
                }
            }
        }
        public SecureString passwordOld
        {
            get { return _passwordOld; }
            set
            {
                if (_passwordOld != value)
                {
                    _passwordOld = value;
                    OnPropertyChanged(nameof(passwordOld));
                }
            }
        }
        public SecureString passwordRepNew
        {
            get { return _passwordRepNew; }
            set
            {
                if (_passwordRepNew != value)
                {
                    _passwordRepNew = value;
                    OnPropertyChanged(nameof(passwordRepNew));
                }
            }
        }
        public SecureString passwordCheck
        {
            get { return _passwordCheck; }
            set
            {
                if (_passwordCheck != value)
                {
                    _passwordCheck = value;
                    OnPropertyChanged(nameof(passwordCheck));
                }
            }
        }
        public SecureString passwordDeleteUser
        {
            get { return _passwordDeleteUser; }
            set
            {
                if (_passwordDeleteUser != value)
                {
                    _passwordDeleteUser = value;
                    OnPropertyChanged(nameof(passwordDeleteUser));
                }
            }
        }
        public string ErrorMessageChangeData
        {
            get { return _errorMessageChangeData; }
            set
            {
                if (_errorMessageChangeData != value)
                {
                    _errorMessageChangeData = value;
                    OnPropertyChanged(nameof(ErrorMessageChangeData));
                }
            }
        }
        public string ErrorMessageChangePassword
        {
            get { return _errorMessageChangePassword; }
            set
            {
                if (_errorMessageChangePassword != value)
                {
                    _errorMessageChangePassword = value;
                    OnPropertyChanged(nameof(ErrorMessageChangePassword));
                }
            }
        }
        public string ErrorMessageChangePassword2
        {
            get { return _errorMessageChangePassword2; }
            set
            {
                if (_errorMessageChangePassword2 != value)
                {
                    _errorMessageChangePassword2 = value;
                    OnPropertyChanged(nameof(ErrorMessageChangePassword2));
                }
            }
        }
        public string ErrorMessageDeleteReservations
        {
            get { return _errorMessageDeleteReservations; }
            set
            {
                if (_errorMessageDeleteReservations != value)
                {
                    _errorMessageDeleteReservations = value;
                    OnPropertyChanged(nameof(ErrorMessageDeleteReservations));
                }
            }
        }
        public string ErrorMessageDeleteUser
        {
            get { return _errorMessageDeleteUser; }
            set
            {
                if (_errorMessageDeleteUser != value)
                {
                    _errorMessageDeleteUser = value;
                    OnPropertyChanged(nameof(ErrorMessageDeleteUser));
                }
            }
        }
        public System.Windows.Visibility IsChangePasswordVisible
        {
            get { return _isChangePasswordVisible; }
            set 
            {
                if (_isChangePasswordVisible != value)
                {
                    _isChangePasswordVisible = value;
                    OnPropertyChanged(nameof(IsChangePasswordVisible));
                }
            }
        }
        public System.Windows.Visibility IsChangeDataVisible
        {
            get { return _isChangeDataVisible; }
            set
            {
                if (_isChangeDataVisible != value)
                {
                    _isChangeDataVisible = value;
                    OnPropertyChanged(nameof(IsChangeDataVisible));
                }
            }
        }
        public System.Windows.Visibility IsDeleteReservationsVisible
        {
            get { return _isDeleteReservationsVisible; }
            set
            {
                if (_isDeleteReservationsVisible != value)
                {
                    _isDeleteReservationsVisible = value;
                    OnPropertyChanged(nameof(IsDeleteReservationsVisible));
                }
            }
        }
        public System.Windows.Visibility IsDeleteUserVisible
        {
            get { return _isDeleteUserVisible; }
            set
            {
                if (_isDeleteUserVisible != value)
                {
                    _isDeleteUserVisible = value;
                    OnPropertyChanged(nameof(IsDeleteUserVisible));
                }
            }
        }
        public ICommand ChangeDataUserCommand { get; }
        public ICommand ChangeUserPasswordCommand { get; }
        public ICommand DeleteReservationsUserCommand { get; }
        public ICommand DeleteUserAccountCommand { get; }

        public ICommand ShowChangeDataUserCommand { get; }
        public ICommand ShowChangeUserPasswordCommand { get; }
        public ICommand ShowDeleteReservationsUserCommand { get; }
        public ICommand ShowDeleteUserAccountCommand { get; }

        private IUserRepository userRepository;
        private IReservationRepository reservationRepository;
        public SettingsViewModel()
        {
            userRepository = new UserRepository();
            reservationRepository = new ReservationRepository();
            CurrentAccount = new UserModel();
            ShowChangeDataUserCommand = new ViewModelCommand(ExecuteShowChangeDataUserCommand);
            ChangeDataUserCommand = new ViewModelCommand(ExecuteChangeDataUserCommand, CanExecuteChangeDataUserCommand);
            ShowChangeUserPasswordCommand = new ViewModelCommand(ExecuteShowChangeUserPasswordCommand);
            ChangeUserPasswordCommand = new ViewModelCommand(ExecuteChangeUserPasswordCommand, CanExecuteChangeUserPasswordCommand);
            ShowDeleteReservationsUserCommand = new ViewModelCommand(ExecuteShowDeleteReservationsUserCommand);
            DeleteReservationsUserCommand = new ViewModelCommand(ExecuteDeleteReservationsUserCommand);
            ShowDeleteUserAccountCommand = new ViewModelCommand(ExecuteShowDeleteUserAccountCommand);
            DeleteUserAccountCommand = new ViewModelCommand(ExecuteDeleteUserAccountCommand);
            LoadCurrentAccountData();
        }

        private void ExecuteShowChangeDataUserCommand(object obj)
        {
            IsChangeDataVisible = System.Windows.Visibility.Visible;
            IsChangePasswordVisible = System.Windows.Visibility.Collapsed;
            IsDeleteReservationsVisible = System.Windows.Visibility.Collapsed;
            IsDeleteUserVisible = System.Windows.Visibility.Collapsed;
            if (CurrentAccount.sex == "M")
                CheckBoxMaleChecked = true;
            else CheckBoxFemaleChecked = true;
        }

        private bool CanExecuteChangeDataUserCommand(object obj)
        {
            bool validChange;
            string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            string mail = CurrentAccount.email;
            if (string.IsNullOrEmpty(CurrentAccount.firstName) || CurrentAccount.firstName.Length < 3)
            {
                ErrorMessageChangeData = "Imię musi być więcej 3 znaków!";
                validChange = false;
            }
            else if (string.IsNullOrEmpty(CurrentAccount.lastName) || CurrentAccount.lastName.Length < 3)
            {
                ErrorMessageChangeData = "Nazwisko musi być więcej 3 znaków!";
                validChange = false;
            }
            else if (string.IsNullOrEmpty(CurrentAccount.email) || !regex.Match(CurrentAccount.email).Success)
            {
                ErrorMessageChangeData = "Niepoprawna poczta!";
                validChange = false;
            }
            else if (CurrentAccount.email != null && userRepository.checkMail(CurrentAccount.email) && CurrentAccount.email != Thread.CurrentPrincipal.Identity.Name)
            {
                ErrorMessageChangeData = "Dana poczta jest juz zarejestrowana!";
                validChange = false;
            }
            else
            {
                validChange = true;
                ErrorMessageChangeData = "";
            }
            return validChange;
        }

        private void ExecuteChangeDataUserCommand(object obj)
        {
            if (CheckBoxMaleChecked)
                CurrentAccount.sex = "M";
            else
                CurrentAccount.sex = "F";
            userRepository.Update(CurrentAccount);
            IsChangeDataVisible = System.Windows.Visibility.Collapsed;
            Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(CurrentAccount.email), null);
        }

        private void ExecuteShowChangeUserPasswordCommand(object obj)
        {
            IsChangePasswordVisible = System.Windows.Visibility.Visible;
            IsDeleteReservationsVisible = System.Windows.Visibility.Collapsed;
            IsChangeDataVisible = System.Windows.Visibility.Collapsed;
            IsDeleteUserVisible = System.Windows.Visibility.Collapsed;
        }

        private bool CanExecuteChangeUserPasswordCommand(object obj)
        {
            bool validChangePassword;
            if (passwordOld == null || passwordOld.Length < 6)
            {
                validChangePassword = false;
                ErrorMessageChangePassword = "Stare hasło powinno być więcej niż 6 znaków!";
            }
            else if (passwordNew == null || passwordNew.Length < 6 || passwordRepNew == null || passwordRepNew.Length < 6)
            {
                validChangePassword = false;
                ErrorMessageChangePassword = "Nowe hasła powinny być więcej niż 6 znaków!";
            }
            else
            {
                if (!userRepository.checkPassRepeat(passwordNew, passwordRepNew))
                {
                    validChangePassword = false;
                    ErrorMessageChangePassword = "Niepoprawne drugie hasło!";
                }
                else
                { 
                    validChangePassword = true;
                    ErrorMessageChangePassword = "";
                }
            }
            return validChangePassword;
        }
        //TODO: fix bug passwordBox
        private void ExecuteChangeUserPasswordCommand(object obj)
        {
            if (!userRepository.checkOldPassword(new NetworkCredential(null, passwordOld), CurrentAccount))
            {
                ErrorMessageChangePassword2 = "Niepoprawne stare hasło!";
            }
            else
            {
                ErrorMessageChangePassword2 = "";
                CurrentAccount.Password = passwordNew;
                userRepository.UpdatePassword(CurrentAccount);
                passwordNew = new SecureString();
                passwordOld = new SecureString();
                passwordRepNew = new SecureString();
                IsChangePasswordVisible = System.Windows.Visibility.Collapsed;
                LoadCurrentAccountData();
            }
            
        }

        private void ExecuteShowDeleteReservationsUserCommand(object obj)
        {
            IsDeleteReservationsVisible = System.Windows.Visibility.Visible;
            IsChangePasswordVisible = System.Windows.Visibility.Collapsed;
            IsChangeDataVisible = System.Windows.Visibility.Collapsed;
            IsDeleteUserVisible = System.Windows.Visibility.Collapsed;
        }

        private void ExecuteDeleteReservationsUserCommand(object obj)
        {
            if (!userRepository.checkOldPassword(new NetworkCredential(null, passwordCheck), CurrentAccount))
            {
                ErrorMessageDeleteReservations = "Niepoprawne hasło!";
            }
            else
            {
                ErrorMessageDeleteReservations = "";
                reservationRepository.DeleteAllReservationuser(CurrentAccount);
                IsDeleteReservationsVisible = System.Windows.Visibility.Collapsed;
            }

        }

        private void ExecuteShowDeleteUserAccountCommand(object obj)
        {
            IsDeleteUserVisible = System.Windows.Visibility.Visible;
            IsDeleteReservationsVisible = System.Windows.Visibility.Collapsed;
            IsChangePasswordVisible = System.Windows.Visibility.Collapsed;
            IsChangeDataVisible = System.Windows.Visibility.Collapsed;
        }

        private void ExecuteDeleteUserAccountCommand(object obj)
        {
            if (!userRepository.checkOldPassword(new NetworkCredential(null, passwordDeleteUser), CurrentAccount))
            {
                ErrorMessageDeleteReservations = "Niepoprawne hasło!";
            }
            else
            {
                ErrorMessageDeleteReservations = "";
                userRepository.Delete(CurrentAccount);
                IsDeleteReservationsVisible = System.Windows.Visibility.Collapsed;
                LogOut();
            }
        }

        private void LogOut()
        {
            Application.Current.Shutdown();
        }

        private void LoadCurrentAccountData()
        {
            var user = userRepository.GetUser(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentAccount.id = user.id;
                CurrentAccount.PESEL = user.PESEL;
                CurrentAccount.firstName = user.firstName;
                CurrentAccount.lastName = user.lastName;
                CurrentAccount.sex = user.sex;
                CurrentAccount.email = user.email;
                CurrentAccount.Password = user.Password;
            }
            else
            {
                //TODO
                MessageBox.Show("User not logged in!");
            }
        }
    }
}
