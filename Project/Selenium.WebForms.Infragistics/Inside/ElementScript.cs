using OpenQA.Selenium;
using Selenium.StandardControls.Inside;

namespace Selenium.WebForms.Infragistics.Inside
{
    internal class ElementScript : IElementCore
    {
        private IWebDriver Driver { get; }

        private IJavaScriptExecutor Js => (IJavaScriptExecutor)Driver;
        private string Script { get; }

        internal ElementScript(IWebDriver driver, string elementScript)
        {
            Driver = driver;
            Script = elementScript;
        }

        public T GetAttribute<T>(string name)
        {
            var script = $"{Script}return element.{name}";
            return (T)Js.ExecuteScript(script);
        }

        public string GetCssValue(string name)
        {
            var script = $"{Script}return (element.currentStyle || document.defaultView.getComputedStyle(element, null)).{name};";
            return (string)Js.ExecuteScript(script);
        }
    }
}