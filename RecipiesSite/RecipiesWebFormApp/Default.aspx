<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RecipiesWebFormApp._Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>

<%@ Register assembly="Telerik.OpenAccess.Web.40" namespace="Telerik.OpenAccess.Web" tagprefix="telerik" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

     <!-- Page Content -->
    <%--<div class="pagecontent" >
        <div class="container">
            <div class="sixteen columns">
                <h3>Inventory Management in your restaurant should be easy, accurate and most of all fast.</h3>
<p><strong> </strong><strong></strong>You need to know how your products are being used, what you have on hand, what&#8217;s being delivered, and what you need to order.</p>
<p>We know accuracy is of the utmost importance in managing food cost and expenses so the challenge is to make dealing with the details as simple as possible.</p>
<p>• Utilize a web-based FIFO <a title="Inventory Management" href="/products/inventory/ "><span style="color: #000000;">inventory management</span></a> system to track food costs<br />
• Menu Recipe Management (including versions and variations)<br />
• Suggested web-based ordering and vendor management tools</p>
<h3><strong>Time to get a handle on your <a title="An Inventory Management System can Cut Food Cost, Reduce Theft and Earn Greater Profit Margins" href="/inventory-management-systems-cut-food-cost-reduce-theft/">food costs</a>?</strong></h3>
<p><strong> </strong>For most restaurants, inventory costs are not only the largest expense-they&#8217;re the most difficult to track and control. Our inventory management solution utilizes all of the information relevant to managing your inventory: vendors, items, costs, and recipes.</p>
<p>&nbsp;</p>
                <p>Coupled with a variety of production events and direct ordering capability, the result is a more efficient and intuitive system that helps you reduce costs by identifying waste and theft, while ensuring proper item production.</p>
<p>• Food and beverage cost control<br />
• Inventory tracking and reporting<br />
• Ordering and purchase history<br />
• Recipe definitions and Costing<br />
• Menu sales and profit analysis</p>
<p>Control undetected <a title="An Inventory Management System can Cut Food Cost, Reduce Theft and Earn Greater Profit Margins" href="/inventory-management-systems-cut-food-cost-reduce-theft/"><span style="color: #000000;">food costs</span></a> with recipe management. Track and account for Recipe Costing based on essential elements, such as meal period, yield, profit per portion, purchase price and more. Utilize POS data to review the item sales to determine how the mix of sales will affect the overall theoretical cost.</p>
            </div>      
        </div>
    </div>--%>
    <!-- END Page Content -->


     <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" Runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Products" Where="UnitPrice &lt; @UnitPrice" >
         <WhereParameters>
             <asp:Parameter DefaultValue="5" Name="UnitPrice" Type="Decimal" />
         </WhereParameters>
     </telerik:OpenAccessLinqDataSource>

     <telerik:RadHtmlChart runat="server" ID="RadHtmlChart1" Width="800px" Height="500px"
               DataSourceID="OpenAccessLinqDataSource1">
               <PlotArea>
                    <Series>
                         <telerik:ColumnSeries DataFieldY="UnitsInStock" Name="Units In Stock">
                              <TooltipsAppearance Visible="false"></TooltipsAppearance>
                         </telerik:ColumnSeries>
                         <telerik:ColumnSeries DataFieldY="UnitsOnOrder" Name="Units On Order">
                              <TooltipsAppearance Visible="false"></TooltipsAppearance>
                         </telerik:ColumnSeries>
                    </Series>
                    <XAxis DataLabelsField="ProductName" Step="1">
                         <LabelsAppearance></LabelsAppearance>
                         <MajorGridLines Visible="false"></MajorGridLines>
                         <MinorGridLines Visible="false"></MinorGridLines>
                    </XAxis>
                    <YAxis>
                         <TitleAppearance Text="Units"></TitleAppearance>
                         <MinorGridLines Visible="false"></MinorGridLines>
                    </YAxis>
               </PlotArea>
               <ChartTitle Text="Products Balance">
               </ChartTitle>
          </telerik:RadHtmlChart>

</asp:Content>
