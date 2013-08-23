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

namespace DynamicApplicationModel
{
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

            base.SaveChanges(failureMode);
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
            IEnumerable<Product> combinedListOfProducts = listOfProductInserts.Concat(listOfProductUpdates);
            foreach (Product product in combinedListOfProducts)
            {
                Type productType = product.GetType();
                var productProperties = productType.GetProperties();

                ProductHistory productHistory = new ProductHistory();
                var productHistoryProperties = productHistory.GetType().GetProperties();

                this.Add(productHistory);

                foreach (PropertyInfo prop in productProperties)
                {
                    if (productHistoryProperties.Any(p => p.Name.Equals(prop.Name)))
                    {
                        object theValue = prop.GetValue(product);
                        productHistory.SetFieldValue(prop.Name, theValue);
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
