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
    public class ExtraWorksController : Controller
    {
        private issDB db = new issDB();

        // GET: ExtraWorks
        public ActionResult Index()
        {
            return View(db.extraWorks.ToList());
        }

        // GET: ExtraWorks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraWork extraWork = db.extraWorks.Find(id);
            if (extraWork == null)
            {
                return HttpNotFound();
            }
            return View(extraWork);
        }

        // GET: ExtraWorks/Create
        public ActionResult Create()
        {
            List<SelectListItem> Project = new List<SelectListItem>();
            Project.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                Project.Add(new SelectListItem { Text = item.ProjectName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.projectId = Project;

            List<SelectListItem> SiteNo = new List<SelectListItem>();
            SiteNo.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                SiteNo.Add(new SelectListItem { Text = item.ID.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.siteNo = SiteNo;

            List<SelectListItem> Site = new List<SelectListItem>();
            Site.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                Site.Add(new SelectListItem { Text = item.SiteName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.SiteNameId = Site;
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

        // POST: ExtraWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,date,projectId,siteNo,siteName,siteAddress,headName,totalAmt,passBy,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] ExtraWork extraWork)
        {
            if (ModelState.IsValid)
            {
                db.extraWorks.Add(extraWork);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.projectId = new SelectList(db.siteDetails, "ID", "ProjectName", extraWork.projectId);
            ViewBag.SiteNameId = new SelectList(db.siteDetails, "ID", "SiteName", extraWork.siteName);
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", extraWork.siteAddress);
            ViewBag.SiteNo = new SelectList(db.siteDetails, "ID", "Id", extraWork.Id);

            return View(extraWork);
        }

        // GET: ExtraWorks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraWork extraWork = db.extraWorks.Find(id);
            if (extraWork == null)
            {
                return HttpNotFound();
            }
            return View(extraWork);
        }

        // POST: ExtraWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,date,projectId,siteNo,siteName,siteAddress,headName,totalAmt,passBy,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] ExtraWork extraWork)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extraWork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(extraWork);
        }

        // GET: ExtraWorks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraWork extraWork = db.extraWorks.Find(id);
            if (extraWork == null)
            {
                return HttpNotFound();
            }
            return View(extraWork);
        }

        // POST: ExtraWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExtraWork extraWork = db.extraWorks.Find(id);
            db.extraWorks.Remove(extraWork);
            db.SaveChanges();
            return RedirectToAction("Index");
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
        public JsonResult SaveExtraWork(ExtraWorkTable extraWorkTable)
        {
            try
            {


                int maxValue = 0;
                var isnull = db.purchaseRequest.Where(x => x.ID != null).ToList();
                if (isnull.Count > 0)
                {
                    maxValue = 1;
                }
                else
                {
                    maxValue = db.purchaseRequest.Max(x => x.ID);
                    maxValue += 1;

                }   
                extraWorkTable.extraWorkId = maxValue;
                extraWorkTable.CreatedDate = DateTime.UtcNow;
                extraWorkTable.UpdatedDate = DateTime.UtcNow;
                extraWorkTable.UpdateBy = Display.Name;

                db.extraWorkTables.Add(extraWorkTable);
                db.SaveChanges();
                var id = db.rateWorkTables.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                return Json(id.Id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {


            }
            return Json("", JsonRequestBehavior.AllowGet);
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
        public JsonResult getListOfExtraWork(int ID)
        {
            try
            {

                List<ExtraWorkTable>  extraWorkTables= db.extraWorkTables.Where(x => x.extraWorkId == ID).ToList();
                return Json(extraWorkTables, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json("data", JsonRequestBehavior.AllowGet);
            }

        }
    }
}
