<%@ Page Language="vb" AutoEventWireup="true" CodeFile="UpdatePanelTest.aspx.vb" Inherits="UpdatePanelTest" %>
<%@ Register Assembly="System.Web.Silverlight"
  Namespace="System.Web.UI.SilverlightControls" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/ xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>  
        <div style="background-color: Lime; padding: 15px; font-family: Verdana; font-size: small">
        <p>This section is ASP.NET content.</p>
     <asp:UpdatePanel id="updatePanel1" runat="server">
       <ContentTemplate>        

        <asp:Button ID="cmdUpdateNoPost" runat="server" Text="Update Label (no postback)" 
            onclick="cmdUpdateNoPost_Click" />&nbsp;

            <br />
            <br />           

        <asp:Label ID="lbl" runat="server"></asp:Label>  

        </ContentTemplate>
        </asp:UpdatePanel>

        <br />
        <asp:Button ID="cmdUpdatePost" runat="server" Text="Update Label (with postback)" 
            onclick="cmdUpdatePost_Click" Width="238px" />
        <br /><br />
        </div>
        <br />
        <asp:Silverlight ID="Silverlight1" Source="~/ClientBin/SilverlightApplication.xap" EnableRedrawRegions="true"
         runat="server" BorderColor="SteelBlue" BorderStyle="Solid" BorderWidth="1" Height="150" Width="100%">
        </asp:Silverlight>  
    </div>
    </form>
</body>
</html>
