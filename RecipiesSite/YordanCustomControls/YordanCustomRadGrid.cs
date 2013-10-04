﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Helpers.Extensions;
using System.Diagnostics;
using System.Drawing;
using RecipiesWebFormApp;
using Microsoft.AspNet.SignalR;
using System.Reflection;
using Telerik.OpenAccess.Web;
using RecipiesModelNS;

namespace YordanCustomControls
{
    public class YordanCustomRadGrid : RadGrid
    {
        protected override void OnItemCreated(GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                if (ScriptManager.GetCurrent(this.Page) != null)
                {
                    Button exportToExcelButton = (e.Item as GridCommandItem).FindControl("ExportToExcelButton") as Button;
                    if (exportToExcelButton != null)
                    {
                        exportToExcelButton.Click += exportButton_Click;
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(exportToExcelButton);
                    }

                    Button exportToPdfButton = (e.Item as GridCommandItem).FindControl("ExportToPdfButton") as Button;
                    if (exportToPdfButton != null)
                    {
                        exportToPdfButton.Click += exportButton_Click;
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(exportToPdfButton);
                    }

                    Button exportToCsvButton = (e.Item as GridCommandItem).FindControl("ExportToCsvButton") as Button;
                    if (exportToCsvButton != null)
                    {
                        exportToCsvButton.Click += exportButton_Click;
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(exportToCsvButton);
                    }

                    Button exportToWordButton = (e.Item as GridCommandItem).FindControl("ExportToWordButton") as Button;
                    if (exportToWordButton != null)
                    {
                        exportToWordButton.Click += exportButton_Click;
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(exportToWordButton);
                    }
                }
            }
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                if (Columns.FindByUniqueNameSafe("Download") != null)
                {
                    TableCell tableCell = dataItem["Download"];
                    if (tableCell != null)
                    {
                        foreach (Control control in tableCell.Controls)
                        {
                            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(control);
                            //Page.ClientScript.RegisterStartupScript();
                        }
                    }
                }
            }
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem editedItem = (e.Item as GridEditableItem);

