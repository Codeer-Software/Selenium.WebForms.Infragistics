using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
            _driver.Url = TestCommon.TargetUrl;
            _webDataGrid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void MoveWebDataGridDriver()
        {
            var element = _webDataGrid.GetColumn(0).Element;
            var otherElement = _webDataGrid.GetColumn(2).Element;

            //element.Click();

            Actions builder = new Actions(_driver);

            // 興味のある技術分野の選択
            var action = new Actions(_driver);

            action.KeyDown(Keys.Control)
                //.Click(element)
                .DragAndDrop(element, otherElement)
                .KeyUp(Keys.Control);
                //.ClickAndHold(_driver.FindElement(By.Id("_webDataGrid_columnheader_0")))
                //.MoveByOffset(100, 0)
                //.Release()
                //.Build();

            var selected = action.Build();
            selected.Perform();


            //var dragAndDrop = builder.DragAndDropToOffset(element, 50, 10).Build();


            //var dragAndDrop = builder.ClickAndHold(element)
            //    .MoveToElement(otherElement)
            //    .Release(otherElement)
            //    .Build();

        }

        [TestMethod]
        public void ReadOnlyWebDataGridDriver()
        {
            _webDataGrid.GetBehaviors().IsReadOnly(0).IsFalse();
        }

        [TestMethod]
        public void FilterWebDataGridDriver()
        {
            _webDataGrid.GetBehaviors().Filter("LastName", 1, "Ishikawa");
        }

        [TestMethod]
        public void PageWebDataGridDriver()
        {
            Assert.AreEqual(_webDataGrid.GetBehaviors().GetPageIndex(), 1);
            _webDataGrid.GetBehaviors().SetPageIndex(2);
            Assert.AreEqual(_webDataGrid.GetBehaviors().GetPageIndex(), 2);
        }

        [TestMethod]
        public void HiddenWebDataGridDriver()
        {
            Assert.AreEqual(_webDataGrid.GetBehaviors().IsHidden(2), false);
            _webDataGrid.GetBehaviors().Hide(2);
            Assert.AreEqual(_webDataGrid.GetBehaviors().IsHidden(2), true);
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
            var childGrid = grid.GetRowIslands(0, 0, 0);
            childGrid.SetExpanded(true);
            Assert.AreEqual(childGrid.GetBehaviors().GetPageIndex(), 1);
            childGrid.GetBehaviors().SetPageIndex(2);
            Assert.AreEqual(childGrid.GetBehaviors().GetPageIndex(), 2);
        }
    }
}