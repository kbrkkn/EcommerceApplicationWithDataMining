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
    public class SizeController : Controller
    {
        SizeManager _sizeManager = new SizeManager();
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Size
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SizeList()
        {
            List<Size> sizes = _sizeManager.GetSizes();
            return View(sizes);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult SizeAdd()
        {
            return View("SizeAdd");
        }

        // POST: Size/SizeAdd
        [HttpPost]
        public ActionResult SizeAdd([Bind(Include = "Size_Name")]Size size)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _sizeManager.Add(size);
                    return RedirectToAction("SizeList");

                }
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Yeniden deneyin");
            }
            return View(size);

        }



        // GET: Size/Delete
        [Authorize(Roles = "Admin")]
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
            Size size = db.Size.Find(id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Size size = db.Size.Find(id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Size size)
        {

            try
            {
                // TODO: Add update logic here
                _sizeManager.Update(size);
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Edit");
            }
            return RedirectToAction("SizeList");
        }

        // POST: Size/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Size size = db.Size.Find(id);
                db.Size.Remove(size);
                db.SaveChanges();
            }


            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("SizeList");
        }
    }
}