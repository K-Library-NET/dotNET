using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WXStudio.EFModel.Entities.DataMgt;

namespace WXStudio.DataMgt.Web.Models
{
    public class SaleBookViewModel : SaleBook
    {
        private SaleBook m_saleBook = null;

        public SaleBookViewModel(SaleBook saleBook)
        {
            this.m_saleBook = saleBook; 
        }

        [Display(Name="看楼确认")]
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
    }
}