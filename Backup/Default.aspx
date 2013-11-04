<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebCamApplicationTheLast._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <asp:Image ID="Image1" runat="server" Height="240px" Visible="False" 
        Width="320px" />

   <!-- Add jQuery library -->
    <script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
    <!-- Add fancyBox main JS and CSS files -->
    <script type="text/javascript" src="js/jquery.fancybox.js?v=2.0.6"></script>
    
    <link rel="stylesheet" type="text/css" href="css/jquery.fancybox.css?v=2.0.6" media="screen" />
    <script type="text/javascript">
        $(document).ready(function () {

            $('.fancybox').fancybox();

        });
	</script>

    <style type="text/css">
        .fancybox-custom .fancybox-skin
        {
            box-shadow: 0 0 50px #222;
            width:360px !important;
        }
    </style> 

    <br />

    <a class="fancybox fancybox.iframe" href="PhotoCapture.aspx">Capture barcode Of Product</a>
    or
    <asp:TextBox ID="product" runat="server"></asp:TextBox>
    <asp:Button ID="Button7" runat="server" onclick="Button7_Click" 
        Text="Find Product" />
&nbsp;<p id="txtDecodertype" runat="server"></p>
    <p id="txtDecodercontent" runat="server"></p>
   <p id="js" runat="server"></p>
   <p id="buyp" runat="server"></p>
    <p runat="server">
        
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                <span class="input" 
    
                    style="font-weight: 700; color: rgb(42, 42, 42); font-family: 'Segoe UI', 'Lucida Grande', Verdana, Arial, Helvetica, sans-serif; font-size: 13px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: 18px; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px;">You are not logged in. Click the Login link to sign in.</span><span 
    
                    style="color: rgb(42, 42, 42); font-family: 'Segoe UI', 'Lucida Grande', Verdana, Arial, Helvetica, sans-serif; font-size: 13px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 18px; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">.</span>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Register.aspx">Register</asp:HyperLink>
            </AnonymousTemplate>
            <LoggedInTemplate>
                <span style="color: rgb(42, 42, 42); font-family: 'Segoe UI', 'Lucida Grande', Verdana, Arial, Helvetica, sans-serif; font-size: 13px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: 18px; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">
                You are logged in. Welcome,
                <asp:LoginName ID="LoginName1" runat="server" />
                 </span>
                &nbsp;&nbsp;
            </LoggedInTemplate>
        </asp:LoginView>
        <asp:LoginStatus ID="LoginStatus1" runat="server" />
        
        <asp:Button ID="Button1" runat="server" Text="Add to List" 
            onclick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="My List" />
            <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
            Text="Create Event" />
            <asp:Button ID="Button5" runat="server" onclick="Button5_Click" 
            Text="View Invites" />
            <asp:Button ID="Button6" runat="server" onclick="Button6_Click" 
            Text="Clear All" />
            </p>
        <br />
            <asp:TextBox ID="search" runat="server"></asp:TextBox>
                <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
                    Text="Search User" />
                    <p id="searchs" runat="server"></p>    
            <p id="addlist" runat="server"></p>
            <p id="mylist" runat="server"></p>
            <p id="invt" runat="server"></p>

    
        
</asp:Content>
