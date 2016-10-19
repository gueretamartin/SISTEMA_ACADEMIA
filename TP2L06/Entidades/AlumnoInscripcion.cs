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
        private string nombreAlumno;
        private string nombreCurso;

        public string NombreAlumno
        {
            get
            {
                return nombreAlumno;
            }

            set
            {
                nombreAlumno = value;
            }
        }
        public string NombreCurso
        {
            get
            {
                return nombreCurso;
            }

            set
            {
                nombreCurso = value;
            }
        }

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
                nombreAlumno = Alumno.Nombre;
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
                nombreCurso = Curso.Id.ToString(); ;
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

