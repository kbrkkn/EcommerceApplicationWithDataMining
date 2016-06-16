using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Abstract
{

    public interface IColorDAL
    {
        List<Color> GetColors();
        Color Get(int id);
        Color Get(string name);
        bool Add(Color color);
        bool Update(Color color);
        bool Delete(int id);
    }
}
