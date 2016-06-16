using Ecommerce.DAL.Concrete.EntityFramework;
using Ecommerce.Entities.ViewModel;
using MVCWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebUI.Controllers.UserPanel
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /ShoppingCart/

        public ActionResult Index()
        {
            var cart = Shopping_Cart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            // Return the view
            return View(viewModel);
        }

        //
        // GET: /Store/AddToCart/5

        public ActionResult AddToCart(int id,string selectedSize)
        {
            var addedSize = db.Size.Find(int.Parse(selectedSize));
            int sizeid = addedSize.Size_Id;

            var addedProduct = db.Product
                .Single(product => product.Product_Id == id);

            // Add it to the shopping cart
            var cart = Shopping_Cart.GetCart(this.HttpContext);

            cart.AddToCart(addedProduct,sizeid);

            // Go back to the main store page for more shopping
            return RedirectToAction("ListProduct", "Home", new { id = addedProduct.Product_Boutique_Id });
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = Shopping_Cart.GetCart(this.HttpContext);

            string productName = db.Cart
                .Single(item => item.RecordId == id).Product.Product_Description;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

           // return Json(results);
            return View(results);
        }
       
        //
        // GET: /ShoppingCart/CartSummary

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = Shopping_Cart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartSummary");
        }
    }
}