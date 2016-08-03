using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class ModuloUsuario : EntidadBase
    {
        #region VARIABLES 
        private Usuario usuario;
        private bool permiteAlta;
        private bool permiteBaja;
        private bool permiteModificacion;
        private bool permiteConsulta;
        private Modulo modulo;
        #endregion

        public Usuario Usuario
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
            }
        }

        public bool PermiteAlta
        {
            get
            {
                return permiteAlta;
            }

            set
            {
                permiteAlta = value;
            }
        }

        public bool PermiteBaja
        {
            get
            {
                return permiteBaja;
            }

            set
            {
                permiteBaja = value;
            }
        }

        public bool PermiteModificacion
        {
            get
            {
                return permiteModificacion;
            }

            set
            {
                permiteModificacion = value;
            }
        }

        public bool PermiteConsulta
        {
            get
            {
                return permiteConsulta;
            }

            set
            {
                permiteConsulta = value;
            }
        }

        public Modulo Modulo
        {
            get
            {
                return modulo;
            }

            set
            {
                modulo = value;
            }
        }
    }
}
