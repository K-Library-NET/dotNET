using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXStudio.EFModel.Entities.Core
{
    [Table("tb_core_config")]
    public class CoreConfig
    {
        public int CoreConfigId
        {
            get;
            set;
        }

        [Required(ErrorMessage = "必须填写配置项的主键")]
        [Display(Name = "配置项主键")]
        public string Key
        {
            get;
            set;
        }

        [Required(ErrorMessage = "必须填写配置值")]
        [Display(Name = "配置值")]
        public string Value
        {
            get;
            set;
        }

        [Required(ErrorMessage = "必须填写配置项名称。")]
        [Display(Name = "配置项名称")]
        public string Comments
        {
            get;
            set;
        }
    }
}
