using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Curso : EntidadBase
    {
        private int anioCalendario;
        private int cupo;
        private Comision comision;
        private Materia materia;
        private string descripcionComision;
        private string descripcionMateria;



        public string DescripcionComision
        {
            get
            {
                return descripcionComision;
            }
            set
            {
                descripcionComision = this.Comision.DescripcionComision;
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
                descripcionMateria = this.Materia.DescripcionMateria;
            }
        }


        public int AnioCalendario
        {
            get
            {
                return anioCalendario;
            }

            set
            {
                anioCalendario = value;
            }
        }

        public int Cupo
        {
            get
            {
                return cupo;
            }

            set
            {
                cupo = value;
            }
        }


        public Comision Comision
        {
            get
            {
                return comision;
            }

            set
            {
                comision = value;
                descripcionComision = comision.DescripcionComision;

            }
        }

        public Materia Materia
        {
            get
            {
                return materia;
            }

            set
            {
                materia = value;
                descripcionMateria = materia.DescripcionMateria;
            }
        }
    }
}

