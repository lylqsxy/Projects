<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication4.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
    
    </div>
    </form>
</body>
</html>
