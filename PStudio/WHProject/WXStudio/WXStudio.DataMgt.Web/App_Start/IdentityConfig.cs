using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using WXStudio.DataMgt.Web.Models;
using WXStudio.EFModel.Entities;
using WXStudio.EFModel.Entities.Core;
using System.Linq;

namespace WXStudio.DataMgt.Web
{
    // 配置此应用程序中使用的应用程序用户管理器。UserManager 在 ASP.NET Identity 中定义，并由此应用程序使用。

    public class ApplicationUserManager : UserManager<WXStudio.EFModel.Entities.Core.CoreAccount>//ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<WXStudio.EFModel.Entities.Core.CoreAccount> store) //ApplicationUser> ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<WXStudio.EFModel.Entities.Core.CoreAccount>(context.Get<WXStudio.EFModel.Entities.WXPstudioDbContext>()));
            //ApplicationUser>(context.Get<ApplicationDbContext>()));
            // 配置用户名的验证逻辑
            manager.UserValidator = new UserValidator<WXStudio.EFModel.Entities.Core.CoreAccount>(manager)//ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // 配置密码的验证逻辑
            manager.PasswordValidator = new PasswordValidator
            {
                //20140921 changed: 降低密码复杂度
                RequiredLength = 4,
                //RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                //RequireNonLetterOrDigit = true,
                RequireDigit = false,
                //RequireDigit = true,
                RequireLowercase = false,
                //RequireLowercase = true,
                RequireUppercase = false,
                //RequireUppercase = true,
            };
            // 注册双重身份验证提供程序。此应用程序使用手机和电子邮件作为接收用于验证用户的代码的一个步骤
            // 你可以编写自己的提供程序并在此处插入。
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<WXStudio.EFModel.Entities.Core.CoreAccount> //ApplicationUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<WXStudio.EFModel.Entities.Core.CoreAccount>  //ApplicationUser>
            {
                Subject = "安全代码",
                BodyFormat = "Your security code is: {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<WXStudio.EFModel.Entities.Core.CoreAccount//>ApplicationUser
                    >(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            InitDefaultUsers();

            return manager;
        }

        private static object m_syncRoot = new object();
        private static bool m_initRoot = false;

        private static void InitDefaultUsers()
        {
            lock (m_syncRoot)
            {
                if (m_initRoot)
                    return;

                #region 默认用户

                using (WXStudio.EFModel.Entities.WXPstudioDbContext context = new EFModel.Entities.WXPstudioDbContext())
                {
                    //var items = from one in context.Users
                    //            where one.UserName == "kanqing"
                    //            select one;

                    //if (items == null || items.Count() <= 0)
                    //{
                    //    //var query = context.Database.SqlQuery<CoreAccount>("SELECT * FROM AspNetUsers where username = 'kanqing'");
                    //    ////CoreAccount admin = new CoreAccount() { UserName = "admin" };
                    //    ////丑陋的方法，强迫数据库初始化
                    //    //if (query != null)
                    //    //{
                    //    //    var temp = query.ToListAsync();
                    //    //    temp.Wait();
                    //    //    if (temp.Result.Count <= 0)
                    //    //    {
                    //    var manager = new UserManager<WXStudio.EFModel.Entities.Core.CoreAccount>( //ApplicationUser>(
                    //        new UserStore<WXStudio.EFModel.Entities.Core.CoreAccount>(
                    //            new WXPstudioDbContext()));

                    //    var adminUser = new WXStudio.EFModel.Entities.Core.CoreAccount();// ApplicationUser()
                    //    adminUser.UserName = "kanqing";
                    //    adminUser.Email = "hacky2000@163.com";
                    //    manager.Create(adminUser, "kq2014");
                    //    //}
                    //}
                }
                #endregion

                m_initRoot = true;
            }
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入电子邮件服务可发送电子邮件。
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入短信服务可发送短信。
            return Task.FromResult(0);
        }
    }
}
