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

namespace Escritorio.Curso
{
    public partial class CursoABM : Base.FormularioBase
    {
        #region VARIABLES
        private Entidades.Curso cursoActual;
        // bool est = true;
        #endregion

        #region PROPIEDADES

        public Entidades.Curso CursoActual
        {
            get { return cursoActual; }
            set { cursoActual = value; }
        }
        #endregion

        #region CONSTRUCTORES
        public CursoABM()
        {

            InitializeComponent();

            this.cmbBoxMaterias.DataSource = (new ControladorMaterias()).dameTodos();
            this.cmbBoxMaterias.ValueMember = "Id";
            this.cmbBoxMaterias.DisplayMember = "DescripcionMateria";

       


        }

        //Recibe el modo del formulario. Internamete debe setear a ModoForm en el modo enviado, este constructor
        //servirá para las altas. Y no recibe ID porque será un nuevo usuario.
        public CursoABM(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        //Recibe un entero que representa el ID del usuario y el Modo en que estará el Formulario
        public CursoABM(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            CursoActual = new ControladorCursos().dameUno(ID);
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
            this.txtID.Text = this.CursoActual.Id.ToString();
            this.txtAño.Text = this.CursoActual.AnioCalendario.ToString();
            this.txtCupo.Text = this.CursoActual.Cupo.ToString();
           
            this.cmbBoxMaterias.SelectedIndex = this.cmbBoxMaterias.FindString(CursoActual.Materia.DescripcionMateria);
        }

        public override void GuardarCambios()
        {
            MapearADatos();
            new ControladorCursos().save(CursoActual);
        }

        public override void MapearADatos()
        {
            //La propiedad State se setea dependiendo el Modo del Formulario
            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    {
                        CursoActual = new Entidades.Curso();
                        Entidades.Materia m = new Entidades.Materia();
                        

                        this.CursoActual.AnioCalendario = Convert.ToInt32(this.txtAño.Text);
                        this.CursoActual.Cupo = Convert.ToInt32(this.txtCupo.Text);
                        this.CursoActual.Materia = m;
                       
                        this.CursoActual.Materia.Id = Convert.ToInt32(this.cmbBoxMaterias.SelectedValue);
                        
                        this.CursoActual.State = Entidades.EntidadBase.States.New;


                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.CursoActual.AnioCalendario = Convert.ToInt32(this.txtAño.Text);
                        this.CursoActual.Cupo = Convert.ToInt32(this.txtCupo.Text);
                        this.CursoActual.Materia.Id = Convert.ToInt32(this.cmbBoxMaterias.SelectedValue);
                         this.CursoActual.State = Entidades.EntidadBase.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.CursoActual.State = Entidades.EntidadBase.States.Deleted;

                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.CursoActual.State = Entidades.EntidadBase.States.Unmodified;
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

        private void CursoABM_Load(object sender, EventArgs e)
        {
            if (ModoForm.Baja == this.Modo)
            {
                this.txtAño.Enabled = false;
                this.txtCupo.Enabled = false;
   
                this.cmbBoxMaterias.Enabled = false;
            }
        }
    }
}