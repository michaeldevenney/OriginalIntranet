using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Shared;

public partial class apps_projects_Default : System.Web.UI.Page
{
    Project currProj;
    User currentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            Label lblHeading;
            lblHeading = (Label)Master.FindControl("lblPageHeading");
            lblHeading.Text = "Veritas - Project Management - Projects";

            ddlProject.DataSource = Project.GetProjectsByStatus(Project.ProjectStatus.Active); 
            ddlProject.DataValueField = "ID";
            ddlProject.DataTextField = "DisplayName";

            UpdateLabels("Project");

            Page.DataBind();

            ddlProject.Items[0].Selected = true;
            LoadForm(int.Parse(ddlProject.Items[0].Value));

            BindRegionsDropdowns();
        }
        else
        {
            if (Session["ProjID"] != null)
            {
                currProj = new Project(Session["ProjID"]);
                //LoadForm(int.Parse(Session["ProjID"].ToString()));
            }
        }

        currentUser = DAL.User.GetUserFromLogin(Page.User.Identity.Name);
    }

    private void UpdateLabels(string inLeadProject)
    {
        if (inLeadProject == "Lead")
        {
            lblProjectLead.Text = "Select " + inLeadProject + ": ";
            lblNumber.Text = inLeadProject + " Number: ";

            //Todo: Need all the labels to be updated.  Just a nice thing to do Mike.
        }
        else
        {

        }

    }
       

    #region CONTROL EVENTS
    
    protected void cmdAddProjLead_Click(object sender, EventArgs e)
    {
        CreateProject();        
    }

    protected void cmdDeleteLeadProj_Click(object sender, EventArgs e)
    {
        DeleteProject();
    }

    protected void cmdSaveLeadProj_Click(object sender, EventArgs e)
    {
        SaveProject();
    }

    protected void rdoActiveAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterLeadProjectDDL();
    }

    protected void rdoLeadProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterLeadProjectDDL();
    }

    protected void cmdSave_Clicked(object sender, EventArgs e)
    {
        SaveProject();
    }
    
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = int.Parse(ddlProject.SelectedItem.Value.ToString());
        LoadForm(id);
    }

    #endregion

    #region HELPER METHODS
 
    private void SendEmail(Project currentProject, string action)
    {
        //// move address list into web.config
        //MailAddress addr = new MailAddress("michael.devenney@veritas-medicalsolutions.com");
        //MailAddress addr1 = new MailAddress("john.brooks@veritas-medicalsolutions.com");
        //MailMessage msg = new MailMessage();
        
        //msg.To.Add(addr);
        //msg.To.Add(addr1);
        //msg.From = new MailAddress("ProjectNotification@veritas-medicalsolutions.com");

        //msg.Subject = "Project " + action;
        //msg.Body = currentUser.Name + " " + action + " the " + currentProject.DisplayName + " project on " + DateTime.Now.ToString("f");

        //SmtpClient server = new SmtpClient("ver-sbs-01");
        //server.Send(msg);
    }

    private void FilterLeadProjectDDL()
    {
        if (rdoLeadProject.SelectedValue == "Projects")
        {
            if (rdoActiveAll.SelectedValue == "Active")
            {
                ddlProject.DataSource = Project.GetProjectsByStatus(Project.ProjectStatus.Active);
            }
            else
            {
                ddlProject.DataSource = Project.GetAllProjects();
            }

            ddlProject.DataValueField = "ID";
            ddlProject.DataTextField = "DisplayName";
            lblProjectLead.Text = "Select Project: ";
        }
        else
        {
            if (rdoActiveAll.SelectedValue == "Active")
            {
                ddlProject.DataSource = Project.GetLeadsByStatus(Project.ProjectStatus.Active);
            }
            else
            {
                ddlProject.DataSource = Project.GetAllLeads();
            }

            
            ddlProject.DataValueField = "ID";
            ddlProject.DataTextField = "DisplayName";
            lblProjectLead.Text = "Select Lead: ";
        }
               
        
        ddlProject.DataBind();

        Session["ProjID"] = (ddlProject.Items.Count > 0 ? ddlProject.SelectedItem.Value : 0.ToString());
        
        if (ddlProject.Items.Count > 0)
        {
            currProj = new Project(Session["ProjID"]);
            LoadForm(int.Parse(Session["ProjID"].ToString()));
        }
    }

    private void LoadForm(int id)
    {
        currProj = new Project(id);
        Session["ProjID"] = id.ToString();

        //todo: format these dates better
        string createdDate = currProj.Created.ToString(); //(proj.Created != null ? DateTime.MinValue : proj.Created);
        string updatedDate = currProj.Updated.ToString();

        string status = "Record created on " + createdDate + " by " + currProj.CreatedBy + ".\r\nLast updated on " +
            updatedDate + " by " + currProj.UpdatedBy;

        ddlDraftsman.SelectedValue = currProj.DesignDraftsmanID.ToString();
        txtProjectName.Text = currProj.ProjectName;
        txtProjectNumber.Text = currProj.ProjectNumber;
        txtHospitalClinic.Text = currProj.HospitalClinicName;
        ddlAssignedPM.SelectedValue = currProj.PMUserID.ToString();
        ddlProjectActivity.SelectedValue = currProj.ProjectActivity;
        ddlEngineeringConsultant.SelectedValue = currProj.EngineeringConsultant;
        lblRecordStatus.Text = status;
        ddlSalesPerson.SelectedValue = currProj.SalespersonID.ToString();
        ddlProspectRegion.SelectedValue = currProj.Region;
        ddlPhysicist.SelectedValue = currProj.PhysicistID.ToString();

    }

    private void SaveProject()
    {
        currProj.DesignDraftsmanID = int.Parse(ddlDraftsman.SelectedItem.Value);
        currProj.ProjectName = txtProjectName.Text;
        currProj.ProjectNumber = txtProjectNumber.Text;
        currProj.PMUserID = int.Parse(ddlAssignedPM.SelectedItem.Value);
        currProj.PMAssigned = ddlAssignedPM.SelectedItem.Text;
        currProj.ProjectActivity = ddlProjectActivity.SelectedItem.Text;
        currProj.Updated = DateTime.Now;
        currProj.UpdatedBy = Utils.GetFormattedUserNameInternal(User.Identity.Name);
        currProj.HospitalClinicName = txtHospitalClinic.Text;
        currProj.EngineeringConsultant = ddlEngineeringConsultant.SelectedItem.Value;
        currProj.SalespersonID = int.Parse(ddlSalesPerson.SelectedItem.Value);
        currProj.Region = ddlProspectRegion.SelectedItem.Value;
        currProj.PhysicistID = int.Parse(ddlPhysicist.SelectedItem.Value);

        currProj.Save();

        //reload record status         
        string createdDate = currProj.Created.ToString(); 
        string updatedDate = currProj.Updated.ToString();

        string status = "Record created on " + createdDate + " by " + currProj.CreatedBy + ".\r\nLast updated on " +
            updatedDate + " by " + currProj.UpdatedBy;

        lblRecordStatus.Text = status;

        User currentUser = DAL.User.GetUserFromLogin(Page.User.Identity.Name);

        ClearFields();

        //SendEmail(currProj, "Updated");
    }

    private void ClearFields()
    {
        
    }

    private void CreateProject()
    {
        Project proj = new Project();

        proj.ProjectLead = ddlProjectLead.SelectedItem.Text;
        proj.Location = txtAddProjLocation.Text;
        proj.ProjectName = txtAddProjName.Text;
        proj.ProjectNumber = txtAddProjNumber.Text;
        proj.PMUserID = int.Parse(ddlAddProjMgr.SelectedItem.Value);
        proj.PMAssigned = ddlAddProjMgr.SelectedItem.Text;     
        proj.ProjectActivity = ddlAddProjActivity.SelectedItem.Text;
        proj.Created = DateTime.Now;
        proj.CreatedBy = Utils.GetFormattedUserNameInternal(User.Identity.Name);
        proj.Updated = DateTime.Now;
        proj.UpdatedBy = Utils.GetFormattedUserNameInternal(User.Identity.Name);
        proj.Region = ddlRegionNew.SelectedItem.Text;

        proj.Save();

        Session["ProjID"] = proj.Id.ToString();

        if (ddlProjectLead.SelectedItem.Text == "Project")
        {
            ddlProject.DataSource = Project.GetAllProjects();
            lblProjectLead.Text = "Select Project: ";
        }
        else
        {
            ddlProject.DataSource = Project.GetAllLeads();
            lblProjectLead.Text = "Select Lead: ";
        }

        ddlProject.DataValueField = "ID";
        ddlProject.DataTextField = "DisplayName";
        ddlProject.DataBind();
        ddlProject.SelectedValue = proj.Id.ToString();
        LoadForm(proj.Id);

        SendEmail(proj, "Created");
    }

    private void DeleteProject()
    {        
        Project currentProject = Project.GetProjectByID(int.Parse(ddlProject.SelectedItem.Value));   
        Project.Delete(ddlProject.SelectedItem.Value);

        SendEmail(currentProject, "Deleted");

        FilterLeadProjectDDL();
    }

    private void BindRegionsDropdowns()
    {
        var regions = Directory.GetDirectories(Paths.Prospects, "*", SearchOption.TopDirectoryOnly);

        for (int i = regions.Length - 1; i > -1; i--)
        {
            regions[i] = regions[i].Substring(42);
        }
        ddlProspectRegion.DataSource = regions;
        ddlProspectRegion.DataBind();

        ddlRegionNew.DataSource = regions;
        ddlRegionNew.DataBind();
    }

    #endregion
}