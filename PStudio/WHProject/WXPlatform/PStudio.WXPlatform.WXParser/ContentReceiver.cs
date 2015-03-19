using PStudio.WXPlatform.Common.Helper;
using PStudio.WXPlatform.WXParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PStudio.WXPlatform.WXParser.BLL;

namespace PStudio.WXPlatform.WXParser
{
    public class ContentReceiver
    {
        ContentSender contentSender = new ContentSender();

        #region 接收普通消息
        //接收到文本消息  处理
        public void DoText(Dictionary<string, string> model)
        {            
            TextBLL textBll = new TextBLL(model);
            textBll.TextResponse();
        }


        //接收到图片消息
        public void DoImg(Dictionary<string, string> model)
        {

        }

        //接收到语音消息
        public void DoVoice(Dictionary<string, string> model)
        {

        }
        //接收到视频消息
        public void DoVideo(Dictionary<string, string> model)
        {

        }


        //接收到地理位置信息
        public void DoLocation(Dictionary<string, string> model)
        {

        }
        //接收到链接消息
        public void DoLink(Dictionary<string, string> Model)
        {

        }
        #endregion
        //----------------------------------------------------------------------------

        //----------------------------------------------------------------------------
        #region 接收事件消息
        //普通关注
        public void DoOn(Dictionary<string, string> model)
        {
            SText mT = new SText();
            mT.Content = XMLHelper.Menu();
            mT.FromUserName = XMLHelper.ReadModel("ToUserName", model);
            mT.ToUserName = XMLHelper.ReadModel("FromUserName", model);
            mT.MsgType = "text";
            mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", model));
            XMLHelper.ResponseToEnd(contentSender.SendText(mT));
        }
        //取消关注
        public void DoUnOn(Dictionary<string, string> model)
        {

        }
        //未关注用户扫描二维码参数
        public void DoOnCode(Dictionary<string, string> model)
        {
            SText mT = new SText();
            mT.Content = XMLHelper.Menu();
            mT.FromUserName = XMLHelper.ReadModel("ToUserName", model);
            mT.ToUserName = XMLHelper.ReadModel("FromUserName", model);
            mT.MsgType = "text";
            mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", model));
            XMLHelper.ResponseToEnd(contentSender.SendText(mT));
        }
        //已经关注的用户扫描二维码参数
        public void DoSubCode(Dictionary<string, string> model)
        {
            SText mT = new SText();
            mT.Content = XMLHelper.Menu();
            mT.FromUserName = XMLHelper.ReadModel("ToUserName", model);
            mT.ToUserName = XMLHelper.ReadModel("FromUserName", model);
            mT.MsgType = "text";
            mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", model));
            XMLHelper.ResponseToEnd(contentSender.SendText(mT));
        }





        //用户提交地理位置
        public void DoSubLocation(Dictionary<string, string> Model)
        {
            throw new NotImplementedException();
        }
        //自定义菜单点击
        public void DoSubClick(Dictionary<string, string> Model)
        {
            SubClickBLL bll = new SubClickBLL(Model);
            bll.ClickResponse();
        }
        //自定义菜单跳转
        public void DoSubView(Dictionary<string, string> Model)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
