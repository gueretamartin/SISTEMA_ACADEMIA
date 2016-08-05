using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario : EntidadBase
    {
        private string nombreUsuario;
        private string clave;
        private bool habilitado;
        private Personas persona;

        #region Propiedades

        public Personas Persona
        {
            get { return persona; }
            set { persona = value; }
        }

        public string NombreUsuario
        {
            get
            {
                return nombreUsuario;
            }

            set
            {
                nombreUsuario = value;
            }
        }

        public string Clave
        {
            get
            {
                return clave;
            }

            set
            {
                clave = value;
            }
        }
        
        public bool Habilitado
        {
            get
            {
                return habilitado;
            }

            set
            {
                habilitado = value;
            }
        }

        #endregion

        #region Constructores

        public Usuario(string nombreUsu, string clave, bool habilitado)
        {

            this.NombreUsuario = nombreUsu;
            this.Clave = clave;
            
            this.Habilitado = habilitado;
        }
        public Usuario() { }
        #endregion
    }

}

