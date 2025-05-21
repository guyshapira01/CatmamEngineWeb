<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" viewStateEncryptionMode ="Never" CodeFile="searchResult.aspx.cs" Inherits="searchResult" %>
<%@ Register Assembly="ViewManager" Namespace="ViewManager" TagPrefix="GP" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>תוצאות חיפוש</title>
    <link rel="stylesheet" href="common/stylesheet.css" type="text/css"  /> 
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    <div>
    <div style="border-bottom:solid 1px lightgray;vertical-align:top;background-color:white;width:100%;font-size:50px;">
        <img align="absmiddle" src="images/logo.gif" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;תוצאות חיפוש
    </div>
    <table dir="rtl"><tr><td><asp:Label ID="lblResults" runat="server"></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="Default.aspx">חזרה למסך החיפוש >></a></td></tr></table>
    
        <GP:GridPro
            AllowSorting="true" AllowPaging="true" PageSize="100" 
            width="100%"
            ID="GridView1" runat="server" BackColor="White"  style="direction:rtl;"
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            GridLines="Both" onpageindexchanging="GridView1_PageIndexChanging" 
            onsorting="GridView1_Sorting" MainViewColumns="שם חלק,מספר יצרן,מקט צהל,שם קטלוג,שם תמונה,מספר תמונה,שם משפחה" 
             >
            <CustomStyles>
                <GridCellStyle  />
            </CustomStyles>
            <PagerSettings PageButtonCount="10"   />
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C"  />
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingRowStyle BackColor="#F7F7F7" />
        </GP:GridPro>
    </div>
    </form>
</body>
</html>
