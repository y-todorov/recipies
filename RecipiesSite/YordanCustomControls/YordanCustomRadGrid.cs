using HtmlAgilityPack;
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

namespace YordanCustomControls
{
    public class YordanCustomRadGrid : RadGrid
    {

        //        protected override void OnLoad(EventArgs e)
        //        { // Define the name and type of the client scripts on the page.
        //            String csname1 = "PopupScript";
        //            Type cstype = this.GetType();

        //            // Get a ClientScriptManager reference from the Page class.
        //            ClientScriptManager cs = Page.ClientScript;

        //            // Check to see if the startup script is already registered.
        //            if (!cs.IsStartupScriptRegistered(cstype, csname1))
        //            {
        //                String cstext1 = @"(function () {
        //                    // Init
        //                var pubnub = PUBNUB.init({
        //                    publish_key: 'pub-c-cc6cdb68-ab44-4f1a-8553-ccc30d96f87a',
        //                    subscribe_key: 'sub-c-bde0a3b8-1538-11e3-bc51-02ee2ddab7fe'
        //                })
        //
        //                pubnub.ready();
        //
        //                pubnub.subscribe({
        //                    channel: 'Products',
        //                    callback: function (message) { rebindGrid(message) }
        //                });
        //
        //                function rebindGrid(message) {
        //                   
        //                    var grid = window.$find(""<%= ((RadGrid)rgProducts).ClientID %>"");
        //
        //                    if (grid != null) {
        //                        debugger;
        //                        var masterTable = grid.get_masterTableView();
        //                        var editedItemsArray = masterTable.get_editItems();
        //                        var isItemInserted  = masterTable.get_isItemInserted()
        //                        if (editedItemsArray.length == 0 && !isItemInserted) {
        //                            masterTable.rebind();
        //                        }
        //                    }
        //                }
        //})();";



        //                cs.RegisterStartupScript(cstype, csname1, cstext1, true);

        //            }
        //            base.OnLoad(e);
        //        }

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

            base.OnItemCreated(e);
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
                    }
                    if (gridBoundColumn.DataType == typeof(decimal))
                    {
                        if (string.IsNullOrEmpty(gridBoundColumn.DataFormatString))
                        {
                            gridBoundColumn.DataFormatString = "{0:C2}";
                        }
                    }

                    // validation
                    //gridBoundColumn.ColumnValidationSettings.EnableRequiredFieldValidation = true;

                    //gridBoundColumn.ColumnValidationSettings.RequiredFieldValidator = new RequiredFieldValidator() { ErrorMessage = "This field is required!" };
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
                    gridDropDownColumn.EnableEmptyListItem = true;
                    gridDropDownColumn.ConvertEmptyStringToNull = true;
                    gridDropDownColumn.DropDownControlType = GridDropDownColumnControlType.RadComboBox;
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

        protected override void OnPreRender(EventArgs e)
        {
            ViewState.Remove("isExporting");
            base.OnPreRender(e);
        }

    }
}
