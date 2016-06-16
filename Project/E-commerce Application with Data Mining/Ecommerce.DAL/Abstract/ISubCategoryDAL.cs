using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Abstract
{

   public interface ISubCategoryDAL

    {
        List<SubCategory> GetAll();
        List<Category> List();
        void Add(SubCategory subcategory);
        void Update(SubCategory subcategory);
        void Delete(int id);
        SubCategory Find(int? id); 
   }
}
