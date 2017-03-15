using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Entidades.CustomEntity
{
    public class RespuestaServidor
    {
        public string Mensaje { get; set; }

        public List<string> ListaErrores { get; set; }

        public bool Error { get; set; }

        public void AgregarError(string error)
        {
            this.Error = true;
            this.ListaErrores.Add(error);
        }

        public void MostrarMensajes()
        {
            if (!this.Error && this.Mensaje != null)
            {
                Mensajeria.MostrarExito(this.Mensaje);
            }
            else
            {
                Mensajeria.MostrarErrores(this.ListaErrores);
            }
        }


        public void AgregarExcepcion(Exception ex)
        {
            this.Error = true;
            if(ex.InnerException != null)
            {
                this.AgregarExcepcion(ex);
            }
            else
            {
                this.ListaErrores.Add(ex.Message);
            }
        }

        public RespuestaServidor()
        {
            this.Error = false;
            this.ListaErrores = new List<string>();
        }
    }
}
