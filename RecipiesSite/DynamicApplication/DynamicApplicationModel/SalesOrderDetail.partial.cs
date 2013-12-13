using System.Linq;

namespace RecipiesModelNS
{
    public partial class SalesOrderDetail : YordanBaseEntity
    {
        public override void Added(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            SalesOrderDetail.UpdateProductsUnitsInStock(SalesOrderDetailId);

            base.Added(e);
        }

        public override void Changed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            SalesOrderDetail.UpdateProductsUnitsInStock(SalesOrderDetailId);
            base.Changed(e);
        }

        private static int? salesOrderDetailId = 0;

        public override void Removing(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            salesOrderDetailId = SalesOrderDetailId;
            base.Removing(e);
        }

        public override void Removed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            SalesOrderDetail.UpdateProductsUnitsInStock(salesOrderDetailId);
            base.Removed(e);
        }

        public static void UpdateProductsUnitsInStock(int? salesOrderDetailId)
        {
            SalesOrderDetail salesOrderDetail = ContextFactory.Current.SalesOrderDetails.FirstOrDefault(sod => sod.SalesOrderDetailId == salesOrderDetailId);
            if (salesOrderDetail.Recipe != null)
            {
                foreach (ProductIngredient pi in salesOrderDetail.Recipe.ProductIngredients)
                {
                    Product.UpdateUnitsInStock(pi.ProductId);
                }
                foreach (RecipeIngredient ri in salesOrderDetail.Recipe.RecipeIngredients1)
                {
                    if (ri.IngredientRecipe != null)
                    {
                        foreach (ProductIngredient pi in ri.IngredientRecipe.ProductIngredients)
                        {
                            Product.UpdateUnitsInStock(pi.ProductId);
                        }
                    }
                }
            }

        }
    }
}