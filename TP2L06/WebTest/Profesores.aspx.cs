using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using Util;
using Entidades.CustomEntity;
using System.Drawing;

namespace WebTest
{

    public partial class Profesores : WebForm
    {
        ControladorUsuario cu = new ControladorUsuario();
        ControladorPersona cp = new ControladorPersona();
        ControladorPlanes cplan = new ControladorPlanes();

        Personas personaActual;

        public Personas PersonaActual { get { return personaActual; } set { personaActual = value; } }
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
                var dataList = cplan.dameTodos();

                this.listIdPlan.DataSource = dataList;

                this.listIdPlan.DataValueField = "Id";
                this.listIdPlan.DataTextField = "DescripcionPlan";
                this.listIdPlan.DataBind();
            }
            this.formActionPanel.Visible = false;
            this.formPanel.Visible = false;



        }

        protected void BindGV()
        {
            List<PersonaMostrar> personasMostrar = new List<PersonaMostrar>();
            List<Personas> personas = new List<Personas>();

            personas = cp.dameTodosProfesores();
            foreach (Personas per in personas)
            {
                PersonaMostrar p = new PersonaMostrar();
                ControladorPlanes cplan = new ControladorPlanes();

                p.NombrePersona = per.Nombre;
                p.ApellidoPersona = per.Apellido;
                p.Direccion = per.Direccion;
                p.Email = per.Email;
                p.Legajo = per.Legajo.ToString();
                p.Telefono = per.Telefono;
                p.Fecha = per.FechaNacimiento.ToString();
                p.Id = per.Id;
                p.Plan = per.Plan.DescripcionPlan;
                personasMostrar.Add(p);
            }
            this.gridView.DataSource = personasMostrar;
            this.gridView.DataBind();
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



        public override void cargarForm(int id)
        {

            this.PersonaActual = this.cp.dameUno(id);
            this.txtId.Text = PersonaActual.Id.ToString();
            this.txtNombrePersona.Text = PersonaActual.Nombre;
            this.txtApellidoPersona.Text = PersonaActual.Apellido;
            this.txtDireccion.Text = PersonaActual.Direccion;
            this.txtEmail.Text = PersonaActual.Email;
            this.txtLegajo.Text = PersonaActual.Legajo.ToString();
            this.txtTelefono.Text = PersonaActual.Telefono.ToString();
            this.txtFecha.Text = PersonaActual.FechaNacimiento.ToString();
            this.listIdPlan.SelectedValue = PersonaActual.Plan.Id.ToString();
        }

        protected void LbtnEditar_Click(object sender, EventArgs e)
        {
            this.lblMensaje.Text = String.Empty;
            if (entidadSeleccionada)
            {
                this.formActionPanel.Visible = true;
                this.formMode = FormModes.Modificacion;
                this.modoReadOnly(false);
                this.formPanel.Visible = true;
                this.cargarForm(this.IdSeleccionado);
            }
        }


        class PersonaMostrar
        {
            internal int IdPersona;
            public int Id { get; set; }
            public string NombrePersona { get; set; }
            public string ApellidoPersona { get; set; }
            public string Direccion { get; set; }
            public string Email { get; set; }
            public string Legajo { get; set; }
            public string Telefono { get; set; }
            public string Fecha { get; set; }
            public string Plan { get; set; }

        }

        protected void lbtnAceptar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;
            this.personaActual = new Entidades.Personas();
            RespuestaServidor rs = this.ValidarCamposNulos();
            if (!rs.Error)
            {
                switch (formMode)
                {
                    case FormModes.Alta:
                        this.personaActual.State = Entidades.EntidadBase.States.New;
                        this.cargarPersona(this.personaActual);
                        break;
                    case FormModes.Modificacion:
                        this.personaActual.Id = this.IdSeleccionado;
                        this.personaActual.State = Entidades.EntidadBase.States.Modified;
                        this.cargarPersona(this.personaActual);
                        break;
                    case FormModes.Baja:
                        this.personaActual.Id = this.IdSeleccionado;
                        this.personaActual.State = Entidades.EntidadBase.States.Deleted;
                        break;
                }
                rs = cp.save(this.personaActual);
                if(rs.Error)
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
            if ((this.formMode == FormModes.Alta || this.formMode == FormModes.Modificacion) && string.IsNullOrEmpty(this.txtNombrePersona.Text))
            {
                rs.AgregarError("Ingrese el nombre");
            }
            if ((this.formMode == FormModes.Alta || this.formMode == FormModes.Modificacion) && string.IsNullOrEmpty(this.txtApellidoPersona.Text))
            {
                rs.AgregarError("Seleccione el apellido");
            }
            if ((this.formMode == FormModes.Alta || this.formMode == FormModes.Modificacion) && string.IsNullOrEmpty(this.txtLegajo.Text))
            {
                rs.AgregarError("Ingrese el legajo");
            }
            if ((this.formMode == FormModes.Alta || this.formMode == FormModes.Modificacion) && string.IsNullOrEmpty(this.txtTelefono.Text))
            {
                rs.AgregarError("Ingrese un teléfono");
            }
            if ((this.formMode == FormModes.Alta || this.formMode == FormModes.Modificacion) && !string.IsNullOrEmpty(this.txtEmail.Text) && !Util.ValidarEmails.esMailValido(txtEmail.Text))
            {
                rs.AgregarError("El email debe tener el formato ejemplo@hotmail.com");
            }
            if ((this.formMode == FormModes.Alta || this.formMode == FormModes.Modificacion) && string.IsNullOrEmpty(this.txtFecha.Text))
            {
                rs.AgregarError("Ingrese la fecha de nacimiento");
            }
            if ((this.formMode == FormModes.Alta || this.formMode == FormModes.Modificacion) && string.IsNullOrEmpty(this.txtDireccion.Text))
            {
                rs.AgregarError("Ingrese la dirección");
            }
            if ((this.formMode == FormModes.Alta || this.formMode == FormModes.Modificacion) && string.IsNullOrEmpty(this.listIdPlan.SelectedValue))
            {
                rs.AgregarError("Seleccione un Plan");
            }
            return rs;
        }

        public override void renovarForm()
        {

            this.txtNombrePersona.Text = String.Empty;
            this.txtApellidoPersona.Text = String.Empty;
            this.txtDireccion.Text = String.Empty;
            this.txtEmail.Text = String.Empty;
            this.txtLegajo.Text = String.Empty;
            this.txtTelefono.Text = String.Empty;
            this.txtFecha.Text = String.Empty;
            this.txtId.Text = String.Empty;
        }

        public override void habilitarForm(bool enabled)
        {
            this.txtNombrePersona.Enabled = enabled;
            this.txtApellidoPersona.Enabled = enabled;
            this.txtDireccion.Enabled = enabled;
            this.txtEmail.Enabled = enabled;
            this.txtLegajo.Enabled = enabled;
            this.txtTelefono.Enabled = enabled;
            this.txtFecha.Enabled = enabled;
            this.txtId.Enabled = enabled;
            this.listIdPlan.Enabled = enabled;
        }

        public void guardarUsuario(Usuario usu)
        {
            this.cu.guardarUsuario(usu);
        }

        protected void lbtnEliminar_Click(object sender, EventArgs e)
        {
            this.lblMensaje.Text = String.Empty;
            if (entidadSeleccionada)
            {
                this.formActionPanel.Visible = true;
                this.formMode = FormModes.Baja;
                this.cargarForm(this.IdSeleccionado);
                this.lblMensaje.Visible = false;
                this.formPanel.Visible = true;
                this.habilitarForm(true);
                this.modoReadOnly(true);
            }
        }

        private void modoReadOnly(bool enabled)
        {
            this.txtNombrePersona.ReadOnly = enabled;
            this.txtApellidoPersona.ReadOnly = enabled;
            this.txtDireccion.ReadOnly = enabled;
            this.txtEmail.ReadOnly = enabled;
            this.txtLegajo.ReadOnly = enabled;
            this.txtTelefono.ReadOnly = enabled;
            this.txtFecha.ReadOnly = enabled;
            this.txtId.ReadOnly = true;
        }
        
        public bool cargarPersona(Entidades.Personas persona)
        {
                persona.Plan = this.cplan.dameUno(Convert.ToInt32(listIdPlan.SelectedValue));
                persona.Nombre = this.txtNombrePersona.Text;
                persona.Apellido = this.txtApellidoPersona.Text;
                persona.Direccion = this.txtDireccion.Text;
                persona.Email = this.txtEmail.Text;
                persona.Legajo = this.txtLegajo.Text == "" ? 0 : Int32.Parse(this.txtLegajo.Text);
                persona.Telefono = this.txtTelefono.Text;
                persona.FechaNacimiento = DateTime.Parse(this.txtFecha.Text);
                TipoPersona tp = new TipoPersona();
                ControladorTipoPersona ctp = new ControladorTipoPersona();
                tp = ctp.dameUno(1);

                persona.TipoPersona = tp;
                return true;
        }

        protected void lbtnNuevo_Click(object sender, EventArgs e)
        {
            {
                this.formPanel.Visible = true;
                this.formActionPanel.Visible = true;
                this.renovarForm();
                this.formMode = FormModes.Alta;
                this.habilitarForm(true);
                this.modoReadOnly(false);
                this.lblMensaje.Visible = false;
            }
        }

        protected void lbtnCancelar_Click(object sender, EventArgs e)
        {
            this.lblMensaje.Text = String.Empty;
            this.formActionPanel.Visible = false;
            this.renovarForm();
        }
    }
}