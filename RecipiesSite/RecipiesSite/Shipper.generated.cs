#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
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
using RecipiesSite;

namespace RecipiesSite	
{
	public partial class Shipper
	{
		private int _shipperID;
		public virtual int ShipperID
		{
			get
			{
				return this._shipperID;
			}
			set
			{
				this._shipperID = value;
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
		
		private IList<Order> _orders = new List<Order>();
		public virtual IList<Order> Orders
		{
			get
			{
				return this._orders;
			}
		}
		
	}
}
#pragma warning restore 1591
