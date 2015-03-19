using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXStudio.Framework.Unity;

namespace WXStudio.EFModel.Entities.DataMgt
{

    [Table("tb_sale_book")]
    public class SaleBook
    {
        public int SaleBookId
        {
            get;
            set;
        }

        /// <summary>
        /// 销售员ID
        /// </summary>
        [Display(Name="销售人员ID")]
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

        [Display(Name = "微信")]
        public string WXNickname
        {
            get;
            set;
        }

        [Display(Name = "姓名(必填)")]
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "手机(必填)")]
        [RegularExpression("1[3|5|7|8|][0-9]{9}", ErrorMessage = "请输入正确的手机号码！")]
        [Required(ErrorMessage = "手机不能为空")]
        public string Phone
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

        /// <summary>
        /// 默认为0，1为现场确认
        /// </summary>
        [Display(Name="看楼确认")]
        public ViewConfirmStateEnum ViewConfirm
        {
            get;
            set;
        }

        /// <summary>
        /// 默认为0，1为签约买楼意向后的确认
        /// </summary>
        [Display(Name="签约确认")]
        public BuyConfirmStateEnum BuyConfirm
        {
            get;
            set;
        }

        [Display(Name = "意向单元")]
        /// <summary>
        /// 意向单元
        /// </summary>
        public string BuyUnit
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

        [NotMapped]
        [Display(Name = "看楼确认")]
        public string ViewConfirmStateString
        {
            get
            {
                if (this.ViewConfirm == ViewConfirmStateEnum.ViewConfirmed)
                {
                    return "现场确认";
                }
                return "未确认";
            }
        }

        [NotMapped]
        [Display(Name = "买楼确认")]
        public string BuyConfirmStateString
        {
            get
            {
                if (this.BuyConfirm == BuyConfirmStateEnum.BuyConfirmed)
                {
                    return "现场确认";
                }
                return "未确认";
            }
        }

        [Display(Name = "销售人员")]
        [Required(ErrorMessage = "若无请填“无”")]
        public string Column1
        {
            get;
            set;
        }

        [Display(Name = "销售人员电话")]
        [Required(ErrorMessage = "若无请填“无”")]
        public string Column2
        {
            get;
            set;
        }

        [Display(Name = "预约渠道")]
        [Required(ErrorMessage = "中介或媒体，若无请填“无”")]
        public string Column3
        {
            get;
            set;
        }

        //20141007 added:


        private long? m_bookDateTimeStamp = TimeStampUtility.GetDefaultTimeStamp();

        [Display(Name = "预订时间（时间戳）")]
        public long? BookDateTimeStamp
        {
            get
            {
                return m_bookDateTimeStamp;
            }
            set
            {
                m_bookDateTimeStamp = value;
            }
        }

        [NotMapped]
        [Display(Name = "预订时间")]
        public DateTime? BookDate
        {
            get
            {
                if (this.BookDateTimeStamp != null)
                    return TimeStampUtility.FromTimeStamp(this.BookDateTimeStamp.Value);
                return null;
            }
            set
            {
                if (value != null)
                    this.BookDateTimeStamp = TimeStampUtility.ToTimeStamp(value.Value);
                else this.BookDateTimeStamp = null;
            }
        }

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

        public int? CompanyId
        {
            get;
            set;
        }
    }
}
