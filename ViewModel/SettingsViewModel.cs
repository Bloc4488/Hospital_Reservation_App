using Hospital_Reservation_App.Model;
using Hospital_Reservation_App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private string _errorMessageChangeData;

        private System.Windows.Visibility _isCheckPasswordVisible = System.Windows.Visibility.Collapsed;
        private System.Windows.Visibility _isChangeDataVisible = System.Windows.Visibility.Collapsed;

        public UserModel CurrentAccount
        {
            get { return _currentAccount; }
            set
            {
                _currentAccount = value;
                OnPropertyChanged(nameof(CurrentAccount));
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
        public System.Windows.Visibility IsCheckPasswordVisible
        {
            get { return _isCheckPasswordVisible; }
            set 
            {
                if (_isCheckPasswordVisible != value)
                {
                    _isCheckPasswordVisible = value;
                    OnPropertyChanged(nameof(IsCheckPasswordVisible));
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
        public ICommand ChangeDataUserCommand { get; }
        public ICommand ChangeUserPasswordCommand { get; }
        public ICommand DeleteReservationsUserCommand { get; }
        public ICommand DeleteUserAccountCommand { get; }

        public ICommand ShowChangeDataUserCommand { get; }
        public ICommand ShowChangeUserPasswordCommand { get; }
        public ICommand ShowDeleteReservationsUserCommand { get; }
        public ICommand ShowDeleteUserAccountCommand { get; }

        private IUserRepository userRepository;
        public SettingsViewModel()
        {
            userRepository = new UserRepository();
            CurrentAccount = new UserModel();
            ShowChangeDataUserCommand = new ViewModelCommand(ExecuteShowChangeDataUserCommand);
            ChangeDataUserCommand = new ViewModelCommand(ExecuteChangeDataUserCommand, CanExecuteChangeDataUserCommand);
            LoadCurrentAccountData();
        }

        private void ExecuteShowChangeDataUserCommand(object obj)
        {
            //IsCheckPasswordVisible = Visibility.Visible;
            IsChangeDataVisible = System.Windows.Visibility.Visible;
        }

        private bool CanExecuteChangeDataUserCommand(object obj)
        {
            bool validChange;
            string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
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
            else if (CurrentAccount.email != null && userRepository.checkMail(CurrentAccount.email))
            {
                ErrorMessageChangeData = "Dana poczta jest juz zarejestrowana!";
                validChange = false;
            }
            else if (CurrentAccount.PESEL == null)
            {
                ErrorMessageChangeData = "PESEl nie może byc pusty";
                validChange = false;
            }
            else if (CurrentAccount.PESEL != null && !userRepository.checkPeselLength(CurrentAccount.PESEL))
            {
                ErrorMessageChangeData = "PESEl musi być 11 znaków!";
                validChange = false;
            }
            else if (userRepository.checkPeselUser(CurrentAccount.PESEL))
            {
                ErrorMessageChangeData = "Taki PESEL jest już zarejestrowany!";
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
            //IsCheckPasswordVisible = Visibility.Visible;
            //IsChangeDataVisible = System.Windows.Visibility.Visible;
            userRepository.Update(CurrentAccount);
            IsChangeDataVisible = System.Windows.Visibility.Collapsed;
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
