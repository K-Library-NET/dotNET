using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXStudio.Framework.WeixinHelper.Model
{
    public class RText
    {
        public string ToUserName { get; set; }// 开发者微信号
        public string FromUserName { get; set; }// 用户号（OpenID）
        public long CreateTime { get; set; }// 创建时间
        public string MsgType { get; set; } //消息类型
        public string Content { get; set; }//内容
        public long MsgId { get; set; }//消息ID
    }
}
