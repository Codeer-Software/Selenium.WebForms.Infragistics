namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridDropDownProviderDriver : WebDataGridEditorProviderDriver
    {
        #region Constructors

        public WebDataGridDropDownProviderDriver(WebDataGridDriver webDataGrid, string id)
            : base(webDataGrid, id)
        {
        }

        #endregion Constructors

        #region Methods

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

        #endregion Methods
    }

    public static class WebDataGridDropDownProviderDriverExtensions
    {
        public static WebDataGridDropDownProviderDriver GetDropDownProvider(this WebDataGridDriver grid, string id) => new WebDataGridDropDownProviderDriver(grid, id);
    }
}