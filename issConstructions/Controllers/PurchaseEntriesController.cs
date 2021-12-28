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
   // [CustomAuthorize(Roles = "Admin,Manager")]
    public class PurchaseEntriesController : Controller
    {
        private issDB db = new issDB();

        // GET: PurchaseEntries
        public ActionResult Index()
        {
            var purchaseEntries = db.purchaseEntries.Include(p => p.Category).Include(p => p.Supplier);
            return View(purchaseEntries.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }

        // GET: PurchaseEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseEntry purchaseEntry = db.purchaseEntries.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (purchaseEntry == null)
            {
                return HttpNotFound();
            }
            return View(purchaseEntry);
        }

        // GET: PurchaseEntries/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName");
            ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername");
            ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address");
            ViewBag.ProjectId = new SelectList(db.siteDetails, "ID", "ProjectName");
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName");
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress");
            ViewBag.OrderId = new SelectList(db.PurchaseOrders, "ID", "OrderId");
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
        [HttpPost]
        public JsonResult SupplierId(int supplier_Name)
        {
            if (supplier_Name > 0)
            {
                var resp = db.supplierMasters.Where(x => x.CategoryId == supplier_Name).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SupplierAddressId(int supplier_Address)
        {
            if (supplier_Address > 0)
            {
                var resp = db.supplierMasters.Where(x => x.ID == supplier_Address).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SiteId(int site_Name)
        {
            if (site_Name > 0)
            {
                var resp = db.siteDetails.Where(x => x.ID == site_Name).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TaxId(int tax_Amount)
        {
            if (tax_Amount > 0)
            {
                var resp = db.productMasters.Where(x => x.ID == tax_Amount).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }

        // POST: PurchaseEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OrderId,purchaseId,purchaseDate,CategoryId,SupplierId,SupplierAddressId,ProjectId,SiteId,SiteAddressId,mobileno,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseEntry purchaseEntry)
        {
            if (ModelState.IsValid)
            {
                purchaseEntry.CreatedDate = DateTime.UtcNow;
                purchaseEntry.UpdatedDate = DateTime.UtcNow;
                db.purchaseEntries.Add(purchaseEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", purchaseEntry.CategoryId);
            ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername", purchaseEntry.SupplierId);
            ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address", purchaseEntry.SupplierAddressId);
            ViewBag.ProjectId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseEntry.ProjectId);
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseEntry.SiteId);
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseEntry.SiteAddressId);
            ViewBag.OrderId = new SelectList(db.PurchaseOrders, "ID", "OrderId", purchaseEntry.OrderId);

            return View(purchaseEntry);
        }

        // GET: PurchaseEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseEntry purchaseEntry = db.purchaseEntries.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (purchaseEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", purchaseEntry.CategoryId);
            ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername", purchaseEntry.SupplierId);
            ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address", purchaseEntry.SupplierAddressId);
            ViewBag.ProjectId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseEntry.ProjectId);
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseEntry.SiteId);
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseEntry.SiteAddressId);
            ViewBag.OrderId = new SelectList(db.PurchaseOrders, "ID", "SiteAddress", purchaseEntry.OrderId);
            return View(purchaseEntry);
        }

        // POST: PurchaseEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OrderId,purchaseId,purchaseDate,CategoryId,SupplierId,SupplierAddressId,ProjectId,SiteId,SiteAddressId,mobileno,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseEntry purchaseEntry)
        {
            if (ModelState.IsValid)
            {
                purchaseEntry.UpdatedDate = DateTime.UtcNow;
                purchaseEntry.CreatedDate = DateTime.UtcNow;
                db.Entry(purchaseEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", purchaseEntry.CategoryId);
            ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername", purchaseEntry.SupplierId);
            ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address", purchaseEntry.SupplierAddressId);
            ViewBag.ProjectId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseEntry.ProjectId);
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseEntry.SiteId);
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseEntry.SiteAddressId);
            ViewBag.OrderId = new SelectList(db.PurchaseOrders, "ID", "OrderId", purchaseEntry.OrderId);
            return View(purchaseEntry);
        }

        // GET: PurchaseEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseEntry purchaseEntry = db.purchaseEntries.Find(id);
            if (purchaseEntry == null)
            {
                return HttpNotFound();
            }
            return View(purchaseEntry);
        }

        // POST: PurchaseEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseEntry purchaseEntry = db.purchaseEntries.Find(id);
            purchaseEntry.isDeleted = true;
            db.purchaseEntries.Remove(purchaseEntry);
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
