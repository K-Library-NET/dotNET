using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WXStudio.DataMgt.Web.Models;
using WXStudio.EFModel.Entities;
using WXStudio.EFModel.Entities.DataMgt;

namespace WXStudio.DataMgt.Web.Controllers
{
    [Authorize]
    public class SalesmanScoresController : Controller
    {
        private WXPstudioDbContext db = new WXPstudioDbContext();

        // GET: SalesmanScores
        public ActionResult Index()
        {
            try
            {
                IEnumerable<SalesmanScoreViewModel> vms = SalesmanScoreViewModel.ToList(db);
                return View(vms);
            }
            catch (Exception e)
            {
                LogHelper.Error("销售积分Index出错。", e);
                return View(new List<SalesmanScoreViewModel>());
            }
        }

        // GET: SalesmanScores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesmanScore salesmanScore = db.SalesmanScores.Find(id);
            if (salesmanScore == null)
            {
                return HttpNotFound();
            }
            return View(salesmanScore);
        }

        // GET: SalesmanScores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesmanScores/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesmanScoreId,SaleBookId,SalesmanId,ScoreValue,Comments,Column1,Column2,Column3,CompanyID")] SalesmanScore salesmanScore)
        {
            if (ModelState.IsValid)
            {
                db.SalesmanScores.Add(salesmanScore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salesmanScore);
        }

        // GET: SalesmanScores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesmanScore salesmanScore = db.SalesmanScores.Find(id);
            if (salesmanScore == null)
            {
                return HttpNotFound();
            }
            return View(salesmanScore);
        }

        // POST: SalesmanScores/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesmanScoreId,SaleBookId,SalesmanId,ScoreValue,Comments,Column1,Column2,Column3,CompanyID")] SalesmanScore salesmanScore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesmanScore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salesmanScore);
        }

        // GET: SalesmanScores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesmanScore salesmanScore = db.SalesmanScores.Find(id);
            if (salesmanScore == null)
            {
                return HttpNotFound();
            }
            return View(salesmanScore);
        }

        // POST: SalesmanScores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesmanScore salesmanScore = db.SalesmanScores.Find(id);
            db.SalesmanScores.Remove(salesmanScore);
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
