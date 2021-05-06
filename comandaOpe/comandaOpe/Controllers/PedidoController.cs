using comandaOpe.Data;
using comandaOpe.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace comandaOpe.Controllers
{
    public class PedidoController : Controller
    {
        #region Pedido

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
                var produto = new ProdutoModel().Buscar(Convert.ToInt32(produtoID));

                var intComandaID = Convert.ToInt32(id_comanda_pedido);
                var intQuantidade = Convert.ToInt32(quantidade);
                var doubleValor = Convert.ToDouble(valor);

                var novoPedido = new Pedido()
                {
                    id_comanda_pedido = intComandaID,
                    descricao_produto = produto.descricao,
                    quantidade = intQuantidade,
                    valor = intQuantidade,
                    status = true
                };

                var retorno = new PedidoModel().Inserir(novoPedido);

                if (retorno != 0) { TempData["Sucesso"] = "Pedido inserido na comanda"; }
                else { TempData["Falha"] = "Erro ao inserir pedido na comanda"; }
            }
            catch (Exception e)
            {
                TempData["Falha"] = "Erro ao inserir pedido na comanda: " + e.Message; 
            }

            return RedirectToAction("ListarProdutos", new { id_comanda_pedido = id_comanda_pedido });
        }

        #endregion
    }
}