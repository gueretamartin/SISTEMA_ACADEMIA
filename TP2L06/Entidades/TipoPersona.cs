using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TipoPersona : EntidadBase
    {
        string descripcionTipo;
        
        public string DescripcionTipo
        {
            get
            {
                return descripcionTipo;

            }

            set
            {
                descripcionTipo = value;
            }
        }
    }
}
