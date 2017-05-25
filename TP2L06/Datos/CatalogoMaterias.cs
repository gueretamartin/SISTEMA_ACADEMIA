using Entidades;
using Entidades.CustomEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CatalogoMaterias : Conexion
    {
        RespuestaServidor rs;

        public CatalogoMaterias()
        {
            rs = new RespuestaServidor();
        }

        public List<Materia> getAll()
        {
            List<Materia> materias = new List<Materia>();
            Materia mat = null;
            this.OpenConnection();
            try
            {
                SqlCommand cmdMaterias = new SqlCommand("Select * from materias", Con);
                SqlDataReader drMaterias = cmdMaterias.ExecuteReader();
                while (drMaterias.Read())
                {
                    mat = new Materia();
                    mat.DescripcionMateria = (string)drMaterias["desc_materia"];
                    mat.HorasSemanales = (int)drMaterias["hs_semanales"];
                    mat.HorasTotales = (int)drMaterias["hs_totales"];
                    mat.Id = (int)drMaterias["id_materia"];
                    mat.Plan = new CatalogoPlanes().GetOne((int)drMaterias["id_plan"]);
                    materias.Add(mat);
                }
                drMaterias.Close();
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de usuarios", Ex);
               
            }
            finally
            {
                this.CloseConnection();
            }
            return materias;
        }

        public Materia GetOne(int id)
        {
            Materia mat = new Materia();
            this.OpenConnection();
            try
            {
                SqlCommand cmdMaterias = new SqlCommand("Select * from materias where id_materia = @id", Con);
                cmdMaterias.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataReader drMaterias = cmdMaterias.ExecuteReader();
                while (drMaterias.Read())
                {
                    mat.DescripcionMateria = (string)drMaterias["desc_materia"];
                    mat.HorasSemanales = (int)drMaterias["hs_semanales"];
                    mat.HorasTotales = (int)drMaterias["hs_totales"];
                    mat.Id = (int)drMaterias["id_materia"];
                    mat.Plan = new CatalogoPlanes().GetOne((int)drMaterias["id_plan"]);
                }
                drMaterias.Close();
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de usuarios", Ex);
               
            }
            finally
            {
                this.CloseConnection();
            }
            return mat;
        }

        #region METODOS PARA EL ABM

        public RespuestaServidor Save(Materia mat)
        {
            if (mat.State == Entidades.EntidadBase.States.Deleted)
            {
                rs = this.Delete(mat.Id);
            }
            else if (mat.State == Entidades.EntidadBase.States.New)
            {
                rs = this.Insert(mat);
            }
            else if (mat.State == Entidades.EntidadBase.States.Modified)
            {
                rs = this.Update(mat);
            }
            else
            mat.State = Entidades.EntidadBase.States.Unmodified;
            return rs;
        }

        public RespuestaServidor Delete(int id)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE materias WHERE id_materia=@id", Con);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmdDelete.ExecuteReader();
                rs.Mensaje = "Materia eliminada con éxito";
            }
            catch (Exception Ex)
            {
                if (rs.ContieneExcepcion(Ex, "FK_cursos_materias"))
                    rs.AgregarError("La materia no puede ser eliminada porque tiene cursos asignados");
                 else
                    rs.AgregarExcepcion(Ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return rs;
        }

        public RespuestaServidor Update(Materia mat)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE materias SET desc_materia=@desc, hs_semanales=@hsSem, hs_totales=@hsTot, id_plan=@idPlan WHERE id_materia = @id", Con);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = mat.Id;
                cmdSave.Parameters.Add("@desc", SqlDbType.VarChar, 50).Value = mat.DescripcionMateria;
                cmdSave.Parameters.Add("@hsSem", SqlDbType.Int).Value = mat.HorasSemanales;
                cmdSave.Parameters.Add("@hsTot", SqlDbType.Int).Value = mat.HorasTotales;
                cmdSave.Parameters.Add("@idPlan", SqlDbType.Int).Value = mat.Plan.Id;
                cmdSave.ExecuteReader();
                rs.Mensaje = "Materia modificada con éxito";
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

        public RespuestaServidor Insert(Materia mat)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT INTO materias(desc_materia,hs_semanales,hs_totales,id_plan) " +
                    "VALUES(@desc_materia,@hsSem,@hsTot,@id_plan) " +
                    "SELECT @@identity", //esta linea es para recuperar el ID que asignó el SQL automaticamente
                    Con);
                
                cmdSave.Parameters.Add("@desc_materia", SqlDbType.VarChar, 50).Value = mat.DescripcionMateria;
                cmdSave.Parameters.Add("@hsSem", SqlDbType.Int).Value = mat.HorasSemanales;
                cmdSave.Parameters.Add("@hsTot", SqlDbType.Int).Value = mat.HorasTotales;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = mat.Plan.Id;

                mat.Id = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); // asi se obtiene el ID que asigno al BD automaticamente
                rs.Mensaje = "Materia cargada con éxito";
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
