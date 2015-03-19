using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WXStudio.Framework.WeixinHelper.Model
{
   public  class SImg
    {
        public string ToUserName { get; set; }// 用户号（OpenID）
        public string FromUserName { get; set; }// 开发者微信号

        public long CreateTime { get; set; }// 创建时间

        public string MsgType { get; set; } //消息类型

        public string MediaId { get; set; }//通过上传的图片ID
    }
}
