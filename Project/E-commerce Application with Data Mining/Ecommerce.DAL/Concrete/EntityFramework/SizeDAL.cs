using Ecommerce.DAL.Abstract;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.DAL.Concrete.EntityFramework
{
    public class SizeDAL: ISizeDAL
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public List<Size> GetSizes()
        {
            return (_context.Size.ToList());
        }

        public Size Get(int id)
        {
            return _context.Size.FirstOrDefault(u => u.Size_Id == id);
        }

        public Size Get(string name)
        {
            return _context.Size.FirstOrDefault(u => u.Size_Name == name);
        }

        public bool Add(Size size)
        {
            bool result = false;
            try
            {
                _context.Size.Add(size);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool Update(Size size)
        {
            bool result = false;
            try
            {
                Size updatedSize = _context.Size.FirstOrDefault(x => x.Size_Id == size.Size_Id);
                updatedSize.Size_Name = size.Size_Name;

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
                Size size = Get(id);
                _context.Size.Remove(size);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        Size ISizeDAL.Get(string name)
        {
            throw new NotImplementedException();
        }
    }
}
