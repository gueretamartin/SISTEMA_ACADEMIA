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

namespace Escritorio.Especialidad
{
    public partial class EspecialidadABM : Base.FormularioBase
    {
        #region VARIABLES
        private Entidades.Especialidad especialidadActual;
        // bool est = true;
        #endregion

        #region PROPIEDADES

        public Entidades.Especialidad EspecialidadActual
        {
            get { return especialidadActual; }
            set { especialidadActual = value; }
        }
        #endregion

        #region CONSTRUCTORES
        public EspecialidadABM()
        {

            InitializeComponent();
            


        }

        //Recibe el modo del formulario. Internamete debe setear a ModoForm en el modo enviado, este constructor
        //servirá para las altas. Y no recibe ID porque será un nuevo usuario.
        public EspecialidadABM(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        //Recibe un entero que representa el ID del usuario y el Modo en que estará el Formulario
        public EspecialidadABM(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            EspecialidadActual = new ControladorEspecialidad().dameUno(ID);
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
            this.txtID.Text = this.EspecialidadActual.Id.ToString();
            this.txtDescripcion.Text = this.EspecialidadActual.DescripcionEspecialidad;
        }

        public override void GuardarCambios()
        {
            MapearADatos();
            new ControladorEspecialidad().save(EspecialidadActual);
        }

        public override void MapearADatos()
        {
            //La propiedad State se setea dependiendo el Modo del Formulario
            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    {
                        EspecialidadActual = new Entidades.Especialidad();
                        
                        this.EspecialidadActual.DescripcionEspecialidad = this.txtDescripcion.Text;
                        this.EspecialidadActual.State = Entidades.EntidadBase.States.New;


                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.EspecialidadActual.DescripcionEspecialidad = this.txtDescripcion.Text;
                        this.EspecialidadActual.State = Entidades.EntidadBase.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.EspecialidadActual.State = Entidades.EntidadBase.States.Deleted;

                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.EspecialidadActual.State = Entidades.EntidadBase.States.Unmodified;
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
            if (ModoForm.Baja == this.Modo)
            {
                this.txtDescripcion.Enabled = false;
            }
        }

      
    }
}
