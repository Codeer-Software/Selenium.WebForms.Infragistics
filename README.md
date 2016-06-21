# Selenium.WebForms.Infragistics

Created by Ishikawa-Tatsuya Matsui-Bin  
[![NuGet Version and Downloads count](https://buildstats.info/nuget/Selenium.WebForms.Infragistics)](https://www.nuget.org/packages/Selenium.WebForms.Infragistics/)
 [![Build status](https://ci.appveyor.com/api/projects/status/kjp5m07it442ltyc?svg=true)](https://ci.appveyor.com/project/binnmti/selenium-webforms-infragistics)

What is Selenium.WebForms.Infragistics?
---
- Wrapped test library selenium in C#
- You can use the control of Infragistics to simple

![image](/image.png)

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
![WebDataGrid](http://www.infragistics.com/media/41596/webdatagrid-multi-footer.png)

- WebHierarchicalDataGrid  
![WebHierarchicalDataGrid](http://www.infragistics.com/media/41600/whdg-multi-footer.png)

Cell
---
![Cell_Editing](http://help.infragistics.com/Help/Doc/ASPNET/2012.2/CLR4.0/html/images/WebDataGrid_Enabling_Cell_Editing_01.png)
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
- Edit start enum
```cs 
public enum EditStartMode
{
    JavaScript,     //To edit in the JavaScript of EnterEditMode
    SingleClick,    //To edit in SingleClick
    DoubleClick,    //To edit in DoubleClick
    F2,             //To edit in F2 Key
}
```
Editor Providers
---
![Editor Providers](http://www.infragistics.com/help/aspnet/images/webhierarchicaldatagrid_using_editor_providers_01.png
)
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
Cell finishEditing
---
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

Column
---
![Sort](http://help.infragistics.com/Help/Doc/ASPNET/2012.2/CLR4.0/html/images/WebDataGrid_Sort_Columns_in_Code_01.png)
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

Element & ElementInfo([Detail](https://github.com/Codeer-Software/Selenium.StandardControls/blob/master/README.md#about-element--elementinfo))
---
```cs 
grid.GetCell(0, 0).Element.Click();
grid.GetColumn(1).Element.SendKeys(Keys.Enter);
grid.GetActiveCellElement().Text.Is("AA");

grid.GetCell(0, 0).Info.InnerHtml.Is("1");
grid.GetCell(0, 0).Info.FontBold.IsFalse();
grid.GetCell(0, 0).Info.BackGroundColor.Is("rgba(219, 112, 147, 1)");
grid.GetCell(0, 0).Info.TextAlign.Is("left");
```

WebHierarchicalDataGrid
---
![WebHierarchicalDataGrid](http://www.infragistics.com/media/41600/whdg-multi-footer.png)
- Basic
```cs 
var grid = new WebHierarchicalDataGridDriver(_driver, "MainContent__webHierarchicalDataGrid");
grid.GetCell(0, 0).Edit("100");
grid.GetCell(0, 0).Text.Is("100");
```
- Hierarchy
```cs 
var childGrid = grid.GetRowIslands(0, 0, 0);
childGrid.SetExpanded(true);    //Can not edit not open
childGrid.GetCell.GetCell(0, 0).Edit("100");
childGrid.GetCell(0, 0).Text.Is("100");
```

Wait for Ajax indicator
---
```cs 
IgAjax.WaitForAjaxIndicator(Driver);
```
Author Info
---
Ishikawa-Tatsuya & Matsui-Bin is a software developer in Japan at Codeer, Inc.  
Ishikawa-Tatsuya & Matsui-Bin is awarding Microsoft MVP.

License
---
This library is under the Apache License.
