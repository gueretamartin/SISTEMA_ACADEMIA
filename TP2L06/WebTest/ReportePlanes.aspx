<%@ Page Language="C#"  MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="ReportePlanes.aspx.cs" Inherits="WebTest.ReportePlanes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">
    
    
               <h4>Seleccione la Especialidad: <asp:DropDownList id="idwher" runat="server" Width="89px" CssClass="text-center" OnSelectedIndexChanged="idwher_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                   <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ver todos" />
               </h4>
                <br />
                <br />


    <div id="pdfPrinter">
        <h1>Reporte de Planes</h1>
        <table id="table" class="table table-striped table-bordered table-hover table-condensed">
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
        </div>  
    <button onclick="javascript:imprimirReporte('Planes')">PDF</button>
</asp:Content>
    