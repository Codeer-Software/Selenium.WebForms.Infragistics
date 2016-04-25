using System.Collections.Generic;
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

        public IWebDriver Driver { get; }
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

        public WebDataGridColumnDriver GetColumn(int columnIndex) => new WebDataGridColumnDriver(this, columnIndex);

        public long GetPageIndex() => (long)Js.ExecuteScript($"{new WebDataGridJSutility(this).GetGridScript} return {GridScript}.get_behaviors().get_paging().get_pageIndex();") + 1;

        public void SetPageIndex(int page)
        {
            Js.ExecuteScript($"{new WebDataGridJSutility(this).GetGridScript}{GridScript}.get_behaviors().get_paging().set_pageIndex({page - 1});");
        }
    }
}