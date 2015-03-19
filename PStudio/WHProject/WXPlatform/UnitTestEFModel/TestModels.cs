using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PStudio.WXPlatform.EFModel;

namespace UnitTestEFModel
{
    /// <summary>
    /// TestModels 的摘要说明
    /// </summary>
    [TestClass]
    public class TestModels
    {
        public TestModels()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private WXPlatformContext m_context = null;

        [TestInitialize()]
        public void Initialize()
        {
            m_context = new WXPlatformContext(1);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            m_context.Dispose();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        /// 测试文章类的对象CRUD
        /// </summary>
        [TestMethod]
        public void TestArticles()
        {
            //
            // TODO:  在此处添加测试逻辑
            //
        }

        /// <summary>
        /// 测试关键字类的对象CRUD
        /// </summary>
        [TestMethod]
        public void TestKeyWords()
        {
            //
            // TODO:  在此处添加测试逻辑
            //
        }

        /// <summary>
        /// 测试用户类的对象CRUD
        /// </summary>
        [TestMethod]
        public void TestUsers()
        {
            //
            // TODO:  在此处添加测试逻辑
            //
        }

        /// <summary>
        /// 测试消息记录类的对象CRUD
        /// </summary>
        [TestMethod]
        public void TestMsgs()
        {
            //
            // TODO:  在此处添加测试逻辑
            //
        }

        /// <summary>
        /// 测试自定义菜单类的对象CRUD
        /// </summary>
        [TestMethod]
        public void TestPMenus()
        { 
            var pmenu = m_context.PersonalMenus.Create();

            pmenu.OrgId = m_context.OrgId;
            pmenu.Name = "自己测试";

            m_context.PersonalMenus.Add(pmenu);
            m_context.SaveChanges();

            var result = m_context.PersonalMenus.Where(
                (menu => menu.Name == "自己测试")
                );

            if (result != null && result.Count() > 0)
            {
                foreach (var one in result)
                {
                    one.Note = "http://localhost:test/";
                }
                m_context.SaveChanges();
            }
            else
            {
                Assert.Fail("新增方法有问题。");
            }

            var finded2 = from one1 in m_context.PersonalMenus
                          where one1.Name == "自己测试" && one1.Note == "http://localhost:test/" //这绝对不是一个网址，用来测试而已
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
                m_context.PersonalMenus.Remove(one2);
            }
            m_context.SaveChanges();

            var finded3 = from one2 in m_context.PersonalMenus
                          where one2.Name == "自己测试"
                          select one2;

            if (finded3 != null && finded3.Count() > 0)
            {
                Assert.Fail("删除方法有问题。");
            }
        }
    }
}
