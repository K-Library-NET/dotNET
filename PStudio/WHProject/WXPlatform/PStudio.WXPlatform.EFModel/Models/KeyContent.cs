using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PStudio.WXPlatform.EFModel.Models
{
    /// <summary>
    /// 关键字返回内容
    /// </summary>
    //[Table("tb_KeyContent")]
    public class KeyContent
    {
        ///// <summary>
        ///// 主键
        ///// </summary>
        //public int KeyContentId
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 标题
        /// </summary>
        //[MaxLength(150)]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 内容
        /// </summary>
        //[MaxLength(700)]
        public string Content
        {
            get;
            set;
        }

        ///// <summary>
        ///// 关键字ID
        ///// </summary> 
        //public int KeyWordId
        //{
        //    get;
        //    set;
        //}

        //public virtual KeyWord KeyWord
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 类型-文本图片……？
        /// </summary> 
        //[DefaultValue(1)]
        public int Type
        {
            get;
            set;
        }

        /// <summary>
        /// 图片
        /// </summary>
        //[MaxLength(250)]
        public string MinImg
        {
            get;
            set;
        }


        /// <summary>
        /// 置顶：0不是，1是
        /// </summary>
        public int ITop
        {
            get;
            set;
        }

        /// <summary>
        /// 置顶开始时间
        /// </summary>
        //[DefaultValue(DateTime.Now)]
        public DateTime TopBeginTime
        {
            get;
            set;
        }

        /// <summary>
        /// 置顶结束时间
        /// </summary>
        //[DefaultValue(DateTime.Now)]
        public DateTime TopEndTime
        {
            get;
            set;
        }

        ///// <summary>
        ///// 创建时间
        ///// </summary>
        ////[DefaultValue(DateTime.Now)]
        //public DateTime CreateTime
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 状态
        ///// </summary>
        ////[DefaultValue(1)]
        //public int State
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// '#' href=。。。
        /// </summary>
        //[MaxLength(250)]
        public string Href
        {
            get;
            set;
        }
    }
}
