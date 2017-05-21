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
        private string denominacion;
        private Materia materia;
        private string descripcionMateria;


       public string Denominacion
        {
            get
            {
                return denominacion;
            }
            set
            {
                denominacion = value;
            }
        }

        public string NombreMostrar
        {
            get
            {
                return materia.DescripcionMateria + ", " + this.denominacion; 
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

