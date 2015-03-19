using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WXStudio.Framework.WeixinHelper.Model
{
   public  class Smusic
    {
        public string ToUserName { get; set; }// 用户号（OpenID）
        public string FromUserName { get; set; }// 开发者微信号

        public long CreateTime { get; set; }// 创建时间

        public string MsgType { get; set; } //消息类型

        public string Title { get; set; }//音乐标题
        public string Description { get; set; }//音乐描述
        public string MusicURL { get; set; }//音乐链接
        public string HQMusicUrl { get; set; }//高音质链接
        public string ThumbMediaId { get; set; }//上传的缩略图ID
    }
}
