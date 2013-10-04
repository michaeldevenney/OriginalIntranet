using System;
using System.Web.UI;
using DAL;


public partial class it_Lookups_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadList();
            FillForm();
        }
    }

    protected void lstLookupLists_Change(object sender, EventArgs e)
    {
        FillForm();
    }

    protected void lstLookupItems_Change(object sender, EventArgs e)
    {
        FillForm();
    }

    protected void lstLookupItems2_Change(object sender, EventArgs e)
    {
        FillForm();
    }


    #region HELPER METHODS

    private void LoadList()
    {        
        lstLookupLists.DataSource = Lookup.GetAllListNames();
        lstLookupLists.DataTextField = "LookupList";
        lstLookupLists.DataBind();

        lstLookupLists.SelectedValue = lstLookupLists.Items[0].Text;
    }

    private void FillForm()
    {
        string currentList = lstLookupLists.SelectedItem.Text;

        lstLookupListItems.DataSource = Lookup.GetDistinctLookupValues(currentList);
        lstLookupListItems.DataTextField = "LookupValue";
        lstLookupListItems.DataValueField = "Id";
        lstLookupListItems.DataBind();

        lstLookupListItems.SelectedIndex = 0;
        string currentListItem = lstLookupListItems.SelectedItem.Text;

        lstLookupListItems2.DataSource = Lookup.GetSecondaryLookupList(currentList, currentListItem);
        lstLookupListItems2.DataTextField = "SecondaryValue";
        lstLookupListItems2.DataValueField = "Id";
        lstLookupListItems2.DataBind();

    }

    #endregion
}