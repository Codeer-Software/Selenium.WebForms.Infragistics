using System.Linq;
using System.Threading;
using OpenQA.Selenium;

namespace Selenium.WebForms.Infragistics
{
    /// <summary>
    /// Infragistics Ajax
    /// </summary>
    public static class IgAjax
    {
        const string IndicatorClass = "ig_AjaxIndicator";

        /// <summary>
        /// Wait for Ajax indicator
        /// </summary>
        /// <param name="driver">driver for FindElements</param>
        public static void WaitForAjaxIndicator(IWebDriver driver)
        {
            var wait = driver.FindElements(By.ClassName(IndicatorClass)).FirstOrDefault();
            while (wait != null && wait.Displayed)
            {
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Wait for the display of the Ajax indicator
        /// </summary>
        /// <param name="driver">driver for FindElements</param>
        public static void WaitForShowAjaxIndicator(IWebDriver driver)
        {
            var wait = driver.FindElements(By.ClassName(IndicatorClass)).FirstOrDefault();
            while (wait != null && !wait.Displayed)
            {
                Thread.Sleep(10);
            }
        }
    }
}
