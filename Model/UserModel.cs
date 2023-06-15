using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    /// <summary>
    /// Class represents users table of DataBase.
    /// </summary>
    public class UserModel
    {
        public string id { get; set; }
        public SecureString PESEL { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string sex { get; set; }
        public string email { get; set; }
        public SecureString Password { get; set; }
        public string privilege { get; set; }
        public string DisplayName { get; set; }
    }
}
