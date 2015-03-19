using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PStudio.WXPlatform.EFModel.Models
{
    /// <summary>
    /// 分组
    /// </summary>
    [Table("tb_Group")]
    public class Group
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int GroupId
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<User> Users
        {
            get;
            set;
        }
        
        /// <summary>
        /// 名称-本地
        /// </summary>
        //[Requried,MaxLength(50)]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 对应微信分组ID
        /// </summary>
        //[DefaultValue(0)]
        public int WeiId
        {
            get;
            set;
        }

        /// <summary>
        /// 微信分组名
        /// </summary>
        //[MaxLength(50)]
        public string WeiName
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
       //[MaxLength(100)]
        public string Note
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
    }
}
