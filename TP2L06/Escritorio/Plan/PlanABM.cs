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

namespace Escritorio.Plan
{
    public partial class PlanABM : Base.FormularioBase
    {
        #region VARIABLES
        private Entidades.Plan planActual;
        //bool est = true;
        #endregion

        #region PROPIEDADES

        public Entidades.Plan PlanActual
        {
            get { return planActual; }
            set { planActual = value; }
        }
        #endregion

        #region CONSTRUCTORES
        public PlanABM()
        {

            InitializeComponent();
            this.cmbBoxEspecialidades.DataSource = (new ControladorEspecialidad()).dameTodos();
            this.cmbBoxEspecialidades.ValueMember = "Id";
            this.cmbBoxEspecialidades.DisplayMember = "DescripcionEspecialidad";


        }

        //Recibe el modo del formulario. Internamete debe setear a ModoForm en el modo enviado, este constructor
        //servirá para las altas. Y no recibe ID porque será un nuevo usuario.
        public PlanABM(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        //Recibe un entero que representa el ID del usuario y el Modo en que estará el Formulario
        public PlanABM(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            PlanActual = new ControladorPlanes().dameUno(ID);
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
            this.txtID.Text = this.PlanActual.Id.ToString();
            this.txtPlan.Text = this.PlanActual.DescripcionPlan;
            this.cmbBoxEspecialidades.SelectedIndex = this.cmbBoxEspecialidades.FindString(PlanActual.Especialidad.DescripcionEspecialidad);
        }

        public override void GuardarCambios()
        {
            MapearADatos();
            new ControladorPlanes().save(PlanActual);
        }

        public override void MapearADatos()
        {
            //La propiedad State se setea dependiendo el Modo del Formulario
            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    {
                        PlanActual = new Entidades.Plan();
                        Entidades.Especialidad e = new Entidades.Especialidad();

                        this.PlanActual.DescripcionPlan = this.txtPlan.Text;
                        this.PlanActual.Especialidad = e;
                        this.PlanActual.Especialidad.Id = Convert.ToInt32(this.cmbBoxEspecialidades.SelectedValue);
                        this.PlanActual.State = Entidades.EntidadBase.States.New;


                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.PlanActual.DescripcionPlan = this.txtPlan.Text;
                        this.PlanActual.Especialidad.Id = Convert.ToInt32(this.cmbBoxEspecialidades.SelectedValue);
                        this.PlanActual.State = Entidades.EntidadBase.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.PlanActual.State = Entidades.EntidadBase.States.Deleted;

                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.PlanActual.State = Entidades.EntidadBase.States.Unmodified;
                        break;
                    }
            }
        }

        public override bool Validar()
        {
            Boolean estado = true;
            try
            {
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
                }
                return estado;
            }
            catch (Exception e)
            {
                Notificar("ERROR", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                estado = false;
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

        #endregion

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PlanABM_Load(object sender, EventArgs e)
        {
            if (ModoForm.Baja == this.Modo)
            {
                this.txtPlan.Enabled = false;
                this.cmbBoxEspecialidades.Enabled = false;
            }
        }
    }
}
