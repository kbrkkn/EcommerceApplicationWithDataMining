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
    public class CategoryManager
    {
        private ICategoryDAL _categoryDAL = new CategoryDAL();

        public List<Category> GetAll() {
            return _categoryDAL.GetAll();
        }
        public void Add(Category category) {_categoryDAL.Add(category); }

        public void Update(Category category) {  _categoryDAL.Update(category); }

        public void Delete(int id) {  _categoryDAL.Delete(id); }

        public Category Find(int? id)
        {
            return _categoryDAL.Find(id);
        }
    }
}
