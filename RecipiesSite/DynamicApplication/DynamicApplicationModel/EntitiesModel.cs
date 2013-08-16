﻿#pragma warning disable 1591
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
	public partial class RecipiesModel : OpenAccessContext, IRecipiesModelUnitOfWork
	{ 
		private static string connectionStringName = @"Connection";
			
		private static BackendConfiguration backend = GetBackendConfiguration();
		
			
		private static MetadataSource metadataSource = XmlMetadataSource.FromAssemblyResource("EntitiesModel.rlinq");
	
		public RecipiesModel()
			:base(connectionStringName, backend, metadataSource)
		{ }
		
		public RecipiesModel(string connection)
			:base(connection, backend, metadataSource)
		{ }
	
		public RecipiesModel(BackendConfiguration backendConfiguration)
			:base(connectionStringName, backendConfiguration, metadataSource)
		{ }
			
		public RecipiesModel(string connection, MetadataSource metadataSource)
			:base(connection, backend, metadataSource)
		{ }
		
		public RecipiesModel(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource)
			:base(connection, backendConfiguration, metadataSource)
		{ }
			
		public IQueryable<Territory> Territories 
		{
	    	get
	    	{
	        	return this.GetAll<Territory>();
	    	}
		}
		
		public IQueryable<Supplier> Suppliers 
		{
	    	get
	    	{
	        	return this.GetAll<Supplier>();
	    	}
		}
		
		public IQueryable<Shipper> Shippers 
		{
	    	get
	    	{
	        	return this.GetAll<Shipper>();
	    	}
		}
		
		public IQueryable<Region> Regions 
		{
	    	get
	    	{
	        	return this.GetAll<Region>();
	    	}
		}
		
		public IQueryable<Product> Products 
		{
	    	get
	    	{
	        	return this.GetAll<Product>();
	    	}
		}
		
		public IQueryable<Order> Orders 
		{
	    	get
	    	{
	        	return this.GetAll<Order>();
	    	}
		}
		
		public IQueryable<OrderDetail> OrderDetails 
		{
	    	get
	    	{
	        	return this.GetAll<OrderDetail>();
	    	}
		}
		
		public IQueryable<Employee> Employees 
		{
	    	get
	    	{
	        	return this.GetAll<Employee>();
	    	}
		}
		
		public IQueryable<Customer> Customers 
		{
	    	get
	    	{
	        	return this.GetAll<Customer>();
	    	}
		}
		
		public IQueryable<CustomerDemographic> CustomerDemographics 
		{
	    	get
	    	{
	        	return this.GetAll<CustomerDemographic>();
	    	}
		}
		
		public IQueryable<Category> Categories 
		{
	    	get
	    	{
	        	return this.GetAll<Category>();
	    	}
		}
		
		public IQueryable<XUnit> XUnits 
		{
	    	get
	    	{
	        	return this.GetAll<XUnit>();
	    	}
		}
		
		public IQueryable<XSupplier> XSuppliers 
		{
	    	get
	    	{
	        	return this.GetAll<XSupplier>();
	    	}
		}
		
		public IQueryable<XProduct> XProducts 
		{
	    	get
	    	{
	        	return this.GetAll<XProduct>();
	    	}
		}
		
		public IQueryable<XCategory> XCategories 
		{
	    	get
	    	{
	        	return this.GetAll<XCategory>();
	    	}
		}
		
		public static BackendConfiguration GetBackendConfiguration()
		{
			BackendConfiguration backend = new BackendConfiguration();
			backend.Backend = "MsSql";
			backend.ProviderName = "System.Data.SqlClient";
			return backend;
		}
	}

	public interface IRecipiesModelUnitOfWork : IUnitOfWork
	{
		IQueryable<Territory> Territories 
		{ 
			get;
		}

		IQueryable<Supplier> Suppliers 
		{ 
			get;
		}

		IQueryable<Shipper> Shippers 
		{ 
			get;
		}

		IQueryable<Region> Regions 
		{ 
			get;
		}

		IQueryable<Product> Products 
		{ 
			get;
		}

		IQueryable<Order> Orders 
		{ 
			get;
		}

		IQueryable<OrderDetail> OrderDetails 
		{ 
			get;
		}

		IQueryable<Employee> Employees 
		{ 
			get;
		}

		IQueryable<Customer> Customers 
		{ 
			get;
		}

		IQueryable<CustomerDemographic> CustomerDemographics 
		{ 
			get;
		}

		IQueryable<Category> Categories 
		{ 
			get;
		}

		IQueryable<XUnit> XUnits 
		{ 
			get;
		}

		IQueryable<XSupplier> XSuppliers 
		{ 
			get;
		}

		IQueryable<XProduct> XProducts 
		{ 
			get;
		}

		IQueryable<XCategory> XCategories 
		{ 
			get;
		}

	}
}
#pragma warning restore 1591

