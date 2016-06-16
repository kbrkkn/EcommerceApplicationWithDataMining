using Ecommerce.BLL.Concrete;
using Ecommerce.DAL.Concrete.EntityFramework;
using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;
using MVCWebUI.DataMining;
using MVCWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebUI.Controllers.UserPanel
{
    [Authorize]
    public class CheckOutController : Controller
    {
        OrderManager x = new OrderManager();
        ApplicationDbContext db = new ApplicationDbContext();
      
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        //
        // POST: /Checkout/AddressAndPayment

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
            
          
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                    //Save Order
                    db.Order.Add(order);
                    db.SaveChanges();

                    //Process the order
                    var cart = Shopping_Cart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
             //   }

            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete

        public ActionResult Complete(int id)//id is orderid,data mining için bu orderid ile alınan ürünleri arraya koy
            //bu array ile rules'ın antec.arrayinin elemanları birbirine eşitse,consq. getir.
        {
            // Validate customer owns this order
            bool isValid = db.Order.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);
            List<int> productids= db.OrderDetail.Where(x => x.OrderId == id).Select(x => x.ProductId).ToList();

            if (isValid)
            {
                //OrderDetail y = x.Find(id);
                List<int[]> transactionlist = AprioriProcess.CreateTransactions();
                List<int> allproducts = AprioriProcess.ListAllProducts();
                List<AdvicedProduct> advices = AprioriProcess.DoAppriori(productids, allproducts, allproducts.Count, transactionlist);
                ViewBag.advice = advices;
                return View();
            }
            else
            {
                return View("Error");
            }
        }
        
            
       

    }
}