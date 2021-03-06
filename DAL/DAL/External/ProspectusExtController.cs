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
    /// Controller class for ProspectusExt
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class ProspectusExtController
    {
        // Preload our schema..
        ProspectusExt thisSchemaLoad = new ProspectusExt();
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
        public ProspectusExtCollection FetchAll()
        {
            ProspectusExtCollection coll = new ProspectusExtCollection();
            Query qry = new Query(ProspectusExt.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ProspectusExtCollection FetchByID(object Id)
        {
            ProspectusExtCollection coll = new ProspectusExtCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public ProspectusExtCollection FetchByQuery(Query qry)
        {
            ProspectusExtCollection coll = new ProspectusExtCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (ProspectusExt.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (ProspectusExt.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? ExternalID,int? LinkID,string Facility,string City,string State,string Country,bool NewConstr,bool FacilityUpgrade,bool Supplemental,string OwnerRepName,string OwnerRepPhone,string OwnerRepEmail,string PhysicistName,string PhysicistPhone,string PhysicistEmail,string OtherContactType,string OtherContactName,string OtherContactPhone,string OtherContactEmail,bool ProductInfo,bool RoomLayouts,bool ConstructionDetails,bool PhysicsReport,bool BudgetEstimate,bool MeetingRequested,bool EntryDoors,bool AcceleratorPit,bool FacilityDevelopment,bool InteriorFinishes,bool DuctShielding,bool Foundations,bool Vroc,bool Quantum,DateTime? ConstructionStartDate,DateTime? MachineDelivery,DateTime? FoundationInstall,DateTime? FirstTreatmentDate,string RegulatoryDesignRequirements,string RegulatoryDesignRequirementOther,string OtherRegDesignReqPublic,string OtherRegDesignReqControlled,bool Synched)
	    {
		    ProspectusExt item = new ProspectusExt();
		    
            item.ExternalID = ExternalID;
            
            item.LinkID = LinkID;
            
            item.Facility = Facility;
            
            item.City = City;
            
            item.State = State;
            
            item.Country = Country;
            
            item.NewConstr = NewConstr;
            
            item.FacilityUpgrade = FacilityUpgrade;
            
            item.Supplemental = Supplemental;
            
            item.OwnerRepName = OwnerRepName;
            
            item.OwnerRepPhone = OwnerRepPhone;
            
            item.OwnerRepEmail = OwnerRepEmail;
            
            item.PhysicistName = PhysicistName;
            
            item.PhysicistPhone = PhysicistPhone;
            
            item.PhysicistEmail = PhysicistEmail;
            
            item.OtherContactType = OtherContactType;
            
            item.OtherContactName = OtherContactName;
            
            item.OtherContactPhone = OtherContactPhone;
            
            item.OtherContactEmail = OtherContactEmail;
            
            item.ProductInfo = ProductInfo;
            
            item.RoomLayouts = RoomLayouts;
            
            item.ConstructionDetails = ConstructionDetails;
            
            item.PhysicsReport = PhysicsReport;
            
            item.BudgetEstimate = BudgetEstimate;
            
            item.MeetingRequested = MeetingRequested;
            
            item.EntryDoors = EntryDoors;
            
            item.AcceleratorPit = AcceleratorPit;
            
            item.FacilityDevelopment = FacilityDevelopment;
            
            item.InteriorFinishes = InteriorFinishes;
            
            item.DuctShielding = DuctShielding;
            
            item.Foundations = Foundations;
            
            item.Vroc = Vroc;
            
            item.Quantum = Quantum;
            
            item.ConstructionStartDate = ConstructionStartDate;
            
            item.MachineDelivery = MachineDelivery;
            
            item.FoundationInstall = FoundationInstall;
            
            item.FirstTreatmentDate = FirstTreatmentDate;
            
            item.RegulatoryDesignRequirements = RegulatoryDesignRequirements;
            
            item.RegulatoryDesignRequirementOther = RegulatoryDesignRequirementOther;
            
            item.OtherRegDesignReqPublic = OtherRegDesignReqPublic;
            
            item.OtherRegDesignReqControlled = OtherRegDesignReqControlled;
            
            item.Synched = Synched;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,int? ExternalID,int? LinkID,string Facility,string City,string State,string Country,bool NewConstr,bool FacilityUpgrade,bool Supplemental,string OwnerRepName,string OwnerRepPhone,string OwnerRepEmail,string PhysicistName,string PhysicistPhone,string PhysicistEmail,string OtherContactType,string OtherContactName,string OtherContactPhone,string OtherContactEmail,bool ProductInfo,bool RoomLayouts,bool ConstructionDetails,bool PhysicsReport,bool BudgetEstimate,bool MeetingRequested,bool EntryDoors,bool AcceleratorPit,bool FacilityDevelopment,bool InteriorFinishes,bool DuctShielding,bool Foundations,bool Vroc,bool Quantum,DateTime? ConstructionStartDate,DateTime? MachineDelivery,DateTime? FoundationInstall,DateTime? FirstTreatmentDate,string RegulatoryDesignRequirements,string RegulatoryDesignRequirementOther,string OtherRegDesignReqPublic,string OtherRegDesignReqControlled,bool Synched)
	    {
		    ProspectusExt item = new ProspectusExt();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.ExternalID = ExternalID;
				
			item.LinkID = LinkID;
				
			item.Facility = Facility;
				
			item.City = City;
				
			item.State = State;
				
			item.Country = Country;
				
			item.NewConstr = NewConstr;
				
			item.FacilityUpgrade = FacilityUpgrade;
				
			item.Supplemental = Supplemental;
				
			item.OwnerRepName = OwnerRepName;
				
			item.OwnerRepPhone = OwnerRepPhone;
				
			item.OwnerRepEmail = OwnerRepEmail;
				
			item.PhysicistName = PhysicistName;
				
			item.PhysicistPhone = PhysicistPhone;
				
			item.PhysicistEmail = PhysicistEmail;
				
			item.OtherContactType = OtherContactType;
				
			item.OtherContactName = OtherContactName;
				
			item.OtherContactPhone = OtherContactPhone;
				
			item.OtherContactEmail = OtherContactEmail;
				
			item.ProductInfo = ProductInfo;
				
			item.RoomLayouts = RoomLayouts;
				
			item.ConstructionDetails = ConstructionDetails;
				
			item.PhysicsReport = PhysicsReport;
				
			item.BudgetEstimate = BudgetEstimate;
				
			item.MeetingRequested = MeetingRequested;
				
			item.EntryDoors = EntryDoors;
				
			item.AcceleratorPit = AcceleratorPit;
				
			item.FacilityDevelopment = FacilityDevelopment;
				
			item.InteriorFinishes = InteriorFinishes;
				
			item.DuctShielding = DuctShielding;
				
			item.Foundations = Foundations;
				
			item.Vroc = Vroc;
				
			item.Quantum = Quantum;
				
			item.ConstructionStartDate = ConstructionStartDate;
				
			item.MachineDelivery = MachineDelivery;
				
			item.FoundationInstall = FoundationInstall;
				
			item.FirstTreatmentDate = FirstTreatmentDate;
				
			item.RegulatoryDesignRequirements = RegulatoryDesignRequirements;
				
			item.RegulatoryDesignRequirementOther = RegulatoryDesignRequirementOther;
				
			item.OtherRegDesignReqPublic = OtherRegDesignReqPublic;
				
			item.OtherRegDesignReqControlled = OtherRegDesignReqControlled;
				
			item.Synched = Synched;
				
	        item.Save(UserName);
	    }
    }
}
