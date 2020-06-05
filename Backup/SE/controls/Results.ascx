<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Results.ascx.cs" Inherits="pafap.controls.Results" %>

<link rel="stylesheet" href="../css/pafap_index.css" type="text/css" />
<link rel="stylesheet" href="../css/pafap_instant.css" type="text/css" />
    <link rel="stylesheet" href="../css/pafap_paging.css" type="text/css" />
    <script src="../js/functions.js" type="text/javascript"></script>
<asp:Label ID="lblQueryTime" runat="server"></asp:Label><br />
<div style="text-align:left;">
  <div id="pafap_search_container"> 
  <b class="pafap_search_top">
  	<b class="pafap_search_b1"></b>
  	<b class="pafap_search_b2"></b>
  	<b class="pafap_search_b3"></b>
  	<b class="pafap_search_b4"></b>
  </b> 
    <div class="pafap_search_content" style="padding:5px; "> 
        <asp:Label ID="lbltest" runat="server"></asp:Label><br />
        <asp:Repeater ID="rptResults" runat="server">
        <ItemTemplate>
            <%# DataBinder.Eval(Container.DataItem,"pth") %>
            <%# DataBinder.Eval(Container.DataItem,"fn") %><br>
            <hr class="hrdisplay" />
        </ItemTemplate>
    </asp:Repeater>
        <p class="paging" style="font-style:normal;">
        <asp:LinkButton ID="lnkPrev" runat="server" OnClick="lnkPrev_Click">Prev</asp:LinkButton>
        <<asp:PlaceHolder ID="plcPaging" runat="server" />
        <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">Next</asp:LinkButton>
        </p>
    </div> 
    	<b class="pafap_search_bottom">
    		<b class="pafap_search_b4"></b>
    		<b class="pafap_search_b3"></b>
    		<b class="pafap_search_b2"></b>
    		<b class="pafap_search_b1"></b>
    	</b> 
    </div> 
</div>
