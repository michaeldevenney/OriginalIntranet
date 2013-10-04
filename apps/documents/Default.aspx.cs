using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class apps_documents_Default : System.Web.UI.Page
{
    private const string VirtualImageRoot = @"~/_docs";
    public string currentVirtualFolder;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Label lblHeading;
            lblHeading = (Label)Master.FindControl("lblPageHeading");
            lblHeading.Text = "Document Library";

            PopulateTree();   
        }
    }

    private void PopulateTree()
    {
        DirectoryInfo rootFolder = new DirectoryInfo(Server.MapPath(VirtualImageRoot));
        TreeNode root = AddNodeAndDescendents(rootFolder, null);
        tvDocs.Nodes.Add(root);
    }

    private TreeNode AddNodeAndDescendents(DirectoryInfo folder, TreeNode parentNode)
    {
        //string virtualFolderPath;

        if (parentNode == null)
        {
            currentVirtualFolder = VirtualImageRoot;
        }
        else
        {
            currentVirtualFolder = VirtualImageRoot + @"/" + folder.Name + "/";
        }


        TreeNode node = new TreeNode(folder.Name, currentVirtualFolder);

        //Recurse through this folder's subfolders 
        DirectoryInfo[] subFolders = folder.GetDirectories();

        foreach (DirectoryInfo subFolder in subFolders)
        {
            TreeNode child = AddNodeAndDescendents(subFolder, node);

            foreach (FileInfo file in subFolder.GetFiles())
            {

                int index = file.FullName.LastIndexOf("\\");
                string strname = file.FullName.Substring(index + 1);
                string[] name = strname.Split('.');
                string url = currentVirtualFolder + "\\" + strname;

                TreeNode tn = new TreeNode();
                tn = new TreeNode(name[0], file.FullName, null, url, "_blank");

                child.ChildNodes.Add(tn);
            }

            child.NavigateUrl = null;
            child.ImageUrl = null;
            child.Expanded = false;
            node.ChildNodes.Add(child);

        }
        //Return the new TreeNode 
        return node;
    }

    protected void tvDocs_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (tvDocs.SelectedNode.ChildNodes.Count > 0)
            tvDocs.SelectedNode.ToggleExpandState();
    }
}