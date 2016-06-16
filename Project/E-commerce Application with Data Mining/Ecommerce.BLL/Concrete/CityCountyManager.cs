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
    public class CityCountyManager
    {
        private ICityCountyDAL _citycountyDAL = new CityCountyDAL();
        public List<County> GetAll() { return _citycountyDAL.GetAll(); }
        public void Add(County citycounty) { _citycountyDAL.Add(citycounty); }
        public void Update(County citycounty) { _citycountyDAL.Update(citycounty); }
        public void Delete(int id) { _citycountyDAL.Delete(id); }
        public County Find(int? id) { return _citycountyDAL.Find(id); }
        public List<City> List() { return _citycountyDAL.List(); }
    }
}
