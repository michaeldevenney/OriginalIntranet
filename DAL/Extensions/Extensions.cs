using SubSonic;
using System;

namespace DAL
{
    public partial class Project : ActiveRecord<Project>, IActiveRecord
    {
        public enum ProjectStatus { Active, Archived, Tentative }

        public string DisplayName
        {
            get { return "(" + ProjectNumber + ") " + ProjectName; }
        }

        public static ProjectCollection GetProjectsByStatus(ProjectStatus inStatus)
        {
            if (inStatus == ProjectStatus.Active)
            {
                return new Select().From<Project>()
                .Where("ID").In(new Select("ID").From<ActiveProject>())
                .ExecuteAsCollection<ProjectCollection>();
            }
            else
            {
                return new Select().From<Project>()
                    .Where("ProjectActivity").IsEqualTo(inStatus.ToString())
                    .And("ProjectLead").IsEqualTo("Project")
                    .And("ProjectNumber").IsNotEqualTo("_Archi")
                    .OrderAsc("ProjectNumber")
                    .ExecuteAsCollection<ProjectCollection>();
            }
        }

        public static ProjectCollection GetLeadsByStatus(ProjectStatus inStatus)
        {
            return new Select().From<Project>()
                .Where("ProjectActivity").IsEqualTo(inStatus.ToString())
                .And("ProjectLead").IsEqualTo("Lead")
                .OrderAsc("ProjectNumber")
                .ExecuteAsCollection<ProjectCollection>();
        }

        public static ProjectCollection GetAllProjects()
        {
            return new Select().From<Project>()
                .Where("ProjectLead").IsEqualTo("Project")
                .OrderAsc("ProjectNumber")
                .ExecuteAsCollection<ProjectCollection>();
        }

        public static ProjectCollection GetAllLeads()
        {
            return new Select().From<Project>()
                .Where("ProjectLead").IsEqualTo("Lead")
                .OrderAsc("ProjectNumber")
                .ExecuteAsCollection<ProjectCollection>();
        }

        public static Project GetProjectByID(int inProjectID)
        {
            return new Project(inProjectID);
        }

        public static Project GetLeadByID(int inLeadID)
        {
            return new Project(inLeadID);
        }

        /// <summary>
        /// Get all leads that have at least one Estimate
        /// </summary>
        /// <returns>Lead Collection</returns>
        public static ProjectCollection AllLeadsWithEstimate()
        {
            return new Select().From<Project>()
                .Where("ProjectLead").IsEqualTo("Lead")
                .And("ID").In(new Select("JobName")
                                .From("EstimatesAll")
                                .Where("EstimateSent").IsEqualTo(true))
                .OrderAsc("ProjectNumber")
                .ExecuteAsCollection<ProjectCollection>();
        }

        public static string GetNextProjectNumber()
        {
            return new Select("num").From<NextProjectNumber>()
                .ExecuteScalar<string>();
        }

    }

    public partial class Report : ActiveRecord<Report>, IActiveRecord
    {
        /// <summary>
        /// Returns a collection of reports with links to SSRS server objects and name for display
        /// </summary>
        /// <param name="reportList">Name of report list to return, use ALL to return all reports.</param>
        /// <returns>ReportCollection</returns>
        public static ReportCollection GetReports(string reportList)
        {
            if (reportList == "ALL")
            {
                return new Select().From<Report>()
                    .Where("Active").IsEqualTo(true)
                    .OrderAsc("ReportName")
                    .ExecuteAsCollection<ReportCollection>();
            }
            else
            {
                return new Select().From<Report>()
                    .Where("List").IsEqualTo(reportList)
                    .And("Active").IsEqualTo(true)
                    .OrderAsc("ReportName")
                    .ExecuteAsCollection<ReportCollection>();
            }
        }
    }

