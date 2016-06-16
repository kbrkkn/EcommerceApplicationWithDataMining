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
   public class SubCategoryManager

    {
       private ISubCategoryDAL _subcategoryDAL = new SubCategoryDAL();
       public List<SubCategory> GetAll() { return _subcategoryDAL.GetAll(); }
       public void Add(SubCategory subcategory) {  _subcategoryDAL.Add(subcategory); }
       public void Update(SubCategory subcategory) { _subcategoryDAL.Update(subcategory); }
       public void Delete(int id) { _subcategoryDAL.Delete(id); }
       public SubCategory Find(int? id) {return _subcategoryDAL.Find(id); }
       public List<Category> List(){ return _subcategoryDAL.List();}
   }
}
