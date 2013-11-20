namespace RecipiesModelNS
{
    public partial class SalesOrderDetail : YordanBaseEntity
    {
        public override void Added(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            //SalesOrderHeader.UpdateProductsUnitsInStock(SalesOrderHeaderId);

            base.Added(e);
        }

        public override void Changed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            SalesOrderHeader.UpdateProductsUnitsInStock(SalesOrderHeaderId);
            base.Changed(e);
        }

        private static int? salesOrderHeaderId = 0;

        public override void Removing(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            salesOrderHeaderId = SalesOrderHeaderId;
            base.Removing(e);
        }

        public override void Removed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            SalesOrderHeader.UpdateProductsUnitsInStock(salesOrderHeaderId);
            base.Removed(e);
        }
    }
}