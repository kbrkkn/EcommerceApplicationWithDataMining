using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Abstract
{

   public interface ICategoryDAL
    {
       List<Category> GetAll();//kategorileri listele
       void Add(Category category);//ekle
       void Update(Category category);//güncelle
       void Delete(int id);//sil
       Category Find(int? id);
    }
}
