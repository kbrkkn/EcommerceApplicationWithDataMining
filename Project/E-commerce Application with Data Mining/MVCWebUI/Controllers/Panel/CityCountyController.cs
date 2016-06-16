using Ecommerce.BLL.Concrete;
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
    public class CityCountyController : Controller
    {
        CityCountyManager _citycountyManager = new CityCountyManager();
        // GET: CityCounty
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CityCountyList()
        {
        
            List<County> citycounty = _citycountyManager.GetAll();
            return View(citycounty);
        }


        // GET: CityCounty/Create
        [Authorize(Roles = "Admin")]
        public ActionResult CityCountyAdd()
        {
            ViewBag.City = new SelectList(_citycountyManager.List(), "Id", "Name");
            return View();
        }

        // POST: CityCounty/Create
        [HttpPost]
        public ActionResult CityCountyAdd(County citycounty)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _citycountyManager.Add(citycounty);
                    return RedirectToAction("CityCountyList");
                }
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Yeniden deneyin");
            }
            return View(citycounty);
        }

        // GET: CityCounty/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            County citycounty = _citycountyManager.Find(id);
            ViewBag.City = new SelectList(_citycountyManager.List(), "Id", "Name");
            if (citycounty == null)
            {
                return HttpNotFound();
            }
            return View(citycounty);
        }

        // POST: CityCounty/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, County citycounty)
        {
            try
            {
                _citycountyManager.Update(citycounty);
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Edit");
            }
            return RedirectToAction("CityCountyList");

        }

        // GET: CityCounty/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id, bool? Error = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Error.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "silinemiyor";
            }
            County citycounty = _citycountyManager.Find(id);
            if (citycounty == null)
            {
                return HttpNotFound();
            }
            return View(citycounty);
        }


        // POST: CityCounty/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {

                _citycountyManager.Delete(id);

            }


            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, Error = true });
            }
            return RedirectToAction("CityCountyList");
        }
    }
}

