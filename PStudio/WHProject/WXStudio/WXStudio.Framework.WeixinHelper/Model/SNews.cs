using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WXStudio.Framework.WeixinHelper.Model
{
    public class SNews
    {
        public string ToUserName { get; set; }// 用户号（OpenID）
        public string FromUserName { get; set; }// 开发者微信号

        public long CreateTime { get; set; }// 创建时间

        public string MsgType { get; set; } //消息类型

        public int ArticleCount { get; set; }//图文个数

        public List<ArticlesModel> Articles { get; set; }//图文列表
    }
    public class ArticlesModel //默认第一条大图显示
    {
        public string Title { get; set; }//标题
        public string Description { get; set; }//描述
        public string PicUrl { get; set; }//图片链接 
        public string Url { get; set; }//点击之后跳转的链接

    }
}
