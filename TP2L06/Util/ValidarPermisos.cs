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
            switch (tipoUsuario)
            {
                case 1: // Profesores
                    if (moduloAcceso.Equals("RegistrarNotas"))
                       return true;

                    return false;
                case 2: //Alumnos
                    if (moduloAcceso.Equals("AlumnoInscripcion"))
                        return true;
                    return false;
                case 3: //Recepcionista
                    if (moduloAcceso.Equals("Curso"))
                        return true;
                    if (moduloAcceso.Equals("Profesores"))
                        return true;
                    if (moduloAcceso.Equals("Materias"))
                        return true;
                    if (moduloAcceso.Equals("Plan"))
                        return true;
                    if (moduloAcceso.Equals("Alumnos"))
                        return true;
                    if (moduloAcceso.Equals("Especialidad"))
                        return true;
                    return false;
                case 4:
                    return false;
                case 5: //Administrador
                        //TODO: EL ADMINISTRADOR TIENE ACCESO A TODAS PARTES.
                    return true;
                default:
                    return false;
            }
            return false;
        }
    }
}
