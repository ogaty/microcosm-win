using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using microcosm.DB;
using System.IO;
using System.Windows.Controls;
using System.Windows.Threading;

namespace microcosmtest
{
    /// <summary>
    /// DatabaseWindow周りのテスト
    /// </summary>
    [TestClass]
    public class DbTest
    {
        public DbTest()
        {
        }

        [TestMethod]
        public void TestCreateDirectoryInfo()
        {
            DBTree tree = new DBTree();
            DirectoryInfo d = new DirectoryInfo(@"data");
            TreeViewItem item;
            item = tree.CreateDirectoryNode(d);
            TreeViewItem subItem = (TreeViewItem)item.Items[0];
            DbItem value = (DbItem)subItem.Tag;
            string filename = Directory.GetCurrentDirectory() + @"\data\test.csm";
            Assert.AreEqual(filename, value.fileName);
            Assert.AreEqual(false, value.isDir);

        }

        [TestMethod]
        public void TestUserDataFromFile()
        {
            DbItem item = new DbItem();
            item.fileName = Directory.GetCurrentDirectory() + @"\data\test.csm";
            UserData udata = item.getUserdata();
            Assert.AreEqual("testuser", udata.name);
        }

    }
}
