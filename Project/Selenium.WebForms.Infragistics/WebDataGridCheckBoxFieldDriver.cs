using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridCheckBoxFieldDriver
    {
        #region Properties
        public WebDataGridDriver WebDataGrid { get; }
        #endregion Properties
        #region Constructors
        public WebDataGridCheckBoxFieldDriver(WebDataGridDriver webDataGrid)
        {
            WebDataGrid = webDataGrid;
        }

        #endregion Constructors
        #region Methods
        public void Edit(bool check)
        {
            var js = new WebDataGridJSutility(WebDataGrid);
            var current = (bool)WebDataGrid.Js.ExecuteScript(js.GetGridScript + js.GetActiveCellScript + "return activeCell.get_value();");
            if (current != check)
            {
                WebDataGrid.Js.ExecuteScript(js.GetGridScript + js.GetActiveCellScript + "activeCell.get_element().children[0].click();");
            }
        }
        #endregion Methods
    }

    public static class CheckBoxFieldDriverExtensions
    {
        public static WebDataGridCheckBoxFieldDriver GetCheckBoxField(this WebDataGridDriver grid) => new WebDataGridCheckBoxFieldDriver(grid);
    }
}