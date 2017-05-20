using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Materia : EntidadBase
    {
        private string descripcionMateria;
        private int horasSemanales;
        private int horasTotales;
        private Plan plan;
        private string descripcionPlan;


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
        public string DescripcionMateria
        {
            get
            {
                return descripcionMateria;
            }

            set
            {
                descripcionMateria = value;
            }
        }

        public int HorasSemanales
        {
            get
            {
                return horasSemanales;
            }

            set
            {
                horasSemanales = value;
            }
        }

        public int HorasTotales
        {
            get
            {
                return horasTotales;
            }

            set
            {
                horasTotales = value;
            }
        }

        public Plan Plan
        {
            get
            {
                return plan;
            }

            set
            {
                plan = value;
                descripcionPlan = this.Plan.DescripcionPlan;
            }
        }
    }
}
