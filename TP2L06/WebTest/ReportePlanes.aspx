<%@ Page Language="C#"  MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="ReportePlanes.aspx.cs" Inherits="WebTest.ReportePlanes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">
    
    <h1>Planes</h1><br />
        <table class="table table-striped table-bordered table-hover table-condensed">
        
      <thead>
      <tr>
        <th>Id Plan</th>
        <th>Descripcion</th>
        <th>Especialidad</th>
      </tr>
    </thead>
            
      <tbody>
         
    <asp:Repeater ID="repeaterPlanes" runat = "server">
                <ItemTemplate>
             <tr>
            <td><asp:Label ID="txtPlan" runat ="server" Text='<%# Eval("Id") %>' /></td>
            <td><asp:Label ID="txtDescripcion" runat ="server" Text='<%# Eval("DescripcionPlan") %>' /></td>
            <td><asp:Label ID="txtEspecialidad" runat ="server" Text ='<%#Eval("DescripcionEspecialidad") %>' /></td>
            </tr>
                </ItemTemplate> 

    </asp:Repeater>
          
      </tbody>

        </table>  

</asp:Content>
    