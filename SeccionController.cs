using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models;

namespace PRUEBAS_LOGIN.Controllers
{
    public class SeccionController : Controller
    {
        private Seccion sec = new Seccion();
        //------------------------Index-----------------------
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.alerta = "info";
            ViewBag.res = "Registrar Nueva Seccion";
            return View(sec.listar());
        }
        [HttpPost] // inserta una seccion
        public ActionResult Index(string nu, bool es, string fe, int cu)
        {
            if (sec.Insertar(nu, es, fe, cu, l_indices))
            {
                ViewBag.alerta = "success";
                ViewBag.res = "Seccion Registrada";
            }
            else
            {
                ViewBag.alerta = "danger";
                ViewBag.res = "Seccion no Resgistrada";
            }
            return View(sec.listar());
        }

        private static List<string> l_indices = new List<string>();
        private static List<string> l_alumnos = new List<string>();

        //------------Busqueda de Alumno y Agregar--------------
        [HttpPost]
        public String bus_alu(string dato_alu)
        {
            string res = "";
            var alumnos = new List<Alumno>();
            alumnos = sec.bus_alu(dato_alu);
            foreach (var a in alumnos)
            {
                int id = a.id_alu;
                string dni = a.dni_alu;
                string nom = a.nombre_alu;
                string ape = a.apellidos_alu;
                string boton_sel = "<button class=\"btn btn-warning\" type='button'"
                    + " onclik=\"agr_alu('" + id + "','" + dni + "','" + nom + "','" + ape + "')\""
                    + " data-dismiss='modal'><span class=\"glyphicon glyphicon-check\"> Añadir</span></button>";
                res = res +
                    "<tr><td>" + id + "</td>"
                    + "<td>" + dni + "</td>"
                    + "<td>" + nom + "</td>"
                    + "<td>" + ape + "</td>"
                    + "<td>" + boton_sel + "</td></tr>";
            }
            return res;
        }
        //----------------------Agregar Alumnos-------------
        public String agr_alu(string id, string dni, string nom, string ape)
        {
            string res = "";
            int cont = 0;
            foreach (var w in l_indices)
            {
                if (w.Equals(id))
                {
                    cont++;
                }
            }
            if (cont == 0)
            {
                if (l_alumnos.Count < 8)
                {
                    string boton_bor = "<button class=\"btn btn-danger\" type='button'"
                        + " onclik=\"bor_alu('" + id + "')\""
                        + "><span class=\"glyphicon glyphicon-trash\"> Borrar</span></button>";
                    l_alumnos.Add(
                        "<tr><td>" + id + "</td>"
                        + "<td>" + dni + "</td>"
                        + "<td>" + nom + "</td>"
                        + "<td>" + ape + "</td>"
                        + "<td>" + boton_bor + "</td></tr>"
                        );
                    l_indices.Add(id);
                }
            }
            foreach (var a in l_alumnos)
            {
                res = res + a;
            }
            return res;
        }

        //--------------Limpieza------------
        [HttpPost]

        public void limpiar_alu()
        {
            l_alumnos.Clear();
            l_indices.Clear();
        }
        //----------------Borrar alumno de lista---------------
        public String bor_alu(string id)
        {
            string res = "";
            l_alumnos.RemoveAt(l_indices.IndexOf(id));
            l_indices.RemoveAt(l_indices.IndexOf(id));
            foreach (var a in l_alumnos)
            {
                res = res + a;
            }
            return res;
        }
    }
}