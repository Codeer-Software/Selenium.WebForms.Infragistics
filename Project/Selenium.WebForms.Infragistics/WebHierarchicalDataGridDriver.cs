using System;
using System.Threading;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebHierarchicalDataGridDriver : WebDataGridDriver
    {
        #region Properties
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

        #endregion Properties

        #region Constructors
        public WebHierarchicalDataGridDriver(IWebDriver driver, string id)
            : base(driver, id)
        {
        }
        #endregion Constructors

        #region Methods

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
            var expand = $"{js.GetGridScript}grid.get_gridView().get_rows().get_row({RowIndex}).set_expanded(true);";
            while (true)
            {
                Js.ExecuteScript(expand);
                Thread.Sleep(10);
                break;
            }
        }
        #endregion Methods
    }
}