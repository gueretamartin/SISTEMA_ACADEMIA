<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebTest.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <label>Usuario</label><br />
        <asp:TextBox ID="txtUser" runat="server" Text="nicocda" /><br />
        <label>Password</label><br />
        <asp:TextBox ID="txtPass" runat="server" Text="nicolas23" TextMode="Password" /><br />   
        <p>
        <asp:Button ID="btnLogin" Text="Ingresar" runat="server" OnClick="validarEIngresar"  />
        </p>
        <p>
            <asp:Label ID="lblError" runat="server" Enabled="False" ForeColor="Red" Text="Usuario y/o Contraseña incorrectos" Visible="False"></asp:Label>
        </p>
    </form>
</body>
</html>
