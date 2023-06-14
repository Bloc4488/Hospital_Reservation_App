using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    /// <summary>
    /// Interface represents method showing doctor notes to pacient
    /// </summary>
    public interface IDoctorNoteRepository
    {
        DoctorNoteModel GetDoctorNote(ReservationModel reservation);
        void AddDoctorNote(DoctorNoteModel doctorNote);
    }
}
