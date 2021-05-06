using comandaOpe.Data;
using comandaOpe.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace comandaOpe.Controllers
{
    public class ProdutoController : Controller
    {
        #region Produto
        public IActionResult Produto()
        {
            var ltProduto = new ProdutoModel().Listar().ToList();

            if (ltProduto != null) return View("ListasProdutos", ltProduto);

            else return View("ListasProdutos");

        }
        public IActionResult FormInserirProduto()
        {
            return View("FormInserirProduto");
        }

        public IActionResult InserirProduto(Produto produto)
        {
            try
            {
                var retorno = new ProdutoModel().Inserir(produto);

                if (retorno != 0) { TempData["Sucesso"] = "Produto foi adicionado ao catálogo com sucesso"; }
                else { TempData["Falha"] = "Erro ao inserir o produto no catálogo"; }
            }
            catch (Exception e)
            {
                TempData["Falha"] = "Erro: " + e.Message;
            }

            return RedirectToAction("FormInserirProduto");
        }

        [HttpPost]
        public IActionResult FormEditarProduto(string id_produto)
        {
            try
            {
                Produto produtoEditavel = new ProdutoModel().Buscar(Convert.ToInt32(id_produto));
                
                if (produtoEditavel != null) { return View("FormEditarProduto", produtoEditavel); }
                else{ throw new Exception("Erro ao carregar o Produto" ); }

            }
            catch (Exception e)
            {
                TempData["Falha"] = "Erro ao carregar produto: " + e.Message;

                return RedirectToAction("Produto");
            }
        }

        [HttpPost]
        public IActionResult EditarProduto(Produto produto)
        {
            try
            {
                var produtoEditavel = new ProdutoModel().Buscar(produto.id);
                
                produtoEditavel.descricao = produto.descricao;
                produtoEditavel.preco = produto.preco;
                produtoEditavel.categoria = produto.categoria;

                var retorno = new ProdutoModel().Alterar(produtoEditavel);

                if (retorno != 0)
                {
                    TempData["Sucesso"] = "Produto editado com sucesso !";
                }
                else
                {
                    TempData["Falha"] = "Falha ao editar o produto !";
                    return RedirectToAction("Produto");
                }

                return RedirectToAction("Produto");
            }
            catch (Exception e)
            {
                TempData["Falha"] = "Falha ao editar o produto ! Erro: " + e.Message;
                return RedirectToAction("Produto");
            }
        }
        public IActionResult RemoverProduto(string id_produto)
        {
            try
            {
                var retorno = new ProdutoModel().Remover(Convert.ToInt32(id_produto));

                if (retorno)
                {
                    TempData["Sucesso"] = "Produto removido com sucesso !";
                }
                else
                {
                    TempData["Falha"] = "Falha ao remover o produto.";
                }

                return RedirectToAction("Produto");
            }
            catch (Exception e)
            {
                TempData["Falha"] = "Falha ao editar o produto ! Erro: " + e.Message;
                return RedirectToAction("Produto");
            }
        }
        
        #endregion
    }
}
