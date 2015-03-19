using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXStudio.Framework.WeixinHelper.Model
{
    public class RMenuClick
    {
        public string ToUserName { get; set; }// 开发者微信号
        public string FromUserName { get; set; }// 用户号（OpenID）
        public long CreateTime { get; set; }// 创建时间
        public string MsgType { get; set; } //消息类型

        public string Event { get; set; }//事件类型
        public string EventKey { get; set; }//事件key
    }
}
