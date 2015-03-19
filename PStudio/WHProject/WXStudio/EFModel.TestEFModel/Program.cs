using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModel.TestEFModel
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<WXStudio.EFModel.Entities.WXPstudioDbContext,
            //    WXStudio.EFModel.Entities.Migrations.Configuration>());

            using (WXStudio.EFModel.Entities.WXPstudioDbContext context = new WXStudio.EFModel.Entities.WXPstudioDbContext())
            {
                var sales = new WXStudio.EFModel.Entities.DataMgt.Salesman(); //context.Salesmans.Create();
                sales.Phone = "13650886728";
                var sales1 = from one in context.SaleBooks
                             where one.Phone == sales.Phone
                             select one;
                if (sales1 != null && sales1.Count() > 0)
                {
                    var sales2 = sales1.First();
                    if (sales2 != null)
                    {
                        context.SaleBooks.Remove(sales2);
                        context.SaveChanges();
                    }
                }
                //if (context.CoreAccounts != null && context.CoreAccounts.Count() > 0)
                //{
                //    Console.WriteLine("has result");
                //}
            }
            Console.WriteLine("Finish result");
            Console.ReadLine();
        }
    }
}
