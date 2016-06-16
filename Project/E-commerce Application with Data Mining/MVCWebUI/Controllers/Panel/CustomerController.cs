using Ecommerce.BLL.Concrete;
using Ecommerce.Entities;
using MVCWebUI.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCWebUI.Controllers.PanelCustomer
{
    public class CustomerController : Controller
    {
        CustomerManager _userManager = new CustomerManager();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CustomerDelete(Customer user)
        {

            ViewBag.TypeOfCustomer = _userManager.Delete(user.Customer_Id);
            return RedirectToAction("CustomerList");
        }
        public ActionResult List()
        {
            List<Customer> users = _userManager.GetAll();
            return View(users);
        }

        public ActionResult Add()
        {
            ViewBag.TypeOfCustomer = new SelectList(_userManager.GetTypeOfCustomers(), "TypeOfCustomer_Id", "TypeOfCustomer_Description");
         //   ViewBag.City = new SelectList(_userManager.GetCity(), "Id", "Name");
            ViewBag.County = new SelectList(_userManager.GetCityCounty(), "Id", "Name");
            return View();
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult Add(Customer user,City c, FormCollection form)
        {
            ViewBag.TypeOfCustomer = new SelectList(_userManager.GetTypeOfCustomers(), "TypeOfCustomer_Id", "TypeOfCustomer_Description");
            //   ViewBag.City = new SelectList(_userManager.GetCity(), "Id", "Name");
            ViewBag.County = new SelectList(_userManager.GetCityCounty(), "Id", "Name");
            var typeId = Convert.ToInt32(form["CustomerTypeOfCustomerId"]);
            user.CustomerTypeOfCustomerId = typeId;
            var typeId1 = Convert.ToInt32(form["Customer_County_Id"]);

            user.Customer_County_Id = typeId1;
            user.ActivationCode = "122";
            
            user.IsActive = true;
            user.IsDelete = false;
            user.AddDate = DateTime.Now;
           user.Customer_County_Id = 1;
            
           
            List<string> mail = new List<string>();
         //   bool isSuccess = _userManager.Add(user);
         _userManager.Add(user);

           // if (isSuccess)
          // FunctionHelpers.MailSend(mail);

            return RedirectToAction("List");
        }


        public ActionResult Edit(int id)
        {
            Customer user = _userManager.Get(id);
            ViewBag.TypeOfCustomer = new SelectList(_userManager.GetTypeOfCustomers(), "TypeOfCustomer_Id", "TypeOfCustomer_Description", user.CustomerTypeOfCustomer);
            ViewBag.County = new SelectList(_userManager.GetCityCounty(), "Id", "Name");
            return View(user);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Customer user, FormCollection form)
        {
            var typeId = Convert.ToInt32(form["TypeOfCustomer"]);
            user.CustomerTypeOfCustomerId = typeId;
            user.IsActive = true;
            user.ActivationCode = "122";
            user.CustomerTypeOfCustomerId = typeId;
            user.IsActive = true;
            user.IsDelete = false;
            user.AddDate = DateTime.Now;
            user.Customer_County_Id = 1;

            _userManager.Update(user);
            return RedirectToAction("List");
        }



      

    }
}