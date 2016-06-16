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
    public class SizeManager
    {
        private ISizeDAL _sizeDAL = new SizeDAL();
        public bool Add(Size size)
        {
            return _sizeDAL.Add(size);
        }

        public bool Delete(int id)
        {
            return _sizeDAL.Delete(id);
        }
        public bool Update(Size size)
        {
            return _sizeDAL.Update(size);
        }
        public Size Get(int id)
        {
            return _sizeDAL.Get(id);
        }
        public Size Get(string name)
        {
            return _sizeDAL.Get(name);
        }
        public List<Size> GetSizes()
        {
            return _sizeDAL.GetSizes();
        }
    }
}
