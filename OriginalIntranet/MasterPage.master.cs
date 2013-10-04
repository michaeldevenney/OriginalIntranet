using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using Shared;
using DAL;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public User currentUser;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = User.GetUserFromLogin(Page.User.Identity.Name);
        if(currentUser != null)
            lblLoggedInAs.Text = "Logged in as: " + currentUser.Name;               
       
    }
}
