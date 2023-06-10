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
    public class ReservationRepository : DataBaseRepository
    {
        public void Add(int doctor_id, DateTime res)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO reservations(reservation_id, doctor_id, date_res) VALUES (@res_id, @dr_id, @date)";
                    // command.Parameters.Add("@res_id", MySqlDbType.VarChar).Value = reservation_id;
                    command.Parameters.Add("@dr_id", MySqlDbType.Int32).Value = doctor_id;
                    command.Parameters.Add("@date", MySqlDbType.DateTime).Value = res;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
