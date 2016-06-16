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
    public class CityController : Controller
    {
        CityManager _cityManager = new CityManager();
        // GET: City
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CityList()
        {
            List<City> cities = _cityManager.GetAll();
            return View(cities);
        }


        // GET: City/Create
        [Authorize(Roles = "Admin")]
        public ActionResult CityAdd()
        {
            return View("CityAdd");
        }

        // POST: City/Create
        [HttpPost]
        public ActionResult CityAdd([Bind(Include = "Name")]City city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _cityManager.Add(city);
                    return RedirectToAction("CityList");

                }
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Yeniden deneyin");
            }
            return View(city);

        }

        // GET: City/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = _cityManager.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: City/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, City city)
        {
            try
            {
                // TODO: Add update logic here
                _cityManager.Update(city);
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Edit");
            }
            return RedirectToAction("CityList");

        }

        // GET: City/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id, bool? Error = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Error.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "şehir içinde ilçeler olduğundan silinemiyor";

            }
            City city = _cityManager.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: City/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _cityManager.Delete(id);

            }


            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, Error = true });

            }
            return RedirectToAction("CityList");
        }
    }
}