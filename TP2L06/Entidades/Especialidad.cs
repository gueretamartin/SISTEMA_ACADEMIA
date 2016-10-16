using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Especialidad : EntidadBase
    {
        private string descripcionEspecialidad;
        
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

