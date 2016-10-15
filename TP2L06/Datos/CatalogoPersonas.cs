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
    public class CatalogoPersonas : Conexion
    {
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
                new Exception("Error al recuperar datos de usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return p;
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
                new Exception("Error al recuperar datos de usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return personas;
        }
    }
}
