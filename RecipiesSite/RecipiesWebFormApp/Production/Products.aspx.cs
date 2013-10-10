using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Data.Objects;
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
            Product.UpdateUnitPriceOfAllProducts();
            rgProducts.Rebind();
        }

        protected void rbUpdateUnitsInStock_Click(object sender, EventArgs e)
        {
            Product.UpdateUnitsInStockOfAllProducts();
            rgProducts.Rebind();
        }

        protected void rbUpdateUnitsOnOrder_Click(object sender, EventArgs e)
        {
            Product.UpdateUnitsOnOrderOfAllProducts();
            rgProducts.Rebind();
        }
    }
}