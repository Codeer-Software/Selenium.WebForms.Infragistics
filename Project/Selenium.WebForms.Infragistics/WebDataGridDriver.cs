using OpenQA.Selenium;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridDriver
    {
        public IWebDriver Driver { get; protected set; }
        public IJavaScriptExecutor Js => (IJavaScriptExecutor)Driver;
        public string Id { get; }

        public virtual string GetGrid() => "grid";

        public WebDataGridDriver(IWebDriver driver, string id)
        {
            Driver = driver;
            Id = id;
        }

        public WebDataGridCellDriver GetCell(int rowIndex, int colIndex) => new WebDataGridCellDriver(this, rowIndex, colIndex);

        //ToDo Column Unimplemented
        //public WebDataGridColumns GetColumn(int columnIndex) => new WebDataGridColumns(this, columnIndex);
        public WebDataGridBehaviorsDriver GetBehaviors() => new WebDataGridBehaviorsDriver(this);
    }
}