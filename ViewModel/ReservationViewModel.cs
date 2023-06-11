using Hospital_Reservation_App.Model;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.ViewModel
{
    public class ReservationViewModel : ViewModelBase
    {
        public class BoolStringClass
        {
            public string TheText { get; set; }
            public string TheValue { get; set; }
        }
        private readonly UserModel _currentAccount;

        private List<ReservationModel> _listReservationsUser;


    }
}
