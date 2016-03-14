using System.Threading;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridEditorProviderDriver
    {
        #region Properties

        private string Id { get; }
        private WebDataGridDriver WebDataGrid { get; }
        protected IWebDriver Driver => WebDataGrid.Driver;
        protected IJavaScriptExecutor Js => (IJavaScriptExecutor)Driver;

        #endregion Properties

        #region Constructors

        public WebDataGridEditorProviderDriver(WebDataGridDriver webDataGrid, string id)
        {
            WebDataGrid = webDataGrid;
            Id = id;
        }

        #endregion Constructors

        #region Methods

        public void Edit(string text)
        {
            EditCore(text);
        }

        protected virtual void SetValue(string text)
        {
            var id = GetControlId();
            if (string.IsNullOrEmpty(id))
            {
                var editor = Driver.SwitchTo().ActiveElement();
                editor.Clear();
                editor.SendKeys(text);
            }
            else
            {
                Js.ExecuteScript($"{id}.set_value('{text}');");
            }
        }

        protected string GetControlId()
        {
            //Id next tag, however "_clientState" is pull out .
            //But TextBoxProvider is null
            var core = (string)Js.ExecuteScript($"return document.getElementById(\"{Id}\").firstElementChild.id;");
            //// EditorProvider は ig_controls.WebDataGrid1_ctlxx より参照取得
            return (string.IsNullOrEmpty(core) || !core.Contains("_ctl")) ? string.Empty : "ig_controls." + core.Replace("_clientState", "");
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
            //WebDataGrid.Js.ExecuteScript(js.GetGridScript + js.GetActiveCellScript + js.EnterEditModeScript);
            SetValue(text);
            WebDataGrid.Js.ExecuteScript(js.GetGridScript + js.ExitEditModeScript);
        }

        #endregion Methods
    }

    public static class WebDataGridEditorProviderDriverExtensions
    {
        public static WebDataGridEditorProviderDriver GetEditorProvider(this WebDataGridDriver grid, string id) => new WebDataGridEditorProviderDriver(grid, id);
    }
}