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
                        }
                    }
                }
            }

            base.OnItemCreated(e);
        }

        protected override void OnPreRender(EventArgs e)
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
                    if (gridBoundColumn.DataType == typeof(DateTime))
                    {
                        if (string.IsNullOrEmpty(gridBoundColumn.DataFormatString))
                        {
                            gridBoundColumn.DataFormatString = "{0:dd/MM/yyyy}";
                        }
                    }
                    if (gridBoundColumn.DataType == typeof(decimal))
                    {
                        if (string.IsNullOrEmpty(gridBoundColumn.DataFormatString))
                        {
                            gridBoundColumn.DataFormatString = "{0:C}";
                        }
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
                    gridDropDownColumn.EnableEmptyListItem = true;
                    gridDropDownColumn.ConvertEmptyStringToNull = true;
                    gridDropDownColumn.DropDownControlType = GridDropDownColumnControlType.RadComboBox;
                }

            }

            base.OnPreRender(e);
        }
    }
}
