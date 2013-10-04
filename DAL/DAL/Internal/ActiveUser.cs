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
namespace DAL{
    /// <summary>
    /// Strongly-typed collection for the ActiveUser class.
    /// </summary>
    [Serializable]
    public partial class ActiveUserCollection : ReadOnlyList<ActiveUser, ActiveUserCollection>
    {        
        public ActiveUserCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the ActiveUsers view.
    /// </summary>
    [Serializable]
    public partial class ActiveUser : ReadOnlyRecord<ActiveUser>, IReadOnlyRecord
    {
    
	    #region Default Settings
	    protected static void SetSQLProps() 
	    {
		    GetTableSchema();
	    }
	    #endregion
        #region Schema Accessor
	    public static TableSchema.Table Schema
        {
            get
            {
                if (BaseSchema == null)
                {
                    SetSQLProps();
                }
                return BaseSchema;
            }
        }
    	
        private static void GetTableSchema() 
        {
            if(!IsSchemaInitialized)
            {
                //Schema declaration
                TableSchema.Table schema = new TableSchema.Table("ActiveUsers", TableType.View, DataService.GetInstance("Internal"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
                colvarId.ColumnName = "ID";
                colvarId.DataType = DbType.Int32;
                colvarId.MaxLength = 0;
                colvarId.AutoIncrement = false;
                colvarId.IsNullable = false;
                colvarId.IsPrimaryKey = false;
                colvarId.IsForeignKey = false;
                colvarId.IsReadOnly = false;
                
                schema.Columns.Add(colvarId);
                
                TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
                colvarName.ColumnName = "Name";
                colvarName.DataType = DbType.AnsiString;
                colvarName.MaxLength = 50;
                colvarName.AutoIncrement = false;
                colvarName.IsNullable = false;
                colvarName.IsPrimaryKey = false;
                colvarName.IsForeignKey = false;
                colvarName.IsReadOnly = false;
                
                schema.Columns.Add(colvarName);
                
                TableSchema.TableColumn colvarMobilePhone = new TableSchema.TableColumn(schema);
                colvarMobilePhone.ColumnName = "MobilePhone";
                colvarMobilePhone.DataType = DbType.AnsiString;
                colvarMobilePhone.MaxLength = 15;
                colvarMobilePhone.AutoIncrement = false;
                colvarMobilePhone.IsNullable = true;
                colvarMobilePhone.IsPrimaryKey = false;
                colvarMobilePhone.IsForeignKey = false;
                colvarMobilePhone.IsReadOnly = false;
                
                schema.Columns.Add(colvarMobilePhone);
                
                TableSchema.TableColumn colvarEmail = new TableSchema.TableColumn(schema);
                colvarEmail.ColumnName = "Email";
                colvarEmail.DataType = DbType.AnsiString;
                colvarEmail.MaxLength = 50;
                colvarEmail.AutoIncrement = false;
                colvarEmail.IsNullable = true;
                colvarEmail.IsPrimaryKey = false;
                colvarEmail.IsForeignKey = false;
                colvarEmail.IsReadOnly = false;
                
                schema.Columns.Add(colvarEmail);
                
                TableSchema.TableColumn colvarOfficePhone = new TableSchema.TableColumn(schema);
                colvarOfficePhone.ColumnName = "OfficePhone";
                colvarOfficePhone.DataType = DbType.AnsiString;
                colvarOfficePhone.MaxLength = 50;
                colvarOfficePhone.AutoIncrement = false;
                colvarOfficePhone.IsNullable = true;
                colvarOfficePhone.IsPrimaryKey = false;
                colvarOfficePhone.IsForeignKey = false;
                colvarOfficePhone.IsReadOnly = false;
                
                schema.Columns.Add(colvarOfficePhone);
                
                TableSchema.TableColumn colvarExtension = new TableSchema.TableColumn(schema);
                colvarExtension.ColumnName = "Extension";
                colvarExtension.DataType = DbType.AnsiString;
                colvarExtension.MaxLength = 4;
                colvarExtension.AutoIncrement = false;
                colvarExtension.IsNullable = true;
                colvarExtension.IsPrimaryKey = false;
                colvarExtension.IsForeignKey = false;
                colvarExtension.IsReadOnly = false;
                
                schema.Columns.Add(colvarExtension);
                
                TableSchema.TableColumn colvarUsername = new TableSchema.TableColumn(schema);
                colvarUsername.ColumnName = "Username";
                colvarUsername.DataType = DbType.AnsiString;
                colvarUsername.MaxLength = 50;
                colvarUsername.AutoIncrement = false;
                colvarUsername.IsNullable = true;
                colvarUsername.IsPrimaryKey = false;
                colvarUsername.IsForeignKey = false;
                colvarUsername.IsReadOnly = false;
                
                schema.Columns.Add(colvarUsername);
                
                TableSchema.TableColumn colvarPosition = new TableSchema.TableColumn(schema);
                colvarPosition.ColumnName = "Position";
                colvarPosition.DataType = DbType.AnsiString;
                colvarPosition.MaxLength = 50;
                colvarPosition.AutoIncrement = false;
                colvarPosition.IsNullable = true;
                colvarPosition.IsPrimaryKey = false;
                colvarPosition.IsForeignKey = false;
                colvarPosition.IsReadOnly = false;
                
                schema.Columns.Add(colvarPosition);
                
                TableSchema.TableColumn colvarStatus = new TableSchema.TableColumn(schema);
                colvarStatus.ColumnName = "Status";
                colvarStatus.DataType = DbType.AnsiString;
                colvarStatus.MaxLength = 25;
                colvarStatus.AutoIncrement = false;
                colvarStatus.IsNullable = true;
                colvarStatus.IsPrimaryKey = false;
                colvarStatus.IsForeignKey = false;
                colvarStatus.IsReadOnly = false;
                
                schema.Columns.Add(colvarStatus);
                
                TableSchema.TableColumn colvarSortableName = new TableSchema.TableColumn(schema);
                colvarSortableName.ColumnName = "SortableName";
                colvarSortableName.DataType = DbType.AnsiString;
                colvarSortableName.MaxLength = 102;
                colvarSortableName.AutoIncrement = false;
                colvarSortableName.IsNullable = true;
                colvarSortableName.IsPrimaryKey = false;
                colvarSortableName.IsForeignKey = false;
                colvarSortableName.IsReadOnly = false;
                
                schema.Columns.Add(colvarSortableName);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["Internal"].AddSchema("ActiveUsers",schema);
            }
        }
        #endregion
        
        #region Query Accessor
	    public static Query CreateQuery()
	    {
		    return new Query(Schema);
	    }
	    #endregion
	    
	    #region .ctors
	    public ActiveUser()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public ActiveUser(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public ActiveUser(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public ActiveUser(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("Id")]
        [Bindable(true)]
        public int Id 
	    {
		    get
		    {
			    return GetColumnValue<int>("ID");
		    }
            set 
		    {
			    SetColumnValue("ID", value);
            }
        }
	      
        [XmlAttribute("Name")]
        [Bindable(true)]
        public string Name 
	    {
		    get
		    {
			    return GetColumnValue<string>("Name");
		    }
            set 
		    {
			    SetColumnValue("Name", value);
            }
        }
	      
        [XmlAttribute("MobilePhone")]
        [Bindable(true)]
        public string MobilePhone 
	    {
		    get
		    {
			    return GetColumnValue<string>("MobilePhone");
		    }
            set 
		    {
			    SetColumnValue("MobilePhone", value);
            }
        }
	      
        [XmlAttribute("Email")]
        [Bindable(true)]
        public string Email 
	    {
		    get
		    {
			    return GetColumnValue<string>("Email");
		    }
            set 
		    {
			    SetColumnValue("Email", value);
            }
        }
	      
        [XmlAttribute("OfficePhone")]
        [Bindable(true)]
        public string OfficePhone 
	    {
		    get
		    {
			    return GetColumnValue<string>("OfficePhone");
		    }
            set 
		    {
			    SetColumnValue("OfficePhone", value);
            }
        }
	      
        [XmlAttribute("Extension")]
        [Bindable(true)]
        public string Extension 
	    {
		    get
		    {
			    return GetColumnValue<string>("Extension");
		    }
            set 
		    {
			    SetColumnValue("Extension", value);
            }
        }
	      
        [XmlAttribute("Username")]
        [Bindable(true)]
        public string Username 
	    {
		    get
		    {
			    return GetColumnValue<string>("Username");
		    }
            set 
		    {
			    SetColumnValue("Username", value);
            }
        }
	      
        [XmlAttribute("Position")]
        [Bindable(true)]
        public string Position 
	    {
		    get
		    {
			    return GetColumnValue<string>("Position");
		    }
            set 
		    {
			    SetColumnValue("Position", value);
            }
        }
	      
        [XmlAttribute("Status")]
        [Bindable(true)]
        public string Status 
	    {
		    get
		    {
			    return GetColumnValue<string>("Status");
		    }
            set 
		    {
			    SetColumnValue("Status", value);
            }
        }
	      
        [XmlAttribute("SortableName")]
        [Bindable(true)]
        public string SortableName 
	    {
		    get
		    {
			    return GetColumnValue<string>("SortableName");
		    }
            set 
		    {
			    SetColumnValue("SortableName", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string Id = @"ID";
            
            public static string Name = @"Name";
            
            public static string MobilePhone = @"MobilePhone";
            
            public static string Email = @"Email";
            
            public static string OfficePhone = @"OfficePhone";
            
            public static string Extension = @"Extension";
            
            public static string Username = @"Username";
            
            public static string Position = @"Position";
            
            public static string Status = @"Status";
            
            public static string SortableName = @"SortableName";
            
	    }
	    #endregion
	    
	    
	    #region IAbstractRecord Members
        public new CT GetColumnValue<CT>(string columnName) {
            return base.GetColumnValue<CT>(columnName);
        }
        public object GetColumnValue(string columnName) {
            return base.GetColumnValue<object>(columnName);
        }
        #endregion
	    
    }
}
