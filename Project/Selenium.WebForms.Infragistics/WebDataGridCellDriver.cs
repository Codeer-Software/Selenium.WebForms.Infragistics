using Selenium.StandardControls;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridCellDriver
    {
        public WebDataGridDriver Grid { get; }
        public int RowIndex { get; }
        public int ColIndex { get; }
        public string Text => (string)Grid.Js.ExecuteScript(new WebDataGridJSutility(Grid).LineGetGrid + "return " + GetCellScript + ".get_text();");
        public object Value => Grid.Js.ExecuteScript(new WebDataGridJSutility(Grid).LineGetGrid + "return " + GetCellScript + ".get_value();");

        public string GetCellScript => $"{Grid.GetGrid()}.get_rows().get_row({RowIndex}).get_cell({ColIndex})";

        internal WebDataGridCellDriver(WebDataGridDriver grid, int rowIndex, int colIndex)
        {
            Grid = grid;
            RowIndex = rowIndex;
            ColIndex = colIndex;
        }

        public void Activate()
        {
            var js = new WebDataGridJSutility(Grid);
            var grid = Grid.GetGrid();
            //set_activeCellの第二引数を設定しないと、ActiveCellChangedイベントが来ない
            //http://help.jp.infragistics.com/Help/doc/Silverlight/2014.1/CLR4.0/html/InfragisticsSL5.Controls.Grids.XamGrid.v14.1~Infragistics.Controls.Grids.XamGrid~SetActiveCell%28CellBase,CellAlignment,InvokeAction,Boolean,Boolean%29.html
            var setActiveCell = $"{js.LineGetGrid}grid.get_element().focus();{grid}.get_behaviors().get_activation().set_activeCell({GetCellScript},1);";
            Grid.Js.ExecuteScript(setActiveCell);
        }
        public ElementDriver GetElement()
        {
            var script = $"{new WebDataGridJSutility(Grid).LineGetGrid}var element={GetCellScript}.get_element();";
            return new ElementDriver(new ElementScript(Grid.Driver, script));
        }
    }
}