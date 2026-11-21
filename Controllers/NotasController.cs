using Microsoft.AspNetCore.Mvc;
using GestorNotas.Models;

namespace GestorNotas.Controllers
{
    public class NotasController : Controller
    {
        private static List<Nota> notas = new List<Nota>();

        private static int contador = 1;

        public IActionResult Index(string busqueda)
        {
            List<Nota> notasMostrar = new List<Nota>();

                    foreach (var nota in notas)
                    {
                        if (string.IsNullOrWhiteSpace(busqueda) ||
                            nota.Titulo.ToLower().Contains(busqueda.ToLower()) ||
                            nota.Descripcion.ToLower().Contains(busqueda.ToLower()) ||
                            nota.Categoria.ToLower().Contains(busqueda.ToLower()))
                        {
                            notasMostrar.Add(nota);
                        }
                    }

            ViewBag.Busqueda = busqueda;

            return View("~/Views/Home/Index.cshtml", notasMostrar);
        }

        [HttpPost]
        public IActionResult Agregar(string titulo, string contenido, string categoria)
        {
                        if (string.IsNullOrWhiteSpace(titulo) ||
                            string.IsNullOrWhiteSpace(contenido))
                        {
                            ViewBag.Error = "ERROR: Completa el titulo y el contenido.";

                            return View("~/Views/Home/Index.cshtml", notas);
                        }

                        Nota nuevaNota = new Nota();

                        nuevaNota.Id = contador;
                        nuevaNota.Titulo = titulo;
                        nuevaNota.Descripcion = contenido;
                        nuevaNota.Categoria = string.IsNullOrWhiteSpace(categoria)
                            ? "General"
                            : categoria;

                        notas.Add(nuevaNota);

                        contador++;

                        return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
                    for (int i = 0; i < notas.Count; i++)
                    {
                        if (notas[i].Id == id)
                        {
                            notas.RemoveAt(i);
                            break;
                        }
                    }

                    return RedirectToAction("Index");
        }
    }
}
