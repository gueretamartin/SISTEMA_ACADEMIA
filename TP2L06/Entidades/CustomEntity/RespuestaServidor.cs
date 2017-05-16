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
            else if(this.Error)
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

        public bool ContieneExcepcion(Exception ex, string cadenaContenida)
        {
            this.Error = true;
            if (ex.InnerException != null)
            {
                this.ContieneExcepcion(ex, cadenaContenida);
                return false;
            }
            else
            {
                return ex.Message.Contains(cadenaContenida);
            }
        }

        public RespuestaServidor()
        {
            this.Error = false;
            this.ListaErrores = new List<string>();
        }
    }
}
