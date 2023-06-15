using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Reservation_App.Model
{
    public interface ISpecialityRepository
    {

        List<SpecialityModel> GetAll();

        void Add(SpecialityModel model);

        void Delete(SpecialityModel speciality);
    }
}
