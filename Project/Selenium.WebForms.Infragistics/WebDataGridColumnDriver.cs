using System;
using System.Text;
using OpenQA.Selenium;
using Selenium.StandardControls;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    /// <summary>
    /// WebDataGridColumn Driver
    /// </summary>
    public class WebDataGridColumnDriver
    {
        private WebDataGridDriver WebDataGrid { get; }
        private int ColumnIndex { get; }
        private string ColumnKey { get; }
        private string ColumnScript =>  $"{new WebDataGridJSutility(WebDataGrid).GetGridScript} return {WebDataGrid.GridScript}.get_columns().get_column({ColumnIndex})";
        /// <summary>
        /// Column header name
        /// </summary>
        public string Text => (string)WebDataGrid.Js.ExecuteScript(ColumnScript + ".get_header().get_text();");
        /// <summary>
        /// Column header element
        /// </summary>
        public IWebElement Element => (IWebElement)WebDataGrid.Js.ExecuteScript($"{ColumnScript}.get_headerElement();");
        /// <summary>
        /// Column header element infomation
        /// </summary>
        public ElementInfo Info => new ElementInfo(Element);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="webDataGrid">Instance of WebDataGridDriver</param>
        /// <param name="columnIndex">The index of the column</param>
        internal WebDataGridColumnDriver(WebDataGridDriver webDataGrid, int columnIndex)
        {
            WebDataGrid = webDataGrid;
            ColumnIndex = columnIndex;
            ColumnKey = GetColumnKey(ColumnIndex);
        }

        /// <summary>
        /// Read-only state
        /// </summary>
        /// <returns>true:read-only false:not a read-only</returns>
        public bool IsReadOnly() => (bool)ExecuteScript($"return {WebDataGrid.GridScript}.get_behaviors().get_editingCore().get_behaviors().get_cellEditing().get_columnSettings()._items[{ColumnIndex}]._readOnly;");

        /// <summary>
        /// Hidden state
        /// </summary>
        /// <returns>true:hidden false:not a hidden</returns>
        public bool IsHidden() => (bool)ExecuteScript($"return {WebDataGrid.GridScript}.get_columns().get_columnFromKey('{ColumnKey}').get_hidden();");

        /// <summary>
        /// Hides
        /// </summary>
        /// <param name="hidden">true:hidden false:not a hidden</param>
        public void SetHidden(bool hidden)
        {
            ExecuteScript($"{WebDataGrid.GridScript}.get_columns().get_columnFromKey('{ColumnKey}').set_hidden({hidden.ToString().ToLower()});");
        }

        /// <summary>
        /// Sort
        /// </summary>
        /// <param name="sort">Sort type</param>
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

        /// <summary>
        /// Filter
        /// </summary>
        /// <param name="filterIndex">Index of filter rules</param>
        /// <param name="value">Filter values</param>
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

        /// <summary>
        /// Fixing
        /// </summary>
        /// <param name="fix">true: fixed false: not fixed</param>
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