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
    public class paymentEntriesController : Controller
    {
        private issDB db = new issDB();
        // GET: paymentEntries
        public ActionResult Index()
        {
            return View(db.paymentEntries.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }
        // GET: paymentEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paymentEntry paymentEntry = db.paymentEntries.Find(id);
            if (paymentEntry == null)
            {
                return HttpNotFound();
            }
            return View(paymentEntry);
        }
        // GET: paymentEntries/Create
        public ActionResult Create()
        {

            ViewBag.accountLedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.LedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" && x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "ProjectName");
            ViewBag.siteId = new SelectList(db.siteDetails, "ID", "SiteName");
            //ViewBag.accountGroupId = new SelectList(db.accountLedgerMasters, "ID", "AccountGroupID");
            return View();
            }
        // POST: paymentEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,paymentID,paymenttDate,accountLedgerId,accountLedger,SiteNameId,SiteId,siteDetails,givenBy,collectBy,amount,approvedBy,preparedBy,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate,LedgerId")] paymentEntry paymentEntry)
        {
            try
            {
                int invoiceNo = 1;

                paymentEntry.CreatedDate = DateTime.UtcNow;
                paymentEntry.UpdatedDate = DateTime.UtcNow;

                var payment = db.paymentEntries.Where(x => x.isDeleted == false).ToList();
                if (payment != null && payment.Count > 0)
                {
                    invoiceNo = payment.Max(x => x.paymentID);
                    if (invoiceNo != 0)
                    {
                        invoiceNo = invoiceNo + 1;
                    }
                }
                paymentEntry.paymentID = invoiceNo;
                db.paymentEntries.Add(paymentEntry);

                var groupid = db.accountLedgerMasters.Where(x => x.ID == paymentEntry.accountLedgerId).FirstOrDefault();
                paymentEntry.CreatedDate = DateTime.Now;
                db.paymentEntries.Add(paymentEntry);
                masterTbl masterTbl = new masterTbl();
                masterTbl.entryDate = paymentEntry.paymenttDate;
                masterTbl.payType = groupid.AccountLedger;
                masterTbl.AccountID = (paymentEntry.accountLedgerId).ToString();
                masterTbl.parentGroup = groupid.AccountGroup.ParentGroup;
                masterTbl.GroupID = (groupid.AccountGroupID).ToString();
                masterTbl.remarks = paymentEntry.remarks;
                masterTbl.income = paymentEntry.amount;
                masterTbl.expense = '0';
                if (paymentEntry.siteNameId != 0)
                {
                    var sdetails = db.siteDetails.Where(x => x.ID == paymentEntry.siteNameId).FirstOrDefault();
                    masterTbl.projectName = sdetails.ProjectName;
                }
                masterTbl.siteName = paymentEntry.siteName.SiteName;
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

                ViewBag.accountLedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
                ViewBag.LedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
                ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", paymentEntry.siteNameId);
                ViewBag.siteId = new SelectList(db.siteDetails, "ID", "SiteName", paymentEntry.siteId);
                return View(paymentEntry);
            }
        }
        [HttpPost]
        public JsonResult siteNameId(int site_Name_Id)
        {
            if (site_Name_Id > 0)
            {
                var resp = db.siteDetails.Where(x => x.ID == site_Name_Id).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }
        // GET: paymentEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paymentEntry paymentEntry = db.paymentEntries.Find(id);
            if (paymentEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountLedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.LedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.LedgerId = new SelectList(db.accountLedgerMasters, "ID", "AccountLedger", paymentEntry.LedgerId);
            ViewBag.accountLedgerId = new SelectList(db.accountLedgerMasters, "ID", "AccountLedger", paymentEntry.accountLedgerId);
            ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", paymentEntry.siteNameId);
            ViewBag.siteId = new SelectList(db.siteDetails, "ID", "SiteName", paymentEntry.siteId);
            return View(paymentEntry);
        }
        // POST: paymentEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,paymentID,paymenttDate,accountLedgerId,accountLedger,siteNameId,SiteId,siteDetails,givenBy,collectBy,amount,approvedBy,preparedBy,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate,LedgerId")] paymentEntry paymentEntry)
        {
            try
            {
                paymentEntry.CreatedDate = DateTime.UtcNow;
                paymentEntry.UpdatedDate = DateTime.UtcNow;
                db.Entry(paymentEntry).State = EntityState.Modified;
                paymentEntry.paymentID = paymentEntry.paymentID;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                ViewBag.accountLedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
                ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
                ViewBag.LedgerId = new SelectList(db.accountLedgerMasters, "ID", "AccountLedger", paymentEntry.LedgerId);
                ViewBag.accountLedgerId = new SelectList(db.accountLedgerMasters, "ID", "AccountLedger", paymentEntry.accountLedgerId);
                ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", paymentEntry.siteName.ProjectName);
                ViewBag.siteId = new SelectList(db.siteDetails, "ID", "SiteName", paymentEntry.siteName.SiteName);
                return View(paymentEntry);
            }
        }
        // GET: paymentEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paymentEntry paymentEntry = db.paymentEntries.Find(id);
            if (paymentEntry == null)
            {
                return HttpNotFound();
            }
            return View(paymentEntry);
        }
        // POST: paymentEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            paymentEntry paymentEntry = db.paymentEntries.Find(id);
            db.paymentEntries.Remove(paymentEntry);
            paymentEntry.isDeleted = true;
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