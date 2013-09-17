using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using System.Reflection;
using System.Web;

namespace RecipiesModelNS
{
    public enum PurchaseOrderStatusEnum
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
        Completed = 4
    }

    public enum SalesOrderStatusEnum
    {
        None = 0,
        Approved = 2,
        Canceled = 6
    }

    public partial class RecipiesModel
    {

        protected override void Init(string connectionString, Telerik.OpenAccess.BackendConfiguration backendConfiguration, Telerik.OpenAccess.Metadata.MetadataContainer metadataContainer)
        {
            base.Init(connectionString, backendConfiguration, metadataContainer);
        }

        protected override void Init(string connectionString, Telerik.OpenAccess.BackendConfiguration backendConfiguration, Telerik.OpenAccess.Metadata.MetadataContainer metadataContainer, System.Reflection.Assembly callingAssembly)
        {
            base.Init(connectionString, backendConfiguration, metadataContainer, callingAssembly);
        }

        public override void SaveChanges(ConcurrencyConflictsProcessingMode failureMode)
        {
            SetModifiedDateAndModifiedByUserFields();

            PopulateProductHistory();

            SetProperShiftDates();

            List<int> recipeIds = GetUpdatedRecipeIds();

            base.SaveChanges(failureMode);

            UpdateRecipesValuePerPortionFromIngredientsChange(recipeIds);

            base.SaveChanges(failureMode);

            //PubNubMessaging.Core.Pubnub.Instance.Publish("Products", "rebind", (t) => t.ToString(), (t) => t.ToString());
        }

        private void UpdateRecipesValuePerPortionFromIngredientsChange(List<int> recipeIds)
        {
            foreach (int? id in recipeIds)
            {
                if (id.HasValue)
                {
                    Recipe recipe = Recipes.FirstOrDefault(re => re.RecipeId == id);
                    if (recipe != null)
                    {
                        decimal? valuePerPortion = 0;
                        foreach (RecipeIngredient ri in recipe.RecipeIngredients)
                        {
                            valuePerPortion += ri.Cost;
                        }
                        recipe.ValuePerPortion = valuePerPortion;
                    }
                }
            }
        }

        private List<int> GetUpdatedRecipeIds()
        {
            IList<RecipeIngredient> listOfIngredientsInserts = this.GetChanges().GetInserts<RecipeIngredient>();
            IList<RecipeIngredient> listOfIngredientsUpdates = this.GetChanges().GetUpdates<RecipeIngredient>();
            IList<RecipeIngredient> listOfIngredientsDeletes = this.GetChanges().GetDeletes<RecipeIngredient>();

            List<RecipeIngredient> combinedListOfIngredients = listOfIngredientsInserts.Concat(listOfIngredientsUpdates).Concat(listOfIngredientsDeletes).ToList();

            // тук ако е променена съставка, ще ъпдехтнем всички рецепти за сега, колкото и неефективно да е това. Трябва да се измисли по натам.       
            if (combinedListOfIngredients.Count() != 0)
            {
                return Recipes.Select(ri => ri.RecipeId).ToList();
            }
            else
            {
                return new List<int>();
            }
        }

        // Setting the date of shifts to be in 2000 year
        private void SetProperShiftDates()
        {
            IList<Shift> listOfShiftInserts = this.GetChanges().GetInserts<Shift>();
            IList<Shift> listOfShifttUpdates = this.GetChanges().GetUpdates<Shift>();
            IEnumerable<Shift> combinedListOfShifts = listOfShiftInserts.Concat(listOfShifttUpdates);
            foreach (Shift product in combinedListOfShifts)
            {
                if (product.StartHour.HasValue)
                {
                    product.StartHour = new DateTime(2000, 1, 1).
                        AddHours(product.StartHour.Value.Hour).
                        AddMinutes(product.StartHour.Value.Minute);
                }
                if (product.EndHour.HasValue)
                {
                    product.EndHour = new DateTime(2000, 1, 1).
                        AddHours(product.EndHour.Value.Hour).
                        AddMinutes(product.EndHour.Value.Minute);
                }
            }
        }

        private void SetModifiedDateAndModifiedByUserFields()
        {
            string userName = null;
            if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity != null)
            {
                userName = HttpContext.Current.User.Identity.Name;
            }

            IList<object> listOfUpdates = this.GetChanges().GetUpdates<object>();
            IList<object> listOfInserts = this.GetChanges().GetInserts<object>();

            IEnumerable<object> combinedList = listOfInserts.Concat(listOfUpdates);

            foreach (object update in combinedList)
            {
                Type type = update.GetType();
                if (type.GetProperties().Any(p => p.Name.Equals("ModifiedDate")))
                {
                    update.SetFieldValue<DateTime>("ModifiedDate", DateTime.Now);
                    update.SetFieldValue<string>("ModifiedByUser", userName);
                }
            }
        }

        private void PopulateProductHistory()
        {
            IList<Product> listOfProductInserts = this.GetChanges().GetInserts<Product>();
            IList<Product> listOfProductUpdates = this.GetChanges().GetUpdates<Product>();
            IList<Product> listOfProductDeletes = this.GetChanges().GetDeletes<Product>();

            IEnumerable<Product> combinedListOfProducts = listOfProductInserts.Concat(listOfProductUpdates).Concat(listOfProductDeletes);
            if (combinedListOfProducts.Count() > 0)
            {
                //PubNubMessaging.Core.Pubnub.Instance.Publish("Products", "rebind", (t) => t.ToString(), (t) => t.ToString());
            }
            foreach (Product product in combinedListOfProducts)
            {
                var productFields = product.GetType().GetFields(BindingFlags.Instance |
                       BindingFlags.Static |
                       BindingFlags.NonPublic |
                       BindingFlags.Public);
                var productProperties = product.GetType().GetProperties();

                ProductHistory productHistory = new ProductHistory();
                var productHistoryFields = productHistory.GetType().GetFields(BindingFlags.Instance |
                       BindingFlags.Static |
                       BindingFlags.NonPublic |
                       BindingFlags.Public);
                var productHistoryProperties = productHistory.GetType().GetProperties();

                this.Add(productHistory);

                foreach (FieldInfo field in productFields)
                {
                    // Check if this is actual property
                    if (productProperties.Any(p => ("_" + p.Name).Equals(field.Name, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        //object theValue = prop.GetValue(product); This is property and we get exception when deleting products
                        var field2 = productHistoryFields.FirstOrDefault(f => f.Name.Equals(field.Name));
                        if (field2 != null)
                        {
                            try
                            {
                                object theValue = field.GetValue(product);
                                productHistory.SetFieldValue(field.Name, theValue);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }

                }
            }
        }



        protected override void OnDatabaseOpen(Telerik.OpenAccess.BackendConfiguration backendConfiguration, Telerik.OpenAccess.Metadata.MetadataContainer metadataContainer)
        {
            base.OnDatabaseOpen(backendConfiguration, metadataContainer);



        }
    }
}
