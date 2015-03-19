using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXStudio.EFModel.Entities.DataMgt
{
    public enum BuyConfirmStateEnum
    {
        /// <summary>
        /// 未确认
        /// </summary>
        [Display(Name = "未确认")]
        Default = 0,
        /// <summary>
        /// 现场确认
        /// </summary>
        [Display(Name = "现场确认")]
        BuyConfirmed = 1,
    }
}
