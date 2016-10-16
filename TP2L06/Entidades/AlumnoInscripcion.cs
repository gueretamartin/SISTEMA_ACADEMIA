using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class AlumnoInscripcion : EntidadBase
    {
        private string condicion;
        private Personas alumno;
        private Curso curso;
        private int nota;

        public string Condicion
        {
            get
            {
                return condicion;
            }

            set
            {
                condicion = value;
            }
        }

        public Personas Alumno
        {
            get
            {
                return alumno;
            }

            set
            {
                alumno = value;
            }
        }

        public Curso Curso
        {
            get
            {
                return curso;
            }

            set
            {
                curso = value;
            }
        }

        public int Nota
        {
            get
            {
                return nota;
            }

            set
            {
                nota = value;
            }
        }
    }
}

