using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WXStudio.Framework.Unity;

namespace WXStudio.EFModel.Entities.DataMgt
{
    [Table("tb_sale_salesman")]
    public class Salesman
    {
        [Display(Name = "销售人员ID")]
        public int SalesmanId
        {
            get;
            set;
        }

        [Display(Name = "微信号")]
        public string WXCode
        {
            get;
            set;
        }


        [Display(Name = "昵称")]
        public string WXNickname
        {
            get;
            set;
        }


        [Display(Name = "姓名")]
        public string Name
        {
            get;
            set;
        }


        [Display(Name = "手机")]
        [RegularExpression("1[3|5|7|8|][0-9]{9}",ErrorMessage="请输入正确的手机号码！")]
        [Required(ErrorMessage = "手机不能为空")]
        //[Remote("CheckPhoneExists", "Mobile", ErrorMessage = "已经注册该手机！")] 
        public string Phone
        {
            get;
            set;
        }


        [Display(Name = "公司")]
        public string Company
        {
            get;
            set;
        }


        [Display(Name = "地址")]
        public string Address
        {
            get;
            set;
        }


        [Display(Name = "备注")]
        public string Comments
        {
            get;
            set;
        }

        private long? m_createDateTimeStamp = TimeStampUtility.GetDefaultTimeStamp();

        [Display(Name = "创建时间（时间戳）")]
        public long? CreateDateTimeStamp
        {
            get
            {
                return m_createDateTimeStamp;
            }
            set
            {
                m_createDateTimeStamp = value;
            }
        }

        [NotMapped]
        [Display(Name = "创建时间")]
        public DateTime? CreateDate
        {
            get
            {
                if (this.CreateDateTimeStamp != null)
                    return TimeStampUtility.FromTimeStamp(this.CreateDateTimeStamp.Value);
                return null;
            }
            set
        {
                if (value != null)
                    this.CreateDateTimeStamp = TimeStampUtility.ToTimeStamp(value.Value);
                else this.CreateDateTimeStamp = null;
            }
        }

        [Display(Name = "销售二维码")]
        public string Column1
        {
            get;
            set;
        }

        [Display(Name = "销售网址")]
        public string Column2
        {
            get;
            set;
        }

        [Display(Name = "其他")]
        public string Column3
        {
            get;
            set;
        }

        public int? CompanyID
        {
            get;
            set;
        }

        //20141007 added 

        public string Column4
        {
            get;
            set;
        }

        public string Column5
        {
            get;
            set;
        }
    }
}
