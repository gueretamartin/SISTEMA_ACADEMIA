<%@ Page Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="Plan.aspx.cs" Inherits="WebTest.Plan" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">
  
    
    +<asp:Panel ID="gridPanel" runat="server">
      

        <asp:GridView class="table table-hover" ID="gridView" runat="server" AutoGenerateColumns="False"
            OnSelectedIndexChanged="gridView_SelectedIndexChanged"
            SelectedRowStyle-BackColor="#cacaca"
            SelectedRowStyle-ForeColor="WindowText"
            DataKeyNames="ID" Width="147px">
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="ID" />
                <asp:BoundField HeaderText="DescripcionPlan" DataField="DescripcionPlan" />
                <asp:BoundField HeaderText="DescripcionEspecialidad" DataField="DescripcionEspecialidad" />
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

     <asp:Label Text="" ID="lblError" runat="server"/>

    <!-- Formulario de edicion -->

    <asp:Panel ID="formPanel" Visible="false" runat="server">
        <div class="form-group">

            <asp:Label Text="ID Plan: " ID="lblId" runat="server" />
            <asp:TextBox class="form-control" Text="ID" ID="txtId" runat="server" ReadOnly="true" /><br />
             
            <asp:Label Text="Denominación: " ID="lblPlan" runat="server"></asp:Label>
            <asp:TextBox class="form-control" Text="Plan" ID="txtPlan" runat="server" /><br />
            

            <asp:Label Text="Especialidad: " ID="lblIdEspecidad" runat="server" />
            <br />
            <asp:ListBox class="form-control" ID="listEspecialidad" runat="server" Height="188px" Width="170px" /><br />

        </div>
    </asp:Panel>
    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    <asp:Panel ID="formActionPanel" runat="server">
        <asp:LinkButton class="btn btn-success" ID="lbtnAceptar" runat="server" OnClick="lbtnAceptar_Click">Aceptar</asp:LinkButton>
        <asp:LinkButton class="btn btn-danger" ID="lbtnCancelar" runat="server">Cancelar</asp:LinkButton>
    </asp:Panel>

</asp:Content>
