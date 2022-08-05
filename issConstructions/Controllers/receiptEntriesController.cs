using issConstructions.Custom;
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
    public class receiptEntriesController : Controller
    {
        private issDB db = new issDB();
        public object Custom { get; private set; }
        // GET: receiptEntries
        public ActionResult Index()
        {
            return View(db.receiptEntries.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }
        // GET: receiptEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receiptEntry receiptEntry = db.receiptEntries.Find(id);
            if (receiptEntry == null)
            {
                return HttpNotFound();
            }
            return View(receiptEntry);
        }
        // GET: receiptEntries/Create
        public ActionResult Create()
        {


            ViewBag.accountLedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.LedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" && x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "ProjectName");
            ViewBag.siteId = new SelectList(db.siteDetails, "ID", "SiteName");
            //ViewBag.accountGroupId = new SelectList(db.accountLedgerMasters, "ID", "AccountGroupID");
            return View();
        }
        // POST: receiptEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,receiptID,receiptDate,accountLedgerId,accountLedger,SiteNameId,SiteId,siteDetails,approvedBy,preparedBy,givenBy,collectBy,amount,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate,LedgerId")] receiptEntry receiptEntry)
        {
            try
            {
                int invoiceNo = 1;

                receiptEntry.CreatedDate = DateTime.UtcNow;
                receiptEntry.UpdatedDate = DateTime.UtcNow;

                var receipt = db.receiptEntries.Where(x => x.isDeleted == false).ToList();
                if (receipt != null && receipt.Count > 0)
                {
                    invoiceNo = receipt.Max(x => x.receiptID);
                    if (invoiceNo != 0)
                    {
                        invoiceNo = invoiceNo + 1;
                    }
                }
                receiptEntry.receiptID = invoiceNo;
                db.receiptEntries.Add(receiptEntry);

                var groupid = db.accountLedgerMasters.Where(x => x.ID == receiptEntry.accountLedgerId).FirstOrDefault();
                receiptEntry.CreatedDate = DateTime.Now;
                db.receiptEntries.Add(receiptEntry);
                masterTbl masterTbl = new masterTbl();
                masterTbl.entryDate = receiptEntry.receiptDate;
                masterTbl.payType = groupid.AccountLedger;
                masterTbl.AccountID = (receiptEntry.accountLedgerId).ToString();
                masterTbl.parentGroup = groupid.AccountGroup.ParentGroup;
                masterTbl.GroupID = (groupid.AccountGroupID).ToString();
                masterTbl.remarks = receiptEntry.remarks;
                masterTbl.income = receiptEntry.amount;
                masterTbl.expense = '0';
                if (receiptEntry.siteNameId != 0)
                {
                    var sdetails = db.siteDetails.Where(x => x.ID == receiptEntry.siteNameId).FirstOrDefault();
                    masterTbl.projectName = sdetails.ProjectName;
                }
                masterTbl.siteName = receiptEntry.siteName.SiteName;
                masterTbl.type = "Receipt";
                masterTbl.financialYear = "2021";
                masterTbl.CreatedDate = DateTime.UtcNow;
                masterTbl.UpdatedDate = DateTime.UtcNow;
                db.masterTbls.Add(masterTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.LedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
                ViewBag.siteeNameId = new SelectList(db.siteDetails, "ID", "ProjectName", receiptEntry.siteName.ProjectName);
                ViewBag.siteId = new SelectList(db.siteDetails, "ID", "SiteName", receiptEntry.siteName.SiteName);
                return View(receiptEntry);
            }
         }
        public JsonResult siteNameId(int site_Name_Id)
        {
            if (site_Name_Id > 0)
            {
                var resp = db.siteDetails.Where(x => x.ID == site_Name_Id).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }
        // GET: receiptEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receiptEntry receiptEntry = db.receiptEntries.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (receiptEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountLedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.LedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.LedgerId = new SelectList(db.accountLedgerMasters, "ID", "AccountLedger", receiptEntry.LedgerId);
            ViewBag.accountLedgerId = new SelectList(db.accountLedgerMasters, "ID", "AccountLedger", receiptEntry.accountLedgerId);
            ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", receiptEntry.siteNameId);
            ViewBag.siteId = new SelectList(db.siteDetails, "ID", "SiteName", receiptEntry.siteId);
            return View(receiptEntry);
        }
        // POST: receiptEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,receiptID,receiptDate,accountGroupId,accountGroup,accountLedgerId,accountLedger,siteNameId,SiteId,siteDetails,approvedBy,preparedBy,givenBy,collectBy,amount,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate,LedgerId")] receiptEntry receiptEntry)
        {
            try
            {
                receiptEntry.CreatedDate = DateTime.UtcNow;
                receiptEntry.UpdatedDate = DateTime.UtcNow;
                db.Entry(receiptEntry).State = EntityState.Modified;
                receiptEntry.receiptID = receiptEntry.receiptID;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                ViewBag.accountLedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
                ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
                ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", receiptEntry.siteName.ProjectName);
                ViewBag.siteId = new SelectList(db.siteDetails, "ID", "SiteName", receiptEntry.siteName.SiteName);
                return View(receiptEntry);
            }
        }

        // GET: receiptEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receiptEntry receiptEntry = db.receiptEntries.Find(id);
            if (receiptEntry == null)
            {
                return HttpNotFound();
            }
            return View(receiptEntry);
        }
        // POST: receiptEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            receiptEntry receiptEntry = db.receiptEntries.Find(id);
            db.receiptEntries.Remove(receiptEntry);
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
