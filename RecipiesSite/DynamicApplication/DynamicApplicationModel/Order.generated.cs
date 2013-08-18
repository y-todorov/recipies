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
using DynamicApplicationModel;


namespace DynamicApplicationModel	
{
	public partial class Order
	{
		private int _orderID;
		public virtual int OrderID 
		{ 
		    get
		    {
		        return this._orderID;
		    }
		    set
		    {
		        this._orderID = value;
		    }
		}
		
		private int? _customerID;
		public virtual int? CustomerID 
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
		
		private int? _employeeID;
		public virtual int? EmployeeID 
		{ 
		    get
		    {
		        return this._employeeID;
		    }
		    set
		    {
		        this._employeeID = value;
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
		
		private IList<OrderDetail> _orderDetails = new List<OrderDetail>();
		public virtual IList<OrderDetail> OrderDetails 
		{ 
		    get
		    {
		        return this._orderDetails;
		    }
		}
		
	}
}
