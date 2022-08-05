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

            var extrawork = db.extraWorks.Include(p => p.SiteName);
            return View(extrawork.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.Id));

        }

        // GET: ExtraWorks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraWork extraWork = db.extraWorks.Where(x => x.isDeleted == false && x.Id == id).FirstOrDefault();
            if (extraWork == null)
            {
                return HttpNotFound();
            }
            return View(extraWork);
        }

        // GET: ExtraWorks/Create
        public ActionResult Create()
        {
            List<SelectListItem> SiteName = new List<SelectListItem>();
            SiteName.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                SiteName.Add(new SelectListItem { Text = item.ProjectName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.SiteNameId = SiteName;

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
            ViewBag.siteId = Site;
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


        // POST: ExtraWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,date,SiteNameId,siteNo,siteId,siteAddressId,headName,totalAmt,passBy,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate,ExtraWorkId")] ExtraWork extraWork)
        {
            try
            {
                int invoiceNo = 1;

                extraWork.CreatedDate = DateTime.UtcNow;
                extraWork.UpdatedDate = DateTime.UtcNow;

                var Extra = db.extraWorks.Where(x => x.isDeleted == false).ToList();
                if (Extra != null && Extra.Count > 0)
                {
                    invoiceNo = Extra.Max(x => x.ExtraWorkId);
                    if (invoiceNo != 0)
                    {
                        invoiceNo = invoiceNo + 1;
                    }
                }
                extraWork.ExtraWorkId = invoiceNo;
                db.extraWorks.Add(extraWork);


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.SiteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", extraWork.SiteNameId);
                ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", extraWork.siteId);
                ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", extraWork.siteAddressId);
                ViewBag.SiteNoId = new SelectList(db.siteDetails, "ID", "Id", extraWork.siteNoId);

                return View(extraWork);

            }

        }

        // GET: ExtraWorks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraWork extraWork = db.extraWorks.Where(x => x.isDeleted == false && x.Id == id).FirstOrDefault();
            if (extraWork == null)
            {
                return HttpNotFound();
            }
            ViewBag.SiteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", extraWork.SiteNameId);
            ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", extraWork.siteId);
            ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", extraWork.siteAddressId);
            ViewBag.SiteNoId = new SelectList(db.siteDetails, "ID", "Id", extraWork.siteNoId);
            return View(extraWork);
        }

        // POST: ExtraWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,date,SiteNameId,siteNoId,siteId,siteAddressId,headName,totalAmt,passBy,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate,ExtraWorkId")] ExtraWork extraWork)
        {
           
            try
            {
                extraWork.CreatedDate = DateTime.UtcNow;
                extraWork.UpdatedDate = DateTime.UtcNow;
                db.Entry(extraWork).State = EntityState.Modified;
                extraWork.ExtraWorkId = extraWork.ExtraWorkId ;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.SiteNameId = new SelectList(db.siteDetails, "ID", "ProjectName", extraWork.SiteNameId);
                ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName", extraWork.siteId);
                ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", extraWork.siteAddressId);
                ViewBag.SiteNoId = new SelectList(db.siteDetails, "ID", "Id", extraWork.siteNoId);
                return View(extraWork);

            }
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
            List<ExtraWorkTable> lstPR = db.extraWorkTables.Where(x => x.extraWorkId == extraWork.ExtraWorkId).ToList();
            foreach (var item in lstPR)
            {
                db.extraWorkTables.Remove(item);
                db.SaveChanges();
            }


            db.extraWorks.Remove(extraWork);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult SaveExtraWork(ExtraWorkTable extraWorkTable)
        {
            try
            {

                int maxValue = 0;
                var isnull = db.extraWorks.Where(x => x.ExtraWorkId != null).ToList();
                if (isnull.Count == 0)
                {
                    maxValue = 1;
                }
                else
                {
                    maxValue = db.extraWorks.Max(x => x.ExtraWorkId);
                    maxValue += 1;

                }
                extraWorkTable.extraWorkId = maxValue;
                extraWorkTable.CreatedDate = DateTime.UtcNow;
                extraWorkTable.UpdatedDate = DateTime.UtcNow;
                extraWorkTable.UpdateBy = Display.Name;
                db.extraWorkTables.Add(extraWorkTable);
                db.SaveChanges();
                var id = db.extraWorkTables.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
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
        public JsonResult getListOfExtraWork(int invoice)
        {
            try
            {
                var pr = db.extraWorks.Where(x => x.Id == invoice).FirstOrDefault();
                List<ExtraWorkTable> extraWorkTables = db.extraWorkTables.Where(x => x.extraWorkId == pr.ExtraWorkId).ToList();
                return Json(extraWorkTables, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json("data", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult removeExtraWorks(string Id)
        {
            try
            {

                if (!string.IsNullOrEmpty(Id) && Id != "undefined")
                {
                    int maxId = db.extraWorks.Max(x => x.Id);
                    if (maxId != null && maxId == 0)
                    {
                        maxId = 1;
                    }
                    else maxId += 1;
                    int pId = int.Parse(Id);
                    var p = db.extraWorkTables.Where(x => x.Id == pId && x.isDeleted == false).FirstOrDefault();
                    db.extraWorkTables.Remove(p);
                    db.SaveChanges();
                    var resp = db.extraWorkTables.Where(x => x.extraWorkId == maxId && x.isDeleted == false).ToList();
                    return Json(resp, JsonRequestBehavior.AllowGet);

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
