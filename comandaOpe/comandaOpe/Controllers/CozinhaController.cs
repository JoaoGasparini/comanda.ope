using comandaOpe.Data;
using comandaOpe.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace comandaOpe.Controllers
{
    public class CozinhaController : Controller
    {
        public IActionResult ListarPedidosEmAndamento()
        {
            try
            {
                List<Pedido> ltPedidos = new PedidoModel().Listar().Where(pedido => pedido.status != true).ToList();

                foreach(var pedido in ltPedidos.ToList())
                {
                    var buscarProduto = new ProdutoModel().Listar().Where(produto => produto.descricao == pedido.descricao_produto).FirstOrDefault();

                    if (buscarProduto.categoria == "Bebidas" || buscarProduto.categoria == "Bebidas Alcoólicas")
                    {
                        ltPedidos.Remove(pedido);
                    }
                }

                return View("PedidosEmAndamento",ltPedidos);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult ConcluirPedido(string id_pedido)
        {
            try
            {
                var pedido = new PedidoModel().Buscar(Convert.ToInt32(id_pedido));

                pedido.status = true;
                new PedidoModel().Alterar(pedido);

                TempData["Sucesso"] = "Pedido encerrado com sucesso !";
                return RedirectToAction("ListarPedidosEmAndamento");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
