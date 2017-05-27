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
    public class CatalogoPersonas : Conexion
    {
        RespuestaServidor rs;

        public CatalogoPersonas()
        {
            rs = new RespuestaServidor(); 
        }


        public Personas GetOne(int Id)
        {
            Personas p = new Personas();
            try
            {
                this.OpenConnection();

                SqlCommand cmdPersonas = new SqlCommand("SELECT * FROM personas WHERE id_persona=@id", Con);
                cmdPersonas.Parameters.Add("@id", SqlDbType.Int).Value = Id;

                SqlDataReader drPersonas = cmdPersonas.ExecuteReader();

                if (drPersonas.Read())
                {
                    p.Id = (int)drPersonas["id_persona"];
                    p.Nombre = (string)drPersonas["nombre"];
                    p.Apellido = (string)drPersonas["apellido"];
                    p.Direccion = (string)drPersonas["direccion"];
                    p.Email = (string)drPersonas["email"];
                    p.Telefono = (string)drPersonas["telefono"];
                    p.FechaNacimiento = (DateTime)drPersonas["fecha_nac"];
                    p.Legajo = (int)drPersonas["legajo"];
                    p.TipoPersona = new CatalogoTipoPersona().GetOne((int)drPersonas["id_tipo_persona"]);
                    p.Plan = new CatalogoPlanes().GetOne((int)drPersonas["id_plan"]);
                }

                drPersonas.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos de la persona", Ex);
               
            }
            finally
            {
                this.CloseConnection();
            }
            return p;
        }

        public List<Personas> GetAllAlumnosPlan(int IdPlan)
        {
            List<Personas> personas = new List<Personas>();
            Personas p = null;
            try
            {
                this.OpenConnection();
                SqlCommand cmdPersonas = new SqlCommand("select p.* from personas p  inner join planes pl on pl.id_plan = p.id_plan where pl.id_plan = @IdPlan and p.id_tipo_persona = 2", Con);
                cmdPersonas.Parameters.Add("@IdPlan", SqlDbType.Int).Value = IdPlan;
                SqlDataReader drPersonas = cmdPersonas.ExecuteReader();
                while (drPersonas.Read())
                {
                    p = new Personas();
                    p.Id = (int)drPersonas["id_persona"];
                    p.Nombre = (string)drPersonas["nombre"];
                    p.Apellido = (string)drPersonas["apellido"];
                    p.Direccion = (string)drPersonas["direccion"];
                    p.Email = (string)drPersonas["email"];
                    p.Telefono = (string)drPersonas["telefono"];
                    p.FechaNacimiento = (DateTime)drPersonas["fecha_nac"];
                    p.Legajo = (int)drPersonas["legajo"];
                    p.TipoPersona = new CatalogoTipoPersona().GetOne((int)drPersonas["id_tipo_persona"]);
                    p.Plan = new CatalogoPlanes().GetOne((int)drPersonas["id_plan"]);
                    personas.Add(p);
                }
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos de persona", Ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return personas;
        }



        public List<Personas> GetAll()
        {
            List<Personas> personas = new List<Personas>();
            Personas p = null;
            try
            {
                this.OpenConnection();
                SqlCommand cmdPersonas = new SqlCommand("Select * from personas", Con);
                SqlDataReader drPersonas = cmdPersonas.ExecuteReader();
                while(drPersonas.Read())
                {
                    p = new Personas();
                    p.Id = (int)drPersonas["id_persona"];
                    p.Nombre = (string)drPersonas["nombre"];
                    p.Apellido = (string)drPersonas["apellido"];
                    p.Direccion = (string)drPersonas["direccion"];
                    p.Email = (string)drPersonas["email"];
                    p.Telefono = (string)drPersonas["telefono"];
                    p.FechaNacimiento = (DateTime)drPersonas["fecha_nac"];
                    p.Legajo = (int)drPersonas["legajo"];
                    p.TipoPersona = new CatalogoTipoPersona().GetOne((int)drPersonas["id_tipo_persona"]);
                    p.Plan = new CatalogoPlanes().GetOne((int)drPersonas["id_plan"]);
                    personas.Add(p);
                }
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos de persona", Ex);
               
            }
            finally
            {
                this.CloseConnection();
            }
            return personas;
        }

        public List<Personas> GetAllAlumnos()
        {
            List<Personas> personas = new List<Personas>();
            Personas p = null;
            try
            {
                this.OpenConnection();
                SqlCommand cmdPersonas = new SqlCommand("Select * from personas where id_tipo_persona = 2", Con);
                SqlDataReader drPersonas = cmdPersonas.ExecuteReader();
                while (drPersonas.Read())
                {
                    p = new Personas();
                    p.Id = (int)drPersonas["id_persona"];
                    p.Nombre = (string)drPersonas["nombre"];
                    p.Apellido = (string)drPersonas["apellido"];
                    p.Direccion = (string)drPersonas["direccion"];
                    p.Email = (string)drPersonas["email"];
                    p.Telefono = (string)drPersonas["telefono"];
                    p.FechaNacimiento = (DateTime)drPersonas["fecha_nac"];
                    p.Legajo = (int)drPersonas["legajo"];
                    p.TipoPersona = new CatalogoTipoPersona().GetOne((int)drPersonas["id_tipo_persona"]);
                    p.Plan = new CatalogoPlanes().GetOne((int)drPersonas["id_plan"]);
                    personas.Add(p);
                }
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos de persona", Ex);
               
            }
            finally
            {
                this.CloseConnection();
            }
            return personas;
        }

        public List<Personas> GetAllProfesores()
        {
            List<Personas> personas = new List<Personas>();
            Personas p = null;
            try
            {
                this.OpenConnection();
                SqlCommand cmdPersonas = new SqlCommand("Select * from personas  where id_tipo_persona = 1", Con);
                
                SqlDataReader drPersonas = cmdPersonas.ExecuteReader();
                while (drPersonas.Read())
                {
                    p = new Personas();
                    p.Id = (int)drPersonas["id_persona"];
                    p.Nombre = (string)drPersonas["nombre"];
                    p.Apellido = (string)drPersonas["apellido"];
                    p.Direccion = (string)drPersonas["direccion"];
                    p.Email = (string)drPersonas["email"];
                    p.Telefono = (string)drPersonas["telefono"];
                    p.FechaNacimiento = (DateTime)drPersonas["fecha_nac"];
                    p.Legajo = (int)drPersonas["legajo"];
                    p.TipoPersona = new CatalogoTipoPersona().GetOne((int)drPersonas["id_tipo_persona"]);
                    p.Plan = new CatalogoPlanes().GetOne((int)drPersonas["id_plan"]);
                    personas.Add(p);
                }
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos de persona", Ex);
               
            }
            finally
            {
                this.CloseConnection();
            }
            return personas;
        }


        #region METODOS PARA EL ABM
        public RespuestaServidor Save(Personas mat)
        {
            RespuestaServidor rs = new RespuestaServidor();
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
            mat.State = Entidades.EntidadBase.States.Unmodified;
            return rs;
        }

        public RespuestaServidor Delete(int id)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE personas WHERE id_persona=@id", Con);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmdDelete.ExecuteReader();
                rs.Mensaje = "Persona eliminada correctamente";
            }
            catch (Exception Ex)
            {
                if (rs.ContieneExcepcion(Ex, "FK_alumnos_inscripciones_personas"))
                    rs.AgregarError("El alumno no puede ser eliminado porque está inscripto a un curso");
                else if (rs.ContieneExcepcion(Ex, "FK_usuarios_personas"))
                    rs.AgregarError("La persona tiene un usuario registrado en el sistema, no puede eliminarse");
                else
                    rs.AgregarExcepcion(Ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return rs;
        }

        public RespuestaServidor Update(Personas mat)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE personas SET nombre=@nomb, apellido=@ape, direccion=@dire, email=@email, telefono=@tel, fecha_nac=@fNac, legajo=@legajo, id_tipo_persona=@idTipo, id_plan=@idPlan WHERE id_persona=@id", Con);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = mat.Id;
                cmdSave.Parameters.Add("@nomb", SqlDbType.VarChar, 50).Value = mat.Nombre;
                cmdSave.Parameters.Add("@ape", SqlDbType.VarChar, 50).Value = mat.Apellido;
                cmdSave.Parameters.Add("@dire", SqlDbType.VarChar, 50).Value = mat.Direccion;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = mat.Email;
                cmdSave.Parameters.Add("@tel", SqlDbType.VarChar, 50).Value = mat.Telefono;
                cmdSave.Parameters.Add("@fNac", SqlDbType.DateTime).Value = mat.FechaNacimiento;
                cmdSave.Parameters.Add("@legajo", SqlDbType.Int).Value = mat.Legajo;
                cmdSave.Parameters.Add("@idTipo", SqlDbType.Int).Value = mat.TipoPersona.Id;
                cmdSave.Parameters.Add("@idPlan", SqlDbType.Int).Value = mat.Plan.Id;
                cmdSave.ExecuteReader();
                rs.Mensaje = "Persona modificada correctamente";
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

        public RespuestaServidor Insert(Personas mat)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT INTO personas(nombre,apellido,direccion,email,telefono,fecha_nac,legajo,id_tipo_persona,id_plan) " +
                    "VALUES(@nomb,@ape,@dire,@email,@tel,@fNac,@legajo,@idTipo,@idPlan) " +
                    "SELECT @@identity", //esta linea es para recuperar el ID que asignó el SQL automaticamente
                    Con);
                
                cmdSave.Parameters.Add("@nomb", SqlDbType.VarChar, 50).Value = mat.Nombre;
                cmdSave.Parameters.Add("@ape", SqlDbType.VarChar, 50).Value = mat.Apellido;
                cmdSave.Parameters.Add("@dire", SqlDbType.VarChar, 50).Value = mat.Direccion;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = mat.Email;
                cmdSave.Parameters.Add("@tel", SqlDbType.VarChar, 50).Value = mat.Telefono;
                cmdSave.Parameters.Add("@fNac", SqlDbType.DateTime).Value = mat.FechaNacimiento;
                cmdSave.Parameters.Add("@legajo", SqlDbType.Int).Value = mat.Legajo;
                cmdSave.Parameters.Add("@idTipo", SqlDbType.Int).Value = mat.TipoPersona.Id;
                cmdSave.Parameters.Add("@idPlan", SqlDbType.Int).Value = mat.Plan.Id;

                mat.Id = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); // asi se obtiene el ID que asigno al BD automaticamente
                rs.Mensaje = "Persona insertada correctamente";
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
