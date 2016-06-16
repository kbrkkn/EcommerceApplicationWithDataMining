using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.DAL.Abstract
{
    public interface ISizeDAL
    {
        List<Size> GetSizes();
        Size Get(int id);
        Size Get(string name);
        bool Add(Size size);
        bool Update(Size size);
        bool Delete(int id);
    }
}
