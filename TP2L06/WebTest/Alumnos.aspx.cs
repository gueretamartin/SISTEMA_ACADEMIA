using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using Util;

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
                if (!ValidarPermisos.TienePermisosUsuario(tipo.Id, "Alumnos"))
                    Response.Redirect("~/Permisos.aspx");
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

            personas = cp.dameTodos();
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



        protected void LoadForm(int id)
        {

            this.PersonaActual = this.cp.dameUno(id);
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
            switch (formMode)
            {
                case FormModes.Alta:
                    this.personaActual = new Personas();
                    this.personaActual.State = Entidades.EntidadBase.States.New;
                    //DENTRO EL METODO CARGAR PERSON VALIDO LOS OBLIGATORIOS, Y DEVUELVO EXITO = FALSE SI FALTA ALGUNO.
                    bool exito = this.cargarPersona(this.personaActual);
                    if (exito)
                    {
                        cp.save(this.personaActual);
                    }
                    break;
                case FormModes.Modificacion:
                    this.personaActual = new Personas();
                    this.personaActual.Id = this.IdSeleccionado;
                    this.personaActual.State = Entidades.EntidadBase.States.Modified;
                    this.cargarPersona(this.personaActual);
                    cp.save(this.personaActual);
                    break;
                case FormModes.Baja:
                    //this.planActual = new Entidades.Plan();
                    //this.planActual.Id = this.IdSeleccionado;
                    //this.planActual.State = Entidades.EntidadBase.States.Deleted;
                    //ce.save(this.planActual);

                    this.personaActual.State = Entidades.EntidadBase.States.Deleted;

                    cp.save(this.personaActual);
                    break;
            }
            this.renovarForm();

            this.BindGV();
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
            if (entidadSeleccionada)
            {
                this.formActionPanel.Visible = true;
                this.formMode = FormModes.Baja;
                this.cargarForm(this.IdSeleccionado);

            }
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
        public bool cargarPersona(Entidades.Personas persona)
        {
            if (String.IsNullOrEmpty(listIdPlan.SelectedValue))
            {
                this.txtMensaje.Text = "Debe seleccionar un plan para el alumno.";
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



            //this.txtNombrePersona.Enabled = enabled;
            //this.txtApellidoPersona.Enabled = enabled;
            //this.txtDireccion.Enabled = enabled;
            //this.txtEmail.Enabled = enabled;
            //this.txtLegajo.Enabled = enabled;
            //this.txtTelefono.Enabled = enabled;
            //this.txtFecha.Enabled = enabled;
            //this.txtId.Enabled = enabled;
            //this.listIdPlan.Enabled = enabled;
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