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
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Entidades.Plan> planes = new List<Entidades.Plan>();
            planes = new Negocio.ControladorPlanes().dameTodos();
            
            if(!IsPostBack)
            {
                repeaterPlanes.DataSource = planes;
                repeaterPlanes.DataBind();
            }

        }
    }
}