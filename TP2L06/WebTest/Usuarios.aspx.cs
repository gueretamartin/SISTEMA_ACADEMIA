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

    public partial class Usuarios : WebForm
    {
        ControladorUsuario cu = new ControladorUsuario();
        ControladorPersona cp = new ControladorPersona();
        Usuario usuarioActual;

        public Usuario UsuarioActual { get { return usuarioActual; } set { usuarioActual = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.BindGV();
                var dataList = cp.dameTodos();
               
                this.listIdPersona.DataSource = dataList;
               
                this.listIdPersona.DataValueField = "Id";
                this.listIdPersona.DataTextField = "Nombre";
                this.listIdPersona.DataBind();
            }
            this.formActionPanel.Visible = false;
            this.formPanel.Visible = false;
           
            

        }

        protected void BindGV()
        {
            List<PersonaMostrar> personasMostrar = new List<PersonaMostrar>();
            List<Usuario> usuarios = new List<Usuario>();

            usuarios = cu.dameTodos();
            foreach (Usuario user in usuarios)
            {
                PersonaMostrar p = new PersonaMostrar();
                p.NombreUsuario = user.NombreUsuario;
                p.Habilitado = user.Habilitado.ToString();
                p.NombrePersona = user.Persona.Nombre;
                p.ApellidoPersona = user.Persona.Apellido;
                p.Direccion = user.Persona.Direccion;
                p.Email = user.Persona.Email;
                p.Legajo = user.Persona.Legajo.ToString();
                p.Telefono = user.Persona.Telefono;
                p.Fecha = user.Persona.FechaNacimiento.ToString();
                p.Id = user.Id;
                p.IdPersona = user.Persona.Id;
                personasMostrar.Add(p);
            }

            this.gridView.DataSource = personasMostrar;
            this.gridView.DataBind();


        }

        private ControladorUsuario Cu
        {
            get
            {
                if (cu == null)
                {
                    cu = new ControladorUsuario();
                }
                return cu;
            }
        }

        private ControladorPersona Cp
        {
            get
            {
                if (cp == null)
                {
                    cp = new ControladorPersona();
                }
                return cp;
            }
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IdSeleccionado = (int)this.gridView.SelectedValue;
        }



        protected void LoadForm(int id)
        {

            this.UsuarioActual = this.cu.dameUno(id);
         

            this.txtNombreUsuario.Text = UsuarioActual.NombreUsuario;
            this.txtNombrePersona.Text = UsuarioActual.Persona.Nombre;
            this.txtApellidoPersona.Text = UsuarioActual.Persona.Apellido;
            this.txtDireccion.Text = UsuarioActual.Persona.Direccion;
            this.txtEmail.Text = UsuarioActual.Persona.Email;
            this.txtLegajo.Text = UsuarioActual.Persona.Legajo.ToString();
            this.txtTelefono.Text = UsuarioActual.Persona.Telefono.ToString();
            this.txtFecha.Text = UsuarioActual.Persona.FechaNacimiento.ToString();
            this.txtClave.Text = "";
            this.txtRepetirClave.Text = "";
            this.txtId.Text = UsuarioActual.Id.ToString();
            this.listIdPersona.SelectedValue = UsuarioActual.Persona.Id.ToString();


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


        class PersonaMostrar
        {
            internal int IdPersona;

            public int Id { get; set; }
            public string NombreUsuario { get; set; }
            public string Habilitado { get; set; }
            public string NombrePersona { get; set; }
            public string ApellidoPersona { get; set; }
            public string Direccion { get; set; }
            public string Email { get; set; }
            public string Legajo { get; set; }
            public string Telefono { get; set; }
            public string Fecha { get; set; }
        }

        protected void lbtnAceptar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;
            switch (formMode)
            {
                case FormModes.Alta:
                    this.usuarioActual = new Usuario();
                    this.usuarioActual.State = Entidades.EntidadBase.States.New;
                    this.cargarUsuario(this.usuarioActual);
                    this.guardarUsuario(this.usuarioActual);
                    break;
                case FormModes.Modificacion:
                    this.usuarioActual = new Usuario();
                    this.usuarioActual.Id = this.IdSeleccionado;
                    this.usuarioActual.State = Entidades.EntidadBase.States.Modified;
                    this.cargarUsuario(this.usuarioActual);
                    this.guardarUsuario(this.usuarioActual);
                    break;
                case FormModes.Baja:
                    this.borrarEntidad(this.IdSeleccionado);
                    break;

            }
            this.renovarForm();

            this.BindGV();
        }
        public override void renovarForm()
        {
            this.txtNombreUsuario.Text = String.Empty;
            this.txtNombrePersona.Text = String.Empty;
            this.txtApellidoPersona.Text = String.Empty;
            this.txtDireccion.Text = String.Empty;
            this.txtEmail.Text = String.Empty;
            this.txtLegajo.Text = String.Empty;
            this.txtTelefono.Text = String.Empty;
            this.txtFecha.Text = String.Empty;
            this.txtClave.Text = String.Empty;
            this.txtRepetirClave.Text = String.Empty;
            this.txtId.Text = String.Empty;
        }

        public override void habilitarForm(bool enabled)
        {
            this.txtNombreUsuario.Enabled = enabled;
            this.txtNombrePersona.Enabled = enabled;
            this.txtApellidoPersona.Enabled = enabled;
            this.txtDireccion.Enabled = enabled;
            this.txtEmail.Enabled = enabled;
            this.txtLegajo.Enabled = enabled;
            this.txtTelefono.Enabled = enabled;
            this.txtFecha.Enabled = enabled;
            this.txtClave.Enabled = enabled;
            this.txtRepetirClave.Enabled = enabled;
            this.txtId.Enabled = enabled;
            this.listIdPersona.Enabled = enabled;

        }

        public void guardarUsuario(Usuario usu)
        {
            this.cu.guardarUsuario(usu);
        }
        protected void lbtnEliminar_Click(object sender, EventArgs e)
        {
            if (entidadSeleccionada)
            {
                this.formActionPanel.Visible = true;
                this.formMode = FormModes.Baja;
                this.cargarForm(this.IdSeleccionado);
                this.txtRepetirClave.Text = "";
                this.txtClave.Text = "";
            }
        }
        public override void borrarEntidad(int id)
        {
            this.cu.eliminarUsuario(id);
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.formActionPanel.Visible = false;
            this.renovarForm();
        }
        public void cargarUsuario(Usuario usu)
        {
            // usu.Id = Convert.ToInt32(this.txtId.Text);
            usu.NombreUsuario = this.txtNombreUsuario.Text;
            usu.Habilitado = this.chkHabilitado.Checked;
            usu.Clave = this.txtClave.Text;
            Personas per = new Personas();
            per = this.cp.dameUno(Convert.ToInt32(listIdPersona.SelectedValue));
            usu.Persona = per;
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