using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class RN_Objeto
    {
        BD_Objeto bd_objeto = new BD_Objeto();
        public List<EN_Objeto> ListarObjetos()
        {
            return bd_objeto.ListarObjetos();
        }

        public string AñadirObjeto(EN_Objeto objeto)
        {
            return bd_objeto.AñadirObjeto(objeto);
        }

        public string ModificarObjeto(EN_Objeto objeto) 
        {
            return bd_objeto.EditarObjeto(objeto);
        }
    }
}
