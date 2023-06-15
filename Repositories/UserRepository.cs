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
        /// <summary>
        /// The method for add new user to database.
        /// </summary>
        /// <param name="userModel"></param>
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
        /// <summary>
        /// The method for delete user from database.
        /// </summary>
        /// <param name="userModel"></param>
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
        /// <summary>
        /// The method for delete doctor from database.
        /// </summary>
        /// <param name="doctor"></param>
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
        /// <summary>
        /// The method for update data of user in database.
        /// </summary>
        /// <param name="userModel"></param>
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
        /// <summary>
        /// The method for set new password data to database.
        /// </summary>
        /// <param name="userModel"></param>
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
        /// <summary>
        /// The method for check user data in database.
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
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
        /// <summary>
        /// The method checking email from user in database.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>True in case when mail is valid and false when vice versa</returns>
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
        /// <summary>
        /// The method for checking pesel length.
        /// </summary>
        /// <param name="pesel"></param>
        /// <returns>True when pesel length equals 11 and false when not.</returns>
        public bool checkPeselLength(SecureString pesel)
        {
            bool validPeselLength;
            if (SecureStringToString(pesel).Length != 11)
                validPeselLength = false;
            else
                validPeselLength = true;
            return validPeselLength;
        }
        /// <summary>
        /// The method for check user pesel.
        /// </summary>
        /// <param name="pesel"></param>
        /// <returns>True when pesel typed in window and user pesel in database are equals and vice versa.</returns>
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
        /// <summary>
        /// The method for checking typed password in second textbox equals to typed password in first textbox.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordRep"></param>
        /// <returns>True when equals and false when not.</returns>
        public bool checkPassRepeat(SecureString password, SecureString passwordRep)
        {
            if (SecureStringToString(password) == SecureStringToString(passwordRep))
                return true;
            else
                return false;
        }
        /// <summary>
        /// The method for checking if previous password equals to old password before user will change it.
        /// </summary>
        /// <param name="credential"></param>
        /// <param name="userModel"></param>
        /// <returns>True if eqauls and false if not.</returns>
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
        /// <summary>
        /// The method geting user by email.
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>user object.</returns>
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
        /// <summary>
        /// The method geting doctor by email.
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>doctor object</returns>
        public DoctorModel GetDoctor(string Email)
        {
            DoctorModel doctor = new DoctorModel();
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
                    "WHERE u.email = @mail";
                command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = Email;
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                doctor.Id = row[0].ToString();
                doctor.FirstName = row[1].ToString();
                doctor.LastName = row[2].ToString();
                doctor.Fullname = doctor.FirstName + " " + doctor.LastName;
                doctor.Email = row[3].ToString();
                doctor.Speciality = new SpecialityModel();
                doctor.Speciality.Id = row[4].ToString();
                doctor.Speciality.Name = row[5].ToString();
            }
            return doctor;
        }
        /// <summary>
        /// The method geting doctors data.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="speciality"></param>
        /// <returns>list of doctors of current specialties avalible at current date.</returns>
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
        /// <summary>
        /// The method geting patients.
        /// </summary>
        /// <returns>All pacients existing in database.</returns>
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
        /// <summary>
        /// The method geting all doctors.
        /// </summary>
        /// <returns>All doctors from database.</returns>
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
        /// <summary>
        /// The method for delete doctor from database.
        /// </summary>
        /// <param name="doctor"></param>
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
        /// <summary>
        /// The method for adding doctor to doctor table in database by admin.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="speciality"></param>
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
