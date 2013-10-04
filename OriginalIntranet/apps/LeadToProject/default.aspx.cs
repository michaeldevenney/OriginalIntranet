using System.Net;
using System.Net.Mail;
using System.IO;
using Shared;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class apps_LeadToProject_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {

        }
        else
        {
            txtProjectNumber.Text = Project.GetNextProjectNumber();

            //data bind controls
            lstProspects.DataSource = Project.AllLeadsWithEstimate();
            lstProspects.DataTextField = "DisplayName";
            lstProspects.DataValueField = "ID";
            lstProspects.DataBind();

            rptProjects.DataSource = Project.GetProjectsByStatus(Project.ProjectStatus.Active);
            rptProjects.DataBind();

            // dropdown lists 
            ddlEng.DataBind();
            ddlPM.DataBind();
            ddlPhysicist.DataBind();
            ddlEngConsultant.DataBind();

            chkUsers.DataBind();
        }
    }

    protected void cmdGo_Click(object sender, EventArgs e)
    {       

        int prospectID = int.Parse(lstProspects.SelectedItem.Value);
        Project prospect = new Project(prospectID);
        string projectsDirectory = Paths.Projects;
        string execDirectory = Paths.Exec;

        // Build new project name = new project number + current prospect number + project name 
        string newName = prospect.ProjectNumber + " - " + prospect.ProjectName;
        prospect.ProjectName = newName;
        prospect.ProjectNumber = txtProjectNumber.Text;

        newName = prospect.ProjectNumber + " " + prospect.ProjectName;

        // Change ProjectLead field value from Lead to Project
        prospect.ProjectLead = "Project";

        // Save selected estimate ID into project record
        prospect.AcceptedEstimateID = int.Parse(lstJobEstimates.SelectedItem.Value);

        // Save assigned resources into project record
        prospect.PMUserID = int.Parse(ddlPM.SelectedItem.Value);
        prospect.EngineeringConsultant = ddlEngConsultant.SelectedValue;
        prospect.DesignDraftsmanID = int.Parse(ddlEng.SelectedValue);
        prospect.PhysicistID = int.Parse(ddlPhysicist.SelectedValue);

        prospect.Save();

        // Move folders
        if (chkFolders.Checked)
        {
            string sourcePath = prospect.DirectoryPath;
            string destinationPath = projectsDirectory + newName;

            string sourceExecPath = prospect.DirectoryPathExec;
            string destinationExecpath = execDirectory + newName;

            if (Directory.Exists(sourcePath))
            {
                Directory.Move(sourcePath, destinationPath);
                lblJobEstimates.Text += "Prospect folder moved.\r\n";
            }
            else
            {
                lblJobEstimates.Text += "Prospect folder not found, can't be moved.\r\n";
            }

            if (Directory.Exists(sourceExecPath))
            {
                Directory.Move(sourceExecPath, destinationExecpath);
                lblJobEstimates.Text += "Exec folder moved.\r\n";
            }
            else
            {
                lblJobEstimates.Text += "Exec folder not found, can't be moved.\r\n";
            } 
        }

        if (chkNotification.Checked)
        {
            MailMessage msg = new MailMessage();
            DAL.User pm = DAL.User.GetUserByID(int.Parse(prospect.PMUserID.ToString()));
            DAL.User sales = DAL.User.GetUserByID(int.Parse(prospect.SalespersonID.ToString()));
            
            foreach (ListItem chk in chkUsers.Items)
            {
                string tata = chk.Value;

                if (chk.Selected)
                {
                    DAL.User temp = DAL.User.GetUserFromFullName(chk.Text);
                    msg.To.Add(new MailAddress(temp.Email));
                }
            }

            msg.Subject = "New Project: " + newName;
            msg.From = new MailAddress("prospectus@veritas-medicalsolutions.com", "Project Update");
            msg.Bcc.Add(new MailAddress("mikedevenney@verizon.net", "Ho Duck Wo"));

            msg.Body = "The " + prospect.DisplayName + " has been awarded to Veritas and given the number " + prospect.ProjectNumber + ".  Congratulations to " + sales.Name + 
                " on closing the job!  The project has been assigned to " + pm.Name + " from the Project Management group.";

            SmtpClient smtp = new SmtpClient("ver-sbs-01");
            smtp.Credentials = new NetworkCredential("prospectus", "BringInTheSales1", "Veritas");

            smtp.Send(msg);

            lblJobEstimates.Text += "Notification email sent.\r\n";
        }
    }

    protected void lstProspects_Changed(object sender, EventArgs e)
    {
        int prospectID = int.Parse(lstProspects.SelectedItem.Value);
        Project prospect = new Project(prospectID);

        // populate estimates
        lstJobEstimates.DataSource = Estimate.GetEstimatesForJob(prospectID);
        lstJobEstimates.DataTextField = "EstimateNumber";
        lstJobEstimates.DataValueField = "ID";
        lstJobEstimates.DataBind();

        // set resource fields
        ddlEng.SelectedValue = prospect.DesignDraftsmanID.ToString();
        ddlEngConsultant.SelectedValue = prospect.EngineeringConsultant;
        ddlPhysicist.SelectedValue = prospect.PhysicistID.ToString();
        ddlPM.SelectedValue = prospect.PMUserID.ToString();
    }

    protected void chkNotify_Changed(object sender, EventArgs e)
    {
        Panel1.Visible = chkNotification.Checked;
    }
}