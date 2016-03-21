using System.Threading;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;
using System.Linq;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridEditorProviderDriver
    {
        private WebDataGridDriver WebDataGrid { get; }
        public WebDataGridEditorProviderDriver(WebDataGridDriver webDataGrid)
        {
            WebDataGrid = webDataGrid;
        }
        public void Edit(string text)
        {
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

            element.Clear();
            element.SendKeys(text);
            element.SendKeys(Keys.Enter);
        }
    }

    public static class WebDataGridEditorProviderDriverExtensions
    {
        public static WebDataGridEditorProviderDriver GetEditorProvider(this WebDataGridDriver grid) => new WebDataGridEditorProviderDriver(grid);
    }
}