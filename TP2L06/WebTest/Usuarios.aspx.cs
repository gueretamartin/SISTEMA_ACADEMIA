using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using Util;
using System.Drawing;
using Entidades.CustomEntity;

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
            this.lblMensaje.Visible = false;
            this.IdSeleccionado = (int)this.gridView.SelectedValue;
        }
        
        protected void LbtnEditar_Click(object sender, EventArgs e)
        {
            if (entidadSeleccionada)
            {
                this.lblMensaje.Visible = false;
                this.formActionPanel.Visible = true;
                this.formMode = FormModes.Modificacion;
                this.formPanel.Visible = true;
                this.modoReadOnly(false);
                this.cargarForm(this.IdSeleccionado);

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
            this.lblMensaje.Visible = false;
            RespuestaServidor rs = this.ValidarCamposNulos();
            if (!rs.Error)
            {
                switch (formMode)
            {
                case FormModes.Alta:
                    this.UsuarioActual = new Usuario();
                    this.usuarioActual.State = Entidades.EntidadBase.States.New;
                    this.cargarUsuario(this.usuarioActual);
                    break;
                case FormModes.Modificacion:
                    this.usuarioActual = new Usuario();
                    this.usuarioActual.Id = this.IdSeleccionado;
                    this.usuarioActual.State = Entidades.EntidadBase.States.Modified;
                    this.cargarUsuario(this.usuarioActual);
                    break;
                case FormModes.Baja:
                    this.usuarioActual = new Usuario();
                    this.usuarioActual.Id = this.IdSeleccionado;
                    this.usuarioActual.State = Entidades.EntidadBase.States.Deleted;
                    break;
                }
                rs = cu.guardarUsuario(this.usuarioActual);
                if (rs.Error)
                {
                    this.lblMensaje.Text = rs.ListaErrores.FirstOrDefault();
                    this.lblMensaje.ForeColor = Color.Red;
                }
                else
                {
                    this.lblMensaje.ForeColor = Color.Green;
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
                    this.lblMensaje.ForeColor = Color.Red;
                    errorStr += error + "</br>";
                }
                this.lblMensaje.Text = errorStr;
                this.lblMensaje.Visible = true;
                this.formActionPanel.Visible = true;
                this.formPanel.Visible = true;
            }
        }

        private RespuestaServidor ValidarCamposNulos()
        {
            RespuestaServidor rs = new RespuestaServidor();
            int validaInt;
            DateTime validaFecha;
            if (string.IsNullOrEmpty(this.txtNombrePersona.Text))
            {
                rs.AgregarError("Nombre obligatorio");
            }
            if (string.IsNullOrEmpty(this.txtApellidoPersona.Text))
            {
                rs.AgregarError("Apellido obligatorio");
            }
            if (string.IsNullOrEmpty(this.txtLegajo.Text))
            {
                rs.AgregarError("Legajo obligatorio");
            }
            if (!int.TryParse(this.txtLegajo.Text, out validaInt))
            {
                rs.AgregarError("Legajo debe ser un número válido");
            }
            if (string.IsNullOrEmpty(this.txtFecha.Text))
            {
                rs.AgregarError("Fecha obligatorio");
            }
            if (!DateTime.TryParse(this.txtFecha.Text, out validaFecha))
            {
                rs.AgregarError("La fecha debe ser del formato yyyy/MM/dd");
            }
            if (string.IsNullOrEmpty(this.listIdPersona.SelectedValue))
            {
                rs.AgregarError("Persona obligatorio");
            }
            if (string.IsNullOrEmpty(this.txtClave.Text))
            {
                rs.AgregarError("Ingrese la Contraseña");
            }
            if (string.IsNullOrEmpty(this.txtRepetirClave.Text))
            {
                rs.AgregarError("Repita la contraseña");
            }
            if (this.txtClave.Text != this.txtRepetirClave.Text)
            {
                rs.AgregarError("Las contraseñas no coinciden");
            }

            return rs;
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

        protected void lbtnEliminar_Click(object sender, EventArgs e)
        {
            if (entidadSeleccionada)
            {
                this.lblMensaje.Visible = false;
                this.formPanel.Visible = true;
                this.formActionPanel.Visible = true;
                this.formMode = FormModes.Baja;
                this.cargarForm(this.IdSeleccionado);
                this.habilitarForm(true);
                this.modoReadOnly(true);
            }
        }

        private void modoReadOnly(bool enabled)
        {
            this.txtApellidoPersona.ReadOnly = enabled;
            this.txtNombrePersona.ReadOnly = enabled;
            this.txtApellidoPersona.ReadOnly = enabled;
            this.txtDireccion.ReadOnly = enabled;
            this.txtEmail.ReadOnly = enabled;
            this.txtLegajo.ReadOnly = enabled;
            this.txtTelefono.ReadOnly = enabled;
            this.txtFecha.ReadOnly = enabled;
            this.txtId.ReadOnly = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.lblMensaje.Visible = false;
            this.formActionPanel.Visible = false;
            this.renovarForm();
        }

        public void cargarUsuario(Usuario usu)
        {
          
            usu.NombreUsuario = this.txtNombreUsuario.Text;
            usu.Habilitado = this.chkHabilitado.Checked;
            usu.Clave = this.txtClave.Text;
            Personas per = new Personas();
            per = this.cp.dameUno(Convert.ToInt32(listIdPersona.SelectedValue));
            usu.Persona = per;
        }

        override public void cargarForm(int id)
        {
            Usuario usu = new Usuario();
            usu = this.cu.dameUno(id);

            Personas alu = new Personas();
            alu = usu.Persona;

            if (alu != null && usu != null)
            {
                this.txtNombrePersona.Text = alu.Nombre;
                this.txtApellidoPersona.Text = alu.Apellido;
                this.txtDireccion.Text = alu.Direccion;
                this.txtEmail.Text = alu.Email;
                this.txtLegajo.Text = alu.Legajo.ToString();
                this.txtTelefono.Text = alu.Telefono.ToString();
                this.txtFecha.Text = alu.FechaNacimiento.ToString();
                this.txtId.Text = alu.Id.ToString();
                this.listIdPersona.SelectedValue = usu.Persona.Id.ToString();
                this.txtNombreUsuario.Text = usu.NombreUsuario;
                this.chkHabilitado.Checked = usu.Habilitado;
                this.txtClave.Text = usu.Clave;
                this.txtRepetirClave.Text = usu.Clave;
            }
        }




        protected void lbtnNuevo_Click(object sender, EventArgs e)
        {
            {
                this.lblMensaje.Visible = false;
                this.formPanel.Visible = true;
                this.formActionPanel.Visible = true;
                this.renovarForm();
                this.formMode = FormModes.Alta;
                this.habilitarForm(true);
                this.modoReadOnly(false);
                this.txtId.Enabled = false;
            }
        }
    }
}