using System;
using System.Text;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridBehaviorsDriver
    {
        private readonly WebDataGridDriver _webDataGridDriver;

        public enum SortType
        {
            None,
            Ascending,
            Descending,
        }

        public WebDataGridBehaviorsDriver(WebDataGridDriver webDataGridDriver)
        {
            _webDataGridDriver = webDataGridDriver;
        }

        public void Sort(string key, SortType sort = SortType.Ascending)
        {
            SetSort(key, sort);
        }

        public void Sort(int idx, SortType sort = SortType.Ascending)
        {
            Sort(GetColumnKey(idx), sort);
        }

        public void Filter(string key, string value = "")
        {
            SetFilter(key, value);
        }

        public void Filter(int idx, string value = "")
        {
            Filter(GetColumnKey(idx), value);
        }

        public void Page(int page)
        {
            SetPage(page);
        }

        public void Hidden(string key, bool hidden = true)
        {
            SetHidden(key, hidden);
        }

        public void Hidden(int idx, bool hidden = true)
        {
            Hidden(GetColumnKey(idx), hidden);
        }

        public void Fix(string key, bool fix = true)
        {
            SetFix(key, fix);
        }

        public void Fix(int idx, bool fix = true)
        {
            SetFix(GetColumnKey(idx), fix);
        }

        //ToDo  I want to write fixColumn. But fixColumn is nothing. fixColumnByKey
        private void SetFix(string key, bool fix)
        {
            var grid = _webDataGridDriver.GetGrid();
            var columnByKey = fix ? "fixColumnByKey" : "unfixColumnByKey";
            ExecuteScript($"{grid}.get_behaviors().get_columnFixing().{columnByKey}('{key}', $IG.FixLocation.Left)");
        }

        private void SetHidden(string key, bool hidden)
        {
            var grid = _webDataGridDriver.GetGrid();
            ExecuteScript($"{grid}.get_columns().get_columnFromKey('{key}').set_hidden({hidden.ToString().ToLower()});");
        }

        private void SetPage(int page)
        {
            var grid = _webDataGridDriver.GetGrid();
            //get_pageCount,get_pageIndex
            ExecuteScript($"{grid}.get_behaviors().get_paging().set_pageIndex({page - 1});");
        }

        private void SetSort(string key, SortType sort)
        {
            var grid = _webDataGridDriver.GetGrid();
            int type;
            switch (sort)
            {
                case SortType.None: type = 0; break;
                case SortType.Ascending: type = 1; break;
                case SortType.Descending: type = 2; break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sort), sort, null);
            }
            ExecuteScript($"{grid}.get_behaviors().get_sorting().sortColumn({grid}.get_columns().get_columnFromKey('{key}'), {type}, false)");
        }

        private void SetFilter(string key, string value)
        {
            var grid = _webDataGridDriver.GetGrid();

            var script = new StringBuilder();
            //CreateとかAddとか既存でもこの方法しかない？
            script.AppendLine($"var columnFilter = {grid}.get_behaviors().get_filtering().create_columnFilter('{key}');");
            script.AppendLine("var condition = columnFilter.get_condition();");
            //script.AppendLine("condition.set_rule($IG.TextFilterRules.Equals);");
            if (value != "") script.AppendLine($"condition.set_value('{value}');");
            script.AppendLine("var columnFilters = new Array(columnFilter);");
            script.AppendLine($"{grid}.get_behaviors().get_filtering().add_columnFilterRange(columnFilters);");
            script.AppendLine($"{grid}.get_behaviors().get_filtering().applyFilters();");
            ExecuteScript(script.ToString());
        }

        private string GetColumnKey(int idx)
        {
            //待ち受けしないとgridがこないときもある気がする。起動時待ち受けはselenium?
            var grid = _webDataGridDriver.GetGrid();
            return (string)ExecuteScript($"return {grid}.get_columns().get_column({idx}).get_key()");
        }

        private object ExecuteScript(string script)
        {
            var js = new WebDataGridJSutility(_webDataGridDriver);
            return _webDataGridDriver.Js.ExecuteScript($"{js.LineGetGrid}{script}");
        }
    }
}