                foreach (GridColumn col in Columns)
                {
                    if (col.UniqueName != "AutoGeneratedDeleteColumn" && col.UniqueName != "AutoGeneratedEditColumn" && col.IsEditable &&
                        !(col is GridCalculatedColumn))
                    {

                        TableCell cell = editedItem[col];
                        foreach (Control c in cell.Controls)
                        {
                            RadComboBox radComboBox = c as RadComboBox;
                            if (radComboBox != null)
                            {
                                //radComboBox.MaxHeight = 500;
                                //radComboBox.MaxLength = 300;
                                radComboBox.MaxHeight = 300;
                                radComboBox.DropDownAutoWidth = RadComboBoxDropDownAutoWidth.Enabled;
                                radComboBox.MarkFirstMatch = true;                                
                            }
                        }
                    }
                }


            }

            base.OnItemCreated(e);
        }
       
        void radComboBox_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
           
        }

        void exportButton_Click(object sender, EventArgs e)
        {
            // This is important so we do not cut text when exporting info from the grid.
            ViewState.Add("isExporting", true);
        }

        protected override void OnItemDataBound(GridItemEventArgs e)
        {
            GridDataItem gdi = e.Item as GridDataItem;
            if (gdi != null)
            {
                foreach (GridColumn gc in Columns)
                {

                    // for now every column will be trimmed and shown with tooltips. 
                    // If we want to exclude a column from being trimmed we have to add it here
                    if (!(gc is GridButtonColumn || gc is GridEditCommandColumn || gc is GridDropDownColumn || gc is GridCheckBoxColumn))
                    {
                        gdi[gc].ToolTip = HtmlToText.ConvertHtml(gdi[gc].Text);

                        if (ViewState["isExporting"] == null)
                        {
                            gdi[gc].Text = gdi[gc].Text.TrimToLength();
                        }

                        if (gdi.DataItem.GetType() == typeof(Product))
                        {
                            GridBoundColumn gbc = gc as GridBoundColumn;
                            Product product = (Product)gdi.DataItem;


                            if (gbc.DataField.Equals("UnitPrice", StringComparison.InvariantCultureIgnoreCase))
                            {
                                string unitPrice = string.Empty;
                                try
                                {
                                    product.GetAveragePriceLastDays(14, out unitPrice);
                                }
                                catch (Exception ex)
                                {
                                    string error = "Error: " + ex.Message;
                                    gdi[gc].ToolTip = error;
                                    (Page.Master as dynamic).MasterRadNotification.Show(error);
                                    continue;
                                }
                                gdi[gc].ToolTip = unitPrice;
                            }



                            if (gbc != null)
                            {
                                if (product.UnitsOnOrder > 0)
                                {
                                    if (gbc.DataField.Equals("UnitsOnOrder", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        gdi[gc].BackColor = Color.Aqua;
                                    }
                                    //gdi.BackColor = Color.Aqua;                                
                                }
                                if (product.UnitsInStock <= product.ReorderLevel)
                                {
                                    if (gbc.DataField.Equals("UnitsInStock", StringComparison.InvariantCultureIgnoreCase) ||
                                        gbc.DataField.Equals("ReorderLevel", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        gdi[gc].BackColor = Color.Yellow;
                                    }
                                    //gdi.BackColor = Color.YellowGreen; ;
                                }
                            }
                        }




                    }
                }
            }
            GridGroupHeaderItem gghi = e.Item as GridGroupHeaderItem;
            if (gghi != null)
            {
                gghi.DataCell.ToolTip = HtmlToText.ConvertHtml(gghi.DataCell.Text);

                if (ViewState["isExporting"] == null)
                {
                    gghi.DataCell.Text = gghi.DataCell.Text.TrimToLength();
                }
            }
            base.OnItemDataBound(e);
        }

        protected override void OnLoad(EventArgs e)
        {


            GridEditableColumn modifiedDateColumn = Columns.FindByUniqueNameSafe("ModifiedDate") as GridEditableColumn;
            if (modifiedDateColumn != null)
            {
                modifiedDateColumn.ReadOnly = true;
            }
            GridEditableColumn modifiedByUserColumn = Columns.FindByUniqueNameSafe("ModifiedByUser") as GridEditableColumn;
            if (modifiedByUserColumn != null)
            {
                modifiedByUserColumn.ReadOnly = true;
            }

            // setting default columns properties that cannot be set through skin file
            foreach (GridColumn gridColumn in Columns)
            {
                GridBoundColumn gridBoundColumn = gridColumn as GridBoundColumn;
                if (gridBoundColumn != null)
                {
                    if (gridBoundColumn.MaxLength == 0)
                    {
                        gridBoundColumn.MaxLength = 1000;
                    }
                    if (gridBoundColumn.DataType == typeof(DateTime))
                    {
                        if (string.IsNullOrEmpty(gridBoundColumn.DataFormatString) && gridBoundColumn != modifiedDateColumn)
                        {
                            gridBoundColumn.DataFormatString = "{0:dd/MM/yyyy}";
                        }
                        // validation
                        gridBoundColumn.ColumnValidationSettings.EnableRequiredFieldValidation = true;
                        gridBoundColumn.ColumnValidationSettings.RequiredFieldValidator.Text = "This field is required!";
                        gridBoundColumn.ColumnValidationSettings.RequiredFieldValidator.ErrorMessage = "This field is required!";
                        gridBoundColumn.ColumnValidationSettings.RequiredFieldValidator.ForeColor = Color.Red;
                    }
                    if (gridBoundColumn.DataType == typeof(decimal))
                    {
                        if (string.IsNullOrEmpty(gridBoundColumn.DataFormatString))
                        {
                            gridBoundColumn.DataFormatString = "{0:C3}";
                        }
                        if (gridBoundColumn.Aggregate == GridAggregateFunction.None)
                        {
                            gridBoundColumn.Aggregate = GridAggregateFunction.Sum;
                        }
                    }
                    if (gridBoundColumn.DataType == typeof(double) || gridBoundColumn.DataType == typeof(float))
                    {
                        if (string.IsNullOrEmpty(gridBoundColumn.DataFormatString))
                        {
                            gridBoundColumn.DataFormatString = "{0:F3}";
                        }
                        if (gridBoundColumn.Aggregate == GridAggregateFunction.None)
                        {
                            gridBoundColumn.Aggregate = GridAggregateFunction.Sum;
                        }
                    }

                    GridNumericColumn gridNumericColumn = gridBoundColumn as GridNumericColumn;
                    if (gridNumericColumn != null)
                    {
                        gridNumericColumn.DecimalDigits = 3;
                    }

                }
                GridDropDownColumn gridDropDownColumn = gridColumn as GridDropDownColumn;
                if (gridDropDownColumn != null)
                {
                    if (string.IsNullOrEmpty(gridDropDownColumn.EmptyListItemText))
                    {
                        gridDropDownColumn.EmptyListItemText = string.Empty;
                    }
                    if (string.IsNullOrEmpty(gridDropDownColumn.EmptyListItemValue))
                    {
                        gridDropDownColumn.EmptyListItemValue = null;
                    }
                    // because we have validation now
                    gridDropDownColumn.EnableEmptyListItem = false;
                    gridDropDownColumn.ConvertEmptyStringToNull = true;
                    gridDropDownColumn.DropDownControlType = GridDropDownColumnControlType.RadComboBox;
                    // Validation

                    gridDropDownColumn.ColumnValidationSettings.EnableRequiredFieldValidation = true;
                    gridDropDownColumn.ColumnValidationSettings.RequiredFieldValidator.Text = "This field is required!";
                    gridDropDownColumn.ColumnValidationSettings.RequiredFieldValidator.ErrorMessage = "This field is required!";
                    gridDropDownColumn.ColumnValidationSettings.RequiredFieldValidator.ForeColor = Color.Red;

                    // load on demand
                    
                    //gridDropDownColumn.AllowAutomaticLoadOnDemand = true;
                    //gridDropDownColumn.AllowVirtualScrolling = true;
                    //gridDropDownColumn.ShowMoreResultsBox = true;
                    //gridDropDownColumn.ItemsPerRequest = 7;
                    

                }
            }

            base.OnLoad(e);
        }

        protected override void OnColumnCreated(GridColumnCreatedEventArgs e)
        {
            if (e.Column.UniqueName.Equals("AutoGeneratedDeleteColumn"))
            {
                GridButtonColumn autoGeneratedDeleteColumn = e.Column as GridButtonColumn;
                autoGeneratedDeleteColumn.ConfirmText = "Delete this record?";
                autoGeneratedDeleteColumn.ConfirmDialogType = GridConfirmDialogType.RadWindow;
                autoGeneratedDeleteColumn.ConfirmTitle = "Delete";
            }
            base.OnColumnCreated(e);
        }

        protected override void OnDataBound(EventArgs e)
        {
            base.OnDataBound(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            ViewState.Remove("isExporting");
            if (this.MasterTableView.Items.Count < MasterTableView.PageSize)
            {              
                this.ClientSettings.Scrolling.AllowScroll = false;
                //this.Height = new Unit(600, UnitType.Pixel);
                //this.ClientSettings.Scrolling.ScrollHeight = new Unit(600, UnitType.Pixel);
                //this.ClientSettings.Scrolling.UseStaticHeaders = true;
              
                //this.ClientSettings.Scrolling.FrozenColumnsCount = PageSize;
                //this.ClientSettings.Scrolling.EnableVirtualScrollPaging = true;
                //this.ClientSettings.Scrolling.ScrollHeight = 600;
                //this.ClientSettings.Scrolling.UseStaticHeaders = true;                   
            }
            
            base.OnPreRender(e);
        }

        protected override void OnInit(EventArgs e)
        {
            //            if (string.IsNullOrEmpty(ItemType))
            //            {
            //                string itemType = string.Empty;

            //                //try to get the type of the entity shown in the grid. we assume that the grid and the openaccesslinqdatasource are in the same namingContainer
            //                OpenAccessLinqDataSource linqDataSource = NamingContainer.Controls.Cast<Control>().FirstOrDefault(c => c.ID == DataSourceID) as OpenAccessLinqDataSource;

            //                if (linqDataSource != null)
            //                {
            //                    var linqDataSourceFields = linqDataSource.GetType().GetFields(BindingFlags.Instance |
            //                        BindingFlags.Static |
            //                        BindingFlags.NonPublic |
            //                        BindingFlags.Public);

            //                    OpenAccessLinqDataSourceView theView = linqDataSourceFields.FirstOrDefault(f => f.Name == "view").GetValue(linqDataSource) as OpenAccessLinqDataSourceView;

            //                    PropertyInfo prop = theView.GetType().GetProperty("EntityType", (BindingFlags.Instance |
            //                        BindingFlags.Static |
            //                        BindingFlags.NonPublic |
            //                        BindingFlags.Public));
            //                    Type theType = prop.GetValue(theView) as Type;
            //                    itemType = theType.FullName;
            //                    ItemType = itemType;
            //                }
            //            }

            //            if (string.IsNullOrEmpty(ItemType))
            //            {
            //                throw new ApplicationException("ItemType cannot be empty.Source: Custom Grid Control.");
            //            }

            //            string script = @"<script src=""../Scripts/jquery-2.0.3.min.js""></script>
            //        <script src=""../Scripts/jquery.signalR-1.1.3.min.js""></script>
            //        <script src=""/signalr/hubs""></script>
            //        <script>
            ////debugger;
            //            var hub = $.connection.rebindHub;
            //           if (typeof hub !== 'undefined')
            //{
            //            // Unable to get property 'state' of undefined or null reference THIS IS A MISTAKE. I DELIBERATLEY DID NOT DELETE THIS SO I CAN SEE THE ERROR THAT WILL OCCUIF I UNCOMMENT THAT LINE
            //            //hub.state.MyType = ""Products"";
            //            hub.client.rebindRadGrid = function () {
            //
            //                var grid = window.$find(""" + ClientID + @""");
            //
            //            if (grid != null) {
            ////debugger;
            //
            //if (typeof isInRequest === 'undefined') {
            //isInRequest = false;
            //    // variable is undefined
            //}
            //var res = isInRequest; // this is global variable set in ajax start and ajax stop event handlers
            //
            //                var masterTable = grid.get_masterTableView();
            //                var editedItemsArray = masterTable.get_editItems();
            //                var isItemInserted = masterTable.get_isItemInserted()
            //                if (editedItemsArray.length == 0 && !isItemInserted && !isInRequest) {
            //                    masterTable.rebind();
            //                }
            //            }            
            //        }
            //
            //        $.connection.hub.start().done(function () {
            // hub.server.addToGroup($.connection.hub.id, """ + ItemType + @""");
            //        })
            //};
            //</script>";
            //                // ItemType should be setted in markup
            //            //if (ScriptManager.GetRegisteredStartupScripts().Count == 0)
            //            {
            //                ScriptManager.RegisterStartupScript(Page, GetType(), "key", script, false);
            //            }


            base.OnInit(e);
        }

        protected override void OnGroupsChanging(GridGroupsChangingEventArgs e)
        {
            //Expression is added (by drag/grop on group panel)

            //if (e.Action == GridGroupsChangingAction.Group)
            //{
            //    if (e.Expression.GroupByFields[0].FieldName == "CategoryId")
            //    {
            //        GridGroupByField countryGroupField = new GridGroupByField();
            //        countryGroupField.FieldName = "ProductCategory.Name";                   

            //        e.Expression.SelectFields.Clear();
            //        e.Expression.SelectFields.Add(countryGroupField);                 

            //        e.Expression.GroupByFields.Clear();
            //        e.Expression.GroupByFields.Add(countryGroupField);                  
            //    }

            //}

            base.OnGroupsChanging(e);
        }

        protected override void OnItemDeleted(GridDeletedEventArgs e)
        {
            base.OnItemDeleted(e);
            (Page.Master as dynamic).MasterRadNotification.Show("Item was successfully deleted! ");
            //var context = GlobalHost.ConnectionManager.GetHubContext<RebindHub>();
            //context.Clients.Group(ItemType).rebindRadGrid();

        }

        protected override void OnItemInserted(GridInsertedEventArgs e)
        {
            base.OnItemInserted(e);
            (Page.Master as dynamic).MasterRadNotification.Show("Item was successfully inserted! ");
            //var context = GlobalHost.ConnectionManager.GetHubContext<RebindHub>();
            //context.Clients.Group(ItemType).rebindRadGrid();

        }

        protected override void OnItemUpdated(GridUpdatedEventArgs e)
        {
            base.OnItemUpdated(e);
            (Page.Master as dynamic).MasterRadNotification.Show("Item was successfully updated! ");
            //var context = GlobalHost.ConnectionManager.GetHubContext<RebindHub>();
            //context.Clients.Group(ItemType).rebindRadGrid();

        }

        protected override void OnDataBinding(EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            base.OnDataBinding(e);
            stopwatch.Stop();
            long mills = stopwatch.ElapsedMilliseconds;
        }

        public override void DataBind()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            base.DataBind();
            stopwatch.Stop();
            long mills = stopwatch.ElapsedMilliseconds;
        }

        protected override void DataBind(bool raiseOnDataBinding)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            base.DataBind(raiseOnDataBinding);
            stopwatch.Stop();
            long mills = stopwatch.ElapsedMilliseconds;
        }

    }

}
