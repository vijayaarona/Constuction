using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using issConstructions.Models;
using issDomain.Models;

namespace issConstructions.Controllers
{
    public class DesignationMastersController : Controller
    {
        private issDB db = new issDB();

        // GET: DesignationMasters
        public ActionResult Index()
        {
            return View(db.designationMasters.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }

        // GET: DesignationMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DesignationMaster designationMaster = db.designationMasters.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (designationMaster == null)
            {
                return HttpNotFound();
            }
            return View(designationMaster);
        }

        // GET: DesignationMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DesignationMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DesignationName,Remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] DesignationMaster designationMaster)
        {
            if (ModelState.IsValid)
            {
                designationMaster.CreatedDate = DateTime.UtcNow;
                designationMaster.UpdatedDate = DateTime.UtcNow;
                db.designationMasters.Add(designationMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(designationMaster);
        }

        // GET: DesignationMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DesignationMaster designationMaster = db.designationMasters.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (designationMaster == null)
            {
                return HttpNotFound();
            }
            return View(designationMaster);
        }

        // POST: DesignationMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DesignationName,Remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] DesignationMaster designationMaster)
        {
            if (ModelState.IsValid)
            {
                designationMaster.UpdatedDate = DateTime.UtcNow;
                designationMaster.CreatedDate = DateTime.UtcNow;
                db.Entry(designationMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(designationMaster);
        }

        // GET: DesignationMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DesignationMaster designationMaster = db.designationMasters.Find(id);
            if (designationMaster == null)
            {
                return HttpNotFound();
            }
            return View(designationMaster);
        }

        // POST: DesignationMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DesignationMaster designationMaster = db.designationMasters.Find(id);
            db.designationMasters.Remove(designationMaster);
            designationMaster.isDeleted = true;
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
