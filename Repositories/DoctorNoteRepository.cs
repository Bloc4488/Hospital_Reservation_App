using Hospital_Reservation_App.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Hospital_Reservation_App.Repositories
{
    public class DoctorNoteRepository : DataBaseRepository, IDoctorNoteRepository
    {
        /// <summary>
        /// The method geting doctor note to pacient.
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns> doctor note for selected reservation</returns>
        public DoctorNoteModel GetDoctorNote(ReservationModel reservation)
        {
            DoctorNoteModel doctorNote = new DoctorNoteModel();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM doctor_notes WHERE reservation_id = @res_id ORDER BY doctor_note_id DESC";
                command.Parameters.Add("@res_id", MySqlDbType.VarChar).Value = reservation.Id;
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                doctorNote.Id = row[0].ToString();
                doctorNote.Reservation_id = row[1].ToString();
                doctorNote.Note = row[2].ToString();
                break;
            }
            return doctorNote;
        }
        /// <summary>
        /// The method adding doctor note to dataBase after pacients visit.
        /// </summary>
        /// <param name="doctorNote"></param>
        public void AddDoctorNote(DoctorNoteModel doctorNote)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO doctor_notes(reservation_id, note) VALUES (@res_id, @note)";
                    command.Parameters.Add("@res_id", MySqlDbType.VarChar).Value = doctorNote.Reservation_id;
                    command.Parameters.Add("@note", MySqlDbType.Text).Value = doctorNote.Note;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
