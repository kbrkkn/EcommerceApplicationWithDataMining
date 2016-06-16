using Ecommerce.DAL.Abstract;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.DAL.Concrete.EntityFramework
{
    public class CityDAL : ICityDAL
    {
        private ApplicationDbContext _context = new ApplicationDbContext();//DB işlemleri için

        public List<City> GetAll()
        {
            return (_context.City.ToList());//list entity set
        }

        public void Add(City city)
        {
            _context.City.Add(city);
            _context.SaveChanges();

        }

        public void Update(City city)
        {
            City updateCity = _context.City.FirstOrDefault(u => u.Id == city.Id);
            updateCity.Name = city.Name;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            City city = _context.City.Find(id);
            _context.City.Remove(city);
            _context.SaveChanges();
        }

        public City Find(int? id)
        {
            return _context.City.Find(id);
        }
    }


}

