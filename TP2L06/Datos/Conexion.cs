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
        //private String usuario = "DESKTOP-JK0GQK6\\Martin";
        //private String contraseña = "";
        //private String server = "DESKTOP-JK0GQK6\\SQLEXPRESS";
        
        ////NICO
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
        private String usuario = @"LEOPERETTI\LEO";
        private String contraseña = "123peretto";
        private String server = "LEOPERETTI";

        //Clave por defecto a utlizar para la cadena de conexion
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
