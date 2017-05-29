using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Conexion
    {
        private String dataBase = "academia";
        private SqlConnection con;
        // MARTIN
        //private string usuario = "desktop-jk0gqk6\\martin";
        //private string contraseña = "";
        //private string server = "desktop-jk0gqk6\\sqlexpress";

        //////NICO
        //private string usuario = "desktop-sjrvtal\\nico-";
        //private string contraseña = "";
        //private string server = "desktop-sjrvtal\\sqlexpress";


        //NICO NOTEBOOK
        private String usuario = "root";
        private String contraseña = "root";
        private String server = "NICOCDA";

        //LEO
        //private String usuario = "root";
        //private String contraseña = "root";
        //private String server = @"LEO\MSSQL2014";

        //LEO NOOT
        //private String usuario = @"LEOPERETTI\LEO";
        //private String contraseña = "123peretto";
        //private String server = "LEOPERETTI";

        //Clave por defecto a utlizar para la cadena de conexion
        
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
            String datosConexion = "Data Source="+server+"; Initial Catalog=" + dataBase + ";Integrated Security=true; UID=" + usuario + ";PWD=" + contraseña + ";";
            con = new SqlConnection(datosConexion);
            con.Open();
        }


        public void CloseConnection()
        {
            con.Close();
            con = null;
        }
    }
}
