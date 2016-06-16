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
    public class SubCategoryController : Controller
    {
        SubCategoryManager _subcategoryManager = new SubCategoryManager();
        // GET: SubCategory
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SubCategoryList()
        {
            List<SubCategory> subcategory = _subcategoryManager.GetAll();
            return View(subcategory);
        }
    

        // GET: SubCategory/Details/5
        public ActionResult Details(int ?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = _subcategoryManager.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // GET: SubCategory/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(_subcategoryManager.List(),"Category_Id","Category_Name");
            return View();
        }

        // POST: SubCategory/Create
        [HttpPost]
        public ActionResult Create(SubCategory subcategory)
        {
            try
            {
                if (ModelState.IsValid) { _subcategoryManager.Add(subcategory); 
                return RedirectToAction("SubCategoryList");}
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Yeniden deneyin");
            }
            return View(subcategory);
        }

        // GET: SubCategory/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int ?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = _subcategoryManager.Find(id);
            ViewBag.Category = new SelectList(_subcategoryManager.List(), "Category_Id", "Category_Name");
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }


        // POST: SubCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,SubCategory subcategory)
        {
            try
            {
                _subcategoryManager.Update(subcategory);
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Edit");
            }
            return RedirectToAction("SubCategoryList");

        }

        // GET: SubCategory/Delete/5
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
            SubCategory subcategory = _subcategoryManager.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }


        // POST: SubCategory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {

                 _subcategoryManager.Delete(id);
              
            }


            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, Error = true });
            }
            return RedirectToAction("SubCategoryList");
        }
    }
    }

