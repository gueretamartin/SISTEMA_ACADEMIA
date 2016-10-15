using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class ControladorPlanes
    {
        private CatalogoPlanes usuarioData = new CatalogoPlanes();

        public Plan dameUno(int id)
        {
            return usuarioData.GetOne(id);
        }

        internal Plan GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public List<Plan> dameTodos() {
            List<Plan> planes = new List<Plan>();
  
            return planes;
        }
       
    }
}
