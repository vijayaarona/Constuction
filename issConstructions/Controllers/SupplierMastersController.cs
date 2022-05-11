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
using issDomain;
using issDomain.Models;

namespace issConstructions.Controllers
{
    [CustomAuthorize(Roles = "Admin,Manager")]
    public class SupplierMastersController : Controller
    {
        private issDB db = new issDB();
        // GET: SupplierMasters
        public ActionResult Index()
        {
            return View(db.supplierMasters.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }
        // GET: SupplierMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierMaster supplierMaster = db.supplierMasters.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (supplierMaster == null)
            {
                return HttpNotFound();
            }
            return View(supplierMaster);
        }
        // GET: SupplierMasters/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName");
            ViewBag.Suppliername = "";
            return View();
        }
        // POST: SupplierMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Suppliername,isDeleted,address,Contactperson,Phonenumber,EmailID,GSTnumber,MSMEnumber,OpeningBalance,Bankname,Accountnumber,Branch,IFSCcode,CategoryId,City,CreatedDate,UpdateBy,UpdatedDate")] SupplierMaster supplierMaster)
        {
            if (ModelState.IsValid)
            {
                var duplicate = db.supplierMasters.Where(x => x.Suppliername == supplierMaster.Suppliername && x.address == supplierMaster.address).FirstOrDefault();
                if (duplicate == null)
                {
                    supplierMaster.CreatedDate = DateTime.UtcNow;
                    supplierMaster.UpdatedDate = DateTime.UtcNow;
                    db.supplierMasters.Add(supplierMaster);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Suppliername = "Already Exists....!";
                }
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", supplierMaster.CategoryId);
            return View(supplierMaster);
        }
        // GET: SupplierMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            issDomain.Models.SupplierMaster supplierMaster = db.supplierMasters.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (supplierMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", supplierMaster.CategoryId);
            return View(supplierMaster);
        }
        // POST: SupplierMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Suppliername,isDeleted,address,Contactperson,Phonenumber,EmailID,GSTnumber,MSMEnumber,OpeningBalance,Bankname,Accountnumber,Branch,IFSCcode,Categoryid,City,CreatedDate,UpdateBy,UpdatedDate")] SupplierMaster supplierMaster)
        {
            if (ModelState.IsValid)
            {
                supplierMaster.UpdatedDate = DateTime.UtcNow;
                supplierMaster.CreatedDate = DateTime.UtcNow;
                db.Entry(supplierMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplierMaster);
        }
        // GET: SupplierMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierMaster supplierMaster = db.supplierMasters.Find(id);
            if (supplierMaster == null)
            {
                return HttpNotFound();
            }
            return View(supplierMaster);
        }
        // POST: SupplierMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupplierMaster supplierMaster = db.supplierMasters.Find(id);
            supplierMaster.isDeleted = true;
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
