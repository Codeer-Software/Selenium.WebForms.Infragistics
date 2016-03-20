using System;
using System.Threading;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebHierarchicalDataGridDriver : WebDataGridDriver
    {
        public override string GridScript
        {
            get
            {
                var grid = "grid.get_gridView()";
                if (Islands)
                {
                    grid += $".get_rows().get_row({RowIndex}).get_rowIslands({RowIslandsIndex})[{ColIndex}]";
                }
                return grid;
            }
        }
        private int RowIndex { get; set; }
        private int RowIslandsIndex { get; set; }
        private int ColIndex { get; set; }
        private bool Islands { get; set; }

        public WebHierarchicalDataGridDriver(IWebDriver driver, string id)
            : base(driver, id)
        {
        }

        public virtual WebHierarchicalDataGridDriver GetRowIslands(int rowIndex, int rowIslandsIndex, int colIndex)
        {
            var island = new WebHierarchicalDataGridDriver(Driver, Id)
            {
                Islands = true,
                RowIndex = rowIndex,
                RowIslandsIndex = rowIslandsIndex,
                ColIndex = colIndex
            };
            return island;
        }


        public void SetExpanded()
        {
            if (!Islands) return;
            var js = new WebDataGridJSutility(this);
            Js.ExecuteScript($"{js.GetGridScript}grid.get_gridView().get_rows().get_row({RowIndex}).set_expanded(true);");
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