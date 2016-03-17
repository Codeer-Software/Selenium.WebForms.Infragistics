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
        public void EditorProviderWebHierarchicalDataGridChild()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
            var childGrid = grid.GetRowIslands(0, 0, 0);
            childGrid.SetExpanded();

            //ToDo EditorProvider is All Name Change.

            Thread.Sleep(10000);

            //DropDownProvider
            var dropEditor = childGrid.GetEditorProvider("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGrid_DropDownProvider");
            childGrid.GetCell(0, 0).Activate();
            dropEditor.Edit("100");
            Assert.AreEqual("100", childGrid.GetCell(0, 0).Text);

            //TextEditorProvider
            var textEditor = childGrid.GetEditorProvider("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGridTextEditorProvider");
            childGrid.GetCell(0, 2).Activate();
            textEditor.Edit("200");
            Assert.AreEqual("200", childGrid.GetCell(0, 2).Text);

            //DateTimeEditorProvider
            var datepick1 = childGrid.GetEditorProvider("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGridDateTimeEditorProvider");
            childGrid.GetCell(0, 4).Activate();
            datepick1.Edit("1976/10/28");
            Assert.AreEqual("1976/10/28", childGrid.GetCell(0, 4).Text);

            //DatePickerProvider
            var datepick2 = childGrid.GetEditorProvider("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGridDatePickerProvider");
            childGrid.GetCell(0, 5).Activate();
            datepick2.Edit("1976/10/28");
            Assert.AreEqual("1976/10/28", childGrid.GetCell(0, 5).Text);

            //NumericEditorProvider
            var numEditor1 = childGrid.GetEditorProvider("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGridNumericEditorProvider");
            childGrid.GetCell(0, 6).Activate();
            numEditor1.Edit("100");
            Assert.AreEqual("100", childGrid.GetCell(0, 6).Text);

            //TextBoxProvider
            var textBox = childGrid.GetEditorProvider("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGridTextBoxProvider");
            childGrid.GetCell(0, 7).Activate();
            textBox.Edit("abc");
            Assert.AreEqual("abc", childGrid.GetCell(0, 7).Text);
        }

        [TestMethod]
        public void EditorProviderWebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");

            //DropDownProvider
            var dropEditor = grid.GetEditorProvider("MainContent__webDataGrid__webDataGridDropDownProvider");
            grid.GetCell(0, 0).Activate();
            dropEditor.Edit("100");
            Assert.AreEqual("100", grid.GetCell(0, 0).Text);

            //TextBoxProvider
            var textBox = grid.GetEditorProvider("MainContent__webDataGrid__webDataGridTextBoxProvider");
            grid.GetCell(0, 3).Activate();
            textBox.Edit("abc");
            Assert.AreEqual("abc", grid.GetCell(0, 3).Text);

            //TextEditorProvider
            var textEditor = grid.GetEditorProvider("MainContent__webDataGrid__webDataGrid_TextEditorProvider1");
            grid.GetCell(0, 4).Activate();
            textEditor.Edit("200");
            Assert.AreEqual("200", grid.GetCell(0, 4).Text);

            //DateTimeEditorProvider
            var datepick1 = grid.GetEditorProvider("MainContent__webDataGrid__webDataGrid_DateTimeEditorProvider1");
            grid.GetCell(0, 5).Activate();
            datepick1.Edit("1976/10/28");
            Assert.AreEqual("1976/10/28", grid.GetCell(0, 5).Text);

            //DatePickerProvider
            var datepick2 = grid.GetEditorProvider("MainContent__webDataGrid__webDataGrid_DatePickerProvider1");
            grid.GetCell(0, 6).Activate();
            datepick2.Edit("1976/10/28");
            Assert.AreEqual("1976/10/28", grid.GetCell(0, 6).Text);

            //NumericEditorProvider
            var numEditor1 = grid.GetEditorProvider("MainContent__webDataGrid__webDataGrid_NumericEditorProvider1");
            grid.GetCell(0, 7).Activate();
            numEditor1.Edit("100");
            Assert.AreEqual("100", grid.GetCell(0, 7).Text);
        }


    }
}