using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    /// <summary>
    /// Interface represents method of pacient add comment after visited doctor about how it was
    /// </summary>
    interface IGradeAndCommentRepository
    {
        void AddComment(GradeAndCommentModel GradeAndCom);

        GradeAndCommentModel GetComment(VisitModel visit);
    }
}
