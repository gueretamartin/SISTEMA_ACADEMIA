using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using Datos;

namespace Datos
{
    public class CatalogoUsuario : Conexion
    {
        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdUsuarios = new SqlCommand("SELECT * FROM usuarios", Con);

                SqlDataReader drUsuarios = cmdUsuarios.ExecuteReader();

                while (drUsuarios.Read())
                {
                    Usuario usr = new Usuario();

                    usr.Id = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    usr.Email = (string)drUsuarios["email"];
                    // Personas persona = new CatalogoPersonas().GetOne((int)drUsuarios["id_persona"]);
                    Personas persona = null;
                    usr.Persona = persona;

                    usuarios.Add(usr);
                }

                drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de usuarios", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return usuarios;
        }

        public Entidades.Usuario GetOne(int ID)
        {
            Usuario usr = new Usuario();

            try
            {
                this.OpenConnection();

                SqlCommand cmdUsuario = new SqlCommand("SELECT * FROM usuarios WHERE id_usuario=@id", Con);
                cmdUsuario.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                SqlDataReader drUsuarios = cmdUsuario.ExecuteReader();

                if (drUsuarios.Read())
                {
                    usr.Id = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    usr.Email = (string)drUsuarios["email"];
                    Personas persona = new CatalogoPersonas().GetOne((int)drUsuarios["id_persona"]);
                    usr.Persona = persona;
                }

                drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos de usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return usr;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE usuarios WHERE id_usuario=@id", Con);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteReader();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al eliminar usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Usuario usuario)
        {
            if (usuario.State == Entidades.EntidadBase.States.Deleted)
            {
                this.Delete(usuario.Id);
            }
            else if (usuario.State == Entidades.EntidadBase.States.New)
            {
                this.Insert(usuario);
            }
            else if (usuario.State == Entidades.EntidadBase.States.Modified)
            {
                this.Update(usuario);
            }
            usuario.State = Entidades.EntidadBase.States.Unmodified;
        }

        protected void Update(Usuario usuario)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE usuarios SET nombre_usuario=@nombre_usuario, clave=@clave, " +
                    "habilitado=@habilitado, nombre=@nombre, apellido=@apellido, email=@email, id_persona=@id_persona " +
                    "WHERE id_usuario=@id", Con);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = usuario.Id;
                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = usuario.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = usuario.Clave;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = usuario.Habilitado;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = usuario.Nombre;
                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = usuario.Apellido;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = usuario.Email;
                cmdSave.Parameters.Add("@id_persona", SqlDbType.Int).Value = usuario.Persona.Id;
                cmdSave.ExecuteReader();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(Usuario usuario)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT INTO usuarios(nombre_usuario,clave,habilitado,nombre,apellido,email,id_persona) " +
                    "VALUES(@nombre_usuario,@clave,@habilitado,@nombre,@apellido,@email,@id_persona) " +
                    "SELECT @@identity", //esta linea es para recuperar el ID que asignó el SQL automaticamente
                    Con);

                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = usuario.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = usuario.Clave;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = usuario.Habilitado;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = usuario.Nombre;
                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = usuario.Apellido;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = usuario.Email;
                /* Aca hay que ver como lo manejamos, porque aparentemente la persona que se le esta asignando el nuevo usuario 
                 * vendria a ser la persona que esta usando el usuario 
                 * por eso facu hace usuario.Per.ID


                */
               // cmdSave.Parameters.Add("@id_persona", SqlDbType.Int).Value = usuario.Persona.Id;
                usuario.Id = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); // asi se obtiene el ID que asigno al BD automaticamente
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Exception ExcepcionManejada = new Exception("Error al crear usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }


        public Usuario GetOneByUsuario(string nombreUsr)
        {
            Usuario usr = new Usuario();

            try
            {
                this.OpenConnection();

                SqlCommand cmdUsuario = new SqlCommand("SELECT * FROM usuarios WHERE nombre_usuario=@usuario", Con);
                cmdUsuario.Parameters.Add("@usuario", SqlDbType.VarChar, 50).Value = nombreUsr;

                SqlDataReader drUsuarios = cmdUsuario.ExecuteReader();

                if (drUsuarios.Read())
                {
                    usr.Id = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    usr.Email = (string)drUsuarios["email"];
                    Personas persona = new CatalogoPersonas().GetOne((int)drUsuarios["id_persona"]);
                    usr.Persona = persona;
                }

                drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos de usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return usr;
        }
    }
}
