using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecipiesWebFormApp.Production
{
    public partial class xProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void rbUpdateUnitPrice_Click(object sender, EventArgs e)
        {
            List<Product> products = ContextFactory.GetContextPerRequest().Products.ToList();
            foreach (Product product in products)
            {
                product.UnitPrice = (decimal?)product.GetAveragePriceLastDays(14);
            }
            ContextFactory.GetContextPerRequest();
            rgProducts.Rebind();
        }
    }
}