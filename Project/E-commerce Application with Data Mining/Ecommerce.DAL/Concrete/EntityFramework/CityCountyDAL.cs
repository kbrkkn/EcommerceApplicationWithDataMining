using Ecommerce.DAL.Abstract;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Concrete.EntityFramework
{
    public class CityCountyDAL : ICityCountyDAL
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public List<County> GetAll()
        {
            return _context.CityCounty.ToList();
        }


        public void Add(County citycounty)
        {

            _context.CityCounty.Add(citycounty);
            _context.SaveChanges();

        }

        public void Update(County citycounty)
        {

            County updateCityCounty = _context.CityCounty.FirstOrDefault(u => u.Id == citycounty.Id);
            updateCityCounty.Name = citycounty.Name;
            updateCityCounty.City_Id = citycounty.City_Id;

            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            County citycounty = _context.CityCounty.Find(id);
            _context.CityCounty.Remove(citycounty);
            _context.SaveChanges();
        }

        public County Find(int? id)
        {
            return _context.CityCounty.Find(id);
        }


        public List<City> List()
        {
            return _context.City.ToList();
        }
    }
}
