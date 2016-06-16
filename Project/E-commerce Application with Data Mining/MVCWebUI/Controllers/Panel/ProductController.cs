using Ecommerce.BLL.Concrete;
using Ecommerce.DAL.Concrete.EntityFramework;
using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebUI.Controllers.Panel
{
    public class ProductController : Controller
    {
      
        ProductManager _productManager = new ProductManager();
        ImageManager _imageManager = new ImageManager();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductList(int? id)
        {
            //Ecommerce.Entities.ViewModel içerisindeki classlar
            var viewModel = new ProductSizeData();
            var viewmodel = new ProductColorData();

            viewModel.Products = _productManager.GetProducts();
            viewmodel.Products = _productManager.GetProducts();

            if (id != null)
                //Productları listelerken o productın color,size ve stock bilgileri için ilgili alana tıklanır
                //ve bu bilgiler listelenir
            {
                ViewBag.ProductID = id.Value;

                viewModel.ProductSizes = viewModel.Products.Where(
                    i => i.Product_Id == id.Value).Single().Sizes;


                viewmodel.ProductColors = viewmodel.Products.Where(
                    i => i.Product_Id == id.Value).Single().Colors;

                viewModel.ProductStocks = viewModel.Products
                       .Where(i => i.Product_Id == id.Value).Single().Stocks;

            }
            return View(Tuple.Create(viewmodel, viewModel));
        }


        [Authorize(Roles = "Admin")]//Productları sadece Admin role'ünde olanlar ekleyebilir.
        public ActionResult ProductAdd()
        {
            var product = new Product();
            var stock = new Stock();

            product.Colors = new List<Color>();
            product.Sizes = new List<Size>();
            //Product eklerken color ve size için checkboxlar'ı oluşturan metodlar çağırılır.
            AssignColorData(product);
            AssignSizeData(product,stock);

            ViewBag.SubCategory = new SelectList(_productManager.GetSubCategories(), "SubCategory_Id", "SubCategory_Name");
            ViewBag.Boutique = new SelectList(_productManager.GetBoutiques(), "Boutique_Id", "Boutique_Description");
            return View();
        }

   //Color checkboxları oluşturan metot.Her yeni color eklendiğinde,product ekleme sayfasına da onların checkboxı eklenir.
        public void AssignColorData(Product product)
        {
            var allcolors = _productManager.GetColors();
            var productColors = new HashSet<int>(product.Colors.Select(c => c.Color_Id));
            var checkboxes = new List<SelectedColor>();

            foreach (var color in allcolors)
            {
                checkboxes.Add(new SelectedColor
                {
                    Id = color.Color_Id,
                    Name = color.Color_Name,
                    IsSelected = productColors.Contains(color.Color_Id)//Contains bool değer döndürür.

                });
            }
            ViewBag.Colors = checkboxes;
        }

        //Size için checkbox ve her bir checkboxa stock girmek için textbox eklenir.
        public void AssignSizeData(Product product, Stock stock)
        {
            var allsizes = _productManager.GetSizes();
            var productSizes = new HashSet<int>(product.Sizes.Select(c => c.Size_Id));
            var checkboxes = new List<SelectedSizes>();
            foreach (var size in allsizes)
            {

                checkboxes.Add(new SelectedSizes
                {
                    Id = size.Size_Id,
                    Name = size.Size_Name,
                    IsSelected = productSizes.Contains(size.Size_Id),
                  NumberOfstock =stock.NumberOfStock

                });

            }
            ViewBag.Sizes = checkboxes;//ProductAdd View'inde bu viewbag sayesinde listeleme yapılır.
        }


        [HttpPost, ValidateInput(false)]
     public ActionResult ProductAdd(Product product, Image l,Stock stock,FormCollection form, string[] selectedSizes, string[] selectedColors,List<int>numbers)
        {
            ApplicationDbContext db = new ApplicationDbContext();
           var typeId2 = Convert.ToInt32(form["Product_SubCategory_Id"]);
            product.Product_SubCategory_Id = typeId2;
            var typeId3 = Convert.ToInt32(form["Product_Boutique_Id"]);
            product.Product_Boutique_Id = typeId3;

            if (l.File!=null && l.File.ContentLength > 0 )
            {
                var filename = Path.GetFileName(l.File.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                l.File.SaveAs(path);

            }
           
            l.FileName = l.File.FileName;
            l.Product_Id = product.Product_Id;

            byte[] data = new byte[l.File.ContentLength];
            l.File.InputStream.Read(data, 0, l.File.ContentLength);
            l.ImageData = data;
           

            if (selectedColors != null)
            {
                product.Colors = new List<Color>();

                foreach (var assignedColor in selectedColors)
                {
                    var toadd = _productManager.FindtoAddColor(assignedColor);
                    product.Colors.Add(toadd);

                }
            }


            if (selectedSizes != null)
            {
                Queue<int> q = new Queue<int>();
                product.Sizes = new List<Size>();
                product.Stocks = new List<Stock>();
                //stock textboxlarına yazılan sayıları queueye koyar.
                for (int i = 0; i < numbers.Count; i++)
                {
                    q.Enqueue(numbers[i]);
                }

                foreach (var assignedSize in selectedSizes)
                {

                    //seçilen size checkboxının id sini,size tablosundan bulur ve listeye ekler.
                    var toadd = _productManager.FindtoAddSize(assignedSize);
                    product.Sizes.Add(toadd);

                    int sizeıd = toadd.Size_Id;
                    //stokları ekle.
                    product.Stocks.Add(new Stock
                    {
                        Size_Id = sizeıd,
                        Product_Id = product.Product_Id,
                        NumberOfStock = q.Dequeue()//sırasıyla queuden çekerek,listeye ekler.
                    });


                } foreach (var e in product.Stocks) { db.Stock.Add(e); }//stock tablosuna kaydedilir.
            }
           
            
                if (ModelState.IsValid)
                {
                    _productManager.Add(product,l);

                   return RedirectToAction("ProductList");
                }
                   
            return View(product);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult ProductEdit(int id)
        {
            Product user = _productManager.Get(id);
            //  ViewBag.Color = new SelectList(_productManager.GetColors(), "Color_Id", "Color_Name", user.Product_Color_Id);
            //    ViewBag.Size = new SelectList(_productManager.GetSizes(), "Size_Id", "Size_Name", user.Product_Size_Id);
            ViewBag.SubCategory = new SelectList(_productManager.GetSubCategories(), "SubCategory_Id", "SubCategory_Name", user.Product_SubCategory_Id);
            return View(user);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProductEdit(Product user, FormCollection form)
        {
            //  var typeId = Convert.ToInt32(form["Product_Color_Id"]);
            // user.Product_Color_Id = typeId;
            /*   var typeId1 = Convert.ToInt32(form["Product_Size_Id"]);
               user.Product_Size_Id = typeId1;*/
            var typeId2 = Convert.ToInt32(form["Product_SubCategory_Id"]);
            user.Product_SubCategory_Id = typeId2;


            _productManager.Update(user);


            return RedirectToAction("ProductList");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ProductDelete(Product user)
        {

            return RedirectToAction("ProductList");
        }

    }
}