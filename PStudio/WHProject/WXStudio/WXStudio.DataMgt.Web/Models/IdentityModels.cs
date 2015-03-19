using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WXStudio.DataMgt.Web.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    //public class ApplicationUser : IdentityUser
    //{
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // 在此处添加自定义用户声明
    //        return userIdentity;
    //    }
    //}

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
            
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        var context = new ApplicationDbContext();

    //        using (WXStudio.EFModel.Entities.WXPstudioDbContext db = new EFModel.Entities.WXPstudioDbContext())
    //        {
    //            var result = db.CoreConfigs.ToList();
    //            //确保数据库先生成 20140921
    //        }

    //        //var manager = new UserManager<ApplicationUser>(
    //        //    new UserStore<ApplicationUser>(
    //        //        new ApplicationDbContext()));
    //        //if (manager.Find("admin", "admin123") == null)
    //        //{
    //        //    var adminUser = new ApplicationUser()
    //        //    {
    //        //        UserName = "admin",
    //        //        Email = "hacky2000@163.com",
    //        //    };
    //        //    manager.Create(adminUser, "admin123");
    //        //}

    //        return context;
    //    }
    //}
}