using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WXStudio.EFModel.Entities;
using WXStudio.EFModel.Entities.Core;

namespace WXStudio.DataMgt.Web.Controllers
{
    [Authorize]
    public class CoreConfigsController : Controller
    {
        private WXPstudioDbContext db = new WXPstudioDbContext();

        // GET: CoreConfigs
        public ActionResult Index()
        {
            return View(db.CoreConfigs.ToList());
        }

        // GET: CoreConfigs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoreConfig coreConfig = db.CoreConfigs.Find(id);
            if (coreConfig == null)
            {
                return HttpNotFound();
            }
            return View(coreConfig);
        }

        // GET: CoreConfigs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoreConfigs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CoreConfigId,Key,Value,Comments")] CoreConfig coreConfig)
        {
            if (ModelState.IsValid)
            {
                db.CoreConfigs.Add(coreConfig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coreConfig);
        }

        // GET: CoreConfigs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoreConfig coreConfig = db.CoreConfigs.Find(id);
            if (coreConfig == null)
            {
                return HttpNotFound();
            }
            return View(coreConfig);
        }

        // POST: CoreConfigs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CoreConfigId,Key,Value,Comments")] CoreConfig coreConfig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coreConfig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coreConfig);
        }

        // GET: CoreConfigs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoreConfig coreConfig = db.CoreConfigs.Find(id);
            if (coreConfig == null)
            {
                return HttpNotFound();
            }
            return View(coreConfig);
        }

        // POST: CoreConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CoreConfig coreConfig = db.CoreConfigs.Find(id);
            db.CoreConfigs.Remove(coreConfig);
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
