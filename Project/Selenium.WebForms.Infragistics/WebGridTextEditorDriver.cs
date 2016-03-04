using System.Threading;
using OpenQA.Selenium;

namespace Selenium.WebForms.Infragistics
{
    public class WebGridTextEditorDriver
    {
        public WebDataGridDriver Grid { get; }

        public WebGridTextEditorDriver(WebDataGridDriver grid)
        {
            Grid = grid;
        }

        public void EmualteEdit(string text)
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

    public static class WebGridTextEditorDriverExtensions
    {
        public static WebGridTextEditorDriver GetTextEditor(this WebDataGridDriver grid) => new WebGridTextEditorDriver(grid);
    }
}