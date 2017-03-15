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
    public class CatalogoCursos : Conexion
    {
        public List<Curso> getAll()
        {
            List<Curso> cursos = new List<Curso>();
            Curso mat = null;
            this.OpenConnection();
            try
            {
                SqlCommand cmdCursos = new SqlCommand("Select * from cursos", Con);
                SqlDataReader drCursos = cmdCursos.ExecuteReader();
                while (drCursos.Read())
                {
                    mat = new Curso();
                    mat.Cupo = (int)drCursos["cupo"];
                    mat.AnioCalendario = (int)drCursos["anio_calendario"];
                    mat.Materia = new CatalogoMaterias().GetOne((int)drCursos["id_materia"]);
                    mat.Id = (int)drCursos["id_curso"];
                    mat.Comision = new CatalogoComisiones().GetOne((int)drCursos["id_comision"]);
                    cursos.Add(mat);
                }
                drCursos.Close();
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar los cursos", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return cursos;
        }
      

        public Curso GetOne(int id)
        {
            Curso mat = new Curso();
            this.OpenConnection();
            try
            {
                SqlCommand cmdCursos = new SqlCommand("Select * from cursos where id_curso = @id", Con);
                cmdCursos.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataReader drCursos = cmdCursos.ExecuteReader();
                while (drCursos.Read())
                {
                    mat.Cupo = (int)drCursos["cupo"];
                    mat.AnioCalendario = (int)drCursos["anio_calendario"];
                    mat.Materia = new CatalogoMaterias().GetOne((int)drCursos["id_materia"]);
                    mat.Id = (int)drCursos["id_curso"];
                    mat.Comision = new CatalogoComisiones().GetOne((int)drCursos["id_comision"]);
                }
                drCursos.Close();
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar el curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return mat;
        }

        #region METODOS PARA EL ABM

        public void Save(Curso mat)
        {
            if (mat.State == Entidades.EntidadBase.States.Deleted)
            {
                this.Delete(mat.Id);
            }
            else if (mat.State == Entidades.EntidadBase.States.New)
            {
                this.Insert(mat);
            }
            else if (mat.State == Entidades.EntidadBase.States.Modified)
            {
                this.Update(mat);
            }
            mat.State = Entidades.EntidadBase.States.Unmodified;
        }

        public void Delete(int id)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE cursos WHERE id_curso=@id", Con);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmdDelete.ExecuteReader();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al eliminar el curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Update(Curso mat)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE cursos SET id_materia= @idMat, id_comision=@idCom, anio_calendario=@anio, cupo=@cupo WHERE id_curso = @id", Con);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = mat.Id;
                cmdSave.Parameters.Add("@idMat", SqlDbType.Int).Value = mat.Materia.Id;
                cmdSave.Parameters.Add("@idCom", SqlDbType.Int).Value = mat.Comision.Id;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = mat.AnioCalendario;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = mat.Cupo;
                cmdSave.ExecuteReader();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar los datos del curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Insert(Curso mat)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT INTO cursos(id_materia,id_comision,anio_calendario,cupo) " +
                    "VALUES(@idMat,@idCom,@anio,@cupo) " +
                    "SELECT @@identity", //esta linea es para recuperar el ID que asignó el SQL automaticamente
                    Con);

                cmdSave.Parameters.Add("@idMat", SqlDbType.Int).Value = mat.Materia.Id;
                cmdSave.Parameters.Add("@idCom", SqlDbType.Int).Value = mat.Comision.Id;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = mat.AnioCalendario;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = mat.Cupo;

                mat.Id = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); // asi se obtiene el ID que asigno al BD automaticamente
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Exception ExcepcionManejada = new Exception("Error al crear curso", Ex);
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
