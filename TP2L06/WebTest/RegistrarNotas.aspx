<%@ Page Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="RegistrarNotas.aspx.cs" Inherits="WebTest.Inscripciones" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">
    <asp:Panel ID="gridPanel" runat="server">
        <asp:GridView class="table table-hover" ID="gridView" runat="server" AutoGenerateColumns="False"
            OnSelectedIndexChanged="gridView_SelectedIndexChanged"
            SelectedRowStyle-BackColor="#cacaca"
            SelectedRowStyle-ForeColor="WindowText"
            DataKeyNames="ID" Width="147px">
            <Columns>
                <asp:BoundField HeaderText="Alumno" DataField="NombreAlumno" />
                <asp:BoundField HeaderText="Curso" DataField="NombreCurso" />
                <asp:BoundField HeaderText="Condicion" DataField="Condicion" />
                <asp:BoundField HeaderText="Nota" DataField="Nota" />
                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <br />
    <asp:Panel ID="gridActionPanel" runat="server">
        <asp:LinkButton class="btn btn-primary" ID="lbtnEditar" runat="server" OnClick="LbtnEditar_Click">Editar Nota</asp:LinkButton>
        <br />
        <br />
        <asp:Label class="form-control" ForeColor="Red" Text="" ID="txtMensaje" runat="server" Enabled ="false" />
        <br />
    </asp:Panel>

    <!-- Formulario de edicion -->

    <asp:Panel ID="formPanel" Visible="false" runat="server">
        <div class="form-group">
           
            <asp:Label Text="ID Inscripcion: " ID="lblInscripcion" runat="server" />
            <asp:TextBox class="form-control" Text="" ID="txtId" runat="server" ReadOnly="true"/><br />
            
            <asp:Label Text="Alumno: " ID="lblNombreAlumno" runat="server" />
            <asp:TextBox class="form-control" Text="Alumno" ID="txtNombreAlumno" runat="server" ReadOnly="true"/><br />
            
            <asp:Label Text="Curso: " ID="lblCurso" runat="server" />
            <asp:TextBox class="form-control" Text="Curso" ID="txtCurso" runat="server"  ReadOnly="true"/><br />
            
            <asp:Label Text="Nota: " ID="lblNota" runat="server" />
            <asp:TextBox class="form-control" Text="Nota" ID="txtNota" type="number" runat="server" /><br />

            <asp:Label Text="Condición: " ID="lblCondicion" runat="server" />
            <asp:TextBox class="form-control" Text="Condición" ID="txtCondicion" runat="server" /><br />

</div>
    </asp:Panel>
    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    <asp:Panel ID="formActionPanel" runat="server">
        <asp:LinkButton class="btn btn-success" ID="lbtnAceptar" runat="server" OnClick="lbtnAceptar_Click">Aceptar</asp:LinkButton>
        <asp:LinkButton class="btn btn-danger" ID="lbtnCancelar" runat="server">Cancelar</asp:LinkButton>
    </asp:Panel>

</asp:Content>
