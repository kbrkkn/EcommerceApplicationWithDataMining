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
    public class CustomerManager
    {
        private ICustomerDAL _userDAL = new CustomerDAL();


        public Customer Find(int? id) { return _userDAL.Find(id); }
        public List<Customer> GetAll()
        {
            //Aspect Oriented Programming (Enterprise):
            //Secrity
            //Logging
            //Exeption
            //Validation
            //Caching
            return _userDAL.GetAll();
        }

        public Customer Get(int id)
        {
            return _userDAL.Get(id);
        }

        public Customer Get(string uName)
        {   
            return _userDAL.Get(uName);
        }
        public Customer Get(string email, string password)
        {
            return _userDAL.Get(email, password);
        }

        public bool Add(Customer user)
        {
            return _userDAL.Add(user);
        }

        public bool Update(Customer user)
        {
            return _userDAL.Update(user);
        }

        public bool Delete(int id)
        {
            return _userDAL.Delete(id);
        }

        public List<TypeOfCustomer> GetTypeOfCustomers()
        {
            return _userDAL.GetTypeOfCustomers();
        }

        public List<City> GetCity()
        {
            return _userDAL.GetCity();
        }

        public List<County> GetCityCounty()
        {
            return _userDAL.GetCityCounty();
        }

    }
}
