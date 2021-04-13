using comandaOpe.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using comandaOpe.Data;
using comandaOpe.Data.Models;
using System.Linq;

namespace comandaOpe.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        #region teste movendo HOMECONTROLLE 
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion


        public IActionResult UsuarioHome()
        {
            return View();
        }

        public IActionResult Painel()
        {
            return View("PainelAdm", "Usuario");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("UsuarioLogin", "Login");
        }

        //public IActionResult CriarComanda()
        //{
        //    DbSistemaComandaContext contexto = new DbSistemaComandaContext();
        //    Pedido pedidoe = new Pedido();
        //    pedidoe
        //    ComandaModel comanda = new ComandaModel();
        //    Comanda comandae = new Comanda();
        //    comandae.id_cliente = new UsuarioModel().Listar().Where(p => p.login == "victor").FirstOrDefault().id;
        //    comandae.forma_pagamento = Request.Form["formapagamento"];
        //    comanda.Inserir(comandae);


        //}
    }

    
}
