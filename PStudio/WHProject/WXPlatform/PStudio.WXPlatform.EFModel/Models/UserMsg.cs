using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PStudio.WXPlatform.EFModel.Models
{
    /// <summary>
    /// 用户消息
    /// </summary>
    [Table("tb_UserMsg")]
    public class UserMsg
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int UserMsgId
        {
            get;
            set;
        }

        /// <summary>
        /// 组织ID，用于数据分块
        /// </summary>
        public int OrgId
        {
            get;
            set;
        }

        /// <summary>
        /// 消息类型：文本、……
        /// </summary>
        //[DefaultValue(1)]
        public int MsgType
        {
            get;
            set;
        }

        /// <summary>
        /// 事件ID： 自定义
        /// </summary>
        //[DefaultValue(1)]
        public int EventId
        {
            get;
            set;
        }

        /// <summary>
        ///消息内容
        /// </summary>
        //[MaxLength(700)]
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        //[DefaultValue(DateTime.Now)]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        //[DefaultValue(1)]
        public int State
        {
            get;
            set;
        }

        /// <summary>
        /// 回复状态
        /// </summary>
        public int ReState
        {
            get;
            set;
        }

        /// <summary>
        /// 微信消息ID
        /// </summary>
        //[MaxLength(50)]
        public string WeiMsgId
        {
            get;
            set;
        }

        /// <summary>
        /// 用户表主键
        /// </summary>
        public int UserId
        {
            get;
            set;
        }

        /// <summary>
        /// 用户回复的消息
        /// </summary>
        public virtual ICollection<ReUserMsg> ReUserMsgs
        {
            get;
            set;
        }
    }
}
