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

namespace Escritorio.Especialidad
{
    public partial class EspecialidadPrincipal : Form
    {
        public EspecialidadPrincipal()
        {
            InitializeComponent();
            this.dgvEspecialidades.AutoGenerateColumns = false;
        }

        public void Listar()
        {
             ControladorEspecialidad ul = new ControladorEspecialidad();
             dgvEspecialidades.DataSource = ul.dameTodos();  //asignaremos el resultado a la propiedad DataSource de la grilla
        }

        #region Eventos

        private void Especialidad_Load(object sender, EventArgs e)
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
            EspecialidadABM formUsuario = new EspecialidadABM(Escritorio.Base.FormularioBase.ModoForm.Alta);
            formUsuario.ShowDialog();
            this.Listar();
        }

        private void tbsEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(this.dgvEspecialidades.SelectedRows == null)) //Controlamos que la tabla tenga elementos dentro
                {
                    //Obtenemos el ID del usuario de la tabla seleccionado
                    int ID = ((Entidades.Especialidad)this.dgvEspecialidades.SelectedRows[0].DataBoundItem).Id;
                    EspecialidadABM formUsuario = new EspecialidadABM(ID, Escritorio.Base.FormularioBase.ModoForm.Modificacion);
                    formUsuario.ShowDialog();
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
                if (!(this.dgvEspecialidades.SelectedRows == null))
                {
                    int ID = ((Entidades.Especialidad)this.dgvEspecialidades.SelectedRows[0].DataBoundItem).Id;
                    EspecialidadABM formUsuario = new EspecialidadABM(ID, Escritorio.Base.FormularioBase.ModoForm.Baja);
                    formUsuario.ShowDialog();
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
