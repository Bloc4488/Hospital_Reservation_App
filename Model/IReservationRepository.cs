using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    interface IReservationRepository
    {
       void AddRes(ReservationModel reservation);
       void DeleteReservation(ReservationModel reservation);
       void DeleteAllReservationuser(UserModel userModel);
       List<ReservationModel> GetAllReservationsData(UserModel user);
       List<ReservationModel> GetPastReservationsData(UserModel user);
       List<ReservationModel> GetFutureReservationsData(UserModel user);
       List<VisitModel> GetAllReservationsData(DoctorModel user);
       List<VisitModel> GetPastReservationsData(DoctorModel user);
       List<VisitModel> GetFutureReservationsData(DoctorModel user);
       List<DateTime> GetUserTime(DateTime date, UserModel user);
    }
}
