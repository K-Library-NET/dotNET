using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PStudio.WXPlatform.EFModel.Models
{
    /// <summary>
    /// 用户（公网用户）
    /// </summary>
    [Table("tb_User")]
    public class User
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int UserId
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
        /// 自由系统的用户ID
        /// </summary>
        //[DefaultValue(0)]
        public int InId
        {
            get;
            set;
        }

        /// <summary>
        /// 微信openid
        /// </summary>
        //[MaxLength(150)]
        public string OpenId
        {
            get;
            set;
        }

        /// <summary>
        /// 分组ID
        /// </summary>
        //[DefaultValue(0)]
        public int GroupId
        {
            get;
            set;
        }

        public virtual Group Group
        {
            get;
            set;
        }

        /// <summary>
        /// 昵称-微信
        /// </summary>
        //[MaxLength(50)]
        public string NickName
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
        /// 状态，1为正常
        /// </summary>
        //[DefaultValue(1)]
        public int State
        {
            get;
            set;
        }

        /// <summary>
        /// 预留字段1
        /// </summary>
        //[MaxLength(150)]
        public string PreFirst
        {
            get;
            set;
        }

        /// <summary>
        /// 用户资料
        /// </summary>
        public UserData UserData
        {
            get;
            set;
        }
    }
}
