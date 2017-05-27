using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using Entidades.CustomEntity;

namespace WebTest
{
    public partial class Curso : WebForm

    {
        ControladorCursos ce = new ControladorCursos();
        ControladorMaterias conte = new ControladorMaterias();
        Entidades.Curso cursoActual;

        public Entidades.Curso CursoActual { get { return cursoActual; } set { cursoActual = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {

            this.lblMensaje.Visible = false;
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
                var dataList = conte.dameTodos();
                this.listIdMateria.DataSource = dataList;
                this.listIdMateria.DataValueField = "Id";
                this.listIdMateria.DataTextField = "DescripcionMateria";
                this.listIdMateria.DataBind();
            }
            this.formActionPanel.Visible = false;
            this.formPanel.Visible = false;
        }

        protected void BindGV()
        {
            List<Entidades.Curso> planes = new List<Entidades.Curso>();

            planes = ce.dameTodos();
            this.gridView.DataSource = planes;
            this.gridView.DataBind();
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IdSeleccionado = (int)this.gridView.SelectedValue;
        }

        protected void LbtnEditar_Click(object sender, EventArgs e)
        {
            if (entidadSeleccionada)
            {
                this.formActionPanel.Visible = true;
                this.formMode = FormModes.Modificacion;
                this.formPanel.Visible = true;
                this.cargarForm(this.IdSeleccionado);
                this.modoReadOnly(false);
                this.lblMensaje.Visible = false;
            }
        }

        protected void lbtnAceptar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;
            RespuestaServidor rs = this.ValidarCamposNulos();
            if(!rs.Error)
            {
                switch (formMode)
                {
                    case FormModes.Alta:
                        this.CursoActual = new Entidades.Curso();
                        this.cursoActual.State = Entidades.EntidadBase.States.New;
                        this.cargarCurso(this.cursoActual);
                        break;
                    case FormModes.Modificacion:
                        this.cursoActual = new Entidades.Curso();
                        this.cursoActual.Id = this.IdSeleccionado;
                        this.cursoActual.State = Entidades.EntidadBase.States.Modified;
                        this.cargarCurso(this.cursoActual);
                        break;
                    case FormModes.Baja:
                        this.cursoActual = new Entidades.Curso();
                        this.cursoActual.Id = this.IdSeleccionado;
                        this.cursoActual.State = Entidades.EntidadBase.States.Deleted;
                        break;

                }
                rs = ce.save(this.cursoActual);
                if (rs.Error)
                {
                    string errorStr = "";
                    foreach (string error in rs.ListaErrores)
                    {
                        errorStr += error + "</br>";
                    }
                    this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                    this.lblMensaje.Text = errorStr;
                }
                else
                {
                    this.lblMensaje.ForeColor = System.Drawing.Color.Green;
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
                    errorStr += error + "</br>";
                }
                this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                this.lblMensaje.Text = errorStr;
                this.lblMensaje.Visible = true;
                this.formActionPanel.Visible = true;
                this.formPanel.Visible = true;
            }
        }

        private RespuestaServidor ValidarCamposNulos()
        {
            int validaInt;
            RespuestaServidor rs = new RespuestaServidor();
            if (string.IsNullOrEmpty(this.txtDenominacion.Text))
                rs.AgregarError("Denominacion es obligatoria");
            if (string.IsNullOrEmpty(this.listIdMateria.SelectedValue))
                rs.AgregarError("Materia es obligatoria");
            if (string.IsNullOrEmpty(this.txtAnioCalendario.Text))
                rs.AgregarError("Año calendario es obligatorio");
            if(!int.TryParse(this.txtAnioCalendario.Text,out validaInt))
                rs.AgregarError("Año calendario debe ser un entero válido");
            if (string.IsNullOrEmpty(this.txtCupo.Text))
                rs.AgregarError("Cupo es obligatorio");
            if (!int.TryParse(this.txtCupo.Text, out validaInt))
                rs.AgregarError("Cupo debe ser un entero válido");
            return rs;
        }

        public override void renovarForm()
        {
            this.txtId.Text = String.Empty;
            this.txtAnioCalendario.Text = String.Empty;
            this.txtCupo.Text = String.Empty;
            this.txtDenominacion.Text = String.Empty;
        }

        public override void habilitarForm(bool enabled)
        {
            this.txtId.Enabled = enabled;
            this.txtAnioCalendario.Enabled = enabled;
            this.txtCupo.Enabled = enabled;
            this.txtDenominacion.Enabled = enabled;
        }

        public void modoReadOnly(bool readOnly)
        {
            this.txtId.ReadOnly = true;
            this.txtAnioCalendario.ReadOnly = readOnly;
            this.txtCupo.ReadOnly= readOnly;
            this.txtDenominacion.ReadOnly = readOnly;
        }


        protected void lbtnEliminar_Click(object sender, EventArgs e)
        {
            if (entidadSeleccionada)
            {
                this.formActionPanel.Visible = true;
                this.formPanel.Visible = true;
                this.formMode = FormModes.Baja;
                this.cargarForm(this.IdSeleccionado);
                this.modoReadOnly(true);
                this.lblMensaje.Visible = false;
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.lblMensaje.Visible = false;
            this.formActionPanel.Visible = false;
            this.renovarForm();
        }

        protected void lbtnNuevo_Click(object sender, EventArgs e)
        {
            {
                // this.formPanel.Visible = true;
                //  this.LoadForm(this.IdSeleccionado);
                this.lblMensaje.Visible = false;
                this.formPanel.Visible = true;
                this.formActionPanel.Visible = true;
                this.modoReadOnly(false);
                this.renovarForm();
                this.formMode = FormModes.Alta;
                this.habilitarForm(true);
                this.txtId.Enabled = false;
            }
        }

        public override void cargarForm(int id)
        {
            this.CursoActual = this.ce.dameUno(id);
            this.txtId.Text = CursoActual.Id.ToString();
            this.txtAnioCalendario.Text = this.CursoActual.AnioCalendario.ToString();
            this.txtDenominacion.Text = this.CursoActual.Denominacion;
            this.txtCupo.Text = this.CursoActual.Cupo.ToString();
            this.listIdMateria.SelectedValue = CursoActual.Materia.Id.ToString();
        }

        public void cargarCurso(Entidades.Curso cur)
        {
            cur.AnioCalendario = Int32.Parse(this.txtAnioCalendario.Text);
            cur.Cupo = Int32.Parse(this.txtCupo.Text);
            cur.Denominacion = this.txtDenominacion.Text;
            Entidades.Materia mate = new Entidades.Materia();
            mate = this.conte.dameUno(Convert.ToInt32(listIdMateria.SelectedValue));
            cur.Materia = mate;
        }
    }
}