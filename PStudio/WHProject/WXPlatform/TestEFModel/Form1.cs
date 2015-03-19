using PStudio.WXPlatform.EFModel;
using PStudio.WXPlatform.EFModel.AdminModels;
using PStudio.WXPlatform.EFModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestEFModel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SiteContext context = new SiteContext();
            var org = context.SiteOrganizations.Create();
            org.SiteName = "test site 1";
            org.SiteDescription = "自己测试";
            org.ConnectionString = "server=localhost;user id=root;password=root;persistsecurityinfo=True;database=test";
            context.SiteOrganizations.Add(org);
            context.SaveChanges();

            var result = context.SiteOrganizations.Where(
                (organization => organization.SiteName == "test site 1")
                );

            if (result != null && result.Count() > 0)
            {
                foreach (var one in result)
                    context.SiteOrganizations.Remove(one);
            }

            context.SaveChanges();

            var finded2 = from one1 in context.SiteOrganizations
                          where one1.ConnectionString == "http://localhost:3306/"
                          select one1;

            if (finded2 != null && finded2.Count() > 0)
            {
                foreach (var one2 in finded2)
                {
                    one2.SiteDescription = DateTime.Now.ToLongTimeString();
                }

                context.SaveChanges();
            }

            System.Console.WriteLine("OK. ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WXPlatformContext m_context = new WXPlatformContext(1);

            var pmenu = m_context.PersonalMenus.Create();

            pmenu.OrgId = m_context.OrgId;
            pmenu.Name = "test 111";

            m_context.PersonalMenus.Add(pmenu);
            m_context.SaveChanges();




            //var keyword = m_context.KeyWords.Create();

            //keyword.Name = "test1";
            //m_context.KeyWords.Add(keyword);
            //m_context.SaveChanges();

            //m_context.KeyWords.Remove(keyword);
            //m_context.SaveChanges();

            //var vr = m_context.KeyWords.ToArray();

            //foreach (var v in vr)
            //{
            //    Console.Write("v");
            //}

            m_context.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WXPlatformContext m_context = new WXPlatformContext(1);

            KeyWord keyword = m_context.KeyWords.Create();

            keyword.KeyContent = new PStudio.WXPlatform.EFModel.Models.KeyContent();

            keyword.Name = "Test keyword";
            keyword.Note = "Test Note";

            keyword.KeyContent.Content = "Test KeyContent";

            m_context.KeyWords.Add(keyword);
            m_context.SaveChanges();

            m_context.KeyWords.Remove(keyword);
            m_context.SaveChanges();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WXPlatformContext m_context = new WXPlatformContext(1);
            Article article = m_context.Articles.Create();
            article.AOrder = 98;
            article.Title = "test title";
            article.Content = "content text";
            article.ITop = 1;
            m_context.Articles.Add(article);
            m_context.SaveChanges();

            ArtSort sort = m_context.ArtSorts.Create();

            sort.IndexLevel = 1;
            sort.Name = "level1";
            sort.SOrder = 3;

            m_context.ArtSorts.Add(sort);
            m_context.SaveChanges();

            m_context.ArtSorts.Add(new ArtSort() { IndexLevel = 2, Name = "level2", ParentId = sort.ArtSortId });

            article.ArtSort = sort;
            article.ArtSortId = sort.ArtSortId;
            article.ITop = 0;
            m_context.SaveChanges();

            m_context.Entry<ArtSort>(sort).Collection(p => p.Articles).Load();

            Article temp = sort.Articles.First();

            m_context.ArtSorts.Remove(sort);//.Articles.Remove(article);
            m_context.SaveChanges();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WXPlatformContext m_context = new WXPlatformContext(1);

            UserMsg msg = m_context.UserMsgs.Create();

            msg.WeiMsgId = Guid.NewGuid().ToString();
            msg.OrgId = m_context.OrgId;
            m_context.UserMsgs.Add(msg);
            m_context.SaveChanges();

            m_context.Entry<UserMsg>(msg).Collection(m => m.ReUserMsgs).Load();

            ReUserMsg reMsg = new ReUserMsg(){ UserMsgId = msg.UserMsgId, UserMsg = msg, ReContent = "recontent"};
            msg.ReUserMsgs.Add(reMsg);
            m_context.SaveChanges();

            m_context.UserMsgs.Remove(msg);
            m_context.SaveChanges();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WXPlatformContext m_context = new WXPlatformContext(1);

            User user = m_context.Users.Create();
            user.UserData = new UserData();
            user.OpenId = Guid.NewGuid().ToString();
            m_context.Users.Add(user);
            m_context.SaveChanges();

            user.Group = new Group() { Name = "group1", Note = "testNote" };
            m_context.SaveChanges();

            m_context.Groups.Remove(user.Group);
            m_context.SaveChanges();

            //m_context.Users.Remove(user);
            m_context.SaveChanges();
        }
    }
}
