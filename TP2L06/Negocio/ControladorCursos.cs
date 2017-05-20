using Datos;
using Entidades;
using Entidades.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ControladorCursos
    {
        private CatalogoCursos cursoData = new CatalogoCursos();

        //Metodo que le pide al Adaptador que le de un curso
        public Curso dameUno(int id)
        {
            return cursoData.GetOne(id);
        }
        //Metodo que le pide todos los cursos
        public List<Curso> dameTodos()
        {
            List<Curso> cursos = new List<Curso>();
            cursos = cursoData.getAll();
            return cursos;
        }

        public List<Curso> dameTodosPlan(int idPlan)
        {
            List<Curso> cursos = new List<Curso>();
            cursos = cursoData.GetCursosPlan(idPlan);
            return cursos;
        }

        public List<Curso> dameTodosPlanNoAlumno(int idPlan, int idAlumno)
        {
            List<Curso> cursos = new List<Curso>();
            cursos = cursoData.GetCursosPlanNoAlumno(idPlan,idAlumno);
            return cursos;
        }

        //Metodo que le pide que guarde el curso
        public RespuestaServidor save(Curso curso)
        {
            return cursoData.Save(curso);
        }
    }
}
