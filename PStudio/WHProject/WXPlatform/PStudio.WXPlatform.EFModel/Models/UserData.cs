using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PStudio.WXPlatform.EFModel.Models
{
    /// <summary>
    /// 用户资料
    /// </summary>
    //[Table("tb_UserData")]
    public class UserData
    {
        /// <summary>
        /// 主键
        /// </summary>
        //public int UserDataId
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 用户表主键
        /// </summary>
        //[DefaultValue(0)]
        //public int UserId
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 性别：0未知、1男、2女
        /// </summary>
        //[DefaultValue(0)]
        public int Sex
        {
            get;
            set;
        }

        /// <summary>
        /// 城市
        /// </summary>
        //[MaxLength(20)]
        public string City
        {
            get;
            set;
        }

        /// <summary>
        /// 国家
        /// </summary>
        //[MaxLength(30)]
        public string Country
        {
            get;
            set;
        }

        /// <summary>
        /// 省份
        /// </summary>
        //[MaxLength(20)]
        public string Province
        {
            get;
            set;
        }

        /// <summary>
        /// 语言
        /// </summary>
        //[MaxLength(15)]
        public string Language
        {
            get;
            set;
        }

        /// <summary>
        /// 用户头像
        /// </summary>
        //[MaxLength(250)]
        public string HeadImgUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 最后次关注事件
        /// </summary>
        //[MaxLength(50)]
        public string SubTime
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

        /// <summary>
        /// 预留字段1
        /// </summary>
        //[MaxLength(250)]
        public string PreAllocProperty1
        {
            get;
            set;
        }

        /// <summary>
        /// 预留字段2
        /// </summary>
        //[MaxLength(250)]
        public string PreAllocProperty2
        {
            get;
            set;
        }

        /// <summary>
        /// 预留字段3
        /// </summary>
        //[MaxLength(250)]
        public string PreAllocProperty3
        {
            get;
            set;
        }

        /// <summary>
        /// 预留字段4
        /// </summary>
        //[MaxLength(250)]
        public string PreAllocProperty4
        {
            get;
            set;
        }

        /// <summary>
        /// 预留字段5
        /// </summary>
        //[MaxLength(250)]
        public string PreAllocProperty5
        {
            get;
            set;
        }
    }
}
