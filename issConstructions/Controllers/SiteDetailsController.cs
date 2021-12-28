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
    [CustomAuthorize(Roles = "Admin,Manager")]
    public class SiteDetailsController : Controller
    {
        private issDB db = new issDB();

        // GET: SiteDetails
        public ActionResult Index()
        {
            return View(db.siteDetails.Where (x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }

        // GET: SiteDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteDetails siteDetails = db.siteDetails.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (siteDetails == null)
            {
                return HttpNotFound();
            }
            return View(siteDetails);
        }

        // GET: SiteDetails/Create
        public ActionResult Create()
        {
            ViewBag.ProjectName = "";
            return View();
        }

        // POST: SiteDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,isDeleted,CreatedDate,UpdateBy,UpdatedDate,ProjectDate,ProjectName,SiteName,SiteAddress,ClientName,ClientAddress,MobileNo1,MobileNo2,Email1,Email2,DateOfStart,ReffBy,ReffContactNo,Branch,NatureOfWork,WorkStatus,ClientType,SupervisorName,Length,Breath,Sqrft,BuildupArea,FSI,NoOfFloor,CostPerSqrft,TotalCost,AdditionalCost,NetCost,NoOfColoumn,MeasurmentOfBelt,payment1,payment2,payment3,payment4,payment5,payment1Date,payment2Date,payment3Date,payment4Date,payment5Date,TypeOfWork,WorkDescription,Field,Department,TenderName,TenderDate,TenderEMDCost,WorkAllocated,AgreementTime,SecurityDeposit,AdditionalSD,BusinessFund,Reduction,TenderDD,BankName,DDNo,DDDate,RefundDate")] SiteDetails siteDetails)
        {
            if (ModelState.IsValid)
            {
                //var duplicate = db.siteDetails.Where(x => x.ProjectName == siteDetails.SiteName).FirstOrDefault();
                var duplicate = db.siteDetails.Where(x => x.ProjectName == siteDetails.SiteName).FirstOrDefault();

                if (duplicate != null)
                {
                    siteDetails.CreatedDate = DateTime.UtcNow;
                    siteDetails.UpdatedDate = DateTime.UtcNow;
                    db.siteDetails.Add(siteDetails);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ProjectName = "Site alredy exists....!";
                }
            }

            return View(siteDetails);
        }

        // GET: SiteDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteDetails siteDetails = db.siteDetails.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (siteDetails == null)
            {
                return HttpNotFound();
            }
            return View(siteDetails);
        }

        // POST: SiteDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,isDeleted,CreatedDate,UpdateBy,UpdatedDate,ProjectDate,ProjectName,SiteName,SiteAddress,ClientName,ClientAddress,MobileNo1,MobileNo2,Email1,Email2,DateOfStart,ReffBy,ReffContactNo,Branch,NatureOfWork,WorkStatus,ClientType,SupervisorName,Length,Breath,Sqrft,BuildupArea,FSI,NoOfFloor,CostPerSqrft,TotalCost,AdditionalCost,NetCost,NoOfColoumn,MeasurmentOfBelt,payment1,payment2,payment3,payment4,payment5,payment1Date,payment2Date,payment3Date,payment4Date,payment5Date,TypeOfWork,WorkDescription,Field,Department,TenderName,TenderDate,TenderEMDCost,WorkAllocated,AgreementTime,SecurityDeposit,AdditionalSD,BusinessFund,Reduction,TenderDD,BankName,DDNo,DDDate,RefundDate")] SiteDetails siteDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siteDetails).State = EntityState.Modified;
                siteDetails.CreatedDate = DateTime.UtcNow;
                siteDetails.UpdatedDate = DateTime.UtcNow;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siteDetails);
        }

        // GET: SiteDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteDetails siteDetails = db.siteDetails.Find(id);
            if (siteDetails == null)
            {
                return HttpNotFound();
            }
            return View(siteDetails);
        }

        // POST: SiteDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SiteDetails siteDetails = db.siteDetails.Find(id);
            siteDetails.isDeleted = true;
            db.siteDetails.Remove(siteDetails);
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
