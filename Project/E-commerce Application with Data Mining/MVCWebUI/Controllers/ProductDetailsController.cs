using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.DAL.Concrete.EntityFramework;
using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;
using Ecommerce.BLL.Concrete;
using System.Collections;
using MVCWebUI.DataMining;
namespace MVCWebUI.Controllers
{
    public class ProductDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ProductManager _productManager = new ProductManager();

        // GET: Product2
        public ActionResult Index()
        {
            var product = db.Product.Include(p => p.ProductBoutiqueId).Include(p => p.SubCategory);
            return View(product.ToList());
        }
        public void AssignSizeData(ProductSizeData viewModel,int? id,Product product)
        {
            
            viewModel.Products = _productManager.GetProducts();
            viewModel.ProductStocks= viewModel.Products.Where(
                              i => i.Product_Id == id.Value).Single().Stocks;
            var productSizes = new HashSet<int>(viewModel.ProductStocks.Select(c => c.Size_Id));
            var radiobuttons = new List<SelectedSizes>();
            foreach (var size in viewModel.ProductStocks)
            {

                radiobuttons.Add(new SelectedSizes
                {
                    Product_Id=product.Product_Id,
                    Id = size.Size_Id,
                    Name = size.Size.Size_Name,
                    IsSelected = productSizes.Contains(size.Size_Id)

                });

            }
            ViewBag.Sizes = radiobuttons;//Details View'inde bu viewbag sayesinde listeleme yapılır.
        }

        // GET: Product2/Details/5
        public ActionResult Details(int? id)
        {
            ProductManager _productManager = new ProductManager();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            var viewmodel = new ProductColorData();
            var viewModel = new ProductSizeData();

            viewmodel.Products = _productManager.GetProducts();
            viewmodel.ProductColors = viewmodel.Products.Where(
                   i => i.Product_Id == id.Value).Single().Colors;
            AssignSizeData(viewModel,id,product);
            List<int> productid = new List<int>();
            productid.Add(product.Product_Id); 
            //METHOD CALLING
          List<int[]> transactionlist= AprioriProcess.CreateTransactions();
          List<int> allproducts = AprioriProcess.ListAllProducts();
          List<AdvicedProduct> advices = AprioriProcess.DoAppriori(productid, allproducts, allproducts.Count, transactionlist);
          ViewBag.advice = advices;

          if (product == null)
                {
                    return HttpNotFound();
                }
            return View(product);
        }
       
        // GET: Product2/Create
        public ActionResult Create()
        {
            ViewBag.Product_Boutique_Id = new SelectList(db.Boutique, "Boutique_Id", "Boutique_Description");
            ViewBag.Product_SubCategory_Id = new SelectList(db.SubCategory, "SubCategory_Id", "SubCategory_Name");
            return View();
        }

        // POST: Product2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_Id,Product_Description,Product_Cost,Product_Warning,Product_SubCategory_Id,Product_Boutique_Id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Product_Boutique_Id = new SelectList(db.Boutique, "Boutique_Id", "Boutique_Description", product.Product_Boutique_Id);
            ViewBag.Product_SubCategory_Id = new SelectList(db.SubCategory, "SubCategory_Id", "SubCategory_Name", product.Product_SubCategory_Id);
            return View(product);
        }

        // GET: Product2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product_Boutique_Id = new SelectList(db.Boutique, "Boutique_Id", "Boutique_Description", product.Product_Boutique_Id);
            ViewBag.Product_SubCategory_Id = new SelectList(db.SubCategory, "SubCategory_Id", "SubCategory_Name", product.Product_SubCategory_Id);
            return View(product);
        }

        // POST: Product2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_Id,Product_Description,Product_Cost,Product_Warning,Product_SubCategory_Id,Product_Boutique_Id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Product_Boutique_Id = new SelectList(db.Boutique, "Boutique_Id", "Boutique_Description", product.Product_Boutique_Id);
            ViewBag.Product_SubCategory_Id = new SelectList(db.SubCategory, "SubCategory_Id", "SubCategory_Name", product.Product_SubCategory_Id);
            return View(product);
        }

        // GET: Product2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
