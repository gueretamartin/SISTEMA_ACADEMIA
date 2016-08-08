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
    public class CatalogoTipoPersona : Conexion
    {
        public TipoPersona GetOne(int Id)
        {
            TipoPersona p = new TipoPersona();
            try
            {
                this.OpenConnection();

                SqlCommand cmdTipoPersona = new SqlCommand("SELECT * FROM tipo_persona WHERE id_tipo_persona=@id", Con);
                cmdTipoPersona.Parameters.Add("@id", SqlDbType.Int).Value = Id;

                SqlDataReader drTipoPersona = cmdTipoPersona.ExecuteReader();

                if (drTipoPersona.Read())
                {
                    p.Id = (int)drTipoPersona["id_tipo_persona"];
                    p.DescripcionTipo = (string)drTipoPersona["desc_tipo_persona"];
                }

                drTipoPersona.Close();
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
