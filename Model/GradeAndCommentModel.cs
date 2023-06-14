using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    /// <summary>
    /// Class represents grades_and_comments table of DataBase.
    /// </summary>
    public class GradeAndCommentModel
    {
        public string iD { get; set; }

        public string ReservationID { get; set; }   
        public string grade { get; set;}

        public string comment { get; set;}
    }
}
