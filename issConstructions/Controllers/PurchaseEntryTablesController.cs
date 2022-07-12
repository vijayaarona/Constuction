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
    public class PurchaseEntryTablesController : Controller
    {
        private issDB db = new issDB();

        // GET: PurchaseEntryTables
        public ActionResult Index(int Id)
        {
            ViewBag.pId = Id;
            var purchaseEntries = db.purchaseEntries.Where(x => x.ID == Id).FirstOrDefault();
            if (purchaseEntries != null)
            {
                ViewBag.ProjectName = db.siteDetails.Where(x => x.ID == purchaseEntries.ProjectId).FirstOrDefault();
                if (ViewBag.ProjectName == null)
                {
                    ViewBag.ProjectName = "Project 1";

                }
                else ViewBag.ProjectName = ViewBag.ProjectName.SiteName;


                ViewBag.supplierName = db.supplierMasters.Where(x => x.ID == purchaseEntries.SupplierId).FirstOrDefault();
                if (ViewBag.supplierName == null)
                {
                    ViewBag.supplierName = "Supplier 1";

                }
                else ViewBag.supplierName = ViewBag.supplierName.Suppliername;

            }
            else
            {
                ViewBag.ProjectName = "Project 1";
                ViewBag.supplierName = "Supplier 1";
            }

            var purchaseEntryTables = db.purchaseEntryTables.Include(p => p.Product).Where(p => p.purchaseRequestId == Id);
            return View(purchaseEntryTables.ToList());

        }

        // GET: PurchaseEntryTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseEntryTable purchaseEntryTable = db.purchaseEntryTables.Find(id);
            if (purchaseEntryTable == null)
            {
                return HttpNotFound();
            }
            return View(purchaseEntryTable);
        }

        // GET: PurchaseEntryTables/Create
        public ActionResult Create(int id)
        {
            ViewBag.PId = id;
            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName");
            return View();
            
        }

        // POST: PurchaseEntryTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,purchaseRequestId,productId,Description,Tax,Rate,Quantity,Amount,TaxAmount,TotalAmount,discountPercent,discountAmount,ProductNo,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseEntryTable purchaseEntryTable)
        {
            if (ModelState.IsValid)
            {
                db.purchaseEntryTables.Add(purchaseEntryTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName", purchaseEntryTable.productId);
            return View(purchaseEntryTable);
        }

        // GET: PurchaseEntryTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseEntryTable purchaseEntryTable = db.purchaseEntryTables.Find(id);
            if (purchaseEntryTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName", purchaseEntryTable.productId);
            return View(purchaseEntryTable);
        }

        // POST: PurchaseEntryTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,purchaseRequestId,productId,Description,Tax,Rate,Quantity,Amount,TaxAmount,TotalAmount,discountPercent,discountAmount,ProductNo,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseEntryTable purchaseEntryTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseEntryTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", purchaseEntryTable.purchaseRequestId);
            }
            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName", purchaseEntryTable.productId);
            return View(purchaseEntryTable);
        }

        // GET: PurchaseEntryTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseEntryTable purchaseEntryTable = db.purchaseEntryTables.Find(id);
            if (purchaseEntryTable == null)
            {
                return HttpNotFound();
            }
            return View(purchaseEntryTable);
        }

        // POST: PurchaseEntryTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseEntryTable purchaseEntryTable = db.purchaseEntryTables.Find(id);
            db.purchaseEntryTables.Remove(purchaseEntryTable);
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
