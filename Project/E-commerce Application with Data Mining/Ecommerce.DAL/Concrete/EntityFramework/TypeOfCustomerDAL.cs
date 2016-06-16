using Ecommerce.DAL.Abstract;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Concrete.EntityFramework
{
    public class TypeOfCustomerDAL : ITypeOfCustomerDAL
    {

        private ApplicationDbContext _context = new ApplicationDbContext();

        public List<TypeOfCustomer> GetTypeOfCustomers()
        {
            var result = _context.TypeOfCustomer.OrderBy(x => x.TypeOfCustomer_Id).ThenByDescending(x => x.TypeOfCustomer_Description).ToList();

            return result;
        }

        public TypeOfCustomer Get(int? id)
        {
            return _context.TypeOfCustomer.FirstOrDefault(u => u.TypeOfCustomer_Id == id);
        }

        public TypeOfCustomer Get(string description)
        {
            return _context.TypeOfCustomer.FirstOrDefault(u => u.TypeOfCustomer_Description == description);
        }

        public bool Add(TypeOfCustomer typeOfCustomer)
        {
            bool result = false;
            try
            {
                _context.TypeOfCustomer.Add(typeOfCustomer);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool Update(TypeOfCustomer typeOfCustomer)
        {
            bool result = false;
            try
            {
                TypeOfCustomer updatedTypeOfCustomer = _context.TypeOfCustomer.FirstOrDefault(x => x.TypeOfCustomer_Id == typeOfCustomer.TypeOfCustomer_Id);
                updatedTypeOfCustomer.TypeOfCustomer_Description = typeOfCustomer.TypeOfCustomer_Description;

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
                TypeOfCustomer typeOfCustomer = Get(id);
                _context.TypeOfCustomer.Remove(typeOfCustomer);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        TypeOfCustomer ITypeOfCustomerDAL.Get(string description)
        {
            throw new NotImplementedException();
        }

        public List<TypeOfCustomer> GetTypeOfCustomer()
        {
            throw new NotImplementedException();
        }
    }
}
