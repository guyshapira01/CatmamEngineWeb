using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class searchResult : System.Web.UI.Page
{
     string partName="";
    string partMakat="";
    string partYazran = "";
    string searchCatalogPkey = "";
    string searchDomain = "";
    string searchFamily = "";
    int partCount;
    int lastPartCount ;  //Keeps the values for paging back
    string conn;
    DataSet myds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        conn = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        GridView1.AdvanceViewBy = "hyperLink";
        GridView1.AdvanceViewLinkColumn = "שם חלק";
        if (Request.QueryString["checkboxPage"] != null)
        {
            if (Request.QueryString["checkboxPage"].ToString() != "Part")
                GridView1.AdvanceViewLinkColumn = "שם תמונה";
        }
        
        if (!IsPostBack)
        {
            Session.Add("SortDirection", System.Web.UI.WebControls.SortDirection.Descending);
            Session.Add("SortExpression", "");
            
            
            //Session.Add("sortExp", "פריט ASC");
            FindResults();
            //DataTable dataTable = GridView1.DataSource as DataTable;
            //if (dataTable != null)
            //{
            //   DataView dataView = new DataView(dataTable);
            //  dataView.Sort = Session["sortExp"].ToString();
            //   GridView1.DataSource = dataView;
            //  GridView1.DataBind();
            //}
        }
        GridView1.DataBind();
    }

    private void FindResults()
    {
        string checkboxPage = "Text";
        if (Request.QueryString["checkboxPage"] != null)
        {
            checkboxPage = Request.QueryString["checkboxPage"].ToString();
        }
        if (Request.QueryString["parit_name"] != null)
        {
            partName = Request.QueryString["parit_name"].ToString ();
        }
        if (Request.QueryString["parit_makat"] != null)
        {
            partMakat = Request.QueryString["parit_makat"].ToString ();
        }
        if (Request.QueryString["parit_yazran"] != null)
        {
            partYazran = Request.QueryString["parit_yazran"];
        }
        if (Request.QueryString["ddlCat"] != null)
        {
            searchCatalogPkey = Request.QueryString["ddlCat"];
        }
        if (Request.QueryString["searchDomain"] != null)
        {
            searchDomain = Request.QueryString["searchDomain"];
        }
        if (Request.QueryString["ddlFamily"] != null)
        {
            searchFamily = Request.QueryString["ddlFamily"];
        }
        //searchCodeSite = session["SiteCode"];

        //Set how many records per page we want
        int NumPerPage=10;
        if (Request.QueryString["partAmount"] != null)
        {
            NumPerPage = Convert.ToInt32(Request.QueryString["partAmount"]);
            GridView1.PageSize = NumPerPage;
        }

        //Retrieve what page we're currently on
        int CurPage=1;
        //Response.Write(Request.QueryString("CurPage"))
        if (Request.QueryString["CurPage"] != null)
        {
            if (Request.QueryString["CurPage"].ToString() == "")
                CurPage = 1;
            //We're on the 	first page
            else
                CurPage = Convert.ToInt32(Request.QueryString["CurPage"]);
        }

        partCount = 1;
        if (Request.QueryString["CurPartCount"] != null)
        {
            if (Request.QueryString["CurPartCount"].ToString() == "")
                partCount = 1;
            //We're on the 	first page
            else
                partCount = Convert.ToInt32(Request.QueryString["CurPartCount"]);
        }

        lastPartCount = partCount - NumPerPage;
        

        string SQL1 = "";

        //*Search Text.
        if (checkboxPage == "Text")
        {
			
            SQL1 = "SELECT 'catalogs/' & T_CAT_CATALOG.PKEY & '/' & T_CAT_PAGE.PKEY & '.html' AS hyperLink ,";
            SQL1 += "T_CAT_PAGE.PHEBDESC as \"שם תמונה\" ,T_CAT_CATALOG.PHEBDESC as \"שם קטלוג\", T_CAT_FAMILY.PFAMILY AS \"שם משפחה\" ,T_CAT_PAGE.PPAGENUMBER AS \"מספר תמונה\"";
            SQL1 += "FROM ((T_CAT_TEXT LEFT JOIN T_CAT_CATALOG ON T_CAT_TEXT.PKEYCATALOG = T_CAT_CATALOG.PKEY) ";
            SQL1 += "LEFT JOIN T_CAT_PAGE ON T_CAT_TEXT.PKEYPAGE = T_CAT_PAGE.PKEY) LEFT JOIN T_CAT_FAMILY ON T_CAT_CATALOG.PCATALOGFAMILY = T_CAT_FAMILY.PKEY WHERE 1=1  ";
        }


        else if (checkboxPage == "Picture")
        {
			//http://localhost/Mazlah/Templates/ShowTemplate.aspx?Template=normal.html&Pkey=28304a13&ParentKey=28304a1|&Type=2&PkeyCatalog=28302a1
            //SQL1 = " SELECT '../CatmamEngine/catalogs/' & T_CAT_CATALOG.PKEY & '/' & T_CAT_PAGE.PKEY & '.html' AS hyperLink,T_CAT_PAGE.PHEBDESC AS \"שם תמונה\",T_CAT_PAGE.PPAGENUMBER AS \"מספר תמונה\", T_CAT_CATALOG.PHEBDESC AS \"שם קטלוג\", T_CAT_FAMILY.PFAMILY AS \"שם משפחה\" ";
			SQL1 = " SELECT 'http://localhost/Mazlah/Templates/ShowTemplate.aspx?Template=normal.html&Pkey=' & T_CAT_PAGE.PKEY & '&ParentKey=' & T_CAT_NODES.PPARENTKEY & '&CatalogIndex=' & T_CAT_CATALOG.PIMPORTSTATUS &  '&Type=2&PkeyCatalog=' & T_CAT_CATALOG.PKEY AS hyperLink,T_CAT_PAGE.PHEBDESC AS \"שם תמונה\",T_CAT_PAGE.PPAGENUMBER AS \"מספר תמונה\", T_CAT_CATALOG.PHEBDESC AS \"שם קטלוג\", T_CAT_FAMILY.PFAMILY AS \"שם משפחה\" ";
            SQL1 += " FROM ((T_CAT_PAGE INNER JOIN T_CAT_CATALOG ON T_CAT_PAGE.PKEYCATALOG = T_CAT_CATALOG.PKEY) LEFT JOIN T_CAT_FAMILY ON T_CAT_CATALOG.PCATALOGFAMILY = T_CAT_FAMILY.PKEY) INNER JOIN T_CAT_NODES ON T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY  WHERE 1=1  ";
        }


        else  if (checkboxPage == "Part")
        {
			//http://localhost/Mazlah/Templates/ShowTemplate.aspx?Template=normal.html&Pkey=28304a13&ParentKey=28304a1&Type=2&PkeyCatalog=28302a1
			//http://localhost/Mazlah/Templates/ShowTemplate.aspx?Template=normal.html&Pkey=28304a13&ParentKey=28304a1|&Type=2&PkeyCatalog=28302a1?part=area_28304a2243
			//http://localhost/Mazlah/Templates/ShowTemplate.aspx?Template=normal.html&Pkey=28304a2243&ParentKey=28304a13&Type=2&PkeyCatalog=28302a1?part=area_28304a2243
			//http://localhost/Mazlah/Templates/ShowTemplate.aspx?Template=normal.html&Pkey=28304a19&ParentKey=28304a2&Type=2&PkeyCatalog=28302a1
            SQL1 = "SELECT  'http://localhost/Mazlah/Templates/ShowTemplate.aspx?Template=normal.html&Pkey=' & T_CAT_PART.PKEYPAGE & '&ParentKey=' & T_CAT_NODES_1.PPARENTKEY & '&Type=2&PkeyCatalog=' & T_CAT_CATALOG.PKEY & '&CatalogIndex=' & T_CAT_CATALOG.PIMPORTSTATUS & '&part=area_' & T_CAT_PART.PKEY AS hyperLink ";
            SQL1 += " , T_CAT_PART.PHEBDESC AS \"שם חלק\",T_CAT_MAKAT.PMAKAT AS \"מקט צהל\" , T_CAT_VENDORS.PVENDOR_CODE AS \"מספר יצרן\" , T_CAT_PAGE.PHEBDESC AS \"שם תמונה\", ";
            SQL1 += " T_CAT_PAGE.PPAGENUMBER AS \"מספר תמונה\", T_CAT_CATALOG.PHEBDESC AS \"שם קטלוג\" , T_CAT_FAMILY.PFAMILY AS \"שם משפחה\" ";
            SQL1 += " FROM (((((T_CAT_NODES RIGHT JOIN (T_CAT_CATALOG RIGHT JOIN T_CAT_PART ON T_CAT_CATALOG.PKEY = T_CAT_PART.PKEYCATALOG) ON T_CAT_NODES.PKEY = T_CAT_PART.PKEY) LEFT JOIN T_CAT_FAMILY ON T_CAT_CATALOG.PCATALOGFAMILY = T_CAT_FAMILY.PKEY) LEFT JOIN T_CAT_VENDORS ON T_CAT_PART.PKEY = T_CAT_VENDORS.PPARENTKEY) LEFT JOIN T_CAT_MAKAT ON T_CAT_PART.PKEY = T_CAT_MAKAT.PPARENTKEY) LEFT JOIN T_CAT_PAGE ON T_CAT_NODES.PPARENTKEY = T_CAT_PAGE.PKEY) LEFT JOIN T_CAT_NODES AS T_CAT_NODES_1 ON T_CAT_NODES.PPARENTKEY = T_CAT_NODES_1.PKEY WHERE (((T_CAT_PAGE.PKEY) Is Not Null)) ";
			
			//SQL1 += " FROM ((((T_CAT_NODES RIGHT JOIN (T_CAT_CATALOG RIGHT JOIN T_CAT_PART ON T_CAT_CATALOG.PKEY = T_CAT_PART.PKEYCATALOG) ON T_CAT_NODES.PKEY = T_CAT_PART.PKEY) LEFT JOIN T_CAT_FAMILY ON T_CAT_CATALOG.PCATALOGFAMILY = T_CAT_FAMILY.PKEY)";
            //SQL1 += " LEFT JOIN T_CAT_VENDORS ON T_CAT_PART.PKEY = T_CAT_VENDORS.PPARENTKEY) ";
            //SQL1 += " LEFT JOIN T_CAT_MAKAT ON T_CAT_PART.PKEY = T_CAT_MAKAT.PPARENTKEY) ";
            //SQL1 += " LEFT JOIN T_CAT_PAGE ON T_CAT_NODES.PPARENTKEY = T_CAT_PAGE.PKEY ";
            //SQL1 += " WHERE T_CAT_PAGE.PKEY is not NULL  ";
            
        }
        if (searchDomain == "Current")
        {
            SQL1 = SQL1 + " AND T_CAT_CATALOG.PKEY LIKE '" + searchCatalogPkey + "' ";
        }

        if (searchDomain == "Family")
        {
            SQL1 = SQL1 + " AND T_CAT_CATALOG.PCATALOGFAMILY='" + searchFamily + "' ";
        }
        
        // Nodes is not needed since generating the catalog ommits the deleted ones from the tables in access
        if (checkboxPage == "Text")
        {
            SQL1 = SQL1 + " AND T_CAT_CATALOG.PKEY=T_CAT_CATALOG.PKEY ";
        }

        //Add description to the SQL1
        string strSearchName = "";

        if (partName != "")

            strSearchName = "'%" + partName + "%' ";
        else
            strSearchName = "'%'";

        if (checkboxPage == "Picture")
        {
            SQL1 = SQL1 + " AND T_CAT_PAGE.PHEBDESC LIKE " + strSearchName;
        }

        else if (checkboxPage == "Text")
        {
            SQL1 = SQL1 + " AND T_CAT_TEXT.PCONTENT LIKE " + strSearchName;
        }

        else if (checkboxPage == "Part")
        {
            SQL1 = SQL1 + "  AND T_CAT_PART.PHEBDESC LIKE " + strSearchName;
        }

        //Add makat to the SQL1
        if (partMakat != "")
        {
            SQL1 = SQL1 + "  AND T_CAT_PART.PKEY IN ";
            SQL1 = SQL1 + " ( SELECT DISTINCT T_CAT_MAKAT.PPARENTKEY ";
            SQL1 = SQL1 + " FROM T_CAT_MAKAT  WHERE ";
            SQL1 = SQL1 + " T_CAT_MAKAT.PMAKAT LIKE '%" + partMakat + "%') ";

        }

        //add vendor to the SQL1 ..
        if (partYazran != "")
        {
            SQL1 = SQL1 + " AND T_CAT_PART.PKEY IN ";
            SQL1 = SQL1 + " ( SELECT DISTINCT T_CAT_VENDORS.PPARENTKEY ";
            SQL1 = SQL1 + " FROM T_CAT_VENDORS  WHERE ";
            SQL1 = SQL1 + " T_CAT_VENDORS.PVENDOR_NUMBER LIKE '%" + partYazran + "%') ";
        }

        switch (searchDomain)
        {
            case "All":
                //Response.Write("כולם");
                break;
            case "Family":
               // Response.Write("משפחה");
                break;
            case "Current":
                //Response.Write("נוכחי");
                break;
        }

      

        if (checkboxPage == "Part")
        {

            ////Response.Write("		<noYazran>");
            ////if (partYazran != "")
            ////    Response.Write(partYazran);
            ////else
            ////    Response.Write("ללא הגבלה");

            ////Response.Write("</noYazran>" + "</br>");

            ////Response.Write("		<Makat>");
            ////if (partMakat != "")
            ////    Response.Write(partMakat);
            ////else
            ////    Response.Write("ללא הגבלה");

            ////Response.Write("</Makat>" + "</br>");

        }

        if (checkboxPage == "Picture")
        {
            //Response.Write("		<namePage><![CDATA[");
            //if (partName != "")
            //    Response.Write(partName);
            //else
            //    Response.Write("ללא הגבלה");

            //Response.Write("]]></namePage>" + "</br>");
        }
        else
        {
            //Response.Write("		<nameParit><![CDATA[");
            //if (partName != "")
            //    Response.Write(partName);
            //else
            //    Response.Write("ללא הגבלה");

            //Response.Write("]]></nameParit>" + "</br>");
        }
        //Response.Write(SQL1);
        //Response.End();
        dbConnect BL = new dbConnect(conn);
        //Response.Write(SQL1);
        //Response.End();
        myds= BL.getPartResults(SQL1);
        GridView1.DataSource = myds.Tables[0];
        //GridView1.DataBind();
        int startRes = GridView1.PageIndex  * GridView1.PageSize + 1;
        int endRes = (GridView1.PageIndex+1)*GridView1.PageSize ;
        if (endRes > myds.Tables[0].Rows.Count)
            endRes = myds.Tables[0].Rows.Count;
        if (myds.Tables[0].Rows.Count == 0)
        {
            lblResults.Text = "לא נמצאו תוצאות";
            GridView1.Visible = false;
        }
        else
            lblResults.Text = "מציג תוצאות <b>" + startRes.ToString() + "</b> עד <b>" + endRes.ToString() + "</b> מתוך <b>" + myds.Tables[0].Rows.Count + "</b>"; 
       
    }
    
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        //FindResults();
        ////DataTable dataTable = GridView1.DataSource as DataTable;
        ////if (dataTable != null)
        ////{
        ////    DataView dataView = new DataView(dataTable);
        ////    dataView.Sort = Session["sortExp"].ToString();
        ////    //Response.Write("sortExp=" + Session["sortExp"].ToString());
        ////    GridView1.DataSource = dataView;
        ////    GridView1.DataBind();
        ////}
        

        
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
       ////// //Response.Write("sort:" + Session["sortExp"].ToString());
       //////FindResults();
       //////DataTable dataTable = GridView1.DataSource as DataTable;
       ////// if (dataTable != null)
       ////// {
       //////     DataView dataView = new DataView(dataTable);
       //////     dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
       //////     Session["sortExp"] = dataView.Sort;
       //////     GridView1.DataSource = dataView;
       //////     GridView1.DataBind();
       ////// }
    }
    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }
}
