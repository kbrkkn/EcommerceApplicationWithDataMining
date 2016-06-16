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
    public class ProductManager
    {
        private IProductDAL _productDAL = new ProductDAL();

        public Size FindtoAddSize(string assignedsize) { return _productDAL.FindtoAddSize(assignedsize); }

        public Color FindtoAddColor(string assignedcolor) { return _productDAL.FindtoAddColor(assignedcolor); }
        public void Add(Product product)
        {
            _productDAL.Add(product);
        }
        public void Add(Product product, Image im)
        {
            _productDAL.Add(product, im);
        }
     


        public void Delete(int id)
        {
            _productDAL.Delete(id);
        }

        public Product Get(string cName)
        {
            return _productDAL.Get(cName);
        }

        public Product Get(int id)
        {
            return _productDAL.Get(id);
        }

        public Product Get(string cName, string password)
        {
            return _productDAL.Get(cName, password);
        }

        public List<Category> GetCategories()
        {
            return _productDAL.GetCategories();
        }

        public List<Color> GetColors()
        {
            return _productDAL.GetColors();
        }

        public List<Product> GetProducts()
        {
            return _productDAL.GetProducts();
        }

        public List<Size> GetSizes()
        {
            return _productDAL.GetSizes();
        }

        public List<Boutique> GetBoutiques()
        {
            return _productDAL.GetBoutiques();
        }

        public List<SubCategory> GetSubCategories()
        {
            return _productDAL.GetSubCategories();
        }



        public bool Update(Product product)
        {
            return _productDAL.Update(product);
        }


    }
}
