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
namespace DAL
{
    /// <summary>
    /// Controller class for PM_TakeoffInfo
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TakeoffInfoController
    {
        // Preload our schema..
        TakeoffInfo thisSchemaLoad = new TakeoffInfo();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public TakeoffInfoCollection FetchAll()
        {
            TakeoffInfoCollection coll = new TakeoffInfoCollection();
            Query qry = new Query(TakeoffInfo.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TakeoffInfoCollection FetchByID(object Id)
        {
            TakeoffInfoCollection coll = new TakeoffInfoCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TakeoffInfoCollection FetchByQuery(Query qry)
        {
            TakeoffInfoCollection coll = new TakeoffInfoCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (TakeoffInfo.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (TakeoffInfo.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? ProjectID,string DateCompleted,string Pm,string DrawingSet,string Summary,string Status)
	    {
		    TakeoffInfo item = new TakeoffInfo();
		    
            item.ProjectID = ProjectID;
            
            item.DateCompleted = DateCompleted;
            
            item.Pm = Pm;
            
            item.DrawingSet = DrawingSet;
            
            item.Summary = Summary;
            
            item.Status = Status;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,int? ProjectID,string DateCompleted,string Pm,string DrawingSet,string Summary,string Status)
	    {
		    TakeoffInfo item = new TakeoffInfo();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.ProjectID = ProjectID;
				
			item.DateCompleted = DateCompleted;
				
			item.Pm = Pm;
				
			item.DrawingSet = DrawingSet;
				
			item.Summary = Summary;
				
			item.Status = Status;
				
	        item.Save(UserName);
	    }
    }
}
