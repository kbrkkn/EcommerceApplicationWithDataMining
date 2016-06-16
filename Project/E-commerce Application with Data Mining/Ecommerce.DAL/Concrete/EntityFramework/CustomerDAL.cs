using Ecommerce.DAL.Abstract;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Concrete.EntityFramework
{
    public class CustomerDAL : ICustomerDAL
    {

        private ApplicationDbContext _context = new ApplicationDbContext();

        public List<Customer> GetAll()
        {
            //.Take(10): top ile aynıdır, sorgudan dönen ilk 10 kaydı getirir
            var result = _context.Customer.Where(x => x.IsActive == true && x.IsDelete == false)
                .OrderBy(x => x.Customer_Id).ThenByDescending(x => x.Customer_Surname).ToList();

            return result;

            //return _context.User.ToList();
        }

        public Customer Get(int id)
        {
            return _context.Customer.FirstOrDefault(u => u.Customer_Id == id);
        }

        public Customer Get(string uName)
        {
            return _context.Customer.FirstOrDefault(u => u.Customer_Email == uName);
        }

        public Customer Get(string email, string password)
        {
            return _context.Customer.FirstOrDefault(u => u.Customer_Email == email && u.Customer_Password == password);
        }

        public Customer Find(int? id)
        {
            return _context.Customer.Find(id);
        }

        public bool Add(Customer user)
        {
            bool result = false;
            try
            {
                _context.Customer.Add(user);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;

        }

        public bool Update(Customer user)
        {
            bool result = false;
            try
            {
                Customer updateUser = _context.Customer.FirstOrDefault(x => x.Customer_Id == user.Customer_Id);
                updateUser.Customer_Name = user.Customer_Name;
                updateUser.Customer_Surname = user.Customer_Surname;
                updateUser.Customer_Email = user.Customer_Email;
                updateUser.IsActive = user.IsActive;
                updateUser.IsDelete = user.IsDelete;
                updateUser.Customer_Password = user.Customer_Password;
                updateUser.CustomerTypeOfCustomer = user.CustomerTypeOfCustomer;
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;

        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                Customer user = Get(id);
                _context.Customer.Remove(user);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public List<TypeOfCustomer> GetTypeOfCustomers()
        {
            return _context.TypeOfCustomer.ToList();
        }

        public List<City> GetCity()
        {
            return _context.City.ToList();
        }

        public List<County> GetCityCounty()
        {
            return _context.CityCounty.ToList();
        }
    }
}
