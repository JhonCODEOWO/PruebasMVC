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
    }
}