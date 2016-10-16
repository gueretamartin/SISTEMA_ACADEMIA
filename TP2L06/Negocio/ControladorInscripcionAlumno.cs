using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ControladorInscripcionAlumno
    {
        private CatalogoAlumnoInscripcion alumnoInscripcionData = new CatalogoAlumnoInscripcion();

        public AlumnoInscripcion dameUno(int id)
        {
            return alumnoInscripcionData.GetOne(id);
        }

        public List<AlumnoInscripcion> dameTodos()
        {
            var alumnoInscripcion = alumnoInscripcionData.GetAll();
            return alumnoInscripcion;
        }

        public void save(AlumnoInscripcion per)
        {
            alumnoInscripcionData.Save(per);
        }
        public bool alumnoEstaInscripto(int idAlumno, int idCurso)
        {
            if (alumnoInscripcionData.GetAll().Count(c => c.Alumno.Id == idAlumno && c.Curso.Id == idCurso) != 0)
                //Está inscripto
                return true;
            //No está inscripto
            return false;
        }

        //public List<AlumnoCursoPersona> ObtenerAlumnosPorCurso(int idCurso)
        //{
        //    return alumnoInscripcionData.ObtenerAlumnosPorCurso(idCurso);
        //}
    }
}
