using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using RegistroPersonas.Clases;

namespace RegistroPersonas.Conexion
{
    internal class PersonaDAO
    {

        public static List<Persona> RecuperarPersonas()
        {
            List<Persona> personasBD = new List<Persona>();
            SqlConnection conexionBD = ConexionBDConsultas.EstablecerConexion();
            if(conexionBD != null)
            {
                SqlCommand comando = new SqlCommand("Person.SPS_Person_Person", conexionBD);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@BusinessEntityID", null));

                SqlDataReader resultadoBD = comando.ExecuteReader();

                while (resultadoBD.Read())
                {
                    Persona persona = new Persona();
                    String firstN = "";
                    String middleN = "";
                    String lastN = "";
                    persona.Id = ((resultadoBD.IsDBNull(0)) ? 0 : resultadoBD.GetInt32(0));
                    firstN = ((resultadoBD.IsDBNull(4)) ? "" : resultadoBD.GetString(4));
                    persona.FirstName = firstN;
                    middleN = ((resultadoBD.IsDBNull(5)) ? "" : resultadoBD.GetString(5));
                    persona.MiddleName = middleN;
                    lastN = ((resultadoBD.IsDBNull(6)) ? "" : resultadoBD.GetString(6));
                    persona.LastName = lastN;
                    persona.FullName = firstN + " " + middleN + " " + lastN;
                    persona.CardNumber = ((resultadoBD.IsDBNull(13)) ? "" : resultadoBD.GetString(13));
                    persona.EmailAddress = ((resultadoBD.IsDBNull(14)) ? "" : resultadoBD.GetString(14));
                    persona.PhoneNumber = ((resultadoBD.IsDBNull(15)) ? "" : resultadoBD.GetString(15));
                    personasBD.Add(persona);
                }

                resultadoBD.Close();

            }
            else
            {
                personasBD = null;
            }
            return personasBD;
        }
    }
}
