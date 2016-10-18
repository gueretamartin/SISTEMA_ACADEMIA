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
    public partial class CursoPrincipal : Form
    {
        #region Constructores

        public CursoPrincipal()
        {
            InitializeComponent();
            this.dgvCursos.AutoGenerateColumns = false;
        }

        #endregion

        #region Metodos

        public void Listar()
        {
            ControladorCursos ul = new ControladorCursos();
            dgvCursos.DataSource = ul.dameTodos();  //asignaremos el resultado a la propiedad DataSource de la grilla
        }

        #endregion

        #region Eventos

        private void Cursos_Load(object sender, EventArgs e)
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
            CursoABM formUsuario = new CursoABM(Escritorio.Base.FormularioBase.ModoForm.Alta);
            formUsuario.ShowDialog();
            this.Listar();
        }

        private void tbsEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(this.dgvCursos.SelectedRows == null)) //Controlamos que la tabla tenga elementos dentro
                {
                    //Obtenemos el ID del usuario de la tabla seleccionado
                    int ID = ((Entidades.Curso)this.dgvCursos.SelectedRows[0].DataBoundItem).Id;
                    CursoABM formUsuario = new CursoABM(ID, Escritorio.Base.FormularioBase.ModoForm.Modificacion);
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
                if (!(this.dgvCursos.SelectedRows == null))
                {
                    int ID = ((Entidades.Curso)this.dgvCursos.SelectedRows[0].DataBoundItem).Id;
                    CursoABM formUsuario = new CursoABM(ID, Escritorio.Base.FormularioBase.ModoForm.Baja);
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
