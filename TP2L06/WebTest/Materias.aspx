<%@ Page Language="C#" MasterPageFile="~/Page.Master"  AutoEventWireup="true" CodeBehind="Materias.aspx.cs" Inherits="WebTest.Materias" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">
    <asp:Panel ID="gridPanel" runat="server">
        <asp:GridView class="table table-hover" ID="gridView" runat="server" AutoGenerateColumns="False"
            OnSelectedIndexChanged="gridView_SelectedIndexChanged"
            SelectedRowStyle-BackColor="#cacaca"
            SelectedRowStyle-ForeColor="WindowText"
            DataKeyNames="ID" Width="147px">
            <Columns>

                <asp:BoundField HeaderText="ID" DataField="ID" />
                <asp:BoundField HeaderText="DescripcionMateria" DataField="DescripcionMateria" />
                <asp:BoundField HeaderText="HorasSemanales" DataField="HorasSemanales" />
                <asp:BoundField HeaderText="HorasTotales" DataField="HorasTotales" />
                <asp:BoundField HeaderText="DescripcionPlan" DataField="DescripcionPlan" />
                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />

                </Columns>
        </asp:GridView>
    </asp:Panel>
    <br />
    <asp:Panel ID="gridActionPanel" runat="server">
        <asp:LinkButton class="btn btn-primary" ID="lbtnEditar" runat="server" OnClick="LbtnEditar_Click">Editar</asp:LinkButton>
        <asp:LinkButton class="btn btn-danger" ID="lbtnEliminar" runat="server" OnClick="lbtnEliminar_Click">Eliminar</asp:LinkButton>
        <asp:LinkButton class="btn btn-success" ID="lbtnNuevo" runat="server" OnClick="lbtnNuevo_Click">Nuevo</asp:LinkButton>
        <br />
        <br />
    </asp:Panel>

    <!-- Formulario de edicion -->

    <asp:Panel ID="formPanel" Visible="false" runat="server">
        <div class="form-group">

            <asp:Label Text="ID Materia: " ID="lblId" runat="server" />
            <asp:TextBox class="form-control" Text="ID" ID="txtId" runat="server" ReadOnly="false" /><br />
             
            <asp:Label Text="Materia: " ID="lblMateria" runat="server"></asp:Label>
            <asp:TextBox class="form-control" Text="Materia" ID="txtMateria" runat="server" /><br />
          
            <asp:Label Text="Horas Semanales: " ID="lblHorasSemanales" runat="server"></asp:Label>
            <asp:TextBox class="form-control"  ID="txtHsSemanales" runat="server" /><br />
           
            <asp:Label Text="Horas Totales: " ID="lblHorasTotales" runat="server"></asp:Label>
            <asp:TextBox class="form-control"  ID="txtHorasTotales" runat="server" /><br />
          

            <asp:Label Text="Plan: " ID="lblPlan" runat="server" />
            <asp:ListBox class="form-control" ID="listIdPlan" runat="server" ReadOnly="false" /><br />

        </div>
    </asp:Panel>

    <asp:Panel ID="formActionPanel" runat="server">
        <asp:LinkButton class="btn btn-success" ID="lbtnAceptar" runat="server" OnClick="lbtnAceptar_Click">Aceptar</asp:LinkButton>
        <asp:LinkButton class="btn btn-danger" ID="lbtnCancelar" runat="server">Cancelar</asp:LinkButton>
    </asp:Panel>

</asp:Content>

