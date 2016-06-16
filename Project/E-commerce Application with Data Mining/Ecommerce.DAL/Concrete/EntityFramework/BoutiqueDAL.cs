using Ecommerce.DAL.Abstract;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.DAL.Concrete.EntityFramework
{
    public class BoutiqueDAL: IBoutiqueDAL
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
    
        public List<Boutique> GetBoutiques()
        {
            return (_context.Boutique.ToList());
        }

        public Boutique Get(int id)
        {
            return _context.Boutique.FirstOrDefault(u => u.Boutique_Id == id);
        }

        public Boutique Get(string description)
        {
            return _context.Boutique.FirstOrDefault(u => u.Boutique_Description == description);
        }

       /* public Boutique GetNumber(int numberOfProduct)
        {
            return _context.Boutique.FirstOrDefault(u => u.Boutique_Stock == numberOfProduct);
        }*/

        public void Add(Boutique boutique)
        {
            
                _context.Boutique.Add(boutique);
                _context.SaveChanges();
                
           
        }

        public bool Update(Boutique boutique)
        {
            bool result = false;
            try
            {
                Boutique updateBoutique = _context.Boutique.FirstOrDefault(x => x.Boutique_Id == boutique.Boutique_Id);
                updateBoutique.Boutique_Description = boutique.Boutique_Description;
               // updateBoutique.NumberOfProductInBoutique = boutique.NumberOfProductInBoutique;
               // updateBoutique.File = boutique.File;
                updateBoutique.FileName = boutique.FileName;
                updateBoutique.ImageData = boutique.ImageData;
               
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
               result = false;
           }
           return result;
        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                Boutique boutique = Get(id);
                _context.Boutique.Remove(boutique);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public List<Product> GetProducts()
        {
            return _context.Product.ToList();
        }

    }
}
