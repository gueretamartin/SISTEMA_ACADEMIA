using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using Util;
using Entidades.CustomEntity;
using System.Drawing;


namespace WebTest
{
    public partial class Especialidad : WebForm

    {
        ControladorEspecialidad ce = new ControladorEspecialidad();
        Entidades.Especialidad especialidadActual;

        public Entidades.Especialidad EspecialidadActual { get { return especialidadActual; } set { especialidadActual = value; } }

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
                this.BindGV();
            }
            this.formActionPanel.Visible = false;
            this.formPanel.Visible = false;
        }

        protected void BindGV()
        {
            List<Entidades.Especialidad> especialidades = new List<Entidades.Especialidad>();
            
            especialidades = ce.dameTodos();
            this.gridView.DataSource = especialidades;
            this.gridView.DataBind();
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblMensaje.Visible = false;
            this.IdSeleccionado = (int)this.gridView.SelectedValue;
        }

        protected void LbtnEditar_Click(object sender, EventArgs e)
        {
            if (entidadSeleccionada)
            {
                this.lblMensaje.Visible = false;
                this.formActionPanel.Visible = true;
                this.formMode = FormModes.Modificacion;
                this.formPanel.Visible = true;
                this.modoReadOnly(false);
                this.cargarForm(this.IdSeleccionado);
            }
        }

        protected void lbtnAceptar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;
            this.lblMensaje.Visible = false;
            RespuestaServidor rs = this.ValidarCamposNulos();
            if (!rs.Error)
            {
                switch (formMode)
            {
                case FormModes.Alta:
                    this.EspecialidadActual = new Entidades.Especialidad();
                    this.especialidadActual.State = Entidades.EntidadBase.States.New;
                    this.cargarEspecialidad(this.especialidadActual);
                    break;
                case FormModes.Modificacion:
                    this.especialidadActual = new Entidades.Especialidad();
                    this.especialidadActual.Id = this.IdSeleccionado;
                    this.especialidadActual.State = Entidades.EntidadBase.States.Modified;
                    this.cargarEspecialidad(this.especialidadActual);
                    break;
                case FormModes.Baja:
                    this.especialidadActual = new Entidades.Especialidad();
                    this.especialidadActual.Id = this.IdSeleccionado;
                    this.especialidadActual.State = Entidades.EntidadBase.States.Deleted;
                    break;
            }
                rs = ce.save(this.especialidadActual);
                if (rs.Error)
                {
                    string errorStr = "";
                    foreach (string error in rs.ListaErrores)
                    {
                        this.lblMensaje.ForeColor = Color.Red;
                        errorStr += error + "</br>";
                    }
                    this.lblMensaje.Text = errorStr;
                }
                else
                {
                    this.lblMensaje.ForeColor = Color.Green;
                    this.lblMensaje.Text = rs.Mensaje;

                }
                this.lblMensaje.Visible = true;
                this.renovarForm();
                this.BindGV();
            }
            else
            {
                string errorStr = "";
                foreach (string error in rs.ListaErrores)
                {
                    this.lblMensaje.ForeColor = Color.Red;
                    errorStr += error + "</br>";
                }
                this.lblMensaje.Text = errorStr;
                this.lblMensaje.Visible = true;
                this.formActionPanel.Visible = true;
                this.formPanel.Visible = true;
            }
        }

        private RespuestaServidor ValidarCamposNulos()
        {
            RespuestaServidor rs = new RespuestaServidor();
            if (string.IsNullOrEmpty(this.txtEspecialidad.Text))
            {
                rs.AgregarError("Descripción obligatoria");
            }
            return rs;
        }

        public override void renovarForm()
        {
         
            this.txtId.Text = String.Empty;
            this.txtEspecialidad.Text = String.Empty;

        }

        public override void habilitarForm(bool enabled)
        {
           
            this.txtId.Enabled = enabled;
            this.txtEspecialidad.Enabled = enabled;

        }
        protected void lbtnEliminar_Click(object sender, EventArgs e)
        {
            if (entidadSeleccionada)
            {
                this.lblMensaje.Visible = false;
                this.formActionPanel.Visible = true;
                this.formPanel.Visible = true;
                this.formMode = FormModes.Baja;
                this.cargarForm(this.IdSeleccionado);
                this.habilitarForm(true);
                this.modoReadOnly(true);

            }
        }

        private void modoReadOnly(bool enabled)
        {
            this.txtEspecialidad.ReadOnly = enabled;
            this.txtId.ReadOnly = true;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.lblMensaje.Visible = false;
            this.formActionPanel.Visible = false;
            this.renovarForm();
        }

        protected void lbtnNuevo_Click(object sender, EventArgs e)
        {
            {
                this.lblMensaje.Visible = false;
                this.formPanel.Visible = true;
                this.formActionPanel.Visible = true;
                this.renovarForm();
                this.formMode = FormModes.Alta;
                this.modoReadOnly(false);
                this.habilitarForm(true);
                this.txtId.Enabled = false;
            }
        }
        
        public void cargarEspecialidad(Entidades.Especialidad esp)
        {
            esp.DescripcionEspecialidad = this.txtEspecialidad.Text;
        }

        override public void cargarForm(int id)
        {
            Entidades.Especialidad esp = new Entidades.Especialidad();
            esp = this.ce.dameUno(id);

            if (esp != null)
            {
                this.txtEspecialidad.Text = esp.DescripcionEspecialidad;
                this.txtId.Text = esp.Id.ToString();
            }
        }



    }
}