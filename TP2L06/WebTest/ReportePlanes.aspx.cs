using Entidades;
using System;
using System.Collections.Generic;

namespace WebTest
{
    public partial class ReportePlanes : System.Web.UI.Page
    {
        List<Entidades.Plan> planes = new List<Entidades.Plan>();
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


            planes = new Negocio.ControladorPlanes().dameTodos();


            if (!IsPostBack)
            {
                repeaterPlanes.DataSource = planes;
                repeaterPlanes.DataBind();

                Negocio.ControladorEspecialidad ce = new Negocio.ControladorEspecialidad();
                List<Entidades.Especialidad> especialidades = ce.dameTodos();
                this.idwher.DataSource = especialidades;
                this.idwher.DataValueField = "Id";
                this.idwher.DataTextField = "DescripcionEspecialidad";
                this.idwher.DataBind();

            }

        }

        protected void idwher_SelectedIndexChanged(object sender, EventArgs e)
        {
            String where = "where id_especialidad = " + this.idwher.SelectedValue.ToString();
            planes = new Negocio.ControladorPlanes().dameTodosPorCondicion(where);
            repeaterPlanes.DataSource = planes;
            repeaterPlanes.DataBind();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            planes = new Negocio.ControladorPlanes().dameTodos();
            repeaterPlanes.DataSource = planes;
            repeaterPlanes.DataBind();
        }
    }
}