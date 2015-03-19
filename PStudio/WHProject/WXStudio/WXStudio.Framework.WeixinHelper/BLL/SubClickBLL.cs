using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXStudio.Framework.Unity.Helper;
using WXStudio.Framework.WeixinHelper.Model;

namespace WXStudio.Framework.WeixinHelper.BLL
{
    public class SubClickBLL
    {
        private Dictionary<string, string> m_Model = new Dictionary<string, string>();
        private ContentSender m_ContentSender = new ContentSender();
        private string m_EventKey = "";

        public SubClickBLL(Dictionary<string, string> model)
        {
            m_Model = model;
            m_EventKey = XMLHelper.ReadModel("EventKey", model);
            //subClick
        }

        public void ClickResponse()
        {
            switch (m_EventKey)
            {
                case "V1001_Buy":
                    SeeBuild();
                    break;

                case "V1002_See":
                    BuyBuild();
                    break;

                case "V1003_Cent":
                    MyCent();                    
                    break;

                case "V1004_Get":
                    MyLuck();
                    break;

                case "V1005_Apply":
                    ApplyOrg();
                    break;

                default:
                    break;
            }
        }

        private void SeeBuild()
        {
            SText mT = new SText();
            mT.FromUserName = XMLHelper.ReadModel("ToUserName", m_Model);
            mT.ToUserName = XMLHelper.ReadModel("FromUserName", m_Model);
            mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", m_Model));
            mT.MsgType = "text";
            mT.Content = "直接回复“看楼+销售人员手机号码+日期（如20140901）”格式进行自助预约看楼，预约收到成功短信后，凭短信亲临现场并确认，可获得相应积分并进行抽奖";
            XMLHelper.ResponseToEnd(m_ContentSender.SendText(mT));
        }

        private void BuyBuild()
        {
            SText mT = new SText();
            mT.FromUserName = XMLHelper.ReadModel("ToUserName", m_Model);
            mT.ToUserName = XMLHelper.ReadModel("FromUserName", m_Model);
            mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", m_Model));
            mT.MsgType = "text";
            mT.Content = "直接回复“买楼+销售人员手机号码+日期（如20140901）”格式进行自助预定楼盘单元，预约收到成功短信后，将有专属销售人员联系到您，买楼更多积分并抽奖哦。";
            XMLHelper.ResponseToEnd(m_ContentSender.SendText(mT));
        }

        private void ApplyOrg()
        {
            SText mT = new SText();
            mT.FromUserName = XMLHelper.ReadModel("ToUserName", m_Model);
            mT.ToUserName = XMLHelper.ReadModel("FromUserName", m_Model);
            mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", m_Model));
            mT.MsgType = "text";
            mT.Content = "直接回复“注册+手机号码”即可成为瞰粉，注意，微信号和手机号作为瞰青官微相关活动（抽奖、优惠等）的唯一凭证。";
            XMLHelper.ResponseToEnd(m_ContentSender.SendText(mT));
        }

        private void MyCent()
        {
            SText mT = new SText();
            mT.FromUserName = XMLHelper.ReadModel("ToUserName", m_Model);
            mT.ToUserName = XMLHelper.ReadModel("FromUserName", m_Model);
            mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", m_Model));
            mT.MsgType = "text";
            mT.Content = "尊敬的刘欢先生：你的积分为3分。";
            XMLHelper.ResponseToEnd(m_ContentSender.SendText(mT));
        }

        private void MyLuck()
        {
            SText mT = new SText();
            mT.FromUserName = XMLHelper.ReadModel("ToUserName", m_Model);
            mT.ToUserName = XMLHelper.ReadModel("FromUserName", m_Model);
            mT.CreateTime = long.Parse(XMLHelper.ReadModel("CreateTime", m_Model));
            mT.MsgType = "text";
            mT.Content = "成为会员后，直接输入“我要抽奖”即可抽奖，每次消耗1个积分，大奖等着你。\n特等奖，IPhone6一台（共3台）。\n一等奖，IPad一台（共10台）。\n二等奖，小米手机一部（共20部）";
            XMLHelper.ResponseToEnd(m_ContentSender.SendText(mT));
        }
    }
}
