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
    public class ToolsTransfersController : Controller
    {
        private issDB db = new issDB();

        // GET: ToolsTransfers
        public ActionResult Index()
        {
            var toolstransfer  = db.toolsTransfers.Include(p => p.Tools).Include(p => p.Type);
            return View(toolstransfer.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.Id));

        }

        // GET: ToolsTransfers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToolsTransfer toolsTransfer = db.toolsTransfers.Find(id);
            if (toolsTransfer == null)
            {
                return HttpNotFound();
            }
            return View(toolsTransfer);
        }

        // GET: ToolsTransfers/Create
        public ActionResult Create()
        {
          
            List<SelectListItem> Type = new List<SelectListItem>();
            Type.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.tblTypes.ToList())
            {
                Type.Add(new SelectListItem { Text = item.TypeName.ToString(), Value = item.Id.ToString() });
            }
            ViewBag.TypeId = Type;

            List<SelectListItem> Tools = new List<SelectListItem>();
            Tools.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.toolsMasters.ToList())
            {
                Tools.Add(new SelectListItem { Text = item.ToolsName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.ToolsId = Tools;
            
            List<ddlLocations> ddl = new List<ddlLocations>();
            ddlLocations ddlLocations = new ddlLocations();
            ddlLocations.text = "----Please Select----";
            ddl.Add(ddlLocations);
            ViewBag.location = ddl;
            return View();
        }

        // POST: ToolsTransfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TDate,TypeId,ToolsId,qty,AuthPerson,isDeleted,CreatedDate,UpdatedBy,UpdatedDate,GodownId,SNameId,GNameId,SiteNameId")] ToolsTransfer toolsTransfer)
        {
            try
            {
                int invoiceNo = 1;

                toolsTransfer.CreatedDate = DateTime.UtcNow;
                toolsTransfer.UpdatedDate = DateTime.UtcNow;

                var Tooltrans = db.toolsTransfers.Where(x => x.isDeleted == false).ToList();
                if (Tooltrans != null && Tooltrans.Count > 0)
                {
                    invoiceNo = Tooltrans.Max(x => x.Id);
                    if (invoiceNo != 0)
                    {
                        invoiceNo = invoiceNo + 1;
                    }
                }
                toolsTransfer.Id  = invoiceNo;
                db.toolsTransfers.Add(toolsTransfer);


                db.SaveChanges();
                return RedirectToAction("Index", toolsTransfer.Id);
            }

            catch (Exception ex)
            {


                ViewBag.TypeId = new SelectList(db.tblTypes, "Id", "TypeName",toolsTransfer.TypeId);
                ViewBag.ToolsId = new SelectList(db.toolsMasters, "ID", "ToolsName", toolsTransfer.ToolsId);
                
                return View(toolsTransfer);

            }

        }
    

        // GET: ToolsTransfers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToolsTransfer toolsTransfer = db.toolsTransfers.Find(id);
            if (toolsTransfer == null)
            {
                return HttpNotFound();
            }
            return View(toolsTransfer);
        }

        // POST: ToolsTransfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TDate,TypeId,ToolsId,qty,AuthPerson,isDeleted,CreatedDate,UpdateBy,UpdatedDate,GodownId,SNameId")] ToolsTransfer toolsTransfer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toolsTransfer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toolsTransfer);
        }

        // GET: ToolsTransfers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToolsTransfer toolsTransfer = db.toolsTransfers.Find(id);
            if (toolsTransfer == null)
            {
                return HttpNotFound();
            }
            return View(toolsTransfer);
        }



        // POST: ToolsTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToolsTransfer toolsTransfer = db.toolsTransfers.Find(id);
            db.toolsTransfers.Remove(toolsTransfer);
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
        public JsonResult ddl_change(int a)
        {
            List<ddlLocations> lstGodown = new List<ddlLocations>();
            List<ddlLocations> lstSite = new List<ddlLocations>();
            var godwn = db.godowns.ToList();
            var site = db.siteDetails.ToList();
            if (a == 3)
            {
                foreach (var item in godwn)
                {
                    ddlLocations newItem = new ddlLocations();
                    newItem.value = item.Id;
                    newItem.text = item.godownName;
                    lstGodown.Add(newItem);
                }
                foreach (var item in site)
                {
                    ddlLocations newItem = new ddlLocations();
                    newItem.value = item.ID;
                    newItem.text = item.SiteName;
                    lstSite.Add(newItem);
                }

            }
            else if (a == 4)
            {
                foreach (var item in site)
                {
                    ddlLocations newItem = new ddlLocations();
                    newItem.value = item.ID;
                    newItem.text = item.SiteName;
                    lstGodown.Add(newItem);
                }
                foreach (var item in site)
                {
                    ddlLocations newItem = new ddlLocations();
                    newItem.value = item.ID;
                    newItem.text = item.SiteName;
                    lstSite.Add(newItem);
                }
            }
            else if (a == 7)
            {

                foreach (var item in site)
                {
                    ddlLocations newItem = new ddlLocations();
                    newItem.value = item.ID;
                    newItem.text = item.SiteName;
                    lstGodown.Add(newItem);
                }
                foreach (var item in godwn)
                {
                    ddlLocations newItem = new ddlLocations();
                    newItem.value = item.Id;
                    newItem.text = item.godownName;
                    lstSite.Add(newItem);
                }

            }
            var result = new { lstGodown, lstSite };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }

    public class ddlLocations
    {
        public int value { get; set; }
        public string text { get; set; }
    }

}

