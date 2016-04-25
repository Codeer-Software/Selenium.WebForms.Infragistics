//using System;
//using System.Text;
//using Selenium.WebForms.Infragistics.Inside;

//namespace Selenium.WebForms.Infragistics
//{
//    public class WebDataGridBehaviorsDriver
//    {
//        public enum SortType
//        {
//            None,
//            Ascending,
//            Descending,
//        }


//        private WebDataGridDriver WebDataGridDriver { get; }


//        public WebDataGridBehaviorsDriver(WebDataGridDriver webDataGridDriver)
//        {
//            WebDataGridDriver = webDataGridDriver;
//        }


//        //public bool IsReadOnly(int index)
//        //{
//        //    return (bool)ExecuteScript($"return {WebDataGridDriver.GridScript}.get_behaviors().get_editingCore().get_behaviors().get_cellEditing().get_columnSettings()._items[{index}]._readOnly;");
//        //}

//        //public bool IsHidden(int index)
//        //{
//        //    return IsHidden(GetColumnKey(index));
//        //}

//        //public bool IsHidden(string key)
//        //{
//        //    return (bool)ExecuteScript($"return {WebDataGridDriver.GridScript}.get_columns().get_columnFromKey('{key}').get_hidden();");
//        //}

//        //public void SetHidden(string key, bool hidden = true)
//        //{
//        //    ExecuteScript($"{WebDataGridDriver.GridScript}.get_columns().get_columnFromKey('{key}').set_hidden({hidden.ToString().ToLower()});");
//        //}

//        //public void SetHidden(int index, bool hidden = true)
//        //{
//        //    SetHidden(GetColumnKey(index), hidden);
//        //}

//        //public void SetSort(string key, SortType sort = SortType.Ascending)
//        //{
//        //    SetSort(key, sort);
//        //}

//        //public void SetSort(int index, SortType sort = SortType.Ascending)
//        //{
//        //    SetSort(GetColumnKey(index), sort);
//        //}

//        //public void SetFilter(string key, int filterIndex, string value)
//        //{
//        //    SetFilter(key, value, filterIndex.ToString());
//        //}

//        //public void SetFilter(int index, int filterIndex, string value)
//        //{
//        //    SetFilter(GetColumnKey(index), filterIndex, value);
//        //}

//        //public long GetPageIndex()
//        //{
//        //    return (long)ExecuteScript($"return {WebDataGridDriver.GridScript}.get_behaviors().get_paging().get_pageIndex();") + 1;
//        //}

//        //public void SetPageIndex(int page)
//        //{
//        //    ExecuteScript($"{WebDataGridDriver.GridScript}.get_behaviors().get_paging().set_pageIndex({page - 1});");
//        //}

//        //public void SetFixed(string key, bool fix = true)
//        //{
//        //    var columnByKey = fix ? "fixColumnByKey" : "unfixColumnByKey";
//        //    ExecuteScript($"{WebDataGridDriver.GridScript}.get_behaviors().get_columnFixing().{columnByKey}('{key}', $IG.FixLocation.Left)");
//        //}

//        //public void SetFixed(int index, bool fix = true)
//        //{
//        //    SetFixed(GetColumnKey(index), fix);
//        //}

//        //private void SetSort(string key, SortType sort)
//        //{
//        //    int type;
//        //    switch (sort)
//        //    {
//        //        case SortType.None: type = 0; break;
//        //        case SortType.Ascending: type = 1; break;
//        //        case SortType.Descending: type = 2; break;
//        //        default:
//        //            throw new ArgumentOutOfRangeException(nameof(sort), sort, null);
//        //    }
//        //    var grid = WebDataGridDriver.GridScript;
//        //    ExecuteScript($"{grid}.get_behaviors().get_sorting().sortColumn({grid}.get_columns().get_columnFromKey('{key}'), {type}, false)");
//        //}

//        //private void SetFilter(string key, string value, string rule)
//        //{
//        //    var grid = WebDataGridDriver.GridScript;

//        //    //http://help.jp.infragistics.com/Help/doc/ASPNET/2014.2/CLR4.0/html/WebDataGrid_Filtering.html
//        //    var script = new StringBuilder();
//        //    script.AppendLine($"var columnFilter = {grid}.get_behaviors().get_filtering().create_columnFilter('{key}');");
//        //    script.AppendLine("var condition = columnFilter.get_condition();");
//        //    script.AppendLine($"condition.set_rule({rule});");
//        //    if (value != "") script.AppendLine($"condition.set_value('{value}');");
//        //    script.AppendLine("var columnFilters = new Array(columnFilter);");
//        //    script.AppendLine($"{grid}.get_behaviors().get_filtering().add_columnFilterRange(columnFilters);");
//        //    script.AppendLine($"{grid}.get_behaviors().get_filtering().applyFilters();");
//        //    ExecuteScript(script.ToString());
//        //}

//        //private string GetColumnKey(int index)
//        //{
//        //    return (string)ExecuteScript($"return {WebDataGridDriver.GridScript}.get_columns().get_column({index}).get_key()");
//        //}

//        //private object ExecuteScript(string script)
//        //{
//        //    var js = new WebDataGridJSutility(WebDataGridDriver);
//        //    return WebDataGridDriver.Js.ExecuteScript($"{js.GetGridScript}{script}");
//        //}
//    }
//}