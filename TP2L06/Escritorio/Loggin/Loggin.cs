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

namespace Escritorio.Loggin
{
    public partial class Loggin : Form
    {
        public Entidades.Usuario us;

        public Loggin()
        {
            InitializeComponent();
            us = new Entidades.Usuario();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
           
            us = new ControladorUsuario().validarUsuario(txtUsuario.Text, txtPass.Text);

            if (us!=null)
            {
                if ((us.Habilitado == true)) { this.DialogResult = DialogResult.OK; }
                else
                {
                     MessageBox.Show("El usuario no se encuentra Habilitado", "Login"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Usuario y/o contraseña incorrectos", "Login"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkOlvidaPass_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Es Ud. un usuario muy descuidado, haga memoria", "Olvidé mi contraseña",
            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
    }
}
