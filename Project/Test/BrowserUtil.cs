using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace Test
{
    internal class BrowserUtil
    {
        private const string BrowserFileName = @"C:\Work\Codeer\Research\FujitsuInfragisticsJS\Test\UnitTestProject1\browser.txt";

        public enum Browser
        {
            Chrome,
            Ie,
            Firefox,
            Edge
        }

        public static Func<IWebDriver, bool> IsTitle(string title)
        {
            return (driver) =>
            {
                try
                {
                    return driver.Title.Contains(title);
                }
                catch (Exception)
                {
                    return false;
                }
            };
        }

        public static RemoteWebDriver GetDriver(Browser browser)
        {
            RemoteWebDriver driver;
            switch (browser)
            {
                case Browser.Chrome:
                    driver = new ChromeDriver();
                    break;
                case Browser.Ie:
                    //Surface4などZoomLevel200%が推奨だけど、デフォルトだと100%でないと起動しないので
                    var caps = new InternetExplorerOptions {IgnoreZoomLevel = true};
                    driver = new InternetExplorerDriver(caps);
                    break;
                case Browser.Firefox:
                    driver = new FirefoxDriver();
                    break;
                case Browser.Edge:
                    var programFiles = (Environment.Is64BitOperatingSystem) ? "%ProgramFiles(x86)%" : "%ProgramFiles%";
                    var serverPath = Path.Combine(Environment.ExpandEnvironmentVariables(programFiles), "Microsoft Web Driver");
                    var options = new EdgeOptions { PageLoadStrategy = EdgePageLoadStrategy.Eager };
                    driver = new EdgeDriver(serverPath, options);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
            }
            return driver;
        }


        public static RemoteWebDriver GetDriver()
        {
            RemoteWebDriver driver = null;
            if (!File.Exists(BrowserFileName)) return GetDriver(Browser.Firefox);

            foreach (var line in File.ReadLines(BrowserFileName))
            {
                if (line.Contains("//")) continue;

                var browserName = line.ToUpper();
                if (browserName.Contains("CHROME"))
                {
                    driver = GetDriver(Browser.Chrome);
                }
                else if (browserName.Contains("IE"))
                {
                    driver = GetDriver(Browser.Ie);
                }
                else if (browserName.Contains("FIREFOX"))
                {
                    driver = GetDriver(Browser.Firefox);
                }
                else if (browserName.Contains("EDGE"))
                {
                    driver = GetDriver(Browser.Edge);
                }
            }
            return driver ?? GetDriver(Browser.Firefox);
        }
    }
}