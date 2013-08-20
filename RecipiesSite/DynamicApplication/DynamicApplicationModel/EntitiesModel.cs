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
			
		public IQueryable<Vendor> Vendors 
		{
	    	get
	    	{
	        	return this.GetAll<Vendor>();
	    	}
		}
		
		public IQueryable<UnitMeasure> UnitMeasures 
		{
	    	get
	    	{
	        	return this.GetAll<UnitMeasure>();
	    	}
		}
		
		public IQueryable<Store> Stores 
		{
	    	get
	    	{
	        	return this.GetAll<Store>();
	    	}
		}
		
		public IQueryable<ProductCategory> ProductCategories 
		{
	    	get
	    	{
	        	return this.GetAll<ProductCategory>();
	    	}
		}
		
		public IQueryable<Product> Products 
		{
	    	get
	    	{
	        	return this.GetAll<Product>();
	    	}
		}
		
		public IQueryable<OrderDetail> OrderDetails 
		{
	    	get
	    	{
	        	return this.GetAll<OrderDetail>();
	    	}
		}
		
		public IQueryable<Order> Orders 
		{
	    	get
	    	{
	        	return this.GetAll<Order>();
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
		
		public IQueryable<ProductHistory> ProductHistories 
		{
	    	get
	    	{
	        	return this.GetAll<ProductHistory>();
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
		IQueryable<Vendor> Vendors 
		{ 
			get;
		}

		IQueryable<UnitMeasure> UnitMeasures 
		{ 
			get;
		}

		IQueryable<Store> Stores 
		{ 
			get;
		}

		IQueryable<ProductCategory> ProductCategories 
		{ 
			get;
		}

		IQueryable<Product> Products 
		{ 
			get;
		}

		IQueryable<OrderDetail> OrderDetails 
		{ 
			get;
		}

		IQueryable<Order> Orders 
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

		IQueryable<ProductHistory> ProductHistories 
		{ 
			get;
		}

	}
}
#pragma warning restore 1591

