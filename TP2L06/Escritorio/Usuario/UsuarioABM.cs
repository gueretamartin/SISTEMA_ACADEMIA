﻿using Entidades;
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
    public partial class UsuarioABM : Base.FormularioBase
    {

        #region VARIABLES
            private Entidades.Usuario _usuarioActual;
            bool est = true;
        #endregion

        #region PROPIEDADES

        public Entidades.Usuario UsuarioActual
        {
            get { return _usuarioActual; }
            set { _usuarioActual = value; }
        }
        #endregion

        #region CONSTRUCTORES
        public UsuarioABM()
        {
           
            InitializeComponent();
            this.cmbBoxPersonas.DataSource = (new ControladorPersona()).dameTodos();
            this.cmbBoxPersonas.ValueMember = "Id";
            this.cmbBoxPersonas.DisplayMember = "PersonaString";

          
        }

        //Recibe el modo del formulario. Internamete debe setear a ModoForm en el modo enviado, este constructor
        //servirá para las altas. Y no recibe ID porque será un nuevo usuario.
        public UsuarioABM(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        //Recibe un entero que representa el ID del usuario y el Modo en que estará el Formulario
        public UsuarioABM(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            UsuarioActual = new ControladorUsuario().dameUno(ID);
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
       
        
        public  override void  MapearDeDatos()
        {
            this.txtID.Text = this.UsuarioActual.Id.ToString();
            this.chkHabilitado.Checked = this.UsuarioActual.Habilitado;
            this.txtNombre.Text = this.UsuarioActual.Persona.Nombre;
            this.txtApellido.Text = this.UsuarioActual.Persona.Apellido;
            this.txtClave.Text = this.UsuarioActual.Clave;
            this.txtEmail.Text = this.UsuarioActual.Persona.Email;
            this.txtUsuario.Text = this.UsuarioActual.NombreUsuario;
            this.cmbBoxPersonas.SelectedIndex = this.cmbBoxPersonas.FindString(UsuarioActual.Persona.PersonaString);
        }

        public override void GuardarCambios()
        {
            MapearADatos();
            new ControladorUsuario().guardarUsuario(UsuarioActual);
        }

        public override void MapearADatos()
        {
            //La propiedad State se setea dependiendo el Modo del Formulario
            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    {
                        UsuarioActual = new Entidades.Usuario();
                        Personas p = new Personas();

                        this.UsuarioActual.Habilitado = this.chkHabilitado.Checked;
                        this.UsuarioActual.Clave = this.txtClave.Text;
                        this.UsuarioActual.NombreUsuario = this.txtUsuario.Text;
                        this.UsuarioActual.Persona = p;
                        this.UsuarioActual.Persona.Id = Convert.ToInt32(this.cmbBoxPersonas.SelectedValue); 
                        this.UsuarioActual.State = Entidades.EntidadBase.States.New;
                       
                      
                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.UsuarioActual.Habilitado = this.chkHabilitado.Checked;
                        this.UsuarioActual.Clave = this.txtClave.Text;
                        this.UsuarioActual.NombreUsuario = this.txtUsuario.Text;
                        this.UsuarioActual.Persona.Id = Convert.ToInt32(this.cmbBoxPersonas.SelectedValue);
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

        public override bool Validar()
        {
            int idP = Convert.ToInt32(cmbBoxPersonas.SelectedValue);
            
            Boolean estado = true;
            if (est == true)
            {
                if (!(this.Modo == ModoForm.Baja))
                {
                    foreach (Control control in this.tableLayoutPanel1.Controls)
                    {
                        if (!(control == txtID) && !(control == txtNombre) && !(control == txtApellido) && !(control == txtEmail))
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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UsuarioABM_Load(object sender, EventArgs e)
        {
            if (ModoForm.Baja == this.Modo)
            {
                this.txtApellido.Enabled = false;
                this.txtClave.Enabled = false;
                this.txtConfirmarClave.Enabled = false;
                this.txtNombre.Enabled = false;
                this.txtUsuario.Enabled = false;
                this.chkHabilitado.Enabled = false;
                this.chkPass.Enabled = false;
                this.cmbBoxPersonas.Enabled = false;
            }

        }
    }
}
