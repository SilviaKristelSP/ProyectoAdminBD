using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RegistroPersonas.Conexion
{
    internal class ConexionBDTransacciones
    {
        public static SqlConnection EstablecerConexion()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "26.126.64.80";
            builder.InitialCatalog = "AdventureWorks2016";
            builder.UserID = "usrAdventureWorks2016";
            builder.Password = "VEFXN%Q$6KXT";

            SqlConnection conexionBD = new SqlConnection(builder.ConnectionString);
            try
            {
                conexionBD.Open();
            }
            catch (Exception exception)
            {
                conexionBD = null;
            }


            return conexionBD;
        }
    }
}
