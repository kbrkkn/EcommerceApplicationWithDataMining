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
    public class BoutiqueManager
    {
        private IBoutiqueDAL _boutiqueDAL = new BoutiqueDAL();
        public void Add(Boutique boutique)
        {
            _boutiqueDAL.Add(boutique);
        }

        public bool Delete(int id)
        {
            return _boutiqueDAL.Delete(id);
        }
        public bool Update(Boutique boutique)
        {
            return _boutiqueDAL.Update(boutique);
        }

        public Boutique Get(int id)
        {
            return _boutiqueDAL.Get(id);
        }
        public Boutique Get(string description)
        {
            return _boutiqueDAL.Get(description);
        }
     /*   public Boutique GetNumber(int numberOfProduct)
        {
            return _boutiqueDAL.Get(numberOfProduct);
        }*/
        public List<Product> GetProducts()
        {
            return _boutiqueDAL.GetProducts();
        }
        public List<Boutique> GetBoutiques()
        {
            return _boutiqueDAL.GetBoutiques();
        }
    }
}
