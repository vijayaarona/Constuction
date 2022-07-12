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
    //[CustomAuthorize(Roles = "Admin,Manager")]
    public class CategoryMastersController : Controller
    {
        private issDB db = new issDB();
        // GET: CategoryMasters
        public ActionResult Index()
        {
            return View(db.categoryMasters.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }
        // GET: CategoryMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryMaster categoryMaster = db.categoryMasters.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (categoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(categoryMaster);
        }
        // GET: CategoryMasters/Create
        public ActionResult Create()
        {
            ViewBag.CategoryName = "";
            return View();
        }
        // POST: CategoryMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CategoryName,Remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] CategoryMaster categoryMaster)
        {
             if (ModelState.IsValid)
            {
                var duplicate = db.categoryMasters.Where(x => x.CategoryName == categoryMaster.CategoryName).FirstOrDefault();
                if (duplicate == null)
                {
                    categoryMaster.CreatedDate = DateTime.UtcNow;
                    categoryMaster.UpdatedDate = DateTime.UtcNow;
                    db.categoryMasters.Add(categoryMaster);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else {
                    ViewBag.CategoryName = "Already Exists...!";
                }       
            }
            return View(categoryMaster);
        }
        // GET: CategoryMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryMaster categoryMaster = db.categoryMasters.Find(id);
            if (categoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(categoryMaster);
        }
        // POST: CategoryMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryName,Remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] CategoryMaster categoryMaster)
        {
            if (ModelState.IsValid)
            {
                categoryMaster.UpdatedDate = DateTime.UtcNow;
                categoryMaster.CreatedDate = DateTime.UtcNow;
                db.Entry(categoryMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryMaster);
        }
        // GET: CategoryMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryMaster categoryMaster = db.categoryMasters.Find(id);
            if (categoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(categoryMaster);
        }
        // POST: CategoryMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryMaster categoryMaster = db.categoryMasters.Find(id);
            categoryMaster.isDeleted = true;
            db.categoryMasters.Remove(categoryMaster);
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