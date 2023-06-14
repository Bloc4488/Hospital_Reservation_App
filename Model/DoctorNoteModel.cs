using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    public class DoctorNoteModel
    {
        public string Id { get; set; }
        public string Reservation_id { get; set; }
        public string Note { get; set; }
    }
}
