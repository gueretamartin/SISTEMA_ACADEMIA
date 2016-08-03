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
// using Util; VER LO DE VALIDAR MAIL MAS ABAJO
namespace Escritorio.Usuario
{
    public partial class UsuarioABM : Escritorio.Base.FormularioBase
    {


        private Entidades.Usuario _usuarioActual;
        bool est = true;


        public Entidades.Usuario UsuarioActual
        {
            get { return _usuarioActual; }
            set { _usuarioActual = value; }
        }

        #region CONSTRUCTORES
        public UsuarioABM()
        {
            InitializeComponent();
        }

        public UsuarioABM(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        public UsuarioABM(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            UsuarioActual = new ControladorUsuario().dameUno(ID);
            MapearDeDatos();
            switch (modo)
            {
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
       
        public virtual void MapearDeDatos()
        {
            this.txtID.Text = this.UsuarioActual.Id.ToString();
            this.chkHabilitado.Checked = this.UsuarioActual.Habilitado;
            this.txtNombre.Text = this.UsuarioActual.Nombre;
            this.txtApellido.Text = this.UsuarioActual.Apellido;
            this.txtClave.Text = this.UsuarioActual.Clave;
            this.txtEmail.Text = this.UsuarioActual.Email;
            this.txtUsuario.Text = this.UsuarioActual.NombreUsuario;
        }

        public virtual void GuardarCambios()
        {
            MapearADatos();
            new ControladorUsuario().guardarUsuario(UsuarioActual);
        }

        public virtual void MapearADatos()
        {

            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    {
                        UsuarioActual = new Entidades.Usuario();
                        this.UsuarioActual.Habilitado = this.chkHabilitado.Checked;
                        this.UsuarioActual.Nombre = this.txtNombre.Text;
                        this.UsuarioActual.Apellido = this.txtApellido.Text;
                        this.UsuarioActual.Clave = this.txtClave.Text;
                        this.UsuarioActual.Email = this.txtEmail.Text;
                        this.UsuarioActual.NombreUsuario = this.txtUsuario.Text;
                        this.UsuarioActual.State = Entidades.EntidadBase.States.New;
                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.UsuarioActual.Habilitado = this.chkHabilitado.Checked;
                        this.UsuarioActual.Nombre = this.txtNombre.Text;
                        this.UsuarioActual.Apellido = this.txtApellido.Text;
                        this.UsuarioActual.Clave = this.txtClave.Text;
                        this.UsuarioActual.Email = this.txtEmail.Text;
                        this.UsuarioActual.NombreUsuario = this.txtUsuario.Text;
                        this.UsuarioActual.State = Entidades.EntidadBase.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.UsuarioActual.State = Entidades.EntidadBase.States.Deleted;
                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.UsuarioActual.State = Entidades.EntidadBase.States.Unmodified;
                        break;
                    }
            }
        }

        public virtual bool Validar()
        {
            Boolean estado = true;
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
               /*else if (!(Util.ValidarEMails.esMailValido(this.txtEmail.Text)))
                {
                    estado = false;
                    Notificar("Mail no valido", "Mail no valido. Escribe una dirección de correo electrónico con el formato alguien@ejemplo.com.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/
                else if (!(String.Equals(this.txtClave.Text, this.txtConfirmarClave.Text)))
                {
                    estado = false;
                    Notificar("Contraseñas incorrectas", "Las contraseñas no coinciden.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!((this.txtClave.Text).Length >= 8))
                {
                    estado = false;
                    Notificar("Clave corta", "La contraseña debe contener al menos 8 caracteres.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return estado;
        }

        /*MapearDeDatos va a ser utilizado en cada formulario para copiar la información de las entidades a los controles del formulario (TextBox,
          ComboBox, etc) para mostrar la infromación de cada entidad 
         * MapearADatos se va a utilizar para pasar la información de los controles a una entidad para luego enviarla a las capas inferiores
         * GuardarCambios es el método que se encargará de invocar al método correspondiente de la capa de negocio según sea el ModoForm en que se
         encuentre el formulario
         * Validar será el método que devuelva si los datos son válidos para poder registrar los cambios realizados. */


        public void Notificar(string titulo, string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }

        /*Notificar es el método que utilizaremos para unificar el mecanismo de avisos al usuario y en caso de tener que modificar la forma en que se
          realizan los avisos al usuario sólo se debe modificar este método, en lugar de tener que reemplazarlo en toda la aplicación.*/


        public void Notificar(string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
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

        private void chkPass_CheckedChanged(object sender, EventArgs e)
        {
            if (est == true)
            {
                est = false;
                this.txtClave.PasswordChar = '\0';
                this.txtConfirmarClave.PasswordChar = '\0';
            }
            else
            {
                est = true;
                this.txtClave.PasswordChar = '•';
                this.txtConfirmarClave.PasswordChar = '•';
            }
        }

        #endregion


    }
}
