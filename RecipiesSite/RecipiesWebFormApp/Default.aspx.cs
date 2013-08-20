using DynamicApplicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.OpenAccess;

namespace RecipiesWebFormApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ISchemaHandler sh = ContextFactory.GetContextPerRequest().GetSchemaHandler();

                string ddl = sh.CreateDDLScript();
                

                rhcLast10ModifiedProducts.DataSource = ContextFactory.GetContextPerRequest().Products.OrderByDescending(pr => pr.ModifiedDate).Take(10);

                rhcProductsCountByCategory.DataSource = ContextFactory.GetContextPerRequest().ProductCategories.Select(cat => new { CategoryName = cat.Name, ProductCount = cat.Products.Count });

                rhcProductsForReorder.DataSource = ContextFactory.GetContextPerRequest().Products.Where(product => product.UnitsInStock <= product.ReorderLevel);
            }
        }
    }
}