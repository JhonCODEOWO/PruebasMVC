using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocio
{
    public class RN_Graficacion
    {
        BD_Graficacion graficacionData = new BD_Graficacion();
        public List<EN_Graficacion> UsuariosConObjetos()
        {
            return graficacionData.UsuariosConObjetos();
        }
    }
}
