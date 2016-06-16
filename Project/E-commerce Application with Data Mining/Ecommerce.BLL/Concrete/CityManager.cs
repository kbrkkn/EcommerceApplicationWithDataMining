using Ecommerce.DAL.Abstract;
using Ecommerce.DAL.Concrete.EntityFramework;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.BLL.Concrete
{
    public class CityManager
    {
        private ICityDAL _cityDAL = new CityDAL();

        public List<City> GetAll() { return _cityDAL.GetAll(); }

        public void Add(City city) { _cityDAL.Add(city); }

        public void Update(City city) { _cityDAL.Update(city); }

        public void Delete(int id) { _cityDAL.Delete(id); }

        public City Find(int? id) { return _cityDAL.Find(id); }

    }
}
