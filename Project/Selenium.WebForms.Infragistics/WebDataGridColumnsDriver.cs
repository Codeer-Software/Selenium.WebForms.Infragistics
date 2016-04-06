using OpenQA.Selenium;
using Selenium.StandardControls;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridColumnsDriver
    {

        public string Text => (string)WebDataGrid.Js.ExecuteScript(ColumnScript + ".get_header().get_text();");
        private WebDataGridDriver WebDataGrid { get; }
        private int ColmnIndex { get; }
        private string ColumnScript =>  $"{new WebDataGridJSutility(WebDataGrid).GetGridScript} return {WebDataGrid.GridScript}.get_columns().get_column({ColmnIndex})";
        internal WebDataGridColumnsDriver(WebDataGridDriver webDataGrid, int colmnIndex)
        {
            WebDataGrid = webDataGrid;
            ColmnIndex = colmnIndex;
        }
        public IWebElement Element => (IWebElement)WebDataGrid.Js.ExecuteScript($"{ColumnScript}.get_headerElement();");
        public ElementInfo Info => new ElementInfo(Element);
    }
}