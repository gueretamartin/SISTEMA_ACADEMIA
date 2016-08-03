using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Comision : EntidadBase
    {
        private int anioEspecialidad;
        private string descripcionComision;
        private Plan plan;

        public int AnioEspecialidad
        {
            get
            {
                return anioEspecialidad;
            }

            set
            {
                anioEspecialidad = value;
            }
        }

        public string DescripcionComision
        {
            get
            {
                return descripcionComision;
            }

            set
            {
                descripcionComision = value;
            }
        }

        internal Plan Plan
        {
            get
            {
                return plan;
            }

            set
            {
                plan = value;
            }
        }
    }
}

