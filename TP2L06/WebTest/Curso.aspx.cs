using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;

namespace WebTest
{
    public partial class Curso : WebForm

    {
        ControladorCursos ce = new ControladorCursos();
        ControladorMaterias conte = new ControladorMaterias();
        Entidades.Curso planActual;

        public Entidades.Curso CursoActual { get { return planActual; } set { planActual = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindGV();
                var dataList = conte.dameTodos();
                this.listIdMateria.DataSource = dataList;
                this.listIdMateria.DataValueField = "Id";
                this.listIdMateria.DataTextField = "DescripcionMateria";
                this.listIdMateria.DataBind();
            }
            this.formActionPanel.Visible = false;
            this.formPanel.Visible = false;
        }

        protected void BindGV()
        {
            List<Entidades.Curso> planes = new List<Entidades.Curso>();

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
            switch (formMode)
            {
                case FormModes.Alta:
                    this.CursoActual = new Entidades.Curso();
                    this.planActual.State = Entidades.EntidadBase.States.New;
                    this.cargarCurso(this.planActual);
                    ce.save(this.planActual);
                    break;
                case FormModes.Modificacion:
                    this.planActual = new Entidades.Curso();
                    this.planActual.Id = this.IdSeleccionado;
                    this.planActual.State = Entidades.EntidadBase.States.Modified;
                    this.cargarCurso(this.planActual);
                    ce.save(this.planActual);
                    break;
                case FormModes.Baja:
                    this.planActual = new Entidades.Curso();
                    this.planActual.Id = this.IdSeleccionado;
                    this.planActual.State = Entidades.EntidadBase.States.Deleted;
                    ce.save(this.planActual);
                    break;

            }
            this.renovarForm();

            this.BindGV();
        }
        public override void renovarForm()
        {

            this.txtId.Text = String.Empty;
            this.txtAnioCalendario.Text = String.Empty;
            this.txtCupo.Text = String.Empty;
            this.txtDenominacion.Text = String.Empty;


        }

        public override void habilitarForm(bool enabled)
        {

            this.txtId.Enabled = enabled;
            this.txtAnioCalendario.Enabled = enabled;
            this.txtCupo.Enabled = enabled;
            this.txtDenominacion.Enabled = enabled;
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
            this.CursoActual = this.ce.dameUno(id);
            this.txtId.Text = CursoActual.Id.ToString();
            this.txtAnioCalendario.Text = this.CursoActual.AnioCalendario.ToString();
            this.txtDenominacion.Text = this.CursoActual.Denominacion;
        }

        public void cargarCurso(Entidades.Curso cur)
        {
            cur.AnioCalendario = Int32.Parse(this.txtAnioCalendario.Text);
            cur.Cupo = Int32.Parse(this.txtCupo.Text);
            cur.Denominacion = this.txtDenominacion.Text;
            Entidades.Materia mate = new Entidades.Materia();
            mate = this.conte.dameUno(Convert.ToInt32(listIdMateria.SelectedValue));
            cur.Materia = mate;
        }
    }
}