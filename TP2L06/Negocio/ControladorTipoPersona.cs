using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades; 

namespace Negocio
{
    public class ControladorTipoPersona
    {
        private  CatalogoTipoPersona cp  = new CatalogoTipoPersona();

        public TipoPersona dameUno(int id)
        {
            return cp.GetOne(id);
        }

        public object dameTodos()
        {
            throw new NotImplementedException();
        }
    }
}
