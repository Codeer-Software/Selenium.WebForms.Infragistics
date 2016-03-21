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
            _driver = BrowserUtil.GetDriver(BrowserUtil.Browser.Firefox);
            _driver.Url = "http://seleniumwebformsinfragistics.azurewebsites.net/";
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void TestWebDataGridColmnDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            Assert.AreEqual("苗字", grid.GetColumn(1).Text);
        }

        [TestMethod]
        public void TestWebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            var dropEditor = grid.GetEditorProvider();
            var textEditor = grid.GetDataField();
            var checkEditor = grid.GetCheckBoxField();

            //Combo
            grid.GetCell(0, 0).Activate();
            dropEditor.Edit("100");
            Assert.AreEqual("100", grid.GetCell(0, 0).Text);
            Assert.AreEqual((long)100, grid.GetCell(0, 0).Value);

            var cell10 = grid.GetCell(1, 0);
            cell10.Activate();
            dropEditor.Edit("10");
            Assert.AreEqual("10", cell10.Text);

            //Text
            var cell11 = grid.GetCell(1, 1);
            cell11.Activate();
            textEditor.Edit("abc");
            Assert.AreEqual("abc", cell11.Text);

            //Checkbox
            var cell12 = grid.GetCell(1, 2);
            cell12.Activate();
            checkEditor.Edit(true);
            Assert.AreEqual(true, cell12.Value);
        }
    }
}