using Ecommerce.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entities;

namespace Ecommerce.DAL.Concrete.EntityFramework
{
    public class ProductDAL : IProductDAL

    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public void Add(Product product)
        {
                _context.Product.Add(product);
                _context.SaveChanges();
        }
        public void Add(Product product, Image im)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
           im.Product_Id = product.Product_Id;
            _context.Image.Add(im);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // bool result = false;
            //  try
            //  {

            Product product = _context.Product.Find(id);
            _context.Product.Remove(product);
            _context.SaveChanges();

            //   result = true;
            // }
            // catch (Exception)
            // {
            //   result = false;
            // }
            // return result;
        }

        public Product Get(string cName)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            return _context.Product.FirstOrDefault(u => u.Product_Id == id);
        }

        public Product Get(string cName, string password)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetCategories()
        {
            return _context.Category.ToList();
        }

        public List<Color> GetColors()
        {
            return _context.Color.ToList();
        }

        public List<Product> GetProducts()
        {
            //.Take(10): top ile aynıdır, sorgudan dönen ilk 10 kaydı getirir
            /* var result = _context.Product.Where(x => x.Product_NumberInStock != 0 )
                 .OrderBy(x => x.Product_Id).ThenByDescending(x => x.Product_Description).ToList();*/
            return _context.Product.ToList();

            // return result;
        }

        public List<Size> GetSizes()
        {
            return _context.Size.OrderBy(x => x.Size_Name).ToList();
        }

        public List<SubCategory> GetSubCategories()
        {
            return _context.SubCategory.ToList();
        }

        public bool Update(Product product)
        {
            bool result = false;
            try
            {
                Product updatedProduct = _context.Product.FirstOrDefault(x => x.Product_Id == product.Product_Id);
                updatedProduct.Product_Description = product.Product_Description;
               // updatedProduct.Product_Color_Id = product.Product_Color_Id;
                updatedProduct.Product_Cost = product.Product_Cost;
                // updatedProduct.Product_NumberInStock = product.Product_NumberInStock;
                updatedProduct.Product_Warning = product.Product_Warning;
                // updatedProduct.Product_Size_Id = product.Product_Size_Id;
                updatedProduct.Product_SubCategory_Id = product.Product_SubCategory_Id;
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }


        public List<Boutique> GetBoutiques()
        {
            return _context.Boutique.ToList();
        }


        public Product Find(int? id)
        {
            return _context.Product.Find(id);
        }


        public Size FindtoAddSize(string assignedsize)
        {
            var toadd = _context.Size.Find(int.Parse(assignedsize));
            return toadd;        
        }

        public Color FindtoAddColor(string assignedcolor)
        {
            var toadd = _context.Color.Find(int.Parse(assignedcolor));
            return toadd;
        }
    }
}
  