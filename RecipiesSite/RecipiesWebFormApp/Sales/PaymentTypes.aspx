﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentTypes.aspx.cs" Inherits="RecipiesWebFormApp.Sales.PaymentTypes" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register assembly="YordanCustomControls" namespace="YordanCustomControls" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <cc1:YordanCustomRadGrid ID="rgPaymentTypes" runat="server" CellSpacing="0" DataSourceID="OpenAccessLinqDataSourcePaymentTypes" GridLines="None">
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="PaymentTypeId" DataSourceID="OpenAccessLinqDataSourcePaymentTypes">
        <Columns>
            <telerik:GridBoundColumn DataField="PaymentTypeId" DataType="System.Int32" FilterControlAltText="Filter PaymentTypeId column" HeaderText="PaymentTypeId" ReadOnly="True" SortExpression="PaymentTypeId" UniqueName="PaymentTypeId">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ModifiedDate" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ModifiedByUser" FilterControlAltText="Filter ModifiedByUser column" HeaderText="ModifiedByUser" SortExpression="ModifiedByUser" UniqueName="ModifiedByUser">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
</cc1:YordanCustomRadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourcePaymentTypes" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="PaymentTypes">
    </telerik:OpenAccessLinqDataSource>
</asp:Content>
