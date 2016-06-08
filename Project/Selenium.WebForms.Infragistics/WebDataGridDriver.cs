using System.Collections.Generic;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    /// <summary>
    /// WebDataGrid Driver
    /// </summary>
    public class WebDataGridDriver
    {
        /// <summary>
        /// Simple to WebDriver accessor
        /// </summary>
        public IWebDriver Driver { get; }
        /// <summary>
        /// ID of WebDataGrid
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// Simple to IJavaScriptExecutor accessor
        /// </summary>
        public IJavaScriptExecutor Js => (IJavaScriptExecutor)Driver;
        /// <summary>
        /// Grid name of WebDataGrid
        /// </summary>
        protected internal virtual string GridName => WebDataGridJSutility.WebDataGridGridName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver">Drivers to access</param>
        /// <param name="id">ID of WebDataGrid</param>
        public WebDataGridDriver(IWebDriver driver, string id)
        {
            Driver = driver;
            Id = id;
        }

        /// <summary>
        /// Get the active cell element
        /// </summary>
        /// <returns>active cell element</returns>
        public IWebElement GetActiveCellElement()
        {
            var js = new WebDataGridJSutility(this);
            return (IWebElement)Js.ExecuteScript(js.GetGridScript + js.GetActiveCellScript + "return activeCell.get_element();");
        }

        /// <summary>
        /// Get WebDataGridCellDriver
        /// </summary>
        /// <param name="rowIndex">The index of the row</param>
        /// <param name="colIndex">The index of the column</param>
        /// <returns>WebDataGridCellDriver</returns>
        public virtual WebDataGridCellDriver GetCell(int rowIndex, int colIndex) => new WebDataGridCellDriver(this, rowIndex, colIndex);

        /// <summary>
        /// Get WebDataGridCellDriver collection of rows
        /// </summary>
        /// <param name="rowIndex">The index of the row</param>
        /// <returns>WebDataGridCellDriver Collection</returns>
        public IEnumerable<WebDataGridCellDriver> GetCells(int rowIndex)
        {
            var script = new WebDataGridJSutility(this).GetGridScript + $"return {GridName}.get_rows().get_row({rowIndex}).get_cellCount();";
            var count = (long)Js.ExecuteScript(script);
            for (var i = 0; i < count; i++)
            {
                yield return GetCell(rowIndex, i);
            }
        }

        /// <summary>
        /// Number of rows
        /// </summary>
        public long RowCount => (long)Js.ExecuteScript(new WebDataGridJSutility(this).GetGridScript + $"return {GridName}.get_rows().get_length();");

        /// <summary>
        /// Number of columns
        /// </summary>
        public long ColumnCount => (long)Js.ExecuteScript(new WebDataGridJSutility(this).GetGridScript + $"return {GridName}.get_columns().get_length();");

        /// <summary>
        /// Get WebDataGridColumnDriver
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public WebDataGridColumnDriver GetColumn(int columnIndex) => new WebDataGridColumnDriver(this, columnIndex);

        /// <summary>
        /// Get page index
        /// </summary>
        /// <returns></returns>
        public long GetPageIndex() => (long)Js.ExecuteScript($"{new WebDataGridJSutility(this).GetGridScript} return {GridName}.get_behaviors().get_paging().get_pageIndex();") + 1;

        /// <summary>
        /// Set page index
        /// </summary>
        /// <param name="page"></param>
        public void SetPageIndex(int page)
        {
            Js.ExecuteScript($"{new WebDataGridJSutility(this).GetGridScript}{GridName}.get_behaviors().get_paging().set_pageIndex({page - 1});");
        }
    }
}