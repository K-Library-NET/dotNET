using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WXStudio.EFModel.Entities;
using WXStudio.EFModel.Entities.DataMgt;
using WXStudio.Framework.Unity;

namespace WXStudio.DataMgt.Web.Controllers
{
    [Authorize]
    public class SalesmenController : Controller
    {
        private WXPstudioDbContext db = new WXPstudioDbContext();

        // GET: Salesmen
        public ActionResult Index()
        {
            return View(db.Salesmans.ToList());
        }

        // GET: Salesmen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salesman salesman = db.Salesmans.Find(id);
            if (salesman == null)
            {
                return HttpNotFound();
            }
            return View(salesman);
        }

        // GET: Salesmen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salesmen/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "SalesmanId,WXCode,WXNickname,Name,Phone,Company,Address,Comments,CreateDateTimeStamp,Column1,Column2,Column3,CompanyID")] Salesman salesman)
        {
            if (ModelState.IsValid)
            {
                db.Salesmans.Add(salesman);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salesman);
        }

        // GET: Salesmen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salesman salesman = db.Salesmans.Find(id);
            if (salesman == null)
            {
                return HttpNotFound();
            }
            return View(salesman);
        }

        // POST: Salesmen/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesmanId,WXCode,WXNickname,Name,Phone,Company,Address,Comments,CreateDateTimeStamp,Column1,Column2,Column3,CompanyID")] Salesman salesman)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesman).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salesman);
        }

        // GET: Salesmen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salesman salesman = db.Salesmans.Find(id);
            if (salesman == null)
            {
                return HttpNotFound();
            }
            return View(salesman);
        }

        // POST: Salesmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salesman salesman = db.Salesmans.Find(id);
            db.Salesmans.Remove(salesman);
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

        public void ExportCSV()
        {
            var salesmenTable = this.GetSalesmenDataTable(db.Salesmans.ToList());
            MemoryStream stream = CSVUtility.GetCSV(salesmenTable);

            var filename = "Export" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            var contenttype = "text/csv";
            Response.Clear();
            Response.ContentType = contenttype;
            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(stream.ToArray());
            Response.End();
        }

        public DataTable GetSalesmenDataTable(IEnumerable<Salesman> sources)
        {
            var salesmenTable = new DataTable();
            salesmenTable.Columns.Add(new DataColumn("SalesmanId", Type.GetType("System.String")) { Caption = "销售员ID" });
            salesmenTable.Columns.Add(new DataColumn("WXCode", Type.GetType("System.String")) { Caption = "销售员微信号" });
            salesmenTable.Columns.Add(new DataColumn("WXNickname", Type.GetType("System.String")) { Caption = "昵称" });
            salesmenTable.Columns.Add(new DataColumn("Name", Type.GetType("System.String")) { Caption = "姓名" });
            salesmenTable.Columns.Add(new DataColumn("Phone", Type.GetType("System.String")) { Caption = "电话" });
            salesmenTable.Columns.Add(new DataColumn("Company", Type.GetType("System.String")) { Caption = "公司" });
            salesmenTable.Columns.Add(new DataColumn("Address", Type.GetType("System.String")) { Caption = "地址" });
            salesmenTable.Columns.Add(new DataColumn("Comments", Type.GetType("System.String")) { Caption = "备注" });
            salesmenTable.Columns.Add(new DataColumn("CreateDate", Type.GetType("System.String")) { Caption = "创建时间" }); 

            var rand = new Random();
            if (sources != null)
            {
                foreach (var one in sources)
                {
                    Object[] data = new Object[9]; 
                    data[0] = one.SalesmanId;
                    data[1] = one.WXCode;
                    data[2] = one.WXNickname;
                    data[3] = one.Name;
                    data[4] = one.Phone;
                    data[5] = one.Company;
                    data[6] = one.Address;
                    data[7] = one.Comments;
                    data[8] = one.CreateDate != null ? one.CreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                    //data[8] = one.CompanyID.ToString();//debug

                    salesmenTable.Rows.Add(data);
                }
            }

            return salesmenTable;
        }
    }
}
