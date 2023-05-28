using Hospital_Reservation_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthentificateUser(NetworkCredential credential)
        {
            bool validUser = true;
            return validUser;
        }
    }
}
