using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Data.Database;

namespace Negocio
{
    public class ControladorUsuario
    {

        public ControladorUsuario()
        {
        }
        //Instancio un Adaptador de Usuario
        //Me va a devolver los datos de la BD

        private UsuarioDatos usuarioData = new UsuarioDatos();

        //Metodo que le pide al Adaptador que le de un usuario
        public Usuario dameUno(int id)
        {
            return usuarioData.dameUno(id);
        }
        //Metodo que le pide todos los usuarios
        public List<Usuario> dameTodos()
        {
            List<Usuario> usuarios = new List<Usuario>();
            usuarios = usuarioData.dameTodos();
            return usuarios;
        }
        //Metodo que le pide que guarde el usuario
        public void guardarUsuario(Usuario usu)
        {
            Usuario usuario = usu;
            usuarioData.GuardarUsuario(usuario);
        }
        //Metodo que le pide que elimine el usuario
        public void eliminarUsuario(Usuario usu)
        {
            Usuario usuario = usu;
            usuarioData.EliminarUsuario(usuario.Id);
        }
    }
}

