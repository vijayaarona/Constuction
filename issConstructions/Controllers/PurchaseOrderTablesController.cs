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
    public class PurchaseOrderTablesController : Controller
    {
        private issDB db = new issDB();

        // GET: PurchaseOrderTables
        public ActionResult Index(int Id)
        {
             ViewBag.pId = Id;
            var purchaseOrder = db.PurchaseOrders.Where(x => x.ID == Id).FirstOrDefault();
            if (purchaseOrder != null)
            {
                ViewBag.ProjectName = db.siteDetails.Where(x => x.ID == purchaseOrder.ProjectId).FirstOrDefault();
                if (ViewBag.ProjectName == null)
                {
                    ViewBag.ProjectName = "Project 1";

                }
                else ViewBag.ProjectName = ViewBag.ProjectName.SiteName;


                ViewBag.supplierName = db.supplierMasters.Where(x => x.ID == purchaseOrder.SupplierId).FirstOrDefault();
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
           
           
            var purchaseOrderTables = db.purchaseOrderTables.Include(p => p.Product).Where(p => p.purchaseRequestId == Id);
            return View(purchaseOrderTables.ToList());
        }

        // GET: PurchaseOrderTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderTable purchaseOrderTable = db.purchaseOrderTables.Find(id);
            if (purchaseOrderTable == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderTable);
        }

        // GET: PurchaseOrderTables/Create
        public ActionResult Create(int id)
        {
            ViewBag.PId = id;
            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName");
            return View();
        }

        // POST: PurchaseOrderTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,purchaseRequestId,productId,Description,Tax,Rate,Quantity,Amount,TaxAmount,TotalAmount,discountPercent,discountAmount,ProductNo,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseOrderTable purchaseOrderTable)
        {
            if (ModelState.IsValid)
            {
                db.purchaseOrderTables.Add(purchaseOrderTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName", purchaseOrderTable.productId);
            return View(purchaseOrderTable);
        }

        // GET: PurchaseOrderTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderTable purchaseOrderTable = db.purchaseOrderTables.Find(id);
            if (purchaseOrderTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName", purchaseOrderTable.productId);
            return View(purchaseOrderTable);
        }

        // POST: PurchaseOrderTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,purchaseRequestId,productId,Description,Tax,Rate,Quantity,Amount,TaxAmount,TotalAmount,discountPercent,discountAmount,ProductNo,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseOrderTable purchaseOrderTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOrderTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", purchaseOrderTable.purchaseRequestId );
            }
            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName", purchaseOrderTable.productId);
            return View(purchaseOrderTable);
        }

        // GET: PurchaseOrderTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderTable purchaseOrderTable = db.purchaseOrderTables.Find(id);
            if (purchaseOrderTable == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderTable);
        }

        // POST: PurchaseOrderTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrderTable purchaseOrderTable = db.purchaseOrderTables.Find(id);
            db.purchaseOrderTables.Remove(purchaseOrderTable);
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
