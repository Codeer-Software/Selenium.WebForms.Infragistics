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
#if DEBUG
            _driver.Url = "http://localhost:7570/";
#else
            _driver.Url = "http://seleniumwebformsinfragistics.azurewebsites.net/";
#endif
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
            _webDataGrid.GetBehaviors().Sort("Id", WebDataGridBehaviorsDriver.SortType.Descending);
        }

        [TestMethod]
        public void FilterWebDataGridDriver()
        {
            _webDataGrid.GetBehaviors().Filter("LastName", "Ishikawa");
        }

        [TestMethod]
        public void PageWebDataGridDriver()
        {
            _webDataGrid.GetBehaviors().Page(2);
        }

        [TestMethod]
        public void HiddenWebDataGridDriver()
        {
            _webDataGrid.GetBehaviors().Hidden(2);
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
            //var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
            //Open Waiting
            //grid.GetBehaviors().Page(2);
            //var childGrid = grid.GetRowIslands(0, 0, 0);
            //childGrid.GetBehaviors().Page(2);
        }
    }
}