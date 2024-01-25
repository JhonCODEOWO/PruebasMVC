using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace PruebasMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult crud()
        {
            return View();
        }

        public ActionResult CRUD_Relaciones()
        {
            return View();
        }

        public ActionResult UI_Productos()
        {
            return View();
        }

        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult usrAdd(EN_Usuario usuario)
        {
            bool result;
            try
            {
                RN_Usuario user_methods = new RN_Usuario();
                result = user_methods.añadirUsuario(usuario);
                if (result == false)
                {
                    return Json(new { succes = true, message = "Ocurrió un error en el metodo añadir usuario" });
                }
                else
                {
                    return Json(new { succes = true, message = "Añadido" });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Fallo al añadir el registro" });
            }
        }

        [HttpGet]
        public JsonResult mostrarUsuarios()
        {
            try
            {
                List<EN_Usuario> usuarios = new RN_Usuario().mostrarUsuarios();
                //Se crea un json en tiempo de ejecución retornado, el nombre de cada grilla es por el cual podremos acceder como response
                return Json(new { success = true, message = "Listo", data = usuarios }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message});
            }
        }

        [HttpGet]
        public JsonResult obtenerUsuario(int id)
        {
            try
            {
                List<EN_Usuario> usuario = new RN_Usuario().obtenerUsuario(id);
                return Json(new { success = true, message = "Se ha obtenido un resultado", data = usuario }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult actualizarUsuario(EN_Usuario usuario)
        {
            bool resultadoOperacion;
            try
            {
                RN_Usuario user_methods = new RN_Usuario();
                resultadoOperacion = user_methods.modificarUsuario(usuario);
                if (resultadoOperacion)
                {
                    return Json(new { success = true, message = "Actualización realizada con éxito" });
                }
                else
                {
                    return Json(new { success = true, message = "Se ha realizado la operación pero ha ocurrido un error en la capa datos" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult eliminarUsuario(int id)
        {
            bool resultado;
            try
            {
                RN_Usuario user_methods = new RN_Usuario();
                resultado = user_methods.eliminarUsuario(id);
                if (resultado)
                {
                    return Json(new { success = true, message = "Eliminación exitosa" });
                }
                else
                {
                    return Json(new { success = false, message = "Se ha realizado la operacion en la capa datos pero se ha recibido un valor falso" });
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AñadirObjeto(EN_Objeto objeto)
        {
            //Se almacena en resultado llo obtenido por el método, que en el caso de ser 1 quiere decir que si hubo una inserción
            string resultado;
            try
            {
                RN_Objeto rn_objeto = new RN_Objeto();
                resultado = rn_objeto.AñadirObjeto(objeto);
                //Resultado a veces tendra cadenas de texto, por lo que debemos usar int.tryparse para recibir un boleano si se pudo convertir sin obtener una excepcion
                if (int.TryParse(resultado, out int filaAfectada) && filaAfectada == 1)
                {
                    return Json(new { success = true, message = resultado }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = resultado }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult EditarObjeto(EN_Objeto objeto)
        {
            string resultado;
            try
            {
                RN_Objeto rn_objeto = new RN_Objeto();
                resultado = rn_objeto.ModificarObjeto(objeto);
                //Si el resultado es 1 y se puede convertir la variable quiere decir que el valor es correcto
                if (int.TryParse(resultado, out int filaAfectada) && filaAfectada == 1)
                {
                    return Json(new { success = true, message = resultado }, JsonRequestBehavior.AllowGet);
                }
                //Si no podemos convertirlo quiere decir que tiene texto, por lo que proviene de una excepción
                else
                {
                    return Json(new { success = false, message = resultado }, JsonRequestBehavior.AllowGet);
                }
            }
            //Retornamos cualquier otra excepción no valorada...
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ListarObjetos()
        {
            List<EN_Objeto> objetos = new List<EN_Objeto>();
            try
            {
                RN_Objeto objeto = new RN_Objeto();
                objetos = objeto.ListarObjetos();
                if (objetos != null)
                {
                    return Json(new { success = true, message = "Se han obtenido los datos", data = objetos }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Se ha devuelto una lista vacía, verifica en la capa datos" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult UsuariosConObjetos()
        {
            try
            {
                List<EN_Graficacion> resultados = new List<EN_Graficacion>();
                RN_Graficacion graficacion = new RN_Graficacion();
                resultados = graficacion.UsuariosConObjetos();
                if (resultados != null)
                {
                    return Json(new { success = true, data = resultados }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, messaje = "Se ha recibido un valor nulo" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, messaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}