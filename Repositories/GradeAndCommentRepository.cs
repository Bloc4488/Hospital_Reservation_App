using Hospital_Reservation_App.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Hospital_Reservation_App.Repositories
{
    public class GradeAndCommentRepository : DataBaseRepository, IGradeAndCommentRepository
    {
        public void AddComment(GradeAndCommentModel GradeAndCom)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO grades_and_comments(reservation_id, grade, comment) VALUES (@reserve_id, @gr, @com)";
                    command.Parameters.Add("@reserve_id", MySqlDbType.Int64).Value = GradeAndCom.ReservationID;
                    command.Parameters.Add("@gr", MySqlDbType.Int64).Value = GradeAndCom.grade;
                    command.Parameters.Add("@com", MySqlDbType.Text).Value = GradeAndCom.comment;
                    command.ExecuteNonQuery();
                }
            }
        }

    }

    
}
