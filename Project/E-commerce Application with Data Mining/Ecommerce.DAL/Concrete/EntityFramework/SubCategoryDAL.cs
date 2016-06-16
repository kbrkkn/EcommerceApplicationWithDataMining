using Ecommerce.DAL.Abstract;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Concrete.EntityFramework

{
  public class SubCategoryDAL:ISubCategoryDAL
    {
      private ApplicationDbContext _context = new ApplicationDbContext();
        public List<SubCategory> GetAll()
        {
            return _context.SubCategory.ToList();
        }


        public void Add(SubCategory subcategory)
        {
          
                _context.SubCategory.Add(subcategory);
                _context.SaveChanges();
               
        }

        public void Update(SubCategory subcategory)
        {
          
                SubCategory updateSubCategory = _context.SubCategory.FirstOrDefault(u => u.SubCategory_Id == subcategory.SubCategory_Id);
                updateSubCategory.SubCategory_Name = subcategory.SubCategory_Name;
                updateSubCategory.Category_Id = subcategory.Category_Id;

                _context.SaveChanges();
            
        }

        public void Delete(int id)
        {
            SubCategory subcategory = _context.SubCategory.Find(id);
            _context.SubCategory.Remove(subcategory);
            _context.SaveChanges();
        }

        public SubCategory Find(int? id)
        {
            return _context.SubCategory.Find(id);
        }


      public  List<Category>List()
        {
            return _context.Category.ToList();
        }
    }
}
