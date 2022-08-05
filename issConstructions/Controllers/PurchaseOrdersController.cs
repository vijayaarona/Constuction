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
            var purchaseOrders = db.PurchaseOrders.Include(p => p.Category).Include(p => p.Supplier).Include(p => p.SiteName);
            return View(purchaseOrders.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }
        // GET: PurchaseOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseorder = db.PurchaseOrders.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (purchaseorder == null)
            {
                return HttpNotFound();
            }
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
            ViewBag.SiteNameId = Project;
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

            //product No
            int proNo = 0;
            var productNo = db.PurchaseOrders.Where(p => p.ProductNo != null).ToList();

            if (productNo.Count > 0)
            {
                proNo = productNo.Max(x => x.ProductNo);
            }
            else proNo = 1;
            ViewBag.ProductNo = proNo;
            return View(purchaseorder);
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
            ViewBag.SiteNameId = Project;
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
            ViewBag.productId = Product;
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
                if (item.RequestID != 0)
                    Request.Add(new SelectListItem { Text = item.RequestID.ToString(), Value = item.RequestID.ToString() });
            }
            ViewBag.RequestID = Request;

            return View();
        }
        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RequestID,OrderId,ProductNo,OrderDate,CategoryId,SupplierId,SupplierAddressId,SiteNameId,SiteId,SiteAddressId,mobileno,NetAmount,grandTotal,discountPercentage,dicountAmount,isDeleted,CreatedDate,UpdateBy,UpdatedDate,Tax,TotTax,TotAmount")] PurchaseOrder purchaseOrder)
        {
            try
            {
                int invoiceNo = 1;

                purchaseOrder.CreatedDate = DateTime.UtcNow;
                purchaseOrder.UpdatedDate = DateTime.UtcNow;

                var purchase = db.PurchaseOrders.Where(x => x.isDeleted == false).ToList();
                if (purchase != null && purchase.Count > 0)
                {
                    invoiceNo = purchase.Max(x => x.OrderId);
                    if (invoiceNo != 0)
                    {
                        invoiceNo = invoiceNo + 1;
                    }
                }
                purchaseOrder.OrderId = invoiceNo;
                db.PurchaseOrders.Add(purchaseOrder);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", purchaseOrder.CategoryId);
                ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername", purchaseOrder.SupplierId);
                ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address", purchaseOrder.SupplierAddressId);
                ViewBag.SiteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseOrder.SiteNameId);
                ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseOrder.SiteId);
                ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseOrder.SiteAddressId);
                ViewBag.RequestID = new SelectList(db.purchaseRequest, "ID", "ID", purchaseOrder.PurchaseRequest);
                // ViewBag.ProductId = new SelectList(db.purchaseOrderTables, "ID", "ID");
                return View(purchaseOrder);
            }
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
        [HttpPost]
        public JsonResult purchaseReqOrders(int purchaseRequestOrderId)
        {
            if (purchaseRequestOrderId > 0)
            {
                var res = db.purchaseRequest.Where(x => x.RequestID == purchaseRequestOrderId).FirstOrDefault();
                var req = db.purchaseRequestTables.Where(x => x.purchaseRequestId == res.RequestID).ToList();
                
                int maxValue = 0;
                var isnull = db.PurchaseOrders.Where(x => x.OrderId != null).ToList();
                if (isnull.Count == 0)
                {
                    maxValue = 1;
                }
                else
                {
                    maxValue = db.PurchaseOrders.Max(x => x.OrderId);
                    maxValue += 1;

                }
               List<PurchaseRequestTable> list = new List<PurchaseRequestTable>();
                foreach (var item in req)
                {
                    PurchaseOrderTable newItm = new PurchaseOrderTable();
                    PurchaseRequestTable repItem = new PurchaseRequestTable();
                    newItm.Amount = item.Amount;
                    newItm.Description = item.Description;
                    newItm.discountAmount = item.discountAmount;
                    newItm.discountPercent = item.discountPercent;
                    newItm.productId = item.productId;
                    newItm.Rate = item.Rate;
                    newItm.Quantity = item.Quantity;
                    newItm.Tax = item.Tax;
                    newItm.TaxAmount = item.TaxAmount;
                    newItm.TotalAmount = item.TotalAmount;
                    newItm.purchaseRequestId = maxValue;
                    
                   

                    db.purchaseOrderTables.Add(newItm);
                    db.SaveChanges();
                    repItem.Amount = item.Amount;
                    repItem.Description = item.Description;
                    repItem.discountAmount = item.discountAmount;
                    repItem.discountPercent = item.discountPercent;
                    repItem.productId = item.productId;
                    repItem.Rate = item.Rate;
                    repItem.Quantity = item.Quantity;
                    repItem.Tax = item.Tax;
                    repItem.TaxAmount = item.TaxAmount;
                    repItem.TotalAmount = item.TotalAmount;
                    repItem.ProductNo = item.ProductNo;
                    repItem.Product = item.Product;
                    repItem.purchaseRequestId = maxValue;
                    repItem.ID = db.purchaseOrderTables.Max(x => x.ID);
                    list.Add(repItem);
                }
                req = list;
                var result = new { res, req };
                return Json(result, JsonRequestBehavior.AllowGet);

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
            ViewBag.SiteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseOrder.SiteNameId);
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
        public ActionResult Edit([Bind(Include = "ID,RequestID,OrderId,ProductNo,OrderDate,CategoryId,SupplierId,SupplierAddressId,SiteNameId,SiteId,SiteAddressId,mobileno,NetAmount,grandTotal,discountPercentage,dicountAmount,isDeleted,CreatedDate,UpdateBy,UpdatedDate,Tax,TotTax,TotAmount")] PurchaseOrder purchaseOrder)
        {
            try
            {
                purchaseOrder.CreatedDate = DateTime.UtcNow;
                purchaseOrder.UpdatedDate = DateTime.UtcNow;
                db.Entry(purchaseOrder).State = EntityState.Modified;
                purchaseOrder.OrderId = purchaseOrder.OrderId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", purchaseOrder.CategoryId);
                ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername", purchaseOrder.SupplierId);
                ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address", purchaseOrder.SupplierAddressId);
                ViewBag.SiteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseOrder.SiteNameId);
                ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseOrder.SiteId);
                ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseOrder.SiteAddressId);
                return View(purchaseOrder);

            }

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
            List<PurchaseOrderTable> lstPR = db.purchaseOrderTables.Where(x => x.purchaseRequestId == purchaseOrder.OrderId).ToList();
            foreach (var item in lstPR)
            {
                db.purchaseOrderTables.Remove(item);
                db.SaveChanges();
            }


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
        public JsonResult savePurchages(PurchaseOrderTable purchaseOrdertable)
        {
            try
            {

                //PurchaseRequest purchaseRequest = new PurchaseRequest();
                int maxValue = 0;
                var isnull = db.PurchaseOrders.Where(x => x.OrderId != null).ToList();
                if (isnull.Count == 0)
                {
                    maxValue = 1;
                }
                else
                {
                    maxValue = db.PurchaseOrders.Max(x => x.OrderId);
                    maxValue += 1;

                }
                purchaseOrdertable.purchaseRequestId = maxValue;
                purchaseOrdertable.CreatedDate = DateTime.UtcNow;
                purchaseOrdertable.UpdatedDate = DateTime.UtcNow;
                purchaseOrdertable.UpdateBy = Display.Name;

                db.purchaseOrderTables.Add(purchaseOrdertable);
                // db.purchaseRequest.Add(purchaseRequest);
                db.SaveChanges();
                var id = db.purchaseOrderTables.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
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
                if (!string.IsNullOrEmpty(Id) && Id != "undefined")
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
                        var resp = db.purchaseOrderTables.Where(x => x.purchaseRequestId == inNo).ToList();
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

        [HttpPost]
        public JsonResult getListOfPurchaes(int invoice)
        {
            try
            {
                var pr = db.PurchaseOrders.Where(x => x.ID == invoice).FirstOrDefault();
                List<PurchaseOrderTable> purchaseOrderTables = db.purchaseOrderTables.Where(x => x.purchaseRequestId == pr.OrderId).ToList();
                return Json(purchaseOrderTables, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json("data", JsonRequestBehavior.AllowGet);
            }


        }
    }
}

