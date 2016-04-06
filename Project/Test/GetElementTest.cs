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
            _driver.Url = TestCommon.TargetUrl;
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

            var fst = grid.GetCell(0, 0).Info.FontItalic;
            var fw = grid.GetCell(0, 0).Info.FontBold;
            var fs = grid.GetCell(0, 0).Info.FontSize;
            var tu = grid.GetCell(0, 0).Info.TextUnderline;
            var tlt = grid.GetCell(0, 0).Info.TextLineThrough;
            var im = grid.GetCell(0, 0).Info.ImeMode;
            var t = grid.GetCell(0, 0).Info.Text;
            var v = grid.GetCell(0, 0).Info.Value;
            var ih = grid.GetCell(0, 0).Info.InnerHtml;
            var it = grid.GetCell(0, 0).Info.InnerText;
            var w = grid.GetCell(0, 0).Info.Width;
            var h = grid.GetCell(0, 0).Info.Height;
            var f = grid.GetCell(0, 0).Info.Font;
            var c = grid.GetCell(0, 0).Info.Color;
            var bgc = grid.GetCell(0, 0).Info.BackGroundColor;
            var bgi = grid.GetCell(0, 0).Info.BackGroundImage;
            var ta = grid.GetCell(0, 0).Info.TextAlign;

            var cn = grid.GetCell(0, 0).Info.CssClass;
            var ti = grid.GetCell(0, 0).Info.TabIndex;
            var ml = grid.GetCell(0, 0).Info.MaxLength;
        }
    }
}