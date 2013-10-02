using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace RecipiesWebFormApp.Production
{
    public partial class Inventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void YordanCustomRadGridInventory_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem editedItem = (e.Item as GridEditableItem);
                RadComboBox dropDownProductListColumn = editedItem["DropDownProductListColumn"].Controls[0] as RadComboBox;

                //attach SelectedIndexChanged event for the dropdown control  
                dropDownProductListColumn.AutoPostBack = true;
                dropDownProductListColumn.SelectedIndexChanged += dropDownProductListColumn_SelectedIndexChanged;
                dropDownProductListColumn.PreRender += dropDownProductListColumn_PreRender;
              
                RadNumericTextBox quantityByDocumentsRadNumericTextBox = editedItem["QuantityByDocuments"].Controls[0] as RadNumericTextBox;
                quantityByDocumentsRadNumericTextBox.ReadOnly = true;               
                
            }  
        }

        void dropDownProductListColumn_PreRender(object sender, EventArgs e)
        {
            dropDownProductListColumn_SelectedIndexChanged(sender, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, string.Empty, string.Empty));
        }

        void dropDownProductListColumn_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //first reference the edited grid item through the NamingContainer                                                     attribute  
            GridEditableItem editedItem = (sender as RadComboBox).NamingContainer as GridEditableItem;

            RadComboBox dropDownProductListColumn = editedItem["DropDownProductListColumn"].Controls[0] as RadComboBox;
            string productId = dropDownProductListColumn.SelectedValue;
            if (!string.IsNullOrEmpty(productId))
            {
                int intProductId;
                if (int.TryParse(productId, out intProductId))
                {
                    Product product = ContextFactory.GetContextPerRequest().Products.FirstOrDefault(p => p.ProductId == intProductId);
                    if (product != null)
                    {
                        RadNumericTextBox quantityByDocumentsRadNumericTextBox = editedItem["QuantityByDocuments"].Controls[0] as RadNumericTextBox;
                        RadNumericTextBox averageUnitPriceRadNumericTextBox = editedItem["AverageUnitPrice"].Controls[0] as RadNumericTextBox;
                        averageUnitPriceRadNumericTextBox.Text = product.UnitPrice.GetValueOrDefault().ToString();
                        quantityByDocumentsRadNumericTextBox.Text = product.GetQuantityByDocuments().ToString();
                    }

                    //ProductVendor productVendor = ContextFactory.GetContextPerRequest().ProductVendors.FirstOrDefault(pv => pv.ProductId == intProductId && pv.VendorId == VendorId);

                    //tbUnitPrice.Text = productVendor.StandardPrice.ToString();
                }
            }
        }
    }
}