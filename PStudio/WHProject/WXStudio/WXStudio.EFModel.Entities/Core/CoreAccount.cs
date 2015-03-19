using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WXStudio.EFModel.Entities.Core
{

    [Table("tb_core_account")]
    public class CoreAccount : IdentityUser
    {
        public int CoreAccountId
        {
            get;
            set;
        }

        public int CompanyId
        {
            get;
            set;
        }

        [Display(Name = "昵称")]
        public string Nickname
        {
            get;
            set;
        }

        [Display(Name = "账号")]
        public string Account
        {
            get;
            set;
        }

        [Display(Name = "密码")]
        public string Password
        {
            get;
            set;
        }

        [Display(Name = "备注")]
        public string Comments
        {
            get;
            set;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<CoreAccount> manager) 
            //ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }
}
