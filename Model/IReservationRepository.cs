using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    /// <summary>
    /// Interface represents methods user add reservation
    /// user delete selected reservation
    /// user delete all reservations
    /// List of all reservations
    /// List of past reservations
    /// List of future reservations
    /// </summary>
    interface IReservationRepository
    {
       void AddRes(ReservationModel reservation);
       void DeleteReservation(ReservationModel reservation);
       void DeleteAllReservationuser(UserModel userModel);
       List<ReservationModel> GetAllReservationsData(UserModel user);
       List<ReservationModel> GetPastReservationsData(UserModel user);
       List<ReservationModel> GetFutureReservationsData(UserModel user);
       List<DateTime> GetUserTime(DateTime date, UserModel user);
    }
}
