<%@ Page Language="vb" AutoEventWireup="true" CodeFile="MediaPlayback.aspx.vb" Inherits="MediaPlayback" %>

<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls"
    TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:MediaPlayer ID="MediaPlayer1" Width="600" Height="400" runat="server" 
            MediaSource="~/Butterfly.wmv" MediaSkinSource="~/Professional.xaml"   >
            <Chapters>
        <asp:MediaChapter 
            Position="5"             
            Title="Section 1." />
        <asp:MediaChapter 
            Position="10" 
            ThumbnailSource="~/image1.jpg"
            Title="Section 2." />
        <asp:MediaChapter 
            Position="15" 
            ThumbnailSource="~/image1.jpg"
            Title="Section 3." />
    </Chapters>

        </asp:MediaPlayer>
    </div>
    </form>
</body>
</html>
