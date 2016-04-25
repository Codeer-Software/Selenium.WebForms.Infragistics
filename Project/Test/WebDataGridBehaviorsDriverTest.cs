using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using Selenium.WebForms.Infragistics;

namespace Test
{
    [TestClass]
    public class WebDataGridBehaviorsDriverTest
    {
        private static RemoteWebDriver _driver;

        private WebDataGridDriver _webDataGrid;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = BrowserUtil.GetDriver(BrowserUtil.Browser.Firefox);
            _driver.Url = TestCommon.TargetUrl;
            _webDataGrid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void ReadOnlyWebDataGridDriver()
        {
            _webDataGrid.IsReadOnly(0).IsFalse();
        }

        [TestMethod]
        public void FilterWebDataGridDriver()
        {
            _webDataGrid.SetFilter("LastName", 1, "Ishikawa");
        }

        [TestMethod]
        public void PageWebDataGridDriver()
        {
            Assert.AreEqual(_webDataGrid.GetPageIndex(), 1);
            _webDataGrid.SetPageIndex(2);
            Assert.AreEqual(_webDataGrid.GetPageIndex(), 2);
        }

        [TestMethod]
        public void HiddenWebDataGridDriver()
        {
            Assert.AreEqual(_webDataGrid.IsHidden(2), false);
            _webDataGrid.SetHidden(2, true);
            Assert.AreEqual(_webDataGrid.IsHidden(2), true);
        }

        [TestMethod]
        public void FixWebDataGridDriver()
        {
            _webDataGrid.SetFixed(1);
            _webDataGrid.SetFixed(1, false);
        }

        [TestMethod]
        public void PageWebHierarchicalDataGrid()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
            var childGrid = grid.GetRowIslands(0, 0, 0);
            childGrid.SetExpanded(true);
            Assert.AreEqual(childGrid.GetPageIndex(), 1);
            childGrid.SetPageIndex(2);
            Assert.AreEqual(childGrid.GetPageIndex(), 2);
        }
    }
}