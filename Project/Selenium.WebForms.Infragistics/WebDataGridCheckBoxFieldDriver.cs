using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridCheckBoxFieldDriver
    {
        public WebDataGridDriver Grid { get; }

        public WebDataGridCheckBoxFieldDriver(WebDataGridDriver grid)
        {
            Grid = grid;
        }

        public void Edit(bool check)
        {
            var js = new WebDataGridJSutility(Grid);
            var current = (bool)Grid.Js.ExecuteScript(js.LineGetGrid + js.ActiveCell + "return activeCell.get_value();");
            if (current != check)
            {
                Grid.Js.ExecuteScript(js.LineGetGrid + js.ActiveCell + "activeCell.get_element().children[0].click();");
            }
        }
    }

    public static class CheckBoxFieldDriverExtensions
    {
        public static WebDataGridCheckBoxFieldDriver GetCheckBoxField(this WebDataGridDriver grid) => new WebDataGridCheckBoxFieldDriver(grid);
    }
}