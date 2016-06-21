using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.WebForms.Infragistics;

namespace Test
{
    [TestClass]
    public class WebHierarchicalDataGridDriverTest
    {
        private static RemoteWebDriver _driver;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = BrowserUtil.GetDriver(BrowserUtil.Browser.Ie);
            _driver.Url = TestCommon.TargetUrl;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void TestWebHierarchicalDataGridDriver()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");

            var cell01 = grid.GetCell(0, 1);

            cell01.Edit("Dog");
            Assert.AreEqual("Dog", cell01.Text);

            IList<IWebElement> links = _driver.FindElements(By.Id("MainContent_UpdateButton"));
            links.First(element => element != null).Click();
        }

        [TestMethod]
        public void TestWebHierarchicalDataGridDriverChild()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");

            var childGrid = grid.GetRowIslands(0, 0, 0);
            childGrid.SetExpanded(true);

            var childcell06 = childGrid.GetCell(0, 6);
            childcell06.Edit("CDE");
            Assert.AreEqual("CDE", childcell06.Text);
            childcell06.Edit("X");
            Assert.AreEqual("X", childcell06.Text);
            childcell06.Edit("Y");
            Assert.AreEqual("Y", childcell06.Text);

            //Checkbox
            var cell02 = childGrid.GetCell(0, 2);
            cell02.Edit(true);
            Assert.AreEqual(true, cell02.Value);
            cell02.Edit(false);
            Assert.AreEqual(false, cell02.Value);

            //Combobox
            childGrid.GetCell(0, 0).Activate();
            childGrid.GetCell(0, 0).Edit("10");
            Assert.AreEqual("10", childGrid.GetCell(0, 0).Text);
            var cell10 = childGrid.GetCell(1, 0);
            childGrid.GetCell(1, 0).Edit("10");
            Assert.AreEqual("10", cell10.Text);
        }

        [TestMethod]
        public void TestWebHierarchicalDataGridDriverChildWAttack()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
            var childGrid = grid.GetRowIslands(0, 0, 0);
            childGrid.SetExpanded(true);
            childGrid.GetCell(0, 0).Edit("100");

            var childGrid1 = grid.GetRowIslands(1, 0, 0);
            childGrid1.SetExpanded(true);
            childGrid1.GetCell(0, 0).Edit("100");

            childGrid1.GetCell(0, 0).Edit("200");
        }
    }
}