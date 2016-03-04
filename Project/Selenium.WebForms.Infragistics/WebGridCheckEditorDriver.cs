namespace Selenium.WebForms.Infragistics
{
    public class WebGridCheckEditorDriver
    {
        public WebDataGridDriver Grid { get; }

        public WebGridCheckEditorDriver(WebDataGridDriver grid)
        {
            Grid = grid;
        }

        public void EmualteEdit(bool check)
        {
            var js = new WebDataGridJSutility(Grid);
            var current = (bool)Grid.Js.ExecuteScript(js.LineGetGrid + js.ActiveCell + "return activeCell.get_value();");
            if (current != check)
            {
                Grid.Js.ExecuteScript(js.LineGetGrid + js.ActiveCell + "activeCell.get_element().children[0].click();");
            }
        }
    }

    public static class WebGridCheckEditorDriverExtensions
    {
        public static WebGridCheckEditorDriver GetCheckEditor(this WebDataGridDriver grid) => new WebGridCheckEditorDriver(grid);
    }
}