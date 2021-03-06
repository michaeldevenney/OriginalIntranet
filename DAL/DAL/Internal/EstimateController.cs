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
    /// Controller class for Estimates
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class EstimateController
    {
        // Preload our schema..
        Estimate thisSchemaLoad = new Estimate();
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
        public EstimateCollection FetchAll()
        {
            EstimateCollection coll = new EstimateCollection();
            Query qry = new Query(Estimate.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public EstimateCollection FetchByID(object Id)
        {
            EstimateCollection coll = new EstimateCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public EstimateCollection FetchByQuery(Query qry)
        {
            EstimateCollection coll = new EstimateCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (Estimate.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (Estimate.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? Priority,int JobName,string EstimateNumber,bool ReadyForEstimating,DateTime? EstimateDate,string Contact,string ContactEmail,DateTime? Received,DateTime? BidDueDate,int? Estimator,string EstimatesDirectory,string ProspectDirectory,DateTime? ResponseRequestedBy,bool EstimateSent,DateTime? EstimateSentDate,decimal? EstimateTotal,decimal? DoorsTotal,decimal? InteriorsTotal,decimal? BunkerTotal,int StatusID,int StepID,string CurrentStatus,string DoorScope,string InteriorScope,string BunkerScope,string BunkerTitle,string DesignBasis,string LaborType,string PhysicsBasis,string BunkerClarifications,bool? Drawings,bool? Prospectus,bool? TxParameters,string EstimateDescription,int? SupplementalBlockCount,int? InteriorsQty)
	    {
		    Estimate item = new Estimate();
		    
            item.Priority = Priority;
            
            item.JobName = JobName;
            
            item.EstimateNumber = EstimateNumber;
            
            item.ReadyForEstimating = ReadyForEstimating;
            
            item.EstimateDate = EstimateDate;
            
            item.Contact = Contact;
            
            item.ContactEmail = ContactEmail;
            
            item.Received = Received;
            
            item.BidDueDate = BidDueDate;
            
            item.Estimator = Estimator;
            
            item.EstimatesDirectory = EstimatesDirectory;
            
            item.ProspectDirectory = ProspectDirectory;
            
            item.ResponseRequestedBy = ResponseRequestedBy;
            
            item.EstimateSent = EstimateSent;
            
            item.EstimateSentDate = EstimateSentDate;
            
            item.EstimateTotal = EstimateTotal;
            
            item.DoorsTotal = DoorsTotal;
            
            item.InteriorsTotal = InteriorsTotal;
            
            item.BunkerTotal = BunkerTotal;
            
            item.StatusID = StatusID;
            
            item.StepID = StepID;
            
            item.CurrentStatus = CurrentStatus;
            
            item.DoorScope = DoorScope;
            
            item.InteriorScope = InteriorScope;
            
            item.BunkerScope = BunkerScope;
            
            item.BunkerTitle = BunkerTitle;
            
            item.DesignBasis = DesignBasis;
            
            item.LaborType = LaborType;
            
            item.PhysicsBasis = PhysicsBasis;
            
            item.BunkerClarifications = BunkerClarifications;
            
            item.Drawings = Drawings;
            
            item.Prospectus = Prospectus;
            
            item.TxParameters = TxParameters;
            
            item.EstimateDescription = EstimateDescription;
            
            item.SupplementalBlockCount = SupplementalBlockCount;
            
            item.InteriorsQty = InteriorsQty;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,int? Priority,int JobName,string EstimateNumber,bool ReadyForEstimating,DateTime? EstimateDate,string Contact,string ContactEmail,DateTime? Received,DateTime? BidDueDate,int? Estimator,string EstimatesDirectory,string ProspectDirectory,DateTime? ResponseRequestedBy,bool EstimateSent,DateTime? EstimateSentDate,decimal? EstimateTotal,decimal? DoorsTotal,decimal? InteriorsTotal,decimal? BunkerTotal,int StatusID,int StepID,string CurrentStatus,string DoorScope,string InteriorScope,string BunkerScope,string BunkerTitle,string DesignBasis,string LaborType,string PhysicsBasis,string BunkerClarifications,bool? Drawings,bool? Prospectus,bool? TxParameters,string EstimateDescription,int? SupplementalBlockCount,int? InteriorsQty)
	    {
		    Estimate item = new Estimate();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.Priority = Priority;
				
			item.JobName = JobName;
				
			item.EstimateNumber = EstimateNumber;
				
			item.ReadyForEstimating = ReadyForEstimating;
				
			item.EstimateDate = EstimateDate;
				
			item.Contact = Contact;
				
			item.ContactEmail = ContactEmail;
				
			item.Received = Received;
				
			item.BidDueDate = BidDueDate;
				
			item.Estimator = Estimator;
				
			item.EstimatesDirectory = EstimatesDirectory;
				
			item.ProspectDirectory = ProspectDirectory;
				
			item.ResponseRequestedBy = ResponseRequestedBy;
				
			item.EstimateSent = EstimateSent;
				
			item.EstimateSentDate = EstimateSentDate;
				
			item.EstimateTotal = EstimateTotal;
				
			item.DoorsTotal = DoorsTotal;
				
			item.InteriorsTotal = InteriorsTotal;
				
			item.BunkerTotal = BunkerTotal;
				
			item.StatusID = StatusID;
				
			item.StepID = StepID;
				
			item.CurrentStatus = CurrentStatus;
				
			item.DoorScope = DoorScope;
				
			item.InteriorScope = InteriorScope;
				
			item.BunkerScope = BunkerScope;
				
			item.BunkerTitle = BunkerTitle;
				
			item.DesignBasis = DesignBasis;
				
			item.LaborType = LaborType;
				
			item.PhysicsBasis = PhysicsBasis;
				
			item.BunkerClarifications = BunkerClarifications;
				
			item.Drawings = Drawings;
				
			item.Prospectus = Prospectus;
				
			item.TxParameters = TxParameters;
				
			item.EstimateDescription = EstimateDescription;
				
			item.SupplementalBlockCount = SupplementalBlockCount;
				
			item.InteriorsQty = InteriorsQty;
				
	        item.Save(UserName);
	    }
    }
}
