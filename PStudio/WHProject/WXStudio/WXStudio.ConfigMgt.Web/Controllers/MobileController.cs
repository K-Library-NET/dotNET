using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MvcExtension;
using WXStudio.ConfigMgt.Web.Weixin;
using WXStudio.EFModel.Entities;
using WXStudio.EFModel.Entities.DataMgt;
using WXStudio.Framework.Unity.Helper;

namespace WXStudio.ConfigMgt.Web.Controllers
{
    public class MobileController : Controller
    {
        private WXPstudioDbContext db = new WXPstudioDbContext();
        private const string Token = "rELca0DecrfA6LeZeWaA07rcG1Ade1YD";
        private const string AppId = "wxbba0283698d73915";
        private const string AppSecret = "24ffeec6cbf2b7211265f4f03a028bfc ";
        private const string m_Url = "http://localhost:53141/Mobile/UserBookInfo";

        //
        // GET: /Mobile/
        public ActionResult Index()
        {
            return View();
        }

        public string Enter(string name, int numTimes = 1)
        {
            return HttpUtility.HtmlEncode("Hello " + name + ", NumTimes is: " + numTimes);

        }

        #region 主页

        [HttpGet]
        public ActionResult SalesIndex()
        {

            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            //转换成Byte数组
            byte[] b = new byte[s.Length];
            //读取流
            s.Read(b, 0, (int)s.Length);
            //转化成utf8编码
            string postStr = Encoding.UTF8.GetString(b);
            TempData["postStr"] = postStr;

            return View();
        }

        #endregion

        #region 销售注册

