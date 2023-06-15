using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hospital_Reservation_App.ViewModel
{
    /// <summary>
    /// Class to represent ViewModelCommand for buttons logic in View
    /// </summary>
    public class ViewModelCommand : ICommand
    {
        private readonly Action<Object> _executeExtion;
        private readonly Predicate<Object> _canExecuteAction;
        public ViewModelCommand(Action<object> executeExtion)
        {
            _executeExtion = executeExtion;
            _canExecuteAction = null;
        }
        public ViewModelCommand(Action<object> executeExtion, Predicate<object> canExecuteAction)
        {
            _executeExtion = executeExtion;
            _canExecuteAction = canExecuteAction;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter) 
        {
            return _canExecuteAction == null ? true : _canExecuteAction(parameter);
        }
        public void Execute(object parameter) 
        {
            _executeExtion(parameter);
        }
    }
}
