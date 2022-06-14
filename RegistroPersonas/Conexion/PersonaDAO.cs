﻿using System;
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

        public static bool RegistrarPersona(Persona persona)
        {
            bool respuestaInsercion = false;
            SqlConnection conexionBDTransacciones = ConexionBDTransacciones.EstablecerConexion();

            if (conexionBDTransacciones != null)
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

                int hilerasAfectadas = comando.ExecuteNonQuery();
                if(hilerasAfectadas > 0)
                {
                    respuestaInsercion = true;
                }
            }

            return respuestaInsercion;
        }
    }
}