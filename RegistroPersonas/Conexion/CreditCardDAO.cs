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
        public static List<CreditCard> RecuperarEmails(int CreditCardID)
        {
            List<CreditCard> CardBD = new List<CreditCard>();
            SqlConnection conexionBD = ConexionBDConsultas.EstablecerConexion();
            if (conexionBD != null)
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
            else
            {
                CardBD = null;
            }
            return CardBD;
        }
    }

}
