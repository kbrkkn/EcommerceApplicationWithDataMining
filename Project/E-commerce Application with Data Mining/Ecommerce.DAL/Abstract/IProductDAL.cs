using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Abstract
{

    public interface IProductDAL
    {
        List<Product> GetProducts();
        List<Size> GetSizes();
        List<Color> GetColors();
        List<Category> GetCategories();
        List<SubCategory> GetSubCategories();
        List<Boutique> GetBoutiques();

        Product Get(int id);
        Product Get(string cName);
        Product Get(string cName, string password);
        Product Find(int? id);

        Size FindtoAddSize(string assignedsize);

        Color FindtoAddColor(string assignedcolor);

        void Add(Product product,Image im);

       
        void Add(Product product);
        bool Update(Product product);
        void Delete(int id);
    }
}
