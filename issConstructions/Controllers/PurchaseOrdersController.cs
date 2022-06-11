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
  //  [CustomAuthorize(Roles = "Admin,Manager")]
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
            List<SelectListItem> Request = new List<SelectListItem>();
            Request.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.purchaseRequest.ToList())
            {
                Request.Add(new SelectListItem { Text = item.ID.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.RequestID = Request;

            return View();
        }
        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RequestID,OrderId,ProductNo,OrderDate,CategoryId,SupplierId,SupplierAddressId,ProjectId,SiteId,SiteAddressId,mobileno,NetAmount,grandTotal,discountPercentage,dicountAmount,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseOrder purchaseOrder)
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
           // ViewBag.ProductId = new SelectList(db.purchaseOrderTables, "ID", "ID");
            return View(purchaseOrder);
        }
        //[HttpPost]
        //public JsonResult purchaseReqOrders(int purchaseRequestOrderId)
        //{
        //    if (purchaseRequestOrderId > 0)
        //    {
        //        var resp = db.purchaseRequest.Where(x => x.ID == purchaseRequestOrderId).FirstOrDefault();
        //        return Json(resp, JsonRequestBehavior.AllowGet);
        //    }
        //    else return Json("NoData", JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult TaxId(int tax_Amount)
        //{
        //    if (tax_Amount > 0)
        //    {
        //        var resp = db.productMasters.Where(x => x.ID == tax_Amount).ToList();
        //        return Json(resp, JsonRequestBehavior.AllowGet);
        //    }
        //    else return Json("NoData", JsonRequestBehavior.AllowGet);
        //}
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
        public ActionResult Edit([Bind(Include = "ID,RequestID,OrderId,ProductNo,OrderDate,CategoryId,SupplierId,SupplierAddressId,ProjectId,SiteId,SiteAddressId,mobileno,NetAmount,grandTotal,discountPercentage,dicountAmount,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] PurchaseOrder purchaseOrder)
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
        [HttpPost]
        public JsonResult savePurchages(PurchaseOrderTable purchaseOrder)
        {
            try
            {
                //PurchaseTable purchaseTable = new PurchaseTable();
                // purchaseTable.ProductId = Guid.Parse(pId);
                //PurchaseRequestTable.ID = Guid.NewGuid();
                purchaseOrder.CreatedDate = DateTime.UtcNow;
                purchaseOrder.UpdatedDate = DateTime.UtcNow;
                purchaseOrder.UpdateBy = Display.Name;
                db.purchaseOrderTables.Add(purchaseOrder);
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
                    var p = db.purchaseOrderTables.Where(x => x.ID == pId && x.isDeleted == false).FirstOrDefault();
                    int inNo = p.productId;
                    if (p != null)
                    {

                        db.purchaseOrderTables.Remove(p);
                        db.SaveChanges();
                        var resp = db.purchaseOrderTables.Where(x => x.productId == inNo && x.isDeleted == false).ToList();
                        return Json(resp, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
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

