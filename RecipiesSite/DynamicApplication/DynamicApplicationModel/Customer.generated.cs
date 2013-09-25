#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using RecipiesModelNS;


namespace RecipiesModelNS	
{
	public partial class Customer
	{
		private int _customerID;
		public virtual int CustomerID 
		{ 
		    get
		    {
		        return this._customerID;
		    }
		    set
		    {
		        this._customerID = value;
		    }
		}
		
		private string _companyName;
		public virtual string CompanyName 
		{ 
		    get
		    {
		        return this._companyName;
		    }
		    set
		    {
		        this._companyName = value;
		    }
		}
		
		private string _contactName;
		public virtual string ContactName 
		{ 
		    get
		    {
		        return this._contactName;
		    }
		    set
		    {
		        this._contactName = value;
		    }
		}
		
		private string _address;
		public virtual string Address 
		{ 
		    get
		    {
		        return this._address;
		    }
		    set
		    {
		        this._address = value;
		    }
		}
		
		private string _country;
		public virtual string Country 
		{ 
		    get
		    {
		        return this._country;
		    }
		    set
		    {
		        this._country = value;
		    }
		}
		
		private string _phone;
		public virtual string Phone 
		{ 
		    get
		    {
		        return this._phone;
		    }
		    set
		    {
		        this._phone = value;
		    }
		}
		
		private string _email;
		public virtual string Email 
		{ 
		    get
		    {
		        return this._email;
		    }
		    set
		    {
		        this._email = value;
		    }
		}
		
		private DateTime? _modifiedDate;
		public virtual DateTime? ModifiedDate 
		{ 
		    get
		    {
		        return this._modifiedDate;
		    }
		    set
		    {
		        this._modifiedDate = value;
		    }
		}
		
		private string _modifiedByUser;
		public virtual string ModifiedByUser 
		{ 
		    get
		    {
		        return this._modifiedByUser;
		    }
		    set
		    {
		        this._modifiedByUser = value;
		    }
		}
		
		private IList<SalesOrderHeader> _salesOrderHeaders = new List<SalesOrderHeader>();
		public virtual IList<SalesOrderHeader> SalesOrderHeaders 
		{ 
		    get
		    {
		        return this._salesOrderHeaders;
		    }
		}
		
	}
}
