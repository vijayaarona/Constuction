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
            ViewBag.groupNameID = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" && x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.projectNameId = new SelectList(db.siteDetails, "ID", "ProjectName");
            ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "SiteName");
            return View();
        }

        // POST: receiptEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,receiptID,receiptDate,groupNameID,accountLedgerNameId,approvedBy,preparedBy,projectNameId,siteNameId,givenBy,collectBy,amount,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] receiptEntry receiptEntry)
        {
            if (ModelState.IsValid)
            {
               
                db.receiptEntries.Add(receiptEntry);
               // var company = db.accountGroupMasters.Where(x => x.ParentGroup =Custom.).FirstOrDefault();
                masterTbl masterTbl = new masterTbl();
                masterTbl.entryDate = receiptEntry.receiptDate;
                masterTbl.payType = Convert.ToString(receiptEntry.groupNameID);
                masterTbl.AccountID = Convert.ToString(receiptEntry.accountLedgerNameId);
                masterTbl.GroupID = "q";
                masterTbl.description = receiptEntry.remarks;
                masterTbl.expense = '0';
                masterTbl.income = receiptEntry.amount;
                //masterTbl.underGroup = receiptEntry.accountGroup.ParentGroup;
                masterTbl.type = "R";
                masterTbl.financialYear = "2021";
                //masterTbl.projectName = receiptEntry.SiteDetail.ProjectName;
                //masterTbl.siteName = receiptEntry.SiteDetail.SiteName;
                masterTbl.CreatedDate = receiptEntry.CreatedDate;
                masterTbl.UpdatedDate = receiptEntry.UpdatedDate;
                db.masterTbls.Add(masterTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.groupNameID = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.projectNameId = new SelectList(db.siteDetails, "ID", "ProjectName", receiptEntry.projectNameId);
            ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "SiteName", receiptEntry.siteNameId);
            return View(receiptEntry);
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
            ViewBag.groupNameID = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.projectNameId = new SelectList(db.siteDetails, "ID", "ProjectName", receiptEntry.projectNameId);
            ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "SiteName", receiptEntry.siteNameId);

            return View(receiptEntry);
        }

        // POST: receiptEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,receiptID,receiptDate,groupNameID,accountLedgerNameId,approvedBy,preparedBy,projectNameId,siteNameId,givenBy,collectBy,amount,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] receiptEntry receiptEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receiptEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.groupNameID = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.projectNameId = new SelectList(db.siteDetails, "ID", "ProjectName", receiptEntry.projectNameId);
            ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "SiteName", receiptEntry.siteNameId);
            return View(receiptEntry);
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
