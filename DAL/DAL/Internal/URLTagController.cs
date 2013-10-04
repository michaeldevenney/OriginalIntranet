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
    /// Controller class for URLTags
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class URLTagController
    {
        // Preload our schema..
        URLTag thisSchemaLoad = new URLTag();
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
        public URLTagCollection FetchAll()
        {
            URLTagCollection coll = new URLTagCollection();
            Query qry = new Query(URLTag.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public URLTagCollection FetchByID(object Id)
        {
            URLTagCollection coll = new URLTagCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public URLTagCollection FetchByQuery(Query qry)
        {
            URLTagCollection coll = new URLTagCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (URLTag.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (URLTag.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Source,string TargetObjectName,string TargetObjectPath,string Campaign,string LongTargetURL,string ShortenedtURL,bool? Synched)
	    {
		    URLTag item = new URLTag();
		    
            item.Source = Source;
            
            item.TargetObjectName = TargetObjectName;
            
            item.TargetObjectPath = TargetObjectPath;
            
            item.Campaign = Campaign;
            
            item.LongTargetURL = LongTargetURL;
            
            item.ShortenedtURL = ShortenedtURL;
            
            item.Synched = Synched;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,string Source,string TargetObjectName,string TargetObjectPath,string Campaign,string LongTargetURL,string ShortenedtURL,bool? Synched)
	    {
		    URLTag item = new URLTag();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.Source = Source;
				
			item.TargetObjectName = TargetObjectName;
				
			item.TargetObjectPath = TargetObjectPath;
				
			item.Campaign = Campaign;
				
			item.LongTargetURL = LongTargetURL;
				
			item.ShortenedtURL = ShortenedtURL;
				
			item.Synched = Synched;
				
	        item.Save(UserName);
	    }
    }
}
