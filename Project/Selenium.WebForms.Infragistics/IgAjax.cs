using System.Linq;
using System.Threading;
using OpenQA.Selenium;

namespace Selenium.WebForms.Infragistics
{
    public static class IgAjax
    {
        public static void WaitForAjaxIndicator(IWebDriver driver)
        {
            var wait = driver.FindElements(By.ClassName("ig_AjaxIndicator")).FirstOrDefault();
            while (wait != null && wait.Displayed)
            {
                Thread.Sleep(10);
            }
        }
        public static void WaitForShowAjaxIndicator(IWebDriver driver)
        {
            var wait = driver.FindElements(By.ClassName("ig_AjaxIndicator")).FirstOrDefault();
            while (wait != null && !wait.Displayed)
            {
                Thread.Sleep(10);
            }
        }
    }
}
