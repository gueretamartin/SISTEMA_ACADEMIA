using Entidades;
using Negocio;
using System;
using System.Reflection.Emit;

namespace WebTest
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void validarEIngresar(object sender, EventArgs e)
        {
            String nombreUsuario = this.txtUser.Text;
            String pass = this.txtPass.Text;
            ControladorUsuario cu = new ControladorUsuario();
            Usuario user = cu.validarUsuario(nombreUsuario, pass);
            if(user != null)
            {
                Session["idUsuario"] = user.Id;
                Session["user"] = user.NombreUsuario;
                Session["nombreUsuario"] = user.Persona.Nombre;
                Session["apellidoUsuario"] = user.Persona.Apellido;
                Response.Redirect("~/Home.aspx");   
                
                //TODO CREAR VAR DE SESION Y REDIRECCIONARLO
            }
            else
            {
                this.lblError.Text = "No se encontró el Usuario, intente de nuevo";
            }
         }
    }
}