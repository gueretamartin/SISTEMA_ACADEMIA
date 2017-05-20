using Datos;
using Entidades;
using Entidades.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

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

        public List<AlumnoInscripcion> dameTodosAlumno(int idAlumno)
        {
            List<AlumnoInscripcion> alumnoInscripcion = alumnoInscripcionData.GetAllAlumnos(idAlumno);
            return alumnoInscripcion;
        }



        public RespuestaServidor Save(AlumnoInscripcion alInscr)
        {
            RespuestaServidor rs = new RespuestaServidor();

            if (alInscr.State == EntidadBase.States.New)
            {
                if (alInscr.Curso == null)
                {
                    rs.AgregarError("No existe el curso");
                }
                else if (alumnoInscripcionData.GetOne(alInscr.Alumno.Id, alInscr.Curso.Id) != null)
                {
                    rs.AgregarError("La persona ya pertenece a este curso");
                }
                else if (alInscr.Curso.Cupo <= 0)
                    rs.AgregarError("El curso no dispone de cupo para la inscripcion");
                else
                {
                    alInscr.Curso.Cupo--;
                    (new CatalogoCursos()).Update(alInscr.Curso);
                }
            }
            else if (alInscr.State == EntidadBase.States.Deleted)
            {
                alInscr.Curso = new CatalogoCursos().GetOneByInscripcionAlumno(alInscr.Id);
                if (alInscr.Curso == null)
                {
                    rs.AgregarError("No existe el curso");
                }
                else
                {
                    alInscr.Curso.Cupo++;
                    (new CatalogoCursos()).Update(alInscr.Curso);
                }
            }
            //El metodo rs.AgregarError(); setea el rs.Error en true
            if (!rs.Error)
            {
                rs = alumnoInscripcionData.Save(alInscr, rs);
            }
            return rs;

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
