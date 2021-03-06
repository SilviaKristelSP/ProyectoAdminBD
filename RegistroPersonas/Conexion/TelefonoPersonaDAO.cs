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
                try
                {
                    SqlCommand comando = new SqlCommand("Person.SPS_Person.PersonPhone", conexionBD);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add(new SqlParameter("@BusinessEntityID", BusinessEntityID));

                    SqlDataReader resultadoBD = comando.ExecuteReader();

                    while (resultadoBD.Read())
                    {
                        TelefonoPersona telefono = new TelefonoPersona();
                        telefono.BusinessEntityID = ((resultadoBD.IsDBNull(0)) ? 0 : resultadoBD.GetInt32(0));
                        telefono.PhoneNumber = ((resultadoBD.IsDBNull(1)) ? "" : resultadoBD.GetString(1));
                        telefono.PhoneType = ((resultadoBD.IsDBNull(2)) ? 0 : resultadoBD.GetInt32(2));
                        TelefonoBD.Add(telefono);

                    }

                    resultadoBD.Close();
                }
                catch (Exception ex)
                {
                    TelefonoBD = null;
                }
                
            }
            else
            {
                TelefonoBD = null;
            }
            return TelefonoBD;
        }

        public static bool RegistrarTelefono(TelefonoPersona telefono)
        {
            bool respuestaInsercion = false;
            SqlConnection conexionBDTransacciones = ConexionBDTransacciones.EstablecerConexion();

            if (conexionBDTransacciones != null)
            {
                try
                {
                    SqlCommand comando = new SqlCommand("Person.SPI_Person_PersonPhone", conexionBDTransacciones);
                    comando.CommandType = CommandType.StoredProcedure;


                    comando.Parameters.AddWithValue("@BusinessEntityID", telefono.BusinessEntityID);
                    comando.Parameters.AddWithValue("@PhoneNumber", telefono.PhoneNumber);
                    comando.Parameters.AddWithValue("@PhoneNumberTypeID", telefono.PhoneType);
                    SqlParameter estado = new SqlParameter("@Estado", SqlDbType.Int);
                    estado.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(estado);
                    SqlParameter salida = new SqlParameter("@Salida", SqlDbType.VarChar, 65535);
                    salida.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(salida);

                    int hilerasAfectadas = comando.ExecuteNonQuery();
                    if (hilerasAfectadas > 0)
                    {
                        respuestaInsercion = true;
                    }
                }
                catch (Exception ex)
                {

                }
                
            }

            return respuestaInsercion;
        }

        public static bool EditarTelefono(TelefonoPersona telefono)
        {
            bool respuestaEdicion = false;
            SqlConnection conexionBDTransacciones = ConexionBDTransacciones.EstablecerConexion();

            if (conexionBDTransacciones != null)
            {
                try
                {
                    SqlCommand comando = new SqlCommand("Person.SPA_Person_PersonPhone", conexionBDTransacciones);
                    comando.CommandType = CommandType.StoredProcedure;


                    comando.Parameters.AddWithValue("@BusinessEntityID", telefono.BusinessEntityID);
                    comando.Parameters.AddWithValue("@PhoneNumber", telefono.PhoneNumber);
                    comando.Parameters.AddWithValue("@PhoneNumberTypeID", telefono.PhoneType);
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
                catch (Exception ex)
                {

                }   
            }

            return respuestaEdicion;
        }
    }
}