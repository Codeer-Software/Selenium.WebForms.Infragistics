# Selenium.WebForms.Infragistics

Created by Ishikawa-Tatsuya Matsui-Bin  
[![NuGet Version and Downloads count](https://buildstats.info/nuget/Selenium.WebForms.Infragistics)](https://www.nuget.org/packages/Selenium.WebForms.Infragistics/)

What is Selenium.WebForms.Infragistics?
---
The Selenium.WebForms.Infragistics, wraps the Seleinum in C #, test libraries to handle the control of Infragistics easily.

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
- WebDataGrid
- WebHierarchicalDataGrid

WebDataGrid cell editing
---
Specify the columns and rows , without being aware of the differences between the DataField and EditorProvider, it can be edited and acquisition
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

Edit start specified in the enum
 - JavaScript - To edit in the JavaScript of EnterEditMode
 - SingleClick - To edit in SingleClick
 - DoubleClick - To edit in DoubleClick
 - F2 - To edit in F2 Key


Use in does not work normally the case and not the standby after the editing of each cell . Since the part that depends on the screen , there is no single answer , such as may be wait unconditionally 5 seconds . Where it has been making that can last in the Action to set the cell editing.

Example: Send the Enter key until no exception
```cs 
grid.GetCell(0, 0).Edit("abc", WebDataGridCellDriver.EditStartMode.F2, (e) =>
{
    while (true)
    {
        try
        {   
            e.SendKeys(Keys.Enter);
            break;
        }
        catch
        {
        }
        Thread.Sleep(100);
    }
});
```

WebDataGrid Column
---
Operations such as sorting and filtering not only the acquisition of the column name is also possible

- Column Header Name
```cs 
grid.GetColumn(1).Text.Is("Name");
```
- Column Sort
```cs 
grid.GetColumn(0).SetSort();
grid.GetCell(0, 0).Text.Is("10");
grid.GetColumn(0).SetSort(SortType.Descending);
grid.GetCell(0, 0).Text.Is("1");
```
- Column Filter
```cs 
grid.GetCell(0, 1).Text.Is("Ishikawa");
_webDataGrid.GetColumn(1).SetFilter(1, "Ishikawa");
grid.GetCell(0, 1).Text.Is("Matsui");
```
- Column Fixed
```cs 
_webDataGrid.GetColumn(2).SetFixed(true);
_webDataGrid.GetColumn(2).SetFixed(false);
```
