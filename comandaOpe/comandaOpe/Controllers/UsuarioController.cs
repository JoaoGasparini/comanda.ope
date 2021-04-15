using comandaOpe.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using comandaOpe.Data;
using comandaOpe.Data.Models;
using System.Linq;
using System.Collections.Generic;
using System;

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

        #region PRODUTO

        [HttpPost]
        public IActionResult CadastrarProduto(List<Produto> ltProdutos)
        {
            try
            {
                ltProdutos.ForEach(produto => new ProdutoModel().Inserir(produto));
            }
            catch (Exception e)
            {
                throw;
            }
            return View(); //ALTERAR A VIEW CORRETA
        }



        #endregion

        #region CLIENTE

        


        #endregion

        

        
        #region COZINHA

        [HttpGet]
        public List<Pedido> ListarPedidos_NaoProntos()
        {
            var ltPedidosNaoProntos = new PedidoModel().Listar().Where(pedido => pedido.status == false).ToList();
            return ltPedidosNaoProntos;
        }

        [HttpPut]
        public IActionResult AlterarPedido_ProntoCozinha(Pedido pedido)
        {
            try
            {
                PedidoModel pedidoModel = new PedidoModel();

                var alterarPedido = pedidoModel.Buscar(pedido.id);
                pedido.status = true;

                pedidoModel.Alterar(alterarPedido);
            }
            catch (Exception e)
            {

                throw e;
            }
            return View();
        }

        #endregion

    }
}
