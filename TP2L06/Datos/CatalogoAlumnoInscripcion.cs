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
    public class CatalogoAlumnoInscripcion : Conexion
    {
        public List<AlumnoInscripcion> GetAll()
        {
            List<AlumnoInscripcion> AlumnoInscripcion = new List<AlumnoInscripcion>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdAlumnoInscripcion = new SqlCommand("select * from alumnos_inscripciones", Con);
                SqlDataReader drAlumnoInscripcion = cmdAlumnoInscripcion.ExecuteReader();
                while (drAlumnoInscripcion.Read())
                {
                    AlumnoInscripcion mu = new AlumnoInscripcion();
                    mu.Id = (int)drAlumnoInscripcion["id_inscripcion"];
                    mu.Alumno = new CatalogoPersonas().GetOne((int)drAlumnoInscripcion["id_alumno"]);
                    mu.Curso = new CatalogoCursos().GetOne((int)drAlumnoInscripcion["id_curso"]);
                    mu.Condicion = (String)drAlumnoInscripcion["condicion"];
                    mu.Nota = (int)drAlumnoInscripcion["nota"];
                    AlumnoInscripcion.Add(mu);
                }
                drAlumnoInscripcion.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de AlumnoInscripcion", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return AlumnoInscripcion;
        }

        //public List<AlumnoCursoPersona> ObtenerAlumnosPorCurso(int idCurso)
        //{
        //    List<AlumnoCursoPersona> AlumnoInscripcion = new List<AlumnoCursoPersona>();
        //    try
        //    {
        //        this.OpenConnection();
        //        SqlCommand cmdAlumnoInscripcion = new SqlCommand("select alumnos_inscripciones.id_inscripcion as id_inscripcion, personas.nombre as nombre, personas.apellido as apellido, personas.legajo as legajo, alumnos_inscripciones.condicion as condicion, alumnos_inscripciones.nota as nota from alumnos_inscripciones inner join personas on alumnos_inscripciones.id_alumno = personas.id_persona where alumnos_inscripciones.id_curso = @id_curso", Con);
        //        cmdAlumnoInscripcion.Parameters.Add("@id_curso", SqlDbType.Int).Value = idCurso;
        //        SqlDataReader drAlumnoInscripcion = cmdAlumnoInscripcion.ExecuteReader();
        //        while (drAlumnoInscripcion.Read())
        //        {
        //            AlumnoCursoPersona mu = new AlumnoCursoPersona();
        //            mu.ID = (int)drAlumnoInscripcion["id_inscripcion"];
        //            mu.nombre = (String)drAlumnoInscripcion["nombre"];
        //            mu.apellido = (String)drAlumnoInscripcion["apellido"];
        //            mu.legajo = (int)drAlumnoInscripcion["legajo"];
        //            mu.condicion = (String)drAlumnoInscripcion["condicion"];
        //            mu.nota = (int)drAlumnoInscripcion["nota"];
        //            AlumnoInscripcion.Add(mu);
        //        }
        //        drAlumnoInscripcion.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception ExcepcionManejada = new Exception("Error al recuperar lista de AlumnoInscripcion", ex);
        //        throw ExcepcionManejada;
        //    }
        //    finally
        //    {
        //        this.CloseConnection();
        //    }
        //    return AlumnoInscripcion;
        //}

        public AlumnoInscripcion GetOne(int ID)
        {
            AlumnoInscripcion mu = new AlumnoInscripcion();
            try
            {
                this.OpenConnection();
                SqlCommand cmdAlumnoInscripcions = new SqlCommand("select * from alumnos_inscripciones where id_inscripcion = @id", Con);
                cmdAlumnoInscripcions.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drAlumnoInscripcion = cmdAlumnoInscripcions.ExecuteReader();
                if (drAlumnoInscripcion.Read())
                {
                    mu.Id = (int)drAlumnoInscripcion["id_inscripcion"];
                    mu.Alumno = new CatalogoPersonas().GetOne((int)drAlumnoInscripcion["id_alumno"]);
                    mu.Curso = new CatalogoCursos().GetOne((int)drAlumnoInscripcion["id_curso"]);
                    mu.Condicion = (String)drAlumnoInscripcion["condicion"];
                    mu.Nota = (int)drAlumnoInscripcion["nota"];
                }
                drAlumnoInscripcion.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de AlumnoInscripcion", ex);
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
                SqlCommand cmdDelete = new SqlCommand("delete alumnos_inscripciones where id_inscripcion = @id", Con);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar AlumnoInscripcion", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(AlumnoInscripcion AlumnoInscripcion)
        {
            if (AlumnoInscripcion.State == Entidades.EntidadBase.States.New)
            {
                this.Insert(AlumnoInscripcion);
            }
            else if (AlumnoInscripcion.State == Entidades.EntidadBase.States.Deleted)
            {
                this.Delete(AlumnoInscripcion.Id);
            }
            else if (AlumnoInscripcion.State == Entidades.EntidadBase.States.Modified)
            {
                this.Update(AlumnoInscripcion);
            }
            AlumnoInscripcion.State = Entidades.EntidadBase.States.Unmodified;
        }

        public void Update(AlumnoInscripcion AlumnoInscripcion)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("UPDATE alumnos_inscripciones SET id_alumno = @id_alumno, id_curso = @id_curso, condicion = @condicion, nota=@nota" +
                    " WHERE id_inscripcion = @id", Con);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = AlumnoInscripcion.Id;
                cmdSave.Parameters.Add("@id_alumno", SqlDbType.Int).Value = AlumnoInscripcion.Alumno.Id;
                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = AlumnoInscripcion.Curso.Id;
                cmdSave.Parameters.Add("@condicion", SqlDbType.VarChar, 50).Value = AlumnoInscripcion.Condicion;
                cmdSave.Parameters.Add("@nota", SqlDbType.Int).Value = AlumnoInscripcion.Nota;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del AlumnoInscripcion", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(AlumnoInscripcion AlumnoInscripcion)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("INSERT into alumnos_inscripciones (id_alumno,id_curso,condicion,nota) values(@alumno,@curso,@condicion,@nota) select @@identity", Con);
                cmdSave.Parameters.Add("@id_alumno", SqlDbType.Int).Value = AlumnoInscripcion.Alumno.Id;
                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = AlumnoInscripcion.Curso.Id;
                cmdSave.Parameters.Add("@condicion", SqlDbType.VarChar, 50).Value = AlumnoInscripcion.Condicion;
                cmdSave.Parameters.Add("@nota", SqlDbType.Int).Value = AlumnoInscripcion.Nota;
                AlumnoInscripcion.Id = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear AlumnoInscripcion", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
    }
}
