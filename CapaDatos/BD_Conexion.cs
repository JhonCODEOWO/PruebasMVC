using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CapaDatos
{
    public class BD_Conexion
    {
        public string conectar()
        {
            return ConfigurationManager.ConnectionStrings["conexion"].ToString();
        }
    }
}
