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
    public class CatalogoModulos : Conexion
    {
        public List<Modulo> GetAll()
        {
            List<Modulo> modulos = new List<Modulo>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdModulos = new SqlCommand("select * from modulos", Con);
                SqlDataReader drModulos = cmdModulos.ExecuteReader();
                while (drModulos.Read())
                {
                    Modulo mdo = new Modulo();
                    mdo.Id = (int)drModulos["id_modulo"];
                    mdo.Descripcion = (String)drModulos["desc_modulo"];
                    mdo.Ejecuta = (String)drModulos["ejecuta"];
                    modulos.Add(mdo);
                }
                drModulos.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de módulos", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return modulos;
        }

        public Modulo GetOne(int ID)
        {
            Modulo mdo = new Modulo();
            try
            {
                this.OpenConnection();
                SqlCommand cmdModulos = new SqlCommand("select * from modulos where id_modulo = @id", Con);
                cmdModulos.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drModulos = cmdModulos.ExecuteReader();
                if (drModulos.Read())
                {
                    mdo.Id = (int)drModulos["id_modulo"];
                    mdo.Descripcion = (String)drModulos["desc_modulo"];
                    mdo.Ejecuta = (String)drModulos["ejecuta"];
                }
                drModulos.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de mòdulo", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return mdo;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdPlanes = new SqlCommand("delete modulos where id_modulo = @id", Con);
                cmdPlanes.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdPlanes.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar módulo", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Modulo modulo)
        {
            if (modulo.State == Entidades.EntidadBase.States.New)
            {
                this.Insert(modulo);
            }
            else if (modulo.State == Entidades.EntidadBase.States.Deleted)
            {
                this.Delete(modulo.Id);
            }
            else if (modulo.State == Entidades.EntidadBase.States.Modified)
            {
                this.Update(modulo);
            }
            modulo.State = Entidades.EntidadBase.States.Unmodified;
        }

        protected void Update(Modulo modulo)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("UPDATE modulos SET desc_modulo = @desc_modulo," +
               "ejecuta = @ejecuta " +
                    " WHERE id_modulo = @id", Con);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = modulo.Id;
                cmdSave.Parameters.Add("@desc_modulo", SqlDbType.VarChar, 50).Value = modulo.Descripcion;
                cmdSave.Parameters.Add("@ejecuta", SqlDbType.VarChar, 50).Value = modulo.Ejecuta;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del modulo", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(Modulo modulo)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("INSERT into modulos (desc_modulo, ejecuta) " +
                    "values (@desc_modulo, @ejecuta)" +
                    " select @@identity", Con);

                cmdSave.Parameters.Add("@desc_modulo", SqlDbType.VarChar, 50).Value = modulo.Descripcion;
                cmdSave.Parameters.Add("@ejecuta", SqlDbType.VarChar, 50).Value = modulo.Ejecuta;
                modulo.Id = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear plan", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

    }
}
