<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="WebTest.Permisos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <link href="css/jquery-ui.min.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet"/>
    <script src="scripts/jquery-3.1.1.min.js"></script>
    <script src="scripts/jquery-ui.min.js"></script>
    

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        function onReady() {
            $("#accordion").accordion();
        }
    </script>
</head>
<body onload="onReady()">
    <form id="form1" runat="server">
        <div>
            <div id="accordion">
                <h1 style="font-size:50px;"><img src="img/info2.png" /> Permiso Denegado!</h1>
                <div>
                    <p style="padding-left:25%; font-size:30px; font-weight:bold;">
                       Debido al nivel de permisos es imposible acceder a este lugar.
                    </p>
                </div>
                <h3 style="font-size:25px;">Serás redireccionado a la página principal... TKM AMI :)</h3>
                <div>
                    <p>
                       Usche
                    </p>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
