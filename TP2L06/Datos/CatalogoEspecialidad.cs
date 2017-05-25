using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using Entidades.CustomEntity;

namespace Datos
{
    public class CatalogoEspecialidad : Conexion
    {
        RespuestaServidor rs;

        public CatalogoEspecialidad()
        {
            rs = new RespuestaServidor();
        }
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
               
            }
            finally
            {
                this.CloseConnection();
            }
            return p;

        }

        public List<Especialidad> getAll()
        {
            Especialidad p = null;
            List<Especialidad> especialidades = new List<Especialidad>();
            try
            {
                this.OpenConnection();

                SqlCommand cmdEspecialidad = new SqlCommand("SELECT * FROM especialidades", Con);

                SqlDataReader drEspecialidad = cmdEspecialidad.ExecuteReader();

                while (drEspecialidad.Read())
                {
                    p = new Especialidad();
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
               
            }
            finally
            {
                this.CloseConnection();
            }
            return especialidades;
        }

        #region METODOS PARA EL ABM
        public RespuestaServidor Save(Especialidad especialidad)
        {
            if (especialidad.State == Entidades.EntidadBase.States.Deleted)
            {
                rs = this.Delete(especialidad.Id);
            }
            else if (especialidad.State == Entidades.EntidadBase.States.New)
            {
                rs = this.Insert(especialidad);
            }
            else if (especialidad.State == Entidades.EntidadBase.States.Modified)
            {
                rs = this.Update(especialidad);
            }
            else
                especialidad.State = Entidades.EntidadBase.States.Unmodified;
            return rs;
        }

        public RespuestaServidor Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE especialidades WHERE id_especialidad=@id", Con);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteReader();
                rs.Mensaje = "Especialidad eliminada con éxito";
            }
            catch (Exception Ex)
            {
                if (rs.ContieneExcepcion(Ex, "FK_planes_especialidades"))
                    rs.AgregarError("La especialidad no puede ser eliminada porque tiene cursos asignados");
                else
                    rs.AgregarExcepcion(Ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return rs;
        }

        protected RespuestaServidor Update(Especialidad especialidad)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE especialidades SET desc_especialidad=@descripcion " +
                    "WHERE id_especialidad=@id", Con);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = especialidad.Id;
                cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = especialidad.DescripcionEspecialidad;
                cmdSave.ExecuteReader();
                rs.Mensaje = "Especialidad modificada con éxito";
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

        protected RespuestaServidor Insert(Especialidad especialidad)
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
                rs.Mensaje = "Especialidad cargada con éxito";
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
        #endregion
    }
}
