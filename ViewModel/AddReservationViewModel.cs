using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hospital_Reservation_App.ViewModel
{
    public class AddReservationViewModel : ViewModelBase
    {
        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged(nameof(IsVisible));
                }
            }
        }

        public ICommand AddReservationCommand { get; }
        public AddReservationViewModel()
        {
            AddReservationCommand = new ViewModelCommand(ExecuteAddReservationCommand, CanExecuteAddReservationCommand);
        }
        private bool CanExecuteAddReservationCommand(object obj)
        {
            bool validAdd = true;
            return validAdd;
        }
        private void ExecuteAddReservationCommand(object obj)
        {

        }
    }
}
