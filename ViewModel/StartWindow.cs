using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hospital_Reservation_App.ViewModel
{
    public class StartWindow : ViewModelBase
    {
        private string _txt;
        private SecureString _mytxt;
        private bool _loginAttempt = false;
        public ICommand command { get; }

        public string txt
        {
            get { return _txt; }
            set
            {
                if (txt != value)
                {
                    _txt = value;
                    OnPropertyChanged(nameof(txt));
                }
            }
        }
        public SecureString mytxt
        {
            get { return _mytxt; }
            set
            {
                    _mytxt = value;
                    OnPropertyChanged(nameof(mytxt));
            }
        }
        public StartWindow()
        {
            command = new ViewModelCommand(execute, canExecute);
        }

        private bool canExecute(object obj)
        {
            if (SecureStringToString(mytxt) == "123" || _loginAttempt == false)
            {
                _loginAttempt = false;
                return true;
            }
            else
            {
                _loginAttempt = true;
                txt = "Idi w pizdu!!!!";
            }
            return false;
        }

        private void execute(object obj)
        {
            //txt = "Pizda";
            
        }

        private string SecureStringToString(SecureString pass)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(pass);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
