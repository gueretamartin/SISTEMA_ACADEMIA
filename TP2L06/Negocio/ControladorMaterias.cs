using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;

namespace Negocio
{
    public class ControladorMaterias
    {
        private CatalogoMaterias materiaData = new CatalogoMaterias();

        //Metodo que le pide al Adaptador que le de un materia
        public Materia dameUno(int id)
        {
            return materiaData.GetOne(id);
        }
        //Metodo que le pide todos los materias
        public List<Materia> dameTodos()
        {
            List<Materia> materias = new List<Materia>();
            materias = materiaData.getAll();
            return materias;
        }
        //Metodo que le pide que guarde el materia
        public void save(Materia materia)
        {
            materiaData.Save(materia);
        }
    }
}
