using Datos;
using Entidades;
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
        //Metodo que le pide que guarde el curso
        public void save(Curso curso)
        {
            cursoData.Save(curso);
        }
    }
}
