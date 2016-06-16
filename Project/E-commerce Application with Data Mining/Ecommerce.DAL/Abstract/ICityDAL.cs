using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Abstract
{

    public interface ICityDAL
    {
        List<City> GetAll();//şehir listele

        void Add(City city);//ekle
        void Update(City city);//güncelle
        void Delete(int id);//sil
        City Find(int? id);
    }
}
