using Hospital_Reservation_App.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Repositories
{
    public class SpecialityRepository : DataBaseRepository, ISpecialityRepository
    {
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
    }
}
