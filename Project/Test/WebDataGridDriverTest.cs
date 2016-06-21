using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using Selenium.WebForms.Infragistics;

namespace Test
{
    [TestClass]
    public class WebDataGridDriverTest
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
        public void TestColumnCount()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            grid.ColumnCount.Is(8);
        }

        [TestMethod]
        public void TestGetActiveCellElement()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            grid.GetCell(1, 1).Element.Click();
            var element = grid.GetActiveCellElement();
            grid.GetCell(0, 0).Element.Click();
            var element2 = grid.GetActiveCellElement();
            element.Text.IsNot(element2.Text);
        }

        [TestMethod]
        public void TestGetCells()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            Assert.AreEqual("Matsui", grid.GetCells(0).ElementAt(1).Text);
        }


        [TestMethod]
        public void TestWebDataGridColmnDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            Assert.AreEqual("LastName", grid.GetColumn(1).Text);
        }

        [TestMethod]
        public void TestWebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");

            //Combo
            grid.GetCell(0, 0).Edit("100");
            Assert.AreEqual("100", grid.GetCell(0, 0).Text);
            Assert.AreEqual((long)100, grid.GetCell(0, 0).Value);

            var cell10 = grid.GetCell(1, 0);
            grid.GetCell(1, 0).Edit("10");
            Assert.AreEqual("10", cell10.Text);

            //Text
            var cell11 = grid.GetCell(1, 1);
            grid.GetCell(1, 1).Edit("abc");
            Assert.AreEqual("abc", cell11.Text);

            //Checkbox
            var cell12 = grid.GetCell(1, 2);
            grid.GetCell(1, 2).Edit(true);
            Assert.AreEqual(true, cell12.Value);
        }
        [TestMethod]
        public void PageWebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            Assert.AreEqual(grid.GetPageIndex(), 1);
            grid.SetPageIndex(2);
            Assert.AreEqual(grid.GetPageIndex(), 2);
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