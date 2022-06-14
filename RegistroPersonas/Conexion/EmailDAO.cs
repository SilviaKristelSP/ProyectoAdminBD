using RegistroPersonas.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroPersonas.Conexion
{
    internal class EmailDAO
    {
        public static List<Email> RecuperarEmails(int BusinessEntityID)
        {
            List<Email> EmailBD = new List<Email>();
            SqlConnection conexionBD = ConexionBDConsultas.EstablecerConexion();
            if (conexionBD != null)
            {
                SqlCommand comando = new SqlCommand("Person.SPS_Person.EmailAddress", conexionBD);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@BusinessEntityID", BusinessEntityID));

                SqlDataReader resultadoBD = comando.ExecuteReader();

                while (resultadoBD.Read())
                {
                    Email email = new Email();
                    email.BusinessEntityID = ((resultadoBD.IsDBNull(0)) ? 0 : resultadoBD.GetInt32(0));
                    email.EmailID = ((resultadoBD.IsDBNull(1)) ? 0 : resultadoBD.GetInt32(1));
                    email.EmailAddress = ((resultadoBD.IsDBNull(2)) ? "" : resultadoBD.GetString(2));
                    email.rowguid = ((resultadoBD.IsDBNull(3)) ? "" : resultadoBD.GetString(3));
                    EmailBD.Add(email);

                }

                resultadoBD.Close();
            }
            else
            {
                EmailBD = null;
            }
            return EmailBD;
        }
    }
}

