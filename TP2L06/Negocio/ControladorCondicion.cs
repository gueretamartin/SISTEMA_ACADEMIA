using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ControladorCondicion
    {
        private CatalogoCondicion condicionData = new CatalogoCondicion();

        public List<Condicion> dameTodos()
        {
            List<Condicion> condiciones = new List<Condicion>();
            condiciones = condicionData.getAll();
            return condiciones;
        }
    }
}
