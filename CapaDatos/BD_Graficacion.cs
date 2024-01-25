using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class BD_Graficacion
    {
        public List<EN_Graficacion> UsuariosConObjetos()
        {
            try
            {
                List<EN_Graficacion> resultado = new List<EN_Graficacion>();
                string query = "SELECT COUNT(*) as repeticiones, nombre FROM objeto INNER JOIN Persona on objeto.idUsuario = Persona.id GROUP BY nombre;";
                using (SqlConnection sqlConnection = new SqlConnection(new BD_Conexion().conectar()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlConnection.Open();
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.HasRows)
                            {
                                while (sqlDataReader.Read())
                                {
                                    string nombre = sqlDataReader["nombre"].ToString();
                                    int repeticiones = Convert.ToInt32(sqlDataReader["repeticiones"]);
                                    EN_Graficacion datos = new EN_Graficacion
                                    {
                                        repeticiones = repeticiones,
                                        label = nombre
                                    };
                                    resultado.Add(datos);
                                }
                            }
                        }
                        return resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
