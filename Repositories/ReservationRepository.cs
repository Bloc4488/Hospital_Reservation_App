using Hospital_Reservation_App.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Repositories
{
    public class ReservationRepository : DataBaseRepository, IReservationRepository
    {
        public void AddRes(int reservation_id, int doctor_id, DateTime res)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO reservations VALUES (@dr_id, @date)";
                    // command.Parameters.Add("@res_id", MySqlDbType.VarChar).Value = reservation_id;
                    command.Parameters.Add("@dr_id", MySqlDbType.Int32).Value = doctor_id;
                    command.Parameters.Add("@date", MySqlDbType.DateTime).Value = res;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAllReservationuser(UserModel userModel)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM reservations WHERE patient_id = @patient";
                    command.Parameters.Add("@patient", MySqlDbType.VarChar).Value = userModel.id;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
