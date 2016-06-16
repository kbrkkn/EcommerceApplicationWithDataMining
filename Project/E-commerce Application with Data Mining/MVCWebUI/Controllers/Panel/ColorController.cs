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
    public class ColorController : Controller
    {
        ColorManager _colorManager = new ColorManager();
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Color
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ColorList()
        {
            List<Color> colors = _colorManager.GetColors();
            return View(colors);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ColorAdd()
        {
            return View("ColorAdd");
        }

        // POST: Color/ColorAdd
        [HttpPost]
        public ActionResult ColorAdd([Bind(Include = "Color_Name")]Color color)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _colorManager.Add(color);
                    return RedirectToAction("ColorList");

                }
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Yeniden deneyin");
            }
            return View(color);

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Color color)
        {

            try
            {
                // TODO: Add update logic here
                _colorManager.Update(color);
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Edit");
            }
            return RedirectToAction("ColorList");
        }


        // GET: Color/Delete
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
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        // POST: Color/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Color color = db.Color.Find(id);
                db.Color.Remove(color);
                db.SaveChanges();
            }


            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("ColorList");
        }

    }
}