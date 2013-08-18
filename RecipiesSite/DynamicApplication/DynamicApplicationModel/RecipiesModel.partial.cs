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
            IList<object> listOfUpdates = this.GetChanges().GetUpdates<object>();
            IList<object> listOfInserts = this.GetChanges().GetInserts<object>();

            IEnumerable<object> combinedList = listOfInserts.Concat(listOfUpdates);

            foreach (object update in combinedList)
            {
                Type type = update.GetType();
                if (type.GetProperties().Any(p => p.Name.Equals("ModifiedDate")))
                {
                    update.SetFieldValue<DateTime>("ModifiedDate", DateTime.Now);           
                }
            }

            base.SaveChanges(failureMode);
        }
        
        protected override void OnDatabaseOpen(Telerik.OpenAccess.BackendConfiguration backendConfiguration, Telerik.OpenAccess.Metadata.MetadataContainer metadataContainer)
        {
            base.OnDatabaseOpen(backendConfiguration, metadataContainer);

            

        }
    }
}
