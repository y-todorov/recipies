using System;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicApplicationWebApplication.OrderDetails
{
	public partial class List : Page
	{
		private MetaTable table;

        protected MetaTable Table
        {
            get
            {
                return this.table;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.table = DynamicDataRouteHandler.GetRequestMetaTable(this.Context);
            this.GridDataSource.ContextTypeName = this.Table.DataContextType.FullName;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = this.Table.DisplayName;
        }

        protected void Label_PreRender(object sender, EventArgs e)
        {
            Label label = sender as Label;
            if (label == null)
            {
                return;
            }

            DynamicFilter dynamicFilter = label.FindControl("DynamicFilter") as DynamicFilter;
            if (dynamicFilter == null)
            {
                return;
            }

            QueryableFilterUserControl filterUserControl = dynamicFilter.FilterTemplate as QueryableFilterUserControl;
            if (filterUserControl != null && filterUserControl.FilterControl != null)
            {
                label.AssociatedControlID = filterUserControl.FilterControl.GetUniqueIDRelativeTo(label);
            }
        }

        protected void DynamicFilter_FilterChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// GridDataSource control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::Telerik.OpenAccess.Web.OpenAccessLinqDataSource GridDataSource;
	}
}
		
