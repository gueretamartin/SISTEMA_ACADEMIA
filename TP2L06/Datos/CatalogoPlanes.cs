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
                new Exception("Error al recuperar datos de usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return p;
        }
    }
}

