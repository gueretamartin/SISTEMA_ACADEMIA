using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Plan : EntidadBase
    {
        private string descripcionPlan;
        private Especialidad especialidad;
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

        public string DescripcionPlan
        {
            get
            {
                return descripcionPlan;
            }

            set
            {
                descripcionPlan = value;
            }
        }

        public Especialidad Especialidad
        {
            get
            {
                return especialidad;
            }

            set
            {
                especialidad = value;
                descripcionEspecialidad = this.Especialidad.DescripcionEspecialidad;
            }
        }
    }
}
