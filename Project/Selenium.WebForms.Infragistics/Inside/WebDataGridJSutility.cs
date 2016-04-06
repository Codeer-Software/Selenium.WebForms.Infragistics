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

        public static string GridScript => "grid";
        public string GetGridScript => $"var grid = $find(\"{_id}\");";
        public string GetActiveCellScript => $"var activeCell = {_grid}.get_behaviors().get_activation().get_activeCell();";
        public string EnterEditModeScript => $"{_grid}.get_behaviors().get_editingCore().get_behaviors().get_cellEditing().enterEditMode(activeCell);";
    }

    //TODO Infragisticsのはここに集めた方がいいかも
}