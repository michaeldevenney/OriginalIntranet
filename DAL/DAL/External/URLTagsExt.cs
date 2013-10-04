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
	/// Strongly-typed collection for the URLTagsExt class.
	/// </summary>
    [Serializable]
	public partial class URLTagsExtCollection : ActiveList<URLTagsExt, URLTagsExtCollection>
	{	   
		public URLTagsExtCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>URLTagsExtCollection</returns>
		public URLTagsExtCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                URLTagsExt o = this[i];
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
	/// This is an ActiveRecord class which wraps the URLTagsExt table.
	/// </summary>
	[Serializable]
	public partial class URLTagsExt : ActiveRecord<URLTagsExt>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public URLTagsExt()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public URLTagsExt(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public URLTagsExt(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public URLTagsExt(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("URLTagsExt", TableType.Table, DataService.GetInstance("External"));
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
				
				TableSchema.TableColumn colvarSource = new TableSchema.TableColumn(schema);
				colvarSource.ColumnName = "Source";
				colvarSource.DataType = DbType.AnsiString;
				colvarSource.MaxLength = 255;
				colvarSource.AutoIncrement = false;
				colvarSource.IsNullable = true;
				colvarSource.IsPrimaryKey = false;
				colvarSource.IsForeignKey = false;
				colvarSource.IsReadOnly = false;
				colvarSource.DefaultSetting = @"";
				colvarSource.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSource);
				
				TableSchema.TableColumn colvarTargetObjectName = new TableSchema.TableColumn(schema);
				colvarTargetObjectName.ColumnName = "TargetObjectName";
				colvarTargetObjectName.DataType = DbType.AnsiString;
				colvarTargetObjectName.MaxLength = 255;
				colvarTargetObjectName.AutoIncrement = false;
				colvarTargetObjectName.IsNullable = true;
				colvarTargetObjectName.IsPrimaryKey = false;
				colvarTargetObjectName.IsForeignKey = false;
				colvarTargetObjectName.IsReadOnly = false;
				colvarTargetObjectName.DefaultSetting = @"";
				colvarTargetObjectName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTargetObjectName);
				
				TableSchema.TableColumn colvarTargetObjectPath = new TableSchema.TableColumn(schema);
				colvarTargetObjectPath.ColumnName = "TargetObjectPath";
				colvarTargetObjectPath.DataType = DbType.AnsiString;
				colvarTargetObjectPath.MaxLength = 500;
				colvarTargetObjectPath.AutoIncrement = false;
				colvarTargetObjectPath.IsNullable = true;
				colvarTargetObjectPath.IsPrimaryKey = false;
				colvarTargetObjectPath.IsForeignKey = false;
				colvarTargetObjectPath.IsReadOnly = false;
				colvarTargetObjectPath.DefaultSetting = @"";
				colvarTargetObjectPath.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTargetObjectPath);
				
				TableSchema.TableColumn colvarCampaign = new TableSchema.TableColumn(schema);
				colvarCampaign.ColumnName = "Campaign";
				colvarCampaign.DataType = DbType.AnsiString;
				colvarCampaign.MaxLength = 50;
				colvarCampaign.AutoIncrement = false;
				colvarCampaign.IsNullable = true;
				colvarCampaign.IsPrimaryKey = false;
				colvarCampaign.IsForeignKey = false;
				colvarCampaign.IsReadOnly = false;
				colvarCampaign.DefaultSetting = @"";
				colvarCampaign.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCampaign);
				
				TableSchema.TableColumn colvarLongTargetURL = new TableSchema.TableColumn(schema);
				colvarLongTargetURL.ColumnName = "LongTargetURL";
				colvarLongTargetURL.DataType = DbType.AnsiString;
				colvarLongTargetURL.MaxLength = 500;
				colvarLongTargetURL.AutoIncrement = false;
				colvarLongTargetURL.IsNullable = true;
				colvarLongTargetURL.IsPrimaryKey = false;
				colvarLongTargetURL.IsForeignKey = false;
				colvarLongTargetURL.IsReadOnly = false;
				colvarLongTargetURL.DefaultSetting = @"";
				colvarLongTargetURL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongTargetURL);
				
				TableSchema.TableColumn colvarShortenedtURL = new TableSchema.TableColumn(schema);
				colvarShortenedtURL.ColumnName = "ShortenedtURL";
				colvarShortenedtURL.DataType = DbType.AnsiString;
				colvarShortenedtURL.MaxLength = 50;
				colvarShortenedtURL.AutoIncrement = false;
				colvarShortenedtURL.IsNullable = true;
				colvarShortenedtURL.IsPrimaryKey = false;
				colvarShortenedtURL.IsForeignKey = false;
				colvarShortenedtURL.IsReadOnly = false;
				colvarShortenedtURL.DefaultSetting = @"";
				colvarShortenedtURL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarShortenedtURL);
				
				TableSchema.TableColumn colvarSynched = new TableSchema.TableColumn(schema);
				colvarSynched.ColumnName = "Synched";
				colvarSynched.DataType = DbType.Boolean;
				colvarSynched.MaxLength = 0;
				colvarSynched.AutoIncrement = false;
				colvarSynched.IsNullable = true;
				colvarSynched.IsPrimaryKey = false;
				colvarSynched.IsForeignKey = false;
				colvarSynched.IsReadOnly = false;
				colvarSynched.DefaultSetting = @"";
				colvarSynched.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSynched);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["External"].AddSchema("URLTagsExt",schema);
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
		  
		[XmlAttribute("Source")]
		[Bindable(true)]
		public string Source 
		{
			get { return GetColumnValue<string>(Columns.Source); }
			set { SetColumnValue(Columns.Source, value); }
		}
		  
		[XmlAttribute("TargetObjectName")]
		[Bindable(true)]
		public string TargetObjectName 
		{
			get { return GetColumnValue<string>(Columns.TargetObjectName); }
			set { SetColumnValue(Columns.TargetObjectName, value); }
		}
		  
		[XmlAttribute("TargetObjectPath")]
		[Bindable(true)]
		public string TargetObjectPath 
		{
			get { return GetColumnValue<string>(Columns.TargetObjectPath); }
			set { SetColumnValue(Columns.TargetObjectPath, value); }
		}
		  
		[XmlAttribute("Campaign")]
		[Bindable(true)]
		public string Campaign 
		{
			get { return GetColumnValue<string>(Columns.Campaign); }
			set { SetColumnValue(Columns.Campaign, value); }
		}
		  
		[XmlAttribute("LongTargetURL")]
		[Bindable(true)]
		public string LongTargetURL 
		{
			get { return GetColumnValue<string>(Columns.LongTargetURL); }
			set { SetColumnValue(Columns.LongTargetURL, value); }
		}
		  
		[XmlAttribute("ShortenedtURL")]
		[Bindable(true)]
		public string ShortenedtURL 
		{
			get { return GetColumnValue<string>(Columns.ShortenedtURL); }
			set { SetColumnValue(Columns.ShortenedtURL, value); }
		}
		  
		[XmlAttribute("Synched")]
		[Bindable(true)]
		public bool? Synched 
		{
			get { return GetColumnValue<bool?>(Columns.Synched); }
			set { SetColumnValue(Columns.Synched, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varSource,string varTargetObjectName,string varTargetObjectPath,string varCampaign,string varLongTargetURL,string varShortenedtURL,bool? varSynched)
		{
			URLTagsExt item = new URLTagsExt();
			
			item.Source = varSource;
			
			item.TargetObjectName = varTargetObjectName;
			
			item.TargetObjectPath = varTargetObjectPath;
			
			item.Campaign = varCampaign;
			
			item.LongTargetURL = varLongTargetURL;
			
			item.ShortenedtURL = varShortenedtURL;
			
			item.Synched = varSynched;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varSource,string varTargetObjectName,string varTargetObjectPath,string varCampaign,string varLongTargetURL,string varShortenedtURL,bool? varSynched)
		{
			URLTagsExt item = new URLTagsExt();
			
				item.Id = varId;
			
				item.Source = varSource;
			
				item.TargetObjectName = varTargetObjectName;
			
				item.TargetObjectPath = varTargetObjectPath;
			
				item.Campaign = varCampaign;
			
				item.LongTargetURL = varLongTargetURL;
			
				item.ShortenedtURL = varShortenedtURL;
			
				item.Synched = varSynched;
			
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
        
        
        
        public static TableSchema.TableColumn SourceColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TargetObjectNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn TargetObjectPathColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn CampaignColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn LongTargetURLColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn ShortenedtURLColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn SynchedColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string Source = @"Source";
			 public static string TargetObjectName = @"TargetObjectName";
			 public static string TargetObjectPath = @"TargetObjectPath";
			 public static string Campaign = @"Campaign";
			 public static string LongTargetURL = @"LongTargetURL";
			 public static string ShortenedtURL = @"ShortenedtURL";
			 public static string Synched = @"Synched";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
