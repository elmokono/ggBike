﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace goGreenBike.Model
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="test_daniel")]
	public partial class DatabaseEntitiesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DatabaseEntitiesDataContext() : 
				base("Data Source=sm-arba-vdev01;Initial Catalog=test_daniel;Persist Security Info=True" +
						";User ID=webappuser;Password=w3b@ppuS3R", mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseEntitiesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseEntitiesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseEntitiesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseEntitiesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.AddSpins")]
		public int AddSpins([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NChar(25)")] string deviceId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> millis, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Float")] System.Nullable<double> spins)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), deviceId, millis, spins);
			return ((int)(result.ReturnValue));
		}
	}
}
#pragma warning restore 1591
