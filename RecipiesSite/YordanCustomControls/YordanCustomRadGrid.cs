using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

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
                    Control exportToExcelButton = (e.Item as GridCommandItem).FindControl("ExportToExcelButton") as Control;
                    if (exportToExcelButton != null)
                    {
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(exportToExcelButton);
                    }

                    Control exportToPdfButton = (e.Item as GridCommandItem).FindControl("ExportToPdfButton") as Control;
                    if (exportToPdfButton != null)
                    {
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(exportToPdfButton);
                    }

                    Control exportToCsvButton = (e.Item as GridCommandItem).FindControl("ExportToCsvButton") as Control;
                    if (exportToCsvButton != null)
                    {
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(exportToCsvButton);
                    }

                    Control exportToWordButton = (e.Item as GridCommandItem).FindControl("ExportToWordButton") as Control;
                    if (exportToWordButton != null)
                    {
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

        protected override void OnPreRender(EventArgs e)
        {          
            base.OnPreRender(e);
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
    }
}
