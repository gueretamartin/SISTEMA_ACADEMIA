using Entidades;
using Entidades.CustomEntity;
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
using Util;

namespace Escritorio.Persona
{
    public partial class PersonaABM : Base.FormularioBase
    {
        

        #region VARIABLES
        private Entidades.Personas personaActual;
        // bool est = true;
        #endregion

        #region PROPIEDADES

        public Entidades.Personas PersonaActual
        {
            get { return personaActual; }
            set { personaActual = value; }
        }
        #endregion

        #region CONSTRUCTORES
        public PersonaABM()
        {
            InitializeComponent();
            this.cmbBoxPlanes.DataSource = (new ControladorPlanes()).dameTodos();
            this.cmbBoxTipoPersona.DataSource = (new ControladorTipoPersona()).dameTodos();
            this.cmbBoxPlanes.ValueMember = "Id";
            this.cmbBoxPlanes.DisplayMember = "DescripcionPlan";
            this.cmbBoxTipoPersona.ValueMember = "Id";
            this.cmbBoxTipoPersona.DisplayMember = "DescripcionTipo";
        }

        //Recibe el modo del formulario. Internamete debe setear a ModoForm en el modo enviado, este constructor
        //servirá para las altas. Y no recibe ID porque será un nuevo usuario.
        public PersonaABM(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        //Recibe un entero que representa el ID del usuario y el Modo en que estará el Formulario
        public PersonaABM(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            PersonaActual = new ControladorPersona().dameUno(ID);
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
            this.txtID.Text = this.PersonaActual.Id.ToString();
            this.txtNombre.Text  = this.PersonaActual.Nombre;
            this.txtApellido.Text = this.PersonaActual.Apellido;
            this.txtLegajo.Text = this.PersonaActual.Legajo.ToString();
            this.txtEmail.Text = this.PersonaActual.Email;
            this.txtDireccion.Text = this.PersonaActual.Direccion;
            this.txtTelefono.Text = this.PersonaActual.Telefono;
            this.dateFechaNac.Text = this.PersonaActual.FechaNacimiento.ToString();


            this.cmbBoxPlanes.SelectedIndex = this.cmbBoxPlanes.FindString(PersonaActual.Plan.DescripcionPlan);
            this.cmbBoxTipoPersona.SelectedIndex = this.cmbBoxTipoPersona.FindString(PersonaActual.TipoPersona.DescripcionTipo);



        }

        public override void GuardarCambios()
        {
            MapearADatos();
            RespuestaServidor sr = new ControladorPersona().save(PersonaActual);
            sr.MostrarMensajes();
        }

        public override void MapearADatos()
        {
            //La propiedad State se setea dependiendo el Modo del Formulario
            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    {
                        PersonaActual = new Entidades.Personas();
                        Personas p = new Personas();
                        this.PersonaActual.Nombre = this.txtNombre.Text;
                        this.PersonaActual.Apellido = this.txtApellido.Text;
                        this.PersonaActual.Direccion = this.txtDireccion.Text;
                        this.PersonaActual.Telefono = this.txtTelefono.Text;
                        this.PersonaActual.FechaNacimiento = Convert.ToDateTime(this.dateFechaNac.Text);
                        this.PersonaActual.Email = this.txtEmail.Text;
                        this.PersonaActual.TipoPersona = (new ControladorTipoPersona()).dameUno(Convert.ToInt32(this.cmbBoxTipoPersona.SelectedValue));
                        this.PersonaActual.Plan = (new ControladorPlanes()).dameUno(Convert.ToInt32(this.cmbBoxPlanes.SelectedValue));
                        this.PersonaActual.State = Entidades.EntidadBase.States.New;
                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.PersonaActual.Nombre = this.txtNombre.Text;
                        this.PersonaActual.Apellido = this.txtApellido.Text;
                        this.PersonaActual.Direccion = this.txtDireccion.Text;
                        this.PersonaActual.Telefono = this.txtTelefono.Text;
                        this.PersonaActual.FechaNacimiento = Convert.ToDateTime(this.dateFechaNac.Text);
                        this.PersonaActual.Email = this.txtEmail.Text;
                        this.PersonaActual.Legajo = Convert.ToInt32(this.txtLegajo.Text);
                        this.PersonaActual.TipoPersona = (new ControladorTipoPersona()).dameUno(Convert.ToInt32(this.cmbBoxTipoPersona.SelectedValue));
                        this.PersonaActual.Plan = (new ControladorPlanes()).dameUno(Convert.ToInt32(this.cmbBoxPlanes.SelectedValue));
                        this.PersonaActual.State = Entidades.EntidadBase.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.PersonaActual.State = Entidades.EntidadBase.States.Deleted;

                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.PersonaActual.State = Entidades.EntidadBase.States.Unmodified;
                        break;
                    }
            }
        }

        

        private bool validarPersonaExiste(int idP)
        {
            bool est = false;
            Personas p = new ControladorPersona().dameUno(idP);
            if (p.Apellido == null)
            {
                est = false;
            }
            else { est = true; }

            return est;
        }

        public override bool Validar()
        {
            Boolean estado = true;
            try
            {
                if (!(this.Modo == ModoForm.Baja))
                {
                    foreach (Control control in this.tableLayoutPanel1.Controls)
                    {
                        if (!(control == txtID))
                        {
                            if (control is TextBox && control.Text == String.Empty)
                            {
                                estado = false;
                            }
                        }
                    }
                    if (estado == false)
                    {
                        Notificar("Campos vacíos", "Existen campos sin completar.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Util.ValidarEmails.esMailValido(this.txtEmail.Text)))
                    {
                        estado = false;
                        Notificar("Mail no valido", "Mail no valido. Escribe una dirección de correo electrónico con el formato alguien@ejemplo.com.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return estado;
            }
            catch (Exception e)
            {
                Notificar("ERROR", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                estado = false;
            }
            return estado;
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
            if (Validar())
            { 
                GuardarCambios();
                Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

    


        #endregion

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PersonaABM_Load(object sender, EventArgs e)
        {
            if (ModoForm.Baja == this.Modo)
            {
                this.txtApellido.Enabled = false;
                this.txtDireccion.Enabled = false;
                this.txtEmail.Enabled = false;
                this.txtLegajo.Enabled = false;
                this.txtNombre.Enabled = false;
                this.txtTelefono.Enabled = false;
                this.cmbBoxPlanes.Enabled = false;
                this.cmbBoxTipoPersona.Enabled = false;
                this.dateFechaNac.Enabled = false;


            } 
        }

       
    }
}

