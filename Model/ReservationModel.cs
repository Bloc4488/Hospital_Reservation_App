using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    public class ReservationModel
    {
        public DateTime res { get; set; }
        public int pacient_id { get; set; }
        public int doctor_id { get; set; }
    }
}