    public partial class Workflow : ActiveRecord<Workflow>, IActiveRecord
    {
        public static WorkflowCollection GetRemainingStepsForEstimate(int currentStepID)
        {
            int currentStep;
            Workflow temp = new Workflow(currentStepID);
            currentStep = temp.StepX;

            if (temp.StepX == 99)
                currentStep = 0;

            return new Select().From<Workflow>()
               .Where("Application").IsEqualTo("EstimateTracking")
                .And("Step").IsGreaterThan(currentStep)
               .OrderAsc("Step")
               .ExecuteAsCollection<WorkflowCollection>();
        }

        public static string GetResponsibleJobTitleForWorkflowStep(string workflowName, int workflowStepID)
        {
            Workflow temp = new Select().From<Workflow>()
                    .Where("Application").IsEqualTo(workflowName)
                    .And("ID").IsEqualTo(workflowStepID)
                    .ExecuteSingle<Workflow>();

            return temp.ActionParameter;
        }

    }

    public partial class EstimateTimeline : ActiveRecord<EstimateTimeline>, IActiveRecord
    {
        public static EstimateTimeline GetCurrentTimelineStep(int estimateId)
        {
            EstimateTimelineCollection temp = new Select().From<EstimateTimeline>()
                .Where("EstimateId").IsEqualTo(estimateId)
                .OrderDesc("DateTimeStamp")
                .ExecuteAsCollection<EstimateTimelineCollection>();

            if (temp == null)
                return null;

            return temp[0];
        }
    }

    public partial class Estimate : ActiveRecord<Estimate>, IActiveRecord
    {
        #region PROPERTIES

        public string DisplayText
        {
            get 
            {
                Project prospect = new Project(this.JobName);
                return "(" + prospect.ProjectNumber + ") " + prospect.ProjectName + " - " + this.EstimateNumber;
            }
        }

        #endregion

        #region ENUMS
        
        public enum DirType { Exec, Prospect, EstimateNumber }
        public enum Filter { Active, All }
        
        #endregion
        
        #region METHODS        

        public static void SetPriorityToZero(int estimateID)
        {
            Estimate temp = new Estimate(estimateID);
            temp.Priority = 0;
            temp.Save();
        }

        public static EstimateTimelineCollection GetCurrentEstimateTimeline(int estimateId)
        {
            string[] orderBy = { "DateTimeStamp" };

            return new Select().From<EstimateTimeline>()
                .Where("EstimateId").IsEqualTo(estimateId)
                .OrderAsc(orderBy)
                .ExecuteAsCollection<EstimateTimelineCollection>();
        }

        /// <summary>
        /// removes priority from estimates that are sent or dropped from the estimating queue
        /// </summary>
        public static void RemovePriorityFromClosedEstimates()
        {
            EstimateCollection temp = GetPrioritizedClosedEstimates();
            int? x = 1;

            foreach (Estimate est in temp)
            {
                est.Priority = 0;
                est.Save();
                x++;
            }

        }

        public static void UpdateEstimatePriority()
        {
            EstimateCollection temp = GetAllPrioritizedEstimates();
            int? x = 1;

            foreach (Estimate est in temp)
            {
                est.Priority = x;
                est.Save();
                x++;
            }

        }

