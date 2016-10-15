using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
namespace Negocio
{
    public class ControladorPersona
    {
        private CatalogoPersonas personasData = new CatalogoPersonas();

        public Personas dameUno(int id)
        {
            return personasData.GetOne(id);
        }

        public List<Personas> dameTodos()
        {
            var personas = personasData.GetAll();
            return personas;
        }

        public void guardarUsuario(Personas personaActual)
        {
            throw new NotImplementedException();
        }
    }
}
