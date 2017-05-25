using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using Entidades.CustomEntity;
using Util;

namespace WebTest
{
    public partial class Plan : WebForm

    {
        ControladorPlanes ce = new ControladorPlanes();
        ControladorEspecialidad conte = new ControladorEspecialidad();
        Entidades.Plan planActual;

        public Entidades.Plan PlanActual { get { return planActual; } set { planActual = value; } }

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
                var dataList = conte.dameTodos();
                this.listIdPlan.DataSource = dataList;
                this.listIdPlan.DataValueField = "Id";
                this.listIdPlan.DataTextField = "DescripcionEspecialidad";
                this.listIdPlan.DataBind();
            }
            this.formActionPanel.Visible = false;
            this.formPanel.Visible = false;
        }

        protected void BindGV()
        {
            List<Entidades.Plan> planes = new List<Entidades.Plan>();
            
            planes = ce.dameTodos();
            this.gridView.DataSource = planes;
            this.gridView.DataBind();
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IdSeleccionado = (int)this.gridView.SelectedValue;
        }

        protected void LbtnEditar_Click(object sender, EventArgs e)
        {
            if (entidadSeleccionada)
            {
                this.formActionPanel.Visible = true;
                this.formMode = FormModes.Modificacion;
                this.formPanel.Visible = true;
                this.LoadForm(this.IdSeleccionado);
            }
        }

        protected void lbtnAceptar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;
            this.planActual = new Entidades.Plan();
            if (validarPagina())
            {
                switch (formMode)
                {
                    case FormModes.Alta:
                        this.planActual.State = Entidades.EntidadBase.States.New;
                        this.cargarPlan(this.planActual);
                        break;
                    case FormModes.Modificacion:
                        this.planActual.Id = this.IdSeleccionado;
                        this.planActual.State = Entidades.EntidadBase.States.Modified;
                        this.cargarPlan(this.planActual);
                        break;
                    case FormModes.Baja:
                        this.planActual.Id = this.IdSeleccionado;
                        this.planActual.State = Entidades.EntidadBase.States.Deleted;
                        break;
                }
                RespuestaServidor rs = ce.save(this.planActual);
                rs.MostrarMensajes();
                this.renovarForm();
                this.BindGV();
            }
            else
            { this.formActionPanel.Visible = true;
                this.formPanel.Visible = true;
            }
        }

        private bool validarPagina()
        {
            if ((this.formMode == FormModes.Alta || this.formMode == FormModes.Modificacion) && string.IsNullOrEmpty(this.listIdPlan.SelectedValue))
            {
                Mensajeria.MostrarAlerta("Seleccione una Especialidad");
                return false;
            }

            else if ((this.formMode == FormModes.Alta || this.formMode == FormModes.Modificacion) && string.IsNullOrEmpty(this.txtPlan.Text))
            {
                Mensajeria.MostrarAlerta("Especifique la denominación del Plan");
                return false;
            }
            return true;
        }

        public override void renovarForm()
        {
         
            this.txtId.Text = String.Empty;
            this.txtPlan.Text = String.Empty;
            this.lblError.Text = "";

        }

        public override void habilitarForm(bool enabled)
        {
           
            this.txtId.Enabled = enabled;
            this.txtPlan.Enabled = enabled;

        }
        protected void lbtnEliminar_Click(object sender, EventArgs e)
        {
            if (entidadSeleccionada)
            {
                this.formActionPanel.Visible = true;
                this.formMode = FormModes.Baja;
                this.cargarForm(this.IdSeleccionado);
                this.lblMensaje.Visible = false;
                this.formPanel.Visible = true;
                this.habilitarForm(true);
                this.modoReadOnly(true);

            }
        }

        private void modoReadOnly(bool enabled)
        {
            this.txtId.ReadOnly = enabled;
            this.txtPlan.ReadOnly = enabled;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.formActionPanel.Visible = false;
            this.renovarForm();
        }

        protected void lbtnNuevo_Click(object sender, EventArgs e)
        {
            {
                // this.formPanel.Visible = true;
                //  this.LoadForm(this.IdSeleccionado);
                this.formPanel.Visible = true;
                this.formActionPanel.Visible = true;
                this.renovarForm();
                this.formMode = FormModes.Alta;
                this.habilitarForm(true);
                this.txtId.Enabled = false;
            }
        }

        protected void LoadForm(int id)
        {
            this.PlanActual = this.ce.dameUno(id);
            this.txtId.Text = PlanActual.Id.ToString();
            this.txtPlan.Text = PlanActual.DescripcionPlan;
            this.listIdPlan.SelectedValue = PlanActual.Especialidad.Id.ToString();
        }

        public void cargarPlan(Entidades.Plan esp)
        {
            esp.DescripcionPlan = this.txtPlan.Text;
            Entidades.Especialidad espe = new Entidades.Especialidad();

            espe = this.conte.dameUno(Convert.ToInt32(listIdPlan.SelectedValue));
            esp.Especialidad = espe;
        }


    }
}