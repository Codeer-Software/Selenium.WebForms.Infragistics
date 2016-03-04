namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridJSutility
    {
        private readonly string _id;
        private readonly string _grid;

        public WebDataGridJSutility(WebDataGridDriver driver)
        {
            _id = driver?.Id;
            _grid = WebDataGridDriver.GetGrid(driver);
        }
        public string LineGetGrid => $"var grid = $find(\"{_id}\");";
        public string ActiveCell => $"var activeCell = {_grid}.get_behaviors().get_activation().get_activeCell();";
        public string EnterEditMode => $"{_grid}.get_behaviors().get_editingCore().get_behaviors().get_cellEditing().enterEditMode(activeCell);";
        public string ExitEditMode => $"{_grid}.get_behaviors().get_editingCore().get_behaviors().get_cellEditing().exitEditMode(true);";
    }
}
