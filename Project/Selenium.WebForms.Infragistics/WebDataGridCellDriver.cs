﻿using System;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using Selenium.StandardControls;
using Selenium.WebForms.Infragistics.Inside;
using OpenQA.Selenium.IE;
using Selenium.StandardControls.AdjustBrowser;

namespace Selenium.WebForms.Infragistics
{
    /// <summary>
    /// WebDataGridCell Driver
    /// </summary>
    public class WebDataGridCellDriver
    {
        /// <summary>
        /// Edit How to Start
        /// </summary>
        public enum EditStartMode
        {
            /// <summary>
            /// To edit in the JavaScript of EnterEditMode
            /// </summary>
            JavaScript,
            /// <summary>
            /// To edit in SingleClick
            /// </summary>
            SingleClick,
            /// <summary>
            /// To edit in DoubleClick
            /// </summary>
            DoubleClick,
            /// <summary>
            /// To edit in F2 Key
            /// </summary>
            F2,
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="webDataGrid">Instance of WebDataGridDriver</param>
        /// <param name="rowIndex">The index of the row</param>
        /// <param name="colIndex">The index of the column</param>
        internal WebDataGridCellDriver(WebDataGridDriver webDataGrid, int rowIndex, int colIndex)
        {
            WebDataGrid = webDataGrid;
            RowIndex = rowIndex;
            ColIndex = colIndex;
        }

        /// <summary>
        /// Simple to WebDriver accessor
        /// </summary>
        public WebDataGridDriver WebDataGrid { get; }
        /// <summary>
        /// The index of the row
        /// </summary>
        public int RowIndex { get; }
        /// <summary>
        /// The index of the column
        /// </summary>
        public int ColIndex { get; }
        /// <summary>
        /// Get the text of cell
        /// </summary>
        public string Text => (string)WebDataGrid.Js.ExecuteScript(new WebDataGridJSutility(WebDataGrid).GetGridScript + "return " + CellScript + ".get_text();");
        /// <summary>
        /// Get the value of cell
        /// </summary>
        public object Value => WebDataGrid.Js.ExecuteScript(new WebDataGridJSutility(WebDataGrid).GetGridScript + "return " + CellScript + ".get_value();");
        /// <summary>
        /// Script to get the cell
        /// </summary>
        private string CellScript => $"{WebDataGrid.GridName}.get_rows().get_row({RowIndex}).get_cell({ColIndex})";
        /// <summary>
        /// Element of cell
        /// </summary>
        public IWebElement Element
        {
            get
            {
                string script =
                    $"{new WebDataGridJSutility(WebDataGrid).GetGridScript}return {CellScript}.get_element();";
                return (IWebElement)WebDataGrid.Js.ExecuteScript(script);
            }
        }
        /// <summary>
        /// Element Infomation of cell
        /// </summary>
        public ElementInfo Info => new ElementInfo(Element);

        /// <summary>
        /// The cell to activate
        /// </summary>
        public void Activate()
        {
            var js = new WebDataGridJSutility(WebDataGrid);
            var grid = WebDataGrid.GridName;
            //If you don't set the 2nd argument of set_activeCell, not come ActiveCellChanged event
            //http://help.jp.infragistics.com/Help/doc/Silverlight/2014.1/CLR4.0/html/InfragisticsSL5.Controls.Grids.XamGrid.v14.1~Infragistics.Controls.Grids.XamGrid~SetActiveCell%28CellBase,CellAlignment,InvokeAction,Boolean,Boolean%29.html
            var setActiveCell = $"{js.GetGridScript}grid.get_element().focus();{grid}.get_behaviors().get_activation().set_activeCell({CellScript},1);";
            WebDataGrid.Js.ExecuteScript(setActiveCell);
        }

        /// <summary>
        /// To display the cell within the screen
        /// </summary>
        public void Show()
        {
            var remote = Element as RemoteWebElement;
            remote.LocationOnScreenOnceScrolledIntoView.ToString();
        }

        /// <summary>
        /// scrolls the current element into the visible area of the browser window.
        /// </summary>
        /// <param name="alignToTop">If true, the top of the element will be aligned to the top of the visible area of the scrollable ancestor.If false, the bottom of the element will be aligned to the bottom of the visible area of the scrollable ancestor.</param>
        public void ScrollIntoView(bool alignToTop)
        {
            WebDataGrid.Js.ExecuteScript($"arguments[0].scrollIntoView({alignToTop.ToString().ToLower()});", Element);
        }

