using OpenQA.Selenium;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridDriver
    {
        #region Properties

        public IWebDriver Driver { get;  }
        public string Id { get; }
        public IJavaScriptExecutor Js => (IJavaScriptExecutor)Driver;
        public virtual string GridScript => "grid";

        #endregion Properties

        #region Constructors

        public WebDataGridDriver(IWebDriver driver, string id)
        {
            Driver = driver;
            Id = id;
        }
        #endregion Constructors
        #region Methods
        public WebDataGridCellDriver GetCell(int rowIndex, int colIndex) => new WebDataGridCellDriver(this, rowIndex, colIndex);
        //ToDo Column Unimplemented
        //public WebDataGridColumns GetColumn(int columnIndex) => new WebDataGridColumns(this, columnIndex);
        public WebDataGridBehaviorsDriver GetBehaviors() => new WebDataGridBehaviorsDriver(this);
        #endregion Methods
    }
}