using System.Threading;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridDataFieldDriver
    {
        #region Properties

        public WebDataGridDriver WebDataGrid { get; }

        #endregion Properties

        #region Constructors

        public WebDataGridDataFieldDriver(WebDataGridDriver webDataGrid)
        {
            WebDataGrid = webDataGrid;
        }

        #endregion Constructors

        #region Methods

        public void Edit(string text)
        {
            var js = new WebDataGridJSutility(WebDataGrid);
            IWebElement editor;
            while (true)
            {
                WebDataGrid.Js.ExecuteScript(js.GetGridScript + js.GetActiveCellScript + js.EnterEditModeScript);
                editor = WebDataGrid.Driver.FindElement(By.Id(WebDataGrid.Id)).FindElement(By.ClassName("igg_EditCell"));
                if (editor.Displayed)
                {
                    break;
                }
                Thread.Sleep(10);
            }
            editor.Clear();
            editor.SendKeys(text);
            WebDataGrid.Js.ExecuteScript(js.GetGridScript + js.ExitEditModeScript);
        }

        #endregion Methods
    }

    public static class WebDataGridDataFieldDriverExtensions
    {
        public static WebDataGridDataFieldDriver GetDataField(this WebDataGridDriver grid) => new WebDataGridDataFieldDriver(grid);
    }
}