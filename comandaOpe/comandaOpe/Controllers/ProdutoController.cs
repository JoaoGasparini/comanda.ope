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
        private List<Produto> ltProduto;

        public ProdutoController()
        {
            ltProduto = new List<Produto>();
        }

        #region Produto
        public IActionResult Produto()
        {
            ltProduto = new ProdutoModel().Listar().ToList();

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
                new ProdutoModel().Inserir(produto);
                ltProduto = new ProdutoModel().Listar().ToList();

            }
            catch (Exception e)
            {

                throw;
            }
            if (ltProduto != null) return View("ListasProdutos", ltProduto);
            else return View("ListasProdutos");
        }
        [HttpGet]
        public IActionResult FormEditarProduto(string id)
        {
            Produto produtoEditavel = new Produto();

            try
            {
                produtoEditavel = new ProdutoModel().Buscar(Convert.ToInt32(id));

            }
            catch (Exception e)
            {
                throw;
            }
            
            return View("FormEditarProduto", produtoEditavel);
        }

        [HttpPost]
        public IActionResult EditarProduto(Produto produto, string id)
        {
            try
            {
                var produtoEditavel = new ProdutoModel().Buscar(produto.id);
                
                produtoEditavel.descricao = produto.descricao;
                produtoEditavel.preco = produto.preco;
                produtoEditavel.categoria = produto.categoria;

                new ProdutoModel().Alterar(produtoEditavel);

                List<Produto> ltProduto = new ProdutoModel().Listar().ToList();

                if (ltProduto != null) return View("ListasProdutos", ltProduto);
                else return View("ListasProdutos");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IActionResult RemoverProduto(string id)
        {
            try
            {
                new ProdutoModel().Remover(Convert.ToInt32(id));

                List<Produto> ltProduto = new ProdutoModel().Listar().ToList();

                if (ltProduto != null) return View("ListasProdutos", ltProduto);
                else return View("ListasProdutos");
            }
            catch (Exception e)
            {

                throw;
            }
        }
        
        #endregion
    }
}
