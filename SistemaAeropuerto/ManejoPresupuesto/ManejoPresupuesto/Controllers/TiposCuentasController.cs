using Dapper;
using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController : Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;
        private readonly IServicioUsuarios servicioUsuarios;

        public TiposCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas, 
            IServicioUsuarios servicioUsuarios    ) 
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
            this.servicioUsuarios = servicioUsuarios;
        } 

        public async Task<IActionResult> Index()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tiposCuentas = await repositorioTiposCuentas.Obtener(usuarioId);
            return View(tiposCuentas);
        }
         
        public IActionResult Crear()  
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TipoCuentas tipoCuentas)
        {

            if(!ModelState.IsValid)
            {
                return View(tipoCuentas);
            }

            tipoCuentas.UsuarioId = servicioUsuarios.ObtenerUsuarioId();

            var yaExisteTipoCuenta =
                await repositorioTiposCuentas.Existe(tipoCuentas.Nombre, tipoCuentas.UsuarioId);

            if (yaExisteTipoCuenta)
            {
                ModelState.AddModelError(nameof(tipoCuentas.Nombre), $"El nombre {tipoCuentas.Nombre} ya existe");

                return View(tipoCuentas);
            }

            await repositorioTiposCuentas.Crear(tipoCuentas);

            return RedirectToAction("Index");

        }


        [HttpGet]
        public async Task<ActionResult> Editar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(id, usuarioId);

            if (tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(tipoCuenta);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(TipoCuentas tipoCuentas)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuentaExiste = await repositorioTiposCuentas.ObtenerPorId(tipoCuentas.Id, usuarioId);

            if (tipoCuentaExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioTiposCuentas.Actualizar(tipoCuentas);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Borrar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipocuenta = await repositorioTiposCuentas.ObtenerPorId(id, usuarioId);

            if (tipocuenta is null)
            {
                return RedirectToAction("Noencontrado", "Home");
            }

            return View(tipocuenta);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarTipoCuenta(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipocuenta = await repositorioTiposCuentas.ObtenerPorId(id, usuarioId);

            if (tipocuenta is null)
            {
                return RedirectToAction("Noencontrado", "Home");
            }

            await repositorioTiposCuentas.Borrar(id);
            return RedirectToAction("Index");
        }


        [HttpGet]

        public async Task<IActionResult> VerificarExisteTipoCuenta( string nombre)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(nombre, usuarioId);

            if (yaExisteTipoCuenta)
            {
                return Json($"El nombre {nombre} ya existe");
            }

            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> Ordenar([FromBody] int[] ids)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tiposCuentas = await repositorioTiposCuentas.Obtener(usuarioId);
            var idsTiposCuentas = tiposCuentas.Select(x => x.Id);

            var idsTiposCuentasNoPerteneceAlUusario = ids.Except(idsTiposCuentas).ToList();

            if (idsTiposCuentasNoPerteneceAlUusario.Count > 0)
            {
                return Forbid();
            }

            var tiposCuentasOrdenados = ids.Select((valor, indice) =>
              new TipoCuentas() { Id = valor, Orden = indice + 1 }).AsEnumerable();

            await repositorioTiposCuentas.Ordenar(tiposCuentasOrdenados);




            return Ok();
        }


    }
}
