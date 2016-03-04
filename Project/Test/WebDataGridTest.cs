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
    public class WebDataGridTest
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
            grid.GetBehaviors().Sort("Id", WebDataGridBehaviors.SortType.Descending);

            //grid.GetCell(0, 0).WaitActive();
            //Assert.AreEqual("3", grid.GetCell(0, 0).Text);
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


        [TestMethod]
        public void WTest()
        {
            TestWebDataGridDriver();
            TestWebHierarchicalDataGridDriver();
            TestWebHierarchicalDataGridDriverChild();
        }

        [TestMethod]
        public void エディットプロバイダWebHierarchicalDataGrid子供()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
            var childGrid = grid.GetRowIslands(0, 0, 0);

            //DropDownProvider
            var dropEditor = childGrid.GetDropDownEditorProvider("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGrid_DropDownProvider");
            childGrid.GetCell(0, 0).Activate();
            dropEditor.EmualteEdit("100");
            Assert.AreEqual("100", childGrid.GetCell(0, 0).Text);

            //TextEditorProvider
            var textEditor = childGrid.GetEditorProviderDriver("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGridTextEditorProvider");
            childGrid.GetCell(0, 2).Activate();
            textEditor.EmualteEdit("200");
            Assert.AreEqual("200", childGrid.GetCell(0, 2).Text);

            //DateTimeEditorProvider
            var datepick1 = childGrid.GetEditorProviderDriver("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGridDateTimeEditorProvider");
            childGrid.GetCell(0, 4).Activate();
            datepick1.EmualteEdit("1976/10/28");
            Assert.AreEqual("1976/10/28", childGrid.GetCell(0, 4).Text);

            //DatePickerProvider
            var datepick2 = childGrid.GetEditorProviderDriver("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGridDatePickerProvider");
            childGrid.GetCell(0, 5).Activate();
            datepick2.EmualteEdit("1976/10/28");
            Assert.AreEqual("1976/10/28", childGrid.GetCell(0, 5).Text);

            //NumericEditorProvider
            var numEditor1 = childGrid.GetEditorProviderDriver("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGridNumericEditorProvider");
            childGrid.GetCell(0, 6).Activate();
            numEditor1.EmualteEdit("100");
            Assert.AreEqual("100", childGrid.GetCell(0, 6).Text);

            //TextBoxProvider
            var textBox = childGrid.GetEditorProviderDriver("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGridTextBoxProvider");
            childGrid.GetCell(0, 7).Activate();
            textBox.EmualteEdit("abc");
            Assert.AreEqual("abc", childGrid.GetCell(0, 7).Text);
        }


        [TestMethod]
        public void エディットプロバイダWebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");

            //DropDownProvider
            var dropEditor = grid.GetDropDownEditorProvider("MainContent__webDataGrid__webDataGridDropDownProvider");
            grid.GetCell(0, 0).Activate();
            dropEditor.EmualteEdit("100");
            Assert.AreEqual("100", grid.GetCell(0, 0).Text);

            //TextBoxProvider
            var textBox = grid.GetEditorProviderDriver("MainContent__webDataGrid__webDataGridTextBoxProvider");
            grid.GetCell(0, 3).Activate();
            textBox.EmualteEdit("abc");
            Assert.AreEqual("abc", grid.GetCell(0, 3).Text);

            //TextEditorProvider
            var textEditor = grid.GetEditorProviderDriver("MainContent__webDataGrid__webDataGrid_TextEditorProvider1");
            grid.GetCell(0, 4).Activate();
            textEditor.EmualteEdit("200");
            Assert.AreEqual("200", grid.GetCell(0, 4).Text);

            //DateTimeEditorProvider
            var datepick1 = grid.GetEditorProviderDriver("MainContent__webDataGrid__webDataGrid_DateTimeEditorProvider1");
            grid.GetCell(0, 5).Activate();
            datepick1.EmualteEdit("1976/10/28");
            Assert.AreEqual("1976/10/28", grid.GetCell(0, 5).Text);

            //DatePickerProvider
            var datepick2 = grid.GetEditorProviderDriver("MainContent__webDataGrid__webDataGrid_DatePickerProvider1");
            grid.GetCell(0, 6).Activate();
            datepick2.EmualteEdit("1976/10/28");
            Assert.AreEqual("1976/10/28", grid.GetCell(0, 6).Text);

            //NumericEditorProvider
            var numEditor1 = grid.GetEditorProviderDriver("MainContent__webDataGrid__webDataGrid_NumericEditorProvider1");
            grid.GetCell(0, 7).Activate();
            numEditor1.EmualteEdit("100");
            Assert.AreEqual("100", grid.GetCell(0, 7).Text);

            //追加に関しては最終行に書けばそうなるかも。。
            //カラムをダブルクリックなどをエミュレーションさせるとか？
            //infragiticsのソートJSを呼ぶ。←ソート、フィルター？
            //属性をとってみるか？

            //
        }

        [TestMethod]
        public void TestWebHierarchicalDataGridDriver()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
            var textEditor = grid.GetTextEditor();

            //テキスト編集
            var cell01 = grid.GetCell(0, 1);
            cell01.Activate();
            textEditor.EmualteEdit("犬");
            Assert.AreEqual("犬", cell01.Text);

            IList<IWebElement> links = _driver.FindElements(By.Id("MainContent_UpdateButton"));
            links.First(element => element != null).Click();
        }

        [TestMethod]
        public void TestWebHierarchicalDataGridDriverChild()
        {
            var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");

            //子供！
            //ToDo 返すものをContainerGrid Driver に変更
            var childGrid = grid.GetRowIslands(0, 0, 0);
            var childTextEditor = childGrid.GetTextEditor();
            var childCheckEditor = childGrid.GetCheckEditor();

            var childcell07 = childGrid.GetCell(0, 7);
            childcell07.Activate();
            childTextEditor.EmualteEdit("CDE");
            Assert.AreEqual("CDE", childcell07.Text);
            childTextEditor.EmualteEdit("X");
            Assert.AreEqual("X", childcell07.Text);
            childTextEditor.EmualteEdit("Y");
            Assert.AreEqual("Y", childcell07.Text);

            //チェックボックス編集
            var cell03 = childGrid.GetCell(0, 3);
            cell03.Activate();
            childCheckEditor.EmualteEdit(true);
            Assert.AreEqual(true, cell03.Value);
            childCheckEditor.EmualteEdit(false);
            Assert.AreEqual(false, cell03.Value);

            //コンボ編集
            var childDropEditor = childGrid.GetDropDownEditorProvider("ctl00_MainContent__webHierarchicalDataGrid_ctl00__webHierarchicalDataGrid_DropDownProvider");
            childGrid.GetCell(0, 0).Activate();
            childDropEditor.EmualteEdit(1);
            Assert.AreEqual("100", childGrid.GetCell(0, 0).Text);

            childDropEditor.EmualteEdit("10");
            Assert.AreEqual("10", childGrid.GetCell(0, 0).Text);

            var cell10 = childGrid.GetCell(1, 0);
            cell10.Activate();
            childDropEditor.EmualteEdit("10");
            Assert.AreEqual("10", cell10.Text);
        }

        [TestMethod]
        public void TestWebDataGridDriver()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
            var dropEditor = grid.GetDropDownEditorProvider("MainContent__webDataGrid__webDataGridDropDownProvider");
            var textEditor = grid.GetTextEditor();
            var checkEditor = grid.GetCheckEditor();

            //コンボ編集
            grid.GetCell(0, 0).Activate();
            dropEditor.EmualteEdit(1);
            Assert.AreEqual("100", grid.GetCell(0, 0).Text);
            Assert.AreEqual((long)100, grid.GetCell(0, 0).Value);

            dropEditor.EmualteEdit(0);
            Assert.AreEqual("10", grid.GetCell(0, 0).Text);

            var cell10 = grid.GetCell(1, 0);
            cell10.Activate();
            dropEditor.EmualteEdit("10");
            Assert.AreEqual("10", cell10.Text);

            //テキスト編集
            var cell11 = grid.GetCell(1, 1);
            cell11.Activate();
            textEditor.EmualteEdit("abc");
            Assert.AreEqual("abc", cell11.Text);

            //チェックボックス編集
            var cell12 = grid.GetCell(1, 2);
            cell12.Activate();
            checkEditor.EmualteEdit(true);
            Assert.AreEqual(true, cell12.Value);
        }
    }
}
