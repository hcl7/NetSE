<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="pafap.pafapSE" %>
<%--<%@ Register TagPrefix="RES" TagName="Result" Src="~/index.aspx" %>
--%>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="refresh" id="autoRefresh" runat="server" content="" />
    <title>PaFaP NetSE</title>
    <link rel="stylesheet" href="css/pafap_index.css" type="text/css" />
    <link rel="stylesheet" href="css/pafap_instant.css" type="text/css" />
    <script src="js/functions.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/pafap_paging.css" type="text/css" />
    <link rel="stylesheet" href="css/pafap_info.css" type="text/css" />
</head>
<body>
<div id="dialog" title="Log View dialog" style="width: 600px;">
<p><asp:Label ID="lblogview" runat="server" Visible="false" ></asp:Label></p>
</div>
<form id="frmSE" runat="server" onsubmit="return CheckForm();">
<center> 
  <p>&nbsp;</p> 
<a href="index.aspx"><img src="images/LOGO.png" style="border:0px;" alt="pafapSE LOGO" /></a>
<div id="pafap_search_container" style="width:700px;"> 
	<b class="pafap_search_top">
		<b class="pafap_search_b1"></b>
		<b class="pafap_search_b2"></b>
		<b class="pafap_search_b3"></b>
		<b class="pafap_search_b4"></b>
	</b> 
    <div class="pafap_search_content"> 
        <asp:TextBox ID="QuerySearch" MaxLength="100" Width="470" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" Text="Search" Height="20" runat="server" onclick="btnSearch_Click" />
    </div> 
    	<b class="pafap_search_bottom">
    		<b class="pafap_search_b4"></b>
    		<b class="pafap_search_b3"></b>
    		<b class="pafap_search_b2"></b>
    		<b class="pafap_search_b1"></b>
    	</b> 
</div> 
</center> 
<p class="info">
    <asp:Label ID="lblQueryTime" runat="server"></asp:Label>
    <asp:DropDownList ID="ddlpaths" Height="20" runat="server" Visible="false"></asp:DropDownList>
    <asp:LinkButton ID="lnkList" Text="Listo" runat="server" OnClientClick="javascript:return info();" onclick="lnkList_Click" Visible="false"></asp:LinkButton>
    <asp:LinkButton ID="lnkLogView" runat="server" Visible="false" OnClientClick="javascript:return viewlog();">Log View</asp:LinkButton>
</p>

<asp:PlaceHolder ID="phview" runat="server" Visible="false">
<div id="pafap_search_results"> 
  <b class="pafap_search_top">
  	<b class="pafap_search_b1"></b>
  	<b class="pafap_search_b2"></b>
  	<b class="pafap_search_b3"></b>
  	<b class="pafap_search_b4"></b>
  </b> 
  <div class="pafap_search_content_result" style="padding:5px; ">
    <asp:Repeater ID="rptResults" runat="server">
        <ItemTemplate>
        <b class="title_results"><%# DataBinder.Eval(Container.DataItem, "fn") %></b><br />
        <table>
         <tr>
            <td><asp:CheckBox ID="chkcp" runat="server" Text="" /></td>
            <td><asp:Label CssClass="results" ID="lblPath" Text='<%# DataBinder.Eval(Container.DataItem, "pth") %>' runat="server" /></td>
            <td><asp:Label CssClass="results" ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "fn") %>' runat="server" /></td>
            <td><asp:Label CssClass="results" Text='<%# DataBinder.Eval(Container.DataItem, "date_modified") %> ' runat="server" /></td>
            <td><asp:Label CssClass="results" Text='<%# DataBinder.Eval(Container.DataItem, "aspect_ratio")%>' runat="server" /></td>
         </tr>
        </table>
        <hr class="hr_display" />
        </ItemTemplate>
    </asp:Repeater>
    <p class="paging">
        <asp:LinkButton ID="lnkPrev" runat="server" OnClick="lnkPrev_Click">Prev</asp:LinkButton>
        <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">Next</asp:LinkButton>
    </p>
  </div> 
  <b class="pafap_search_bottom_result">
    <b class="pafap_search_b4"></b>
    <b class="pafap_search_b3"></b>
    <b class="pafap_search_b2"></b>
    <b class="pafap_search_b1"></b>
  </b> 
</div> 
</asp:PlaceHolder>

<br />
<hr />
<asp:Label ID="lblstatus" runat="server"></asp:Label>
<center>
<div style="color:#777777;">
	Copyright &#169 2010 <a href="index.aspx">PaFaP</a>
</div>
</center>
</form>
</body>
</html>
