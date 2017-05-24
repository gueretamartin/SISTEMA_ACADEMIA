<%@ Page Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="Alumnos.aspx.cs" Inherits="WebTest.Alumnos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">
    <asp:Panel ID="gridPanel" runat="server">
        <asp:GridView class="table table-hover" ID="gridView" runat="server" AutoGenerateColumns="False"
            OnSelectedIndexChanged="gridView_SelectedIndexChanged"
            SelectedRowStyle-BackColor="#cacaca"
            SelectedRowStyle-ForeColor="WindowText"
            DataKeyNames="ID" Width="147px">
            <Columns>
                <asp:BoundField HeaderText="Nombre" DataField="NombrePersona" />
                <asp:BoundField HeaderText="Apellido" DataField="ApellidoPersona" />
                <asp:BoundField HeaderText="Legajo" DataField="Legajo" />
                <asp:BoundField HeaderText="Telefono" DataField="Telefono" />
                <asp:BoundField HeaderText="Email" DataField="Email" />
                <asp:BoundField HeaderText="Fecha de Nacimiento" DataField="Fecha" />
                <asp:BoundField HeaderText="Direccion" DataField="Direccion" />
                <asp:BoundField HeaderText="Plan" DataField="Plan" />
                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <br />
    <asp:Panel ID="gridActionPanel" runat="server">
        <asp:LinkButton class="btn btn-primary" ID="lbtnEditar" runat="server" OnClick="LbtnEditar_Click">Editar</asp:LinkButton>
        <asp:LinkButton class="btn btn-danger" ID="lbtnEliminar" runat="server" OnClick="lbtnEliminar_Click">Eliminar</asp:LinkButton>
        <asp:LinkButton class="btn btn-success" ID="lbtnNuevo" runat="server" OnClick="lbtnNuevo_Click">Nuevo</asp:LinkButton>
    </asp:Panel>

    <!-- Formulario de edicion -->

    <asp:Panel ID="formPanel" Visible="false" runat="server">
        <div class="form-group">
            <asp:Label Text="ID Persona: " ID="lblId" runat="server" />
            <asp:TextBox class="form-control" Text="ID" ID="txtId" runat="server"  /><br />
            <asp:Label Text="Nombre: " ID="lblNombrePersona" runat="server" />
            <asp:TextBox class="form-control" Text="Nombre" ID="txtNombrePersona" runat="server" /><br />
            <asp:Label Text="Apellido: " ID="lblApellidoPersona" runat="server" />
            <asp:TextBox class="form-control" Text="Apellido" ID="txtApellidoPersona" runat="server"  /><br />
            <asp:Label Text="Legajo: " ID="lblLegajo" runat="server" />
            <asp:TextBox class="form-control" Text="Legajo" ID="txtLegajo" type="number" runat="server" /><br />
            <asp:Label Text="Telefono: " ID="lblTelefono" runat="server" />
            <asp:TextBox class="form-control" Text="Telefono" ID="txtTelefono" runat="server" /><br />
            <asp:Label Text="Email: " ID="lblEmail" runat="server" />
            <asp:TextBox class="form-control" Text="Email" ID="txtEmail" runat="server"  /><br />
            <asp:Label Text="Fecha de Nacimiento (yyyy/MM/dd): " ID="lblFecha" runat="server" />
            <asp:TextBox class="form-control" Text="Fecha de Nacimiento" ID="txtFecha" runat="server" /><br />
            <asp:Label Text="Direccion: " ID="lblDireccion" runat="server" />
            <asp:TextBox class="form-control" Text="Direccion" ID="txtDireccion" runat="server"  /><br />
            <asp:Label Text="Plan: " ID="lblIdPlan" runat="server" />
            <asp:ListBox class="form-control" ID="listIdPlan" runat="server" ReadOnly="false" /><br />
    </div>
    </asp:Panel>
    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    <br />
    <asp:Panel ID="formActionPanel" runat="server">
        <asp:LinkButton class="btn btn-success" ID="lbtnAceptar" runat="server" OnClick="lbtnAceptar_Click">Aceptar</asp:LinkButton>
        <asp:LinkButton class="btn btn-danger" ID="lbtnCancelar" runat="server">Cancelar</asp:LinkButton>
    </asp:Panel>

</asp:Content>
