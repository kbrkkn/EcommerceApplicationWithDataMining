using Ecommerce.DAL.Abstract;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.DAL.Concrete.EntityFramework
{
   public class CategoryDAL:ICategoryDAL
   {
       private ApplicationDbContext _context = new ApplicationDbContext();//DB işlemleri için

       public List<Category> GetAll()
{
           return(_context.Category.ToList());//list entity set
       }


public void Add(Category category)
{
   
        _context.Category.Add(category);
        _context.SaveChanges();
   
}

public void Update(Category category)
{
   
        Category updateCategory = _context.Category.FirstOrDefault(u => u.Category_Id == category.Category_Id);
        updateCategory.Category_Name = category.Category_Name;
        _context.SaveChanges();
   
}

public void Delete(int id)
{
    
    Category category = _context.Category.Find(id);
    _context.Category.Remove(category);
    _context.SaveChanges();
        
   
}


public Category Find(int? id)
{
    return _context.Category.Find(id);
}
   }

    
    }

