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
        RespuestaServidor rs;

        public CatalogoCursos()
        {
            rs = new RespuestaServidor();
        }
        public List<Curso> getAll()
        {
            List<Curso> cursos = new List<Curso>();
            Curso cur = null;
            this.OpenConnection();
            try
            {
                SqlCommand cmdCursos = new SqlCommand("Select * from cursos", Con);
                SqlDataReader drCursos = cmdCursos.ExecuteReader();
                while (drCursos.Read())
                {
                    cur = new Curso();
                    cur.Cupo = (int)drCursos["cupo"];
                    cur.AnioCalendario = (int)drCursos["anio_calendario"];
                    cur.Materia = new CatalogoMaterias().GetOne((int)drCursos["id_materia"]);
                    cur.Id = (int)drCursos["id_curso"];
                    cur.Denominacion = (string)drCursos["denominacion"];
                    cursos.Add(cur);
                }
                drCursos.Close();
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar los cursos", Ex);
               
            }
            finally
            {
                this.CloseConnection();
            }
            return cursos;
        }

        public List<Curso> getAllPorCondicion(string where)
        {
            List<Curso> cursos = new List<Curso>();
            Curso cur = null;
            this.OpenConnection();
            try
            {
                SqlCommand cmdCursos = new SqlCommand("Select * from cursos "+where, Con);
                SqlDataReader drCursos = cmdCursos.ExecuteReader();
                while (drCursos.Read())
                {
                    cur = new Curso();
                    cur.Cupo = (int)drCursos["cupo"];
                    cur.AnioCalendario = (int)drCursos["anio_calendario"];
                    cur.Materia = new CatalogoMaterias().GetOne((int)drCursos["id_materia"]);
                    cur.Id = (int)drCursos["id_curso"];
                    cur.Denominacion = (string)drCursos["denominacion"];
                    cursos.Add(cur);
                }
                drCursos.Close();
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar los cursos", Ex);

            }
            finally
            {
                this.CloseConnection();
            }
            return cursos;
        }

        public List<Curso> GetCursosPlanNoAlumno(int idPlan, int idAlumno)
        {
            string query = "SELECT cursos.* FROM cursos INNER JOIN materias AS m ON m.id_materia = cursos.id_materia INNER JOIN planes AS p ON  p.id_plan = m.id_plan WHERE p.id_plan = @idPlan AND cursos.cupo > 0  AND cursos.id_curso NOT IN (SELECT id_curso FROM alumnos_inscripciones WHERE id_alumno = @idALumno)";
            List<Curso> cursos = new List<Curso>();
            Curso cur = null;
            this.OpenConnection();
            try
            {
                SqlCommand cmdCursos = new SqlCommand(query, Con);
                cmdCursos.Parameters.Add("@idPlan", SqlDbType.Int).Value = idPlan;
                cmdCursos.Parameters.Add("@idAlumno", SqlDbType.Int).Value = idAlumno;
                SqlDataReader drCursos = cmdCursos.ExecuteReader();
                while (drCursos.Read())
                {
                    cur = new Curso();
                    cur.Cupo = (int)drCursos["cupo"];
                    cur.AnioCalendario = (int)drCursos["anio_calendario"];
                    cur.Materia = new CatalogoMaterias().GetOne((int)drCursos["id_materia"]);
                    cur.Id = (int)drCursos["id_curso"];
                    cur.Denominacion = (string)drCursos["denominacion"];
                    cursos.Add(cur);
                }
                drCursos.Close();
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar los cursos", Ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return cursos;
        }

        public Curso GetOneByInscripcionAlumno(int idInscripcion)
        {
            Curso cur = new Curso();
            this.OpenConnection();
            try
            {
                SqlCommand cmdCursos = new SqlCommand("SELECT c.* FROM alumnos_inscripciones INNER JOIN cursos AS c ON c.id_curso = alumnos_inscripciones.id_curso WHERE id_inscripcion = @idInscripcion", Con);
                cmdCursos.Parameters.Add("@idInscripcion", SqlDbType.Int).Value = idInscripcion;
                SqlDataReader drCursos = cmdCursos.ExecuteReader();
                while (drCursos.Read())
                {
                    cur.Cupo = (int)drCursos["cupo"];
                    cur.AnioCalendario = (int)drCursos["anio_calendario"];
                    cur.Materia = new CatalogoMaterias().GetOne((int)drCursos["id_materia"]);
                    cur.Id = (int)drCursos["id_curso"];
                    cur.Denominacion = (string)drCursos["denominacion"];

                }
                drCursos.Close();
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar el curso", Ex);
               
            }
            finally
            {
                this.CloseConnection();
            }
            return cur;
        }

        public List<Curso> GetCursosPlan(int idPlan)
        {
            string query = "SELECT cursos.* FROM cursos INNER JOIN materias AS m ON m.id_materia = cursos.id_materia INNER JOIN planes AS p ON  p.id_plan = m.id_plan WHERE p.id_plan = @idPlan AND cursos.cupo > 0 ";
            List<Curso> cursos = new List<Curso>();
            Curso cur = null;
            this.OpenConnection();
            try
            {
                SqlCommand cmdCursos = new SqlCommand(query, Con);
                cmdCursos.Parameters.Add("@idPlan", SqlDbType.Int).Value = idPlan;
                SqlDataReader drCursos = cmdCursos.ExecuteReader();
                while (drCursos.Read())
                {
                    cur = new Curso();
                    cur.Cupo = (int)drCursos["cupo"];
                    cur.AnioCalendario = (int)drCursos["anio_calendario"];
                    cur.Materia = new CatalogoMaterias().GetOne((int)drCursos["id_materia"]);
                    cur.Id = (int)drCursos["id_curso"];
                    cur.Denominacion = (string)drCursos["denominacion"];
                    cursos.Add(cur);
                }
                drCursos.Close();
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar los cursos", Ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return cursos;

        }



        public Curso GetOne(int id)
        {
            Curso cur = new Curso();
            this.OpenConnection();
            try
            {
                SqlCommand cmdCursos = new SqlCommand("Select * from cursos where id_curso = @id", Con);
                cmdCursos.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataReader drCursos = cmdCursos.ExecuteReader();
                while (drCursos.Read())
                {
                    cur.Cupo = (int)drCursos["cupo"];
                    cur.AnioCalendario = (int)drCursos["anio_calendario"];
                    cur.Materia = new CatalogoMaterias().GetOne((int)drCursos["id_materia"]);
                    cur.Id = (int)drCursos["id_curso"];
                    cur.Denominacion = (string)drCursos["denominacion"];

                }
                drCursos.Close();
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar el curso", Ex);
               
            }
            finally
            {
                this.CloseConnection();
            }
            return cur;
        }

        #region METODOS PARA EL ABM

        public RespuestaServidor Save(Curso cur)
        {
            RespuestaServidor sr = new RespuestaServidor();
            if (cur.State == Entidades.EntidadBase.States.Deleted)
            {
                sr = this.Delete(cur.Id);
            }
            else if (cur.State == Entidades.EntidadBase.States.New)
            {
                this.Insert(cur);
            }
            else if (cur.State == Entidades.EntidadBase.States.Modified)
            {
                this.Update(cur);
            }
            cur.State = Entidades.EntidadBase.States.Unmodified;
            return sr;
        }

        public RespuestaServidor Delete(int id)
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
                if (rs.ContieneExcepcion(Ex, "FK_alumnos_inscripciones_cursos"))
                {
                    rs.AgregarError("No se puede eliminar el curso, porque existen alumnos inscriptos.");
                }
            }
            finally
            {
                this.CloseConnection();
            }
            return rs;
        }

        public RespuestaServidor Update(Curso cur)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE cursos SET id_materia= @idMat,  anio_calendario=@anio, cupo=@cupo, denominacion= @denominacion WHERE id_curso = @id", Con);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = cur.Id;
                cmdSave.Parameters.Add("@idMat", SqlDbType.Int).Value = cur.Materia.Id;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = cur.AnioCalendario;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = cur.Cupo;
                cmdSave.Parameters.Add("@denominacion", SqlDbType.VarChar).Value = cur.Cupo;
                cmdSave.ExecuteReader();
                rs.Mensaje = "Curso modificado correctamente";
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

        public RespuestaServidor Insert(Curso cur)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT INTO cursos(id_materia,anio_calendario,cupo,denominacion) " +
                    "VALUES(@idMat,@anio,@cupo,@denominacion) " +
                    "SELECT @@identity", //esta linea es para recuperar el ID que asignó el SQL autocuricamente
                    Con);

                cmdSave.Parameters.Add("@idMat", SqlDbType.Int).Value = cur.Materia.Id;

                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = cur.AnioCalendario;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = cur.Cupo;
                cmdSave.Parameters.Add("@denominacion", SqlDbType.VarChar).Value = cur.Denominacion;


                cur.Id = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); // asi se obtiene el ID que asigno al BD autocuricamente
                rs.Mensaje = "Curso cargado correctamente";
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
