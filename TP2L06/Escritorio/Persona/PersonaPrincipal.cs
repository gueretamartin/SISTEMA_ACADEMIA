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

namespace Escritorio.Persona
{
    public partial class PersonaPrincipal : Form
    {
        #region Constructores
        public PersonaPrincipal()
        {
            InitializeComponent();
            this.dgvPersonas.AutoGenerateColumns = false;

        }
        #endregion

        #region Metodos

        public void Listar()
        {
            ControladorPersona cp = new ControladorPersona();
            this.dgvPersonas.DataSource = cp.dameTodos();  //asignaremos el resultado a la propiedad DataSource de la grilla
        }

        #endregion

        #region Eventos

        private void Personas_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbsNuevo_Click(object sender, EventArgs e)
        {
            PersonaABM formPersona = new PersonaABM(Escritorio.Base.FormularioBase.ModoForm.Alta);
            formPersona.ShowDialog();
            this.Listar();
        }

        private void tbsEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(this.dgvPersonas.SelectedRows == null)) //Controlamos que la tabla tenga elementos dentro
                {
                    //Obtenemos el ID del Persona de la tabla seleccionado
                    int ID = ((Entidades.Personas)this.dgvPersonas.SelectedRows[0].DataBoundItem).Id;
                    PersonaABM formPersona = new PersonaABM(ID, Escritorio.Base.FormularioBase.ModoForm.Modificacion);
                    formPersona.ShowDialog();
                    this.Listar();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("No existen registros a editar.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbsEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(this.dgvPersonas.SelectedRows == null))
                {
                    int ID = ((Entidades.Personas)this.dgvPersonas.SelectedRows[0].DataBoundItem).Id;
                    PersonaABM formPersona = new PersonaABM(ID, Escritorio.Base.FormularioBase.ModoForm.Baja);
                    formPersona.ShowDialog();
                    this.Listar();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("No existen registros a eliminar.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
