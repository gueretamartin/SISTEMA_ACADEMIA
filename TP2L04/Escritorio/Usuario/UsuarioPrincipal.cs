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

namespace Escritorio.Usuario
{
    public partial class UsuarioPrincipal : Form
    {
        #region Constructores

        public UsuarioPrincipal()
        {
            InitializeComponent();
            this.dgvUsuarios.AutoGenerateColumns = false;
        }

        #endregion

        #region Metodos

        public void Listar()
        {
            ControladorUsuario ul = new ControladorUsuario();
            dgvUsuarios.DataSource = ul.dameTodos();  //asignaremos el resultado a la propiedad DataSource de la grilla
        }

        #endregion

        #region Eventos

        private void Usuarios_Load(object sender, EventArgs e)
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
            UsuarioABM formUsuario = new UsuarioABM(Escritorio.Base.FormularioBase.ModoForm.Alta);
            formUsuario.ShowDialog();
            this.Listar();
        }

        private void tbsEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(this.dgvUsuarios.SelectedRows == null))
                {
                  int ID = ((Entidades.Usuario)this.dgvUsuarios.SelectedRows[0].DataBoundItem).Id;
                    UsuarioABM formUsuario = new UsuarioABM(ID, Escritorio.Base.FormularioBase.ModoForm.Modificacion);
                    formUsuario.ShowDialog();
                    this.Listar(); 
                }
            }
            catch (ArgumentOutOfRangeException ef)
            {
                MessageBox.Show("No existen registros a editar.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbsEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(this.dgvUsuarios.SelectedRows == null))
                {
                     int ID = ((Entidades.Usuario)this.dgvUsuarios.SelectedRows[0].DataBoundItem).Id;
                    UsuarioABM formUsuario = new UsuarioABM(ID, Escritorio.Base.FormularioBase.ModoForm.Baja);
                    formUsuario.ShowDialog();
                    this.Listar(); 
                }
            }
            catch (ArgumentOutOfRangeException ef)
            {
                MessageBox.Show("No existen registros a eliminar.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
