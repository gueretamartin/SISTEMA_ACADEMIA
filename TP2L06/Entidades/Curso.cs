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
            }
        }
    }
}

