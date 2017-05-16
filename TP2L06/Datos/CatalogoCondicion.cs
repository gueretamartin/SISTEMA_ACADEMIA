using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CatalogoCondicion : Conexion
    {
        public List<Condicion> getAll()
        {
            List<Condicion> condiciones = new List<Condicion>();
            Condicion cond = null;
            this.OpenConnection();
            try
            {
                SqlCommand cmdCursos = new SqlCommand("Select * from condiciones", Con);
                SqlDataReader drCondicion = cmdCursos.ExecuteReader();
                while (drCondicion.Read())
                {
                    cond = new Condicion();
                    cond.Id = (int)drCondicion["id_condicion"];
                    cond.Denominacion = (string)drCondicion["condicion"];
                    condiciones.Add(cond);
                }
                drCondicion.Close();
            }
            catch (SqlException Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar las condiciones", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return condiciones;
        }
    }
}
