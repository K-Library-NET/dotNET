using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PStudio.WXPlatform.EFModel.Models
{
    /// <summary>
    /// 自定义菜单
    /// </summary>
    [Table("tb_PersonalMenu")]
    public class PersonalMenu
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int PersonalMenuId
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
        /// 类型：1 click 2 view
        /// </summary>
        //[DefaultValue(1)]
        public int Type
        {
            get;
            set;
        }

        /// <summary>
        /// 父级ID
        /// </summary>
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
        /// 链接地址
        /// </summary>
        //[MaxLength(350)]
        //[DefaultValue("#")]
        public string LinkUrl
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
        /// 排序
        /// </summary>
        //[DefaultValue(99)]
        public int POrder
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
