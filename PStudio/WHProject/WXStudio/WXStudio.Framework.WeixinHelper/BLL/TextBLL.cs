using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXStudio.Framework.Unity.Helper;
using WXStudio.Framework.WeixinHelper.Model;

namespace WXStudio.Framework.WeixinHelper.BLL
{
    public class TextBLL
    {
        private string m_TextContent = "";
        private Dictionary<string, string> m_Model = new Dictionary<string, string>();
        private ContentSender m_ContentSender = new ContentSender();

        public TextBLL(Dictionary<string, string> model)
        {
            string text = XMLHelper.ReadModel("Content", model).Trim();
            m_TextContent = text;
            m_Model = model;
        }

        public void TextResponse()
        {
            if (m_TextContent=="我要抽奖")
            {
                MyLuck();
            }
            else if (m_TextContent.Substring(0,3)=="看楼+")
            {
                SeeBuild();
            }
            else if (m_TextContent.Substring(0, 3) == "买楼+")
            {
                BuyBulid();
            }
            else if (m_TextContent.Substring(0, 3) == "注册+")
            {
                ApplyMember();
            }
            else
            {
                CommonInfo();
            }
        }

        private void ApplyMember()
        {
            SText mT = new SText();
            mT.FromUserName = XMLHelper.ReadModel("ToUserName", m_Model);
            mT.ToUserName = XMLHelper.ReadModel("FromUserName", m_Model);
            mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", m_Model));
            mT.MsgType = "text";
            mT.Content = "注册成功！\n您现在已经是瞰粉的一员，可以参加我们预约看楼、预约买楼、抽奖等活动了。";
            XMLHelper.ResponseToEnd(m_ContentSender.SendText(mT));
        }

        private void MyLuck()
        {
            SText mT = new SText();
            mT.FromUserName = XMLHelper.ReadModel("ToUserName", m_Model);
            mT.ToUserName = XMLHelper.ReadModel("FromUserName", m_Model);
            mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", m_Model));
            mT.MsgType = "text";
            mT.Content = "[测试]\n亲，你的人品差了一点。\n没有收获哦。\n下次加油！\n目前积分为0分。";
            XMLHelper.ResponseToEnd(m_ContentSender.SendText(mT));
        }

        private void SeeBuild()
        {
            SText mT = new SText();
            mT.FromUserName = XMLHelper.ReadModel("ToUserName", m_Model);
            mT.ToUserName = XMLHelper.ReadModel("FromUserName", m_Model);
            mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", m_Model));
            mT.MsgType = "text";
            mT.Content = "[测试]\n报名成功，欢迎您于2014年9月1日光临瞰青楼盘。";
            XMLHelper.ResponseToEnd(m_ContentSender.SendText(mT));
        }

        private void BuyBulid()
        {
            SText mT = new SText();
            mT.FromUserName = XMLHelper.ReadModel("ToUserName", m_Model);
            mT.ToUserName = XMLHelper.ReadModel("FromUserName", m_Model);
            mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", m_Model));
            mT.MsgType = "text";
            mT.Content = "[测试]\n预约成功，稍后会有您的专属销售人员联系，请保持电话畅通。";
            XMLHelper.ResponseToEnd(m_ContentSender.SendText(mT));
        }

        private void CommonInfo()
        {
            SText mT = new SText();
            mT.FromUserName = XMLHelper.ReadModel("ToUserName", m_Model);
            mT.ToUserName = XMLHelper.ReadModel("FromUserName", m_Model);
            mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", m_Model));
            mT.MsgType = "text";
            mT.Content = "[测试]\n感谢你的热情关注，可以让小编知道你的更多心声吗？/偷笑";
            XMLHelper.ResponseToEnd(m_ContentSender.SendText(mT));
        }

            //SText mT = new SText();
            //mT.FromUserName = XMLHelper.ReadModel("ToUserName", model);
            //mT.ToUserName = XMLHelper.ReadModel("FromUserName", model);
            //mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", model));

        //if (text == "?" || text == "？" || text == "帮助")
        //{
        //    mT.Content = mT.Content = XMLHelper.Menu();
        //    mT.MsgType = "text";
        //    XMLHelper.ResponseToEnd(contentSender.SendText(mT));
        //}
        //else
        //{
        //    SNews mN = new SNews();
        //    mN.FromUserName = XMLHelper.ReadModel("ToUserName", model);
        //    mN.ToUserName = XMLHelper.ReadModel("FromUserName", model);
        //    mN.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", model));
        //    mN.MsgType = "news";

        //    //   以下为文章内容，  实际使用时，此处应该是一个跟数据库交互的方法，查询出文章
        //    //文章条数，  文章内容等   都应该由数据库查询出来的数据决定   这里测试，就模拟几条

        //    mN.ArticleCount = 3;
        //    List<ArticlesModel> listNews = new List<ArticlesModel>();
        //    for (int i = 0; i < 3; i++)
        //    {
        //        ArticlesModel ma = new ArticlesModel();
        //        ma.Title = mN.FromUserName + "PStudio工作室欢迎您";
        //        ma.Description = "-描述-" + i.ToString() + "-描述-";
        //        ma.PicUrl = "http://image6.tuku.cn/pic/wallpaper/dongwu/taipingniaogaoqingbizhi/s00" + (i + 1).ToString() + ".jpg";
        //        ma.Url = "http://www.baidu.com/";
        //        listNews.Add(ma);
        //    }
        //    mN.Articles = listNews;
        //    XMLHelper.ResponseToEnd(contentSender.SendNews(mN));
        //}
    }
}
