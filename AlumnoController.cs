using AdminApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminApp.Controllers
{
    public class AlumnoController : Controller
    {
        private Alumno alu = new Alumno();
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.alerta = "info";
            ViewBag.res = "Registrar Nuevo Alumno";
            return View(alu.listar());
        }
        [HttpPost]
        public ActionResult Index(string dni, string nom, string ape)
        {
            if (alu.Insertar(dni, nom, ape))
            {
                ViewBag.alerta = "success";
                ViewBag.res = "El Alumno Ha Sido Registrado";
            }
            else
            {
                ViewBag.alerta = "danger";
                ViewBag.res = "El Alumno No Ha Sido Registrado";
            }
            return View(alu.listar());
        }
        public ActionResult Editar (int id)
        {
            ViewBag.alerta = "info";
            ViewBag.res = "Actualizar Registro De Alumno";
            return View(alu.un_registro(id));
        }
        [HttpPost]
        public ActionResult Editar (int id, string nom, string ape, string dni)
        {
            if (alu.Actualizar(id, nom, ape, dni))
            {
                ViewBag.alerta = "success";
                ViewBag.res = "Datos del Alumno Actualizados";
            }
            else
            {
                ViewBag.alerta = "danger";
                ViewBag.res = "Ha Ocurrido un Error";
            }
            return View(alu.un_registro(id));
        }
        public ActionResult Eliminar (int id)
        {
            if (alu.Eliminar(id))
            {
                return RedirectToAction("Index", "Alumno");
            }
            else
            {
                ViewBag.alerta = "danger";
                ViewBag.res = "El Alumno Se Encuentra Registrado En Una Sección";
                return View (alu.un_registro(id));
            }
        }
    }
}