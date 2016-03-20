using System.Threading;
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
#if DEBUG
            _driver.Url = "http://localhost:7570/";
#else
            _driver.Url = "http://seleniumwebformsinfragistics.azurewebsites.net/";
#endif
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
            childGrid.SetExpanded();

            var childGrid1 = childGrid.GetRowIslands(0, 1, 0);
            childGrid1.SetExpanded();
        }



        [TestMethod]
        public void EditorProviderWebHierarchicalDataGridChild()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
            var childGrid = grid.GetRowIslands(0, 0, 0);
            childGrid.SetExpanded();

            //DropDownProvider
            var dropEditor = childGrid.GetEditorProvider();
            childGrid.GetCell(0, 0).Activate();
            dropEditor.Edit("100");
            Assert.AreEqual("100", childGrid.GetCell(0, 0).Text);

            //TextEditorProvider
            var textEditor = childGrid.GetEditorProvider();
            childGrid.GetCell(0, 2).Activate();
            textEditor.Edit("200");
            Assert.AreEqual("200", childGrid.GetCell(0, 2).Text);

            //DateTimeEditorProvider
            var datepick1 = childGrid.GetEditorProvider();
            childGrid.GetCell(0, 4).Activate();
            datepick1.Edit("1976/10/28");
            Assert.AreEqual("1976/10/28", childGrid.GetCell(0, 4).Text);

            //DatePickerProvider
            var datepick2 = childGrid.GetEditorProvider();
            childGrid.GetCell(0, 5).Activate();
            datepick2.Edit("1976/10/28");
            Assert.AreEqual("1976/10/28", childGrid.GetCell(0, 5).Text);

            //NumericEditorProvider
            var numEditor1 = childGrid.GetEditorProvider();
            childGrid.GetCell(0, 6).Activate();
            numEditor1.Edit("100");
            Assert.AreEqual("100", childGrid.GetCell(0, 6).Text);

            //TextBoxProvider
            var textBox = childGrid.GetEditorProvider();
            childGrid.GetCell(0, 7).Activate();
            textBox.Edit("abc");
            Assert.AreEqual("abc", childGrid.GetCell(0, 7).Text);
        }

        [TestMethod]
        public void EditorProviderWebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");

            //DropDownProvider
            var dropEditor = grid.GetEditorProvider();
            grid.GetCell(0, 0).Activate();
            dropEditor.Edit("100");
            Assert.AreEqual("100", grid.GetCell(0, 0).Text);

            //TextBoxProvider
            var textBox = grid.GetEditorProvider();
            grid.GetCell(0, 3).Activate();
            textBox.Edit("abc");
            Assert.AreEqual("abc", grid.GetCell(0, 3).Text);

            //TextEditorProvider
            var textEditor = grid.GetEditorProvider();
            grid.GetCell(0, 4).Activate();
            textEditor.Edit("200");
            Assert.AreEqual("200", grid.GetCell(0, 4).Text);

            //DateTimeEditorProvider
            var datepick1 = grid.GetEditorProvider();
            grid.GetCell(0, 5).Activate();
            datepick1.Edit("1976/10/28");
            Assert.AreEqual("1976/10/28", grid.GetCell(0, 5).Text);

            //DatePickerProvider
            var datepick2 = grid.GetEditorProvider();
            grid.GetCell(0, 6).Activate();
            datepick2.Edit("1976/10/28");
            Assert.AreEqual("1976/10/28", grid.GetCell(0, 6).Text);

            //NumericEditorProvider
            var numEditor1 = grid.GetEditorProvider();
            grid.GetCell(0, 7).Activate();
            numEditor1.Edit("100");
            Assert.AreEqual("100", grid.GetCell(0, 7).Text);
        }


    }
}