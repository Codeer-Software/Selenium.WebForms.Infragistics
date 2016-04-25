<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InfragisticsWebFormsSample._Default" %>


<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2042, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2042, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v15.2, Version=15.2.20152.2042, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.DataSourceControls" TagPrefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-6">

                    <h2>WebHierarchicalDataGrid</h2>
                    <div style="width: 100%">

                        <asp:AccessDataSource ID="acc1" runat="server" DataFile="~/App_Data/NWind.mdb"
                            DeleteCommand="DELETE FROM [Shippers] WHERE [ShipperID] = ?"
                            InsertCommand="INSERT INTO [Shippers] ([ShipperID], [CompanyName], [Phone]) VALUES (?, ?, ?)"
                            SelectCommand="SELECT * FROM [Shippers]"
                            UpdateCommand="UPDATE [Shippers] SET [CompanyName] = ?, [Phone] = ?, [Place] = ?, [Evaluation] = ? WHERE [ShipperID] = ?">
                            <DeleteParameters>
                                <asp:Parameter Name="ShipperID" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="ShipperID" Type="Int32" />
                                <asp:Parameter Name="CompanyName" Type="String" />
                                <asp:Parameter Name="Phone" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="CompanyName" Type="String" />
                                <asp:Parameter Name="Phone" Type="String" />
                                <asp:Parameter Name="Place" Type="String" />
                                <asp:Parameter Name="Evaluation" Type="Int32" />
                            </UpdateParameters>
                        </asp:AccessDataSource>
                        <asp:AccessDataSource ID="acc2" runat="server" DataFile="~/App_Data/NWind.mdb"
                            DeleteCommand="DELETE FROM [Orders] WHERE [OrderID] = ?"
                            InsertCommand="INSERT INTO [Orders] ([OrderID], [CustomerID], [EmployeeID], [OrderDate], [RequiredDate], [ShippedDate], [ShipVia], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
                            SelectCommand="SELECT * FROM [Orders]"
                            UpdateCommand="UPDATE [Orders] SET [Evaluation] = ?, [OrderDate] = ?, [ShipName] = ?, [Color] = ? WHERE [OrderID] = ?">
                            <DeleteParameters>
                                <asp:Parameter Name="OrderID" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="OrderID" Type="Int32" />
                                <asp:Parameter Name="CustomerID" Type="String" />
                                <asp:Parameter Name="EmployeeID" Type="Int32" />
                                <asp:Parameter Name="OrderDate" Type="DateTime" />
                                <asp:Parameter Name="RequiredDate" Type="DateTime" />
                                <asp:Parameter Name="ShippedDate" Type="DateTime" />
                                <asp:Parameter Name="ShipVia" Type="Int32" />
                                <asp:Parameter Name="Freight" Type="Decimal" />
                                <asp:Parameter Name="ShipName" Type="String" />
                                <asp:Parameter Name="ShipAddress" Type="String" />
                                <asp:Parameter Name="ShipCity" Type="String" />
                                <asp:Parameter Name="ShipRegion" Type="String" />
                                <asp:Parameter Name="ShipPostalCode" Type="String" />
                                <asp:Parameter Name="ShipCountry" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="OrderID" Type="Int32" />
                                <asp:Parameter Name="CustomerID" Type="String" />
                                <asp:Parameter Name="OrderDate" Type="DateTime" />
                                <asp:Parameter Name="ShipVia" Type="Int32" />
                                <asp:Parameter Name="ShipName" Type="String" />
                                <asp:Parameter Name="Evaluation" Type="Int32" />
                                <asp:Parameter Name="Color" Type="Boolean" />
                            </UpdateParameters>
                        </asp:AccessDataSource>
                        <ig:WebHierarchicalDataSource ID="WebHierarchicalDataSource1" runat="server">
                            <DataViews>
                                <ig:DataView ID="acc1_DefaultView" DataMember="DefaultView" DataSourceID="acc1" />
                                <ig:DataView ID="acc2_DefaultView" DataMember="DefaultView" DataSourceID="acc2" />
                            </DataViews>
                            <DataRelations>
                                <ig:DataRelation ChildColumns="ShipVia" ChildDataViewID="acc2_DefaultView" ParentColumns="ShipperID" ParentDataViewID="acc1_DefaultView" />
                            </DataRelations>
                        </ig:WebHierarchicalDataSource>
                        <ig:WebHierarchicalDataGrid ID="_webHierarchicalDataGrid" runat="server" Height="350px" Width="100%" AutoGenerateBands="False" AutoGenerateColumns="False" DataKeyFields="ShipperID" DataMember="acc1_DefaultView" DataSourceID="WebHierarchicalDataSource1" Key="acc1_DefaultView">
                            <Bands>
                                <ig:Band AutoGenerateColumns="False" DataKeyFields="OrderID" DataMember="acc2_DefaultView" Key="acc2_DefaultView">
                                    <Bands>
                                        <ig:Band AutoGenerateColumns="False" DataKeyFields="OrderID" DataMember="acc3_DefaultView" Key="acc3_DefaultView">
                                            <Columns>
                                                <ig:BoundDataField DataFieldName="Evaluation" Key="Evaluation">
                                                    <Header Text="評価">
                                                    </Header>
                                                </ig:BoundDataField>
                                                <ig:BoundDataField DataFieldName="OrderDate" Key="OrderDate">
                                                    <Header Text="日付">
                                                    </Header>
                                                </ig:BoundDataField>
                                                <ig:BoundDataField DataFieldName="ShipName" Key="ShipName">
                                                    <Header Text="場所">
                                                    </Header>
                                                </ig:BoundDataField>
                                                <ig:BoundCheckBoxField DataFieldName="Color" Key="Color">
                                                    <Header Text="チェック">
                                                    </Header>
                                                </ig:BoundCheckBoxField>
                                                <ig:BoundDataField DataFieldName="RequiredDate" Key="RequiredDate">
                                                    <Header Text="開始日">
                                                    </Header>
                                                </ig:BoundDataField>
                                                <ig:BoundDataField DataFieldName="ShippedDate" Key="ShippedDate">
                                                    <Header Text="終了日">
                                                    </Header>
                                                </ig:BoundDataField>
                                                <ig:BoundDataField DataFieldName="Freight" Key="Freight">
                                                    <Header Text="料金">
                                                    </Header>
                                                </ig:BoundDataField>
                                                <ig:BoundDataField DataFieldName="ShipAddress" Key="ShipAddress">
                                                    <Header Text="住所">
                                                    </Header>
                                                </ig:BoundDataField>
                                            </Columns>
                                        </ig:Band>
                                    </Bands>
                                    <GroupingSettings AscendingImageAltText="昇順画像" DescendingImageAltText="降順画像">
                                    </GroupingSettings>
                                    <Columns>
                                        <ig:BoundDataField DataFieldName="Evaluation" Key="Evaluation">
                                            <Header Text="評価">
                                            </Header>
                                        </ig:BoundDataField>
                                        <ig:BoundDataField DataFieldName="OrderDate" Key="OrderDate">
                                            <Header Text="日付">
                                            </Header>
                                        </ig:BoundDataField>
                                        <ig:BoundDataField DataFieldName="ShipName" Key="ShipName">
                                            <Header Text="場所">
                                            </Header>
                                        </ig:BoundDataField>
                                        <ig:BoundCheckBoxField DataFieldName="Color" Key="Color">
                                            <Header Text="チェック">
                                            </Header>
                                        </ig:BoundCheckBoxField>

                                        <ig:BoundDataField DataFieldName="RequiredDate" Key="RequiredDate">
                                            <Header Text="開始日">
                                            </Header>
                                        </ig:BoundDataField>
                                        <ig:BoundDataField DataFieldName="ShippedDate" Key="ShippedDate">
                                            <Header Text="終了日">
                                            </Header>
                                        </ig:BoundDataField>

                                        <ig:BoundDataField DataFieldName="Freight" Key="Freight">
                                            <Header Text="料金">
                                            </Header>
                                        </ig:BoundDataField>

                                        <ig:BoundDataField DataFieldName="ShipAddress" Key="ShipAddress">
                                            <Header Text="住所">
                                            </Header>
                                        </ig:BoundDataField>


                                    </Columns>
                                    <Behaviors>
                                        <ig:EditingCore>
                                            <Behaviors>
                                                <ig:CellEditing>
                                                    <ColumnSettings>
                                                        <ig:EditingColumnSetting ColumnKey="Evaluation" EditorID="_webHierarchicalDataGrid_DropDownProvider" />
                                                        <ig:EditingColumnSetting ColumnKey="ShipAddress" EditorID="_webHierarchicalDataGridTextBoxProvider" />
                                                        <ig:EditingColumnSetting ColumnKey="ShipName" EditorID="_webHierarchicalDataGridTextEditorProvider" />
                                                        <ig:EditingColumnSetting ColumnKey="Freight" EditorID="_webHierarchicalDataGridNumericEditorProvider" />
                                                        <ig:EditingColumnSetting ColumnKey="RequiredDate" EditorID="_webHierarchicalDataGridDateTimeEditorProvider" />
                                                        <ig:EditingColumnSetting ColumnKey="ShippedDate" EditorID="_webHierarchicalDataGridDatePickerProvider" />
                                                    </ColumnSettings>
                                                </ig:CellEditing>
                                            </Behaviors>
                                        </ig:EditingCore>
                                        <ig:Paging PageSize="5">
                                        </ig:Paging>
                                    </Behaviors>
                                </ig:Band>
                            </Bands>
                            <GroupingSettings EmptyGroupAreaText="グループ化するには、列をここへドラッグします。" AscendingImageAltText="昇順画像" DescendingImageAltText="降順画像"></GroupingSettings>
                            <Columns>
                                <ig:BoundDataField DataFieldName="Place" Key="Place">
                                    <Header Text="場所">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="CompanyName" Key="CompanyName">
                                    <Header Text="ペット">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="Phone" Key="Phone">
                                    <Header Text="電話番号">
                                    </Header>
                                </ig:BoundDataField>
                            </Columns>
                            <Behaviors>
                                <ig:EditingCore EnableInheritance="True">
                                    <EditingClientEvents RowUpdated="_webHierarchicalDataGrid_EditingClientEvents_RowUpdated" RowUpdating="_webHierarchicalDataGrid_EditingClientEvents_RowUpdating" />
                                    <Behaviors>
                                        <ig:CellEditing EnableInheritance="True">
                                            <CellEditingClientEvents
                                                EnteredEditMode="_webHierarchicalDataGrid_CellEditing_EnteredEditMode"
                                                ExitedEditMode="_webHierarchicalDataGrid_CellEditing_ExitedEditMode"
                                                EnteringEditMode="_webHierarchicalDataGrid_CellEditing_EnteringEditMode"
                                                ExitingEditMode="_webHierarchicalDataGrid_CellEditing_ExitingEditMode" />
                                        </ig:CellEditing>
                                        <ig:RowAdding>
                                        </ig:RowAdding>
                                        <ig:RowDeleting>
                                        </ig:RowDeleting>
                                    </Behaviors>
                                </ig:EditingCore>
                                <ig:Selection CellClickAction="Row" RowSelectType="Single">
                                </ig:Selection>
                                <ig:RowSelectors>
                                </ig:RowSelectors>
                                <ig:Paging PageSize="2">
                                </ig:Paging>
                                <ig:Activation>
                                    <ActivationClientEvents ActiveCellChanged="_webHierarchicalDataGrid_Activation_ActiveCellChanged" ActiveCellChanging="_webHierarchicalDataGrid_Activation_ActiveCellChanging" />
                                </ig:Activation>

                            </Behaviors>
                            <EditorProviders>
                                <ig:DropDownProvider ID="_webHierarchicalDataGrid_DropDownProvider">
                                    <EditorControl ClientIDMode="Predictable" DropDownContainerMaxHeight="200px" EnableAnimations="False" EnableDropDownAsChild="False">
                                        <Items>
                                            <ig:DropDownItem Selected="False" Text="10" Value="10">
                                            </ig:DropDownItem>
                                            <ig:DropDownItem Selected="False" Text="100" Value="100">
                                            </ig:DropDownItem>
                                        </Items>
                                    </EditorControl>
                                </ig:DropDownProvider>
                                <ig:DatePickerProvider ID="_webHierarchicalDataGridDatePickerProvider">
                                    <EditorControl ClientIDMode="Predictable">
                                    </EditorControl>
                                </ig:DatePickerProvider>
                                <ig:DateTimeEditorProvider ID="_webHierarchicalDataGridDateTimeEditorProvider">
                                    <EditorControl ClientIDMode="Predictable">
                                        <Buttons SpinButtonsDisplay="OnLeft">
                                        </Buttons>
                                    </EditorControl>
                                </ig:DateTimeEditorProvider>
                                <ig:NumericEditorProvider ID="_webHierarchicalDataGridNumericEditorProvider">
                                    <EditorControl ClientIDMode="Predictable">
                                    </EditorControl>
                                </ig:NumericEditorProvider>
                                <ig:TextEditorProvider ID="_webHierarchicalDataGridTextEditorProvider">
                                    <EditorControl ClientIDMode="Predictable">
                                        <Buttons CustomButtonDisplay="OnLeft" SpinButtonsDisplay="OnRight">
                                        </Buttons>
                                    </EditorControl>
                                </ig:TextEditorProvider>
                                <ig:TextBoxProvider ID="_webHierarchicalDataGridTextBoxProvider">
                                    <EditorControl ClientIDMode="Predictable">
                                    </EditorControl>
                                </ig:TextBoxProvider>
                            </EditorProviders>
                        </ig:WebHierarchicalDataGrid>
                        <asp:Button ID="UpdateButton" runat="server" Text="更新" />
                        <input id="button1" type="button" value="Debug" onclick="AddColmns(new Array('氏名', '出身地', new Date(2015, 11, 30)))" />
                    </div>
                </div>
                <div class="col-md-6">
                    <h2>WebDataGrid</h2>
                    <ig:WebDataGrid ID="_webDataGrid" runat="server" AutoGenerateColumns="False" Height="350px" Width="100%" DataKeyFields="Id" TabIndex="1">
                        <Columns>
                            <ig:BoundDataField DataFieldName="Id" Key="Id" CssClass="MyCell">
                                <Header Text="点数" CssClass="MyCell">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="LastName" Key="LastName">
                                <Header Text="苗字">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundCheckBoxField DataFieldName="Support" Key="Support">
                                <Header Text="扶養">
                                </Header>
                            </ig:BoundCheckBoxField>
                            <ig:BoundDataField DataFieldName="Tel" Key="Tel">
                                <Header Text="電話">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Sex" Key="Sex">
                                <Header Text="性別">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="LastLoginDate" Key="LastLoginDate">
                                <Header Text="最終ログイン時間">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="FirstLoginDate" Key="FirstLoginDate">
                                <Header Text="初回ログイン時間">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Age" Key="Age">
                                <Header Text="年齢">
                                </Header>
                            </ig:BoundDataField>
                        </Columns>
                        <EditorProviders>
                            <ig:DropDownProvider ID="_webDataGridDropDownProvider">
                                <EditorControl ClientIDMode="Predictable" DropDownContainerMaxHeight="200px" EnableAnimations="False" EnableDropDownAsChild="False">
                                    <Items>
                                        <ig:DropDownItem Selected="False" Text="10" Value="10">
                                        </ig:DropDownItem>
                                        <ig:DropDownItem Selected="False" Text="100" Value="100">
                                        </ig:DropDownItem>
                                    </Items>
                                </EditorControl>
                            </ig:DropDownProvider>
                            <ig:DatePickerProvider ID="_webDataGrid_DatePickerProvider1">
                                <EditorControl ClientIDMode="Predictable">
                                </EditorControl>
                            </ig:DatePickerProvider>
                            <ig:DateTimeEditorProvider ID="_webDataGrid_DateTimeEditorProvider1">
                                <EditorControl ClientIDMode="Predictable">
                                    <Buttons SpinButtonsDisplay="OnRight">
                                    </Buttons>
                                </EditorControl>
                            </ig:DateTimeEditorProvider>
                            <ig:NumericEditorProvider ID="_webDataGrid_NumericEditorProvider1">
                                <EditorControl ClientIDMode="Predictable">
                                    <Buttons SpinButtonsDisplay="OnRight">
                                    </Buttons>
                                </EditorControl>
                            </ig:NumericEditorProvider>
                            <ig:TextBoxProvider ID="_webDataGridTextBoxProvider">
                                <EditorControl ClientIDMode="Predictable"></EditorControl>
                            </ig:TextBoxProvider>
                            <ig:TextEditorProvider ID="_webDataGrid_TextEditorProvider1">
                                <EditorControl ClientIDMode="Predictable" TextMode="MultiLine">
                                    <Buttons SpinButtonsDisplay="OnRight">
                                    </Buttons>
                                </EditorControl>
                            </ig:TextEditorProvider>
                        </EditorProviders>
                        <Behaviors>
                            <ig:EditingCore>
                                <Behaviors>
                                    <ig:RowAdding>
                                    </ig:RowAdding>
                                    <ig:CellEditing>
                                        <ColumnSettings>
                                            <ig:EditingColumnSetting ColumnKey="Id" EditorID="_webDataGridDropDownProvider"  />
                                            <ig:EditingColumnSetting ColumnKey="Tel" EditorID="_webDataGridTextBoxProvider" />
                                            <ig:EditingColumnSetting ColumnKey="Sex" EditorID="_webDataGrid_TextEditorProvider1" />
                                            <ig:EditingColumnSetting ColumnKey="Age" EditorID="_webDataGrid_NumericEditorProvider1" />
                                            <ig:EditingColumnSetting ColumnKey="LastLoginDate" EditorID="_webDataGrid_DateTimeEditorProvider1" />
                                            <ig:EditingColumnSetting ColumnKey="FirstLoginDate" EditorID="_webDataGrid_DatePickerProvider1" />
                                        </ColumnSettings>
                                    </ig:CellEditing>
                                </Behaviors>
                            </ig:EditingCore>
                            <ig:Activation>
                            </ig:Activation>
                            <ig:Sorting AscendingImageAltText="昇順画像" DescendingImageAltText="降順画像">
                            </ig:Sorting>
                            <ig:ColumnFixing>
                            </ig:ColumnFixing>
                            <ig:Filtering>
                            </ig:Filtering>
                            <ig:Paging PageSize="2">
                            </ig:Paging>
                            <ig:ColumnMoving>
                            </ig:ColumnMoving>
                        </Behaviors>
                    </ig:WebDataGrid>
                </div>

                <script type="text/javascript">

                    function _webHierarchicalDataGrid_RowEdit_EnteringEditMode(sender, eventArgs) {
                        console.log("RowEdit_EnteringEdit");
                    }

                    function _webHierarchicalDataGrid_RowEdit_EnteringEditMode(sender, eventArgs) {
                        console.log("RowEdit_EnteringEdit");
                    }
                    function _webHierarchicalDataGrid_RowEdit_ExitingEditMode(sender, eventArgs) {
                        console.log("RowEdit_ExitingEdit");
                    }

                    function _webHierarchicalDataGrid_CellEditing_EnteredEditMode(sender, eventArgs) {
                        console.log("CellEditing_EnteredEditMode");
                    }
                    function _webHierarchicalDataGrid_CellEditing_ExitedEditMode(sender, eventArgs) {
                        console.log("CellEditing_ExitedEditMode");
                    }
                    function _webHierarchicalDataGrid_CellEditing_EnteringEditMode(sender, eventArgs) {
                        console.log("CellEditing_EnteringEditMode");
                    }
                    function _webHierarchicalDataGrid_CellEditing_ExitingEditMode(sender, eventArgs) {
                        console.log("CellEditing_ExitingEditMode");
                    }
                    function _webHierarchicalDataGrid_Activation_ActiveCellChanged(sender, eventArgs) {
                        console.log("Activation_ActiveCellChanged");
                    }
                    function _webHierarchicalDataGrid_Activation_ActiveCellChanging(sender, eventArgs) {
                        console.log("Activation_ActiveCellChanging");
                    }
                    function _webHierarchicalDataGrid_EditingClientEvents_RowUpdated(sender, eventArgs) {
                        console.log("EditingClientEvents_RowUpdated");
                    }
                    function _webHierarchicalDataGrid_EditingClientEvents_RowUpdating(sender, eventArgs) {
                        console.log("EditingClientEvents_RowUpdating");
                    }
                    function AddColmns(data) {

                        var grid = $find("MainContent__webDataGrid");


                        var bg2 = grid.get_rows().get_row(0).get_cell(0).get_element().className;

                        //$(".myClass").css("border", "3px solid red");


                        //MyCell ig2b0a1c24 "background-color"


                        //var element = $(".MyCell ig2b0a1c24");

                        var element = document.getElementsByClassName("MyCell ig2b0a1c24");

                        var style = (element[0].currentStyle || document.defaultView.getComputedStyle(element[0], null));


                        //var border = $(".MyCell").css("border", "3px solid red");
                        alert("2" + style);
                        alert("2" + style.backgroundColor);

                        //className
                        // dp.set_value(input.value);

                        //var id1 = document.getElementById('MainContent__webDataGrid__webDataGrid_NumericEditorProvider1').firstElementChild.id;
                        //alert(id1);


                        //focus();
                        //var grid = $find("MainContent__webDataGrid");
                        //grid.get_element().focus();
                        //var activeCell = grid.get_behaviors().get_activation().get_activeCell();
                        //grid.get_behaviors().get_editingCore().get_behaviors().get_cellEditing().enterEditMode(activeCell);

                        //var grid = $find("MainContent__webHierarchicalDataGrid");
                        //grid.get_behaviors().get_activation().set_activeCell(grid.get_gridView().get_rows().get_row(2).get_cell(1));
                    }
                </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

