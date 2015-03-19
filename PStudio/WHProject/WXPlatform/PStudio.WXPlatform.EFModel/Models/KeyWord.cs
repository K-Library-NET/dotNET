using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PStudio.WXPlatform.EFModel.Models
{
    /// <summary>
    /// 关键字
    /// </summary>
    [Table("tb_KeyWord")]
    public class KeyWord
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int KeyWordId
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
        /// 名称
        /// </summary>
        //[MaxLength(50)]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        //[MaxLength(250)]
        public string Note
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
        /// 创建时间
        /// </summary>
        //[DefaultValue(DateTime.Now)]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 链接地址
        /// </summary>
        //[MaxLength(250)]
        public string LinkUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 回复消息类型
        /// </summary>
        //[DefaultValue(1)]
        public int ReType
        {
            get;
            set;
        }

        /// <summary>
        /// 关键字返回内容
        /// </summary>
        public KeyContent KeyContent
        {
            get;
            set;
        }
    }
}
