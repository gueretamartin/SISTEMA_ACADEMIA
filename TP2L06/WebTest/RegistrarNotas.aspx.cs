using System;
using System.Collections.Generic;
using Negocio;
using Entidades;
using Entidades.CustomEntity;

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
                if (alumnosInscripciones.Count == 0)
                {
                    this.formActionPanel.Visible = false;
                    this.gridActionPanel.Visible = false;
                    this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                    this.lblMensaje.Visible = true;
                    this.lblMensaje.Text = "No hay alumnos registrados en ninguno de sus cursos";
                }
                else
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
            RespuestaServidor rs = this.ValidarCamposNulos();
            if (!rs.Error)
            {
                switch (formMode)
            {
                case FormModes.Modificacion:
                    this.cargarAlumnoInscripcion();
                    this.inscripcionActual.State = Entidades.EntidadBase.States.Modified;
                        rs = cia.Save(this.inscripcionActual);
                    break;
            }
                if (rs.Error)
                {
                    string errorStr = "";
                    foreach (string error in rs.ListaErrores)
                    {
                        errorStr += error + "</br>";
                    }
                    this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.lblMensaje.ForeColor = System.Drawing.Color.Green;
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
                    errorStr += error + "</br>";
                }
                this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                this.lblMensaje.Text = errorStr;
                this.lblMensaje.Visible = true;
                this.formActionPanel.Visible = true;
                this.formPanel.Visible = true;
            }
        }

        private RespuestaServidor ValidarCamposNulos()
        {
            int validaInt;
            RespuestaServidor rs = new RespuestaServidor();
            if (string.IsNullOrEmpty(this.txtNota.Text))
                rs.AgregarError("La nota es obligatoria");
            else if (int.TryParse(this.txtNota.Text, out validaInt))
                rs.AgregarError("La nota debe ser un entero válido");
            if (string.IsNullOrEmpty(this.txtCondicion.Text))
                rs.AgregarError("La condición es obligatoria");
            return rs;
        }

        public override void habilitarForm(bool enabled)
        {

            this.txtNota.Enabled = enabled;
            this.txtCondicion.Enabled = enabled;
        }

        public void guardarUsuario(Entidades.AlumnoInscripcion alinsc)
        {

            this.cia.Save(alinsc).MostrarMensajes();

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