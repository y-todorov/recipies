﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="RecipiesWebFormApp.HumanResources.Employee" %>
<%@ Register assembly="Telerik.OpenAccess.Web.40" namespace="Telerik.OpenAccess.Web" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Assembly="YordanCustomControls" Namespace="YordanCustomControls" TagPrefix="yordan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceEmployee" Runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Employees" />
    <yordan:YordanCustomRadGrid ID="rgEmployee" runat="server" CellSpacing="0" DataSourceID="OpenAccessLinqDataSourceEmployee" GridLines="None">
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="EmployeeID" DataSourceID="OpenAccessLinqDataSourceEmployee">
            <Columns>
                <telerik:GridBoundColumn DataField="EmployeeID" DataType="System.Int32" FilterControlAltText="Filter EmployeeID column" HeaderText="EmployeeID" ReadOnly="True" SortExpression="EmployeeID" UniqueName="EmployeeID">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LastName" FilterControlAltText="Filter LastName column" HeaderText="LastName" SortExpression="LastName" UniqueName="LastName">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FirstName" FilterControlAltText="Filter FirstName column" HeaderText="FirstName" SortExpression="FirstName" UniqueName="FirstName">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn> 
                <telerik:GridDateTimeColumn DataField="BirthDate" DataType="System.DateTime" FilterControlAltText="Filter BirthDate column" HeaderText="BirthDate" SortExpression="BirthDate" UniqueName="BirthDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="HireDate" DataType="System.DateTime" FilterControlAltText="Filter HireDate column" HeaderText="HireDate" SortExpression="HireDate" UniqueName="HireDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="Address" FilterControlAltText="Filter Address column" HeaderText="Address" SortExpression="Address" UniqueName="Address">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="City" FilterControlAltText="Filter City column" HeaderText="City" SortExpression="City" UniqueName="City">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Region" FilterControlAltText="Filter Region column" HeaderText="Region" SortExpression="Region" UniqueName="Region">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PostalCode" FilterControlAltText="Filter PostalCode column" HeaderText="PostalCode" SortExpression="PostalCode" UniqueName="PostalCode">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Country" FilterControlAltText="Filter Country column" HeaderText="Country" SortExpression="Country" UniqueName="Country">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="HomePhone" FilterControlAltText="Filter HomePhone column" HeaderText="HomePhone" SortExpression="HomePhone" UniqueName="HomePhone">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Extension" FilterControlAltText="Filter Extension column" HeaderText="Extension" SortExpression="Extension" UniqueName="Extension">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PreviousExperience" FilterControlAltText="Filter PreviousExperience column" HeaderText="PreviousExperience" SortExpression="PreviousExperience" UniqueName="PreviousExperience">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ModifiedDate" ReadOnly="true" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="ModifiedByUser" ReadOnly="true" FilterControlAltText="Filter ModifiedByUser column" HeaderText="ModifiedByUser" SortExpression="ModifiedByUser" UniqueName="ModifiedByUser">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </yordan:YordanCustomRadGrid>
</asp:Content>
