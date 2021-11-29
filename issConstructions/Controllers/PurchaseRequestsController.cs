using issConstructions.Models;
using issDomain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace issConstructions.Controllers
{
    public class PurchaseRequestsController : Controller
    {
        private issDB db = new issDB();

        // GET: PurchaseRequests
        public ActionResult Index()
        {
            var purchaseRequest = db.purchaseRequest.Include(p => p.Category).Include(p => p.Supplier);
            return View(purchaseRequest.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }

        // GET: PurchaseRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseRequest purchaseRequest = db.purchaseRequest.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (purchaseRequest == null)
            {
                return HttpNotFound();
            }
            return View(purchaseRequest);
        }

        // GET: PurchaseRequests/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName");
            ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername");
            ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address");
            ViewBag.ProjectId = new SelectList(db.siteDetails, "ID", "ProjectName");
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName");
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress");

            return View();
        }

        // POST: PurchaseRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RequestID,RequestDate,CategoryId,SupplierId,SupplierAddressId,ProjectId,SiteId,SiteAddressId,mobileno,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseRequest purchaseRequest)
        {
            if (ModelState.IsValid)
            {
                purchaseRequest.CreatedDate = DateTime.UtcNow;
                purchaseRequest.UpdatedDate = DateTime.UtcNow;
                db.purchaseRequest.Add(purchaseRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", purchaseRequest.CategoryId);
            ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername", purchaseRequest.SupplierId);
            ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address", purchaseRequest.SupplierAddressId);
            ViewBag.ProjectId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseRequest.ProjectId);
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseRequest.SiteId);
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseRequest.SiteAddressId);

            return View(purchaseRequest);
        }
        [HttpPost]
        public JsonResult SupplierId(int NAME)
        {
            if (NAME > 0)
            {
                var resp = db.supplierMasters.Where(x => x.CategoryId == NAME).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SupplierAddressId(int SUPPLIERNAME)
        {
            if (SUPPLIERNAME > 0)
            {
                var resp = db.supplierMasters.Where(x => x.SupplierId == SUPPLIERNAME).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }



        // GET: PurchaseRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseRequest purchaseRequest = db.purchaseRequest.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (purchaseRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", purchaseRequest.CategoryId);
            ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername", purchaseRequest.SupplierId);
            ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address", purchaseRequest.SupplierAddressId);
            ViewBag.ProjectId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseRequest.ProjectId);
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseRequest.SiteId);
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseRequest.SiteAddressId);

            return View(purchaseRequest);
        }

        // POST: PurchaseRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RequestID,RequestDate,CategoryId,SupplierId,SupplierAddressId,ProjectId,SiteId,SiteAddressId,mobileno,sno,productId,Description,Rate,Quality,Tax,TotalAmount,RequestBy,Remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseRequest purchaseRequest)
        {
            if (ModelState.IsValid)
            {
                purchaseRequest.CreatedDate = DateTime.UtcNow;
                purchaseRequest.UpdatedDate = DateTime.UtcNow;
                db.Entry(purchaseRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", purchaseRequest.CategoryId);
            ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername", purchaseRequest.SupplierId);
            ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address", purchaseRequest.SupplierAddressId);
            ViewBag.ProjectId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseRequest.ProjectId);
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseRequest.SiteId);
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseRequest.SiteAddressId);
            return View(purchaseRequest);
        }

        // GET: PurchaseRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseRequest purchaseRequest = db.purchaseRequest.Find(id);
            if (purchaseRequest == null)
            {
                return HttpNotFound();
            }
            return View(purchaseRequest);
        }

        // POST: PurchaseRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseRequest purchaseRequest = db.purchaseRequest.Find(id);
            purchaseRequest.isDeleted = true;
            db.purchaseRequest.Remove(purchaseRequest);
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
