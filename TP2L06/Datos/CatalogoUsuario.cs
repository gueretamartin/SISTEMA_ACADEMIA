using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using Datos;
using Entidades.CustomEntity;

namespace Datos
{
    public class CatalogoUsuario : Conexion
    {
        RespuestaServidor rs;

        public CatalogoUsuario()
        {
            rs = new RespuestaServidor();
        }
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
                    Personas persona = new CatalogoPersonas().GetOne((int)drUsuarios["id_persona"]);
                    usr.Persona = persona;
                    usuarios.Add(usr);
                }
             drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de usuarios", Ex);
               
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
                    Personas persona = new CatalogoPersonas().GetOne((int)drUsuarios["id_persona"]);
                    usr.Persona = persona;
                }

                drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos de usuario", Ex);
               
            }
            finally
            {
                this.CloseConnection();
            }

            return usr;
        }

        public Entidades.Usuario getUsuario(string userName)
        {
            Usuario usr = new Usuario();

            try
            {
                this.OpenConnection();

                SqlCommand cmdUsuario = new SqlCommand("SELECT * FROM usuarios WHERE nombre_usuario=@userName and habilitado = 1", Con);
                cmdUsuario.Parameters.Add("@userName", SqlDbType.VarChar).Value = userName;

                SqlDataReader drUsuarios = cmdUsuario.ExecuteReader();

                if (drUsuarios.Read())
                {
                    usr.Id = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    Personas persona = new CatalogoPersonas().GetOne((int)drUsuarios["id_persona"]);
                    usr.Persona = persona;
                }

                drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos de usuario", Ex);
               
            }
            finally
            {
                this.CloseConnection();
            }

            return usr;
        }

        #region METODOS PARA EL ABM
        public RespuestaServidor Save(Usuario usuario)
        {
            if (usuario.State == Entidades.EntidadBase.States.Deleted)
            {
               rs =  this.Delete(usuario.Id);
            }
            else if (usuario.State == Entidades.EntidadBase.States.New)
            {
                rs = this.Insert(usuario);
            }
            else if (usuario.State == Entidades.EntidadBase.States.Modified)
            {
                rs = this.Update(usuario);
            }else
            usuario.State = Entidades.EntidadBase.States.Unmodified;
            return rs;
        }

        public RespuestaServidor Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE usuarios WHERE id_usuario=@id", Con);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteReader();
                rs.Mensaje = "Usuario eliminado correctamente";
            }
            catch (Exception Ex)
            {
                rs.AgregarExcepcion(Ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return rs;
        }

        protected RespuestaServidor Update(Usuario usuario)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE usuarios SET nombre_usuario=@nombre_usuario, clave=@clave, " +
                    "habilitado=@habilitado, id_persona=@id_persona " +
                    "WHERE id_usuario=@id", Con);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = usuario.Id;
                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = usuario.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = usuario.Clave;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = usuario.Habilitado;
                cmdSave.Parameters.Add("@id_persona", SqlDbType.Int).Value = usuario.Persona.Id;
                cmdSave.ExecuteReader();
                rs.Mensaje = "Usuario modificado correctamente";
            }
            catch (Exception Ex)
            {
                rs.AgregarExcepcion(Ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return rs;
        }

        protected RespuestaServidor Insert(Usuario usuario)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT INTO usuarios(nombre_usuario,clave,habilitado,id_persona) " +
                    "VALUES(@nombre_usuario,@clave,@habilitado,@id_persona) " +
                    "SELECT @@identity", //esta linea es para recuperar el ID que asignó el SQL automaticamente
                    Con);

                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = usuario.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = usuario.Clave;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = usuario.Habilitado;
                cmdSave.Parameters.Add("@id_persona", SqlDbType.Int).Value = usuario.Persona.Id;
                usuario.Id = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); // asi se obtiene el ID que asigno al BD automaticamente
                rs.Mensaje = "Usuario insertado correctamente";
            }
            catch (Exception Ex)
            {
                rs.AgregarExcepcion(Ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return rs;
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
                    Personas persona = new CatalogoPersonas().GetOne((int)drUsuarios["id_persona"]);
                    usr.Persona = persona;
                }

                drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos de usuario", Ex);
            }
            finally
            {
                this.CloseConnection();
            }

            return usr;
        }
        #endregion
    }
}
