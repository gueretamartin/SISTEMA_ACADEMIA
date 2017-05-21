using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTest
{
    public partial class ReportePlanes : System.Web.UI.Page
    {
        List<Entidades.Plan> planes = new List<Entidades.Plan>();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            planes = new Negocio.ControladorPlanes().dameTodos();
            
            if(!IsPostBack)
            {
                //Entidades.Plan planTodos = new Entidades.Plan();
                ////planTodos.DescripcionEspecialidad = "Todos";
                //planes.Add(planTodos);
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