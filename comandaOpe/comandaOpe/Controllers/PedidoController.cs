using comandaOpe.Data;
using comandaOpe.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace comandaOpe.Controllers
{
    public class PedidoController : Controller
    {
        [HttpGet]
        public IActionResult ListarProdutos(string id_comanda_pedido)
        {
            try
            {
                var ltProdutos = new ProdutoModel().Listar().ToList();

                ltProdutos.ForEach(produto => produto.id_comanda = id_comanda_pedido);

                var gpCategoria = ltProdutos.GroupBy(pCategoria => pCategoria.categoria).OrderBy(pCategoria => pCategoria.Key).ToList();

                return View("FazerPedidos", gpCategoria);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult ListarProduto(string id_comanda_pedido)
        {
            try
            {
                var ltProdutos = new ProdutoModel().Listar().ToList();

                ltProdutos.ForEach(produto => produto.id_comanda = id_comanda_pedido);

                var gpCategoria = ltProdutos.GroupBy(pCategoria => pCategoria.categoria).OrderBy(pCategoria => pCategoria.Key).ToList();

                return View("FazerPedidos", gpCategoria);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult InserirPedido(string quantidade, string id_comanda_pedido, string produtoID, string valor)
        {
            try
            {
                var intComandaID = Convert.ToInt32(id_comanda_pedido);
                var intProdutoID = Convert.ToInt32(produtoID);
                var intQuantidade = Convert.ToInt32(quantidade);
                var doubleValor = Convert.ToDouble(valor);

                var novoPedido = new Pedido()
                {
                    id_comanda_pedido = intComandaID,
                    id_produto = intProdutoID,
                    quantidade = intQuantidade,
                    valor = intQuantidade * doubleValor,
                    status = true
                };

                new PedidoModel().Inserir(novoPedido);

                return RedirectToAction("ListarProdutos", new { id_comanda_pedido = id_comanda_pedido });
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
