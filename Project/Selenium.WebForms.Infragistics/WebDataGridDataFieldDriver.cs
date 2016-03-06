using System.Threading;
using OpenQA.Selenium;
using Selenium.WebForms.Infragistics.Inside;

namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridDataFieldDriver
    {
        public WebDataGridDriver Grid { get; }

        public WebDataGridDataFieldDriver(WebDataGridDriver grid)
        {
            Grid = grid;
        }

        public void Edit(string text)
        {
            var js = new WebDataGridJSutility(Grid);
            //編集状態にしてエディタ取得
            IWebElement editor;
            while (true)
            {
                Grid.Js.ExecuteScript(js.LineGetGrid + js.ActiveCell + js.EnterEditMode);
                editor = Grid.Driver.FindElement(By.Id(Grid.Id)).FindElement(By.ClassName("igg_EditCell"));
                if (editor.Displayed)
                {
                    break;
                }
                Thread.Sleep(10);
            }

            //テキスト変更
            editor.Clear();
            editor.SendKeys(text);

            //編集終了
            Grid.Js.ExecuteScript(js.LineGetGrid + js.ExitEditMode);
        }
    }

    public static class WebDataGridDataFieldDriverExtensions
    {
        public static WebDataGridDataFieldDriver GetDataField(this WebDataGridDriver grid) => new WebDataGridDataFieldDriver(grid);
    }
}