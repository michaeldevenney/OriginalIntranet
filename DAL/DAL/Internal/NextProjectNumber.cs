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
    /// Strongly-typed collection for the NextProjectNumber class.
    /// </summary>
    [Serializable]
    public partial class NextProjectNumberCollection : ReadOnlyList<NextProjectNumber, NextProjectNumberCollection>
    {        
        public NextProjectNumberCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the NextProjectNumber view.
    /// </summary>
    [Serializable]
    public partial class NextProjectNumber : ReadOnlyRecord<NextProjectNumber>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("NextProjectNumber", TableType.View, DataService.GetInstance("Internal"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarNum = new TableSchema.TableColumn(schema);
                colvarNum.ColumnName = "num";
                colvarNum.DataType = DbType.AnsiString;
                colvarNum.MaxLength = 33;
                colvarNum.AutoIncrement = false;
                colvarNum.IsNullable = true;
                colvarNum.IsPrimaryKey = false;
                colvarNum.IsForeignKey = false;
                colvarNum.IsReadOnly = false;
                
                schema.Columns.Add(colvarNum);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["Internal"].AddSchema("NextProjectNumber",schema);
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
	    public NextProjectNumber()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public NextProjectNumber(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public NextProjectNumber(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public NextProjectNumber(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("Num")]
        [Bindable(true)]
        public string Num 
	    {
		    get
		    {
			    return GetColumnValue<string>("num");
		    }
            set 
		    {
			    SetColumnValue("num", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string Num = @"num";
            
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
