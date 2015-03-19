using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace WXStudio.Framework.Unity.Helper
{
    public class XMLHelper
    {
        //把接收到的XML转为字典
        public static Dictionary<string, string> XmlModel(string xmlStr)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            Dictionary<string, string> mo = new Dictionary<string, string>();
            var data = doc.DocumentElement.ChildNodes;
            //.SelectNodes("xml");
            for (int i = 0; i < data.Count; i++)
            {
                mo.Add(data.Item(i).LocalName, data.Item(i).InnerText);
            }
            return mo;
        }



        ////从字典中读取指定的值
        public static string ReadModel(string key, Dictionary<string, string> model)
        {
            string str = "";
            model.TryGetValue(key, out str);
            if (str == null)
                str = "";

            return str;
        }
        //输出字符串并结束当前页面进程 MVC必须加return
        public static void ResponseToEnd(string str)
        {
            //HttpContext.Current.Response.Write(str);
            //HttpContext.Current.Response.End();
            return;
        }

        //输出字符串并结束当前页面进程 MVC必须加return
        public static string Menu()
        {
            string Content = "";
            Content += "欢迎关注联发瞰青/微笑\n\n";
            Content += "作为瞰青官微平台，可以帮助你们实现更多自助服务和资讯\n";
            Content += "1、楼盘介绍\n";
            Content += "2、预约看楼\n";
            Content += "3、预约买楼\n";
            Content += "4、查询积分\n";
            Content += "5、抽奖活动\n";
            return Content;
        }
    }
}
