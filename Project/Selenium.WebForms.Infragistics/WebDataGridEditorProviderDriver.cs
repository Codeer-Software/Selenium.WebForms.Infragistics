using System.Threading;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridEditorProviderDriver
    {
        private WebDataGridDriver WebDataGrid { get; }
        protected string Id { get; }
        protected IWebDriver Driver => WebDataGrid.Driver;
        protected IJavaScriptExecutor Js => (IJavaScriptExecutor)Driver;

        public WebDataGridEditorProviderDriver(WebDataGridDriver webDataGrid, string id)
        {
            WebDataGrid = webDataGrid;
            Id = id;
        }

        public void Edit(string text)
        {
            EditCore(text);
        }

        protected virtual void SetValue(string text)
        {
            var editor = Driver.SwitchTo().ActiveElement();
            editor.Clear();
            editor.SendKeys(text);
        }

        private void EditCore(string text)
        {
            var js = new WebDataGridJSutility(WebDataGrid);
            while (true)
            {
                WebDataGrid.Js.ExecuteScript(js.GetGridScript + js.GetActiveCellScript + js.EnterEditModeScript);
                var e = Driver.FindElement(By.Id(Id));
                if (e.Displayed)
                {
                    break;
                }
                Thread.Sleep(10);
            }
            SetValue(text);
            WebDataGrid.Js.ExecuteScript(js.GetGridScript + js.ExitEditModeScript);
        }
    }

    public static class WebDataGridEditorProviderDriverExtensions
    {
        public static WebDataGridEditorProviderDriver GetEditorProvider(this WebDataGridDriver grid,string id) => new WebDataGridEditorProviderDriver(grid, id);
    }
}