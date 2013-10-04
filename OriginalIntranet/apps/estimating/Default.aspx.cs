using System.Drawing;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Shared;
using sites;

public partial class apps_estimating_Default : System.Web.UI.Page
{
    public Estimate currentEstimate;
    Project currentProspect;
    User currentSalesperson;
    User currentEstimator;

    #region CONTROL EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(customXertificateValidation);

            Label lblHeading;
            lblHeading = (Label)Master.FindControl("lblPageHeading");
            lblHeading.Text = "Veritas - Estimating - Estimate Tracking";

            txtAdditionalInformation.Enabled = false;

            BindControls(Estimate.Filter.Active);
        }
        else
        {
            if (currentEstimate == null)
                currentEstimate = new Estimate(lstEstimates.SelectedItem.Value);
            if (currentProspect == null)
                currentProspect = new Project(currentEstimate.JobName);
            if (currentSalesperson == null)
                currentSalesperson = new User(currentProspect.SalespersonID);
            if (currentEstimator == null)
                currentEstimator = new User(currentEstimate.Estimator);
        }
    }

    protected void cmdAddStep_Click(object sender, EventArgs e)
    {
        // Populate and save step into timeline for this estimate
        EstimateTimeline newTimelineStep = new EstimateTimeline();

        // Retrieve workflow step information
        Workflow currentWorkflowStep = new Workflow("Action", ddlWorkflowStep.SelectedItem.Text);

        // Management Review step has no responsible user - at this point
        DAL.User respUser;

        if (ddlResponsible.SelectedItem != null)
        {
            respUser = new User(int.Parse(ddlResponsible.SelectedItem.Value));
            newTimelineStep.ResponsibleUserID = respUser.Id;
        }

        // set date and time for occurrence of current step 
        DateTime stamp = DateTime.Now;

        // write values into new timeline step
        newTimelineStep.EstimateID = currentEstimate.Id;
        newTimelineStep.WorkflowStepID = currentWorkflowStep.Id;
        newTimelineStep.StepX = currentWorkflowStep.StepX;
        newTimelineStep.DateTimeStamp = stamp;

        // if user input of additional info is necessary, append to comments field
        if (currentWorkflowStep.Id == 126 || currentWorkflowStep.Id == 132 || currentWorkflowStep.Id == 134 || currentWorkflowStep.Id == 133)
            newTimelineStep.Comments = txtAdditionalInformation.Text;
        
        newTimelineStep.Save();
                     
        
        // if this is the final "Sent" workflow step remove the Outlook folder and 
        // MoveOutlookFolder(currentProspect.ProjectNumber + " - " + currentProspect.ProjectName, "Sent");
        
        // Perform any associated workflow items for this step
        WorkflowStep(currentWorkflowStep);

        // update current estimate fields
        string status = SetEstimateStatus(currentWorkflowStep, stamp, "Add");
        if (currentWorkflowStep.Id == 126 || currentWorkflowStep.Id == 132 || currentWorkflowStep.Id == 133 || currentWorkflowStep.Id == 134)
        {
            currentEstimate.CurrentStatus = status + " - " + txtAdditionalInformation.Text;
            newTimelineStep.DisplayText = status + " - " + txtAdditionalInformation.Text;
        }
        else
        {
            currentEstimate.CurrentStatus = status;
            newTimelineStep.DisplayText = status;
        }

        if (currentWorkflowStep.Id == 126 || currentWorkflowStep.Id == 133)
        {
            currentEstimate.ReadyForEstimating = false;
        }

 
        currentEstimate.StepID = currentWorkflowStep.Id;
        currentEstimate.StatusID = currentWorkflowStep.StepX;
        
        newTimelineStep.Save();

        // Save current estimate and clear entry fields
        SaveEstimate();
        txtAdditionalInformation.Text = "";

        // Repopulate the estimate timeline
        lstTimeline.DataSource = Estimate.GetCurrentEstimateTimeline(currentEstimate.Id);
        lstTimeline.DataTextField = "DisplayText";
        lstTimeline.DataValueField = "Id";
        lstTimeline.DataBind();

        // RE POPULATE ddlWorkflowStep with remaining steps for estimate
        ddlWorkflowStep.DataSource = Workflow.GetRemainingStepsForEstimate(currentEstimate.StatusID);
        ddlWorkflowStep.DataTextField = "Action";
        ddlWorkflowStep.DataValueField = "ID";
        ddlWorkflowStep.DataBind();

        lblCurrentStatus.Text = currentEstimate.CurrentStatus;

    }

    protected void cmdEditStep_Click(object sender, EventArgs e)
    {
        // Retrieve timeline record
        int timelineID = int.Parse(lstTimeline.SelectedValue);
        EstimateTimeline temp = new EstimateTimeline(timelineID);

        // Make updates to fields based on user entry
        temp.Comments = txtAdditionalInfoEdit.Text;
        temp.DateTimeStamp = DateTime.Parse(txtDateTimeStampEdit.Text);
        temp.ResponsibleUserID = int.Parse(ddlResponsibleEdit.SelectedValue);
        
        Workflow step = new Workflow(temp.WorkflowStepID);
        
        // Generate displayed status string for timeline step
        string status = SetEstimateStatus(step, temp.DateTimeStamp, "Edit");

        // If this is the current step for the Estimate, apply changes to Displayed status.
        if (temp.WorkflowStepID == currentEstimate.StepID)
        {  
            // if additional info present, append to comments field
            if (step.Id == 126 || step.Id == 132 || step.Id == 134 || step.Id == 133)
            {
                status += " - " + txtAdditionalInfoEdit.Text;
                temp.Comments = txtAdditionalInfoEdit.Text;
                currentEstimate.CurrentStatus = status;
            }
            else
            {
                currentEstimate.CurrentStatus = status;
            }

            // only save CurrentEstimate if we're updating the current status...
            currentEstimate.Save();
        }

        temp.DisplayText = status;
        temp.Save();

        lblCurrentStatus.Text = status;

        // Repopulate the estimate timeline
        lstTimeline.DataSource = Estimate.GetCurrentEstimateTimeline(currentEstimate.Id);
        lstTimeline.DataTextField = "DisplayText";
        lstTimeline.DataValueField = "Id";
        lstTimeline.DataBind();

        // RE POPULATE ddlWorkflowStep with remaining steps for estimate
        ddlWorkflowStep.DataSource = Workflow.GetRemainingStepsForEstimate(currentEstimate.StatusID);
        ddlWorkflowStep.DataTextField = "Action";
        ddlWorkflowStep.DataValueField = "ID";
        ddlWorkflowStep.DataBind();
        
    }

    protected void cmdDelDoor_Click(object sender, EventArgs e)
    {
        int doorId = int.Parse(lstDoors.SelectedValue);

        EstimateItem.Delete(doorId);
        
        // refresh doors list data
        lstDoors.DataSource = DAL.Estimate.GetDoors(currentEstimate.Id);
        lstDoors.DataTextField = "DisplayText";
        lstDoors.DataValueField = "Id";
        lstDoors.DataBind();
    }

    protected void cmdDelVault_Click(object sender, EventArgs e)
    {
        int vaultId = int.Parse(lstVaults.SelectedValue);

        EstimateItem.Delete(vaultId);
        
        //refresh vaults list data
        lstVaults.DataSource = DAL.Estimate.GetRooms(currentEstimate.Id);
        lstVaults.DataTextField = "DisplayText";
        lstVaults.DataValueField = "Id";
        lstVaults.DataBind();
    }

    protected void ddlEstFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        // wha choo wanna see?
        Estimate.Filter filter = (ddlEstFilter.SelectedItem.Text == "Active" ? Estimate.Filter.Active : Estimate.Filter.All);         
        BindControls(filter);
    }       

    public void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    public void ddlPhysicsBasis_SelectedIndexChanged(object sender, EventArgs e)
    {
        PhysicsBasi currentPhysics = new PhysicsBasi(ddlPhysicsBasis.SelectedItem.Value);
        this.txtPhysicsBasis.Text = ddlPhysicsBasis.SelectedItem.Text + " as " + currentPhysics.TextX;
    }

    public void lstEstimates_SelectedIndexChanged(object sender, EventArgs e)
    {
        int? estID = int.Parse(lstEstimates.SelectedItem.Value);

        currentEstimate = new Estimate(estID);
        currentProspect = new Project(currentEstimate.JobName);
        currentSalesperson = new User(currentProspect.SalespersonID);
        currentEstimator = new User(currentEstimate.Estimator);

        // Room and Door Lists
        lstDoors.DataSource = DAL.Estimate.GetDoors(currentEstimate.Id);
        lstDoors.DataTextField = "DisplayText";
        lstDoors.DataValueField = "Id";
        lstDoors.DataBind();

        if (lstDoors.Items.Count > 0)
            cmdDelDoor.Enabled = true;

        lstVaults.DataSource = DAL.Estimate.GetRooms(currentEstimate.Id);
        lstVaults.DataTextField = "DisplayText";
        lstVaults.DataValueField = "Id";
        lstVaults.DataBind();

        if (lstVaults.Items.Count > 0)
            cmdDelVault.Enabled = true;

        FillForm();
    }
    
    public void cmdSave_Click(object sender, EventArgs e)
    {
        SaveEstimate();
    }

    public void cmdNew_Click(object sender, EventArgs e)
    {
        currentEstimate = CreateEstimate();
        FillForm();
    }

    public void cmdDelete_Click(object sender, EventArgs e)
    {
        int? estID = int.Parse(lstEstimates.SelectedItem.Value);

        // Not just yet.
        //Directory.Delete(currentEstimate.EstimatesDirectory);
        //Directory.Delete(currentEstimate.ProspectDirectory);

        Estimate.Delete(estID);
        lstEstimates.Items.Clear();

        BindControls(Estimate.Filter.Active);
    }

    protected void cmdAddRoom_Click(object sender, EventArgs e)
    {
        txtVaultQty.BackColor = Color.White;
        ddlEntryType.BackColor = Color.White;
        ddlRoomType.BackColor = Color.White;

        // fake validation
        if (txtVaultQty.Text.Length < 1)
        {
            txtVaultQty.BackColor = Color.Red;
            return;
        }
        else
        {
            if (ddlEntryType.SelectedItem.Text == "...")
            {
                ddlEntryType.BackColor = Color.Red;
                return;
            }
            else
            {

                if (ddlRoomType.SelectedItem.Text == "...")
                {
                    ddlRoomType.BackColor = Color.Red;
                }
            }
        }
        
        EstimateItem newRoom = new EstimateItem();
        newRoom.ItemType = "Room";
        newRoom.Entry = ddlEntryType.SelectedItem.Text;
        newRoom.EstimateID = currentEstimate.Id;
        newRoom.Quantity = int.Parse(txtVaultQty.Text);
        newRoom.Type = ddlRoomType.SelectedItem.Text;

        string roomie = newRoom.Quantity > 1 ? " rooms" : " room";

        newRoom.DisplayText = "(" + newRoom.Quantity.ToString() + ") "
            + newRoom.Entry + " entry " + newRoom.Type + roomie;

        newRoom.Save();
                
        lstVaults.DataSource = DAL.Estimate.GetRooms(currentEstimate.Id);
        lstVaults.DataTextField = "DisplayText";
        lstVaults.DataValueField = "Id";
        lstVaults.DataBind();

        ddlEntryType.SelectedValue = "...";
        ddlRoomType.SelectedValue = "...";
        txtVaultQty.Text = "";
    }

    protected void cmdAddDoor_Click(object sender, EventArgs e)
    {
        txtDoorQty.BackColor = Color.White;
        ddlSize.BackColor = Color.White;
        ddlStyle.BackColor = Color.White;

        // fake validation
        if (txtDoorQty.Text.Length < 1)
        {
            txtDoorQty.BackColor = Color.Red;
            return;
        }
        else
        {
            if (ddlSize.SelectedItem.Text == "...")
            {
                ddlSize.BackColor = Color.Red;
                return;
            }
            else
            {

                if (ddlStyle.SelectedItem.Text == "...")
                {
                    ddlStyle.BackColor = Color.Red;
                }
            }
        }

        EstimateItem newDoor = new EstimateItem();
        newDoor.ItemType = "Door";
        newDoor.EstimateID = currentEstimate.Id;
        newDoor.Quantity = int.Parse(txtDoorQty.Text);

        string doorage = newDoor.Quantity > 1 ? " doors" : " door";

        newDoor.Size = ddlSize.SelectedItem.Text;
        newDoor.Style = ddlStyle.SelectedItem.Text;
        newDoor.DisplayText = "(" + newDoor.Quantity.ToString() + ") " 
            + newDoor.Size + " " + newDoor.Style + doorage;

        newDoor.Save();

        lstDoors.DataSource = DAL.Estimate.GetDoors(currentEstimate.Id);
        lstDoors.DataTextField = "DisplayText";
        lstDoors.DataValueField = "Id";
        lstDoors.DataBind();

        ddlSize.SelectedValue = "...";
        ddlStyle.SelectedValue = "...";
        txtDoorQty.Text = "";
    }

    protected void UpdateEstimateTotal(object sender, EventArgs e)
    {
        ((TextBox)sender).Text = ((TextBox)sender).Text;
        decimal newTotal = decimal.Parse(txtDoorsTotal.Text) + decimal.Parse(txtInteriorsTotal.Text) + decimal.Parse(txtBunkerTotal.Text);

        txtEstimatetotal.Text = newTotal.ToString();
    }

    protected void ddlWorkflowStep_Change(object sender, EventArgs e)
    {
        //change responsible dropdown contents based on step selected
        PopulateResponsibleUserList(int.Parse(ddlWorkflowStep.SelectedItem.Value), "Add");
        txtAdditionalInformation.Enabled = (ddlWorkflowStep.SelectedItem.Value == "126" || ddlWorkflowStep.SelectedItem.Value == "132" ||
            ddlWorkflowStep.SelectedItem.Value == "133" ||ddlWorkflowStep.SelectedItem.Value == "134");
    }

    protected void cmdDelTimelineStep_Click(object sender, EventArgs e)
    {
        // received step cannot be deleted
        EstimateTimeline step = new EstimateTimeline(int.Parse(lstTimeline.SelectedItem.Value));
        if (step.StepX == 0)
            return;
        
        EstimateTimeline.Delete(int.Parse(lstTimeline.SelectedItem.Value));

        // Repopulate the estimate timeline
        lstTimeline.DataSource = Estimate.GetCurrentEstimateTimeline(currentEstimate.Id);
        lstTimeline.DataTextField = "DisplayText";
        lstTimeline.DataValueField = "Id";
        lstTimeline.DataBind();

        EstimateTimeline temp = EstimateTimeline.GetCurrentTimelineStep(currentEstimate.Id);

        if (step.WorkflowStepID == 123)
        {
            DateTime? nulDate = null;

            currentEstimate.EstimateSent = false;
            currentEstimate.EstimateSentDate = nulDate;
        }

        currentEstimate.StatusID = int.Parse(temp.StepX.ToString());
        currentEstimate.CurrentStatus = temp.DisplayText;
        lblCurrentStatus.Text = temp.DisplayText;

        currentEstimate.Save();

        // RE POPULATE ddlWorkflowStep with remaining steps for estimate
        ddlWorkflowStep.DataSource = Workflow.GetRemainingStepsForEstimate(currentEstimate.StatusID);
        ddlWorkflowStep.DataTextField = "Action";
        ddlWorkflowStep.DataValueField = "ID";
        ddlWorkflowStep.DataBind();
    }

    protected void lstEstimateTimeline_SelectedIndexChanged(object sender, EventArgs e)
    {
        EstimateTimeline step = new EstimateTimeline(int.Parse(lstTimeline.SelectedValue));
        Workflow wf = new Workflow(step.WorkflowStepID);
        
        // Populate editing fields with selected timeline step details
        lblStep.Text = wf.Action;
        PopulateResponsibleUserList(step.WorkflowStepID, "Edit");
        ddlResponsibleEdit.SelectedValue = step.ResponsibleUserID.ToString();
        txtDateTimeStampEdit.Text = step.DateTimeStamp.ToString("d");
        txtAdditionalInfoEdit.Text = step.Comments;        
    }
    
    #endregion

    #region DATA ACCESS

    public UserCollection GetUsersByTitle(string inTitle)
    {
        return DAL.User.GetUsersByTitle(inTitle);
    }

    public LookupCollection GetLookupList(string inListName)
    {
        return Lookup.GetLookupList(inListName);
    }

    public ProjectCollection GetActiveProjects()
    {
        return Project.GetProjectsByStatus(Project.ProjectStatus.Active);
    }

    public PhysicsBasiCollection GetPhysicsBasis()
    {
        return PhysicsBasi.GetPhysicsBasis();
    }

    public WorkflowCollection GetRemainingStepsForEstimate()
    {
        int currentStep = int.Parse(currentEstimate.StatusID.ToString());
        return Workflow.GetRemainingStepsForEstimate(currentStep);
    }

    #endregion

    #region HELPER METHODS

    private void SaveEstimate()
    {
        DateTime? nullDate = new DateTime();
        nullDate = null;

        // Prospect/Estimate Information        
        currentEstimate.BidDueDate = (txtBidDate.Text.Length > 0) ? DateTime.Parse(txtBidDate.Text) : nullDate;
        currentEstimate.Contact = txtClientContact.Text;
        currentEstimate.ContactEmail = txtContactEmail.Text;        
        currentEstimate.Estimator = int.Parse(ddlEstimator.SelectedValue);
        currentEstimate.EstimateDescription = txtEstDescription.Text;
        currentEstimate.InteriorsQty = Utils.ParseNullableInt(txtInteriors.Text);
        currentEstimate.SupplementalBlockCount = Utils.ParseNullableInt(txtSupplementalQty.Text);

        // update the hospital name if necessary
        currentProspect.HospitalClinicName = txtHospitalClinic.Text;
        
        // Financial Information
        currentEstimate.InteriorsTotal = decimal.Parse(txtInteriorsTotal.Text);
        currentEstimate.DoorsTotal = decimal.Parse(txtDoorsTotal.Text);
        currentEstimate.BunkerTotal = decimal.Parse(txtBunkerTotal.Text);
        currentEstimate.EstimateTotal = decimal.Parse(txtEstimatetotal.Text);

        // Scope Information
        currentEstimate.DesignBasis = txtDesignBasis.Text;
        currentEstimate.PhysicsBasis = txtPhysicsBasis.Text;
        currentEstimate.LaborType = ddlLaborType.SelectedItem.Text;
        currentEstimate.DoorScope = txtDoorScope.Text;
        currentEstimate.BunkerScope = txtBunkerScope.Text;
        currentEstimate.BunkerTitle = txtBunkerHeader.Text;
        currentEstimate.BunkerClarifications = this.txtBunkerClarifications.Text;
        currentEstimate.InteriorScope = txtInteriorScope.Text;
        currentEstimate.LaborType = (ddlLaborType.Text == "Select..." ? string.Empty : ddlLaborType.Text);

        // save updated objects back to the database
        currentProspect.Save();
        currentEstimate.Save();

        currentProspect = new Project(currentEstimate.JobName);
        currentSalesperson = new User(currentProspect.SalespersonID);
        currentEstimator = new User(currentEstimate.Estimator);
    }

    private void PopulateResponsibleUserList(int workflowStepID, string editAdd)
    {
        string title = Workflow.GetResponsibleJobTitleForWorkflowStep("EstimateTracking", workflowStepID);

        if (editAdd == "Add")
        {
            ddlResponsible.DataSource = DAL.User.GetUsersByTitle(title);
            ddlResponsible.DataTextField = "Name";
            ddlResponsible.DataValueField = "Id";
            ddlResponsible.DataBind();           
            
        }
        else
        {
            ddlResponsibleEdit.DataSource = DAL.User.GetUsersByTitle(title);
            ddlResponsibleEdit.DataTextField = "Name";
            ddlResponsibleEdit.DataValueField = "Id";
            ddlResponsibleEdit.DataBind();
        }
        
        
    }

    private void BindControls(Estimate.Filter inFilter)
    {
        if(inFilter == Estimate.Filter.Active)
        {
            lstEstimates.DataSource = Estimate.GetActive();
        }
        else
        {
            lstEstimates.DataSource = Estimate.GetAll();
        }
        lstEstimates.DataTextField = "DisplayText";
        lstEstimates.DataValueField = "EstId";
        lstEstimates.DataBind();
        
        
        ddlPhysicsBasis.DataBind();

        lstEstimates.Items[0].Selected = true;
        
        currentEstimate = new Estimate(lstEstimates.SelectedValue);
        currentProspect = new Project(currentEstimate.JobName);
        currentEstimator = new User(currentEstimate.Estimator);
        currentSalesperson = new User(currentProspect.SalespersonID);

        
        // Workflow step dropdown
        ddlWorkflowStep.DataSource = Workflow.GetRemainingStepsForEstimate(currentEstimate.StepID);
        ddlWorkflowStep.DataTextField = "Action";
        ddlWorkflowStep.DataValueField = "ID";
        ddlWorkflowStep.DataBind();

        // Current estimate Timeline listbox
        lstTimeline.DataSource = Estimate.GetCurrentEstimateTimeline(currentEstimate.Id);
        lstTimeline.DataTextField = "DisplayText";
        lstTimeline.DataValueField = "Id";
        lstTimeline.DataBind();
        lstTimeline.SelectedIndex = 0;


        // Estimator dropdown list
        ddlEstimator.DataSource = DAL.User.GetUsersByTitle("Estimator");
        ddlEstimator.DataTextField = "Name";
        ddlEstimator.DataValueField = "Id";
        ddlEstimator.DataBind();
                
        if(ddlWorkflowStep.SelectedItem != null) PopulateResponsibleUserList(int.Parse(ddlWorkflowStep.SelectedItem.Value), "Add");        
        
        // Room and Door Lists
        lstDoors.DataSource = DAL.Estimate.GetDoors(currentEstimate.Id);
        lstDoors.DataTextField = "DisplayText";
        lstDoors.DataValueField = "Id";
        lstDoors.DataBind();

        lstVaults.DataSource = DAL.Estimate.GetRooms(currentEstimate.Id);
        lstVaults.DataTextField = "DisplayText";
        lstVaults.DataValueField = "Id";
        lstVaults.DataBind();

        FillForm();
    }

    private void FillForm()
    {
        lblProspectHeading.Text = currentEstimate.EstimateNumber + " - " + currentEstimate.DisplayText + " - " + currentSalesperson.Name;

        DateTime? temp = currentEstimate.BidDueDate;
        string tempDate = "";
        if (temp != null)
            tempDate = DateTime.Parse(temp.ToString()).ToString("d");
        
        // Prospect/Estimate Information
        txtClientContact.Text = currentEstimate.Contact;
        txtContactEmail.Text = currentEstimate.ContactEmail;
        ddlEstimator.SelectedValue = currentEstimate.Estimator.ToString();        
        txtHospitalClinic.Text = currentProspect.HospitalClinicName;
        txtBidDate.Text = tempDate;
        txtEstDescription.Text = currentEstimate.EstimateDescription;
        txtInteriors.Text = currentEstimate.InteriorsQty.ToString();
        txtSupplementalQty.Text = currentEstimate.SupplementalBlockCount.ToString();
        
        // Estimate Dates
        // POPULATE listbox with current estimate timeline
        lstTimeline.DataSource = Estimate.GetCurrentEstimateTimeline(currentEstimate.Id);
        lstTimeline.DataTextField = "DisplayText";
        lstTimeline.DataValueField = "Id";
        lstTimeline.DataBind();

        // POPULATE ddlWorkflowStep with remaining steps for estimate
        ddlWorkflowStep.DataSource = Workflow.GetRemainingStepsForEstimate(currentEstimate.StepID);
        ddlWorkflowStep.DataTextField = "Action";
        ddlWorkflowStep.DataValueField = "ID";
        ddlWorkflowStep.DataBind();

        // POPULATE ddlResponsible with correct names for next step
        if(ddlWorkflowStep.Items.Count > 0)
            PopulateResponsibleUserList(int.Parse(ddlWorkflowStep.SelectedItem.Value), "Add");        
        lblCurrentStatus.Text = currentEstimate.CurrentStatus;
        
        // Scope and Financial Information
        ddlLaborType.SelectedValue = currentEstimate.LaborType == "" ? "Select..." : currentEstimate.LaborType;
        txtDesignBasis.Text = currentEstimate.DesignBasis;
        txtPhysicsBasis.Text = currentEstimate.PhysicsBasis;
        txtBunkerTotal.Text = Utils.NullSafeDecimal(currentEstimate.BunkerTotal).ToString("F2");
        txtBunkerHeader.Text = currentEstimate.BunkerTitle;
        txtBunkerScope.Text = currentEstimate.BunkerScope;
        txtInteriorsTotal.Text = Utils.NullSafeDecimal(currentEstimate.InteriorsTotal).ToString("F2");
        txtInteriorScope.Text = currentEstimate.InteriorScope;
        txtDoorsTotal.Text = Utils.NullSafeDecimal(currentEstimate.DoorsTotal).ToString("F2");
        txtDoorScope.Text = currentEstimate.DoorScope;
        txtBunkerClarifications.Text = currentEstimate.BunkerClarifications;
        txtEstimatetotal.Text = Utils.NullSafeDecimal(currentEstimate.EstimateTotal).ToString("F2");

        //Filesystem Information
        txtEstimatesDirectory.Text = currentEstimate.EstimatesDirectory;
        txtProspectDirectory.Text = currentEstimate.ProspectDirectory;
        hlExecDir.NavigateUrl = @"file:\\\" + currentEstimate.EstimatesDirectory;
        hlProspectDir.NavigateUrl = @"file:\\\" + currentEstimate.ProspectDirectory;
    }

    public Estimate CreateEstimate()
    {
        Estimate temp = new Estimate();
        DateTime? nullDate = null;

        temp.JobName = this.currentProspect.Id;
        temp.Estimator = DAL.User.GetUserFromLogin(Page.User.Identity.Name).Id;
        temp.EstimateNumber = Estimate.GetNextEstimateNumberForJob(currentProspect.Id);
        temp.Received = DateTime.Now;
        temp.Priority = Estimate.GetNextPriority();
        temp.ReadyForEstimating = true;
        temp.Received = DateTime.Now;
        temp.CurrentStatus = DateTime.Now.ToString("d") + " - Received by Estimating.";
        temp.EstimateSent = false;
        temp.EstimateSentDate = nullDate;
        temp.EstimatesDirectory = currentProspect.DirectoryPathExec;
        temp.ProspectDirectory = currentProspect.DirectoryPath;
                
        temp.Save();

        DAL.User estimator = new DAL.User(temp.Estimator);

        EstimateTimeline tempStep = new EstimateTimeline();
        tempStep.EstimateID = temp.Id;
        tempStep.WorkflowStepID = 131;
        tempStep.StepX = 0;
        tempStep.ResponsibleUserID = temp.Estimator;
        tempStep.DateTimeStamp = DateTime.Now;
        tempStep.DisplayText = DateTime.Now.ToString("d") + " - Received by Estimating.  Assigned to " + estimator.Name;

        tempStep.Save();
        
        CreateEstNumberDir(temp.EstimateNumber);

        BindControls(Estimate.Filter.Active);

        return temp;
    }

    private void CreateEstNumberDir(string inEstNumber)
    {
        string pathBaseProspect;
        string pathBaseExec;

        // assuming the current Prospect is correct for the new Estimate being created.
        if (Directory.Exists(currentProspect.DirectoryPath))
        {
            pathBaseProspect = currentProspect.DirectoryPath;
                       
            // create Physics
            Directory.CreateDirectory(pathBaseProspect + @"\Physics\" + inEstNumber);

            // create Engineering
            Directory.CreateDirectory(pathBaseProspect + @"\Engineering\Archdwgs\Archive\" + inEstNumber);

            // Create Sales
            Directory.CreateDirectory(pathBaseProspect + @"\Sales\Estimates\" + inEstNumber);
            Directory.CreateDirectory(pathBaseProspect + @"\Sales\Prospectus\" + inEstNumber);
            Directory.CreateDirectory(pathBaseProspect + @"\Sales\SketchUp\" + inEstNumber);

        }

        if (Directory.Exists(currentProspect.DirectoryPathExec))
        {
            pathBaseExec = currentProspect.DirectoryPathExec;

            Directory.CreateDirectory(pathBaseExec + @"\" + inEstNumber);
                        
        }
    }

    private void WorkflowStep(Workflow currentStep)
    {
        // Future use for workflow actions to be run when processing this Workflow step
                
        // Estimate Sent or Dropped
        if (currentStep.TerminalStep)
        {
            DateTime stamp = DateTime.Now;
            currentEstimate.EstimateSent = true;

            // if sent stamp the date into EstimateSentDate field, no date is used if dropped to keep dropped estimates out of reports.
            if(currentStep.Id == 123)
                currentEstimate.EstimateSentDate = stamp;

            Estimate.RemovePriorityFromClosedEstimates();
            Estimate.UpdateEstimatePriority();
        }

        // Step = Put on Hold.
        // Remove Priority
        if (currentStep.Id == 133)
        {
            Estimate.SetPriorityToZero(currentEstimate.Id);
        }
        
    }

    private void WorkflowCustomStep(Workflow activity)
    {
        switch (activity.ActionParameter)
        {
            case "RenumberEstimatePriority":
                Estimate.UpdateEstimatePriority();
                break;

            default:
                break;
        }
    }

    private void WorkflowFieldValueChange(Workflow activity, string inDirection)
    {
        // get table to be changed

        // get field to be changed

        // get value to change field to
        if (inDirection == "Forward")
        {

        }
        else
        {

        }
    }       

    private void WorkflowStatusUpdate(Workflow activity)
    {
        throw new NotImplementedException();
    }

    private string SetEstimateStatus(Workflow activity, DateTime stamp, string addEdit)
    {
        string status = activity.Status;
        status = status.Replace("<<SALESPERSON>>", currentSalesperson.Name);

        string name = addEdit == "Add" ? ddlResponsible.SelectedItem.Text : ddlResponsibleEdit.SelectedItem.Text;

        status = status.Replace("<<DRAFTSMAN>>", name);
        status = status.Replace("<<ESTIMATOR>>", name);
        status = status.Replace("<<PHYSICIST>>", name);

        return stamp.ToString("d") + " - " + status;        
    }

    private void WorkflowCreateDir(Workflow activity)
    {
        // do it
    }

    private void WorkflowSendMail(Workflow activity)
    {

        // TODO: NEED TO CREATE DATA STRUCTURE FOR WORKFLOW STEPS WITH MULTIPLE ACTIONS (CHILD RECORDS)

        //// create message
        //MailMessage msg = new MailMessage();

        //// do replacements on message
        //string bodyText = MailMessageReplacements(activity.Message);

        //// set to and from addresses
        //msg.From = new MailAddress("michael.devenney@veritas-medicalsolutions.com");
        //msg.To.Add(new MailAddress(activity.ActionParameter));
        //msg.CC.Add(new MailAddress("michael.devenney@veritas-medicalsolutions.com"));
        //msg.Subject = activity.Application + " - " + currentProspect.ProjectName;
        //msg.Body = bodyText;

        //// send mail
        //SmtpClient client = new SmtpClient("ver-sbs-01");
        //client.Send(msg);
    }

    private string MailMessageReplacements(string inBodyText)
    {
        inBodyText = inBodyText.Replace("<LEAD>", currentProspect.DisplayName);
        inBodyText = inBodyText.Replace("<<ProspectRefDwgs>>", currentEstimate.ProspectDirectory + @"Engineering\RefDwgs");
        inBodyText = inBodyText.Replace("<BR>", "\r\n");
        inBodyText = inBodyText.Replace("<T>", "\t");
        inBodyText = inBodyText.Replace("<CONTACT>", currentEstimate.Contact);
        inBodyText = inBodyText.Replace("<HOSPITALNAME>", currentProspect.HospitalClinicName);
        inBodyText = inBodyText.Replace("<ESTIMATETOTAL>", Utils.NullSafeDecimal(currentEstimate.EstimateTotal).ToString("C2"));
        inBodyText = inBodyText.Replace("<DOORTOTAL>", Utils.NullSafeDecimal(currentEstimate.DoorsTotal).ToString("C2"));
        inBodyText = inBodyText.Replace("<DOORSCOPE>", currentEstimate.DoorScope);
        inBodyText = inBodyText.Replace("<INTERIORTOTAL>", Utils.NullSafeDecimal(currentEstimate.InteriorsTotal).ToString("C2"));
        inBodyText = inBodyText.Replace("<INTERIORSCOPE>", currentEstimate.InteriorScope);
        inBodyText = inBodyText.Replace("<BUNKERTOTAL>", Utils.NullSafeDecimal(currentEstimate.BunkerTotal).ToString("C2"));
        inBodyText = inBodyText.Replace("<BUNKERSCOPE>", currentEstimate.BunkerScope);
        inBodyText = inBodyText.Replace("<ESTNUM>", currentEstimate.EstimateNumber);        
        
        return inBodyText;
    }

    private static bool customXertificateValidation(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
    {
        return true;
    }

    #endregion
}