using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PStudio.WXPlatform.EFModel.Models
{
    /// <summary>
    /// 文章
    /// </summary>
    [Table("tb_Article")]
    public class Article
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ArticleId
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
        /// 类别ID
        /// </summary>
        //[DefaultValue(0)]
        public int ArtSortId
        {
            get;
            set;
        }

        public virtual ArtSort ArtSort
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

        /// <summary>
        /// 标题
        /// </summary>
        //[MaxLength(100)]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 关键字
        /// </summary>
        //[MaxLength(150)]
        public string Keywords
        {
            get;
            set;
        }

        /// <summary>
        /// 简介
        /// </summary>
        //[MaxLength(680)]
        public string Summary
        {
            get;
            set;
        }

        /// <summary>
        /// 内容
        /// </summary>
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
        /// 发布时间
        /// </summary>
        //[DefaultValue(DateTime.Now)]
        public DateTime PublishTime
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        //[DefaultValue(99)]
        public int AOrder
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
        /// 缩略图
        /// </summary>
        //[MaxLength(350)]
        public string MinImg
        {
            get;
            set;
        }
    }
}
