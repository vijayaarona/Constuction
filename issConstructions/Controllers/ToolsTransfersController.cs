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
    public class ToolsTransfersController : Controller
    {
        private issDB db = new issDB();

        // GET: ToolsTransfers
        public ActionResult Index()
        {

            return View(db.toolsTransfers.ToList());
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

            ViewBag.ToolsId = new SelectList(db.toolsMasters, "ID", "ToolsName");
            
            ViewBag.ToolsName = "";
            return View();
        }

       

        // POST: ToolsTransfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TDate,Type,FLocation,TLocation,ToolsId,qty,AuthPerson,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] ToolsTransfer toolsTransfer)
        {
            if (ModelState.IsValid)
            {
                db.toolsTransfers.Add(toolsTransfer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toolsTransfer);
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
        public ActionResult Edit([Bind(Include = "Id,TDate,Type,FLocation,TLocation,ToolsId,qty,AuthPerson,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] ToolsTransfer toolsTransfer)
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

        //[HttpPost]
        //public JsonResult FLocId(int From_Id)
        //{
        //    if (From_Id > 0)
        //    {
        //        //var resp = db.siteDetails.Where(x => x.ID == From_Id).ToList();
        //        //return Json(resp, JsonRequestBehavior.AllowGet);
        //    }
        //    else return Json(" ", JsonRequestBehavior.AllowGet);
        //}
    }
}
