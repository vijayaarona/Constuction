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

            //Purchase Type

            List<SelectListItem> type = new List<SelectListItem>();
            type.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            type.Add(new SelectListItem { Text = "Godown", Value = "1" });
            type.Add(new SelectListItem { Text = "Site", Value = "2" });
            ViewBag.type = type;

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
            var productNo = db.purchaseRequest.Where(p => p.ProductNo != null).ToList();

            if (productNo.Count > 0)
            {
                proNo = productNo.Max(x => x.ProductNo);
            }
            else proNo = 1;
            ViewBag.ProductNo = proNo;

            List<SelectListItem> order = new List<SelectListItem>();
            order.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.PurchaseOrders.Where(x => x.Status == null).ToList())
            {
                if (item.OrderId != 0)
                    order.Add(new SelectListItem { Text = item.OrderId.ToString(), Value = item.OrderId.ToString() });
            }
            ViewBag.OrderId = order;
            //ViewBag.type = new SelectList(db.siteDetails.Where(x => x. == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            return View();
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
        public JsonResult purchaseType(int purchase_type)
        {
            if (purchase_type > 0)
            {
                var resp = db.siteDetails.Where(x => x.ID == purchase_type).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }
        // POST: PurchaseEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OrderId,PurchaseOrder,ProductNo,Invoice,purchaseId,purchaseDate,CategoryId,Category,SupplierId,SupplierAddressId,Supplier,SiteNameId,SiteId,SiteAddressId,SiteDetails,mobileno,ReceivedBy,Remarks,ReffBillNo,DeliveryNo,totalDiscount,totalTax,freightCharges,NetAmount,grandTotal,discountPercentage,isDeleted,CreatedDate,UpdateBy,UpdatedDate,Tax,TotTax,TotAmount,PurType")] PurchaseEntry purchaseEntry)
        {
            try
            {
                int invoiceNo = 1;

                purchaseEntry.CreatedDate = DateTime.UtcNow;
                purchaseEntry.UpdatedDate = DateTime.UtcNow;

                var purchase = db.purchaseEntries.Where(x => x.isDeleted == false).ToList();
                if (purchase != null && purchase.Count > 0)
                {
                    invoiceNo = purchase.Max(x => x.purchaseId);
                    if (invoiceNo != 0)
                    {
                        invoiceNo = invoiceNo + 1;
                    }
                }
                purchaseEntry.purchaseId = invoiceNo;
                db.purchaseEntries.Add(purchaseEntry);
                var OrderId = db.purchaseEntries.Where(x => x.OrderId==null).ToList();

                if (OrderId != null && OrderId.Count > 0)
                {

                    var puc = db.PurchaseOrders.Where(x => x.OrderId == purchaseEntry.OrderId).FirstOrDefault();
                    puc.Status = "1";
                    db.Entry(puc).State = EntityState.Modified;
                }
                    db.SaveChanges();
                     return RedirectToAction("Index");

                   
            }
            catch (Exception ex)
            {

                ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", purchaseEntry.CategoryId);
                ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername", purchaseEntry.SupplierId);
                ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address", purchaseEntry.SupplierAddressId);
                ViewBag.SiteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseEntry.SiteNameId);
                ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseEntry.SiteId);
                ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseEntry.SiteAddressId);
                ViewBag.OrderId = new SelectList(db.PurchaseOrders, "ID", "OrderId", purchaseEntry.OrderId);
                return View(purchaseEntry);
            }
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
            ViewBag.SiteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseEntry.SiteNameId);
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseEntry.SiteId);
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseEntry.SiteAddressId);
            ViewBag.OrderId = new SelectList(db.PurchaseOrders, "ID", "SiteAddress", purchaseEntry.OrderId);
            
            return View(purchaseEntry);
        }
        [HttpPost]
        public JsonResult purchaseReqOrders(int purchaseRequestOrderId)
        {
            if (purchaseRequestOrderId > 0)
            {
                var res = db.PurchaseOrders.Where(x => x.OrderId == purchaseRequestOrderId).FirstOrDefault();
                var req = db.purchaseOrderTables.Where(x => x.purchaseRequestId == res.OrderId).ToList();

                int maxValue = 0;
                var isnull = db.purchaseEntries.Where(x => x.purchaseId!= null).ToList();
                if (isnull.Count == 0)
                {
                    maxValue = 1;
                }
                else
                {
                    maxValue = db.purchaseEntries.Max(x => x.purchaseId);
                    maxValue += 1;
                }
                List<PurchaseOrderTable> list = new List<PurchaseOrderTable>();
                var itemTable = db.purchaseEntryTables.Where(x => x.purchaseRequestId == res.OrderId).ToList();
                if (itemTable.Count == 0)
                {
                    foreach (var item in req)
                    {
                        PurchaseEntryTable newItm = new PurchaseEntryTable();
                        PurchaseOrderTable repItem = new PurchaseOrderTable();
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
                        newItm.ProductNo = item.ProductNo;
                        newItm.Product = item.Product;
                        db.purchaseEntryTables.Add(newItm);
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
                        repItem.ID = db.purchaseEntryTables.Max(x => x.ID);
                        list.Add(repItem);
                    }
                }
                else
                {
                    foreach (var item in itemTable)
                    {
                        PurchaseOrderTable repItem = new PurchaseOrderTable();
                        repItem.Amount = item.Amount;
                        repItem.CreatedDate = item.CreatedDate;
                        repItem.Description = item.Description;
                        repItem.discountAmount = item.discountAmount;
                        repItem.discountPercent = item.discountPercent;
                        repItem.ID = item.ID;
                        repItem.Product = item.Product;
                        repItem.productId = item.productId;
                        repItem.ProductNo = item.ProductNo;
                        repItem.purchaseRequestId = item.purchaseRequestId;
                        repItem.Quantity = item.Quantity;
                        repItem.Rate = item.Rate;
                        repItem.Tax = item.Tax;
                        repItem.TaxAmount = item.TaxAmount;
                        repItem.TotalAmount = item.TotalAmount;
                        repItem.UpdateBy = item.UpdateBy;
                        list.Add(repItem);
                    }
                    req = list;
                }

                var result = new { res, req };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }

        // POST: PurchaseEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OrderId,PurchaseOrder,ProductNo,Invoice,purchaseId,purchaseDate,CategoryId,Category,SupplierId,SupplierAddressId,Supplier,SiteNameId,SiteId,SiteAddressId,SiteDetails,mobileno,ReceivedBy,Remarks,ReffBillNo,DeliveryNo,totalDiscount,totalTax,freightCharges,NetAmount,grandTotal,discountPercentage,isDeleted,CreatedDate,UpdateBy,UpdatedDate,Tax,TotTax,TotAmount,PurType")] PurchaseEntry purchaseEntry)
        {
            try
            {
                purchaseEntry.CreatedDate = DateTime.UtcNow;
                purchaseEntry.UpdatedDate = DateTime.UtcNow;
                db.Entry(purchaseEntry).State = EntityState.Modified;
                purchaseEntry.purchaseId  = purchaseEntry.purchaseId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", purchaseEntry.CategoryId);
                ViewBag.SupplierId = new SelectList(db.supplierMasters, "ID", "Suppliername", purchaseEntry.SupplierId);
                ViewBag.SupplierAddressId = new SelectList(db.supplierMasters, "ID", "address", purchaseEntry.SupplierAddressId);
                ViewBag.SiteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", purchaseEntry.SiteNameId);
                ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", purchaseEntry.SiteId);
                ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", purchaseEntry.SiteAddressId);
                return View(purchaseEntry);

            }

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
        [HttpPost]
        public JsonResult savePurchages(PurchaseEntryTable purchaseEntryTable)
        {
            try
            {

                int maxValue = 0;
                var isnull = db.purchaseEntries.Where(x => x.purchaseId != null).ToList();
                if (isnull.Count == 0)
                {
                    maxValue = 1;
                }
                else
                {
                    maxValue = db.purchaseEntries.Max(x => x.purchaseId);
                    maxValue += 1;

                }

                
                purchaseEntryTable.purchaseRequestId = maxValue;
                purchaseEntryTable.CreatedDate = DateTime.UtcNow;
                purchaseEntryTable.UpdatedDate = DateTime.UtcNow;
                purchaseEntryTable.UpdateBy = Display.Name;


                db.purchaseEntryTables.Add(purchaseEntryTable);
                db.SaveChanges();

                var id = db.purchaseEntryTables.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
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
                    var p = db.purchaseEntryTables.Where(x => x.ID == pId).FirstOrDefault();

                    if (p != null)
                    {
                        int inNo = p.productId;
                        db.purchaseEntryTables.Remove(p);
                        db.SaveChanges();
                        var resp = db.purchaseEntryTables.Where(x => x.purchaseRequestId == p.purchaseRequestId).ToList();
                        return Json(resp, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        int inNo = p.productId;
                        var resp = db.purchaseEntryTables.Where(x => x.purchaseRequestId == inNo).ToList();
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
                var pr = db.purchaseEntries.Where(x => x.ID == invoice).FirstOrDefault();
                List<PurchaseEntryTable> purchaseEntryTables = db.purchaseEntryTables.Where(x => x.purchaseRequestId == pr.purchaseId).ToList();
                return Json(purchaseEntryTables, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json("data", JsonRequestBehavior.AllowGet);
            }


        }
    }
}