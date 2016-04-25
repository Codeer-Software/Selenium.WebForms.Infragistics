using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using Selenium.WebForms.Infragistics;

namespace Test
{
    [TestClass]
    public class WebDataGridColumnDriverTest
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
            _webDataGrid.GetColumn(0).IsReadOnly().IsFalse();
        }

        [TestMethod]
        public void FilterWebDataGridDriver()
        {
            _webDataGrid.GetColumn(1).SetFilter(1, "Ishikawa");
        }


        [TestMethod]
        public void HiddenWebDataGridDriver()
        {
            Assert.AreEqual(_webDataGrid.GetColumn(2).IsHidden(), false);
            _webDataGrid.GetColumn(2).SetHidden(true);
            Assert.AreEqual(_webDataGrid.GetColumn(2).IsHidden(), true);
        }

        [TestMethod]
        public void FixWebDataGridDriver()
        {
            _webDataGrid.GetColumn(1).SetFixed();
            _webDataGrid.GetColumn(1).SetFixed(false);
        }
    }
}