        public static void ChangePriority(Estimate inEst, string inDirection)
        {
            switch (inDirection)
            {
                case "UP":
                    // return if move is impossible.
                    if (inEst.Priority == 1)
                        break;

                    // Get estimate with next higer priority (lower #)
                    Estimate neighborUp = Estimate.GetEstimateByPriority(inEst.Priority - 1);
                    inEst.Priority = inEst.Priority - 1;
                    neighborUp.Priority = neighborUp.Priority + 1;

                    inEst.Save();
                    neighborUp.Save();
                    break;

                case "DOWN":
                    EstimateCollection allEsts = Estimate.GetAllPrioritizedEstimates();

                    int lastItem = allEsts.Count - 1;

                    // return if move is impossible.
                    if (inEst.Priority == allEsts[lastItem].Priority)
                        break;

                    // Get estimate with next lower Priority (higher #)
                    Estimate neighborDown = Estimate.GetEstimateByPriority(inEst.Priority + 1);
                    inEst.Priority = inEst.Priority + 1;
                    neighborDown.Priority = neighborDown.Priority - 1;

                    inEst.Save();
                    neighborDown.Save();
                    break;

                case "FIRST":
                    EstimateCollection allLower = Estimate.GetAllEstimatesWithLowerPriority(inEst.Priority);

                    foreach (Estimate est in allLower)
                    {
                        est.Priority++;
                        est.Save();
                    }

                    inEst.Priority = 1;
                    inEst.Save();

                    break;

                case "LAST":
                    EstimateCollection allHigher = Estimate.GetAllEstimatesWithGreaterPriority(inEst.Priority);

                    foreach (Estimate est in allHigher)
                    {
                        est.Priority--;
                        est.Save();
                    }

                    EstimateCollection temp = Estimate.GetAllPrioritizedEstimates();
                    inEst.Priority = temp.Count;
                    inEst.Save();

                    break;

                default:
                    break;
            }
        }

        public static Estimate GetEstimateByPriority(int? inPriority)
        {
            return new Select().From<Estimate>()
                .Where("Priority").IsEqualTo(inPriority)
                .ExecuteSingle<Estimate>();
        }

        public static EstimateCollection GetEditableActiveEstimates()
        {
            return new Select().From<Estimate>()
                .Where("ID").In(new Select("EstID").From("EstimatesActive"))
                .OrderAsc("Priority")
                .ExecuteAsCollection<EstimateCollection>();
        }

        public static EstimateCollection GetEstimatesForJob(int inProspectID)
        {
            return new Select().From<Estimate>()
                .Where("JobName").IsEqualTo(inProspectID)
                .OrderAsc("EstimateNumber")
                .ExecuteAsCollection<EstimateCollection>();
        }

        public static EstimateItemCollection GetDoors(int inEstimateID)
        {
            return new Select().From<EstimateItem>()
                .Where("EstimateID").IsEqualTo(inEstimateID)
                .And("ItemType").IsEqualTo("Door")
                .ExecuteAsCollection<EstimateItemCollection>();
        }

        public static EstimateItemCollection GetRooms(int inEstimateID)
        {
            return new Select().From<EstimateItem>()
                .Where("EstimateID").IsEqualTo(inEstimateID)
                .And("ItemType").IsEqualTo("Room")
                .ExecuteAsCollection<EstimateItemCollection>();
        }

        /// <summary>
        /// Estimate requests not yet ready for the Estimating Department
        /// </summary>
        /// <returns>EstimateCollection </returns>
        public static EstimatesSalesQueueCollection GetSalesQueue()
        {            
            return new Select().From<EstimatesSalesQueue>()                
                .OrderAsc("DisplayText")
                .ExecuteAsCollection<EstimatesSalesQueueCollection>();
        }

        public static EstimateCollection GetPrioritizedClosedEstimates()
        {
            return new Select().From<Estimate>()
                .Where("ReadyForEstimating").IsEqualTo(false)
                .And("Priority").IsGreaterThan(0)
                .OrderAsc("Priority")
                .ExecuteAsCollection<EstimateCollection>();
        }

        public static EstimateCollection GetAllPrioritizedEstimates()
        {
            return new Select().From<Estimate>()
                .Where("ReadyForEstimating").IsEqualTo(true)
                .And("Priority").IsGreaterThan(0)
                .OrderAsc("Priority")
                .ExecuteAsCollection<EstimateCollection>();
        }

        public static EstimateCollection GetAllEstimatesWithGreaterPriority(int? inPriority)
        {
            return new Select().From<Estimate>()
                .Where("EstimateSentDate").IsNull()
                .And("Priority").IsGreaterThan(inPriority)
                .ExecuteAsCollection<EstimateCollection>();
        }

