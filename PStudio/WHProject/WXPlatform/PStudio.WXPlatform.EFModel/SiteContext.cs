using PStudio.WXPlatform.EFModel.AdminModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PStudio.WXPlatform.EFModel
{

    public class SiteContext : DbContext
    {
        public SiteContext()
            : base("name=SiteDbContext")
        {

        }

        public DbSet<Organization> SiteOrganizations { get; set; }
    }
}
