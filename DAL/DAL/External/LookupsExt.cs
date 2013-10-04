using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
// <auto-generated />
namespace DALRemote
{
	/// <summary>
	/// Strongly-typed collection for the LookupsExt class.
	/// </summary>
    [Serializable]
	public partial class LookupsExtCollection : ActiveList<LookupsExt, LookupsExtCollection>
	{	   
		public LookupsExtCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LookupsExtCollection</returns>
		public LookupsExtCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LookupsExt o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the L_LookupsExt table.
	/// </summary>
	[Serializable]
	public partial class LookupsExt : ActiveRecord<LookupsExt>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LookupsExt()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LookupsExt(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LookupsExt(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LookupsExt(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("L_LookupsExt", TableType.Table, DataService.GetInstance("External"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "ID";
				colvarId.DataType = DbType.Int32;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarLookupList = new TableSchema.TableColumn(schema);
				colvarLookupList.ColumnName = "LookupList";
				colvarLookupList.DataType = DbType.AnsiString;
				colvarLookupList.MaxLength = 25;
				colvarLookupList.AutoIncrement = false;
				colvarLookupList.IsNullable = false;
				colvarLookupList.IsPrimaryKey = false;
				colvarLookupList.IsForeignKey = false;
				colvarLookupList.IsReadOnly = false;
				colvarLookupList.DefaultSetting = @"";
				colvarLookupList.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLookupList);
				
				TableSchema.TableColumn colvarLookupValue = new TableSchema.TableColumn(schema);
				colvarLookupValue.ColumnName = "LookupValue";
				colvarLookupValue.DataType = DbType.AnsiString;
				colvarLookupValue.MaxLength = 50;
				colvarLookupValue.AutoIncrement = false;
				colvarLookupValue.IsNullable = false;
				colvarLookupValue.IsPrimaryKey = false;
				colvarLookupValue.IsForeignKey = false;
				colvarLookupValue.IsReadOnly = false;
				colvarLookupValue.DefaultSetting = @"";
				colvarLookupValue.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLookupValue);
				
				TableSchema.TableColumn colvarSecondaryValue = new TableSchema.TableColumn(schema);
				colvarSecondaryValue.ColumnName = "SecondaryValue";
				colvarSecondaryValue.DataType = DbType.AnsiString;
				colvarSecondaryValue.MaxLength = 50;
				colvarSecondaryValue.AutoIncrement = false;
				colvarSecondaryValue.IsNullable = true;
				colvarSecondaryValue.IsPrimaryKey = false;
				colvarSecondaryValue.IsForeignKey = false;
				colvarSecondaryValue.IsReadOnly = false;
				colvarSecondaryValue.DefaultSetting = @"";
				colvarSecondaryValue.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSecondaryValue);
				
				TableSchema.TableColumn colvarSortOrder = new TableSchema.TableColumn(schema);
				colvarSortOrder.ColumnName = "SortOrder";
				colvarSortOrder.DataType = DbType.Int32;
				colvarSortOrder.MaxLength = 0;
				colvarSortOrder.AutoIncrement = false;
				colvarSortOrder.IsNullable = true;
				colvarSortOrder.IsPrimaryKey = false;
				colvarSortOrder.IsForeignKey = false;
				colvarSortOrder.IsReadOnly = false;
				colvarSortOrder.DefaultSetting = @"";
				colvarSortOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSortOrder);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["External"].AddSchema("L_LookupsExt",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public int Id 
		{
			get { return GetColumnValue<int>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("LookupList")]
		[Bindable(true)]
		public string LookupList 
		{
			get { return GetColumnValue<string>(Columns.LookupList); }
			set { SetColumnValue(Columns.LookupList, value); }
		}
		  
		[XmlAttribute("LookupValue")]
		[Bindable(true)]
		public string LookupValue 
		{
			get { return GetColumnValue<string>(Columns.LookupValue); }
			set { SetColumnValue(Columns.LookupValue, value); }
		}
		  
		[XmlAttribute("SecondaryValue")]
		[Bindable(true)]
		public string SecondaryValue 
		{
			get { return GetColumnValue<string>(Columns.SecondaryValue); }
			set { SetColumnValue(Columns.SecondaryValue, value); }
		}
		  
		[XmlAttribute("SortOrder")]
		[Bindable(true)]
		public int? SortOrder 
		{
			get { return GetColumnValue<int?>(Columns.SortOrder); }
			set { SetColumnValue(Columns.SortOrder, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varLookupList,string varLookupValue,string varSecondaryValue,int? varSortOrder)
		{
			LookupsExt item = new LookupsExt();
			
			item.LookupList = varLookupList;
			
			item.LookupValue = varLookupValue;
			
			item.SecondaryValue = varSecondaryValue;
			
			item.SortOrder = varSortOrder;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varLookupList,string varLookupValue,string varSecondaryValue,int? varSortOrder)
		{
			LookupsExt item = new LookupsExt();
			
				item.Id = varId;
			
				item.LookupList = varLookupList;
			
				item.LookupValue = varLookupValue;
			
				item.SecondaryValue = varSecondaryValue;
			
				item.SortOrder = varSortOrder;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn LookupListColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn LookupValueColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SecondaryValueColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SortOrderColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string LookupList = @"LookupList";
			 public static string LookupValue = @"LookupValue";
			 public static string SecondaryValue = @"SecondaryValue";
			 public static string SortOrder = @"SortOrder";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
