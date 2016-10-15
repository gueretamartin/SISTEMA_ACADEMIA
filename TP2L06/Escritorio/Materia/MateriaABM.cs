using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Escritorio.Materia
{
    public partial class MateriaABM : Base.FormularioBase
    {
        #region VARIABLES
        private Entidades.Materia materiaActual;
        // bool est = true;
        #endregion

        #region PROPIEDADES

        public Entidades.Materia MateriaActual
        {
            get { return materiaActual; }
            set { materiaActual = value; }
        }
        #endregion

        #region CONSTRUCTORES
        public MateriaABM()
        {
            InitializeComponent();
        }

        //Recibe el modo del formulario. Internamete debe setear a ModoForm en el modo enviado, este constructor
        //servirá para las altas. Y no recibe ID porque será un nuevo usuario.
        public MateriaABM(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        //Recibe un entero que representa el ID del usuario y el Modo en que estará el Formulario
        public MateriaABM(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            MateriaActual = (new ControladorMaterias()).dameUno(ID);
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

        private void MateriaABM_Load(object sender, EventArgs e)
        {
            /*this.lstBoxPersonas.DataSource = (new ControladorPersona()).dameTodos();
            this.lstBoxPersonas.ValueMember = "Id";
            this.lstBoxPersonas.DisplayMember = "PersonaString";
            this.lstBoxPersonas.SelectedIndex = this.lstBoxPersonas.FindString(MateriaActual.Persona.PersonaString);

            if (ModoForm.Baja == this.Modo)
            {
                this.lstBoxPersonas.Visible = false;
                this.lblIdPersona.Visible = false;
            }*/
        }

    }
}
