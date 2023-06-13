using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    interface IUserRepository
    {
        void Add(UserModel userModel);
        void Delete(UserModel userModel);
        void Update(UserModel userModel);
        void UpdatePassword(UserModel userModel);
        bool AuthentificateUser(NetworkCredential credential);
        bool checkMail(string email);
        bool checkPeselLength(SecureString pesel);
        bool checkPeselUser(SecureString pesel);
        bool checkPassRepeat(SecureString password, SecureString passwordRep);
        bool checkOldPassword(NetworkCredential credential, UserModel userModel);
        UserModel GetUser(string Email);
        List<DoctorModel> GetDoctorsData(DateTime date, SpecialityModel speciality);
    }
}
