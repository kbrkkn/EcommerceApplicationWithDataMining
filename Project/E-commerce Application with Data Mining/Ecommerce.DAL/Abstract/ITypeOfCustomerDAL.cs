using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Abstract
{

    public interface ITypeOfCustomerDAL
    {
        List<TypeOfCustomer> GetTypeOfCustomers();
        TypeOfCustomer Get(int? id);
        TypeOfCustomer Get(string description);

        List<TypeOfCustomer> GetTypeOfCustomer();

        bool Add(TypeOfCustomer description);
        bool Update(TypeOfCustomer description);
        bool Delete(int id);
    }
}