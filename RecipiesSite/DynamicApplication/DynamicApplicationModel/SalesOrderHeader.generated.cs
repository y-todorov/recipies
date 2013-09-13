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
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using RecipiesModelNS;


namespace RecipiesModelNS	
{
	public partial class SalesOrderHeader
	{
		private int _salesOrderHeaderId;
		public virtual int SalesOrderHeaderId 
		{ 
		    get
		    {
		        return this._salesOrderHeaderId;
		    }
		    set
		    {
		        this._salesOrderHeaderId = value;
		    }
		}
		
		private int? _customerId;
		public virtual int? CustomerId 
		{ 
		    get
		    {
		        return this._customerId;
		    }
		    set
		    {
		        this._customerId = value;
		    }
		}
		
		private int? _employeeId;
		public virtual int? EmployeeId 
		{ 
		    get
		    {
		        return this._employeeId;
		    }
		    set
		    {
		        this._employeeId = value;
		    }
		}
		
		private int? _statusId;
		public virtual int? StatusId 
		{ 
		    get
		    {
		        return this._statusId;
		    }
		    set
		    {
		        this._statusId = value;
		    }
		}
		
		private string _accountName;
		public virtual string AccountName 
		{ 
		    get
		    {
		        return this._accountName;
		    }
		    set
		    {
		        this._accountName = value;
		    }
		}
		
		private DateTime? _orderDate;
		public virtual DateTime? OrderDate 
		{ 
		    get
		    {
		        return this._orderDate;
		    }
		    set
		    {
		        this._orderDate = value;
		    }
		}
		
		private DateTime? _requiredDate;
		public virtual DateTime? RequiredDate 
		{ 
		    get
		    {
		        return this._requiredDate;
		    }
		    set
		    {
		        this._requiredDate = value;
		    }
		}
		
		private DateTime? _shippedDate;
		public virtual DateTime? ShippedDate 
		{ 
		    get
		    {
		        return this._shippedDate;
		    }
		    set
		    {
		        this._shippedDate = value;
		    }
		}
		
		private string _shipName;
		public virtual string ShipName 
		{ 
		    get
		    {
		        return this._shipName;
		    }
		    set
		    {
		        this._shipName = value;
		    }
		}
		
		private string _shipAddress;
		public virtual string ShipAddress 
		{ 
		    get
		    {
		        return this._shipAddress;
		    }
		    set
		    {
		        this._shipAddress = value;
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
		
		private SalesOrderStatus _salesOrderStatus;
		public virtual SalesOrderStatus SalesOrderStatus 
		{ 
		    get
		    {
		        return this._salesOrderStatus;
		    }
		    set
		    {
		        this._salesOrderStatus = value;
		    }
		}
		
		private Employee _employee;
		public virtual Employee Employee 
		{ 
		    get
		    {
		        return this._employee;
		    }
		    set
		    {
		        this._employee = value;
		    }
		}
		
		private Customer _customer;
		public virtual Customer Customer 
		{ 
		    get
		    {
		        return this._customer;
		    }
		    set
		    {
		        this._customer = value;
		    }
		}
		
		private IList<SalesOrderDetail> _salesOrderDetails = new List<SalesOrderDetail>();
		public virtual IList<SalesOrderDetail> SalesOrderDetails 
		{ 
		    get
		    {
		        return this._salesOrderDetails;
		    }
		}
		
	}
}
