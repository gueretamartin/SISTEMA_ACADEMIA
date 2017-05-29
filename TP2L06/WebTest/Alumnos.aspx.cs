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

    public partial class Alumnos : WebForm
    {
        ControladorUsuario cu = new ControladorUsuario();
        ControladorPersona cp = new ControladorPersona();
        ControladorPlanes cplan = new ControladorPlanes();

        Personas personaActual;

        public Personas PersonaActual { get { return personaActual; } set { personaActual = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO: AL INICIAR EL FORMULARIO VALIDO QUE TIPO DE PERSONA ES Y VALIDO SI DEJO ENTRAR O NO...
            //TODO: TODOS LOS WEB FORM DEBEN IMPLEMENTAR ESTE PEDAZO DE CODIGO PARA QUE SE VALIDEN LOS PERMISOS DE USUARIOS...
            TipoPersona tipo = (TipoPersona)Session["tipousuario"];
            if (tipo != null)
            {
                if (!ValidarPermisos.TienePermisosUsuario(tipo.Id, this.Page.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "")))
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

            personas = cp.dameTodosAlumnos();
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
            this.lblMensaje.Visible = false;
            RespuestaServidor rs = this.ValidarCamposNulos();
            if (!rs.Error)
            {
                switch (formMode)
                {
                    case FormModes.Alta:
                        this.personaActual = new Personas();
                        this.personaActual.State = Entidades.EntidadBase.States.New;
                        //DENTRO EL METODO CARGAR PERSON VALIDO LOS OBLIGATORIOS, Y DEVUELVO EXITO = FALSE SI FALTA ALGUNO.
                        this.cargarPersona(this.personaActual);
                        break;
                    case FormModes.Modificacion:
                        this.personaActual = new Personas();
                        this.personaActual.Id = this.IdSeleccionado;
                        this.personaActual.State = Entidades.EntidadBase.States.Modified;
                        this.cargarPersona(this.personaActual);
                        break;
                    case FormModes.Baja:
                        this.personaActual = new Entidades.Personas();
                        this.personaActual.Id = this.IdSeleccionado;
                        this.personaActual.State = Entidades.EntidadBase.States.Deleted;
                        
                        break;
                }
                rs = cp.save(this.personaActual);
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
                foreach(string error in rs.ListaErrores)
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
            if(!string.IsNullOrEmpty(this.txtEmail.Text) &&  !Util.ValidarEmails.esMailValido(txtEmail.Text))
            {
                rs.AgregarError("El email debe tener el formato ejemplo@hotmail.com");
            }
            if (string.IsNullOrEmpty(this.txtFecha.Text))
            {
                rs.AgregarError("Fecha obligatorio");
            }
            if(!DateTime.TryParse(this.txtFecha.Text, out validaFecha))
            {
                rs.AgregarError("La fecha debe ser del formato yyyy/MM/dd");
            }
            if (string.IsNullOrEmpty(this.listIdPlan.SelectedValue))
            {
                rs.AgregarError("Plan obligatorio");
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
            this.listIdPlan.DataSource = cplan.dameTodos(); ;
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

        public bool cargarPersona(Entidades.Personas persona)
        {
            if (String.IsNullOrEmpty(listIdPlan.SelectedValue))
            {
                this.lblMensaje.Text = "Debe seleccionar un plan para el alumno.";
                this.lblMensaje.Visible = true;
                return false;
            }
            else
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
                tp = ctp.dameUno(2);
                persona.TipoPersona = tp; //CREAR ENUMERADOR
                return true;
            }

        }

        override public void cargarForm(int id)
        {
            Personas alu = new Personas();
            alu = this.cp.dameUno(id);

            if (alu != null)
            {
                this.txtNombrePersona.Text = alu.Nombre;
                this.txtApellidoPersona.Text = alu.Apellido;
                this.txtDireccion.Text = alu.Direccion;
                this.txtEmail.Text = alu.Email;
                this.txtLegajo.Text = alu.Legajo.ToString();
                this.txtTelefono.Text = alu.Telefono.ToString();
                this.txtFecha.Text = alu.FechaNacimiento.ToString();
                this.txtId.Text = alu.Id.ToString();
                this.listIdPlan.SelectedValue = alu.Plan.Id.ToString();
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