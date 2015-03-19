using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PStudio.WXPlatform.EFModel;

namespace UnitTestEFModel
{
    [TestClass]
    public class TestSiteContext
    {
        private SiteContext m_context = null;

        [TestInitialize()]
        public void Initialize()
        {
            m_context = new SiteContext();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            m_context.Dispose();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var org = this.m_context.SiteOrganizations.Create();
            org.SiteName = "test site 1";
            org.SiteDescription = "自己测试";
            org.ConnectionString = "server=localhost;user id=root;password=root;persistsecurityinfo=True;database=test";
            m_context.SiteOrganizations.Add(org);
            m_context.SaveChanges();

            var result = m_context.SiteOrganizations.Where(
                (organization => organization.SiteName == "test site 1")
                );

            if (result != null && result.Count() > 0)
            {
                foreach (var one in result)
                {
                    one.ConnectionString = "http://localhost:test/";
                }
                m_context.SaveChanges();
            }
            else
            {
                Assert.Fail("新增方法有问题。");
            }

            var finded2 = from one1 in m_context.SiteOrganizations
                          where one1.SiteDescription == "自己测试" && one1.ConnectionString == "http://localhost:test/" //这绝对不是一个网址，用来测试而已
                          select one1;

            if (finded2 != null && finded2.Count() > 0)
            {
            }
            else
            {
                Assert.Fail("修改方法有问题。");
            }

            foreach (var one2 in finded2)
            {
                m_context.SiteOrganizations.Remove(one2);
            }
            m_context.SaveChanges();

            var finded3 = from one2 in m_context.SiteOrganizations
                          where one2.SiteDescription == "自己测试"
                          select one2;

            if (finded3 != null && finded3.Count() > 0)
            {
                Assert.Fail("删除方法有问题。");
            }
        }
    }
}