        public static EstimateCollection GetAllEstimatesWithLowerPriority(int? inPriority)
        {
            return new Select().From<Estimate>()
                .Where("EstimateSentDate").IsNull()
                .And("Priority").IsLessThan(inPriority)
                .ExecuteAsCollection<EstimateCollection>();
        }

        public static string GetNextEstimateNumberForJob(int? prospectID)
        {
            EstimateCollection temp = new Select().From<Estimate>()
                .Where("JobName").IsEqualTo(prospectID)
                .ExecuteAsCollection<EstimateCollection>();

            int tmpDate = DateTime.Today.Year;
            string yearString = tmpDate.ToString().Substring(2);

            if (temp.Count == 0)
            {
                return prospectID + "-01";
            }
            else
            {
                string estNumber = (temp.Count > 9 ? (temp.Count + 1).ToString() : "0" + (temp.Count + 1).ToString());
                return prospectID + "-" + estNumber;
            }
        }

        public static EstimatesActiveCollection GetActive()
        {
            return new Select().From<EstimatesActive>()                                
                .OrderAsc("Priority")
                .ExecuteAsCollection<EstimatesActiveCollection>();
        }

        public static EstimatesAllCollection GetAll()
        {
            return new Select().From<EstimatesAll>()                                
                .OrderAsc("DisplayText")
                .ExecuteAsCollection<EstimatesAllCollection>();
        }
                
        public static int? GetNextPriority()
        {
            EstimateCollection temp = new
                Select().From<Estimate>()
                .Where("EstimateSentDate").IsNull()
                .And("ReadyForEstimating").IsEqualTo(true)
                .OrderDesc("Priority")
                .ExecuteAsCollection<EstimateCollection>();

            return temp[0].Priority + 1;
        }

        #endregion

    }

    public partial class User : ActiveRecord<User>, IActiveRecord
    {
        public enum UserStatus { Active, Terminated }

        public string Initials
        {
            get 
            {
                string initials;
                string name = this.Name;
                int space = name.IndexOf(" ");
                initials = name.Substring(0, 1) + name.Substring(space, 1);
                return initials;
            }
        }

        public string FirstName
        {
            get
            {
                string firstName;
                string name = this.Name;
                int space = name.IndexOf(" ");
                firstName = name.Substring(0, space);
                return firstName;
            }
        }

        public static UserCollection GetUsersByTitle(string inTitle)
        {
            return new Select().From<User>()
                .Where("Position").IsEqualTo(inTitle)
                .And("Status").IsEqualTo("Active")
                .OrderAsc("Name")
                .ExecuteAsCollection<UserCollection>();
        }

        public static int? GetUserIDFromLogin(string inUsername)
        {
            string userLogin = inUsername.Substring(inUsername.IndexOf("\\") + 1);
            User u = new Select().From<User>().Where("Username").IsEqualTo(userLogin).ExecuteSingle<User>();
            return u.Id;
        }

        public static User GetUserFromLogin(string inUsername)
        {
            string userLogin = inUsername.Substring(inUsername.IndexOf("\\") + 1);
            return new Select().From<User>().Where("Username").IsEqualTo(userLogin).ExecuteSingle<User>();

        }

        public static User GetUserByID(int p)
        {
            return new User(p);
        }

        public static User GetUserFromFullName(string inName)
        {
            return new Select().From<User>().Where("Name").IsEqualTo(inName).ExecuteSingle<User>();
        }
        
        public static UserCollection GetUsersByStatus(UserStatus inStatus)
        {
            if (inStatus == UserStatus.Active)
            {
                return new Select().From<User>()
                     .Where("ID").In(new Select("ID").From<ActiveUser>())
                     .ExecuteAsCollection<UserCollection>();
            }
            else
            {
                return new Select().From<User>()
                     .Where("Status").IsEqualTo(inStatus)
                     .ExecuteAsCollection<UserCollection>();
            }

        }

