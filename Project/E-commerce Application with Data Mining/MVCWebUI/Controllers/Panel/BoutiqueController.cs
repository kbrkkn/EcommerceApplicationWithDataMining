using Ecommerce.BLL.Concrete;
using Ecommerce.DAL.Concrete.EntityFramework;
using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCWebUI.Controllers.Panel
{
    public class BoutiqueController : Controller
    {
        BoutiqueManager _boutiqueManager = new BoutiqueManager();
        ProductManager _productManager = new ProductManager();
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Boutique
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BoutiqueList()
        {
            List<Boutique> boutiques = _boutiqueManager.GetBoutiques();
            return View(boutiques);
        }

        public ActionResult ProductSizeColor(int? id, int? sizeID, int? colorID)
        {

            var viewModel = new ProductSizeData();
            var viewmodel = new ProductColorData();


            viewModel.Products = db.Product.Where(x => x.Product_Id == id).ToList();
            viewmodel.Products =db.Product.Where(x => x.Product_Id == id).ToList();

            if (id != null)
            {
                ViewBag.ProductID = id.Value;
                viewModel.ProductSizes = viewModel.Products.Where(
                    i => i.Product_Id == id.Value).Single().Sizes;

                viewmodel.ProductColors = viewmodel.Products.Where(
                    i => i.Product_Id == id.Value).Single().Colors;

                viewModel.ProductStocks = viewModel.Products
                       .Where(i => i.Product_Id == id.Value).Single().Stocks;
            }

            if (colorID != null)
            {
                ViewBag.SizeID = colorID.Value;
                var selectedSize = viewmodel.ProductColors.Where(x => x.Color_Id == colorID).Single();

            }

            if (sizeID != null)
            {
                ViewBag.SizeID = sizeID.Value;
                var selectedSize = viewModel.ProductSizes.Where(x => x.Size_Id == sizeID).Single();

                ViewBag.StokID = sizeID.Value;
                var stocks = viewModel.ProductStocks.Where(x => x.Size_Id == sizeID).Single();

            }

            return View(Tuple.Create(viewmodel, viewModel));
        } 

        public ActionResult ProductList(int id)
        {

            var productBoutique = db.Product.Where(m => m.Product_Boutique_Id == id).ToList();

            return View(productBoutique);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult BoutiqueAdd()
        {
            return View();
        }

        // POST: Boutique/BoutiqueAdd
         [HttpPost, ValidateInput(false)]
        public ActionResult BoutiqueAdd(Boutique boutique, FormCollection form)
        {
      
    
          
                    if (boutique.File.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(boutique.File.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                       boutique.File.SaveAs(path);

                    }
                    boutique.FileName = boutique.File.FileName;

                    byte[] data = new byte[boutique.File.ContentLength];
                    boutique.File.InputStream.Read(data, 0, boutique.File.ContentLength);
                    boutique.ImageData = data;
                
             if (ModelState.IsValid) {
                    _boutiqueManager.Add(boutique);

                    return RedirectToAction("BoutiqueList"); }
                    return View(boutique);

       

        }

        //ProductAdd

        [Authorize(Roles = "Admin")]
         public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Boutique boutique = db.Boutique.Find(id);
            if (boutique == null)
            {
                return HttpNotFound();
            }
            return View(boutique);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Boutique boutique)
        {

            try
            {
                // TODO: Add update logic here
                _boutiqueManager.Update(boutique);
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Edit");
            }
            return RedirectToAction("BoutiqueList");
        }

        // GET: Boutique delete
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Boutique boutique = db.Boutique.Find(id);
            if (boutique == null)
            {
                return HttpNotFound();
            }
            return View(boutique);
        }

        // POST: Boutique/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _boutiqueManager.Delete(id);

            }

            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, Error = true });

            }
            return RedirectToAction("BoutiqueList");
        }
    }
}