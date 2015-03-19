using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PStudio.WXPlatform.EFModel.AdminModels
{
    [Table("tb_siteorganizations")]
    public class Organization
    {
        public int OrganizationId
        {
            get;
            set;
        }

        public string SiteName
        {
            get;
            set;
        }

        public string SiteDescription
        {
            get;
            set;
        }

        public string ConnectionString
        {
            get;
            set;
        }
    }
}
