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
    public class CatalogoEspecialidad : Conexion
    {
        public Especialidad getOne(int Id)
        {
            Especialidad p = new Especialidad();
            try
            {
                this.OpenConnection();

                SqlCommand cmdEspecialidad = new SqlCommand("SELECT * FROM especialidades WHERE id_especialidad=@id", Con);
                cmdEspecialidad.Parameters.Add("@id", SqlDbType.Int).Value = Id;

                SqlDataReader drEspecialidad = cmdEspecialidad.ExecuteReader();

                if (drEspecialidad.Read())
                {
                    p.Id = (int)drEspecialidad["id_especialidad"];
                    p.DescripcionEspecialidad = (string)drEspecialidad["desc_especialidad"];
            }

                drEspecialidad.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar especialidad", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return p;

        }

        public List<Especialidad> getAll()
        {
            Especialidad p = new Especialidad();
            List<Especialidad> especialidades = new List<Especialidad>();
            try
            {
                this.OpenConnection();

                SqlCommand cmdEspecialidad = new SqlCommand("SELECT * FROM especialidades", Con);

                SqlDataReader drEspecialidad = cmdEspecialidad.ExecuteReader();

                while (drEspecialidad.Read())
                {
                    p.Id = (int)drEspecialidad["id_especialidad"];
                    p.DescripcionEspecialidad = (string)drEspecialidad["desc_especialidad"];
                    especialidades.Add(p);
                }

                drEspecialidad.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar especialidades", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return especialidades;
        }

        #region METODOS PARA EL ABM
        public void Save(Especialidad especialidad)
        {
            if (especialidad.State == Entidades.EntidadBase.States.Deleted)
            {
                this.Delete(especialidad.Id);
            }
            else if (especialidad.State == Entidades.EntidadBase.States.New)
            {
                this.Insert(especialidad);
            }
            else if (especialidad.State == Entidades.EntidadBase.States.Modified)
            {
                this.Update(especialidad);
            }
            especialidad.State = Entidades.EntidadBase.States.Unmodified;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE especialidades WHERE id_especialidad=@id", Con);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteReader();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al eliminar especialidad", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Update(Especialidad especialidad)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE especialidades SET desc_especialidad=@descripcion " +
                    "WHERE id_especialidad=@id", Con);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = especialidad.Id;
                cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = especialidad.DescripcionEspecialidad;
                cmdSave.ExecuteReader();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos de la especialidad", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(Especialidad especialidad)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT INTO especialidades(desc_especialidad) " +
                    "VALUES(@descripcion) " +
                    "SELECT @@identity", //esta linea es para recuperar el ID que asignó el SQL automaticamente
                    Con);

                cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = especialidad.DescripcionEspecialidad;
                
                // cmdSave.Parameters.Add("@id_persona", SqlDbType.Int).Value = especialidad.Persona.Id;
                especialidad.Id = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); // asi se obtiene el ID que asigno al BD automaticamente
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Exception ExcepcionManejada = new Exception("Error al crear la especialidad", Ex);
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
