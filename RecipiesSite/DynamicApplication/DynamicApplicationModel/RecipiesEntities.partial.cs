using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace RecipiesModelNS
{
    public partial class RecipiesEntities : DbContext
    {
        protected override System.Data.Entity.Validation.DbEntityValidationResult ValidateEntity(
            System.Data.Entity.Infrastructure.DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            return base.ValidateEntity(entityEntry, items);
        }

        protected override bool ShouldValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry)
        {
            return base.ShouldValidateEntity(entityEntry);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            IEnumerable<DbEntityEntry> entries = ChangeTracker.Entries();

            List<YordanBaseEntity> addedEntities = new List<YordanBaseEntity>();
            List<YordanBaseEntity> modifiedEntities = new List<YordanBaseEntity>();
            List<YordanBaseEntity> deletedEntities = new List<YordanBaseEntity>();


           

            foreach (DbEntityEntry entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    YordanBaseEntity ybe = entry.Entity as YordanBaseEntity;
                    if (ybe != null)
                    {
                        ybe.Adding(entry);
                        addedEntities.Add(ybe);

                    }
                }
                else if (entry.State == EntityState.Deleted)
                {
                    YordanBaseEntity ybe = entry.Entity as YordanBaseEntity;
                    if (ybe != null)
                    {
                        ybe.Removing(entry);
                        deletedEntities.Add(ybe);

                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    YordanBaseEntity ybe = entry.Entity as YordanBaseEntity;
                    if (ybe != null)
                    {
                        ybe.Changing(entry);
                        modifiedEntities.Add(ybe);
                           
                    }
                }
            }
            int result = base.SaveChanges();

            // {System.InvalidOperationException: The object is in a detached state. This operation cannot be performed on an ObjectStateEntry when the object is detached.
   //at System.Data.Entity.Core.Objects.ObjectStateEntry.ValidateState()
   //at System.Data.Entity.Core.Objects.EntityEntry.get_Entity()
   //at System.Data.Entity.Internal.InternalContext.<GetStateEntries>b__1c(ObjectStateEntry e)
   //at System.Linq.Enumerable.WhereSelectArrayIterator`2.MoveNext()
   //at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
   //at RecipiesModelNS.RecipiesEntities.SaveChanges() in c:\Projects\recipies\RecipiesSite\DynamicApplication\DynamicApplicationModel\RecipiesEntities.partial.cs:line 35
   //at InventoryManagementMVC.Controllers.CategoryController.Destroy(DataSourceRequest request, IEnumerable`1 categories) in c:\Projects\InventoryManagement\InventoryManagement\InventoryManagementMVC\Controllers\CategoryController.cs:line 98}

            foreach (YordanBaseEntity ybe in addedEntities)
            {
                ybe.Added();
            }
            foreach (YordanBaseEntity ybe in modifiedEntities)
            {
                ybe.Changed();
            }
            foreach (YordanBaseEntity ybe in deletedEntities)
            {
                ybe.Removed();
            }

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}