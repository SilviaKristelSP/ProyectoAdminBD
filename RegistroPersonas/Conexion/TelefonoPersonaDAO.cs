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
    internal class TelefonoPersonaDAO
    {


        public static List<TelefonoPersona> RecuperarTelefonos(int BusinessEntityID)
        {
            List<TelefonoPersona> TelefonoBD = new List<TelefonoPersona>();
            SqlConnection conexionBD = ConexionBDConsultas.EstablecerConexion();
            if (conexionBD != null)
            {
                SqlCommand comando = new SqlCommand("Person.SPS_Person.PersonPhone", conexionBD);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@BusinessEntityID", BusinessEntityID));

                SqlDataReader resultadoBD = comando.ExecuteReader();

                while (resultadoBD.Read())
                {
                    TelefonoPersona telefono = new TelefonoPersona();
                    telefono.BusinnesEntityID = ((resultadoBD.IsDBNull(0)) ? 0 : resultadoBD.GetInt32(0));
                    telefono.PhoneNumber = ((resultadoBD.IsDBNull(1)) ? "" : resultadoBD.GetString(1));
                    telefono.PhoneType = ((resultadoBD.IsDBNull(2)) ? 0 : resultadoBD.GetInt32(2));
                    TelefonoBD.Add(telefono);

                }

                resultadoBD.Close();
            }
            else
            {
                TelefonoBD = null;
            }
            return TelefonoBD;
        }
    }
}