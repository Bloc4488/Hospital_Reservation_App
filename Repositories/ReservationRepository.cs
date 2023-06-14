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
        public void AddRes(ReservationModel reservation)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO reservations(patient_id, doctor_id, date_res) VALUES (@pac_id, @dr_id, @date)";
                    command.Parameters.Add("@pac_id", MySqlDbType.Int64).Value = reservation.PatientId;
                    command.Parameters.Add("@dr_id", MySqlDbType.Int64).Value = reservation.Doctor.Id;
                    command.Parameters.Add("@date", MySqlDbType.DateTime).Value = reservation.ReservationTime;
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<ReservationModel> GetAllReservationsData(UserModel user)
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
                    "WHERE res.patient_id = @id " +
                    "ORDER BY res.date_res DESC";
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = user.id;
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                ReservationModel reservation = new ReservationModel
                {
                    Doctor = new DoctorModel()
                };
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

        public List<ReservationModel> GetPastReservationsData(UserModel user)
        {
            List<ReservationModel> reservations = new List<ReservationModel>();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            DateTime date = DateTime.Now;
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
                    "WHERE res.patient_id = @id AND res.date_res < @date " +
                    "ORDER BY res.date_res DESC";
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = user.id;
                command.Parameters.Add("@date", MySqlDbType.DateTime).Value = date;
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                ReservationModel reservation = new ReservationModel
                {
                    Doctor = new DoctorModel()
                };
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

        public List<ReservationModel> GetFutureReservationsData(UserModel user)
        {
            List<ReservationModel> reservations = new List<ReservationModel>();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            DateTime date = DateTime.Now;
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
                    "WHERE res.patient_id = @id AND res.date_res > @date " +
                    "ORDER BY res.date_res DESC";
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = user.id;
                command.Parameters.Add("@date", MySqlDbType.DateTime).Value = date;
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                ReservationModel reservation = new ReservationModel
                {
                    Doctor = new DoctorModel()
                };
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

        public List<VisitModel> GetAllReservationsData(DoctorModel user)
        {
            List<VisitModel> reservations = new List<VisitModel>();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT res.reservation_id AS id_reservation, res.patient_id AS id_patient, res.doctor_id AS id_doctor, u.firstname, u.lastname, " +
                    "res.date_res AS reservation_time " +
                    "FROM reservations res " +
                    "INNER JOIN users u ON u.id = res.patient_id " +
                    "WHERE res.doctor_id = @id " +
                    "ORDER BY res.date_res DESC";
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = user.Id;
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                VisitModel reservation = new VisitModel();
                reservation.Doctor = new DoctorModel();
                reservation.Patient = new UserModel();
                reservation.ReservationId = row[0].ToString();
                reservation.Patient.id = row[1].ToString();
                reservation.Doctor.Id = row[2].ToString();
                reservation.Patient.firstName = row[3].ToString();
                reservation.Patient.lastName = row[4].ToString();
                reservation.Patient.DisplayName = reservation.Patient.firstName + " " + reservation.Patient.lastName;
                reservation.ReservationTime = (DateTime)row[5];
                reservations.Add(reservation);
            }
            return reservations;
        }
        public List<VisitModel> GetPastReservationsData(DoctorModel user)
        {
            List<VisitModel> reservations = new List<VisitModel>();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            DateTime date = DateTime.Now;
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT res.reservation_id AS id_reservation, res.patient_id AS id_patient, res.doctor_id AS id_doctor, u.firstname, u.lastname, " +
                    "res.date_res AS reservation_time " +
                    "FROM reservations res " +
                    "INNER JOIN users u ON u.id = res.patient_id " +
                    "WHERE res.doctor_id = @id AND res.date_res > @date " +
                    "ORDER BY res.date_res DESC";
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = user.Id;
                command.Parameters.Add("@date", MySqlDbType.DateTime).Value = date;
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                VisitModel reservation = new VisitModel();
                reservation.Doctor = new DoctorModel();
                reservation.Patient = new UserModel();
                reservation.ReservationId = row[0].ToString();
                reservation.Patient.id = row[1].ToString();
                reservation.Doctor.Id = row[2].ToString();
                reservation.Patient.firstName = row[3].ToString();
                reservation.Patient.lastName = row[4].ToString();
                reservation.Patient.DisplayName = reservation.Patient.firstName + " " + reservation.Patient.lastName;
                reservation.ReservationTime = (DateTime)row[5];
                reservations.Add(reservation);
            }
            return reservations;
        }
        public List<VisitModel> GetFutureReservationsData(DoctorModel user)
        {
            List<VisitModel> reservations = new List<VisitModel>();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            DateTime date = DateTime.Now;
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT res.reservation_id AS id_reservation, res.patient_id AS id_patient, res.doctor_id AS id_doctor, u.firstname, u.lastname, " +
                    "res.date_res AS reservation_time " +
                    "FROM reservations res " +
                    "INNER JOIN users u ON u.id = res.patient_id " +
                    "WHERE res.doctor_id = @id AND res.date_res < @date " +
                    "ORDER BY res.date_res DESC";
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = user.Id;
                command.Parameters.Add("@date", MySqlDbType.DateTime).Value = date;
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                VisitModel reservation = new VisitModel();
                reservation.Doctor = new DoctorModel();
                reservation.Patient = new UserModel();
                reservation.ReservationId = row[0].ToString();
                reservation.Patient.id = row[1].ToString();
                reservation.Doctor.Id = row[2].ToString();
                reservation.Patient.firstName = row[3].ToString();
                reservation.Patient.lastName = row[4].ToString();
                reservation.Patient.DisplayName = reservation.Patient.firstName + " " + reservation.Patient.lastName;
                reservation.ReservationTime = (DateTime)row[5];
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

        public List<DateTime> GetUserTime(DateTime date, UserModel user)
        {
            List<DateTime> result = new List<DateTime>();
            DateTime dateFirst = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0);
            DateTime dateLast = new DateTime(date.Year, date.Month, date.Day, 17, 0, 0);
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT date_res FROM reservations WHERE patient_id = @id AND date_res BETWEEN @datef AND @datel";
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = user.id;
                command.Parameters.Add("@datef", MySqlDbType.DateTime).Value = dateFirst;
                command.Parameters.Add("@datel", MySqlDbType.DateTime).Value = dateLast;
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                DateTime dhour = (DateTime)row[0];
                result.Add(dhour);
            }
            return result;
        }
    }
}
