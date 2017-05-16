using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Escritorio.AlumnoInscripcion
{
    public partial class AlumnoInscripcionABM : Base.FormularioBase
    {
        #region VARIABLES
        private Entidades.AlumnoInscripcion alumnoInscripcionActual;
        //bool est = true;
        #endregion

        #region PROPIEDADES

        public Entidades.AlumnoInscripcion AlumnoInscripcionActual
        {
            get { return alumnoInscripcionActual; }
            set { alumnoInscripcionActual = value; }
        }
        #endregion

        #region CONSTRUCTORES
        public AlumnoInscripcionABM()
        {

            InitializeComponent();
            this.cmbBoxAlumnos.DataSource = (new ControladorPersona()).dameTodosAlumnos();
            this.cmbBoxAlumnos.ValueMember = "Id";
            this.cmbBoxAlumnos.DisplayMember = "PersonaString";

            this.cmbBoxCursos.DataSource = (new ControladorCursos()).dameTodos();
            this.cmbBoxCursos.ValueMember = "Id";
            this.cmbBoxCursos.DisplayMember = "Id";

        }

      
        public AlumnoInscripcionABM(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        //Recibe un entero que representa el ID del usuario y el Modo en que estará el Formulario
        public AlumnoInscripcionABM(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            AlumnoInscripcionActual = new ControladorInscripcionAlumno().dameUno(ID);
            MapearDeDatos();
            switch (modo)
            { //Dependiendo el modo, la ventana de carga como se setea
                case (ModoForm.Alta):
                    this.btnAceptar.Text = "Guardar";
                    break;
                case (ModoForm.Modificacion):
                    this.btnAceptar.Text = "Guardar";
                    break;
                case (ModoForm.Baja):
                    this.btnAceptar.Text = "Eliminar";
                    break;
                case (ModoForm.Consulta):
                    this.btnAceptar.Text = "Aceptar";
                    break;
            }
        }


        #endregion

        #region METODOS


        public override void MapearDeDatos()
        {
            this.txtID.Text = this.AlumnoInscripcionActual.Id.ToString();
            this.cmbBoxCursos.SelectedIndex = this.cmbBoxCursos.FindString(AlumnoInscripcionActual.Curso.Id.ToString());
            this.cmbBoxAlumnos.SelectedIndex = this.cmbBoxAlumnos.FindString(AlumnoInscripcionActual.Alumno.PersonaString);
            this.txtCondicion.Text = this.AlumnoInscripcionActual.Condicion;
            this.txtNota.Text = this.AlumnoInscripcionActual.Nota.ToString();
            
        }

        public override void GuardarCambios()
        {
            MapearADatos();
            new ControladorInscripcionAlumno().save(AlumnoInscripcionActual);
        }

        public override void MapearADatos()
        {
            //La propiedad State se setea dependiendo el Modo del Formulario
            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    {
                       AlumnoInscripcionActual = new Entidades.AlumnoInscripcion();
                       //Hay que ver si no hay que crear una instancia de alumno y curso

                        this.AlumnoInscripcionActual.Condicion = String.IsNullOrEmpty(this.txtCondicion.Text) ? "-------" : this.txtCondicion.Text;
                        this.AlumnoInscripcionActual.Nota = String.IsNullOrEmpty(this.txtNota.Text) ? 0 : Convert.ToInt32(this.txtNota.Text);
                        this.AlumnoInscripcionActual.Alumno = new ControladorPersona().dameUno(Convert.ToInt32(this.cmbBoxAlumnos.SelectedValue));
                        this.AlumnoInscripcionActual.Curso = new ControladorCursos().dameUno(Convert.ToInt32(this.cmbBoxCursos.SelectedValue));
                        this.AlumnoInscripcionActual.State = Entidades.EntidadBase.States.New;
                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.AlumnoInscripcionActual.Condicion = this.txtCondicion.Text;
                        this.AlumnoInscripcionActual.Nota = Convert.ToInt32(this.txtNota.Text);
                        this.AlumnoInscripcionActual.Alumno = new ControladorPersona().dameUno(Convert.ToInt32(this.cmbBoxAlumnos.SelectedValue));
                        this.AlumnoInscripcionActual.Curso = new ControladorCursos().dameUno(Convert.ToInt32(this.cmbBoxCursos.SelectedValue));
                        this.AlumnoInscripcionActual.State = Entidades.EntidadBase.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.AlumnoInscripcionActual.State = Entidades.EntidadBase.States.Deleted;

                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.AlumnoInscripcionActual.State = Entidades.EntidadBase.States.Unmodified;
                        break;
                    }
            }
        }

       


        public new void Notificar(string titulo, string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }


        public new void Notificar(string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            this.Notificar(this.Text, mensaje, botones, icono);
        }

        #endregion

        #region Eventos

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //if (Validar())
            //{
                GuardarCambios();
                Close();
            //}
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AlumnoInscripcionABM_Load(object sender, EventArgs e)
        {
            if (ModoForm.Baja == this.Modo)
            {
                //this.txtApellido.Enabled = false;
                //this.txtClave.Enabled = false;
                //this.txtConfirmarClave.Enabled = false;
                //this.txtNombre.Enabled = false;
                //this.txtUsuario.Enabled = false;
                //this.chkHabilitado.Enabled = false;
                //this.chkPass.Enabled = false;
                //this.cmbBoxPersonas.Enabled = false;
            }

        }


        #endregion
    }
}
