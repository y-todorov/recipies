using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecipiesWebFormApp.Production
{
    public partial class Recipes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OpenAccessLinqDataSourceRecipeIngredients_InsertedUpdatedDeleted(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceStatusEventArgs e)
        {
            // So value per portion will be updated
            rgRecipes.Rebind();
        }
    }
}