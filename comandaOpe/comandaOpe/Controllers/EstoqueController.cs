using comandaOpe.Data;
using comandaOpe.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace comandaOpe.Controllers
{
    public class EstoqueController : Controller
    {
        private EstoqueModel estoqueModel { get; set; }
        public EstoqueController()
        {
            estoqueModel = new EstoqueModel();
        }

        [HttpGet]
        public IActionResult ListarEstoque()
        {
            try
            {
                var ltEstoque = estoqueModel.Listar().ToList();

                return View("ListarEstoque", ltEstoque);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public IActionResult AdicionarItem()
        {
            return View("FormAdicionarItem");
        }
        [HttpPost]
        public IActionResult AdicionarItem(Estoque itemEstoque)
        {
            try
            {
                var novoItemEstoque = estoqueModel.Inserir(itemEstoque);
                
                if (novoItemEstoque > 0) TempData["Sucesso"] = "Item adicionado com sucesso !";
                else TempData["Falha"] = "Problema na inserção do item";

            }
            catch (Exception e)
            {
                if (e.ToString().Contains("duplicate key value violates unique"))
                {
                    TempData["Falha"] = "Esse item ja está cadastrado.";
                }
            }

            return RedirectToAction("ListarEstoque");
        }
        [HttpPost]
        public IActionResult SomarItem(string id, string quantidadeNova)
        {
            try
            {
                var itemSomado = estoqueModel.Buscar(Convert.ToInt32(id));
                itemSomado.quantidade += Convert.ToInt32(quantidadeNova);
                estoqueModel.Alterar(itemSomado);

                return RedirectToAction("ListarEstoque");
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpPost]
        public IActionResult SubtrairItem(string id, string quantidadeNova)
        {
            try
            {
                var itemSubtraido = estoqueModel.Buscar(Convert.ToInt32(id));
                itemSubtraido.quantidade -= Convert.ToInt32(quantidadeNova);
                estoqueModel.Alterar(itemSubtraido);

                return RedirectToAction("ListarEstoque");
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet]
        public IActionResult RemoverItem(string id)
        {
            try
            {
                var itemExcluir = estoqueModel.Remover(Convert.ToInt32(id));

                if (itemExcluir) TempData["Sucessos"] = "Item removido com sucesso !";
                else TempData["Falha"] = "Problema na remoção do item";

                return RedirectToAction("ListarEstoque");
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
