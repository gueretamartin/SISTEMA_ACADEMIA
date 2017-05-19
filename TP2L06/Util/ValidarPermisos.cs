using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class ValidarPermisos
    {
        public static bool TienePermisosUsuario(int tipoUsuario, string moduloAcceso)
        {
            bool permiso = false;
            switch (tipoUsuario)
            {
                case 1:
                    permiso = false;
                    break;
                case 2:
                    //TODO: ACA HAGO QUE UN ALUMNO LOGUEADO SOLO PUEDA ACCEDER AL MODULO DE ALUMNOS. SI INTENTA ACCEDER A CUALQUIER OTRA PARTE, NO VA A PODER...
                    //PARA QUE TENGA ACCESO VALIDA EN UN IF EL NOMBRE DEL MODULO AL QUE QUIERE ACCEDER Y RETORNA TRUE...
                    //EL NOMBRE DEL FORMULARIO QUE NO APAREZCA ACA ES PORQUE NO PUEDE ACCEDER...
                    if (moduloAcceso.Equals("Alumnos"))
                        permiso = true;
                    else
                        permiso = false;
                    break;
                case 3:
                    permiso = false;
                    break;
                case 4:
                    permiso = false;
                    break;
                case 5:
                    //TODO: EL ADMINISTRADOR TIENE ACCESO A TODAS PARTES.
                    permiso = true;
                    break;
            }
            return permiso;
        }
    }
}
