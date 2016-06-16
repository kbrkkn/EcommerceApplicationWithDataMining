using Ecommerce.DAL.Abstract;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Concrete.EntityFramework
{

    public class ColorDAL: IColorDAL
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public List<Color> GetColors()
        {
            return (_context.Color.ToList());
        }

        public Color Get(int id)
        {
            return _context.Color.FirstOrDefault(u => u.Color_Id == id);
        }

        public Color Get(string name)
        {
            return _context.Color.FirstOrDefault(u => u.Color_Name == name);
        }

        public bool Add(Color color)
        {
            bool result = false;
            try
            {
                _context.Color.Add(color);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool Update(Color color)
        {
            bool result = false;
            try
            {
                Color updatedColor = _context.Color.FirstOrDefault(x => x.Color_Id == color.Color_Id);
                updatedColor.Color_Name = color.Color_Name;

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
                Color color = Get(id);
                _context.Color.Remove(color);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        Color IColorDAL.Get(string name)
        {
            throw new NotImplementedException();
        }
    }
}
