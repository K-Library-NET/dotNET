using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PStudio.WXPlatform.WXWeb.Projects.chongqinglianfa.Menu
{
    public partial class SelectMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetPage("https://api.weixin.qq.com/cgi-bin/menu/get?access_token=iSeU3ZZ9cw_Pu2H6dB2IZfu4Rva6yyGgoOcEX9CcWnzkCpo6-ARM5OxGHKC11rxLLzsROukN5UWGeovioWxSag");
            //GetPage("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=iSeU3ZZ9cw_Pu2H6dB2IZfu4Rva6yyGgoOcEX9CcWnzkCpo6-ARM5OxGHKC11rxLLzsROukN5UWGeovioWxSag");
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