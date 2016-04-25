using System;
using System.Text;
using OpenQA.Selenium;
using Selenium.StandardControls;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridColumnDriver
    {

        public string Text => (string)WebDataGrid.Js.ExecuteScript(ColumnScript + ".get_header().get_text();");
        private WebDataGridDriver WebDataGrid { get; }
        private int ColumnIndex { get; }
        private string ColumnKey { get; }
        private string ColumnScript =>  $"{new WebDataGridJSutility(WebDataGrid).GetGridScript} return {WebDataGrid.GridScript}.get_columns().get_column({ColumnIndex})";
        internal WebDataGridColumnDriver(WebDataGridDriver webDataGrid, int columnIndex)
        {
            WebDataGrid = webDataGrid;
            ColumnIndex = columnIndex;
            ColumnKey = GetColumnKey(ColumnIndex);
        }
        public IWebElement Element => (IWebElement)WebDataGrid.Js.ExecuteScript($"{ColumnScript}.get_headerElement();");
        public ElementInfo Info => new ElementInfo(Element);

        public bool IsReadOnly() => (bool)ExecuteScript($"return {WebDataGrid.GridScript}.get_behaviors().get_editingCore().get_behaviors().get_cellEditing().get_columnSettings()._items[{ColumnIndex}]._readOnly;");


        public bool IsHidden() => (bool)ExecuteScript($"return {WebDataGrid.GridScript}.get_columns().get_columnFromKey('{ColumnKey}').get_hidden();");

        public void SetHidden(bool hidden)
        {
            ExecuteScript($"{WebDataGrid.GridScript}.get_columns().get_columnFromKey('{ColumnKey}').set_hidden({hidden.ToString().ToLower()});");
        }

        public void SetSort(WebDataGridDriver.SortType sort = WebDataGridDriver.SortType.Ascending)
        {
            int type;
            switch (sort)
            {
                case WebDataGridDriver.SortType.None: type = 0; break;
                case WebDataGridDriver.SortType.Ascending: type = 1; break;
                case WebDataGridDriver.SortType.Descending: type = 2; break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sort), sort, null);
            }
            var grid = WebDataGrid.GridScript;
            ExecuteScript($"{grid}.get_behaviors().get_sorting().sortColumn({grid}.get_columns().get_columnFromKey('{ColumnKey}'), {type}, false)");
        }

        public void SetFilter(int filterIndex, string value)
        {
            var grid = WebDataGrid.GridScript;
            //http://help.jp.infragistics.com/Help/doc/ASPNET/2014.2/CLR4.0/html/WebDataGrid_Filtering.html
            var script = new StringBuilder();
            script.AppendLine($"var columnFilter = {grid}.get_behaviors().get_filtering().create_columnFilter('{ColumnKey}');");
            script.AppendLine("var condition = columnFilter.get_condition();");
            script.AppendLine($"condition.set_rule({filterIndex});");
            if (value != "") script.AppendLine($"condition.set_value('{value}');");
            script.AppendLine("var columnFilters = new Array(columnFilter);");
            script.AppendLine($"{grid}.get_behaviors().get_filtering().add_columnFilterRange(columnFilters);");
            script.AppendLine($"{grid}.get_behaviors().get_filtering().applyFilters();");
            ExecuteScript(script.ToString());

        }

        public void SetFixed(bool fix = true)
        {
            var columnByKey = fix ? "fixColumnByKey" : "unfixColumnByKey";
            ExecuteScript($"{WebDataGrid.GridScript}.get_behaviors().get_columnFixing().{columnByKey}('{ColumnKey}', $IG.FixLocation.Left)");
        }

        private string GetColumnKey(int colIndex) => (string)ExecuteScript($"return {WebDataGrid.GridScript}.get_columns().get_column({colIndex}).get_key()");

        private object ExecuteScript(string script)
        {
            var js = new WebDataGridJSutility(WebDataGrid);
            return WebDataGrid.Js.ExecuteScript($"{js.GetGridScript}{script}");
        }


    }
}