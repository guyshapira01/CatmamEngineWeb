using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class _Default : System.Web.UI.Page 
{
    dbConnect dbops;
	string sPKEYCATALOG;
    protected void Page_Load(object sender, EventArgs e)
    {

        dbops = new dbConnect(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        dbops.OpenConnection();
        if (!IsPostBack)
        {
            if(Request.QueryString.Count > 0 && Request.QueryString["PKEYCATALOG"]!=null ) 
			{
				sPKEYCATALOG=Request.QueryString["PKEYCATALOG"].ToString();
			}
			FillBooksDDL();
            FillFamiliesDDL();
        }
        dbops.CloseConnection();
    }

    private void FillFamiliesDDL()
    {
        string SQL = "SELECT distinct T_CAT_CATALOG.PCATALOGFAMILY FROM T_CAT_CATALOG";
        DataTable dt = new DataTable();
        dt = dbops.getDT(SQL);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string familyVal=dt.Rows[i]["PCATALOGFAMILY"].ToString();
            SQL = "SELECT T_CAT_FAMILY.PKEY, T_CAT_FAMILY.PFAMILY FROM T_CAT_FAMILY ";
            SQL += "WHERE T_CAT_FAMILY.PKEY = '" + familyVal + "'";
            DataTable dt1 = new DataTable();
            dt1 = dbops.getDT(SQL);

            string familyName = dt1.Rows[0]["PFAMILY"].ToString();

            ListItem listI = new ListItem(familyName, familyVal);
            ddlFamily.Items.Add(listI);

        }
    }

    private void FillBooksDDL()
    {
        string strCatalog = "";
        if (Request.QueryString["Catalog"] != null)
        {
            strCatalog = Request.QueryString["Catalog"].ToString();
        }
		if(sPKEYCATALOG!="")
		{
			strCatalog=sPKEYCATALOG;
			
		}
		
        string SQL = "SELECT * FROM T_CAT_CATALOG ORDER BY T_CAT_CATALOG.PHEBDESC ASC";
        DataTable dt = new DataTable();
        dt = dbops.getDT(SQL);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (strCatalog == dt.Rows[i]["PKEY"].ToString())
            {
                ListItem listI = new ListItem(dt.Rows[i]["PHEBDESC"].ToString(), dt.Rows[i]["PKEY"].ToString());
                listI.Selected = true;
                ddlCat.Items.Add(listI);
            }
            else
            {
                ListItem listI = new ListItem(dt.Rows[i]["PHEBDESC"].ToString(), dt.Rows[i]["PKEY"].ToString());
                ddlCat.Items.Add(listI);
            }
        }

    }
    protected void ddlFamily_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void imgSRC_Click(object sender, ImageClickEventArgs e)
    {
        string strLocation = "SearchResult.aspx?checkboxPage=" + Request.Form["checkboxPage"].ToString();
        strLocation += "&searchDomain=" + Request.Form["searchDomain"].ToString();
        strLocation += "&parit_name=" + Request.Form["parit_name"].ToString();
        strLocation += "&parit_makat=" + Request.Form["parit_makat"].ToString();
        strLocation += "&parit_yazran=" + Request.Form["parit_yazran"].ToString();
        strLocation += "&ddlCat=" + Request.Form["ddlCat"].ToString();
        strLocation += "&ddlFamily=" + Request.Form["ddlFamily"].ToString();
        strLocation += "&partAmount=" + Request.Form["partAmount"].ToString();
        strLocation += "&SiteCode=" + Request.Form["SiteCode"].ToString();
        Response.Redirect(strLocation);
    }
}
