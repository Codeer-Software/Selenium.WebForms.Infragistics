using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridDriver
    {
        public IWebDriver Driver { get;  }
        public string Id { get; }
        public IJavaScriptExecutor Js => (IJavaScriptExecutor)Driver;
        public virtual string GridScript => WebDataGridJSutility.GridScript;
        public WebDataGridDriver(IWebDriver driver, string id)
        {
            Driver = driver;
            Id = id;
        }

        public virtual WebDataGridCellDriver GetCell(int rowIndex, int colIndex) => new WebDataGridCellDriver(this, rowIndex, colIndex);
        public IEnumerable<WebDataGridCellDriver> GetCells(int rowIndex)
        {
            var script = new WebDataGridJSutility(this).GetGridScript + $"return {GridScript}.get_rows().get_row({rowIndex}).get_cellCount();";
            var count = (long)Js.ExecuteScript(script);
            for (int i = 0; i < count; i++)
            {
                yield return GetCell(rowIndex, i);
            }
        }

        public long RowCount => (long)Js.ExecuteScript(new WebDataGridJSutility(this).GetGridScript + $"return {GridScript}.get_rows().get_length();");

        public WebDataGridColumnsDriver GetColumn(int columnIndex) => new WebDataGridColumnsDriver(this, columnIndex);
        public WebDataGridBehaviorsDriver GetBehaviors() => new WebDataGridBehaviorsDriver(this);
    }
}