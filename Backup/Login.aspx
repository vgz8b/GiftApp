<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebCamApplicationTheLast.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h1>
            Login
        </h1>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ValidationGroup="Login1" />
        <p>
            &nbsp;</p>
    
    </div>
            <asp:Login ID="Login1" runat="server" 
        DestinationPageUrl="~/Default.aspx">
            </asp:Login>
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Home" />
    </form>
</body>
</html>
