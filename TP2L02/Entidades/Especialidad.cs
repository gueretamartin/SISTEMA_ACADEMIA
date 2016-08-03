using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Especialidad : EntidadBase
    {
        private string nombreEspecialidad;
        private string descripcionEspecialidad;


        public string NombreEspecialidad
        {
            get
            {
                return nombreEspecialidad;

            }

            set
            {
                nombreEspecialidad = value;
            }
        }

        public string DescripcionEspecialidad
        {
            get
            {
                return descripcionEspecialidad;
            }

            set
            {
                descripcionEspecialidad = value;
            }
        }
    }
}

