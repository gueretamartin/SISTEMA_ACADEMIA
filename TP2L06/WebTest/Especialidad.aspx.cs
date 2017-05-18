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
    public partial class Especialidad : WebForm

    {
        ControladorEspecialidad ce = new ControladorEspecialidad();
        Entidades.Especialidad especialidadActual;

        public Entidades.Especialidad EspecialidadActual { get { return especialidadActual; } set { especialidadActual = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindGV();
            }
            this.formActionPanel.Visible = false;
            this.formPanel.Visible = false;
        }

        protected void BindGV()
        {
            List<Entidades.Especialidad> especialidades = new List<Entidades.Especialidad>();
            
            especialidades = ce.dameTodos();
            this.gridView.DataSource = especialidades;
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
                this.LoadForm(this.IdSeleccionado);
            }
        }

        protected void lbtnAceptar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;
            switch (formMode)
            {
                case FormModes.Alta:
                    this.EspecialidadActual = new Entidades.Especialidad();
                    this.especialidadActual.State = Entidades.EntidadBase.States.New;
                    this.cargarEspecialidad(this.especialidadActual);
                    ce.save(this.especialidadActual);
                    break;
                case FormModes.Modificacion:
                    this.especialidadActual = new Entidades.Especialidad();
                    this.especialidadActual.Id = this.IdSeleccionado;
                    this.especialidadActual.State = Entidades.EntidadBase.States.Modified;
                    this.cargarEspecialidad(this.especialidadActual);
                    ce.save(this.especialidadActual);
                    break;
                case FormModes.Baja:
                    this.especialidadActual = new Entidades.Especialidad();
                    this.especialidadActual.Id = this.IdSeleccionado;
                    this.especialidadActual.State = Entidades.EntidadBase.States.Deleted;
                    ce.save(this.especialidadActual);
                    break;

            }
            this.renovarForm();

            this.BindGV();
        }
        public override void renovarForm()
        {
         
            this.txtId.Text = String.Empty;
            this.txtEspecialidad.Text = String.Empty;

        }

        public override void habilitarForm(bool enabled)
        {
           
            this.txtId.Enabled = enabled;
            this.txtEspecialidad.Enabled = enabled;

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
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.formActionPanel.Visible = false;
            this.renovarForm();
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

        protected void LoadForm(int id)
        {
            this.EspecialidadActual = this.ce.dameUno(id);
            this.txtId.Text = EspecialidadActual.Id.ToString();
            this.txtEspecialidad.Text = EspecialidadActual.DescripcionEspecialidad;
        }

        public void cargarEspecialidad(Entidades.Especialidad esp)
        {
            esp.DescripcionEspecialidad = this.txtEspecialidad.Text;
        }


    }
}