        [HttpGet]
        public ActionResult SalesJoin()
        {
            //Stream s = System.Web.HttpContext.Current.Request.InputStream;
            ////转换成Byte数组
            //byte[] b = new byte[s.Length];
            ////读取流
            //s.Read(b, 0, (int)s.Length);
            ////转化成utf8编码
            //string postStr = Encoding.UTF8.GetString(b);
            //TempData["OpenID"] = postStr;

            //CreateSalesmenView
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalesJoin([Bind(Include = "SalesmanId,WXCode,WXNickname,Name,Phone,Company,Address,Comments,CreateDate,Column1,Column2,Column3,CompanyID")] Salesman salesman,int orgid=-1)
        {
            var userWeixinInfo = GetWeixinUserInfo();
            if (userWeixinInfo != null)
            {
                salesman.WXCode = userWeixinInfo.openid;
                salesman.WXNickname = userWeixinInfo.nickname;
            }
            else
            {
                salesman.WXCode = "-1";
                salesman.WXNickname = "无";
            }

            if (ModelState.IsValid)
            {

                var sales = from one in db.Salesmans
                            where one.Phone == salesman.Phone
                            select one;

                if (sales.Count() == 0)
                {
                    salesman.CompanyID = orgid;
                    salesman.CreateDate = DateTime.Now;
                    salesman.Column1 = string.Format("http://42.96.198.241/wxconfig/QRCode/{0}.jpg", salesman.Phone);
                    salesman.Column2 = string.Format("http://42.96.198.241/wxconfig/Mobile/UsersSalesViewInfo?id={0}", salesman.SalesmanId);
                    //TempData["mycodeimg"] = string.Format("~/QRCode/{0}.jpg", salesman.Phone);
                    TempData["mycodeimg"] = salesman.Column1;
                    TempData["myurl"] = salesman.Column2;

                    db.Salesmans.Add(salesman);
                    db.SaveChanges();

                    var codeParams = CodeDescriptor.Init(ErrorCorrectionLevel.H, salesman.Column2, QuietZoneModules.Two, 5);
                    codeParams.TryEncode();

                    using (var ms = new MemoryStream())
                    {
                        //string filename = Path.Combine("D:\\IIS_Web\\Config\\QRCode", salesman.Phone + ".jpg");
                        string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QRCode", salesman.Phone + ".jpg");
                        codeParams.Render(ms);
                        System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                        image.Save(filename,System.Drawing.Imaging.ImageFormat.Jpeg);
                        ms.Close();
                        ms.Dispose();
                        image.Dispose();
                    }
                    return RedirectToAction("SalesJoinSuccess");
                }
                else
                {
                    return RedirectToAction("SalesJoinReDone");
                }
            }

            return View(salesman);
        }

        [HttpGet]
        public ActionResult CheckSalesPhoneExists(string phone)
        {
            var sales = from one in db.Salesmans
                        where one.Phone == phone
                        select one;

            return Json(sales.Count() == 0, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SalesJoinSuccess()
        {
            return View();
        }

        public ActionResult SalesJoinReDone()
        {
            return View();
        }

        #endregion

        #region 预约登记

        [HttpGet]
        public ActionResult UsersSalesBookInfo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsersSalesBookInfo([Bind(Include = "SaleBookId,SalesmanId,WXCode,WXNickname,Name,Phone,Comments,ViewConfirm,BuyConfirm,BuyUnit,Column1,Column2,Column3,CompanyId")] SaleBook saleBook, int id = -1)
        {
            var userInfo = GetWeixinUserInfo();
            if (userInfo != null)
            {
                saleBook.WXCode = userInfo.openid;
                saleBook.WXNickname = userInfo.nickname;
            }
            else
            {
                saleBook.WXCode = "-1";
                saleBook.WXNickname = "无";
            }
            if (ModelState.IsValid)
            {
                
                saleBook.SalesmanId = id;
                saleBook.CreateDate = DateTime.Now;
                db.SaleBooks.Add(saleBook);
                db.SaveChanges();
                return RedirectToAction("UserBookSuccess");
            }
            return View(saleBook);
        }

        [HttpGet]
        public ActionResult UsersSalesViewInfo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsersSalesViewInfo([Bind(Include = "SaleBookId,SalesmanId,WXCode,WXNickname,Name,Phone,Comments,ViewConfirm,BuyConfirm,BuyUnit,Column1,Column2,Column3,CompanyId")] SaleBook saleBook, int id = -1)
        {
            var userInfo = GetWeixinUserInfo();
            if (userInfo != null)
            {
                saleBook.WXCode = userInfo.openid;
                saleBook.WXNickname = userInfo.nickname;
            }
            else
            {
                saleBook.WXCode = "-1";
                saleBook.WXNickname = "无";
            }
            if (ModelState.IsValid)
            {
                saleBook.SalesmanId = id;
                saleBook.CreateDate = DateTime.Now;
                db.SaleBooks.Add(saleBook);
                db.SaveChanges();
                return RedirectToAction("UserBookSuccess");
            }
            return View(saleBook);
        }



        public ActionResult UserBookInfo()
        {
            //CreateSalesmenView
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserBookInfo([Bind(Include = "SaleBookId,SalesmanId,WXCode,WXNickname,Name,Phone,Comments,ViewConfirm,BuyConfirm,BuyUnit,Column1,Column2,Column3,CompanyId")] SaleBook saleBook, int id = -1)
        {
            var userInfo = GetWeixinUserInfo();
            if (userInfo != null)
            {
                saleBook.WXCode = userInfo.openid;
                saleBook.WXNickname = userInfo.nickname;
            }
            else
            {
                saleBook.WXCode = "-1";
                saleBook.WXNickname = "非微信";
            }
            if (ModelState.IsValid)
            {
                saleBook.SalesmanId = id;
                saleBook.CreateDate = DateTime.Now;
                db.SaleBooks.Add(saleBook);
                db.SaveChanges();
                return RedirectToAction("UserBookSuccess");
            }

            return View(saleBook);
        }

        public ActionResult UserBookSuccess()
        {
            return View();
        }

        public ActionResult CheckBookPhoneExists(string phone)
        {
            var sales = from one in db.SaleBooks
                        where one.Phone == phone
                        select one;

            return Json(sales.Count() == 0, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 私有方法

        private OAuthUserInfo GetWeixinUserInfo()
        {
            OAuthUserInfo user = null;
            try
            {
                var result = OAuth.GetAccessToken(AppId, AppSecret, "kdkddsd");
                user = OAuth.GetUserInfo(result.access_token, result.openid);
            }
            catch (Exception ex)
            {

            }
            return user;
        }

        #endregion

        private string GetAccessToken(string appid, string appSecret)
        {
            AccessTokenResult tokenResult = new AccessTokenResult();
            Senparc.Weixin.MP.CommonAPIs.AccessTokenHandlerWapper.Do(appid, appSecret, GetAPIInterface);
            Senparc.Weixin.MP.CommonAPIs.AccessTokenContainer container = new Senparc.Weixin.MP.CommonAPIs.AccessTokenContainer();
            return tokenResult.access_token;

            Senparc.Weixin.MP.AdvancedAPIs.OAuthUserInfo userinfo = new Senparc.Weixin.MP.AdvancedAPIs.OAuthUserInfo();
            Senparc.Weixin.MP.AdvancedAPIs.OpenIdResultJson openID = new Senparc.Weixin.MP.AdvancedAPIs.OpenIdResultJson();
            Senparc.Weixin.MP.AdvancedAPIs.OpenIdResultJson_Data openidData = new Senparc.Weixin.MP.AdvancedAPIs.OpenIdResultJson_Data();
            
        }

        private WxJsonResult GetAPIInterface(string accessToken)
        {
            WxJsonResult result=new WxJsonResult();
            return result;
        }

        public string GetPage(string posturl)
        {
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                Response.Write(content);
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }

	}
}