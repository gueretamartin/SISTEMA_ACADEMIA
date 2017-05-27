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
    public partial class Materias : WebForm

    {
        ControladorMaterias ce = new ControladorMaterias();
        ControladorPlanes cp = new ControladorPlanes();
        Entidades.Materia materiaActual;

        public Entidades.Materia MateriaActual { get { return materiaActual; } set { materiaActual = value; } }

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
                var dataList = cp.dameTodos();

                this.listIdPlan.DataSource = dataList;
                this.listIdPlan.DataValueField = "Id";
                this.listIdPlan.DataTextField = "DescripcionPlan";
                this.listIdPlan.DataBind();
            }
            this.formActionPanel.Visible = false;
            this.formPanel.Visible = false;
        }

        protected void BindGV()
        {
            List<Entidades.Materia> materias = new List<Entidades.Materia>();

            materias = ce.dameTodos();
            this.gridView.DataSource = materias;
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
                        this.MateriaActual = new Entidades.Materia();
                        this.materiaActual.State = Entidades.EntidadBase.States.New;
                        this.cargarMateria(this.materiaActual);
                        break;
                    case FormModes.Modificacion:
                        this.materiaActual = new Entidades.Materia();
                        this.materiaActual.Id = this.IdSeleccionado;
                        this.materiaActual.State = Entidades.EntidadBase.States.Modified;
                        this.cargarMateria(this.materiaActual);
                        break;
                    case FormModes.Baja:
                        this.materiaActual = new Entidades.Materia();
                        this.materiaActual.Id = this.IdSeleccionado;
                        this.materiaActual.State = Entidades.EntidadBase.States.Deleted;
                        break;

                }

                rs = ce.save(this.materiaActual);
                if(rs.Error)
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
            int validaInt;
            int validaInt2;
            if (string.IsNullOrEmpty(this.txtMateria.Text))
            {
                rs.AgregarError("Descripción obligatoria");
            }
            if (string.IsNullOrEmpty(this.txtHsSemanales.Text))
            {
                rs.AgregarError("Horas Semanales obligatorio");
            }
            if (string.IsNullOrEmpty(this.txtHorasTotales.Text))
            {
                rs.AgregarError("Horas Totales obligatorio");
            }
            if (!int.TryParse(this.txtHorasTotales.Text, out validaInt))
            {
                rs.AgregarError("Las Horas totales deben ser un número válido");
            }
            if (!int.TryParse(this.txtHorasTotales.Text, out validaInt2))
            {
                rs.AgregarError("Las Horas totales deben ser un número válido");
            }
            if (string.IsNullOrEmpty(this.listIdPlan.SelectedValue))
            {
                rs.AgregarError("Plan obligatorio");
            }
            return rs;
        }
        public override void renovarForm()
        {

            this.txtId.Text = String.Empty;
            this.txtMateria.Text = String.Empty;
            this.listIdPlan.DataSource = cp.dameTodos();
        }

        public override void habilitarForm(bool enabled)
        {

            this.txtId.Enabled = enabled;
            this.txtMateria.Enabled = enabled;
            this.txtHorasTotales.Enabled = enabled;
            this.txtHsSemanales.Enabled = enabled;

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

        private void modoReadOnly(bool ro)
        {
            this.txtHorasTotales.ReadOnly = ro;
            this.txtHsSemanales.ReadOnly = ro;
            this.txtMateria.ReadOnly = ro;
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
                this.modoReadOnly(false);
                this.formMode = FormModes.Alta;
                this.habilitarForm(true);
                this.txtId.Enabled = false;
            }
        }
        

        public void cargarMateria(Entidades.Materia mat)
        {
            Entidades.Plan plan = new Entidades.Plan();
            mat.DescripcionMateria = this.txtMateria.Text;
            mat.HorasSemanales = Int32.Parse(this.txtHsSemanales.Text);
            mat.HorasTotales = Int32.Parse(this.txtHorasTotales.Text);
            plan = this.cp.dameUno(Convert.ToInt32(listIdPlan.SelectedValue));
            mat.Plan = plan;
        }

        override public void cargarForm(int id)
        {
            Entidades.Materia mat = new Entidades.Materia();
            mat = this.ce.dameUno(id);

            if (mat != null)
            {
                this.txtMateria.Text = mat.DescripcionMateria.ToString();
                this.txtHorasTotales.Text = mat.HorasTotales.ToString();
                this.txtHsSemanales.Text = mat.HorasSemanales.ToString();
                this.txtId.Text = mat.Id.ToString();
                this.listIdPlan.SelectedValue = mat.Plan.Id.ToString();
            }
        }
        
    }
}