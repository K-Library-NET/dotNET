using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WXStudio.Framework.Unity.Helper;
using WXStudio.Framework.WeixinHelper;

namespace PStudio.WXPlatform.WXWeb.Projects.chongqinglianfa
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitIndex();
        }
        public void InitIndex()
        {
            ContentReceiver receiver = new ContentReceiver();

            Dictionary<string, string> Model = new Dictionary<string, string>();
            string xmlData = string.Empty;
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                ParserPOST(receiver, ref Model, ref xmlData);
            }
            else if (Request.HttpMethod.ToUpper() == "GET")
            {
                //get
                string echostr = Request.QueryString["echostr"];
                echostr = echostr + "（GET请求）";
                //这里直接返回echostr参数接入成功;
                XMLHelper.ResponseToEnd(echostr);
            }
            else
            {
                Response.Status = "403";
                XMLHelper.ResponseToEnd("");
            }
        }

        private void ParserPOST(ContentReceiver receiver, ref Dictionary<string, string> Model, ref string xmlData)
        {
            using (Stream stream = Request.InputStream)
            {
                Byte[] byteData = new Byte[stream.Length];
                stream.Read(byteData, 0, (Int32)stream.Length);
                xmlData = Encoding.UTF8.GetString(byteData);
            }
            if ((xmlData + "").Length > 0)//!string.IsNullOrEmpty(xmlData)
            {
                try
                {
                    Model = XMLHelper.XmlModel(xmlData);
                }
                catch
                {
                    //未能正确处理 给微信服务器回复默认值
                    DefaultResult();
                }
            }
            if (Model.Count > 0)
            {
                string msgType = XMLHelper.ReadModel("MsgType", Model);
                #region 判断消息类型
                switch (msgType)
                {
                    case "text":
                        receiver.DoText(Model);//文本消息
                        break;
                    case "image":
                        receiver.DoImg(Model);//图片
                        break;
                    case "voice": //声音
                        receiver.DoVoice(Model);
                        break;

                    case "video"://视频
                        receiver.DoVideo(Model);
                        break;

                    case "location"://地理位置
                        receiver.DoLocation(Model);
                        break;
                    case "link"://链接
                        receiver.DoLink(Model);
                        break;
                    #region 事件
                    case "event":
                        switch (XMLHelper.ReadModel("Event", Model))
                        {
                            case "subscribe":
                                if (XMLHelper.ReadModel("EventKey", Model).IndexOf("qrscene_") >= 0)
                                {
                                    receiver.DoOnCode(Model);//带参数的二维码扫描关注
                                }
                                else
                                {
                                    receiver.DoOn(Model);//普通关注
                                }
                                break;
                            case "unsubscribe":
                                receiver.DoUnOn(Model);//取消关注
                                break;

                            case "SCAN":
                                receiver.DoSubCode(Model);//已经关注的用户扫描带参数的二维码
                                break;
                            case "LOCATION"://用户上报地理位置
                                receiver.DoSubLocation(Model);
                                break;
                            case "CLICK"://自定义菜单点击
                                receiver.DoSubClick(Model);
                                break;
                            case "VIEW"://自定义菜单跳转事件
                                receiver.DoSubView(Model);
                                break;
                        };
                        break;
                    #endregion
                }
                #endregion
            }
        }
        //返回默认值
        public void DefaultResult()
        {
            XMLHelper.ResponseToEnd("");
        }

        private bool CheckSignature()
        {
            string signature = System.Web.HttpContext.Current.Request.QueryString["signature"];
            string timestamp = System.Web.HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = System.Web.HttpContext.Current.Request.QueryString["nonce"];
            //加密/校验流程：           
            //1. 将token、timestamp、nonce三个参数进行字典序排序     
            string[] ArrTmp = { signature, timestamp, nonce };
            Array.Sort(ArrTmp);//字典排序              
            //2.将三个参数字符串拼接成一个字符串进行sha1加密       
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            //3.开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。    
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}