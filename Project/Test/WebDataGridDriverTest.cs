﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void TestWebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            var dropEditor = grid.GetDropDownProvider("MainContent__webDataGrid__webDataGridDropDownProvider");
            var textEditor = grid.GetDataField();
            var checkEditor = grid.GetCheckBoxField();

            //Combo
            grid.GetCell(0, 0).Activate();
            dropEditor.Edit(1);
            Assert.AreEqual("100", grid.GetCell(0, 0).Text);
            Assert.AreEqual((long)100, grid.GetCell(0, 0).Value);

            dropEditor.Edit(0);
            Assert.AreEqual("10", grid.GetCell(0, 0).Text);

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