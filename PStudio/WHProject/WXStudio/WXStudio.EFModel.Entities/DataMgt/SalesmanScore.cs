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
    [Table("tb_sale_salesmanscore")]
    public class SalesmanScore
    {
        public int SalesmanScoreId
        {
            get;
            set;
        }

        public int SaleBookId
        {
            get;
            set;
        }

        public int SalesmanId
        {
            get;
            set;
        }

        public float ScoreValue
        {
            get;
            set;
        }

        public string Comments
        {
            get;
            set;
        }

        public string Column1
        {
            get;
            set;
        }

        public string Column2
        {
            get;
            set;
        }

        public string Column3
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

        public int CompanyID
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
