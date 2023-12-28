using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocio
{
    public class RN_Usuario
    {
        BD_Usuario user_methods = new BD_Usuario();
        public bool añadirUsuario(EN_Usuario usuario)
        {
            return user_methods.agregarUsuario(usuario);
        }

        public List<EN_Usuario> mostrarUsuarios()
        {
            return user_methods.mostrarUsuarios();
        }

        public List<EN_Usuario> obtenerUsuario(int id)
        {
            return user_methods.obtenerUsuario(id);
        }

        public bool modificarUsuario(EN_Usuario usuario)
        {
            return user_methods.actualizarUsuario(usuario);
        }
    }
}
