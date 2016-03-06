using System;
using System.Threading;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebHierarchicalDataGridDriver : WebDataGridDriver
    {
        public int RowIndex { get; set; }
        public int RowIslandsIndex { get; set; }
        public int ColIndex { get; set; }
        public bool Islands { get; set; }

        public WebHierarchicalDataGridDriver(IWebDriver driver, string id)
            : base(driver, id)
        {
        }

        public override string GetGrid()
        {
            var grid = "grid.get_gridView()";
            if (Islands)
            {
                grid += $".get_rows().get_row({RowIndex}).get_rowIslands({RowIslandsIndex})[{ColIndex}]";
            }
            return grid;
        }

        public WebHierarchicalDataGridDriver GetRowIslands(int rowIndex, int rowIslandsIndex, int colIndex)
        {
            Islands = true;
            RowIndex = rowIndex;
            RowIslandsIndex = rowIslandsIndex;
            ColIndex = colIndex;
            return this;
        }


        public void SetExpanded()
        {
            if (!Islands) return;
            var js = new WebDataGridJSutility(this);
            var expand = $"{js.LineGetGrid}grid.get_gridView().get_rows().get_row({RowIndex}).set_expanded(true);";
            while (true)
            {
                Js.ExecuteScript(expand);
                Thread.Sleep(10);
                break;
            }
        }
    }
}