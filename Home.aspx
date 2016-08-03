<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="OAparIdentityApp.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Literal runat="server" ID="litStatus" />
<br />
<asp:Button ID="btnLogOut" runat="server" Text="Log out" OnClick="btnLogOut_Click"  />

    </div>
    </form>
</body>
</html>
