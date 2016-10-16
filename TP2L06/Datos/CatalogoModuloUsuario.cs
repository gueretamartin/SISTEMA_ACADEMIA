using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CatalogoModuloUsuario : Conexion
    {
        public List<ModuloUsuario> GetAll()
        {
            List<ModuloUsuario> ModuloUsuarios = new List<ModuloUsuario>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdModuloUsuario = new SqlCommand("select * from modulos_usuarios", Con);
                SqlDataReader drModuloUsuario = cmdModuloUsuario.ExecuteReader();
                while (drModuloUsuario.Read())
                {
                    ModuloUsuario mu = new ModuloUsuario();
                    mu.Id = (int)drModuloUsuario["id_modulo_usuario"];
                    mu.Usuario = new CatalogoUsuario().GetOne((int)drModuloUsuario["id_usuario"]);
                    mu.Modulo = new CatalogoModulos().GetOne((int)drModuloUsuario["id_modulo"]);
                    mu.PermiteAlta = (bool)drModuloUsuario["alta"];
                    mu.PermiteBaja = (bool)drModuloUsuario["baja"];
                    mu.PermiteConsulta = (bool)drModuloUsuario["modificacion"];
                    mu.PermiteModificacion = (bool)drModuloUsuario["consulta"];
                    ModuloUsuarios.Add(mu);
                }
                drModuloUsuario.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de ModuloUsuarios", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return ModuloUsuarios;
        }
        public List<ModuloUsuario> GetAll(int idUsuario)
        {
            List<ModuloUsuario> ModuloUsuarios = new List<ModuloUsuario>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdModuloUsuario = new SqlCommand("select * from modulos_usuarios where id_usuario = @id_usuario", Con);
                cmdModuloUsuario.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idUsuario;
                SqlDataReader drModuloUsuario = cmdModuloUsuario.ExecuteReader();
                while (drModuloUsuario.Read())
                {
                    ModuloUsuario mu = new ModuloUsuario();
                    mu.Id = (int)drModuloUsuario["id_modulo_usuario"];
                    mu.Usuario = new CatalogoUsuario().GetOne((int)drModuloUsuario["id_usuario"]);
                    mu.Modulo = new CatalogoModulos().GetOne((int)drModuloUsuario["id_modulo"]);
                    mu.PermiteAlta = (bool)drModuloUsuario["alta"];
                    mu.PermiteBaja = (bool)drModuloUsuario["baja"];
                    mu.PermiteConsulta = (bool)drModuloUsuario["modificacion"];
                    mu.PermiteModificacion = (bool)drModuloUsuario["consulta"];
                    ModuloUsuarios.Add(mu);
                }
                drModuloUsuario.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de ModuloUsuarios", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return ModuloUsuarios;
        }

        public ModuloUsuario GetOne(int ID)
        {
            ModuloUsuario mu = new ModuloUsuario();
            try
            {
                this.OpenConnection();
                SqlCommand cmdModuloUsuarios = new SqlCommand("select * from modulos_usuarios where id_modulo_usuario = @id", Con);
                cmdModuloUsuarios.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drModuloUsuario = cmdModuloUsuarios.ExecuteReader();
                if (drModuloUsuario.Read())
                {
                    mu.Id = (int)drModuloUsuario["id_modulo_usuario"];
                    mu.Usuario = new CatalogoUsuario().GetOne((int)drModuloUsuario["id_usuario"]);
                    mu.Modulo = new CatalogoModulos().GetOne((int)drModuloUsuario["id_modulo"]);
                    mu.PermiteAlta = (bool)drModuloUsuario["alta"];
                    mu.PermiteBaja = (bool)drModuloUsuario["baja"];
                    mu.PermiteConsulta = (bool)drModuloUsuario["modificacion"];
                    mu.PermiteModificacion = (bool)drModuloUsuario["consulta"];
                }
                drModuloUsuario.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de ModuloUsuarios", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return mu;
        }

        public ModuloUsuario GetOne(int idModulo, int idUsuario)
        {
            ModuloUsuario mu = new ModuloUsuario();
            try
            {
                this.OpenConnection();
                SqlCommand cmdModuloUsuarios = new SqlCommand("select * from modulos_usuarios where id_modulo= @id_modulo and id_usuario = @id_usuario", Con);
                cmdModuloUsuarios.Parameters.Add("@id_modulo", SqlDbType.Int).Value = idModulo;
                cmdModuloUsuarios.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idUsuario;
                SqlDataReader drModuloUsuario = cmdModuloUsuarios.ExecuteReader();
                if (drModuloUsuario.Read())
                {
                    mu.Id = (int)drModuloUsuario["id_modulo_usuario"];
                    mu.Usuario = new CatalogoUsuario().GetOne((int)drModuloUsuario["id_usuario"]);
                    mu.Modulo = new CatalogoModulos().GetOne((int)drModuloUsuario["id_modulo"]);
                    mu.PermiteAlta = (bool)drModuloUsuario["alta"];
                    mu.PermiteBaja = (bool)drModuloUsuario["baja"];
                    mu.PermiteConsulta = (bool)drModuloUsuario["modificacion"];
                    mu.PermiteModificacion = (bool)drModuloUsuario["consulta"];
                }
                drModuloUsuario.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de ModuloUsuarios", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return mu;
        }


        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("delete modulos_usuarios where id_modulo_usuario = @id", Con);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar ModuloUsuario", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(ModuloUsuario ModuloUsuario)
        {
            if (ModuloUsuario.State == Entidades.EntidadBase.States.New)
            {
                this.Insert(ModuloUsuario);
            }
            else if (ModuloUsuario.State == Entidades.EntidadBase.States.Deleted)
            {
                this.Delete(ModuloUsuario.Id);
            }
            else if (ModuloUsuario.State == Entidades.EntidadBase.States.Modified)
            {
                this.Update(ModuloUsuario);
            }
            ModuloUsuario.State = Entidades.EntidadBase.States.Unmodified;
        }

        protected void Update(ModuloUsuario ModuloUsuario)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("UPDATE modulos_usuarios SET id_modulo = @id_modulo," +
               "id_usuario = @id_usuario,alta = @alta, baja = @baja, modificacion = @modificacion,consulta = @consulta" +
                    " WHERE id_modulo_usuario = @id", Con);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = ModuloUsuario.Id;
                cmdSave.Parameters.Add("@id_modulo", SqlDbType.Int).Value = ModuloUsuario.Modulo.Id;
                cmdSave.Parameters.Add("@id_usuario", SqlDbType.Int).Value = ModuloUsuario.Usuario.Id;
                cmdSave.Parameters.Add("@alta", SqlDbType.Bit).Value = ModuloUsuario.PermiteAlta;
                cmdSave.Parameters.Add("@baja", SqlDbType.Bit).Value = ModuloUsuario.PermiteBaja;
                cmdSave.Parameters.Add("@modificacion", SqlDbType.Bit).Value = ModuloUsuario.PermiteModificacion;
                cmdSave.Parameters.Add("@consulta", SqlDbType.Bit).Value = ModuloUsuario.PermiteConsulta;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del ModuloUsuario", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(ModuloUsuario ModuloUsuario)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("INSERT into modulos_usuarios (id_modulo," +
               "id_usuario, alta, baja, modificacion,consulta) values(@id_modulo,@id_usuario,@alta,@baja,@modificacion,@consulta) select @@identity", Con);
                cmdSave.Parameters.Add("@id_modulo", SqlDbType.Int).Value = ModuloUsuario.Modulo.Id;
                cmdSave.Parameters.Add("@id_usuario", SqlDbType.Int).Value = ModuloUsuario.Usuario.Id;
                cmdSave.Parameters.Add("@alta", SqlDbType.Bit).Value = ModuloUsuario.PermiteAlta;
                cmdSave.Parameters.Add("@baja", SqlDbType.Bit).Value = ModuloUsuario.PermiteBaja;
                cmdSave.Parameters.Add("@modificacion", SqlDbType.Bit).Value = ModuloUsuario.PermiteModificacion;
                cmdSave.Parameters.Add("@consulta", SqlDbType.Bit).Value = ModuloUsuario.PermiteConsulta;
                ModuloUsuario.Id = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear ModuloUsuario", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

    }
}
