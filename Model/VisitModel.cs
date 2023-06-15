using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    /// <summary>
    /// Class represents visit data of reservation.
    /// </summary>
    public class VisitModel
    {
        public string ReservationId { get; set; }
        public UserModel Patient { get; set; }
        public DoctorModel Doctor { get; set; }
        public DateTime ReservationTime { get; set; }
    }
}
