using System;
using System.Text;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridBehaviorsDriver
    {
        public enum SortType
        {
            None,
            Ascending,
            Descending,
        }


        private WebDataGridDriver WebDataGridDriver { get; }


        public WebDataGridBehaviorsDriver(WebDataGridDriver webDataGridDriver)
        {
            WebDataGridDriver = webDataGridDriver;
        }

        public bool IsHidden(int idx)
        {
            return IsHidden(GetColumnKey(idx));
        }

        public bool IsHidden(string key)
        {
            return (bool)ExecuteScript($"return {WebDataGridDriver.GridScript}.get_columns().get_columnFromKey('{key}').get_hidden();");
        }

        public void Hide(string key, bool hidden = true)
        {
            ExecuteScript($"{WebDataGridDriver.GridScript}.get_columns().get_columnFromKey('{key}').set_hidden({hidden.ToString().ToLower()});");
        }

        public void Hide(int idx, bool hidden = true)
        {
            Hide(GetColumnKey(idx), hidden);
        }

        public void Sort(string key, SortType sort = SortType.Ascending)
        {
            SetSort(key, sort);
        }

        public void Sort(int idx, SortType sort = SortType.Ascending)
        {
            Sort(GetColumnKey(idx), sort);
        }

        public void Filter(string key, string value = "", string rule = "$IG.TextFilterRules.Equals")
        {
            SetFilter(key, value, rule);
        }

        public void Filter(int idx, string value = "", string rule = "$IG.TextFilterRules.Equals")
        {
            Filter(GetColumnKey(idx), value, rule);
        }

        public long GetPageIndex()
        {
            return (long)ExecuteScript($"return {WebDataGridDriver.GridScript}.get_behaviors().get_paging().get_pageIndex();") + 1;
        }

        public void SetPageIndex(int page)
        {
            ExecuteScript($"{WebDataGridDriver.GridScript}.get_behaviors().get_paging().set_pageIndex({page - 1});");
        }

        public void Fix(string key, bool fix = true)
        {
            var columnByKey = fix ? "fixColumnByKey" : "unfixColumnByKey";
            ExecuteScript($"{WebDataGridDriver.GridScript}.get_behaviors().get_columnFixing().{columnByKey}('{key}', $IG.FixLocation.Left)");
        }
        
        public void Fix(int idx, bool fix = true)
        {
            Fix(GetColumnKey(idx), fix);
        }
        
        private void SetSort(string key, SortType sort)
        {
            int type;
            switch (sort)
            {
                case SortType.None: type = 0; break;
                case SortType.Ascending: type = 1; break;
                case SortType.Descending: type = 2; break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sort), sort, null);
            }
            var grid = WebDataGridDriver.GridScript;
            ExecuteScript($"{grid}.get_behaviors().get_sorting().sortColumn({grid}.get_columns().get_columnFromKey('{key}'), {type}, false)");
        }

        private void SetFilter(string key, string value, string rule)
        {
            var grid = WebDataGridDriver.GridScript;

            //http://help.jp.infragistics.com/Help/doc/ASPNET/2014.2/CLR4.0/html/WebDataGrid_Filtering.html
            var script = new StringBuilder();
            script.AppendLine($"var columnFilter = {grid}.get_behaviors().get_filtering().create_columnFilter('{key}');");
            script.AppendLine("var condition = columnFilter.get_condition();");
            script.AppendLine($"condition.set_rule({rule});");
            if (value != "") script.AppendLine($"condition.set_value('{value}');");
            script.AppendLine("var columnFilters = new Array(columnFilter);");
            script.AppendLine($"{grid}.get_behaviors().get_filtering().add_columnFilterRange(columnFilters);");
            script.AppendLine($"{grid}.get_behaviors().get_filtering().applyFilters();");
            ExecuteScript(script.ToString());
        }

        private string GetColumnKey(int idx)
        {
            return (string)ExecuteScript($"return {WebDataGridDriver.GridScript}.get_columns().get_column({idx}).get_key()");
        }

        private object ExecuteScript(string script)
        {
            var js = new WebDataGridJSutility(WebDataGridDriver);
            return WebDataGridDriver.Js.ExecuteScript($"{js.GetGridScript}{script}");
        }
    }
}