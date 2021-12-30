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
            ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" && x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.projectNameId = new SelectList(db.siteDetails, "ID", "ProjectName");
            ViewBag.siteDetailsId = new SelectList(db.siteDetails, "ID", "SiteName");
            return View();
        }

        // POST: paymentEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,paymentID,paymenttDate,accountLedgerId,accountLedger,siteDetailsId,siteDetails,givenBy,collectBy,amount,approvedBy,preparedBy,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] paymentEntry paymentEntry)
        {
            if (ModelState.IsValid)
            {
                var groupid = db.accountLedgerMasters.Where(x => x.ID == paymentEntry.accountLedgerId).FirstOrDefault();
                //var parentid = db.accountGroupMasters.Where(x => x.ID == paymentEntry.accountGroupId).FirstOrDefault();
               // var data = db.accountLedgerMasters.Where(x => x.ID == paymentEntry.accountLedgerId).FirstOrDefault();
                paymentEntry.CreatedDate = DateTime.Now;
                db.paymentEntries.Add(paymentEntry);
                masterTbl masterTbl = new masterTbl();
                masterTbl.entryDate = paymentEntry.paymenttDate;
                masterTbl.payType = Convert.ToString(paymentEntry.accountLedgerId);
                masterTbl.AccountID = groupid.AccountLedger;
                 //masterTbl.underGroup = paymentEntry.accountGroup.ParentGroup;
                masterTbl.GroupID = Convert.ToString(groupid.AccountGroupID);
                masterTbl.description = paymentEntry.remarks;
                masterTbl.expense = paymentEntry.amount;
                masterTbl.income = '0';
                if (paymentEntry.siteDetailsId != 0)
                {
                    var sdetails = db.siteDetails.Where(x => x.ID == paymentEntry.siteDetailsId).FirstOrDefault();
                    masterTbl.projectName = sdetails.ProjectName;
                }
                masterTbl.siteName = paymentEntry.siteDetails.SiteName;
                masterTbl.type = "Payment";
                masterTbl.financialYear = "2021";
                masterTbl.CreatedDate = DateTime.UtcNow;
                masterTbl.UpdatedDate = DateTime.UtcNow;
                db.masterTbls.Add(masterTbl);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accountLedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.projectNameId = new SelectList(db.siteDetails, "ID", "ProjectName", paymentEntry.siteDetails.ProjectName);
            ViewBag.siteDetailsId = new SelectList(db.siteDetails, "ID", "SiteName", paymentEntry.siteDetails.SiteName);
           // ViewBag.accountGroupId = new SelectList(db.accountLedgerMasters, "ID", "AccountGroupID", paymentEntry.accountGroupId);
            return View(paymentEntry);
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
            ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.projectNameId = new SelectList(db.siteDetails, "ID", "ProjectName", paymentEntry.siteDetails.ProjectName);
            ViewBag.siteDetailsId = new SelectList(db.siteDetails, "ID", "SiteName", paymentEntry.siteDetails.SiteName);
            return View(paymentEntry);
        }

        // POST: paymentEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,paymentID,paymenttDate,accountLedgerId,accountLedger,siteDetailsId,siteDetails,givenBy,collectBy,amount,approvedBy,preparedBy,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] paymentEntry paymentEntry)
        {
            {
                if (ModelState.IsValid)
                {
                    paymentEntry.CreatedDate = DateTime.UtcNow;
                    paymentEntry.UpdatedDate = DateTime.UtcNow;
                    db.Entry(paymentEntry).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.accountLedgerId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
                ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
                ViewBag.projectNameId = new SelectList(db.siteDetails, "ID", "ProjectName", paymentEntry.siteDetails.ProjectName);
                ViewBag.siteDetailsId = new SelectList(db.siteDetails, "ID", "SiteName", paymentEntry.siteDetails.SiteName);
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
