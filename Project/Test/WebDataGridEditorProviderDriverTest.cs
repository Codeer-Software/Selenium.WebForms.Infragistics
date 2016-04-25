using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using Selenium.WebForms.Infragistics;

namespace Test
{
    [TestClass]
    public class WebDataGridEditorProviderDriverTest
    {
        private static RemoteWebDriver _driver;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = BrowserUtil.GetDriver(BrowserUtil.Browser.Chrome);
            _driver.Url = TestCommon.TargetUrl;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void EditorProviderWebHierarchicalDataGridChild2()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
            var childGrid = grid.GetRowIslands(0, 0, 0);
            childGrid.SetExpanded(true);
        }

        [TestMethod]
        public void EditorProviderWebHierarchicalDataGridChild()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
            var childGrid = grid.GetRowIslands(0, 0, 0);
            childGrid.SetExpanded(true);

            //DropDownProvider
            childGrid.GetCell(0, 0).Edit("100");
            Assert.AreEqual("100", childGrid.GetCell(0, 0).Text);

            //TextEditorProvider
            childGrid.GetCell(0, 2).Edit("200");
            Assert.AreEqual("200", childGrid.GetCell(0, 2).Text);

            //DateTimeEditorProvider
            childGrid.GetCell(0, 4).Edit("10/28/1976"); //<- Azure 
            Assert.AreEqual("10/28/1976", childGrid.GetCell(0, 4).Text);

            //DatePickerProvider
            childGrid.GetCell(0, 5).Edit("10/28/1976"); 
            Assert.AreEqual("10/28/1976", childGrid.GetCell(0, 5).Text);

            //NumericEditorProvider
            childGrid.GetCell(0, 6).Edit("100");
            Assert.AreEqual("100", childGrid.GetCell(0, 6).Text);

            //TextBoxProvider
            childGrid.GetCell(0, 7).Edit("abc");
            Assert.AreEqual("abc", childGrid.GetCell(0, 7).Text);
        }

        [TestMethod]
        public void EditorProviderWebDataGridDriver4Attack()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");

            grid.GetCell(0, 0).Edit("Dog");

            grid.GetCell(0, 1).Edit("Dog");

            grid.GetCell(1, 1).Edit("Dog");

            grid.GetCell(0, 0).Edit("Dog");

            var grid2 = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            grid2.GetCell(0, 0).Edit("abc");

            grid2.GetCell(1, 0).Edit("abc");

            grid2.GetCell(1, 1).Edit("abc");

            grid2.GetCell(1, 0).Edit("abc");
        }

        [TestMethod]
        public void EditorProviderWebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");

            //DropDownProvider
            grid.GetCell(0, 0).Edit("100");
            Assert.AreEqual("100", grid.GetCell(0, 0).Text);

            //TextBoxProvider
            grid.GetCell(0, 3).Edit("abc");
            Assert.AreEqual("abc", grid.GetCell(0, 3).Text);

            //TextEditorProvider
            grid.GetCell(0, 4).Edit("200");
            Assert.AreEqual("200", grid.GetCell(0, 4).Text);

            //DateTimeEditorProvider

            grid.GetCell(0, 5).Edit("10/28/1976");
            Assert.AreEqual("10/28/1976", grid.GetCell(0, 5).Text);

            //DatePickerProvider

            grid.GetCell(0, 6).Edit("10/28/1976");
            Assert.AreEqual("10/28/1976", grid.GetCell(0, 6).Text);

            //NumericEditorProvider

            grid.GetCell(0, 7).Edit("100");
            Assert.AreEqual("100", grid.GetCell(0, 7).Text);
        }
    }
}