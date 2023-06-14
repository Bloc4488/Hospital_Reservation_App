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
    }
}
