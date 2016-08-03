<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OAparIdentityApp.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h4 style="font-size: medium">Register a new user</h4>
<hr />
<p>
   <asp:Literal runat="server" ID="litStatusMessage" />
</p>
<div style="margin-bottom: 10px">
   <asp:Label runat="server" AssociatedControlID="txtbxUserName">User name</asp:Label>
   <br />
   <asp:TextBox runat="server" ID="txtbxUserName" />
</div>
<div style="margin-bottom: 10px">
   <asp:Label runat="server" AssociatedControlID="txtbxPassword">Password</asp:Label>
   <br />
   <asp:TextBox runat="server" ID="txtbxPassword" TextMode="Password" />
</div>
<div style="margin-bottom: 10px">
   <asp:Label runat="server" AssociatedControlID="txtbxConfirmPassword">Confirm password</asp:Label>
   <br />
   <asp:TextBox runat="server" ID="txtbxConfirmPassword" TextMode="Password" />
</div>
<div>
   <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
</div>


    </form>
</body>
</html>
