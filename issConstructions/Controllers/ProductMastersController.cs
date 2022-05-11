using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using issConstructions.Custom;
using issConstructions.Models;
using issDomain.Models;

namespace issConstructions.Controllers
{
    [CustomAuthorize(Roles = "Admin,Manager")]
    public class ProductMastersController : Controller
    {
        private issDB db = new issDB();
        // GET: ProductMasters
        public ActionResult Index()
        {
            var productMasters = db.productMasters.Include(p => p.Category);
            return View(productMasters.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }
        // GET: ProductMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductMaster productMaster = db.productMasters.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (productMaster == null)
            {
                return HttpNotFound();
            }
            return View(productMaster);
        }
        // GET: ProductMasters/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName");
            ViewBag.ProductName = "";
            return View();
        }
        // POST: ProductMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductName,UOM,Tax,CategoryId,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] ProductMaster productMaster)
        {
            if (ModelState.IsValid)
            {
                var duplicate = db.productMasters.Where(x => x.ProductName == productMaster.ProductName).FirstOrDefault();
                if (duplicate == null)
                {
                    productMaster.CreatedDate = DateTime.UtcNow;
                    productMaster.UpdatedDate = DateTime.UtcNow;
                    db.productMasters.Add(productMaster);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ProductName = "Already Exists....!";
                }
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", productMaster.CategoryId);
            return View(productMaster);
        }
        // GET: ProductMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductMaster productMaster = db.productMasters.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (productMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", productMaster.CategoryId);
            return View(productMaster);
        }
        // POST: ProductMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductName,UOM,Tax,CategoryId,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] ProductMaster productMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productMaster).State = EntityState.Modified;
                productMaster.CreatedDate = DateTime.UtcNow;
                productMaster.UpdatedDate = DateTime.UtcNow;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", productMaster.CategoryId);
            return View(productMaster);
        }
        // GET: ProductMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductMaster productMaster = db.productMasters.Find(id);
            if (productMaster == null)
            {
                return HttpNotFound();
            }
            return View(productMaster);
        }
        // POST: ProductMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductMaster productMaster = db.productMasters.Find(id);
            productMaster.isDeleted = true;
            db.productMasters.Remove(productMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}