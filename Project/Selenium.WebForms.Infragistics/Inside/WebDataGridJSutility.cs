namespace Selenium.WebForms.Infragistics.Inside
{
    internal class WebDataGridJSutility
    {
        private readonly string _id;
        private readonly string _grid;

        internal WebDataGridJSutility(WebDataGridDriver driver)
        {
            _id = driver?.Id;
            _grid = driver?.GridScript;
        }

        public string GetGridScript => $"var grid = $find(\"{_id}\");";
        public string GetActiveCellScript => $"var activeCell = {_grid}.get_behaviors().get_activation().get_activeCell();";
        public string EnterEditModeScript => $"{_grid}.get_behaviors().get_editingCore().get_behaviors().get_cellEditing().enterEditMode(activeCell);";
        public string ExitEditModeScript => $"{_grid}.get_behaviors().get_editingCore().get_behaviors().get_cellEditing().exitEditMode(true);";
    }
}