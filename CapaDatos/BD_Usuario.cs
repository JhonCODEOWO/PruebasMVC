using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data;

namespace CapaDatos
{
    public class BD_Usuario
    {
        public bool agregarUsuario(EN_Usuario usuario)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(new BD_Conexion().conectar()))
                {
                    string query = "INSERT INTO Persona(nombre, apellido, edad, fecha_de_registro) VALUES (@nombre, @apellidos, @edad, @fecharegi)";
                    SqlCommand sqlCommand = new SqlCommand(query, conexion);
                    sqlCommand.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar);
                    sqlCommand.Parameters["@nombre"].Value = usuario.nombre;
                    sqlCommand.Parameters.Add("@apellidos", SqlDbType.VarChar);
                    sqlCommand.Parameters["@apellidos"].Value = usuario.apellidos;
                    sqlCommand.Parameters.Add("@edad", SqlDbType.Int);
                    sqlCommand.Parameters["@edad"].Value = usuario.edad;
                    sqlCommand.Parameters.Add("@fecharegi", SqlDbType.VarChar);
                    sqlCommand.Parameters["@fecharegi"].Value = usuario.fecharegistro;

                    conexion.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                return true;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //Devuelve una lista con todos los registros de la base de datos
        public List<EN_Usuario> mostrarUsuarios()
        {
            List<EN_Usuario> usuarios = new List<EN_Usuario>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(new BD_Conexion().conectar()))
                {
                    string query = "SELECT * FROM Persona";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    //Si tiene filas...
                    if (reader.HasRows)
                    {
                        //Mientras el reader tenga que leer...
                        while (reader.Read())
                        {
                            //Creamos un objeto y añadimos en base a su tipo de dato primero eligiendo el tipo de dato de la fuente, que en este caso es la base de datos y luego el tipo de dato de la clase si se requiere
                            EN_Usuario usuario = new EN_Usuario
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                apellidos = reader.GetString(2),
                                edad = reader.GetInt32(3),
                                fecharegistro = reader.GetDateTime(4).ToString()
                            };
                            //Añadimos el objeto a la lista
                            usuarios.Add(usuario);

                        }
                    }
                    sqlConnection.Close();
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        //Devuelve un solo usuario usada para modificar aunque podrias implementarlo para otros fines como mostrar el nombre del usuario después de un login
        public List<EN_Usuario> obtenerUsuario(int id)
        {
            List<EN_Usuario> usuarios = new List<EN_Usuario>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(new BD_Conexion().conectar()))
                {
                    string query = "SELECT nombre, apellido, edad FROM Persona WHERE id = @id";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    sqlConnection.Open();
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EN_Usuario usuario = new EN_Usuario
                            {
                                nombre = dataReader.GetString(0),
                                apellidos = dataReader.GetString(1),
                                edad = dataReader.GetInt32(2)
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool actualizarUsuario(EN_Usuario usuario)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(new BD_Conexion().conectar()))
                {
                    string query = "UPDATE Persona SET nombre = @nombre, apellido = @apellido, edad = @edad WHERE id = @id";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                    sqlCommand.Parameters.Add("@nombre", SqlDbType.VarChar);
                    sqlCommand.Parameters["@nombre"].Value = usuario.nombre;

                    sqlCommand.Parameters.Add("@apellido", SqlDbType.VarChar);
                    sqlCommand.Parameters["@apellido"].Value = usuario.apellidos;

                    sqlCommand.Parameters.Add("@edad", SqlDbType.Int);
                    sqlCommand.Parameters["@edad"].Value = usuario.edad;

                    sqlCommand.Parameters.Add("@id", SqlDbType.Int);
                    sqlCommand.Parameters["@id"].Value = usuario.id;

                    //sqlCommand.Parameters.Add("@nombre", SqlDbType.VarChar).Value = usuario.nombre;
                    //sqlCommand.Parameters.Add("@apellido", SqlDbType.VarChar).Value = usuario.apellidos;
                    //sqlCommand.Parameters.Add("@edad", SqlDbType.Int).Value = usuario.edad;
                    //sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = usuario.id;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public bool eliminarUsuario(int id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(new BD_Conexion().conectar()))
                {
                    string query = "DELETE FROM Persona WHERE id = @id";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int);
                    sqlCommand.Parameters["@id"].Value = id;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
    }
}
