using OpenQA.Selenium;

namespace Selenium.WebForms.Infragistics
{
    public class WebHierarchicalDataGridDriver : WebDataGridDriver
    {
        public int RowIndex { get; set; }
        public int RowIslandsIndex { get; set; }
        public int ColIndex { get; set; }
        public bool Islands { get; set; }

        public WebHierarchicalDataGridDriver(IWebDriver driver, string id)
            : base(driver, id)
        {
            Hierarchical = true;
        }

        public WebHierarchicalDataGridDriver GetRowIslands(int rowIndex, int rowIslandsIndex, int colIndex)
        {
            Islands = true;
            RowIndex = rowIndex;
            RowIslandsIndex = rowIslandsIndex;
            ColIndex = colIndex;
            return this;
        }
    }
}