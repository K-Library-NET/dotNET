using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;

namespace WXStudio.ConfigMgt.Web.Weixin
{
    public class CustomMessageHandler : MessageHandler<MessageContext>
    {
        public CustomMessageHandler(Stream inputStream)

            : base(inputStream)
        {



        }



        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {

            var responseMessage = base.CreateResponseMessage<ResponseMessageText>(); //ResponseMessageText也可以是News等其他类型

            responseMessage.Content = "这条消息来自DefaultResponseMessage。";

            return responseMessage;

        }


        public override void OnExecuting()
        {

            if (RequestMessage.FromUserName == "olPjZjsXuQPJoV0HlruZkNzKc91E")
            {

                CancelExcute = true; //终止此用户的对话



                //如果没有下面的代码，用户不会收到任何回复，因为此时ResponseMessage为null



                //添加一条固定回复

                var responseMessage = CreateResponseMessage<ResponseMessageText>();

                responseMessage.Content = "Hey！你已经被拉黑啦！";



                ResponseMessage = responseMessage;//设置返回对象

            }

        }
    }
}