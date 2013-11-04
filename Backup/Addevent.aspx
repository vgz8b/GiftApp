<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Addevent.aspx.cs" Inherits="WebCamApplicationTheLast.WebForm3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:Label ID="Label1" runat="server" Text="Enter Event Name :"></asp:Label>
    <asp:TextBox ID="eventname" runat="server"></asp:TextBox>
    <br />
    Event Place&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :<asp:TextBox 
        ID="place" runat="server"></asp:TextBox>
        <br />Event Time&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :<asp:Label ID="Label2" runat="server" Text='<%# Bind("DateStart") %>'></asp:Label>
  <asp:TextBox ID="TextBoxEditStageDetailsDateStart" type="datetime-local" runat="server"
            Text='<%# GetFormattedDate(Eval("DateStart")) %>' CssClass="TextBoxDateTime"></asp:TextBox>
  
    <br />
   <p id="addevent" runat="server"></p>
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Add Event" />
    <br />
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
        Text="Back To Home" />
  
    <br />
    <br />
    </form>
</body>
</html>
