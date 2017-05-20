using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using Util;

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
                if (!ValidarPermisos.TienePermisosUsuario(tipo.Id, "Materia"))
                    Response.Redirect("~/Permisos.aspx");
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
            switch (formMode)
            {
                case FormModes.Alta:
                    this.MateriaActual = new Entidades.Materia();
                    this.materiaActual.State = Entidades.EntidadBase.States.New;
                    this.cargarMateria(this.materiaActual);
                    ce.save(this.materiaActual);
                    break;
                case FormModes.Modificacion:
                    this.materiaActual = new Entidades.Materia();
                    this.materiaActual.Id = this.IdSeleccionado;
                    this.materiaActual.State = Entidades.EntidadBase.States.Modified;
                    this.cargarMateria(this.materiaActual);
                    ce.save(this.materiaActual);
                    break;
                case FormModes.Baja:
                    this.materiaActual = new Entidades.Materia();
                    this.materiaActual.Id = this.IdSeleccionado;
                    this.materiaActual.State = Entidades.EntidadBase.States.Deleted;
                    ce.save(this.materiaActual);
                    break;

            }
            this.renovarForm();

            this.BindGV();
        }
        public override void renovarForm()
        {

            this.txtId.Text = String.Empty;
            this.txtMateria.Text = String.Empty;

        }

        public override void habilitarForm(bool enabled)
        {

            this.txtId.Enabled = enabled;
            this.txtMateria.Enabled = enabled;
            this.txtHorasTotales.Enabled = enabled;
            this.txtHsSemanales.Enabled = enabled;
            this.listIdPlan.Enabled = enabled;

        }
        protected void lbtnEliminar_Click(object sender, EventArgs e)
        {
            if (entidadSeleccionada)
            {
                this.formActionPanel.Visible = true;
                this.formMode = FormModes.Baja;
                this.cargarForm(this.IdSeleccionado);

            }
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
            this.MateriaActual = this.ce.dameUno(id);
            this.txtId.Text = MateriaActual.Id.ToString();
            this.txtMateria.Text = MateriaActual.DescripcionMateria;
            
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


    }
}