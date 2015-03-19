using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXStudio.EFModel.Entities
{
    public class DropCreateDbWhenModelChange : CreateDatabaseIfNotExists<WXPstudioDbContext>
    {
        public override void InitializeDatabase(WXPstudioDbContext context)
        {
            try
            {
                var exists = context.Database.Exists();
                if (exists) //&& context.Database.CompatibleWithModel(true))
                {
                    return;
                }

                base.InitializeDatabase(context);

                #region 默认配置项
                var defConfig1 = context.CoreConfigs.Create();

                defConfig1.Key = "ViewHouseScore";
                defConfig1.Value = "300";
                defConfig1.Comments = "看楼积分";

                var defConfig2 = context.CoreConfigs.Create();

                defConfig2.Key = "BuyHouseScore";
                defConfig2.Value = "5000";
                defConfig2.Comments = "买楼积分";

                context.CoreConfigs.Add(defConfig1);
                context.CoreConfigs.Add(defConfig2);

                context.SaveChanges();
                #endregion

                #region test 销售人员

                //DebugAddDatas(context);

                #endregion
                //debug
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        private static void DebugAddDatas(WXPstudioDbContext context)
        {
            context.Salesmans.Add(new DataMgt.Salesman()
            {
                CreateDate = DateTime.Now,
                Name = "Test1",
                Phone = "18520476327",
                WXCode = "WXCode 111",
                WXNickname = "WX Nickname 111",
                Address = "Addr1"
            });

            context.Salesmans.Add(new DataMgt.Salesman()
            {
                CreateDate = DateTime.Now,
                Name = "Test2",
                Phone = "13826403668",
                WXCode = "WXCode 222",
                WXNickname = "WX Nickname 222",
                Address = "Addr2"
            });

            context.SaleBooks.Add(new DataMgt.SaleBook()
            {
                CreateDate = DateTime.Now,
                Name = "Test1",
                Phone = "13826403668",
                WXCode = "WXCode 111",
                WXNickname = "WX Nickname 111",
            });

            context.SaleBooks.Add(new DataMgt.SaleBook()
            {
                CreateDate = DateTime.Now,
                Name = "Test2",
                Phone = "18520476327",
                WXCode = "WXCode 222",
                WXNickname = "WX Nickname 222",
            });

            //try
            //{
            context.SaveChanges();
            //}
            //catch (Exception e)
            //{
            //    if (e is System.Data.Entity.Validation.DbEntityValidationException)
            //    {
            //        System.Data.Entity.Validation.DbEntityValidationException de = e as System.Data.Entity.Validation.DbEntityValidationException;
            //        Console.WriteLine(
            //            de.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage
            //         );
            //        Console.WriteLine(
            //            de.EntityValidationErrors.Skip(1).First().ValidationErrors.First().ErrorMessage
            //         );
            //    }
            //    Console.WriteLine(e.Data);
            //}
        }

        protected override void Seed(WXPstudioDbContext context)
        {
            var exists = context.Database.Exists();
            if (exists && context.Database.CompatibleWithModel(true))
            {
                return;
            }

            // If the database exists and doesn't match the model
            // then prompt for input
            if (exists)
            {
                context.Database.Delete();
            }
            // Database either didn't exist or it didn't match
            // the model and the user chose to delete it
            context.Database.Create();

            base.Seed(context);
        }
    }
}
