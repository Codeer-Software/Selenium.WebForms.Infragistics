using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.WebForms.Infragistics;

namespace Test
{
    [TestClass]
    public class WebDataGridBehaviorsDriverTest
    {
        private static RemoteWebDriver _driver;

        [TestInitialize]
        public void TestInitialize()
        {
#if DEBUG
            _driver = BrowserUtil.GetDriver(BrowserUtil.Browser.Firefox);
            _driver.Url = "http://localhost:7570/";
#else
            _driver = BrowserUtil.GetDriver();
            _driver.Url = "http://infragisticswebformssample.azurewebsites.net/";
#endif
            BrowserUtil.IsTitle("InfragisticsWebFormsSample");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void ソートWebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            grid.GetBehaviors().Sort("Id", WebDataGridBehaviorsDriver.SortType.Descending);
        }

        [TestMethod]
        public void フィルタWebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            grid.GetBehaviors().Filter("LastName", "石川");
        }
        [TestMethod]
        public void ページWebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            grid.GetBehaviors().Page(2);
        }

        [TestMethod]
        public void 非表示WebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            grid.GetBehaviors().Hidden(2);
        }

        [TestMethod]
        public void 固定WebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            grid.GetBehaviors().Fix(1);

            grid.GetBehaviors().Fix(1, false);
        }
        [TestMethod]
        public void ページWebHierarchicalDataGrid親子()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
            grid.GetBehaviors().Page(2);

            //子供の操作をするときに自分で開く必要がある。
            //var childGrid = grid.GetRowIslands(0, 0, 0);
            //childGrid.GetBehaviors().Page(2);
        }
    }
}
