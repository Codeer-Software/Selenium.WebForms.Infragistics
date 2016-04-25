using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridDriver
    {
        public enum SortType
        {
            None,
            Ascending,
            Descending,
        }
        public IWebDriver Driver { get;  }
        public string Id { get; }
        public IJavaScriptExecutor Js => (IJavaScriptExecutor)Driver;
        public virtual string GridScript => WebDataGridJSutility.GridScript;
        public WebDataGridDriver(IWebDriver driver, string id)
        {
            Driver = driver;
            Id = id;
        }

        public IWebElement GetActiveCellElement()
        {
            var js = new WebDataGridJSutility(this);
            return (IWebElement)Js.ExecuteScript(js.GetGridScript + js.GetActiveCellScript + "return activeCell.get_element();");
        }

        public virtual WebDataGridCellDriver GetCell(int rowIndex, int colIndex) => new WebDataGridCellDriver(this, rowIndex, colIndex);
        public IEnumerable<WebDataGridCellDriver> GetCells(int rowIndex)
        {
            var script = new WebDataGridJSutility(this).GetGridScript + $"return {GridScript}.get_rows().get_row({rowIndex}).get_cellCount();";
            var count = (long)Js.ExecuteScript(script);
            for (var i = 0; i < count; i++)
            {
                yield return GetCell(rowIndex, i);
            }
        }

        public long RowCount => (long)Js.ExecuteScript(new WebDataGridJSutility(this).GetGridScript + $"return {GridScript}.get_rows().get_length();");
        public long ColumnCount => (long)Js.ExecuteScript(new WebDataGridJSutility(this).GetGridScript + $"return {GridScript}.get_columns().get_length();");

        public WebDataGridColumnsDriver GetColumn(int columnIndex) => new WebDataGridColumnsDriver(this, columnIndex);

        public bool IsReadOnly(int index) => (bool)ExecuteScript($"return {GridScript}.get_behaviors().get_editingCore().get_behaviors().get_cellEditing().get_columnSettings()._items[{index}]._readOnly;");

        public bool IsHidden(int index) => IsHidden(GetColumnKey(index));

        public bool IsHidden(string key) => (bool)ExecuteScript($"return {GridScript}.get_columns().get_columnFromKey('{key}').get_hidden();");

        public void SetHidden(string key, bool hidden)
        {
            ExecuteScript($"{GridScript}.get_columns().get_columnFromKey('{key}').set_hidden({hidden.ToString().ToLower()});");
        }

        public void SetHidden(int index, bool hidden)
        {
            SetHidden(GetColumnKey(index), hidden);
        }

        public void SetSort(string key, SortType sort = SortType.Ascending)
        {
            int type;
            switch (sort)
            {
                case SortType.None: type = 0; break;
                case SortType.Ascending: type = 1; break;
                case SortType.Descending: type = 2; break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sort), sort, null);
            }
            var grid = GridScript;
            ExecuteScript($"{grid}.get_behaviors().get_sorting().sortColumn({grid}.get_columns().get_columnFromKey('{key}'), {type}, false)");
        }

        public void SetSort(int index, SortType sort = SortType.Ascending)
        {
            SetSort(GetColumnKey(index), sort);
        }

        public void SetFilter(string key, int filterIndex, string value)
        {
            SetFilter(key, value, filterIndex.ToString());
        }

        public void SetFilter(int index, int filterIndex, string value)
        {
            SetFilter(GetColumnKey(index), filterIndex, value);
        }

        public long GetPageIndex() => (long)ExecuteScript($"return {GridScript}.get_behaviors().get_paging().get_pageIndex();") + 1;

        public void SetPageIndex(int page)
        {
            ExecuteScript($"{GridScript}.get_behaviors().get_paging().set_pageIndex({page - 1});");
        }

        public void SetFixed(string key, bool fix = true)
        {
            var columnByKey = fix ? "fixColumnByKey" : "unfixColumnByKey";
            ExecuteScript($"{GridScript}.get_behaviors().get_columnFixing().{columnByKey}('{key}', $IG.FixLocation.Left)");
        }

        public void SetFixed(int index, bool fix = true)
        {
            SetFixed(GetColumnKey(index), fix);
        }

        private void SetFilter(string key, string value, string rule)
        {
            var grid = GridScript;

            //http://help.jp.infragistics.com/Help/doc/ASPNET/2014.2/CLR4.0/html/WebDataGrid_Filtering.html
            var script = new StringBuilder();
            script.AppendLine($"var columnFilter = {grid}.get_behaviors().get_filtering().create_columnFilter('{key}');");
            script.AppendLine("var condition = columnFilter.get_condition();");
            script.AppendLine($"condition.set_rule({rule});");
            if (value != "") script.AppendLine($"condition.set_value('{value}');");
            script.AppendLine("var columnFilters = new Array(columnFilter);");
            script.AppendLine($"{grid}.get_behaviors().get_filtering().add_columnFilterRange(columnFilters);");
            script.AppendLine($"{grid}.get_behaviors().get_filtering().applyFilters();");
            ExecuteScript(script.ToString());
        }

        private string GetColumnKey(int index) => (string)ExecuteScript($"return {GridScript}.get_columns().get_column({index}).get_key()");

        private object ExecuteScript(string script)
        {
            var js = new WebDataGridJSutility(this);
            return Js.ExecuteScript($"{js.GetGridScript}{script}");
        }


    }
}