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

        public string GetSort(string key)
        {
            var grid = WebDataGridDriver.GridScript;
            var a = ExecuteScript($" return {grid}.get_behaviors().get_sorting().sortColumn({grid}.get_columns().get_columnFromKey('{key}'))");
            return (string)a;
        }
        public long GetPage()
        {
            return (long)ExecuteScript($"return {WebDataGridDriver.GridScript}.get_behaviors().get_paging().get_pageIndex();") + 1;
        }
        public bool GetHidden(int idx)
        {
            return GetHidden(GetColumnKey(idx));
        }
        public bool GetHidden(string key)
        {
            return (bool)ExecuteScript($"return {WebDataGridDriver.GridScript}.get_columns().get_columnFromKey('{key}').get_hidden();");
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
            var columnByKey = fix ? "fixColumnByKey" : "unfixColumnByKey";
            ExecuteScript($"{WebDataGridDriver.GridScript}.get_behaviors().get_columnFixing().{columnByKey}('{key}', $IG.FixLocation.Left)");
        }

        private void SetHidden(string key, bool hidden)
        {
            ExecuteScript($"{WebDataGridDriver.GridScript}.get_columns().get_columnFromKey('{key}').set_hidden({hidden.ToString().ToLower()});");
        }

        private void SetPage(int page)
        {
            ExecuteScript($"{WebDataGridDriver.GridScript}.get_behaviors().get_paging().set_pageIndex({page - 1});");
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