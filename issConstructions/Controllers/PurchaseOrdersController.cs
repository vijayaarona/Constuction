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
    public class PurchaseOrdersController : Controller
    {
        private issDB db = new issDB();

        // GET: PurchaseOrders
        public ActionResult Index()
        {
            var purchaseOrders = db.PurchaseOrders.Include(p => p.Category).Include(p => p.Supplier).Include(p => p.SiteDetails);
            return View(purchaseOrders.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }

        // GET: PurchaseOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName");
            ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername");
            ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address");
            ViewBag.ProjectId = new SelectList(db.siteDetails, "ID", "ProjectName");
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName");
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress");
            ViewBag.RequestID = new SelectList(db.purchaseRequest, "ID", "ID");
            //Product
            var listItems = new SelectList(db.productMasters, "ID", "ProductName");
            List<SelectListItem> Product = new List<SelectListItem>();
            foreach (var item in db.productMasters.ToList())
            {
                Product.Add(new SelectListItem { Text = item.ProductName, Value = item.ID.ToString() });
            }
            ViewBag.ProductId = Product;

            //Tax
            var listsItem = new SelectList(db.productMasters, "ID", "Tax");
            List<SelectListItem> Tax = new List<SelectListItem>();
            foreach (var items in db.productMasters.ToList())
            {
                Tax.Add(new SelectListItem { Text = items.Tax.ToString(), Value = items.ID.ToString() });
            }
            ViewBag.ProductTax = Tax;
            return View();
        }

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RequestID,OrderId,OrderDate,CategoryId,SupplierId,SupplierAddressId,ProjectId,SiteId,SiteAddressId,mobileno,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                purchaseOrder.CreatedDate = DateTime.UtcNow;
                purchaseOrder.UpdatedDate = DateTime.UtcNow;
                db.PurchaseOrders.Add(purchaseOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", purchaseOrder.CategoryId);
            ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername", purchaseOrder.SupplierId);
            ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address", purchaseOrder.SupplierAddressId);
            ViewBag.ProjectId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseOrder.ProjectId);
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseOrder.SiteId);
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseOrder.SiteAddressId);
            ViewBag.RequestID = new SelectList(db.purchaseRequest, "ID", "ID",purchaseOrder.PurchaseRequest);

            return View(purchaseOrder);
        }

        [HttpPost]
        public JsonResult purchaseReqOrders(int purchaseRequestOrderId)
        {
            if (purchaseRequestOrderId > 0)
            {
                var resp = db.purchaseRequest.Where(x => x.ID == purchaseRequestOrderId).FirstOrDefault();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }

        // GET: PurchaseOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", purchaseOrder.CategoryId);
            ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername", purchaseOrder.SupplierId);
            ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address", purchaseOrder.SupplierAddressId);
            ViewBag.ProjectId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseOrder.ProjectId);
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseOrder.SiteId);
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseOrder.SiteAddressId);
            ViewBag.RequestID = new SelectList(db.purchaseRequest, "ID", "ID", purchaseOrder.PurchaseRequest);
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RequestID,OrderId,OrderDate,CategoryId,SupplierId,SupplierAddressId,ProjectId,SiteId,SiteAddressId,mobileno,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                purchaseOrder.CreatedDate = DateTime.UtcNow;
                purchaseOrder.UpdatedDate = DateTime.UtcNow;
                db.Entry(purchaseOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", purchaseOrder.CategoryId);
            ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername", purchaseOrder.SupplierId);
            ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address", purchaseOrder.SupplierAddressId);
            ViewBag.ProjectId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseOrder.ProjectId);
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseOrder.SiteId);
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseOrder.SiteAddressId);
            ViewBag.RequestID = new SelectList(db.purchaseRequest, "ID", "ID", purchaseOrder.PurchaseRequest);
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            purchaseOrder.isDeleted = true;
            db.PurchaseOrders.Remove(purchaseOrder);
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
