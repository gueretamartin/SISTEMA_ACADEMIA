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
    public partial class ComisionPrincipal : Form
    {
        public ComisionPrincipal()
        {
            InitializeComponent();
            this.dgvComisiones.AutoGenerateColumns = false;
        }

        #region Metodos

        public void Listar()
        {
            ControladorComisiones ul = new ControladorComisiones();
            dgvComisiones.DataSource = ul.dameTodos();  //asignaremos el resultado a la propiedad DataSource de la grilla
        }

        #endregion


        #region Eventos

        private void Comisiones_Load(object sender, EventArgs e)
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
            ComisionABM formUsuario = new ComisionABM(Escritorio.Base.FormularioBase.ModoForm.Alta);
            formUsuario.ShowDialog();
            this.Listar();
        }

        private void tbsEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(this.dgvComisiones.SelectedRows == null)) //Controlamos que la tabla tenga elementos dentro
                {
                    int ID = ((Entidades.Comision)this.dgvComisiones.SelectedRows[0].DataBoundItem).Id;
                    ComisionABM formUsuario = new ComisionABM(ID, Escritorio.Base.FormularioBase.ModoForm.Modificacion);
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
                if (!(this.dgvComisiones.SelectedRows == null))
                {
                    int ID = ((Entidades.Comision)this.dgvComisiones.SelectedRows[0].DataBoundItem).Id;
                    ComisionABM formUsuario = new ComisionABM(ID, Escritorio.Base.FormularioBase.ModoForm.Baja);
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
