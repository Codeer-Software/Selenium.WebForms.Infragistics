using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    /// <summary>
    /// WebHierarchicalDataGrid Driver
    /// </summary>
    public class WebHierarchicalDataGridDriver : WebDataGridDriver
    {
        private class Indexs
        {
            public int RowIndex { get; set; }
            public int RowIslandsIndex { get; set; }
            public int ColIndex { get; set; }
            public Indexs Parent { get; set; }

        }
        private Indexs Index { get; set; }
        private int Islands { get; set; }
        private string GridExpandedName => GetGridName(true);
        /// <summary>
        /// Grid name of WebHierarchicalDataGridDriver
        /// </summary>
        protected internal override string GridName => GetGridName(false);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver">Drivers to access</param>
        /// <param name="id">ID of WebDataGrid</param>
        public WebHierarchicalDataGridDriver(IWebDriver driver, string id)
            : base(driver, id)
        {
        }

        /// <summary>
        /// Get WebHierarchicalDataGridDriver
        /// </summary>
        /// <param name="rowIndex">The index of the row</param>
        /// <param name="rowIslandsIndex">A collection of row islands or child ContainerGrids</param>
        /// <param name="colIndex">The index of the column</param>
        /// <returns></returns>
        public WebHierarchicalDataGridDriver GetRowIslands(int rowIndex, int rowIslandsIndex, int colIndex)
        {
            var child = new WebHierarchicalDataGridDriver(Driver, Id);
            child.Islands += Islands;
            child.Islands++;
            child.Index = new Indexs
            {
                RowIndex = rowIndex,
                RowIslandsIndex = rowIslandsIndex,
                ColIndex = colIndex,
                Parent = Index,
            };
            return child;
        }

        /// <summary>
        /// Opening and closing of the hierarchy
        /// Wait until the opening and closing
        /// </summary>
        /// <param name="isExpanded">true:open false:close</param>
        public void SetExpanded(bool isExpanded)
        {
            if (Islands == 0) return;
            var js = new WebDataGridJSutility(this);
            Js.ExecuteScript($"{js.GetGridScript}{GridExpandedName}.get_rows().get_row({Index.RowIndex}).set_expanded({isExpanded.ToString().ToLower()});");
            IgAjax.WaitForAjaxIndicator(Driver);
        }

        private string GetGridName(bool expanded)
        {
            var grid = WebDataGridJSutility.WebHierarchicalDataGridGridName;
            if (Index == null) return grid;
            var idxs = new List<Indexs> { Index };
            var top = Index.Parent;
            while (top != null)
            {
                idxs.Add(top);
                top = top.Parent;
            }
            idxs.Reverse();
            if (expanded) idxs.RemoveAt(idxs.Count - 1);
            grid = idxs.Aggregate(grid, (current, idx) => current + $".get_rows().get_row({idx.RowIndex}).get_rowIslands({idx.RowIslandsIndex})[{idx.ColIndex}]");
            return grid;
        }
    }
}