using Entidades;
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
            this.lstBoxPlan.DataSource = (new ControladorPlanes()).dameTodos();
            this.lstBoxTipoPersona.DataSource = (new ControladorTipoPersona()).dameTodos();
            this.lstBoxPlan.ValueMember = "Id";
            this.lstBoxPlan.DisplayMember = "DescripcionPlan";
            this.lstBoxTipoPersona.ValueMember = "Id";
            this.lstBoxTipoPersona.DisplayMember = "DescripcionTipo";
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


            this.lstBoxPlan.SelectedIndex = this.lstBoxPlan.FindString(PersonaActual.Plan.DescripcionPlan);
            this.lstBoxTipoPersona.SelectedIndex = this.lstBoxTipoPersona.FindString(PersonaActual.TipoPersona.DescripcionTipo);



        }

        public override void GuardarCambios()
        {
            MapearADatos();
            new ControladorPersona().save(PersonaActual);
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
                        this.PersonaActual.Legajo = Convert.ToInt32(this.txtLegajo.Text);
                        this.PersonaActual.TipoPersona = (new ControladorTipoPersona()).dameUno(Convert.ToInt32(this.lstBoxTipoPersona.SelectedValue));
                        this.PersonaActual.Plan = (new ControladorPlanes()).dameUno(Convert.ToInt32(this.lstBoxPlan.SelectedValue));
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
                        this.PersonaActual.TipoPersona = (new ControladorTipoPersona()).dameUno(Convert.ToInt32(this.lstBoxTipoPersona.SelectedValue));
                        this.PersonaActual.Plan = (new ControladorPlanes()).dameUno(Convert.ToInt32(this.lstBoxPlan.SelectedValue));
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

        /*MapearDeDatos va a ser utilizado en cada formulario para copiar la información de las entidades a los controles del formulario (TextBox,
          ComboBox, etc) para mostrar la infromación de cada entidad 
         * MapearADatos se va a utilizar para pasar la información de los controles a una entidad para luego enviarla a las capas inferiores
         * GuardarCambios es el método que se encargará de invocar al método correspondiente de la capa de negocio según sea el ModoForm en que se
         encuentre el formulario
         * Validar será el método que devuelva si los datos son válidos para poder registrar los cambios realizados. */


        public new void Notificar(string titulo, string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }

        /*Notificar es el método que utilizaremos para unificar el mecanismo de avisos al usuario y en caso de tener que modificar la forma en que se
          realizan los avisos al usuario sólo se debe modificar este método, en lugar de tener que reemplazarlo en toda la aplicación.*/


        public new void Notificar(string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            this.Notificar(this.Text, mensaje, botones, icono);
        }

        #endregion

        #region Eventos

        private void btnAceptar_Click(object sender, EventArgs e)
        {
           
                GuardarCambios();
                Close();
            
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
                this.lstBoxPlan.Visible = false;
                this.lstBoxTipoPersona.Visible = false; 
            } 
        }

       
    }
}

