using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            _driver.Url = "http://seleniumwebformsinfragistics.azurewebsites.net/";
            _webDataGrid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void SortWebDataGridDriver()
        {

            var a = _webDataGrid.GetBehaviors().GetSort("Id");
            _webDataGrid.GetBehaviors().Sort("Id", WebDataGridBehaviorsDriver.SortType.Descending);
            _webDataGrid.GetCell(0, 0).WaitForActive();
            var b = _webDataGrid.GetBehaviors().GetSort("Id");


        }

        [TestMethod]
        public void FilterWebDataGridDriver()
        {
            _webDataGrid.GetBehaviors().Filter("LastName", "Ishikawa");
        }

        [TestMethod]
        public void PageWebDataGridDriver()
        {
            Assert.AreEqual(_webDataGrid.GetBehaviors().GetPage(), 1);
            _webDataGrid.GetBehaviors().Page(2);
            Assert.AreEqual(_webDataGrid.GetBehaviors().GetPage(), 2);
        }

        [TestMethod]
        public void HiddenWebDataGridDriver()
        {
            Assert.AreEqual(_webDataGrid.GetBehaviors().GetHidden(2), false);
            _webDataGrid.GetBehaviors().Hidden(2);
            Assert.AreEqual(_webDataGrid.GetBehaviors().GetHidden(2), true);
        }

        [TestMethod]
        public void FixWebDataGridDriver()
        {
            _webDataGrid.GetBehaviors().Fix(1);
            _webDataGrid.GetBehaviors().Fix(1, false);
        }

        [TestMethod]
        public void PageWebHierarchicalDataGrid()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
            //grid.GetBehaviors().Page(2);
            //ToDo Need Waiting

            var childGrid = grid.GetRowIslands(0, 0, 0);
            childGrid.SetExpanded();
            Assert.AreEqual(childGrid.GetBehaviors().GetPage(), 1);
            childGrid.GetBehaviors().Page(2);
            Assert.AreEqual(childGrid.GetBehaviors().GetPage(), 2);
        }
    }
}