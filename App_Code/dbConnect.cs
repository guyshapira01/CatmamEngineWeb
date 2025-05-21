using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.Odbc;

/// <summary>
/// Summary description for dbConnect
/// </summary>
public class dbConnect
{
    private string sConnectionString;
    private OdbcConnection odbcCon;

    public dbConnect(string db)
	{
        odbcCon = new OdbcConnection(db);
	}
    public void OpenConnection()
    {
        odbcCon.Open();
    }
    public void CloseConnection()
    {
        odbcCon.Close();
    }
    public DataTable getDT(string sSql)
    {
        OdbcDataAdapter adpt = new OdbcDataAdapter(sSql, odbcCon);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        return dt;
    }
    public DataSet getPartResults(string ssql)
    {
        DataSet curDs= new DataSet();
        OpenConnection();
        OdbcDataAdapter adpt = new OdbcDataAdapter(ssql, odbcCon);
        adpt.Fill(curDs);
        CloseConnection();
        return (curDs);

    }
}
