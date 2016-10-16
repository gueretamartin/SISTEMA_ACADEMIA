using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ControladorComisiones
    {
        private CatalogoComisiones comisionData = new CatalogoComisiones();

        //Metodo que le pide al Adaptador que le de un comision
        public Comision dameUno(int id)
        {
            return comisionData.GetOne(id);
        }
        //Metodo que le pide todos los comisiones
        public List<Comision> dameTodos()
        {
            List<Comision> comisiones = new List<Comision>();
            comisiones = comisionData.getAll();
            return comisiones;
        }
        //Metodo que le pide que guarde el comision
        public void save(Comision comision)
        {
            comisionData.Save(comision);
        }
    }
}
