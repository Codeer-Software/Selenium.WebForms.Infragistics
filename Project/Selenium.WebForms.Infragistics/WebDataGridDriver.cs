using OpenQA.Selenium;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridDriver
    {
        public IWebDriver Driver { get; protected set; }
        public IJavaScriptExecutor Js => (IJavaScriptExecutor)Driver;
        public string Id { get; }
        public bool Hierarchical { get; protected set; }

        public static string GetGrid(WebDataGridDriver driver)
        {
            string grid;
            if (driver?.Hierarchical == true)
            {
                grid = "grid.get_gridView()";
                var hierarchical = driver as WebHierarchicalDataGridDriver;
                if (hierarchical != null && hierarchical.Islands)
                {
                    grid += $".get_rows().get_row({hierarchical.RowIndex}).get_rowIslands({hierarchical.RowIslandsIndex})[{hierarchical.ColIndex}]";
                }
            }
            else
            {
                grid = "grid";
            }
            return grid;
        }


        public WebDataGridDriver(IWebDriver driver, string id)
        {
            Driver = driver;
            Id = id;
        }

        public WebDataGridCell GetCell(int rowIndex, int colIndex) => new WebDataGridCell(this, rowIndex, colIndex);
        //public WebDataGridColumns GetColumn(int columnIndex) => new WebDataGridColumns(this, columnIndex);
        public WebDataGridBehaviors GetBehaviors() => new WebDataGridBehaviors(this);

    }
}