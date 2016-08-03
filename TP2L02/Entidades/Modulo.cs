using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Modulo : EntidadBase
    {
        private string descripcion;

        private string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        
    }
}
