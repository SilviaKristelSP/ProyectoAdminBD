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
    internal class CreditCardDAO
    {
        public static List<CreditCard> RecuperarCreditCard(int CreditCardID)
        {
            List<CreditCard> CardBD = new List<CreditCard>();
            SqlConnection conexionBD = ConexionBDConsultas.EstablecerConexion();
            if (conexionBD != null)
            {
                try
                {
                    SqlCommand comando = new SqlCommand("Sales.SPS_Sales_CreditCard", conexionBD);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add(new SqlParameter("@CreditCardID", CreditCardID));

                    SqlDataReader resultadoBD = comando.ExecuteReader();

                    while (resultadoBD.Read())
                    {
                        CreditCard card = new CreditCard();
                        card.Id = ((resultadoBD.IsDBNull(0)) ? 0 : resultadoBD.GetInt32(0));
                        card.CardType = ((resultadoBD.IsDBNull(1)) ? "" : resultadoBD.GetString(1));
                        card.CardNumber = ((resultadoBD.IsDBNull(2)) ? "" : resultadoBD.GetString(2));
                        card.ExpMonth = ((resultadoBD.IsDBNull(3)) ? 0 : resultadoBD.GetInt32(3));
                        card.ExpYear = ((resultadoBD.IsDBNull(4)) ? 0 : resultadoBD.GetInt32(4));
                        CardBD.Add(card);

                    }

                    resultadoBD.Close();
                }
                catch (Exception ex)
                {
                    CardBD = null;
                }
                
            }
            else
            {
                CardBD = null;
            }
            return CardBD;
        }

        public static bool RegistrarCreditCard(CreditCard card)
        {
            bool respuestaInsercion = false;
            SqlConnection conexionBDTransacciones = ConexionBDTransacciones.EstablecerConexion();

            if (conexionBDTransacciones != null)
            {
                try
                {
                    SqlCommand comando = new SqlCommand("Sales.SPI_Sales_CreditCard", conexionBDTransacciones);
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@BusinessEntityID", card.CardType);
                    comando.Parameters.AddWithValue("@CardType", card.CardType);
                    comando.Parameters.AddWithValue("@CardNumber", card.CardNumber);
                    comando.Parameters.AddWithValue("@ExpMonth", card.ExpMonth);
                    comando.Parameters.AddWithValue("@ExpYear", card.ExpYear);
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

        public static bool EditarCreditCard(CreditCard card)
        {
            bool respuestaEdicion = false;
            SqlConnection conexionBDTransacciones = ConexionBDTransacciones.EstablecerConexion();

            if (conexionBDTransacciones != null)
            {
                try
                {
                    SqlCommand comando = new SqlCommand("Sales.SPA_Sales_CreditCard", conexionBDTransacciones);
                    comando.CommandType = CommandType.StoredProcedure;


                    comando.Parameters.AddWithValue("@CreditCard", card.Id);
                    comando.Parameters.AddWithValue("@CardType", card.CardType);
                    comando.Parameters.AddWithValue("@CardNumber", card.CardNumber);
                    comando.Parameters.AddWithValue("@ExpMonth", card.ExpMonth);
                    comando.Parameters.AddWithValue("@ExpYear", card.ExpYear);
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
