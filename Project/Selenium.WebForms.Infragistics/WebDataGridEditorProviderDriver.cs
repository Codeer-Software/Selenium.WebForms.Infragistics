using System.Threading;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridEditorProviderDriver
    {
        private string Id { get; }
        private WebDataGridDriver Grid { get; }
        protected IWebDriver Driver => Grid.Driver;
        protected IJavaScriptExecutor Js => (IJavaScriptExecutor)Driver;

        public WebDataGridEditorProviderDriver(WebDataGridDriver grid, string id)
        {
            Grid = grid;
            Id = id;
        }

        public void Edit(string text)
        {
            EditCore(text);
        }

        private void EditCore(string text)
        {
            var js = new WebDataGridJSutility(Grid);
            //編集状態にしてエディタ取得
            while (true)
            {
                Grid.Js.ExecuteScript(js.LineGetGrid + js.ActiveCell + js.EnterEditMode);
                var e = Driver.FindElement(By.Id(Id));
                if (e.Displayed)
                {
                    break;
                }
                Thread.Sleep(10);
            }
            SetValue(text);
            //編集終了
            Grid.Js.ExecuteScript(js.LineGetGrid + js.ExitEditMode);
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
                Js.ExecuteScript($"{GetControlId()}.set_value('{text}');");
            }
        }

        protected string GetControlId()
        {
            //Idの次のタグ、但し、_clientStateは抜く。TextBoxProviderはこれだとnull
            var core = (string)Js.ExecuteScript($"return document.getElementById(\"{Id}\").firstElementChild.id;");
            return (string.IsNullOrEmpty(core)) ? string.Empty : "ig_controls." + core.Replace("_clientState", "");
        }
    }

    public static class WebDataGridEditorProviderDriverExtensions
    {
        public static WebDataGridEditorProviderDriver GetEditorProvider(this WebDataGridDriver grid, string id) => new WebDataGridEditorProviderDriver(grid, id);
    }
}