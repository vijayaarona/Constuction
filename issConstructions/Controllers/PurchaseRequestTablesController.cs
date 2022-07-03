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
    public class PurchaseRequestTablesController : Controller
    {
        private issDB db = new issDB();

        // GET: PurchaseRequestTables
        public ActionResult Index(int Id = 0)
        {
            ViewBag.pId = Id;
            var purchaseRequest = db.purchaseRequest.Where(x => x.ID == Id).FirstOrDefault();
            if (purchaseRequest != null)
            {
                ViewBag.ProjectName = db.siteDetails.Where(x => x.ID == purchaseRequest.ProjectId).FirstOrDefault();
                if (ViewBag.ProjectName == null)
                {
                    ViewBag.ProjectName = "Project 1";

                }
                else ViewBag.ProjectName = ViewBag.ProjectName.SiteName;


                ViewBag.supplierName = db.supplierMasters.Where(x => x.ID == purchaseRequest.SupplierId).FirstOrDefault();
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
            
            var purchaseRequestTables = db.purchaseRequestTables.Include(p => p.Product).Where(x => x.purchaseRequestId == Id);
            return View(purchaseRequestTables.ToList());
        }

        // GET: PurchaseRequestTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseRequestTable purchaseRequestTable = db.purchaseRequestTables.Find(id);
            if (purchaseRequestTable == null)
            {
                return HttpNotFound();
            }
            return View(purchaseRequestTable);
        }

        // GET: PurchaseRequestTables/Create
        public ActionResult Create(int id)
        {
            ViewBag.PId = id;
            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName");
            return View();
        }

        // POST: PurchaseRequestTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,purchaseRequestId,productId,Description,Tax,Rate,Quantity,Amount,TaxAmount,TotalAmount,discountPercent,discountAmount,ProductNo,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseRequestTable purchaseRequestTable)
        {
            if (ModelState.IsValid)
            {
                db.purchaseRequestTables.Add(purchaseRequestTable);
                db.SaveChanges();
                return RedirectToAction("Index",purchaseRequestTable.purchaseRequestId);
            }

            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName", purchaseRequestTable.productId);
            return View(purchaseRequestTable);
        }

        // GET: PurchaseRequestTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseRequestTable purchaseRequestTable = db.purchaseRequestTables.Find(id);
            if (purchaseRequestTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName", purchaseRequestTable.productId);
            return View(purchaseRequestTable);
        }

        // POST: PurchaseRequestTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,purchaseRequestId,productId,Description,Tax,Rate,Quantity,Amount,TaxAmount,TotalAmount,discountPercent,discountAmount,ProductNo,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseRequestTable purchaseRequestTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseRequestTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",purchaseRequestTable.purchaseRequestId);
            }
            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName", purchaseRequestTable.productId);
            return View(purchaseRequestTable);
        }

        // GET: PurchaseRequestTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseRequestTable purchaseRequestTable = db.purchaseRequestTables.Find(id);
            if (purchaseRequestTable == null)
            {
                return HttpNotFound();
            }
            return View(purchaseRequestTable);
        }

        // POST: PurchaseRequestTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseRequestTable purchaseRequestTable = db.purchaseRequestTables.Find(id);
            db.purchaseRequestTables.Remove(purchaseRequestTable);
            db.SaveChanges();
            return RedirectToAction("Index", purchaseRequestTable.purchaseRequestId);
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
