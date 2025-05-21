<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Linkware Search Engine </title>
    <link rel="stylesheet" href="search1.css" type="text/css" />
    <link rel="stylesheet" href="common/StyleSheet.css" type="text/css" />
</head>

<script type="text/javascript">
function checkPage() {	
    //alert(document.getElementById('checkboxPage_Cataltog').checked);
	/*if ( ( Search_Form.checkboxPage_Picture.checked ) || ( Search_Form.checkboxPage_Text.checked ) ) {
			//alert("Disabled");
   			document.Search_Form.parit_makat.disabled = true;
   			document.getElementById('parit_makat').style.visibility = 'none';
   			document.Search_Form.parit_makat.style.backgroundColor = "gray"
   			document.Search_Form.parit_yazran.disabled = true;
   			document.Search_Form.parit_yazran.style.backgroundColor = "gray"
			//PageOrParit.innerText = "דף"
    }
    else if (document.getElementById('checkboxPage_Cataltog').checked) {
            alert("Disabled");
            document.Search_Form.parit_makat.style.disabled = true;
            document.Search_Form.parit_makat.style.backgroundColor = "gray"
            document.Search_Form.parit_yazran.disabled = true;
            document.Search_Form.parit_yazran.style.backgroundColor = "gray"
            //PageOrParit.innerText = "דף"
        }
	else {
			//alert("Enabled");
   			document.Search_Form.parit_makat.disabled = false;   			
   			document.Search_Form.parit_makat.style.backgroundColor = "white"
   			document.Search_Form.parit_yazran.disabled = false;   			
   			document.Search_Form.parit_yazran.style.backgroundColor = "white"
			//PageOrParit.innerText = "פריט"
    }*/
    alert();
    const checkbox = document.getElementById("checkboxPage_Catalog");
    const paritMakat = document.getElementById("parit_makat");
    const paritYazran = document.getElementById("parit_yazran");
    alert(2);
    if (checkbox.checked) {
        alert(checkbox.checked);
        paritMakat.disabled = true;
        paritYazran.disabled = true;
    } else {
        paritMakat.disabled = false;
        paritYazran.disabled = false;
    }
}

function getParameterByName(name)
{
	
	name = name.replace("/[\[]/","\\[").replace("/[\]]/","\\]");
	
	var regex = new RegExp("[\\?&]"+name+"=([^&#]*)");
	result=regex.exec(location.search.toUpperCase());
	return result === null ? "" : decodeURIComponent(result[1].replace("/\+/g", " "));
}
function setSelectedRadio()
{
	var name = getParameterByName('PKEYCATALOG');
	if(name != '')
		{
			var elm= document.getElementById("searchin1");
			elm.checked = true;
		}
}
function formValidation()
		{
			if ( (document.Search_Form.parit_name.value == "") && (document.Search_Form.parit_makat.value == "") && (document.Search_Form.parit_yazran.value == ""))
				{
					if ( confirm("?פעולה זו עלולה להימשך זמן רב. האם ברצונך להמשיך"))
						{
							return(true);
						}
					else
						{
							return(false);
						}
				}
			else
				{
					return (true);
				}
		}
</script>

<body style="background-color: #f3f8fa;" onload="setSelectedRadio();">
    <form id="form1" runat="server">
    <div id="srch_div" class="headerText" style="width: 100%; text-align: center;">
        <br />
        <br />
        <br />
        <br />
        <br />
        <table dir="rtl" style="border: solid 0px black; width: 800px; background-color: #d8d1f7;"
            cellspacing="0" cellpadding="0" id="TABLE1" align="center" class="SearchFormStyleTable">
            <tr style="background-color: white;">
                <td colspan="2">
                    <span style="width: 100%; text-align: center">
                        <img src="images/logo.gif" /></span>
                </td>
            </tr>
            <tr>
                <td style="background-color: #ece8fd;">
                    <table cellpadding="2" cellspacing="2">
                        <tr>
                            <td style="background-color: #ece8fd; text-align: right" colspan="2" class="headerTextBold">
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <input onclick="JavaScript:checkPage()" value="Part" id="checkboxPage_Picture" name="checkboxPage"
                                    type="radio" checked="checked" />פריט&nbsp;&nbsp;
                                <input onclick="JavaScript:checkPage()" value="Picture" id="checkboxPage_Text" name="checkboxPage"
                                    type="radio" />תמונה&nbsp;&nbsp;
                              <input onclick="JavaScript:checkPage()" value="Cataltog" id="checkboxPage_Cataltog" name="checkboxPage"
                                    type="radio" />קטלוג</nobr>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                שם
                            </td>
                            <td align="right">
                                <input size="20" id="txt1" lang="he" name="parit_name" class="SearchFormStyleInputName" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                מק"ט
                            </td>
                            <td>
                                <input size="20" id="htxt1" lang="he" name="parit_makat" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                מספר יצרן
                            </td>
                            <td valign="top" align="right" class="SearchFormStyleTDImage">
                                <input size="20" lang="he" name="parit_yazran" id="txt3" class="SearchFormStyleInputParitYazran" /></nobr>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                כמות תוצאות
                            </td>
                            <td align="right" valign="top" colspan="2" class="SearchFormStyleTDImage">
                            <select language="hebrew" name="partAmount" id="partAmount" value="30" class="SearchFormStyleInputNumForPage">
                                <option value="10">10</option>
                                <option value="20">20</option>
                                <option value="40">40</option>
                                <option value="50">50</option>
                                <option value="100" selected="true">100</option>
                                <option value="200">200</option>
                            </select>
                                
                            </td>
                        </tr>
                        
                    </table>
                </td>
                <td valign="top">
                    <table cellpadding="2" cellspacing="2">
                        <tr>
                            <td style="background-color: #d8d1f7; text-align: right" colspan="2" class="headerTextBold">
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="background-color: #d8d1f7;" colspan="2">
                                
                                <input id="searchin3" name="searchDomain" type="radio" value="All" checked="checked" />
                                כל הקטלוגים
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: #d8d1f7;" colspan="1" align="right">
                                <input id="searchin1" name="searchDomain" type="radio" value="Current" />קטלוג
                            </td>
                            <td style="background-color: #d8d1f7;">
                                <p dir="rtl">
                                    <!--<select id="ddCat" runat="server" lang="he" name="ddCat" class="SearchFormStyleSelectCatalog" onfocus="JavaScript:changeDef('Search_Form.searchin1')">

		</select>-->
                                    <asp:DropDownList ID="ddlCat" runat="server">
                                    </asp:DropDownList>
                                </p>
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="background-color: #d8d1f7;" colspan="1" align="right">
                                <input id="searchin2" name="searchDomain" type="radio" value="Family" />
                                משפחה
                            </td>
                            <td style="background-color: #d8d1f7;" align="right">
                                <asp:DropDownList Width="290" runat="server" ID="ddlFamily" name="ddFamily">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        
                    </table>
                </td>
                
            </tr>
            <tr>
                <td style="background-color: #bfb3f8;" colspan="4">
                    <p align="center">
                        <asp:ImageButton ID="imgSRC" runat="server" ImageUrl="images/search2.gif" alt="חפש"
                            OnClick="imgSRC_Click" />
                    </p>
                </td>
            </tr>
        </table>
        <input type="hidden" lang="he" name="SiteCode" id="txt5" value="a">
    </div>
    </form>
</body>
</html>
