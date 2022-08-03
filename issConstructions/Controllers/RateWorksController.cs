﻿using System;
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
    public class RateWorksController : Controller
    {
        private issDB db = new issDB();

        // GET: RateWorks
        public ActionResult Index()
        {

            var ratework = db.rateWorks.Include(p => p.SiteName);
            return View(ratework.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.Id));

            // return View(db.rateWorks.ToList());
        }

        // GET: RateWorks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateWork rateWork = db.rateWorks.Where(x => x.isDeleted == false && x.Id == id).FirstOrDefault();
            if (rateWork == null)
            {
                return HttpNotFound();
            }

            return View(rateWork);
        }

        // GET: RateWorks/Create
        public ActionResult Create()
        {
            List<SelectListItem> Project = new List<SelectListItem>();
            Project.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                Project.Add(new SelectListItem { Text = item.ProjectName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.SiteNameId = Project;

            List<SelectListItem> SiteNo = new List<SelectListItem>();
            SiteNo.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                SiteNo.Add(new SelectListItem { Text = item.ID.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.siteNoId = SiteNo;

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

            List<SelectListItem> umoId = new List<SelectListItem>();
            umoId.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.productMasters.ToList())
            {
                umoId.Add(new SelectListItem { Text = item.UOM.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.UmoId = umoId;

            return View();
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

        // POST: RateWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,SiteNameId,siteNoId,siteId,siteAddressId,headName,totalAmount,deduction,netAmount,passedBy,remark,isDeleted,CreatedDate,UpdateBy,UpdatedDate,RateWorkId")] RateWork rateWork)
        {

            try
            {
                int invoiceNo = 1;

                rateWork.CreatedDate = DateTime.UtcNow;
                rateWork.UpdatedDate = DateTime.UtcNow;

                var Rate = db.rateWorks.Where(x => x.isDeleted == false).ToList();
                if (Rate != null && Rate.Count > 0)
                {
                    invoiceNo = Rate.Max(x => x.RateWorkId);
                    if (invoiceNo != 0)
                    {
                        invoiceNo = invoiceNo + 1;
                    }
                }
                rateWork.RateWorkId = invoiceNo;
                db.rateWorks.Add(rateWork);


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.SiteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", rateWork.SiteNameId);
                ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", rateWork.siteId);
                ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", rateWork.siteAddressId);
                ViewBag.SiteNoId = new SelectList(db.siteDetails, "ID", "Id", rateWork.Id);

                return View(rateWork);

            }

        }
            
        // GET: RateWorks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateWork rateWork = db.rateWorks.Find(id);
            if (rateWork == null)
            {
                return HttpNotFound();
            }
            return View(rateWork);
        }

        // POST: RateWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,SiteNameId,siteNoId,siteNameId,siteAddressId,headName,totalAmount,deduction,netAmount,passedBy,remark,isDeleted,CreatedDate,UpdateBy,UpdatedDate,RateWorkId")] RateWork rateWork)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rateWork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rateWork);
        }

        // GET: RateWorks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateWork rateWork = db.rateWorks.Find(id);
            if (rateWork == null)
            {
                return HttpNotFound();
            }
            return View(rateWork);
        }

        // POST: RateWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RateWork rateWork = db.rateWorks.Find(id);
            List<RateWorkTable> lstPR = db.rateWorkTables.Where(x => x.rateId == rateWork.RateWorkId).ToList();
            foreach (var item in lstPR)
            {
                db.rateWorkTables.Remove(item);
                db.SaveChanges();
            }


            db.rateWorks.Remove(rateWork);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult SaveRateWork(RateWorkTable rateWorkTable)
        {
            try
            {
                int maxValue = 0;
                var isnull = db.rateWorks.Where(x => x.RateWorkId != null).ToList();
                if (isnull.Count == 0)
                {
                    maxValue = 1;
                }
                else
                {
                    maxValue = db.rateWorks.Max(x => x.RateWorkId);
                    maxValue += 1;

                }
                rateWorkTable.rateId = maxValue;
                rateWorkTable.CreatedDate = DateTime.UtcNow;
                rateWorkTable.UpdatedDate = DateTime.UtcNow;
                rateWorkTable.UpdateBy = Display.Name;
                db.rateWorkTables.Add(rateWorkTable);
                db.SaveChanges();
                var id = db.rateWorkTables.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                return Json(id.Id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {


            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult removeRateWorks(string Id)
        {
            try
            {

                if (!string.IsNullOrEmpty(Id) && Id != "undefined")
                {
                    int maxId = db.rateWorks.Max(x => x.Id);
                    if (maxId != null && maxId == 0)
                    {
                        maxId = 1;
                    }
                    else maxId += 1;
                    int pId = int.Parse(Id);
                    var p = db.rateWorkTables.Where(x => x.Id == pId && x.isDeleted == false).FirstOrDefault();
                    db.rateWorkTables.Remove(p);
                    db.SaveChanges();
                    var resp = db.rateWorkTables.Where(x => x.rateId == maxId && x.isDeleted == false).ToList();
                    return Json(resp, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {

                return Json("data", JsonRequestBehavior.AllowGet);
            }
            return Json("data", JsonRequestBehavior.AllowGet);
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
        public JsonResult getListOfRateWork(int invoice)
        {
            try
            {
                var pr = db.rateWorks.Where(x => x.Id == invoice).FirstOrDefault();
                List<RateWorkTable> rateWorkTables = db.rateWorkTables.Where(x => x.rateId == pr.RateWorkId).ToList();
                return Json(rateWorkTables, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json("data", JsonRequestBehavior.AllowGet);
            }

        }

    }
    
}

