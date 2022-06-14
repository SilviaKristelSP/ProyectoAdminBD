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
                try
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
                catch(Exception ex)
                {
                    personasBD = null;
                }

            }
            else
            {
                personasBD = null;
            }
            return personasBD;
        }

        public static int RegistrarPersona(Persona persona)
        {
            int respuestaInsercion = -1;
            SqlConnection conexionBDTransacciones = ConexionBDTransacciones.EstablecerConexion();

            if (conexionBDTransacciones != null)
            {
                try
                {
                    SqlCommand comando = new SqlCommand("Person.SPI_Person_Person", conexionBDTransacciones);
                    comando.CommandType = CommandType.StoredProcedure;

                    int emailPromotion = 0;

                    comando.Parameters.AddWithValue("@PersonType", "SC");
                    comando.Parameters.AddWithValue("@NameStyle", false);
                    comando.Parameters.AddWithValue("@Title", persona.Title);
                    comando.Parameters.AddWithValue("@FirstName", persona.FirstName);
                    comando.Parameters.AddWithValue("@MiddleName", persona.MiddleName);
                    comando.Parameters.AddWithValue("@LastName", persona.LastName);
                    comando.Parameters.AddWithValue("@EmailPromotion", emailPromotion);
                    SqlParameter estado = new SqlParameter("@Estado", SqlDbType.Int);
                    estado.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(estado);
                    SqlParameter salida = new SqlParameter("@Salida", SqlDbType.VarChar, 65535);
                    salida.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(salida);

                    SqlParameter businessID = new SqlParameter("@BusinessID", SqlDbType.Int);
                    businessID.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(businessID);

                    int hilerasAfectadas = comando.ExecuteNonQuery();
                    if (hilerasAfectadas > 0)
                    {
                        respuestaInsercion = Convert.ToInt32(comando.Parameters["@BusinessID"].Value);
                    }
                }
                catch(Exception ex)
                {

                }
                
            }

            return respuestaInsercion;
        }

        public static Persona RecuperarPersonaCompleta(int businessId)
        {
            Persona personaBD = new Persona();
            SqlConnection conexionBD = ConexionBDConsultas.EstablecerConexion();
            if(conexionBD != null)
            {
                try
                {
                    SqlCommand comando = new SqlCommand("Person.SPS_Person_FullPerson", conexionBD);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add(new SqlParameter("@BusinessEntityID", businessId));

                    SqlDataReader resultadoBD = comando.ExecuteReader();

                    if (resultadoBD.Read())
                    {
                        String firstN = "";
                        String middleN = "";
                        String lastN = "";
                        personaBD.Id = ((resultadoBD.IsDBNull(0)) ? 0 : resultadoBD.GetInt32(0));
                        firstN = ((resultadoBD.IsDBNull(1)) ? "" : resultadoBD.GetString(1));
                        personaBD.FirstName = firstN;
                        middleN = ((resultadoBD.IsDBNull(2)) ? "" : resultadoBD.GetString(2));
                        personaBD.MiddleName = middleN;
                        lastN = ((resultadoBD.IsDBNull(3)) ? "" : resultadoBD.GetString(3));
                        personaBD.LastName = lastN;
                        personaBD.FullName = firstN + " " + middleN + " " + lastN;
                        personaBD.PersonType = ((resultadoBD.IsDBNull(4)) ? "" : resultadoBD.GetString(4));
                        personaBD.Title = ((resultadoBD.IsDBNull(5)) ? "" : resultadoBD.GetString(5));
                        personaBD.EmailPromotion = ((resultadoBD.IsDBNull(6)) ? 0 : resultadoBD.GetInt32(6));
                        personaBD.IdEmailAddress = ((resultadoBD.IsDBNull(7)) ? 0 : resultadoBD.GetInt32(7));
                        personaBD.EmailAddress = ((resultadoBD.IsDBNull(8)) ? "" : resultadoBD.GetString(8));
                        personaBD.PhoneNumber = ((resultadoBD.IsDBNull(9)) ? "" : resultadoBD.GetString(9));
                        personaBD.PhoneNumberType = ((resultadoBD.IsDBNull(10)) ? 0 : resultadoBD.GetInt32(10));
                        personaBD.IdCreditCard = ((resultadoBD.IsDBNull(11)) ? 0 : resultadoBD.GetInt32(11));
                        personaBD.CardNumber = ((resultadoBD.IsDBNull(12)) ? "" : resultadoBD.GetString(12));
                        personaBD.CardType = ((resultadoBD.IsDBNull(13)) ? "" : resultadoBD.GetString(13));
                        personaBD.ExpMonth = ((resultadoBD.IsDBNull(14)) ? 0 : resultadoBD.GetByte(14));
                        personaBD.ExpYear = ((resultadoBD.IsDBNull(15)) ? 0 : resultadoBD.GetInt16(15));
                    }

                    resultadoBD.Close();
                }
                catch(Exception ex)
                {
                    personaBD = null;
                }

            }
            else
            {
                personaBD = null;
            }
            return personaBD;
        }

        public static bool EditarPersona(Persona persona)
        {
            bool respuestaEdicion = false;
            SqlConnection conexionBDTransacciones = ConexionBDTransacciones.EstablecerConexion();

            if (conexionBDTransacciones != null)
            {
                try
                {
                    SqlCommand comando = new SqlCommand("Person.SPA_Person_Person", conexionBDTransacciones);
                    comando.CommandType = CommandType.StoredProcedure;

                    int emailPromotion = 0;

                    comando.Parameters.AddWithValue("@BussinessEntityID", persona.Id);
                    comando.Parameters.AddWithValue("@PersonType", persona.PersonType);
                    comando.Parameters.AddWithValue("@NameStyle", false);
                    comando.Parameters.AddWithValue("@Title", persona.Title);
                    comando.Parameters.AddWithValue("@FirstName", persona.FirstName);
                    comando.Parameters.AddWithValue("@MiddleName", persona.MiddleName);
                    comando.Parameters.AddWithValue("@LastName", persona.LastName);
                    comando.Parameters.AddWithValue("@EmailPromotion", emailPromotion);
                    SqlParameter estado = new SqlParameter("@Estado", SqlDbType.Int);
                    estado.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(estado);
                    SqlParameter salida = new SqlParameter("@Salida", SqlDbType.VarChar, 65535);
                    salida.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(salida);

                    int hilerasAfectadas = comando.ExecuteNonQuery();
                    if (hilerasAfectadas > 0)
                    {
                        respuestaEdicion = true;
                    }
                }
                catch(Exception ex)
                {

                }
                
            }

            return respuestaEdicion;
        }

        public static bool EliminarPersona(int BusinesID)
        {
            bool respuestaEliminacion = false;
            SqlConnection conexionBDTransacciones = ConexionBDTransacciones.EstablecerConexion();

            if (conexionBDTransacciones != null)
            {
                try
                {
                    SqlCommand comando = new SqlCommand("Person.SPE_Person_Person", conexionBDTransacciones);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@BusinessEntityID", BusinesID);
                    SqlParameter estado = new SqlParameter("@Estado", SqlDbType.Int);
                    estado.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(estado);
                    SqlParameter salida = new SqlParameter("@Salida", SqlDbType.VarChar, 65535);
                    salida.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(salida);

                    int hilerasAfectadas = comando.ExecuteNonQuery();
                    if(hilerasAfectadas > 0)
                    {
                        respuestaEliminacion = true;
                    }
                }
                catch (Exception e)
                {
                    respuestaEliminacion = false;
                }
            }
            return respuestaEliminacion;
        }

    }
}
