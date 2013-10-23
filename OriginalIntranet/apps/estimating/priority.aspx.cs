using System.Net.Mail;
using sites;
using System;
using Shared;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using DAL;
using System.Configuration;
using System.Net;

public partial class apps_estimating_priority : System.Web.UI.Page
{    
    public Project currentProspect;
    public User currentSalesperson;
    public User currentEstimator;
    public EstimateCollection estimateQueue;
    public EstimatesSalesQueueCollection salesQueue;
    public Estimate activeSalesQueueItem;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lblHeading;
        lblHeading = (Label)Master.FindControl("lblPageHeading");
        lblHeading.Text = "Veritas - Estimate Priority";

        if (!IsPostBack)
        {
            BindControls();
            cmdSubmitEst.Enabled = activeSalesQueueItem.BidDueDate != null;
        }
        else
        {
            estimateQueue = (EstimateCollection)Cache["Estimates"];
            activeSalesQueueItem = (Estimate)Cache["ActiveEstimate"];
        }
    }

    protected void cmdSubmitEst_Clicked(object sender, EventArgs e)
    {
        currentProspect = new DAL.Project(activeSalesQueueItem.JobName);
        currentEstimator = new DAL.User(activeSalesQueueItem.Estimator);
        currentSalesperson = new DAL.User(currentProspect.SalespersonID);

        activeSalesQueueItem.ReadyForEstimating = true;
        activeSalesQueueItem.StepID = 131;
        activeSalesQueueItem.CurrentStatus = DateTime.Now.ToString("d") + " - Received by Estimating.  Assigned to " + currentEstimator.Name;
        activeSalesQueueItem.Priority = Estimate.GetNextPriority();
        activeSalesQueueItem.Save();

        EstimateTimeline newStep = new EstimateTimeline();
        newStep.EstimateID = activeSalesQueueItem.Id;
        newStep.DateTimeStamp = DateTime.Now;
        newStep.DisplayText = activeSalesQueueItem.CurrentStatus;
        newStep.ResponsibleUserID = activeSalesQueueItem.Estimator;
        newStep.StepX = 0;
        newStep.WorkflowStepID = 131;
        newStep.Save();

        EmailSalesperson();
        BindControls();
    }

    protected void cmdDelSalesItem_Click(object sender, EventArgs e)
    {
        int prospectID = int.Parse(lstSalesQueue.SelectedItem.Value);
        Estimate.Delete(prospectID);
        BindControls();
    }

    protected void cmdAddProjLead_Click(object sender, EventArgs e)
    {
        CreateProject();
        ClearNewFields();
        BindControls();
    }

    protected void lstSalesQueue_SelectedIndexChanged(object sender, EventArgs e)
    {
        activeSalesQueueItem = new Estimate(lstSalesQueue.SelectedValue);
        Cache["ActiveEstimate"] = activeSalesQueueItem;

        rdoDrawings.SelectedValue = Utils.NullableBoolToRadioButton(activeSalesQueueItem.Drawings);
        rdoProspectus.SelectedValue = Utils.NullableBoolToRadioButton(activeSalesQueueItem.Prospectus);
        rdoTxParams.SelectedValue = Utils.NullableBoolToRadioButton(activeSalesQueueItem.TxParameters);
        txtEstDueDate.Text = activeSalesQueueItem.BidDueDate.ToString();
        
        cmdSubmitEst.Enabled = activeSalesQueueItem.BidDueDate != null;

        currentProspect = Project.GetLeadByID(activeSalesQueueItem.JobName);
        currentSalesperson = new User(currentProspect.SalespersonID);
        currentEstimator = new User(activeSalesQueueItem.Estimator);
    }

    protected void lstEstimates_Reorder(object sender, ReorderListItemReorderEventArgs e)
    {
        Estimate moved = estimateQueue[e.OldIndex];
        estimateQueue.RemoveAt(e.OldIndex);
        estimateQueue.Insert(e.NewIndex, moved);

        int i = 1;
        foreach (Estimate est in estimateQueue)
        {
            est.Priority = i++;
            est.Save();
        }

        Cache["Estimates"] = estimateQueue;
    }

    #region HELPER METHODS

    private void EmailSalesperson()
    {        
        MailMessage msg = new MailMessage();
        msg.CC.Add(new MailAddress("bill.bushnell@Veritas-medicalsolutions.com"));
        msg.Bcc.Add(new MailAddress("mikedevenney@verizon.net"));
        msg.To.Add(new MailAddress(currentSalesperson.Email));
        msg.To.Add(new MailAddress("michael.devenney@veritas-medicalsolutions.com"));
        msg.From = new MailAddress("prospectus@veritas-medicalsolutions.com");
        msg.Subject = "Estimate number for new prospect";

        string inits = currentSalesperson.Initials;

        msg.Body = currentSalesperson.FirstName + ",\r\n\nYour lead: " + activeSalesQueueItem.DisplayText + 
            " has been submitted to the estimating department and assigned estimate number: " + 
            activeSalesQueueItem.EstimateNumber + ".  " + currentEstimator.Name + " will be preparing the estimate.";

        SmtpClient smtp = new SmtpClient("ver-sbs-01");
        smtp.Credentials = new NetworkCredential("prospectus", "BringInTheSales1", "Veritas");
        smtp.Send(msg);
    }

    private void ClearNewFields()
    {
        txtAddProjName.Text = "";
        txtAddProjNumber.Text = "";
        ddlAddProjMgr.ClearSelection();
        ddlSalesperson.ClearSelection();
        ddlProspectRegion.ClearSelection();
        chkCreateExec.Checked = true;
        chkCreateOutlook.Checked = true;
        chkCreateProspect.Checked = true;
    }

    private void CreateProject()
    {
        Project proj = new Project();

        proj.ProjectLead = "Lead";

        proj.ProjectName = txtAddProjName.Text;
        proj.ProjectNumber = txtAddProjNumber.Text;
        proj.PMUserID = int.Parse(ddlAddProjMgr.SelectedItem.Value);
        proj.PMAssigned = ddlAddProjMgr.SelectedItem.Text;
        proj.Region = ddlProspectRegion.SelectedItem.Text;
        proj.SalespersonID = int.Parse(ddlSalesperson.SelectedItem.Value);
        proj.ProjectActivity = "Active";
        proj.Created = DateTime.Now;
        proj.CreatedBy = Utils.GetFormattedUserNameInternal(User.Identity.Name);
        proj.Updated = DateTime.Now;
        proj.UpdatedBy = Utils.GetFormattedUserNameInternal(User.Identity.Name);
        proj.DesignDraftsmanID = 128;
        proj.EngineeringConsultant = "Unassigned";
        proj.PhysicistID = 137;
        
        if (chkCreateExec.Checked)
            proj.DirectoryPathExec = CreateDir(Estimate.DirType.Exec);
        if (chkCreateProspect.Checked)
            proj.DirectoryPath = CreateDir(Estimate.DirType.Prospect);
        if (chkCreateOutlook.Checked)
            CreateOutlookFolder(proj.ProjectNumber + " - " + proj.ProjectName, "Not Sent");

        proj.Save();
        currentProspect = proj;

        Estimate temp = new Estimate();
        temp.JobName = proj.Id;
        var giggles = DAL.User.GetUserFromLogin(Page.User.Identity.Name);
        
        if(giggles != null)
            temp.Estimator = DAL.User.GetUserFromLogin(Page.User.Identity.Name).Id;
        
        temp.EstimateNumber = Estimate.GetNextEstimateNumberForJob(proj.Id);
        temp.Received = DateTime.Now;
        temp.Priority = 0;
        temp.CurrentStatus = DateTime.Now.ToString("d") + " - Added to Sales Queue.";
        temp.StepID = 135;
        temp.StatusID = 0;

        temp.Save();

        activeSalesQueueItem = temp;

        if (chkCreateExec.Checked)
        {
            temp.EstimatesDirectory = CreateDir(Estimate.DirType.EstimateNumber);
        }

        if (chkCreateProspect.Checked)
            temp.ProspectDirectory = proj.DirectoryPath;

        

        temp.Save();
        activeSalesQueueItem = temp;
    }

    public string copyDirectory(string sourceDir, string destDir)
    {
        String[] Files;
        DirectoryInfo newDir;
        string returnValue = string.Empty;

        if (destDir[destDir.Length - 1] != Path.DirectorySeparatorChar)
            destDir += Path.DirectorySeparatorChar;

        if (!Directory.Exists(destDir))
        {
            newDir = Directory.CreateDirectory(destDir);
            returnValue = newDir.FullName;
        }

        Files = Directory.GetFileSystemEntries(sourceDir);

        foreach (string Element in Files)
        {
            if (Directory.Exists(Element))
                copyDirectory(Element, destDir + Path.GetFileName(Element));
            else
                File.Copy(Element, destDir + Path.GetFileName(Element), true);
        }

        return returnValue;
    }

    private string CreateDir(Estimate.DirType dirType)
    {
        if (dirType == Estimate.DirType.Exec)
        {
            string region = ddlProspectRegion.SelectedItem.Text;
            string project = txtAddProjNumber.Text;
            string path = "";

            if (region == "United States")
            {
                path = Utils.BetweenAandM(project.Substring(0, 1)) ? 
                    Paths.Exec + region + @"\A-M\" + txtAddProjNumber.Text + " - " + txtAddProjName.Text :
                    Paths.Exec + region + @"\N-Z\" + txtAddProjNumber.Text + " - " + txtAddProjName.Text;
            }
            else
            {
                path = Paths.Exec + region + @"\" + txtAddProjNumber.Text + " - " + txtAddProjName.Text;
            }


            DirectoryInfo execDir = Directory.CreateDirectory(path);
            return execDir.FullName;
        }

        if (dirType == Estimate.DirType.Prospect) {
            string folderTemplatePath = ConfigurationManager.AppSettings["ProspectFolderTemplate"];
            string newDir = copyDirectory(folderTemplatePath, Paths.Prospects
                + ddlProspectRegion.SelectedItem.Text + @"\" + txtAddProjNumber.Text + " - " + txtAddProjName.Text);
            return newDir;
        }

        if (dirType == Estimate.DirType.EstimateNumber)
        {
            string pathBaseProspect;
            string pathBaseExec;

            // assuming the current Prospect is correct for the new Estimate being created.
            if (Directory.Exists(currentProspect.DirectoryPath))
            {
                pathBaseProspect = currentProspect.DirectoryPath;


                // create Physics
                Directory.CreateDirectory(pathBaseProspect + @"\Physics\" + activeSalesQueueItem.EstimateNumber);

                // create Engineering
                Directory.CreateDirectory(pathBaseProspect + @"\Engineering\Archdwgs\Archive\" + activeSalesQueueItem.EstimateNumber);

                // Create Sales
                Directory.CreateDirectory(pathBaseProspect + @"\Sales\Estimates\" + activeSalesQueueItem.EstimateNumber);
                Directory.CreateDirectory(pathBaseProspect + @"\Sales\Prospectus\" + activeSalesQueueItem.EstimateNumber);
                Directory.CreateDirectory(pathBaseProspect + @"\Sales\SketchUp\" + activeSalesQueueItem.EstimateNumber);

            }

            if (Directory.Exists(currentProspect.DirectoryPathExec))
            {
                pathBaseExec = currentProspect.DirectoryPathExec;

                Directory.CreateDirectory(pathBaseExec + @"\" + activeSalesQueueItem.EstimateNumber);

            }

        }

        return string.Empty;
    }

    protected void rdoBttnLists_Click(object sender, EventArgs e)
    {
        activeSalesQueueItem.TxParameters = Utils.NullSafeBool(rdoTxParams.SelectedValue);
        activeSalesQueueItem.Prospectus = Utils.NullSafeBool(rdoProspectus.SelectedValue);
        activeSalesQueueItem.Drawings = Utils.NullSafeBool(rdoDrawings.SelectedValue);
        activeSalesQueueItem.BidDueDate = Utils.ParseNullableDateTime(txtEstDueDate.Text);

        cmdSubmitEst.Enabled = (activeSalesQueueItem.BidDueDate != null);

        activeSalesQueueItem.Save();
    }

    private void BindControls()
    {
        // Bind Estimate Queue
        estimateQueue = Estimate.GetEditableActiveEstimates();
        lstEstimates.DataSource = estimateQueue;
        lstEstimates.DataBind();
        
        //Sales Queue
        salesQueue = Estimate.GetSalesQueue();
        lstSalesQueue.DataSource = salesQueue;
        lstSalesQueue.DataTextField = "DisplayText";
        lstSalesQueue.DataValueField = "EstId";
        lstSalesQueue.DataBind();
        
        lstSalesQueue.Items[0].Selected = true;
        activeSalesQueueItem = new Estimate(lstSalesQueue.SelectedValue);
                
        string[] regions = Directory.GetDirectories(Paths.Prospects, "*", SearchOption.TopDirectoryOnly);

        for (int i = regions.Length - 1; i > -1; i--)
        {
            var fullPath = regions[i];
            var region = fullPath.Substring(fullPath.LastIndexOf('\\') + 1);

             regions[i] = region;
        }
        ddlProspectRegion.DataSource = regions;
        ddlProspectRegion.DataBind();

        // Update Cache
        Cache["SalesQueue"] = salesQueue;
        Cache["ActiveEstimate"] = activeSalesQueueItem;
        Cache["Estimates"] = estimateQueue;
        Cache["Regions"] = regions;

        ddlAddProjMgr.DataBind();
        ddlSalesperson.DataBind();
    }

    #endregion

    #region EXCHANGE WEB SERVICE METHODS

    static ExchangeServiceBinding CreateESB()
    {
        // Define the service binding and the user account to use.
        ExchangeServiceBinding esb = new ExchangeServiceBinding();
        esb.RequestServerVersionValue = new RequestServerVersion();
        esb.RequestServerVersionValue.Version = ExchangeVersionType.Exchange2007_SP1;

        // use credentials of currently logged in user
        esb.UseDefaultCredentials = true;
        //esb.Credentials = new NetworkCredential("michael.devenney", "Sigmaphi2", "Veritas");
        esb.Url = @"https://sites/EWS/Exchange.asmx";

        return esb;
    }

    private void CreateOutlookFolder(string newFolderName, string destinationFolder)
    {
        ExchangeServiceBinding esb = CreateESB();

        DistinguishedFolderIdType myInbox = new DistinguishedFolderIdType();
        myInbox.Id = DistinguishedFolderIdNameType.inbox;

        FolderIdType estFolder = FindFolder(esb, myInbox, destinationFolder);

        CreateFolder(esb, estFolder, newFolderName);
    }

    static void MoveOutlookFolder(string folderToMove, string destinationFolderName)
    {
        ExchangeServiceBinding esb = CreateESB();
        DistinguishedFolderIdType myInbox = new DistinguishedFolderIdType();
        myInbox.Id = DistinguishedFolderIdNameType.inbox;

        // get source folder info
        FolderIdType sourceFolderID = FindFolder(esb, myInbox, folderToMove);
        // get target folder info
        FolderIdType destinationFolderID = FindFolder(esb, myInbox, destinationFolderName);

        // call move method
        MoveFolder(esb, sourceFolderID, destinationFolderID);
    }

    static FolderIdType FindFolder(ExchangeServiceBinding esb, DistinguishedFolderIdType fiFolderID, String fnFldName)
    {

        FolderIdType rvFolderID = new FolderIdType();

        // Create the request and specify the travesal type
        FindFolderType findFolderRequest = new FindFolderType();
        findFolderRequest.Traversal = FolderQueryTraversalType.Deep;

        // Define the properties returned in the response
        FolderResponseShapeType responseShape = new FolderResponseShapeType();
        responseShape.BaseShape = DefaultShapeNamesType.Default;
        findFolderRequest.FolderShape = responseShape;

        // Identify which folders to search
        DistinguishedFolderIdType[] folderIDArray = new DistinguishedFolderIdType[1];
        folderIDArray[0] = new DistinguishedFolderIdType();
        folderIDArray[0].Id = fiFolderID.Id;

        //Add Restriction for DisplayName
        RestrictionType ffRestriction = new RestrictionType();
        IsEqualToType ieToType = new IsEqualToType();
        PathToUnindexedFieldType diDisplayName = new PathToUnindexedFieldType();
        diDisplayName.FieldURI = UnindexedFieldURIType.folderDisplayName;
        FieldURIOrConstantType ciConstantType = new FieldURIOrConstantType();
        ConstantValueType cvConstantValueType = new ConstantValueType();
        cvConstantValueType.Value = fnFldName;
        ciConstantType.Item = cvConstantValueType;
        ieToType.Item = diDisplayName;
        ieToType.FieldURIOrConstant = ciConstantType;
        ffRestriction.Item = ieToType;
        findFolderRequest.Restriction = ffRestriction;

        // Add the folders to search to the request
        findFolderRequest.ParentFolderIds = folderIDArray;

        try
        {
            // Send the request and get the response
            FindFolderResponseType findFolderResponse = esb.FindFolder(findFolderRequest);

            // Get the response messages
            ResponseMessageType[] rmta = findFolderResponse.ResponseMessages.Items;

            foreach (ResponseMessageType rmt in rmta)
            {
                // Cast to the correct response message type
                FindFolderResponseMessageType ffResponse = (FindFolderResponseMessageType)rmt;

                foreach (FolderType fFoundFolder in ffResponse.RootFolder.Folders)
                {
                    rvFolderID = fFoundFolder.FolderId;
                }
            }
        }
        catch (Exception e)
        {
            string problem = e.Message;
        }

        return rvFolderID;
    }

    static void CreateFolder(ExchangeServiceBinding esb, FolderIdType fiFolderID, String fnFldName)
    {

        // Create the request
        FolderType folder = new FolderType();
        folder.DisplayName = fnFldName;

        TargetFolderIdType targetID = new TargetFolderIdType();
        targetID.Item = fiFolderID;

        CreateFolderType createFolder = new CreateFolderType();
        createFolder.Folders = new FolderType[] { folder };
        createFolder.ParentFolderId = targetID;

        try
        {
            // Send the request and get the response
            CreateFolderResponseType response = esb.CreateFolder(createFolder);

            // Get the response messages
            ResponseMessageType[] rmta = response.ResponseMessages.Items;

        }
        catch (Exception e)
        {
            string problem = e.Message;
        }
    }

    static void MoveFolder(ExchangeServiceBinding esb, FolderIdType sourceFolderID, FolderIdType destinationFolderID)
    {
        MoveFolderType moveFolder = new MoveFolderType();
        moveFolder.FolderIds = new FolderIdType[] { sourceFolderID };
        TargetFolderIdType targetID = new TargetFolderIdType();
        targetID.Item = destinationFolderID;
        moveFolder.ToFolderId = targetID;

        try
        {
            // Send the request and get the response
            MoveFolderResponseType response = esb.MoveFolder(moveFolder);

            // Get the response messages
            ResponseMessageType[] rmta = response.ResponseMessages.Items;

        }
        catch (Exception e)
        {
            string problem = e.Message;
        }

    }


    #endregion
}