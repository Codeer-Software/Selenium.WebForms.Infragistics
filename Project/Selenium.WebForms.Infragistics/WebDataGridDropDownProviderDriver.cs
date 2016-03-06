namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridDropDownProviderDriver : WebDataGridEditorProviderDriver
    {
        public WebDataGridDropDownProviderDriver(WebDataGridDriver grid, string id)
            : base(grid, id)
        {
        }

        protected override void SetValue(string text)
        {
            var editor = Driver.SwitchTo().ActiveElement();
            editor.Clear();
            editor.SendKeys(text);
        }

        public void Edit(int index)
        {
            var value = (string)Js.ExecuteScript($"return {GetControlId()}.get_items().getItem({index}).get_text()");
            Edit(value);
        }
    }

    public static class WebDataGridDropDownProviderDriverExtensions
    {
        public static WebDataGridDropDownProviderDriver GetDropDownProvider(this WebDataGridDriver grid, string id) => new WebDataGridDropDownProviderDriver(grid, id);
    }
}