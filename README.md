# Selenium.WebForms.Infragistics

Created by Ishikawa-Tatsuya Matsui-Bin  
[![NuGet Version and Downloads count](https://buildstats.info/nuget/Selenium.WebForms.Infragistics)](https://www.nuget.org/packages/Selenium.WebForms.Infragistics/)

What is Selenium.WebForms.Infragistics?
---
The Selenium.WebForms.Infragistics, wraps the Seleinum in C #, test libraries to handle WebDataGrid and WebHierarchicalDataGrid of Infragistics easily.

Sample Code
---
```cs  
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;

namespace Test
{
    [TestClass]
    public class TestInfragistics
    {
        FirefoxDriver _driver;

        [TestInitialize]
        public void TestInitialize()
        {
          _driver = new FirefoxDriver();
        }

        [TestCleanup]
        public void TestCleanup()
        {
          _driver.Dispose();
        }

        [TestMethod]
        public void TextBox()
        {
          var grid = new WebDataGridDriver(_driver, "MainContent__webDataGrid");
          grid.GetCell(0, 0).Edit("100");
          grid.GetCell(0, 0).Text.Is("100");
        }
    }
}
```
Corresponding control
---
Editing and text acquisition like are possible without being aware of the differences in DataField and EditorProvider by specifying the cell
- BoundDataField
```cs 
grid.GetCell(0, 0).Edit("1");
grid.GetCell(0, 0).Text.Is("1");
```
- BoundCheckBoxField
```cs 
grid.GetCell(0, 1).Edit(true);
grid.GetCell(0, 1).Value.IsTrue();
```
- DropDownProvider
```cs 
grid.GetCell(0, 2).Edit("Tomato");
grid.GetCell(0, 2).Text.Is("Tomato");
```
- DateTimeEditorProvider
```cs 
grid.GetCell(0, 3).Edit("2016/05/25");
grid.GetCell(0, 3).Text.Is("2016/05/25");
```
- DatePickerProvider
```cs 
grid.GetCell(0, 4).Edit("2016/05/25");
grid.GetCell(0, 4).Text.Is("2016/05/25");
```
- NumericEditorProvider
```cs 
grid.GetCell(0, 5).Edit("200");
grid.GetCell(0, 5).Text.Is("200");
```
- TextBoxProvider
```cs 
grid.GetCell(0, 6).Edit("TextBox");
grid.GetCell(0, 6).Text.Is("TextBox");
```
- TextEditorProvider
```cs 
grid.GetCell(0, 7).Edit("TextEditor");
grid.GetCell(0, 7).Text.Is("TextEditor");
```

