<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebTest.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <link href="css/customCss/login.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-3.1.1.min.js"></script>
    <script src="scripts/customScripts/login.js"></script>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="formulario col-sm-6 col-md-4 col-md-offset-4">
                <div class="col-lg-12" style="margin-top:50px;">
                    <img style="margin:10px;" src="/img/header.png"/>
                </div>
                <form id="form1" class="form-signin" runat="server">
                    <label>Usuario</label><br />
                    <asp:TextBox ID="txtUser" class="form-control" runat="server" Text="nicocda" /><br />
                    <label>Password</label><br />
                    <asp:TextBox ID="txtPass" class="form-control" runat="server" Text="nicolas23" TextMode="Password" /><br />
                    <p>
                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                    <p>
                        <asp:Button ID="btnLogin" class="btn btn-info" Text="Ingresar" runat="server" OnClick="validarEIngresar" />
                    </p>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
