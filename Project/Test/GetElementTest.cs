using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using Selenium.WebForms.Infragistics;

namespace Test
{
    [TestClass]
    public class GetElementTest
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
        public void CSS()
        {
            var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");

            grid.GetCell(0, 0).Info.InnerHtml.Is("1");
            grid.GetCell(0, 0).Info.InnerText.Is("1");
            grid.GetCell(0, 0).Info.Text.IsNull();
            grid.GetCell(0, 0).Info.Value.IsNull();
            //grid.GetCell(0, 0).Info.CssClass;
            //grid.GetCell(0, 0).Info.Width.Is("93px");
            //grid.GetCell(0, 0).Info.Height.Is("31px");
            grid.GetCell(0, 0).Info.FontSize.Is("13px");
            grid.GetCell(0, 0).Info.Font.Is("'Segoe UI', Verdana, Helvetica, sans-serif");
            grid.GetCell(0, 0).Info.FontBold.IsFalse();
            grid.GetCell(0, 0).Info.FontItalic.IsFalse();
            grid.GetCell(0, 0).Info.TextUnderline.IsTrue();
            grid.GetCell(0, 0).Info.TextLineThrough.IsTrue();
            grid.GetCell(0, 0).Info.Color.Is("rgba(255, 255, 255, 1)");
            grid.GetCell(0, 0).Info.BackGroundColor.Is("rgba(219, 112, 147, 1)");
            grid.GetCell(0, 0).Info.BackGroundImage.Is("none");
            grid.GetCell(0, 0).Info.TabIndex.Is(-1);
            grid.GetCell(0, 0).Info.ImeMode.Is("");
            grid.GetCell(0, 0).Info.MaxLength.IsNull();
            grid.GetCell(0, 0).Info.TextAlign.Is("left");
        }
    }
}