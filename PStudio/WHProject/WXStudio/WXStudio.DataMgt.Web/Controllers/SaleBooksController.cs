using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WXStudio.DataMgt.Web.Models;
using WXStudio.EFModel.Entities;
using WXStudio.EFModel.Entities.DataMgt;
using WXStudio.Framework.Unity;

namespace WXStudio.DataMgt.Web.Controllers
{
    [Authorize]
    public class SaleBooksController : Controller
    {
        private WXPstudioDbContext db = new WXPstudioDbContext();

        // GET: SaleBooks
        public ActionResult Index()
        {
            //var items = from one in db.SaleBooks
            //            select new SaleBookViewModel(one);
            return View(db.SaleBooks.ToList());
        }

        // GET: SaleBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleBook saleBook = db.SaleBooks.Find(id);
            if (saleBook == null)
            {
                return HttpNotFound();
            }
            return View(saleBook);
        }

        // GET: SaleBooks/ViewConfirm/5
        public ActionResult ViewConfirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleBook saleBook = db.SaleBooks.Find(id);
            if (saleBook == null)
            {
                return HttpNotFound();
            }

            SetViewConfirmBinding(saleBook);

            return View(saleBook);
        }

        private void SetViewConfirmBinding(SaleBook saleBook)
        {
            BindingHelper1 h1 = new BindingHelper1() { Id = ViewConfirmStateEnum.Default, Name = "未确认" };
            BindingHelper1 h2 = new BindingHelper1() { Id = ViewConfirmStateEnum.ViewConfirmed, Name = "现场确认" };

            //var selectList = new SelectList(new SelectListItem[] { enumListitem1, enumListitem2 });
            this.ViewData["ViewConfirmEnum"] = new SelectList(new BindingHelper1[] { h1, h2 }, "Id", "Name"); // selectList;

            var salemans = db.Salesmans.ToList();
            var selectList = new SelectList(salemans, "SalesmanId", "Name", saleBook.SalesmanId);
            this.ViewData["SalesmanIdEnum"] = selectList;
        }

        // GET: SaleBooks/BuyConfirm/5
        public ActionResult BuyConfirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleBook saleBook = db.SaleBooks.Find(id);
            if (saleBook == null)
            {
                return HttpNotFound();
            }

            //BindingHelper2 h1 = new BindingHelper2() { Id = BuyConfirmStateEnum.Default, Name = "未确认" };
            //BindingHelper2 h2 = new BindingHelper2() { Id = BuyConfirmStateEnum.BuyConfirmed, Name = "现场确认" };
            this.SetBindConfirmBinding(saleBook);
            //this.ViewData["BuyConfirmEnum"] = new SelectList(new BindingHelper2[] { h1, h2 }, "Id", "Name"); // selectList;

            if (saleBook == null)
            {
                return HttpNotFound();
            }
            return View(saleBook);
        }

        // GET: SaleBooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaleBooks/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleBookId,SalesmanId,WXCode,WXNickname,Name,Phone,Comments,ViewConfirm,BuyConfirm,BuyUnit,Column1,Column2,Column3,CompanyId")] SaleBook saleBook)
        {
            if (ModelState.IsValid)
            {
                db.SaleBooks.Add(saleBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(saleBook);
        }

        // GET: SaleBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleBook saleBook = db.SaleBooks.Find(id);
            if (saleBook == null)
            {
                return HttpNotFound();
            }
            return View(saleBook);
        }

        // POST: SaleBooks/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleBookId,SalesmanId,WXCode,WXNickname,Name,Phone,Comments,CreateDateTimeStamp,ViewConfirm,BuyConfirm,BuyUnit,Column1,Column2,Column3,CompanyId")] SaleBook saleBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saleBook);
        }

        // GET: SaleBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleBook saleBook = db.SaleBooks.Find(id);
            if (saleBook == null)
            {
                return HttpNotFound();
            }
            return View(saleBook);
        }

        // POST: SaleBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaleBook saleBook = db.SaleBooks.Find(id);
            db.SaleBooks.Remove(saleBook);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult ChangeViewConfirm([Bind(Include = "SaleBookId,SalesmanId,WXCode,WXNickname,Name,Phone,Comments,ViewConfirm,BuyConfirm,BuyUnit,Column1,Column2,Column3,CompanyId")] SaleBook model)
        {
            if (model != null)
            {
                SaleBook saleBook = db.SaleBooks.Find(model.SaleBookId);

                this.SetViewConfirmBinding(saleBook);
                //BindingHelper1 h1 = new BindingHelper1() { Id = ViewConfirmStateEnum.Default, Name = "未确认" };
                //BindingHelper1 h2 = new BindingHelper1() { Id = ViewConfirmStateEnum.ViewConfirmed, Name = "现场确认" };

                ////var selectList = new SelectList(new SelectListItem[] { enumListitem1, enumListitem2 });
                //this.ViewData["ViewConfirmEnum"] = new SelectList(new BindingHelper1[] { h1, h2 }, "Id", "Name"); // selectList;

                //var salemans = from one in db.Salesmans
                //               select new BindingHelper1() { Id = one.SalesmanId, Name = one.Name };

                //this.ViewData["SalesmanIdEnum"] = new SelectList(salemans);

                if (saleBook != null && saleBook.ViewConfirm == ViewConfirmStateEnum.ViewConfirmed)
                {
                    ModelState.AddModelError("ViewConfirm", "已经现场确认的记录，不能够再次确认！");
                    return View("ViewConfirm", saleBook);
                    //return View(saleBook);
                }
                if (saleBook != null && saleBook.ViewConfirm == 0)
                {
                    saleBook.BuyUnit = model.BuyUnit;
                    saleBook.SalesmanId = model.SalesmanId;
                    saleBook.ViewConfirm = model.ViewConfirm;
                }

                var defConfig1 = new WXStudio.EFModel.Entities.Core.CoreConfig();

                defConfig1.Key = "ViewHouseScore";//看楼积分
                var result2 = db.CoreConfigs.Where(item => item.Key == "ViewHouseScore");
                if (result2 != null && result2.Count() > 0)
                {
                    var defConfig2 = result2.First();
                    if (defConfig2 == null)
                        return RedirectToAction("Index");

                    double d = 500;
                    if (defConfig2 != null && !string.IsNullOrEmpty(defConfig2.Value)
                        && double.TryParse(defConfig2.Value, out d))
                    {
                        var salesmanScore = db.SalesmanScores.Create();
                        salesmanScore.CompanyID = model.CompanyId != null ? model.CompanyId.Value : 0;
                        salesmanScore.CreateDateTimeStamp = TimeStampUtility.ToTimeStamp(DateTime.Now);
                        salesmanScore.SaleBookId = model.SaleBookId;
                        salesmanScore.SalesmanId = model.SalesmanId;
                        salesmanScore.ScoreValue = (float)d;
                        salesmanScore.Comments = model.Comments;

                        db.SalesmanScores.Add(salesmanScore);
                    }
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ChangeBuyConfirm([Bind(Include = "SaleBookId,SalesmanId,WXCode,WXNickname,Name,Phone,Comments,ViewConfirm,BuyConfirm,BuyUnit,Column1,Column2,Column3,CompanyId")] SaleBook model)
        {
            if (model != null)
            {
                SaleBook saleBook = db.SaleBooks.Find(model.SaleBookId);

                SetBindConfirmBinding(saleBook);

                if (saleBook != null && saleBook.BuyConfirm == BuyConfirmStateEnum.BuyConfirmed)
                {
                    ModelState.AddModelError("BuyConfirm", "已经现场确认的记录，不能够再次确认！");
                    return View("BuyConfirm", saleBook);
                    //return View(saleBook);
                }
                if (saleBook != null && saleBook.BuyConfirm == 0)
                {
                    saleBook.SalesmanId = model.SalesmanId;
                    saleBook.BuyUnit = model.BuyUnit;
                    saleBook.BuyConfirm = model.BuyConfirm;
                }
                var defConfig1 = new WXStudio.EFModel.Entities.Core.CoreConfig();

                defConfig1.Key = "BuyHouseScore";//买楼积分
                var result2 = db.CoreConfigs.Where(item => item.Key == "BuyHouseScore");
                if (result2 != null && result2.Count() > 0)
                {
                    var defConfig2 = result2.First();
                    if (defConfig2 == null)
                        return RedirectToAction("Index");

                    double d = 500;
                    if (defConfig2 != null && !string.IsNullOrEmpty(defConfig2.Value)
                        && double.TryParse(defConfig2.Value, out d))
                    {
                        var salesmanScore = db.SalesmanScores.Create();
                        salesmanScore.CompanyID = saleBook.CompanyId != null ? saleBook.CompanyId.Value : 0;
                        salesmanScore.CreateDateTimeStamp = TimeStampUtility.ToTimeStamp(DateTime.Now);
                        salesmanScore.SaleBookId = saleBook.SaleBookId;
                        salesmanScore.SalesmanId = saleBook.SalesmanId;
                        salesmanScore.ScoreValue = (float)d;
                        salesmanScore.Comments = saleBook.Comments;

                        db.SalesmanScores.Add(salesmanScore);
                    }
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private void SetBindConfirmBinding(SaleBook saleBook)
        {
            BindingHelper2 h1 = new BindingHelper2() { Id = BuyConfirmStateEnum.Default, Name = "未确认" };
            BindingHelper2 h2 = new BindingHelper2() { Id = BuyConfirmStateEnum.BuyConfirmed, Name = "现场确认" };

            this.ViewData["BuyConfirmEnum"] = new SelectList(new BindingHelper2[] { h1, h2 }, "Id", "Name"); // selectList;

            var salemans = db.Salesmans.ToList();
            var selectList = new SelectList(salemans, "SalesmanId", "Name", saleBook.SalesmanId);
            this.ViewData["SalesmanIdEnum"] = selectList;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ExportCSV()
        {
            try
            {
                var salebookTable = this.GetSaleBooksDataTable(db.SaleBooks.ToList());
                MemoryStream stream = CSVUtility.GetCSV(salebookTable);

                var filename = "Export" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                var contenttype = "text/csv";
                Response.Clear();
                Response.ContentType = contenttype;
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
            catch (Exception e)
            {
                LogHelper.Error("预约管理——导出Excel出错。", e);
            }
        }

        public DataTable GetSaleBooksDataTable(IEnumerable<SaleBook> sources)
        {
            var salebookTable = new DataTable();
            salebookTable.Columns.Add(new DataColumn("WXCode", Type.GetType("System.String")) { Caption = "销售员微信号" });
            salebookTable.Columns.Add(new DataColumn("WXNickname", Type.GetType("System.String")) { Caption = "微信" });
            salebookTable.Columns.Add(new DataColumn("Name", Type.GetType("System.String")) { Caption = "姓名" });
            salebookTable.Columns.Add(new DataColumn("Phone", Type.GetType("System.String")) { Caption = "电话" });
            salebookTable.Columns.Add(new DataColumn("Comments", Type.GetType("System.String")) { Caption = "备注" });
            salebookTable.Columns.Add(new DataColumn("ViewConfirm", Type.GetType("System.String")) { Caption = "看楼确认" });
            salebookTable.Columns.Add(new DataColumn("BuyConfirm", Type.GetType("System.String")) { Caption = "签约确认" });
            salebookTable.Columns.Add(new DataColumn("BuyUnit", Type.GetType("System.String")) { Caption = "意向单元" });
            //salebookTable.Columns.Add(new DataColumn("CompanyId", Type.GetType("System.String")) { Caption = "组织ID" });

            var rand = new Random();
            if (sources != null)
            {
                foreach (var one in sources)
                {
                    Object[] data = new Object[8];
                    data[0] = one.WXCode;
                    data[1] = one.WXNickname;
                    data[2] = one.Name;
                    data[3] = one.Phone;
                    data[4] = one.Comments;
                    data[5] = one.ViewConfirm == ViewConfirmStateEnum.ViewConfirmed ? "现场确认" : "未确认";
                    data[6] = one.BuyConfirm == BuyConfirmStateEnum.BuyConfirmed ? "现场确认" : "未确认";
                    data[7] = one.BuyUnit;
                    //data[8] = one.CompanyId.ToString();//debug

                    salebookTable.Rows.Add(data);
                }
            }

            return salebookTable;
        }
    }

    class BindingHelper1
    {
        public ViewConfirmStateEnum Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }

    class BindingHelper2
    {
        public BuyConfirmStateEnum Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
