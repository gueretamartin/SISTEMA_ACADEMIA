using System;
using System.Collections.Generic;
using Negocio;
using System.Windows.Forms;
using Entidades;

namespace WebTest
{
    public partial class AlumnoInscripcion : WebForm

    {
        ControladorInscripcionAlumno ce = new ControladorInscripcionAlumno();
        ControladorCursos conte = new ControladorCursos();
        Entidades.AlumnoInscripcion _AlumnoInscripcionActual;
        
        public Entidades.AlumnoInscripcion AlumnoInscripcionActual { get { return _AlumnoInscripcionActual; } set { _AlumnoInscripcionActual = value; } }
        List<Entidades.AlumnoInscripcion> alumnoinsc { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindGV();
               
            }
            this.formActionPanel.Visible = false;
            this.formPanel.Visible = false;
        }

        protected void BindGV()
        {
          alumnoinsc = new List<Entidades.AlumnoInscripcion>();

            if((Entidades.Personas)Session["Persona"] != null)
            {
                alumnoinsc = ce.dameTodosAlumno(((Entidades.Personas)Session["Persona"]).Id);
                this.gridView.DataSource = alumnoinsc;
                this.gridView.DataBind();
            }
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IdSeleccionado = (int)this.gridView.SelectedValue;
        }

        protected void lbtnAceptar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            this.AlumnoInscripcionActual = new Entidades.AlumnoInscripcion();
            switch (formMode)
            {
                case FormModes.Alta:
                    this.AlumnoInscripcionActual.State = Entidades.EntidadBase.States.New;
                    this.cargarAlumnoInscripcion(this.AlumnoInscripcionActual);
                    
                    break;
                case FormModes.Baja:
                    this.AlumnoInscripcionActual.Id = this.IdSeleccionado;
                    this.AlumnoInscripcionActual.State = Entidades.EntidadBase.States.Deleted;
                    break;
            }
            ce.Save(this.AlumnoInscripcionActual).MostrarMensajes();
            this.renovarForm();
            this.BindGV();
        }
        
        public override void renovarForm()
        {
            this.txtId.Text = String.Empty;
        }

        public override void habilitarForm(bool enabled)
        {

            this.txtId.Enabled = enabled;
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
                if (Session["idPlan"] != null && Session["Persona"] != null)
                {
                    var dataList = conte.dameTodosPlanNoAlumno((int)Session["idPlan"],((Entidades.Personas)Session["Persona"]).Id);
                    this.listIdCurso.DataSource = dataList;
                    this.listIdCurso.DataValueField = "Id";
                    this.listIdCurso.DataTextField = "Denominacion";
                    this.listIdCurso.DataBind();
                }
                this.formPanel.Visible = true;
                this.formActionPanel.Visible = true;
                this.renovarForm();
                this.formMode = FormModes.Alta;
                this.habilitarForm(true);
                this.txtId.Enabled = false;
            }
        }
        
        public void cargarAlumnoInscripcion(Entidades.AlumnoInscripcion alInscr)
        {
            alInscr.Curso = new ControladorCursos().dameUno(Convert.ToInt32(listIdCurso.SelectedValue));
            alInscr.Alumno = (Entidades.Personas)Session["Persona"];
            alInscr.Condicion = "Inscripto";
            alInscr.Nota = 0;
        }
    }
}