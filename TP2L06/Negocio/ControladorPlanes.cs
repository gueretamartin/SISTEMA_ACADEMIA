using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades.CustomEntity;

namespace Negocio
{
    public class ControladorPlanes
    {
        private CatalogoPlanes planesData = new CatalogoPlanes();

        public Plan dameUno(int id)
        {
            return planesData.GetOne(id);
        }
        
        public List<Plan> dameTodos() {
            List<Plan> planes = new List<Plan>();
            planes = planesData.GetAll();
            return planes;
        }

        public RespuestaServidor save(Plan plan)
        {
          return  planesData.Save(plan);
        }
    }
}
