using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class ControladorEspecialidad 
    {
        private CatalogoEspecialidad especialidadData = new CatalogoEspecialidad();

        //Metodo que le pide al Adaptador que le de un especialidad
        public Especialidad dameUno(int id)
        {
            return especialidadData.getOne(id);
        }
        //Metodo que le pide todos los especialidades
        public List<Especialidad> dameTodos()
        {
            List<Especialidad> especialidades = new List<Especialidad>();
            especialidades = especialidadData.getAll();
            return especialidades;
        }
        //Metodo que le pide que guarde el especialidad
        public void save(Especialidad especialidad)
        {
            especialidadData.Save(especialidad);
        }
    }
}
