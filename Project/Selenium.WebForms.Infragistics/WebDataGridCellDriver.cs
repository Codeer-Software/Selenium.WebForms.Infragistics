using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
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


        internal WebDataGridCellDriver(WebDataGridDriver webDataGrid, int rowIndex, int colIndex)
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

        public void Show()
        {
            var remote = Element as RemoteWebElement;
            remote.LocationOnScreenOnceScrolledIntoView.ToString();
        }

        public void Edit(string text)
        {
            Edit(text, e=> e.SendKeys(Keys.Enter));
        }

        public void Edit(string text, Action<IWebElement> finishEditing)
        {
            IWebElement element = ToEditingMode();
            try
            {
                WebDataGrid.Js.ExecuteScript("arguments[0].select();", element);
                element.SendKeys(Keys.Delete);
            }
            catch
            {
                try
                {
                    element.Clear();
                }
                catch {}
            }
            element.SendKeys(text);
            finishEditing(element);
        }

        public IWebElement ToEditingMode()
        {
            Show();
            Activate();
            var js = new WebDataGridJSutility(WebDataGrid);
            IWebElement element;
            while (true)
            {
                try
                {
                    // Combo box corresponding to are calling in a row
                    new Actions(WebDataGrid.Driver).DoubleClick(Element).Build().Perform();
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
            Show();
            Activate();
            var js = new WebDataGridJSutility(WebDataGrid);
            var current = (bool)Value;
            if (current != check)
            {
                var core = (IWebElement)WebDataGrid.Js.ExecuteScript(js.GetGridScript + js.GetActiveCellScript + "return activeCell.get_element().children[0];");
                core.Click();
            }
        }

        public ElementInfo Info => new ElementInfo(Element);
        public IWebElement Element
        {
            get
            {
                string script =
                    $"{new WebDataGridJSutility(WebDataGrid).GetGridScript}return {CellScript}.get_element();";
                return (IWebElement) WebDataGrid.Js.ExecuteScript(script);
            }
        }

    }
}