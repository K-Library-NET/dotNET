using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXStudio.EFModel.Entities.Core;
using WXStudio.EFModel.Entities.DataMgt;

namespace WXStudio.EFModel.Entities
{
    public class WXPstudioDbContext : Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext<CoreAccount> //DbContext
    {
        public WXPstudioDbContext()
            : base("name=defaultConnStr")
        {
            Database.SetInitializer<WXPstudioDbContext>(new DropCreateDbWhenModelChange());
        }

        public static WXPstudioDbContext Create()
        {
            return new WXPstudioDbContext();
        }

        //public DbSet<CoreAccount> CoreAccounts
        //{
        //    get;
        //    set;
        //}

        public DbSet<CoreCompany> CoreCompanies
        {
            get;
            set;
        }

        public DbSet<CoreConfig> CoreConfigs
        {
            get;
            set;
        }

        public DbSet<SaleBook> SaleBooks
        {
            get;
            set;
        }

        public DbSet<Salesman> Salesmans
        {
            get;
            set;
        }

        public DbSet<SalesmanScore> SalesmanScores
        {
            get;
            set;
        }
    }
}
