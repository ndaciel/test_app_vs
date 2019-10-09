<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TransportService.WebForm1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
    <div>
        <div>
            LINK:
            <asp:TextBox ID="txtLINK" runat="server" Width="519px"></asp:TextBox>
            <br />
        </div>
        <div>
            JSON:
            <asp:TextBox ID="txtJSON" runat="server" Height="115px" Width="495px"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="ShowResultjson" />
            <asp:Button ID="btnJsoncodebehind" runat="server"  Text="JSON Code Behind" 
                onclick="btnJsoncodebehind_Click" />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>


    
    </div>
    <div>
    
    </div>
    </form>
</body>
</html>