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
using System.Drawing;

namespace WebTest
{
    public partial class Plan : WebForm

    {
        ControladorPlanes cp = new ControladorPlanes();
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
                this.listEspecialidad.DataSource = dataList;
                this.listEspecialidad.DataValueField = "Id";
                this.listEspecialidad.DataTextField = "DescripcionEspecialidad";
                this.listEspecialidad.DataBind();
            }
            this.formActionPanel.Visible = false;
            this.formPanel.Visible = false;
        }

        protected void BindGV()
        {
            List<Entidades.Plan> planes = new List<Entidades.Plan>();
            
            planes = cp.dameTodos();
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
                this.cargarForm(this.IdSeleccionado);
                this.modoReadOnly(false);
                this.lblMensaje.Visible = false;
            }
        }

        protected void lbtnAceptar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;
            this.planActual = new Entidades.Plan();
            RespuestaServidor rs = this.ValidarCamposNulos();
            if (!rs.Error)
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
                rs = cp.save(this.planActual);
                if (rs.Error)
                {
                    this.lblMensaje.Text = rs.ListaErrores.FirstOrDefault();
                    this.lblMensaje.ForeColor = Color.Red;
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
            if ((this.formMode == FormModes.Alta || this.formMode == FormModes.Modificacion) && string.IsNullOrEmpty(this.listEspecialidad.SelectedValue))
            {
                rs.AgregarError("Seleccione una Especialidad");
            }
            if ((this.formMode == FormModes.Alta || this.formMode == FormModes.Modificacion) && string.IsNullOrEmpty(this.txtPlan.Text))
            {
                rs.AgregarError("Especifique la denominación del Plan");          
            }
            return rs;
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
            this.txtId.ReadOnly = true;
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
                this.modoReadOnly(false);
                this.lblMensaje.Visible = false;
            }
        }

        public void cargarPlan(Entidades.Plan esp)
        {
            esp.DescripcionPlan = this.txtPlan.Text;
            Entidades.Especialidad espe = new Entidades.Especialidad();

            espe = this.conte.dameUno(Convert.ToInt32(listEspecialidad.SelectedValue));
            esp.Especialidad = espe;
        }

        public override void cargarForm(int id)
        {
            Entidades.Plan plan = new Entidades.Plan();
            plan = cp.dameUno(id);
            if (plan != null)
            {
                this.txtId.Text = plan.Id.ToString();
                this.txtPlan.Text = plan.DescripcionPlan;
                this.listEspecialidad.SelectedValue = plan.Especialidad.Id.ToString();
            }
        }
    }
}