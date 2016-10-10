using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class UsuarioData
    {
        
        public Usuario getUsuario()
        {
        Usuario usu = new Usuario("Admin", "admin");
        return usu;
        }       
    }
}
