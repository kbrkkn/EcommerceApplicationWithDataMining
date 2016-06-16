using Ecommerce.DAL.Abstract;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Concrete.EntityFramework
{
    public class ImageDAL : IImageDAL
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public void Add(Image[] im)
        {
            foreach (Image c in im)
            {

                _context.Image.Add(c);
                _context.SaveChanges();
            }
        }

        public void Add(Image im)
        {
            _context.Image.Add(im);
            _context.SaveChanges();

        }
    }
}
