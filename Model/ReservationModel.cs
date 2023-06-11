using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    public class ReservationModel
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public DoctorModel Doctor { get; set; }
        public DateTime ReservationTime { get; set; }
    }
}
