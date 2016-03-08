using Selenium.StandardControls;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridCellDriver
    {
        #region Properties

        public WebDataGridDriver WebDataGrid { get; }
        public int RowIndex { get; }
        public int ColIndex { get; }
        public string Text => (string)WebDataGrid.Js.ExecuteScript(new WebDataGridJSutility(WebDataGrid).GetGridScript + "return " + CellScript + ".get_text();");
        public object Value => WebDataGrid.Js.ExecuteScript(new WebDataGridJSutility(WebDataGrid).GetGridScript + "return " + CellScript + ".get_value();");

        public string CellScript => $"{WebDataGrid.GridScript}.get_rows().get_row({RowIndex}).get_cell({ColIndex})";

        #endregion Properties

        #region Constructors

        internal WebDataGridCellDriver(WebDataGridDriver webDataGrid, int rowIndex, int colIndex)
        {
            WebDataGrid = webDataGrid;
            RowIndex = rowIndex;
            ColIndex = colIndex;
        }

        #endregion Constructors

        #region Methods

        public void Activate()
        {
            var js = new WebDataGridJSutility(WebDataGrid);
            var grid = WebDataGrid.GridScript;
            //If you don't set the 2nd argument of set_activeCell, not come ActiveCellChanged event
            //http://help.jp.infragistics.com/Help/doc/Silverlight/2014.1/CLR4.0/html/InfragisticsSL5.Controls.Grids.XamGrid.v14.1~Infragistics.Controls.Grids.XamGrid~SetActiveCell%28CellBase,CellAlignment,InvokeAction,Boolean,Boolean%29.html
            var setActiveCell = $"{js.GetGridScript}grid.get_element().focus();{grid}.get_behaviors().get_activation().set_activeCell({CellScript},1);";
            WebDataGrid.Js.ExecuteScript(setActiveCell);
        }

        public ElementDriver GetElement()
        {
            var script = $"{new WebDataGridJSutility(WebDataGrid).GetGridScript}var element={CellScript}.get_element();";
            return new ElementDriver(new ElementScript(WebDataGrid.Driver, script));
        }

        #endregion Methods
    }
}