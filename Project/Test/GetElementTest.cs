using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using Selenium.WebForms.Infragistics;
using Test;

namespace Test
{
    [TestClass]
    public class GetElementTest
    {
        private static RemoteWebDriver _driver;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = BrowserUtil.GetDriver();
            _driver.Url = "http://infragisticswebformssample.azurewebsites.net/";
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void CSS()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");

            var fst = grid.GetCell(0, 0).GetElement().FontItalic;
            var fw = grid.GetCell(0, 0).GetElement().FontBold;
            var fs = grid.GetCell(0, 0).GetElement().FontSize;
            var tu = grid.GetCell(0, 0).GetElement().TextUnderline;
            var tlt = grid.GetCell(0, 0).GetElement().TextLineThrough;
            var im = grid.GetCell(0, 0).GetElement().ImeMode;
            var t = grid.GetCell(0, 0).GetElement().Text;
            var v = grid.GetCell(0, 0).GetElement().Value;
            var ih = grid.GetCell(0, 0).GetElement().InnerHtml;
            var it = grid.GetCell(0, 0).GetElement().InnerText;
            var w = grid.GetCell(0, 0).GetElement().Width;
            var h = grid.GetCell(0, 0).GetElement().Height;
            var f = grid.GetCell(0, 0).GetElement().Font;
            var c = grid.GetCell(0, 0).GetElement().Color;
            var bgc = grid.GetCell(0, 0).GetElement().BackGroundColor;
            var bgi = grid.GetCell(0, 0).GetElement().BackGroundImage;
            var ta = grid.GetCell(0, 0).GetElement().TextAlign;

            var cn = grid.GetCell(0, 0).GetElement().CssClass;
            var ti = grid.GetCell(0, 0).GetElement().TabIndex;
            var ml = grid.GetCell(0, 0).GetElement().MaxLength;
        }
    }
}