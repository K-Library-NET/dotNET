using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WXStudio.EFModel.Entities.DataMgt;

namespace WXStudio.DataMgt.Web.Models
{
    public class SalesmanScoreViewModel
    {
        public SalesmanScoreViewModel()
        {//销售人员ID，手机，姓名，预约人员姓名，手机，记录生成时间

        }

        internal static IEnumerable<SalesmanScoreViewModel> ToList(WXStudio.EFModel.Entities.WXPstudioDbContext db)
        {
            List<SalesmanScoreViewModel> models = new List<SalesmanScoreViewModel>();

            try
            {
                ILookup<int, Salesman> lookup = db.Salesmans.ToLookup(p => p.SalesmanId);
                ILookup<int, SaleBook> lookup2 = db.SaleBooks.ToLookup(p => p.SaleBookId);

                try
                {
                    foreach (var one in db.SalesmanScores)
                    {
                        SalesmanScoreViewModel re = new SalesmanScoreViewModel() { SalesmanScore = one };
                        if (lookup.Contains(re.SalesmanScore.SalesmanId) && lookup[re.SalesmanScore.SalesmanId] != null
                            && lookup[re.SalesmanScore.SalesmanId].Count() > 0)
                        {
                            var first = lookup[re.SalesmanId].First();
                            re.SalesmanName = first.Name;
                            re.SalesmanPhone = first.Phone;
                        }
                        if (lookup2.Contains(re.SalesmanScore.SaleBookId) && lookup2[re.SalesmanScore.SaleBookId] != null
                            && lookup2[re.SalesmanScore.SaleBookId].Count() > 0)
                        {
                            var first = lookup2[re.SalesmanScore.SaleBookId].First();
                            re.ClientName = first.Name;
                            re.ClientPhone = first.Phone;
                        }

                        if (re.SalesmanScore.CreateDate != null)
                        {
                            re.CreateDate = re.SalesmanScore.CreateDate.Value;
                        }
                        models.Add(re);
                    }
                }
                catch (Exception ex2)
                {
                    LogHelper.Error("SalesmanScoreViewModel.ToList EX2 出错：" + ex2.StackTrace, ex2);        
                }
            }
            catch (Exception e)
            {
                LogHelper.Error("SalesmanScoreViewModel.ToList出错：" + e.StackTrace, e);
            }

            return models;
        }

        /// <summary>
        /// 记录生成时间
        /// </summary>
        [Display(Name = "记录生成时间")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 销售人员手机
        /// </summary>
        [Display(Name = "预约人员手机")]
        public string ClientPhone { get; set; }

        /// <summary>
        /// 销售人员姓名
        /// </summary>
        [Display(Name = "预约人员姓名")]
        public string ClientName { get; set; }

        /// <summary>
        /// 预约人员姓名
        /// </summary>
        [Display(Name = "销售人员姓名")]
        public string SalesmanName { get; set; }

        /// <summary>
        /// 预约人员手机
        /// </summary>
        [Display(Name = "销售人员手机")]
        public string SalesmanPhone { get; set; }

        /// <summary>
        /// 销售人员ID
        /// </summary>
        [Display(Name = "销售人员ID")]
        public int SalesmanId
        {
            get
            {
                if (this.SalesmanScore != null)
                    return this.SalesmanScore.SalesmanId;

                return 0;
            }
        }

        public EFModel.Entities.DataMgt.SalesmanScore SalesmanScore { get; set; }
    }
}