using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Abstract
{

    public interface ICityCountyDAL
    {
        List<County> GetAll();
        List<City> List();
        void Add(County city_county);
        void Update(County citycounty);
        void Delete(int id);
        County Find(int? id);
    }
}
