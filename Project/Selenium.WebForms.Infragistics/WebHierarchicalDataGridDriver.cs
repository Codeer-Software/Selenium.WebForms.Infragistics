using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebHierarchicalDataGridDriver : WebDataGridDriver
    {
        public override string GridScript => GetScript(false);
        private string GridExpandedScript => GetScript(true);

        private class Indexs
        {
            public int RowIndex { get; set; }
            public int RowIslandsIndex { get; set; }
            public int ColIndex { get; set; }
            public Indexs Parent { get; set; }

        }
        private Indexs Index { get; set; }
        private int Islands { get; set; }

        public WebHierarchicalDataGridDriver(IWebDriver driver, string id)
            : base(driver, id)
        {
        }

        private string GetScript(bool expanded)
        {
            var grid = "grid.get_gridView()";
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

        public virtual WebHierarchicalDataGridDriver GetRowIslands(int rowIndex, int rowIslandsIndex, int colIndex)
        {
            var child = new WebHierarchicalDataGridDriver(Driver, Id);
            child.Islands += Islands;
            child.Islands++;
            child.Index = new Indexs()
            {
                RowIndex = rowIndex,
                RowIslandsIndex = rowIslandsIndex,
                ColIndex = colIndex,
                Parent = Index,
            };
            return child;
        }


        public void SetExpanded()
        {
            if (Islands == 0) return;
            var js = new WebDataGridJSutility(this);
            Js.ExecuteScript($"{js.GetGridScript}{GridExpandedScript}.get_rows().get_row({Index.RowIndex}).set_expanded(true);");
            while (true)
            {
                try
                {
                    GetCell(0, 0).Activate();
                    break;
                }
                catch (InvalidOperationException)
                {
                }
                Thread.Sleep(10);
            }
        }
    }
}