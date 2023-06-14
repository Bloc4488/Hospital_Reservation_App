using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    interface IGradeAndCommentRepository
    {
        void AddComment(GradeAndCommentModel GradeAndCom);

        GradeAndCommentModel GetComment(VisitModel visit);
    }
}
