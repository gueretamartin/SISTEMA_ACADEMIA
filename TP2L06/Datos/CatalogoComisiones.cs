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
    public class CatalogoComisiones : Conexion
    {
        public List<Comision> getAll()
        {
            List<Comision> comisiones = new List<Comision>();
            Comision com = null;
            this.OpenConnection();
            try
            {
                SqlCommand cmdComisiones = new SqlCommand("Select * from comisiones", Con);
                SqlDataReader drComisiones = cmdComisiones.ExecuteReader();
                while (drComisiones.Read())
                {
                    com = new Comision();
                    com.DescripcionComision = (string)drComisiones["desc_comision"];
                    com.AnioEspecialidad = (int)drComisiones["anio_especialidad"];
                    com.Plan = new CatalogoPlanes().GetOne((int)drComisiones["id_plan"]);
                    comisiones.Add(com);
                }
                drComisiones.Close();
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de comisiones", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return comisiones;
        }

        public Comision GetOne(int id)
        {
            Comision com = new Comision();
            this.OpenConnection();
            try
            {
                SqlCommand cmdComisiones = new SqlCommand("Select * from comisiones where id_comision = @id", Con);
                cmdComisiones.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataReader drComisiones = cmdComisiones.ExecuteReader();
                if (drComisiones.Read())
                {
                    com.DescripcionComision = (string)drComisiones["desc_comision"];
                    com.AnioEspecialidad = (int)drComisiones["anio_especialidad"];
                    com.Plan = new CatalogoPlanes().GetOne((int)drComisiones["id_plan"]);
                }
                drComisiones.Close();
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar la comision", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return com;
        }

        #region METODOS PARA EL ABM

        public void Save(Comision com)
        {
            if (com.State == Entidades.EntidadBase.States.Deleted)
            {
                this.Delete(com.Id);
            }
            else if (com.State == Entidades.EntidadBase.States.New)
            {
                this.Insert(com);
            }
            else if (com.State == Entidades.EntidadBase.States.Modified)
            {
                this.Update(com);
            }
            com.State = Entidades.EntidadBase.States.Unmodified;
        }

        public void Delete(int id)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE comisiones WHERE id_comision=@id", Con);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmdDelete.ExecuteReader();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al eliminar la comision", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Update(Comision com)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE comisiones SET desc_comision=@desc, anio_especialidad=@anio id_plan=@idPlan WHERE id_comision = @id", Con);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = com.Id;
                cmdSave.Parameters.Add("@desc_comision", SqlDbType.VarChar, 50).Value = com.DescripcionComision;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = com.AnioEspecialidad;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = com.Plan.Id;
                cmdSave.ExecuteReader();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar los datos de la comision", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Insert(Comision com)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT INTO usuarios(desc_comision,anio_especialidad,id_plan) " +
                    "VALUES(@desc_comision,@anio,@id_plan) " +
                    "SELECT @@identity", //esta linea es para recuperar el ID que asignó el SQL automaticamente
                    Con);

                cmdSave.Parameters.Add("@desc_comision", SqlDbType.VarChar, 50).Value = com.DescripcionComision;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = com.AnioEspecialidad;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = com.Plan.Id;

                com.Id = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); // asi se obtiene el ID que asigno al BD automaticamente
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Exception ExcepcionManejada = new Exception("Error al crear comision", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        #endregion
    }
}
