using WXStudio.Framework.WeixinHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXStudio.Framework.WeixinHelper
{
    public class ContentSender
    {
        public string SendText(SText model)
        {
            return string.Format(@"<xml>
                        <ToUserName><![CDATA[{0}]]></ToUserName>
                        <FromUserName><![CDATA[{1}]]></FromUserName>
                        <CreateTime>{2}</CreateTime>
                        <MsgType><![CDATA[{3}]]></MsgType>
                        <Content><![CDATA[{4}]]></Content>
                     </xml>", model.ToUserName, model.FromUserName, model.CreateTime, model.MsgType, model.Content);
        }



        public string SendImg(SImg model)
        {
            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <Image>
                                    <MediaId><![CDATA[{4}]]></MediaId>
                                    </Image>
                                    </xml>", model.ToUserName, model.FromUserName, model.CreateTime, model.MsgType, model.MediaId);
        }

        public string SendVoice(SVoice model)
        {
            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <Voice>
                                    <MediaId><![CDATA[{4}]]></MediaId>
                                    </Voice>
                                    </xml>", model.ToUserName, model.FromUserName, model.CreateTime, model.MsgType, model.MediaId);
        }

        public string SendVideo(SVideo model)
        {
            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <Video>
                                    <MediaId><![CDATA[{4}]]></MediaId>
                                    <Title><![CDATA[{5}]]></Title>
                                    <Description><![CDATA[{6}]]></Description>
                                    </Video> 
                                    </xml>", model.ToUserName, model.FromUserName, model.CreateTime, model.MsgType, model.MediaId, model.Title, model.Description);

        }

        public string SendMusic(Smusic model)
        {
            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <Music>
                                    <Title><![CDATA[{4}]]></Title>
                                    <Description><![CDATA[{5}]]></Description>
                                    <MusicUrl><![CDATA[{6}]]></MusicUrl>
                                    <HQMusicUrl><![CDATA[{7}]]></HQMusicUrl>
                                    <ThumbMediaId><![CDATA[{8}]]></ThumbMediaId>
                                    </Music>
                                    </xml>", model.ToUserName, model.FromUserName,
                                           model.CreateTime, model.MsgType,
                                           model.Title, model.Description,
                                           model.MusicURL, model.HQMusicUrl, model.ThumbMediaId);


        }

        public string SendNews(SNews model)
        {
            string art = "";
            foreach (var one in model.Articles)
            {

                art += string.Format(@"
                                    <item>
                                    <Title><![CDATA[{0}]]></Title>
                                    <Description><![CDATA[{1}]]></Description>
                                    <PicUrl><![CDATA[{2}]]></PicUrl>
                                    <Url><![CDATA[{3}]]></Url>
                                    </item>", one.Title, one.Description, one.PicUrl, one.Url);


            }

            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <ArticleCount>{4}</ArticleCount>
                                    <Articles>{5}</Articles>
                                    </xml> ", model.ToUserName, model.FromUserName, model.CreateTime, model.MsgType,
                                            model.ArticleCount, art);
        }
    }
}
