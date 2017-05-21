using System;
using System.Collections.Generic;
using Negocio;
using Entidades;

namespace WebTest
{

    public partial class Inscripciones : WebForm
    {
        ControladorInscripcionAlumno cia = new ControladorInscripcionAlumno();

        Entidades.AlumnoInscripcion inscripcionActual;

        public Entidades.AlumnoInscripcion InscripcionActual { get { return inscripcionActual; } set { inscripcionActual = value; } }
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
            List<Entidades.AlumnoInscripcion> alumnosInscripciones = new List<Entidades.AlumnoInscripcion>();
            if (Session["Persona"] != null){
                alumnosInscripciones = cia.dameTodosDocente(((Personas)Session["Persona"]).Id);
                this.gridView.DataSource = alumnosInscripciones;
                this.gridView.DataBind();
            }
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IdSeleccionado = (int)this.gridView.SelectedValue;
        }

        protected void LoadForm(int id)
        {

            this.InscripcionActual = this.cia.dameUno(id);
            this.txtId.Text = InscripcionActual.Id.ToString();
            this.txtNombreAlumno.Text = InscripcionActual.NombreAlumno;
            this.txtCurso.Text = InscripcionActual.NombreCurso;
            this.txtNota.Text = InscripcionActual.Nota > 0 ? InscripcionActual.Nota.ToString() : "";
            this.txtCondicion.Text = InscripcionActual.Condicion;
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
                case FormModes.Modificacion:
                    this.cargarAlumnoInscripcion();
                    this.inscripcionActual.State = Entidades.EntidadBase.States.Modified;
                    cia.Save(this.inscripcionActual).MostrarMensajes();
                    break;
            }
            this.renovarForm();
            this.BindGV();
        }

        public override void habilitarForm(bool enabled)
        {

            this.txtNota.Enabled = enabled;
            this.txtCondicion.Enabled = enabled;
        }

        public void guardarUsuario(Entidades.AlumnoInscripcion alinsc)
        {
            this.cia.Save(alinsc);
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
        //public override void borrarEntidad(int id)
        //{
        //    this.cp.
        //}
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.formActionPanel.Visible = false;
            this.renovarForm();
        }
        public void cargarAlumnoInscripcion()
        {
           this.inscripcionActual = cia.dameUno(this.IdSeleccionado);
            this.inscripcionActual.Nota = this.txtNota.Text == "" ? 0 : Int32.Parse(this.txtNota.Text);
            this.inscripcionActual.Condicion = this.txtCondicion.Text;

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
    }
}