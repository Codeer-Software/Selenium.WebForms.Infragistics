namespace Selenium.WebForms.Infragistics
{
    public class WebDataGridEditorProviderFirefoxDriver : WebDataGridEditorProviderDriver
    {
        private readonly string _igControls;

        /// <summary>
        ///
        /// </summary>
        /// <param name="webDataGrid"></param>
        /// <param name="igControls">ig_controls.WebDataGrid1_ctlxx</param>
        //http://blogs.jp.infragistics.com/blogs/yuki-mita/archive/2014/05/13/26540.aspx
        public WebDataGridEditorProviderFirefoxDriver(WebDataGridDriver webDataGrid, string id, string igControls) : base(webDataGrid, id)
        {
            _igControls = igControls;
        }

        protected override void SetValue(string text)
        {
            Js.ExecuteScript($"{_igControls}.set_value('{text}');");
        }
    }

    public static class WebDataGridEditorProviderFirefoxDriverExtensions
    {
        public static WebDataGridEditorProviderFirefoxDriver GetEditorProvider(this WebDataGridDriver grid, string id, string igControls) => new WebDataGridEditorProviderFirefoxDriver(grid, id, igControls);
    }
}