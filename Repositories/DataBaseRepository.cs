using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Repositories
{
    public abstract class DataBaseRepository
    {
        private readonly string _connectionString;
        public DataBaseRepository()
        {
            _connectionString = "server = localhost; port = 3306; username = root; password = root; database = hospitaldb";
        }
        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection( _connectionString );
        }
    }
}
