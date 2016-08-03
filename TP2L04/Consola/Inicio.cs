using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Consola
{
    public class Inicio
    {

        static void Main(string[] args)
        {
            Usuarios menu = new Usuarios();
            menu.Menu();
            Console.ReadKey();
        }
    }
}
