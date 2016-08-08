using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Web
{
    public partial class Usuarios : System.Web.UI.Page
    {
        ControladorUsuario cu;

        private ControladorUsuario Cu
        {
            get { return cu; }
            set { cu = value; }
        }

        private void LoadGrid()
        {
            this.gridView.DataSource = cu.dameTodos();
            this.gridView.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
    }
}