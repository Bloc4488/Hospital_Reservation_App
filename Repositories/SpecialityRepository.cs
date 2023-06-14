using Hospital_Reservation_App.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Repositories
{
    public class SpecialityRepository : DataBaseRepository, ISpecialityRepository
    {
        /// <summary>
        /// The method getting all specialties in hospital from database.
        /// </summary>
        /// <returns>list of all specialties in hospital.</returns>
        public List<SpecialityModel> GetAll()
        {
            List<SpecialityModel> specialityModels = new List<SpecialityModel>();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT speciality_id, name FROM specialties";
                mySqlDataAdapter.SelectCommand = command;
                mySqlDataAdapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                SpecialityModel specialityModel = new SpecialityModel();
                specialityModel.Id = row[0].ToString();
                specialityModel.Name = row[1].ToString();
                specialityModels.Add(specialityModel);
            }
            return specialityModels;
        }
        /// <summary>
        /// The method for adding new speciality in database 
        /// </summary>
        /// <param name="speciality"></param>
        public void Add(SpecialityModel speciality)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO specialties(name) VALUES (@name)";
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = speciality.Name;
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// The method for delete selected speciality from database.
        /// </summary>
        /// <param name="speciality"></param>
        public void Delete(SpecialityModel speciality)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE users SET privelege = 1 " +
                    "WHERE id IN (" +
                    "SELECT user_id FROM doctors d " +
                    "JOIN specialties s ON d.speciality_id = s.speciality_id " +
                    "WHERE s.speciality_id = @spec_id); " +
                    "DELETE FROM specialties WHERE speciality_id = @spec_id;";
                command.Parameters.Add("@spec_id", MySqlDbType.Int64).Value = speciality.Id;
                command.ExecuteNonQuery();
            }
        }
    }
}
