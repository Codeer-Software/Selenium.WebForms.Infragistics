namespace Selenium.WebForms.Infragistics
{
    public class DropDownEditorProviderDriver : EditorProviderDriver
    {
        public DropDownEditorProviderDriver(WebDataGridDriver grid, string id)
            : base(grid, id)
        {
        }

        protected override void SetValue(string text)
        {
            var editor = Driver.SwitchTo().ActiveElement();
            editor.Clear();
            editor.SendKeys(text);
        }
        public void EmualteEdit(int index)
        {
            var script = $"return {GetControlId()}.get_items().getItem({index}).get_text()";
            var value = (string)Js.ExecuteScript(script);
            EmualteEdit(value);
        }
    }
    public static class DropDownEditorProviderDriverExtensions
    {
        public static DropDownEditorProviderDriver GetDropDownEditorProvider(this WebDataGridDriver grid, string id) => new DropDownEditorProviderDriver(grid, id);
    }
}