        /// <summary>
        /// To edit a cell
        /// </summary>
        /// <param name="text">You want to edit text</param>
        /// <param name="mode">Edit How to Start</param>
        public void Edit(string text, EditStartMode mode = EditStartMode.F2)
        {
            Edit(text, mode, e => e.SendKeys(Keys.Enter));
        }

        /// <summary>
        /// To edit a cell
        /// </summary>
        /// <param name="text">You want to edit text</param>
        /// <param name="mode">Edit How to Start</param>
        /// <param name="finishEditing">Edit How to Finish</param>
        public void Edit(string text, EditStartMode mode, Action<IWebElement> finishEditing)
        {
            IWebElement element = ToEditingMode(mode);
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
                catch { }
            }
            element.SendKeys(text);
            finishEditing(element);
        }

        /// <summary>
        /// To edit a cell(CheckBox only)
        /// </summary>
        /// <param name="check">true: check　false: Do not check</param>
        public void Edit(bool check)
        {
            Show();
            Activate();
            while ((bool)Value != check)
            {
                Show();
                Element.Focus();
                Element.SendKeys(Keys.Space);
                if ((bool)Value == check) break;
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Start editing by mode
        /// </summary>
        /// <param name="mode">Edit How to Start</param>
        /// <returns>Cell element that you want to edit</returns>
        public IWebElement ToEditingMode(EditStartMode mode = EditStartMode.F2)
        {
            switch (mode)
            {
                case EditStartMode.JavaScript:
                    return ToEditingMode(() =>
                    {
                        var js = new WebDataGridJSutility(WebDataGrid);
                        WebDataGrid.Js.ExecuteScript(js.GetGridScript + js.GetActiveCellScript + js.EnterEditModeScript);
                    });
                case EditStartMode.DoubleClick:
                    return ToEditingMode(() =>
                    {
                        new Actions(WebDataGrid.Driver).DoubleClick(Element).Build().Perform();
                    });
                case EditStartMode.SingleClick:
                    return ToEditingMode(() =>
                    {
                        Element.ClickEx();
                    });
                case EditStartMode.F2:
                    return ToEditingMode(() =>
                    {
                        if (Element.GetWebDriver() is InternetExplorerDriver)
                        {
                            //ie's F2 is difference.
                            System.Windows.Forms.SendKeys.SendWait("{F2}");
                        }
                        else
                        {
                            Element.SendKeys(Keys.F2);
                        }
                    });
                default:
                    throw new NotFoundException();
            }

        }
        private IWebElement ToEditingMode(Action action)
        {
            Show();
            Activate();
            IWebElement activeElement;
            while (true)
            {
                try
                {
                    // Combo box corresponding to are calling in a row
                    Show();
                    Element.Focus();
                    action();
                    Element.GetJS().ExecuteScript("");//sync.
                    activeElement = WebDataGrid.Driver.SwitchTo().ActiveElement();
                    var cellRect = GetRect(Element);
                    var activeRect = GetRect(activeElement);
                    var intersect = activeRect;
                    intersect.Intersect(cellRect);

                    //Certified and editing elements if the cross more than half of the activeElement area
                    var certifyTargetRect = ((activeRect.Width* activeRect.Height) / 2) < (intersect.Width* intersect.Height);
                    if (activeElement.Displayed && (activeElement.TagName == "input" || activeElement.TagName == "textarea")
                        && activeElement.Size.Height != 0 && activeElement.Size.Width != 0 && certifyTargetRect)
                    {
                        break;
                    }
                }
                catch { }
                Thread.Sleep(10);
            }
            return activeElement;
        }

        private Rectangle GetRect(IWebElement element)
        {
            var left = WebDataGrid.Js.ExecuteScript("return arguments[0].getBoundingClientRect().left;", element);
            var top = WebDataGrid.Js.ExecuteScript("return arguments[0].getBoundingClientRect().top;", element);
            var width = WebDataGrid.Js.ExecuteScript("return arguments[0].getBoundingClientRect().width;", element);
            var height = WebDataGrid.Js.ExecuteScript("return arguments[0].getBoundingClientRect().height;", element);
            return new Rectangle(ToInt(left), ToInt(top), ToInt(width), ToInt(height));
        }

        private static int ToInt(object value)
        {
            if (value is double)
            {
                return (int)(double)value;
            }
            if (value is float)
            {
                return (int)(float)value;
            }
            if (value is long)
            {
                return (int)(long)value;
            }
            if (value is int)
            {
                return (int)value;
            }
            throw new NotSupportedException();
        }
    }
}