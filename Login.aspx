<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OAparIdentityApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h4 style="font-size: medium">Log In</h4>
<hr />
<div>
   <asp:Literal runat="server" ID="litErrorMsg" Text="Invalid username or password." Visible="false" />
</div>
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
   <asp:Button ID="btnSignIn" runat="server" Text="Log in" OnClick="btnSignIn_Click"  />
   <a href="Register.aspx">New User</a>
</div>

    </form>
</body>
</html>
