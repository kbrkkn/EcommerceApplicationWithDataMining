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
    public class TypeOfCustomerManager
    {
        private ITypeOfCustomerDAL _typeOfCustomerDAL = new TypeOfCustomerDAL();

        public bool Add(TypeOfCustomer typeOfCustomer)
        {
            return _typeOfCustomerDAL.Add(typeOfCustomer);
        }

        public bool Delete(int id)
        {
            return _typeOfCustomerDAL.Delete(id);
        }
        public bool Update(TypeOfCustomer typeOfCustomer)
        {
            return _typeOfCustomerDAL.Update(typeOfCustomer);
        }
        public TypeOfCustomer Get(int? id)
        {
            return _typeOfCustomerDAL.Get(id);
        }
        public TypeOfCustomer Get(string description)
        {
            return _typeOfCustomerDAL.Get(description);
        }
        public List<TypeOfCustomer> GetTypeOfCustomers()
        {
            return _typeOfCustomerDAL.GetTypeOfCustomers();
        }
    }
}