using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class TestProvider
    {
        private String dataBase = "academia";
        private SqlConnection con;
        private String usuario = "root";
        private String contraseña = "root";
        private String server = @"LEO\MSSQL2014";
        const string consKeyDefaultCnnString = "ConnStringLocal";

        public SqlConnection Con
        {
            get
            {
                return con;
            }

            set
            {
                con = value;
            }
        }
        public void OpenConnection()
        {
            String datosConexion = "Data Source=" + server + "; Initial Catalog=" + dataBase + ";Integrated Security=true; UID=" + usuario + ";PWD=" + contraseña + ";";
            con = new SqlConnection(datosConexion);
            con.Open();
        }
        public void CloseConnection()
        {
            con.Close();
            con = null;
        }


        public void LeerTabla()
        {
            this.OpenConnection();
            IDbCommand objCmd = Con.CreateCommand();
            objCmd.CommandText = "SELECT * FROM Usuarios";
            IDataReader objDR = objCmd.ExecuteReader();
            while (objDR.Read())
                Console.WriteLine("{0}\t{1}", objDR.GetString(1), objDR.GetString(2));
            this.CloseConnection();
        }
            
    }
}
