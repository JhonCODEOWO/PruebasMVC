using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaDatos
{
    public class BD_Objeto
    {
        public string AñadirObjeto(EN_Objeto objeto)
        {
            string resultado;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(new BD_Conexion().conectar()))
                {
                    string query = "INSERT INTO objeto VALUES (@idObjeto, @nombreObjeto, @idPersona)";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.Add("@idObjeto", SqlDbType.Int).Value = objeto.id;
                        sqlCommand.Parameters.Add("@nombreObjeto", SqlDbType.VarChar).Value = objeto.nombreObjeto;
                        sqlCommand.Parameters.Add("@idPersona", SqlDbType.Int).Value = objeto.usuarioId;
                        sqlCommand.CommandType = CommandType.Text;

                        sqlConnection.Open();
                        //Para validacion de si es o no exitosa convertir a int
                        resultado =  sqlCommand.ExecuteNonQuery().ToString();
                    }
                }
                return resultado;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                {
                    return "Parece que estas ingresando un dato con un valor ya existente en la base de datos";
                }
                else
                {
                    return ex.Message.ToString();
                }
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string EditarObjeto(EN_Objeto objeto)
        {
            string resultado;
            try
            {
                string query = "UPDATE objeto SET nombreObjeto = @nombreObjeto, idUsuario = @idUsuario WHERE idObjeto = @idObjeto";
                using (SqlConnection sqlConnection = new SqlConnection(new BD_Conexion().conectar()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.Add("@idObjeto", SqlDbType.Int).Value = objeto.id;
                        sqlCommand.Parameters.Add("@nombreObjeto", SqlDbType.VarChar).Value = objeto.nombreObjeto;
                        sqlCommand.Parameters.Add("@idUsuario", SqlDbType.Int).Value = objeto.usuarioId;
                        sqlCommand.CommandType = CommandType.Text;

                        sqlConnection.Open();
                        resultado = sqlCommand.ExecuteNonQuery().ToString();
                    }
                }
                return resultado;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                {
                    return "Parece que estas ingresando un dato con un valor ya existente en la base de datos";
                }
                else
                {
                    return ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public List<EN_Objeto> ListarObjetos()
        {
            List<EN_Objeto> objetos = new List<EN_Objeto>();
            try
            {
                string query = "SELECT idObjeto, nombreObjeto, CONCAT(nombre, ' ', apellido) as usuario FROM objeto INNER JOIN Persona ON Persona.id = objeto.idUsuario;";
                using (SqlConnection sqlConnection = new SqlConnection(new BD_Conexion().conectar()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlConnection.Open();
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                EN_Objeto objeto = new EN_Objeto
                                {
                                    id = Convert.ToInt32(reader["idObjeto"]),
                                    nombreObjeto = reader["nombreObjeto"].ToString(),
                                    datosUsuario = reader["usuario"].ToString()
                                };
                                objetos.Add(objeto);
                            }
                        }
                    }
                }
                return objetos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
