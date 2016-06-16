using Ecommerce.BLL.Concrete;
using Ecommerce.DAL.Concrete.EntityFramework;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebUI.Controllers
{
    public class HomeController : Controller
    {
        BoutiqueManager _boutiqueManager = new BoutiqueManager();
        ProductManager _productManager = new ProductManager();
        ColorManager _colorManager = new ColorManager();

        public ActionResult Index()
        {
            List<Boutique> boutiques = _boutiqueManager.GetBoutiques();
            return View(boutiques);
         
        }
        public ActionResult ListProduct(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var productBoutique = db.Product.Where(m => m.Product_Boutique_Id == id).ToList();

            return View(productBoutique);
           
        }

        public ActionResult View(Product product,int id)
        {

            ViewBag.Color = new SelectList(_productManager.GetColors(), "Color_Id", "Color_Name");
            ViewBag.Size = new SelectList(_productManager.GetSizes(), "Size_Id", "Size_Name");
            ApplicationDbContext db = new ApplicationDbContext();
             product = db.Product.Where(m => m.Product_Id== id).First();

            return View(product);


        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SepeteEkle(Product product, FormCollection form)
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}