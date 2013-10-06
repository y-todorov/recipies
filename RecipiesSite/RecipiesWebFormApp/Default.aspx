<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RecipiesWebFormApp._Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <table>
        <tr>
            <td>
                <telerik:RadHtmlChart runat="server" ID="rhcLast10ModifiedProducts">
                    <PlotArea>
                        <Series>
                            <telerik:ColumnSeries DataFieldY="UnitsInStock" Name="Units In Stock">
                            </telerik:ColumnSeries>
                            <telerik:ColumnSeries DataFieldY="UnitsOnOrder" Name="Units On Order">
                            </telerik:ColumnSeries>
                        </Series>
                        <XAxis DataLabelsField="Name">
                        </XAxis>
                        <YAxis>
                            <TitleAppearance Text="Units"></TitleAppearance>
                            <MinorGridLines Visible="false"></MinorGridLines>
                        </YAxis>
                    </PlotArea>
                    <ChartTitle Text="Last 10 modified products">
                    </ChartTitle>
                </telerik:RadHtmlChart>
            </td>
            <td>
                <telerik:RadHtmlChart runat="server" ID="rhcProductsCountByCategory">
                    <PlotArea>
                        <Series>
                            <telerik:ColumnSeries DataFieldY="ProductCount" Name="Product count">
                            </telerik:ColumnSeries>
                        </Series>
                        <XAxis DataLabelsField="CategoryName">
                        </XAxis>
                        <YAxis>
                            <TitleAppearance Text="Count"></TitleAppearance>
                        </YAxis>
                    </PlotArea>
                    <ChartTitle Text="Products count per category">
                    </ChartTitle>
                </telerik:RadHtmlChart>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadHtmlChart runat="server" ID="rhcVendorsLastWeek">
                    <PlotArea>
                        <Series>
                            <telerik:ColumnSeries DataFieldY="Price" Name="Euro">
                            </telerik:ColumnSeries>
                        </Series>
                        <XAxis DataLabelsField="VendorName">
                        </XAxis>
                        <YAxis>
                            <TitleAppearance Text="Value"></TitleAppearance>
                        </YAxis>
                    </PlotArea>
                    <ChartTitle Text="Purchases per vendor last 7 days">
                    </ChartTitle>
                </telerik:RadHtmlChart>
            </td>
            <td>
                <telerik:RadHtmlChart runat="server" ID="rhcMostExpensiveProducts">
                    <PlotArea>
                        <Series>
                            <telerik:ColumnSeries DataFieldY="UnitPrice" Name="Unit Price">
                            </telerik:ColumnSeries>
                        </Series>
                        <XAxis DataLabelsField="Name">
                        </XAxis>
                        <YAxis>
                            <TitleAppearance Text="Price"></TitleAppearance>
                        </YAxis>
                    </PlotArea>
                    <ChartTitle Text="Top 10 most expensive products">
                    </ChartTitle>
                </telerik:RadHtmlChart>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadHtmlChart runat="server" ID="rhcProductsForReorder">
                    <PlotArea>
                        <Series>
                            <telerik:ColumnSeries DataFieldY="UnitsInStock" Name="Units In Stock">
                            </telerik:ColumnSeries>
                            <telerik:ColumnSeries DataFieldY="UnitsOnOrder" Name="Units On Order">
                            </telerik:ColumnSeries>
                            <telerik:ColumnSeries DataFieldY="ReorderLevel" Name="Reorder Level">
                            </telerik:ColumnSeries>
                        </Series>
                        <XAxis DataLabelsField="Name">
                            <LabelsAppearance></LabelsAppearance>
                        </XAxis>
                        <YAxis>
                            <TitleAppearance Text="Units"></TitleAppearance>
                            <MinorGridLines Visible="false"></MinorGridLines>
                        </YAxis>
                    </PlotArea>
                    <ChartTitle Text="Low products">
                    </ChartTitle>
                </telerik:RadHtmlChart>

            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
