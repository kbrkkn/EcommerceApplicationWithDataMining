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
    public class CategoryController : Controller
    {
        CategoryManager _categoryManager = new CategoryManager();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CategoryList()
        {
            List<Category> categories = _categoryManager.GetAll();
            return View(categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(int ?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _categoryManager.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Category/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View("CategoryAdd");
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Category_Name")]Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoryManager.Add(category);
                    return RedirectToAction("CategoryList");

                }
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Yeniden deneyin");
            }
            return View(category);

        }

        // GET: Category/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _categoryManager.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
   
        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            try
            {
                // TODO: Add update logic here
                _categoryManager.Update(category);
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Edit");
            }
            return RedirectToAction("CategoryList");

        }

        // GET: Category/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id, bool? Error = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Error.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Kategori içinde alt kategoriler olduğundan silinemiyor";

            }
            Category category = _categoryManager.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                 _categoryManager.Delete(id);
              
            }


            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, Error = true });

            }
            return RedirectToAction("CategoryList");
        }
    }
}