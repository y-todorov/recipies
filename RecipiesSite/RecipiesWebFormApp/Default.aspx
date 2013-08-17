<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RecipiesWebFormApp._Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>

<%@ Register assembly="Telerik.OpenAccess.Web.40" namespace="Telerik.OpenAccess.Web" tagprefix="telerik" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

     <telerik:RadHtmlChart runat="server" ID="RadHtmlChart1" Width="487px" Height="264px"          >
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
                         <LabelsAppearance></LabelsAppearance>
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
</asp:Content>
