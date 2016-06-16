using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Abstract
{

     public interface ICustomerDAL
    {
        List<TypeOfCustomer> GetTypeOfCustomers();
        List<Customer> GetAll();
        Customer Get(int id);
        Customer Get(string cName);
        Customer Get(string email, string password);

        Customer Find(int? id);

        List<City> GetCity();
        List<County> GetCityCounty();

        bool Add(Customer customer);
        bool Update(Customer customer);
        bool Delete(int id);
    }
}
