using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.CustomEntity
{
    public class RespuestaServidor
    {
        public string mensajeExito { get; set; }

        public List<string> listaErrores { get; set; }

        public bool error { get; set; }

        public RespuestaServidor()
        {
            this.listaErrores = new List<string>();
        }
    }
}
