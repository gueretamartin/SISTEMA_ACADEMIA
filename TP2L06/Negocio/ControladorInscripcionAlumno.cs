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

        public RespuestaServidor Save(AlumnoInscripcion per)
        {
            RespuestaServidor rs = new RespuestaServidor();
            if(per.Curso == null)
            {
                rs.AgregarError("No existe la comision");
            }
            else if (per.State == EntidadBase.States.New)
            {
                if(per.Curso.Cupo <= 0)
                    rs.AgregarError("El curso no dispone de cupo para la inscripcion");
                else
                {
                    per.Curso.Cupo--;
                    (new CatalogoCursos()).Update(per.Curso);
                }
            }
           else if (per.State == EntidadBase.States.Deleted)
            {
                per.Curso.Cupo++;
                (new CatalogoCursos()).Update(per.Curso);
            }
            //El metodo rs.AgregarError(); setea el rs.Error en true
            if (!rs.Error)
            {
                rs = alumnoInscripcionData.Save(per, rs);
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
