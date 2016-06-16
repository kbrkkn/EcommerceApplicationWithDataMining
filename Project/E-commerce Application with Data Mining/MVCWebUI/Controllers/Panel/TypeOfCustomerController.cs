using Ecommerce.BLL.Concrete;
using Ecommerce.DAL.Concrete.EntityFramework;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MVCWebUI.Controllers.Panel
{
    public class TypeOfCustomerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        TypeOfCustomerManager _typeOfCustomerManager = new TypeOfCustomerManager();
        // GET: TypeOfCustomer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TypeOfCustomerList()
        {
            List<TypeOfCustomer> typeOfCustomers = _typeOfCustomerManager.GetTypeOfCustomers();
            return View(typeOfCustomers);
        }
        public ActionResult Add()
        {
            return View("Add");
        }

        // POST: TypeOfCustomer/Add
        [HttpPost]
        public ActionResult Add([Bind(Include = "TypeOfCustomer_Description")]TypeOfCustomer typeOfCustomer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _typeOfCustomerManager.Add(typeOfCustomer);
                    return RedirectToAction("TypeOfCustomerList");

                }
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Yeniden deneyin");
            }
            return View(typeOfCustomer);

        }
        // GET: City/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfCustomer typeOfCustomer = _typeOfCustomerManager.Get(id);
            if (typeOfCustomer == null)
            {
                return HttpNotFound();
            }
            return View(typeOfCustomer);
        }

        // POST: City/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TypeOfCustomer city)
        {
            try
            {
                // TODO: Add update logic here
                _typeOfCustomerManager.Update(city);
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Edit");
            }
            return RedirectToAction("TypeOfCustomerList");

        }


        // GET: TypeOfCustomer/Delete
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
            TypeOfCustomer typeOfCustomer = db.TypeOfCustomer.Find(id);
            if (typeOfCustomer == null)
            {
                return HttpNotFound();
            }
            return View(typeOfCustomer);
        }

        // POST: TypeOfCustomer/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                TypeOfCustomer typeOfCustomer = db.TypeOfCustomer.Find(id);
                db.TypeOfCustomer.Remove(typeOfCustomer);
                db.SaveChanges();
            }


            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("TypeOfCustomerList");
        }
    }
}