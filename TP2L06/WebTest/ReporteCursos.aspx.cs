using Entidades;
using System;
using System.Collections.Generic;

namespace WebTest
{
    

    public partial class ReporteCursos : System.Web.UI.Page
    {
        Negocio.ControladorCursos cc = new Negocio.ControladorCursos();


        protected void Page_Load(object sender, EventArgs e)
        {
            TipoPersona tipo = (TipoPersona)Session["tipousuario"];
            if (tipo != null)
            {
                if (!Util.ValidarPermisos.TienePermisosUsuario(tipo.Id, this.Page.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "")))
                    Response.Redirect("~/Permisos.aspx");
            }
            else
                Response.Redirect("~/Login.aspx");




            if (!IsPostBack)
            {
                repeaterCursos.DataSource = cc.dameTodos();
                repeaterCursos.DataBind();

                Negocio.ControladorMaterias cm = new Negocio.ControladorMaterias();
                List<Materia> materias = new List<Materia>();
                materias.Add(new Materia() { Id = -1, DescripcionMateria = "Ver Todas" });
                materias.AddRange(cm.dameTodos());
                this.DropDownListMateria.DataSource = materias;
                this.DropDownListMateria.DataValueField = "Id";
                this.DropDownListMateria.DataTextField = "DescripcionMateria";
                this.DropDownListMateria.DataBind();

                List<string> años = new List<string>();
                años.Add("Todos");
                for (int i = 1980; i < 2020; i++)
                    años.Add(i.ToString());
                this.DropDownListAño.DataSource = años;
                this.DropDownListAño.DataBind();
            }
        }

        protected void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idMateria = int.TryParse(this.DropDownListMateria.SelectedValue,out idMateria)? idMateria : -1;
            int año = int.TryParse(this.DropDownListAño.SelectedValue, out año)? año : -1;
            repeaterCursos.DataSource = new Negocio.ControladorCursos().dameTodosPorCondicion(idMateria,año);
            repeaterCursos.DataBind();
        }
    }
}