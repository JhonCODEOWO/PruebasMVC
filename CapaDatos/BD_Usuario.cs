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
    }
}
