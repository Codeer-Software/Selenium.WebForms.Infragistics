using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridColumnsDriver
    {
        #region Properties

        public string Text => (string)WebDataGrid.Js.ExecuteScript(ColumnScript + ".get_header().get_text();");
        //public string CssClass => (string)WebDataGrid.Js.ExecuteScript(ColumnScript + ".get_header().get_cssClass();");

        private WebDataGridDriver WebDataGrid { get; }
        private int ColmnIndex { get; }
        private string ColumnScript =>  $"{new WebDataGridJSutility(WebDataGrid).GetGridScript} return {WebDataGrid.GridScript}.get_columns().get_column({ColmnIndex})";

        #endregion Properties
        internal WebDataGridColumnsDriver(WebDataGridDriver webDataGrid, int colmnIndex)
        {
            WebDataGrid = webDataGrid;
            ColmnIndex = colmnIndex;
        }
    }
}