        public static UserCollection GetUsersNotSynchronized()
        {
            return new Select().From<User>()
                .Where("Synched").IsEqualTo(false)
                .And("Name").IsNotEqualTo("Unassigned")
                .ExecuteAsCollection<UserCollection>();
        }       
    }

    public partial class Lookup : ActiveRecord<Lookup>, IActiveRecord
    {
        public static LookupCollection GetLookupList(string inListName)
        {
            return new Select().From<Lookup>()
                .Where("LookupList").IsEqualTo(inListName)
                .ExecuteAsCollection<LookupCollection>();
        }

        public static LookupCollection GetAllListNames()
        {
            string[] fields = { "LookupList" };

            return new Select(fields)
                .From<Lookup>()
                .Distinct()
                .ExecuteAsCollection<LookupCollection>();
        }

        public static LookupCollection GetDistinctLookupValues(string inListName)
        {
            string[] fields = { "LookupValue" };

            return new Select(fields)
                .From<Lookup>()
                .Distinct()
                .Where("LookupList").IsEqualTo(inListName)
                .ExecuteAsCollection<LookupCollection>();
        }


        public static LookupCollection GetSecondaryLookupList(string inCurrentList, string inCurrentListItem)
        {
            string[] fields = { "LookupList" };

            return new Select(fields)
                .From<Lookup>()
                .Distinct()
                .Where("LookupList").IsEqualTo(inCurrentList)
                .And("LookupValue").IsEqualTo(inCurrentListItem)
                .ExecuteAsCollection<LookupCollection>();

        }
    }

    public partial class Prospectu : ActiveRecord<Prospectu>, IActiveRecord
    {
        public static ProspectRoomCollection GetRooms(int inProspectusID)
        {
            return new Select().From<ProspectRoom>()
                .Where("ProspectusID").IsEqualTo(inProspectusID)
                .ExecuteAsCollection<ProspectRoomCollection>();
        }

        public static bool HasRooms(int inProspectusID, out int outRoomCount)
        {
            ProspectRoomCollection temp = new Select().From<ProspectRoom>()
                .Where("ProspectusID").IsEqualTo(inProspectusID)
                .ExecuteAsCollection<ProspectRoomCollection>();

            outRoomCount = temp.Count;

            return (temp.Count > 0);
        }

        public static void AddRooms(int numberOfRooms, int prospectusID)
        {
            for (int i = 1; i <= numberOfRooms; i++)
            {
                ProspectRoom temp = new ProspectRoom();
                temp.ProspectusID = prospectusID;
                temp.Description = "New Room" + i.ToString();
                temp.Save();
            }
        }

        public static PhysicsBasiCollection GetPhysicsRegulationsList()
        {
            return new Select().From<PhysicsBasi>()
                .ExecuteAsCollection<PhysicsBasiCollection>();
        }
    }   

    public partial class ProspectRoom : ActiveRecord<ProspectRoom>, IActiveRecord
    {
        public static LookupCollection GetRoomTypes()
        {
            return new Select("LookupValue").From<Lookup>()
                .Distinct()
                .Where("LookupList").IsEqualTo("ProspectusRooms")
                .ExecuteAsCollection<LookupCollection>();
        }

        public static LookupCollection GetVendorMachineListing(string inRoomType)
        {
            return new Select().From<Lookup>()
                .Where("LookupList").IsEqualTo("ProspectusRooms")
                .And("LookupValue").IsEqualTo(inRoomType)
                .ExecuteAsCollection<LookupCollection>();
        }
    }    

    public partial class Workflow : ActiveRecord<Workflow>, IActiveRecord
    {
        public static WorkflowCollection GetWorkflowStepByControl(string inWorkFlowName, string inTriggerControl)
        {
            return new Select().From<Workflow>()
                .Where("Control").IsEqualTo(inTriggerControl)
                .And("Application").IsEqualTo(inWorkFlowName)
                .ExecuteAsCollection<WorkflowCollection>();
        }
    }

