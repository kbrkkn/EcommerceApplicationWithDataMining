using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.DAL.Abstract
{
    public interface IBoutiqueDAL
    {
        List<Product> GetProducts();
        List<Boutique> GetBoutiques();
        Boutique Get(int id);
        Boutique Get(string description);
       // Boutique GetNumber(int numberOfProduct);
        void Add(Boutique boutique);
        bool Update(Boutique boutique);
        bool Delete(int id);

    }
}
