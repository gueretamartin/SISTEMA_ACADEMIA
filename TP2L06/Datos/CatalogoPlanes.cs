using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public class CatalogoPlanes : Conexion
    {
        public Plan GetOne(int Id)
        {
            Plan p = new Plan();
            try
            {
                this.OpenConnection();

                SqlCommand cmdPlanes = new SqlCommand("SELECT * FROM planes WHERE id_plan=@id", Con);
                cmdPlanes.Parameters.Add("@id", SqlDbType.Int).Value = Id;

                SqlDataReader drPlanes = cmdPlanes.ExecuteReader();

                if (drPlanes.Read())
                {
                    p.Id = (int)drPlanes["id_plan"];
                    p.DescripcionPlan = (string)drPlanes["desc_plan"];
                    p.Especialidad = new CatalogoEspecialidad().getOne((int)drPlanes["id_especialidad"]);

                }

                drPlanes.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos de plan", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return p;
        }

        public List<Plan> GetAll()
        {
            Plan p = new Plan();
            List<Plan> planes = new List<Plan>();
            try
            {
                this.OpenConnection();

                SqlCommand cmdPlanes = new SqlCommand("SELECT * FROM planes", Con);

                SqlDataReader drPlanes = cmdPlanes.ExecuteReader();

                while (drPlanes.Read())
                {
                    p = new Plan();
                    p.Id = (int)drPlanes["id_plan"];
                    p.DescripcionPlan = (string)drPlanes["desc_plan"];
                    p.Especialidad = new CatalogoEspecialidad().getOne((int)drPlanes["id_especialidad"]);
                    planes.Add(p);

                }

                drPlanes.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos de plan", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return planes;
        }


        #region METODOS PARA EL ABM
        public void Save(Plan plan)
        {
            if (plan.State == Entidades.EntidadBase.States.Deleted)
            {
                this.Delete(plan.Id);
            }
            else if (plan.State == Entidades.EntidadBase.States.New)
            {
                this.Insert(plan);
            }
            else if (plan.State == Entidades.EntidadBase.States.Modified)
            {
                this.Update(plan);
            }
            plan.State = Entidades.EntidadBase.States.Unmodified;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE planes WHERE id_plan=@id", Con);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteReader();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al eliminar plan", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Update(Plan plan)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE planes SET desc_plan@descripcion, id_especialidad = @idEspec " +
                    "WHERE id_plan=@id", Con);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = plan.Id;
                cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = plan.DescripcionPlan;
                cmdSave.Parameters.Add("@idEspec", SqlDbType.Int).Value = plan.Especialidad.Id;
                cmdSave.ExecuteReader();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del plan", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(Plan plan)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT INTO planes(desc_plan, id_especialidad) " +
                    "VALUES(@descripcion,@idEspec) " +
                    "SELECT @@identity", //esta linea es para recuperar el ID que asignó el SQL automaticamente
                    Con);
                cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = plan.DescripcionPlan;
                cmdSave.Parameters.Add("@idEspec", SqlDbType.Int).Value = plan.Especialidad.Id;
                /* Aca hay que ver como lo manejamos, porque aparentemente la persona que se le esta asignando el nuevo plan 
                 * vendria a ser la persona que esta usando el plan 
                 * por eso facu hace plan.Per.ID


                */
                // cmdSave.Parameters.Add("@id_persona", SqlDbType.Int).Value = plan.Persona.Id;
                plan.Id = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); // asi se obtiene el ID que asigno al BD automaticamente
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Exception ExcepcionManejada = new Exception("Error al crear plan", Ex);
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

