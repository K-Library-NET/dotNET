using PStudio.WXPlatform.EFModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PStudio.WXPlatform.EFModel
{
    /// <summary>
    /// 微信平台数据上下文
    /// </summary>
    public class WXPlatformContext : DbContext
    {
        public WXPlatformContext(int orgId)
            : base(WXPlatformContext.GetConnectionStringByOrgId(orgId))
        {
            this.m_orgId = orgId;
        }

        public const string DEFAULT_CONTEXT_NAME = "name=WXDbContext";
        private int m_orgId;

        /// <summary>
        /// 组织ID
        /// </summary>
        public int OrgId
        {
            get { return m_orgId; }
            set { m_orgId = value; }
        }

        private static string GetConnectionStringByOrgId(int orgId)
        {
            using (SiteContext context = new SiteContext())
            {
                var result = from one in context.SiteOrganizations
                             where one.OrganizationId == orgId
                             select one.ConnectionString;

                if (result != null && result.Count() > 0)
                    return result.First();
            }

            return DEFAULT_CONTEXT_NAME;
        }

        /// <summary>
        /// 自定义菜单集合
        /// </summary>
        public DbSet<PersonalMenu> PersonalMenus { get; set; }

        /// <summary>
        /// 用户集合
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// 用户分组集合
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        /// 文章集合
        /// </summary>
        public DbSet<Article> Articles { get; set; }

        /// <summary>
        /// 文章分类集合
        /// </summary>
        public DbSet<ArtSort> ArtSorts { get; set; }

        /// <summary>
        /// 关键字集合
        /// </summary>
        public DbSet<KeyWord> KeyWords { get; set; }

        /// <summary>
        /// 用户消息集合
        /// </summary>
        public DbSet<UserMsg> UserMsgs { get; set; }
    }
}
