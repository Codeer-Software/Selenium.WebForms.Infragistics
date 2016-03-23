using System;
using System.Threading;
using OpenQA.Selenium;
using Selenium.StandardControls;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridCellDriver
    {

        public WebDataGridDriver WebDataGrid { get; }
        public int RowIndex { get; }
        public int ColIndex { get; }
        public string Text => (string)WebDataGrid.Js.ExecuteScript(new WebDataGridJSutility(WebDataGrid).GetGridScript + "return " + CellScript + ".get_text();");
        public object Value => WebDataGrid.Js.ExecuteScript(new WebDataGridJSutility(WebDataGrid).GetGridScript + "return " + CellScript + ".get_value();");

        public string CellScript => $"{WebDataGrid.GridScript}.get_rows().get_row({RowIndex}).get_cell({ColIndex})";


        protected internal WebDataGridCellDriver(WebDataGridDriver webDataGrid, int rowIndex, int colIndex)
        {
            WebDataGrid = webDataGrid;
            RowIndex = rowIndex;
            ColIndex = colIndex;
        }


        public void Activate()
        {
            var js = new WebDataGridJSutility(WebDataGrid);
            var grid = WebDataGrid.GridScript;
            //If you don't set the 2nd argument of set_activeCell, not come ActiveCellChanged event
            //http://help.jp.infragistics.com/Help/doc/Silverlight/2014.1/CLR4.0/html/InfragisticsSL5.Controls.Grids.XamGrid.v14.1~Infragistics.Controls.Grids.XamGrid~SetActiveCell%28CellBase,CellAlignment,InvokeAction,Boolean,Boolean%29.html
            var setActiveCell = $"{js.GetGridScript}grid.get_element().focus();{grid}.get_behaviors().get_activation().set_activeCell({CellScript},1);";
            WebDataGrid.Js.ExecuteScript(setActiveCell);
        }

        public void WaitForActive()
        {
            while (true)
            {
                try
                {
                    Activate();
                    break;
                }
                catch (InvalidOperationException)
                {
                }
                Thread.Sleep(10);
            }
        }

        public void Edit(string text)
        {
            Edit(text, e=> e.SendKeys(Keys.Enter));
        }

        public void Edit(string text, Action<IWebElement> finishEditing)
        {
            IWebElement element = ToEditingMode();
            element.Clear();
            element.SendKeys(text);
            finishEditing(element);
        }

        public IWebElement ToEditingMode()
        {
            Activate();
            var js = new WebDataGridJSutility(WebDataGrid);
            IWebElement element;
            while (true)
            {
                WebDataGrid.Js.ExecuteScript(js.GetGridScript + js.GetActiveCellScript + js.EnterEditModeScript);
                try
                {
                    element = WebDataGrid.Driver.SwitchTo().ActiveElement();
                    if (element.Displayed && element.TagName == "input" || element.TagName == "textarea")
                    {
                        break;
                    }
                }
                catch (StaleElementReferenceException)
                {
                }
                Thread.Sleep(10);
            }
            return element;
        }

        public void Edit(bool check)
        {
            Activate();
            var js = new WebDataGridJSutility(WebDataGrid);
            var current = (bool)WebDataGrid.Js.ExecuteScript(js.GetGridScript + js.GetActiveCellScript + "return activeCell.get_value();");
            if (current != check)
            {
                WebDataGrid.Js.ExecuteScript(js.GetGridScript + js.GetActiveCellScript + "activeCell.get_element().children[0].click();");
            }
        }

        public ElementDriver GetElement() => new ElementDriver(GetWebElement());
        public IWebElement GetWebElement()
        {
            string script = $"{new WebDataGridJSutility(WebDataGrid).GetGridScript}return {CellScript}.get_element();";
            return (IWebElement)WebDataGrid.Js.ExecuteScript(script);
        }

    }
}