    public partial class PhysicsBasi : ActiveRecord<PhysicsBasi>, IActiveRecord
    {
        public static PhysicsBasiCollection GetPhysicsBasis()
        {
            return new Select().From<PhysicsBasi>()
                  .ExecuteAsCollection<PhysicsBasiCollection>();
        }
    }

    public partial class URLTag : ActiveRecord<URLTag>, IActiveRecord
    {
        public static URLTagCollection GetNewTags()
        {
            return new Select().From<URLTag>()
                .Where("Synched").IsEqualTo(false)
                .ExecuteAsCollection<URLTagCollection>();
        }
    }
}

namespace DALRemote
{
    public partial class ProspectusExt : ActiveRecord<ProspectusExt>, IActiveRecord
    {
        public static bool Exists(int inLinkID)
        {
            ProspectusExtCollection temp =
                new Select().From<ProspectusExt>()
                .Where("LinkID").IsEqualTo(inLinkID)
                .ExecuteAsCollection<ProspectusExtCollection>();

            return temp.Count > 0;
        }

        public static ProspectusExt GetByLinkID(int inLinkID)
        {
            return new Select().From<ProspectusExt>()
                .Where("LinkID").IsEqualTo(inLinkID)
                .ExecuteSingle<ProspectusExt>();
        }

        public static ProspectusLinkRequestExtCollection GetNewLinks()
        {
            return new Select().From<ProspectusLinkRequestExt>()
                .Where("Synched").IsEqualTo(false)
                .ExecuteAsCollection<ProspectusLinkRequestExtCollection>();
        }

        public static ProspectusExtCollection GetNewProspecti()
        {
            return new Select().From<ProspectusExt>()
                .Where("Synched").IsEqualTo(false)
                .ExecuteAsCollection<ProspectusExtCollection>();
        }

        public static ProspectRoomExtCollection GetRooms(int inProspectusID)
        {
            return new Select().From<ProspectRoomExt>()
                .Where("ProspectusID").IsEqualTo(inProspectusID)
                .ExecuteAsCollection<ProspectRoomExtCollection>();
        }

        public static bool HasRooms(int inProspectusID, out int outRoomCount)
        {
            ProspectRoomExtCollection temp = new Select().From<ProspectRoomExt>()
                .Where("ProspectusID").IsEqualTo(inProspectusID)
                .ExecuteAsCollection<ProspectRoomExtCollection>();

            outRoomCount = temp.Count;

            return (temp.Count > 0);
        }

        public static void AddRooms(int numberOfRooms, int prospectusID)
        {
            for (int i = 1; i <= numberOfRooms; i++)
            {
                ProspectRoomExt temp = new ProspectRoomExt();
                temp.ProspectusID = prospectusID;
                temp.Description = "New Room" + i.ToString();
                temp.Save();
            }
        }

        public static PhysicsBasisExtCollection GetPhysicsRegulationsList()
        {
            return new Select().From<PhysicsBasisExt>()
                .ExecuteAsCollection<PhysicsBasisExtCollection>();
        }
    }

    public partial class ProspectRoomExt : ActiveRecord<ProspectRoomExt>, IActiveRecord
    {
        public static LookupsExtCollection GetRoomTypes()
        {
            return new Select("LookupValue").From<LookupsExt>()
                .Distinct()
                .Where("LookupList").IsEqualTo("ProspectusRooms")
                .ExecuteAsCollection<LookupsExtCollection>();
        }

        public static LookupsExtCollection GetVendorMachineListing(string inRoomType)
        {
            return new Select().From<LookupsExt>()
                .Where("LookupList").IsEqualTo("ProspectusRooms")
                .And("LookupValue").IsEqualTo(inRoomType)
                .ExecuteAsCollection<LookupsExtCollection>();
        }
    }
}