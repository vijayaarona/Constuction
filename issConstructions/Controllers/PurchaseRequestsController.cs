using issConstructions.Models;
using issDomain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using issConstructions.Custom;
using System.Web.Mvc;

namespace issConstructions.Controllers
{
    public class PurchaseRequestsController : Controller
    {
        private issDB db = new issDB();
        // GET: PurchaseRequests
        public ActionResult Index()
        {
            var purchaseRequest = db.purchaseRequest.Include(p => p.Category).Include(p => p.Supplier).Include(p => p.SiteDetails);
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
            List<SelectListItem> Category = new List<SelectListItem>();
            Category.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.categoryMasters.ToList())
            {
                Category.Add(new SelectListItem { Text = item.CategoryName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.CategoryId = Category;
            List<SelectListItem> Supplier = new List<SelectListItem>();
            Supplier.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.supplierMasters.ToList())
            {
                Supplier.Add(new SelectListItem { Text = item.Suppliername.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.SupplierId = Supplier;
            List<SelectListItem> SupplierAddress = new List<SelectListItem>();
            SupplierAddress.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.supplierMasters.ToList())
            {
                SupplierAddress.Add(new SelectListItem { Text = item.address.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.SupplierAddressId = SupplierAddress;
            List<SelectListItem> Project = new List<SelectListItem>();
            Project.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                Project.Add(new SelectListItem { Text = item.ProjectName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.ProjectId = Project;
            List<SelectListItem> Site = new List<SelectListItem>();
            Site.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                Site.Add(new SelectListItem { Text = item.SiteName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.SiteId = Site;
            List<SelectListItem> SiteAddress = new List<SelectListItem>();
            SiteAddress.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                SiteAddress.Add(new SelectListItem { Text = item.SiteAddress.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.SiteAddressId = SiteAddress;
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
        // POST: PurchaseRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RequestID,RequestDate,CategoryId,SupplierId,SupplierAddressId,SiteDetailsId,ProjectId,SiteId,SiteAddressId,mobileno,NetAmount,grandTotal,discountPercentage,dicountAmount,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseRequest purchaseRequest)
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
        public JsonResult SupplierId(int supplier_NameId)
        {
            if (supplier_NameId > 0)
            {
                var resp = db.supplierMasters.Where(x => x.CategoryId == supplier_NameId).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SupplierAddressId(int supplier_AddressId)
        {
            if (supplier_AddressId > 0)
            {
                var resp = db.supplierMasters.Where(x => x.ID == supplier_AddressId).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SiteId(int site_NameId)
        {
            if (site_NameId > 0)
            {
                var resp = db.siteDetails.Where(x => x.ID == site_NameId).ToList();
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
        public ActionResult Edit([Bind(Include = "ID,RequestID,RequestDate,CategoryId,SupplierId,SupplierAddressId,SiteDetailsId,ProjectId,SiteId,SiteAddressId,mobileno,NetAmount,grandTotal,discountPercentage,dicountAmount,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseRequest purchaseRequest)
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
        [HttpPost]
        public JsonResult savePurchages(PurchaseRequestTable PurchaseRequestTable)
        {
            try
            {
                //PurchaseTable purchaseTable = new PurchaseTable();
                // purchaseTable.ProductId = Guid.Parse(pId);
                //PurchaseRequestTable.ID = Guid.NewGuid();
                PurchaseRequestTable.CreatedDate = DateTime.UtcNow;
                PurchaseRequestTable.UpdatedDate = DateTime.UtcNow;
                PurchaseRequestTable.UpdateBy = Display.Name;
                db.purchaseRequestTables.Add(PurchaseRequestTable);
                db.SaveChanges();
                var id = db.purchaseRequestTables.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                return Json(id.ID, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {


            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult removePurchages(string Id)
        {
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    int pId = int.Parse(Id);

                    var p = db.purchaseRequestTables.Where(x => x.ID == pId && x.isDeleted == false).FirstOrDefault();
                    if (p != null)
                    {
                        int inNo = p.purchaseRequestId;
                        db.purchaseRequestTables.Remove(p);
                        db.SaveChanges();
                        var resp = db.purchaseRequestTables.Where(x => x.purchaseRequestId == inNo).ToList();
                        return Json(resp, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception ex)
            {

                return Json("data", JsonRequestBehavior.AllowGet);
            }
            return Json("data", JsonRequestBehavior.AllowGet);
        }
    }
}
