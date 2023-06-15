using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    /// <summary>
    /// Class represents reservations table of DataBase.
    /// </summary>
    public class ReservationModel
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public DoctorModel Doctor { get; set; }
        public DateTime ReservationTime { get; set; }
    }
}
