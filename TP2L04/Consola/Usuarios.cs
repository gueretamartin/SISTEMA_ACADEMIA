using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;
using Entidades;

namespace Consola
{
    class Usuarios
    {
        private ControladorUsuario cu = new ControladorUsuario();
        int rta = 0;
        //Menu Interactivo
        public void Menu()
        {
            while (rta != 6)
            {
                Console.WriteLine("  Seleccione la opcion que desee realizar:");
                Console.WriteLine("1– Listado General");
                Console.WriteLine("2– Consultar (Mostrar Datos de un Ussuario)");
                Console.WriteLine("3– Agregar ");
                Console.WriteLine("4 - Modificar ");
                Console.WriteLine("5 - Eliminar");
                Console.WriteLine("6 - Salir");
                string opcion = System.Console.ReadLine();
                rta = int.Parse(opcion);
                switch (rta)
                {
                    case 1:
                        {
                            Console.Clear();
                            this.MostrarDatos(cu.dameTodos());
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            try
                            {
                                Console.Clear();
                                this.Consultar();
                                break;
                            }
                            //Por si no introduce un entero
                            catch (FormatException)
                            {
                                Console.WriteLine("Introduzca Enteros");
                                break;
                            }
                            //Si me pone un ID que no corresponde
                            catch (Exception)
                            {
                                Console.WriteLine("Introduzca los datos correctamente");
                                break;
                            }
                        }
                    case 3:
                        {
                            this.Agregar();
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            this.Modificar();
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();
                            this.Eliminar();
                            break;
                        }
                    case 6:
                        {
                            Environment.Exit(0);
                            break;
                        }

                }
            }
        }
        public void ListadoGeneral()
        {
            System.Console.Clear();
            List<Entidades.Usuario> usuarios = cu.dameTodos();
            foreach (Entidades.Usuario usu in usuarios)
            {
                MostrarDatos(usu);
            }
        }
        public void Consultar()
        {
            Console.WriteLine("Ingrese el Id del usuario a buscar");
            string idIntroducido = System.Console.ReadLine();
            int idABuscar = int.Parse(idIntroducido);
            this.MostrarDatos(cu.dameUno(idABuscar));
        }
        public void Agregar()
        {
            Entidades.Usuario usuario = new Entidades.Usuario();
            System.Console.WriteLine("Ingrese los datos del usuario a registrar");
            System.Console.WriteLine("Nombre: ");
            usuario.NombreUsuario = Console.ReadLine();
            System.Console.WriteLine("Apellido: ");
            usuario.Apellido = Console.ReadLine();
            System.Console.WriteLine("Clave: ");
            usuario.Clave = Console.ReadLine();
            System.Console.WriteLine("email: ");
            usuario.Email = Console.ReadLine();
            usuario.Habilitado = true;
            cu.guardarUsuario(usuario);

        }
        public void Modificar()
        {
            Console.WriteLine("Introduzca el ID del Usuario a modificar");
            var ki = Console.ReadKey();
            int idIntroducido = int.Parse(ki.KeyChar.ToString());
            Entidades.Usuario usu = new Entidades.Usuario();
            usu = cu.dameUno(idIntroducido);
            this.MostrarDatos(usu);
            Console.WriteLine("Modifique los datos");
            System.Console.WriteLine("Nombre: ");
            usu.Nombre = Console.ReadLine();
            System.Console.WriteLine("Apellido: ");
            usu.Apellido = Console.ReadLine();
            System.Console.WriteLine("Clave: ");
            usu.Clave = Console.ReadLine();
            System.Console.WriteLine("email: ");
            usu.Email = Console.ReadLine();
            usu.Id = idIntroducido;
            usu.Habilitado = true;
            usu.State = EntidadBase.States.Modified;
            cu.guardarUsuario(usu);
        }
        public void Eliminar()
        {
            Console.WriteLine("Ingrese el Id del usuario a eliminar");
            string idIntroducido = System.Console.ReadLine();
            int idABuscar = int.Parse(idIntroducido);
            //cu.eliminarUsuario(cu.dameUno(idABuscar));
        }



        //USO DE SOBRECARGA PARA LOS METODOS MOSTRAR USUARIO, UNO PARA UN SOLO USUARIO Y OTRO PARA LISTA DE USUARIOS
        public void MostrarDatos(Entidades.Usuario usu)
        {
            Console.WriteLine("\t\tUsuario:" + usu.Id);
            System.Console.WriteLine("\t\tNombre: " + usu.Nombre);
            Console.WriteLine("\t\tApellido: " + usu.Apellido);
            System.Console.WriteLine("\t\tNombre de Usuario: " + usu.NombreUsuario);
            System.Console.WriteLine("\t\tClave: " + usu.Clave);
            System.Console.WriteLine("\t\tEmail: " + usu.Email);
            System.Console.WriteLine("\t\tHabilitado: " + usu.Habilitado);

        }
        //Cambiamos el Metodo ListadoGeneral del TP por MostrarDatos usando sobrecarga.
        public void MostrarDatos(List<Entidades.Usuario> usus)
        {

            foreach (Entidades.Usuario usu in usus)
            {
                MostrarDatos(usu);
                Console.WriteLine("");
            }
        }
    }

}
