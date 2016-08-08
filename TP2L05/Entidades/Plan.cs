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
            }
        }
    }
}
