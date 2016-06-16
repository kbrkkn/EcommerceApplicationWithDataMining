using Ecommerce.DAL.Abstract;
using Ecommerce.DAL.Concrete;
using Ecommerce.DAL.Concrete.EntityFramework;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Concrete
{
    public class ImageManager
    {
        private IImageDAL i = new ImageDAL();

        public void Add(Image im)
        {
            i.Add(im);
        }


        public void Add(Image[] im)
        {
            foreach (Image s in im)
            {
                i.Add(s);
            }
        }
    }
}
