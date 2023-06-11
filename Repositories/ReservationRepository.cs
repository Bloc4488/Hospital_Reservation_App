using Hospital_Reservation_App.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Data;

namespace Hospital_Reservation_App.Repositories
{
    public class ReservationRepository : DataBaseRepository, IReservationRepository
    {
        public void AddRes(string pacient_id, string doctor_id, DateTime res)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO reservations(patient_id, doctor_id, date_res) VALUES (@pac_id, @dr_id, @date)";
                    command.Parameters.Add("@pac_id", MySqlDbType.Int64).Value = pacient_id;
                    command.Parameters.Add("@dr_id", MySqlDbType.Int64).Value = doctor_id;
                    command.Parameters.Add("@date", MySqlDbType.DateTime).Value = res;
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<ReservationModel> GetReservationsData(UserModel user)
        {
            List<ReservationModel> reservations = new List<ReservationModel>();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT res.reservation_id AS id_reservation, res.patient_id AS id_patient, res.doctor_id AS id_doctor, u.firstname, u.lastname, " +
                    "doc.speciality_id, spec.name AS specialty_name, res.date_res AS reservation_time " +
                    "FROM reservations res " +
                    "INNER JOIN doctors doc ON doc.user_id = res.doctor_id " +
                    "INNER JOIN specialties spec ON spec.speciality_id = doc.speciality_id " +
                    "INNER JOIN users u ON u.id = doc.user_id " +
                    "WHERE res.patient_id = @id";
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = user.id;
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                ReservationModel reservation = new ReservationModel();
                reservation.Doctor = new DoctorModel();
                reservation.Doctor.Speciality = new SpecialityModel();
                reservation.Id = row[0].ToString();
                reservation.PatientId = row[1].ToString();
                reservation.Doctor.Id = row[2].ToString();
                reservation.Doctor.FirstName = row[3].ToString();
                reservation.Doctor.LastName = row[4].ToString();
                reservation.Doctor.Fullname = reservation.Doctor.FirstName + " " + reservation.Doctor.LastName;
                reservation.Doctor.Speciality.Id = row[5].ToString();
                reservation.Doctor.Speciality.Name = row[6].ToString();
                reservation.ReservationTime = (DateTime)row[7];
                reservations.Add(reservation);
            }
            return reservations;
        }

        public void DeleteReservation(ReservationModel reservation)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM reservations WHERE reservation_id = @id";
                    command.Parameters.Add("@id", MySqlDbType.VarChar).Value = reservation.Id;
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
