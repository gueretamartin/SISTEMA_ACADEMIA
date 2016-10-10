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

namespace Escritorio
{
    public partial class formularioLogin : Form
    {
        public formularioLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            //Code of handler 
            Usuario usuario = new Usuario();
            UserController uc = new UserController();
            usuario = uc.getUsuario();

            if(this.txtUsuario.Text==usuario.NombreUsuario && this.txtPass.Text == usuario.Password){
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Usuario y/o Contraseña incorrectos", "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }

        private void lnkOlvidaPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Es un usuario muy descuidado!", "Olvidé mi Contraseña",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
