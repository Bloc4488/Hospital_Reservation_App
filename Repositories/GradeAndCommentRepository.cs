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
        /// <summary>
        /// The method user adding comment about selected visit
        /// </summary>
        /// <param name="GradeAndCom"></param>
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
        /// <summary>
        /// The method getting selected comment for show them to doctor 
        /// </summary>
        /// <param name="visit"></param>
        /// <returns></returns>
        public GradeAndCommentModel GetComment(VisitModel visit)
        {
            GradeAndCommentModel commentModel = new GradeAndCommentModel();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM grades_and_comments WHERE reservation_id = @id ORDER BY comment_id DESC";
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = visit.ReservationId;
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                commentModel.iD = row[0].ToString();
                commentModel.ReservationID = row[1].ToString();
                commentModel.grade = row[2].ToString();
                commentModel.comment = row[3].ToString();
                break;
            }
            return commentModel;
        }
    }
}
