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
                                <TooltipsAppearance Visible="false"></TooltipsAppearance>
                            </telerik:ColumnSeries>
                            <telerik:ColumnSeries DataFieldY="UnitsOnOrder" Name="Units On Order">
                                <TooltipsAppearance Visible="false"></TooltipsAppearance>
                            </telerik:ColumnSeries>
                        </Series>
                        <XAxis DataLabelsField="Name" Step="1">
                            <LabelsAppearance RotationAngle="30"></LabelsAppearance>
                            <MajorGridLines Visible="false"></MajorGridLines>
                            <MinorGridLines Visible="false"></MinorGridLines>

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
                                <TooltipsAppearance Visible="false"></TooltipsAppearance>
                            </telerik:ColumnSeries>
                        </Series>
                        <XAxis DataLabelsField="CategoryName" Step="1">
                            <LabelsAppearance RotationAngle="30"></LabelsAppearance>
                            <MajorGridLines Visible="false"></MajorGridLines>
                            <MinorGridLines Visible="false"></MinorGridLines>
                        </XAxis>
                        <YAxis>
                            <TitleAppearance Text="Count"></TitleAppearance>
                            <MinorGridLines Visible="false"></MinorGridLines>
                        </YAxis>
                    </PlotArea>
                    <ChartTitle Text="Products count per category">
                    </ChartTitle>
                </telerik:RadHtmlChart>
            </td>
        </tr>
    </table>





</asp:Content>
