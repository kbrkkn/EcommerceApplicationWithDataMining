using Ecommerce.BLL.Concrete;
using Ecommerce.Entities;
using MVCWebUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebUI.Controllers.ControlPanel
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dashboard
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            CustomerManager customer = new CustomerManager();
            string email = form["txtEPosta"];
            string password = form["txtPassword"];

            Customer c=customer.Get(email,password);

            if (c != null) {
                Session[FunctionHelpers.UserSession] = c;
            }

            return RedirectToAction("Index", "Dashboard");
        }

    }
}