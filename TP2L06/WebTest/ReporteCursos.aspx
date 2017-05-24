<%@ Page Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="ReporteCursos.aspx.cs" Inherits="WebTest.ReporteCursos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">


    <h4>Seleccione la Materia:</h4>
        <asp:DropDownList ID="DropDownListMateria" runat="server" Width="89px" CssClass="text-center" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
    <h4>Seleccione Año:</h4>
        <asp:DropDownList ID="DropDownListAño" runat="server" Width="89px" CssClass="text-center" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
    <br />
    <br />


    <div id="pdfPrinter">
        <h1>Reporte de Cursos</h1>
        <table id="table" class="table table-striped table-bordered table-hover table-condensed">
            <thead>
                <tr>
                    <th>Id Curso</th>
                    <th>Materia</th>
                    <th>Curso</th>
                    <th>Año</th>
                    <th>Cupo</th>
                </tr>
            </thead>

            <tbody>
                <asp:Repeater ID="repeaterCursos" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' /></td>
                            <td>
                                <asp:Label ID="lblMateria" runat="server" Text='<%# Eval("DescripcionMateria") %>' /></td>
                            <td>
                                <asp:Label ID="lblCurso" runat="server" Text='<%#Eval("Denominacion") %>' /></td>
                            <td>
                                <asp:Label ID="lblAño" runat="server" Text='<%#Eval("AnioCalendario") %>' /></td>
                            <td>
                                <asp:Label ID="lblCupo" runat="server" Text='<%#Eval("Cupo") %>' /></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>

        </table>
    </div>
    <button onclick="javascript:imprimirReporte('Cursos')">PDF</button>
</asp:Content>
