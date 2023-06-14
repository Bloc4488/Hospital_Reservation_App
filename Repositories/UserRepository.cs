using Hospital_Reservation_App.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Hospital_Reservation_App.Repositories
{
    public class UserRepository : DataBaseRepository, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    string passHash = BCrypt.Net.BCrypt.HashPassword(SecureStringToString(userModel.Password));
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO users(PESEL, firstname, lastname, sex, email, password) VALUES (@pesel, @fname, @lname, @sx, @mail, @pass)";
                    command.Parameters.Add("@pesel", MySqlDbType.VarChar).Value = SecureStringToString(userModel.PESEL);
                    command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = userModel.firstName;
                    command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = userModel.lastName;
                    command.Parameters.Add("@sx", MySqlDbType.VarChar).Value = userModel.sex;
                    command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = userModel.email;
                    command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passHash;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(UserModel userModel)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM users WHERE id = @patient";
                    command.Parameters.Add("@patient", MySqlDbType.VarChar).Value = userModel.id;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(DoctorModel doctor)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM users WHERE id = @doctor";
                    command.Parameters.Add("@doctor", MySqlDbType.VarChar).Value = doctor.Id;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(UserModel userModel)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE users SET PESEL = @pesel, firstname = @fname, lastname = @lname, sex = @sx, email = @mail WHERE id = @UserId";
                    command.Parameters.Add("@pesel", MySqlDbType.VarChar).Value = SecureStringToString(userModel.PESEL);
                    command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = userModel.firstName;
                    command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = userModel.lastName;
                    command.Parameters.Add("@sx", MySqlDbType.VarChar).Value = userModel.sex;
                    command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = userModel.email;
                    command.Parameters.Add("@UserId", MySqlDbType.Int64).Value = userModel.id;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePassword(UserModel userModel) 
        { 
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    string passHash = BCrypt.Net.BCrypt.HashPassword(SecureStringToString(userModel.Password));
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE users SET password = @pass WHERE id = @UserId";
                    command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passHash;
                    command.Parameters.Add("@UserId", MySqlDbType.Int64).Value = userModel.id;
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool AuthentificateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                string passUserDB = "";
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM users WHERE email = @mail";
                command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = credential.UserName;
                MySqlDataReader data = command.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        passUserDB = Convert.ToString(data.GetValue(6));
                    }
                    if (BCrypt.Net.BCrypt.Verify(Convert.ToString(credential.Password), passUserDB))
                    {
                        validUser = true;
                    }
                    else
                    {
                        validUser = false;
                    }
                }
                else
                {
                    validUser = false;
                }
            }
            return validUser;
        }

        public String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public bool checkMail(string email)
        {
            bool validMail;
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT email FROM users WHERE email = @mail";
                command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = email;
                validMail = command.ExecuteScalar() == null ? false : true;
            }
            return validMail;
        }

        public bool checkPeselLength(SecureString pesel)
        {
            bool validPeselLength;
            if (SecureStringToString(pesel).Length != 11)
                validPeselLength = false;
            else
                validPeselLength = true;
            return validPeselLength;
        }

        public bool checkPeselUser(SecureString pesel)
        {
            bool validPeselUser;
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT PESEL FROM users WHERE PESEL = @pesel";
                command.Parameters.Add("@pesel", MySqlDbType.VarChar).Value = SecureStringToString(pesel);
                validPeselUser = command.ExecuteScalar() == null ? false : true;
            }
            return validPeselUser;
        }

        public bool checkPassRepeat(SecureString password, SecureString passwordRep)
        {
            if (SecureStringToString(password) == SecureStringToString(passwordRep))
                return true;
            else
                return false;
        }
        public bool checkOldPassword(NetworkCredential credential, UserModel userModel)
        {
            if (BCrypt.Net.BCrypt.Verify(Convert.ToString(credential.Password), SecureStringToString(userModel.Password)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public UserModel GetUser(string Email)
        {
            UserModel user = new UserModel();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM users WHERE email = @mail";
                command.Parameters.Add("@mail", MySqlDbType.VarChar).Value=Email;
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach(DataRow row in table.Rows)
            {
                user.id = row[0].ToString();
                user.PESEL = new NetworkCredential("", row[1].ToString()).SecurePassword;
                user.firstName = row[2].ToString();
                user.lastName = row[3].ToString();
                user.sex = row[4].ToString();
                user.email = row[5].ToString();
                user.Password = new NetworkCredential("", row[6].ToString()).SecurePassword;
                user.privilege = row[7].ToString();
            }
            return user;
        }

        public List<DoctorModel> GetDoctorsData(DateTime date, SpecialityModel speciality)
        {
            List<DoctorModel> doctorModels = new List<DoctorModel>();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT u.firstname, u.lastname, u.id AS user_id, d.speciality_id, s.name AS speciality_name " +
                    "FROM doctors d " +
                    "JOIN users u ON d.user_id = u.id " +
                    "JOIN specialties s ON d.speciality_id = s.speciality_id " +
                    "WHERE d.user_id NOT IN (" +
                    "SELECT r.doctor_id " +
                    "FROM reservations r " +
                    "WHERE r.date_res = @date) " +
                    "AND d.speciality_id = @spec_id";
                command.Parameters.Add("@date", MySqlDbType.DateTime).Value = date;
                command.Parameters.Add("@spec_id", MySqlDbType.Int64).Value = speciality.Id;
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                DoctorModel doctorModel = new DoctorModel();
                doctorModel.Speciality = new SpecialityModel();
                doctorModel.FirstName = row[0].ToString();
                doctorModel.LastName = row[1].ToString();
                doctorModel.Fullname = doctorModel.FirstName + " " + doctorModel.LastName;
                doctorModel.Id = row[2].ToString();
                doctorModel.Speciality.Id = row[3].ToString();
                doctorModel.Speciality.Name = row[4].ToString();
                doctorModels.Add(doctorModel);
            }
            return doctorModels;
        }
        public List<UserModel> GetPatients()
        {
            List<UserModel> users = new List<UserModel>();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT id, firstname, lastname, email, privilege FROM users WHERE privilege = 1";
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                UserModel user = new UserModel();
                user.id = row[0].ToString();
                user.firstName = row[1].ToString();
                user.lastName = row[2].ToString();
                user.email = row[3].ToString();
                user.privilege = row[4].ToString();
                users.Add(user);
            }
            return users;
        }
        public List<DoctorModel> GetDoctors()
        {
            List<DoctorModel> users = new List<DoctorModel>();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT u.id, u.firstname, u.lastname, u.email, sp.speciality_id, sp.name FROM users u " +
                    "JOIN doctors d ON d.user_id = u.id " +
                    "JOIN specialties sp ON sp.speciality_id = d.speciality_id " +
                    "WHERE u.privilege = 2";
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                DoctorModel user = new DoctorModel();
                user.Speciality = new SpecialityModel();
                user.Id = row[0].ToString();
                user.FirstName = row[1].ToString();
                user.LastName = row[2].ToString();
                user.Email = row[3].ToString();
                user.Speciality.Id = row[4].ToString();
                user.Speciality.Name = row[5].ToString();
                users.Add(user);
            }
            return users;
        }

        public void DeleteDoctorFromDoctors(DoctorModel doctor)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE users SET privilege = 1 WHERE id = @UserId; " +
                        "DELETE FROM doctors WHERE user_id = @UserID";
                    command.Parameters.Add("@UserId", MySqlDbType.Int32).Value = doctor.Id;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void CreateDoctor(UserModel user, SpecialityModel speciality)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE users SET privilege = 2 WHERE id = @UserId; " +
                        "INSERT INTO doctors(user_id, speciality_id) VALUES (@UserId, @spec_id)";
                    command.Parameters.Add("@UserId", MySqlDbType.Int64).Value = user.id;
                    command.Parameters.Add("@spec_id", MySqlDbType.Int64).Value = speciality.Id;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
