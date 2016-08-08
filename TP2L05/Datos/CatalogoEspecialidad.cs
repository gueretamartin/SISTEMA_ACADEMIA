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
    class CatalogoEspecialidad : Conexion
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
