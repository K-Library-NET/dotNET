using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PStudio.WXPlatform.EFModel.Models
{
    /// <summary>
    /// 文章分类
    /// </summary>
    [Table("tb_ArtSort")]
    public class ArtSort
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ArtSortId
        {
            get;
            set;
        }

        public virtual ICollection<Article> Articles { get; set; }

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
        /// 父级ID
        /// </summary>
        //[DefaultValue(0)]
        public int ParentId
        {
            get;
            set;
        }

        /// <summary>
        /// 当前级别
        /// </summary>
        //[DefaultValue(1)]
        public int IndexLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        //[DefaultValue(99)]
        public int SOrder
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
        /// 备注
        /// </summary>
        //[MaxLength(150)]
        public string Note
        {
            get;
            set;
        }
    }
}
