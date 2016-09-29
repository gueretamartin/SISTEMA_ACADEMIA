using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class UserController
    {
        public Usuario getUsuario() { 
        UsuarioData ud = new UsuarioData();
        Usuario usu = ud.getUsuario();
            return usu;
        }
    }
}
