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
    public class ColorManager
    {
        private IColorDAL _colorDAL = new ColorDAL();
        public bool Add(Color color)
        {
            return _colorDAL.Add(color);
        }

        public bool Delete(int id)
        {
            return _colorDAL.Delete(id);
        }
        public bool Update(Color color)
        {
            return _colorDAL.Update(color);
        }
        public Color Get(int id)
        {
            return _colorDAL.Get(id);
        }
        public Color Get(string name)
        {
            return _colorDAL.Get(name);
        }
        public List<Color> GetColors()
        {
            return _colorDAL.GetColors();
        }
    }
}
