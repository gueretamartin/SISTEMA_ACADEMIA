using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;

namespace Escritorio.Comision
{
    public partial class ComisionABM : Base.FormularioBase
    {
        #region VARIABLES
        private Entidades.Comision comisionActual;
        //bool est = true;
        #endregion

        #region PROPIEDADES

        public Entidades.Comision ComisionActual
        {
            get { return comisionActual; }
            set { comisionActual = value; }
        }
        #endregion

        #region CONSTRUCTORES
        public ComisionABM()
        {
            InitializeComponent();
            this.cmbBoxPlanes.DataSource = (new ControladorPlanes()).dameTodos();
            this.cmbBoxPlanes.ValueMember = "Id";
            this.cmbBoxPlanes.DisplayMember = "DescripcionPlan";
        }

        public ComisionABM(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        //Recibe un entero que representa el ID del usuario y el Modo en que estará el Formulario
        public ComisionABM(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            ComisionActual = new ControladorComisiones().dameUno(ID);
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
            this.txtID.Text = this.ComisionActual.Id.ToString();
            this.txtAnio.Text = this.ComisionActual.AnioEspecialidad.ToString();
            this.txtDescripcion.Text = this.ComisionActual.DescripcionComision;
            this.cmbBoxPlanes.SelectedIndex = this.cmbBoxPlanes.FindString(ComisionActual.Plan.DescripcionPlan);
        }

        public override void GuardarCambios()
        {
            MapearADatos();
            new ControladorComisiones().save(ComisionActual);
        }

        public override void MapearADatos()
        {
            //La propiedad State se setea dependiendo el Modo del Formulario
            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    {
                        ComisionActual = new Entidades.Comision();
                        this.ComisionActual.AnioEspecialidad = Convert.ToInt32(this.txtAnio.Text);
                        this.ComisionActual.DescripcionComision = this.txtDescripcion.Text;
                        this.ComisionActual.Plan = (new ControladorPlanes()).dameUno(Convert.ToInt32(this.cmbBoxPlanes.SelectedValue));
                        this.ComisionActual.State = Entidades.EntidadBase.States.New;


                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.ComisionActual.AnioEspecialidad = Convert.ToInt32(this.txtAnio.Text);
                        this.ComisionActual.DescripcionComision = this.txtDescripcion.Text;
                        this.ComisionActual.Plan = (new ControladorPlanes()).dameUno(Convert.ToInt32(this.cmbBoxPlanes.SelectedValue));
                        this.ComisionActual.State = Entidades.EntidadBase.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.ComisionActual.State = Entidades.EntidadBase.States.Deleted;

                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.ComisionActual.State = Entidades.EntidadBase.States.Unmodified;
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

        #region EVENTOS
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar()) { 
            GuardarCambios();
            Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void ComisionABM_Load(object sender, EventArgs e)
        {

            if (ModoForm.Baja == this.Modo)
            {
                this.cmbBoxPlanes.Enabled = false;
                this.txtAnio.Enabled = false;
                this.txtDescripcion.Enabled = false;
            }           
        }
        #endregion
    }
}
