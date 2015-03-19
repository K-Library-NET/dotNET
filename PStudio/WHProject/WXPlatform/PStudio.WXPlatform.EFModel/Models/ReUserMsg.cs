using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PStudio.WXPlatform.EFModel.Models
{
    /// <summary>
    /// 回复消息表
    /// </summary>
    [Table("tb_ReUserMsg")]
    public class ReUserMsg
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ReUserMsgId
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

        public virtual UserMsg UserMsg
        {
            get;
            set;
        }

        /// <summary>
        /// 消息表主键
        /// </summary>
        public int UserMsgId
        {
            get;
            set;
        }

        /// <summary>
        /// 回复类型
        /// </summary>
        //[DefaultValue(1)]
        public int ReType
        {
            get;
            set;
        }

        /// <summary>
        /// 回复点 // 1.文章
        /// </summary>
        //[DefaultValue(1)]
        public int ReFrom
        {
            get;
            set;
        }

        /// <summary>
        /// 回复的内容？
        /// </summary>
        //[MaxLength(80)]
        public string ReContent
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
    }
}
