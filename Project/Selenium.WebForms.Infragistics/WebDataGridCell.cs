using System;
using System.Threading;
using Selenium.StandardControls;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridCell
    {
        public WebDataGridDriver Grid { get; }
        public int RowIndex { get; }
        public int ColIndex { get; }
        public string Text => (string)Grid.Js.ExecuteScript(new WebDataGridJSutility(Grid).LineGetGrid + "return " + GetCell + ".get_text();");
        public object Value => Grid.Js.ExecuteScript(new WebDataGridJSutility(Grid).LineGetGrid + "return " + GetCell + ".get_value();");
        public string GetCell
        {
            get
            {
                var grid = WebDataGridDriver.GetGrid(Grid);
                return $"{grid}.get_rows().get_row({RowIndex}).get_cell({ColIndex})";
            }
        }

        internal WebDataGridCell(WebDataGridDriver grid, int rowIndex, int colIndex)
        {
            Grid = grid;
            RowIndex = rowIndex;
            ColIndex = colIndex;
        }

        public void Activate()
        {
            var js = new WebDataGridJSutility(Grid);
            var grid = WebDataGridDriver.GetGrid(Grid);

            //set_activeCellの第二引数を設定しないと、ActiveCellChangedイベントが来ない
            //http://help.jp.infragistics.com/Help/doc/Silverlight/2014.1/CLR4.0/html/InfragisticsSL5.Controls.Grids.XamGrid.v14.1~Infragistics.Controls.Grids.XamGrid~SetActiveCell%28CellBase,CellAlignment,InvokeAction,Boolean,Boolean%29.html
            var setActiveCell = $"{js.LineGetGrid}grid.get_element().focus();{grid}.get_behaviors().get_activation().set_activeCell({GetCell},1);";
            if (Grid.Hierarchical)
            {
                var hierarchicalGrid = Grid as WebHierarchicalDataGridDriver;
                if (hierarchicalGrid != null && hierarchicalGrid.Islands)
                {
                    var expand = $"{js.LineGetGrid}grid.get_gridView().get_rows().get_row({RowIndex}).set_expanded(true);";
                    while (true)
                    {
                        Grid.Js.ExecuteScript(expand);
                        Thread.Sleep(10);
                        //次に呼ぶべきメソッドで例外が出ないか
                        try
                        {
                            Grid.Js.ExecuteScript(setActiveCell);
                        }
                        catch (InvalidOperationException)
                        {
                            continue;
                        }
                        break;
                    }
                }
            }
            Grid.Js.ExecuteScript(setActiveCell);
        }
    }

    public static class WebElementExtensions
    {
        public static ElementDriver GetElement(this WebDataGridCell cell)
        {
            var script = $"{new WebDataGridJSutility(cell.Grid).LineGetGrid}var {ElementDriver.VarName}={cell.GetCell}.get_element();";
            return new ElementDriver(cell.Grid.Driver, script);
        }
    }
}