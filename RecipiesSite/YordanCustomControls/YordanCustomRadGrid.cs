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
                TableCell tableCell = dataItem["Download"];
                if (tableCell != null)
                {
                    foreach (Control control in tableCell.Controls)
                    {
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(control);
                    }                  
                }
                //double dbl = Double.Parse(dataItem["PHONE"].Text.ToString());
                //string str = String.Format("{0:###-###-##}", dbl);
                //dataItem["PHONE"].Text = str;
                
            } 

            base.OnItemCreated(e);
        }
    }
}
