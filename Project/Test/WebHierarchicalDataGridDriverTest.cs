﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.WebForms.Infragistics;

namespace Test
{
    [TestClass]
    public class WebHierarchicalDataGridDriverTest
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
        public void TestWebHierarchicalDataGridDriver()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
            var textEditor = grid.GetDataField();

            //テキスト編集
            var cell01 = grid.GetCell(0, 1);
            cell01.Activate();
            textEditor.Edit("犬");
            Assert.AreEqual("犬", cell01.Text);

            IList<IWebElement> links = _driver.FindElements(By.Id("MainContent_UpdateButton"));
            links.First(element => element != null).Click();
        }

        [TestMethod]
        public void Expand()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
            var childGrid = grid.GetRowIslands(0, 0, 0);

            childGrid.SetExpanded();

        }

        [TestMethod]
        public void TestWebHierarchicalDataGridDriverChild()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");

            var childGrid = grid.GetRowIslands(0, 0, 0);
            var childTextEditor = childGrid.GetDataField();
            var childCheckEditor = childGrid.GetCheckBoxField();

            var childcell07 = childGrid.GetCell(0, 7);
            childcell07.Activate();
            childTextEditor.Edit("CDE");
            Assert.AreEqual("CDE", childcell07.Text);
            childTextEditor.Edit("X");
            Assert.AreEqual("X", childcell07.Text);
            childTextEditor.Edit("Y");
            Assert.AreEqual("Y", childcell07.Text);

            //チェックボックス編集
            var cell03 = childGrid.GetCell(0, 3);
            cell03.Activate();
            childCheckEditor.Edit(true);
            Assert.AreEqual(true, cell03.Value);
            childCheckEditor.Edit(false);
            Assert.AreEqual(false, cell03.Value);

            //コンボ編集
            var childDropEditor = childGrid.GetDropDownProvider("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGrid_DropDownProvider");
            childGrid.GetCell(0, 0).Activate();
            childDropEditor.Edit(1);
            Assert.AreEqual("100", childGrid.GetCell(0, 0).Text);

            childDropEditor.Edit("10");
            Assert.AreEqual("10", childGrid.GetCell(0, 0).Text);

            var cell10 = childGrid.GetCell(1, 0);
            cell10.Activate();
            childDropEditor.Edit("10");
            Assert.AreEqual("10", cell10.Text);
        }

    }
}
