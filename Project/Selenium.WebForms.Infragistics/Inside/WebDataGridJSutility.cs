namespace Selenium.WebForms.Infragistics.Inside
{
    //外から使うものではないのでinsiderフォルダ的な所にしたい。
    internal class WebDataGridJSutility
    {
        private readonly string _id;
        private readonly string _grid;

        internal WebDataGridJSutility(WebDataGridDriver driver)
        {
            _id = driver?.Id;
            _grid = driver?.GetGrid();
        }

        public string LineGetGrid => $"var grid = $find(\"{_id}\");";
        public string ActiveCell => $"var activeCell = {_grid}.get_behaviors().get_activation().get_activeCell();";
        public string EnterEditMode => $"{_grid}.get_behaviors().get_editingCore().get_behaviors().get_cellEditing().enterEditMode(activeCell);";
        public string ExitEditMode => $"{_grid}.get_behaviors().get_editingCore().get_behaviors().get_cellEditing().exitEditMode(true);";
    }
}