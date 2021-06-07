using comandaOpe.Data;
using comandaOpe.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace comandaOpe.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        #region CLIENTE
        public IActionResult Cliente()
        {
            try
            {
                var ltComandas = new ComandaModel().Listar().OrderBy(comanda => comanda.status).OrderBy(comanda => comanda.numero_comanda).ToList();

                return View("Cliente", ltComandas);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ViewResult FormCliente(string comandaID)
        {
            try
            {
                var idInt = Convert.ToInt32(comandaID);
                var ltComanda = new ComandaModel().Listar().Where(comanda => comanda.id == idInt);
                Comanda comanda = new Comanda();

                if (ltComanda != null)
                {
                    comanda = ltComanda.FirstOrDefault();
                }

                return View("AbrirComanda", comanda);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public IActionResult CadastrarCliente(Cliente cliente, string comandaID)
        {
            try
            {
                int idComanda = Convert.ToInt32(comandaID);
                var pesquisaCpf = new ClienteModel().Listar().Where(cli => cliente.cpf == cli.cpf);

                var retorno = false;

                if (pesquisaCpf.Count() == 0)
                {
                    int idCliente = new ClienteModel().Inserir(cliente);

                    retorno = AbrirComanda(idCliente, idComanda, 1);

                    
                }
                else { retorno = AbrirComanda(pesquisaCpf.FirstOrDefault().id, idComanda, 1); }

                if (retorno) { TempData["Sucesso"] = "Comanda aberta ! Clique em Gerenciar comanda para inserir pedidos."; }

                else { TempData["Falha"] = "Erro ao abrir a comanda."; }
            }
            catch (Exception e)
            {
                TempData["Falha"] = "Erro ao abrir a comanda: " + e.Message ;
            }

            return RedirectToAction("Cliente");
        }

        #endregion

        #region COMANDA

        public IActionResult FormComanda()
        {
            try
            {
                var ltComandasSemUso = new ComandaModel().Listar().Where(comanda => comanda.status == false).ToList();

                if (ltComandasSemUso.Count == 0) { TempData["comandaIndisponiveis"] = "Todas as comandas estão em uso."; }

                return View("AbrirComanda", ltComandasSemUso);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AbrirComanda(int idCliente, int idComanda, int idFunc)
        {
            try
            {
                Comanda_Cliente comanda_cliente = new Comanda_Cliente()
                {
                    id_cliente = idCliente,
                    id_comanda = idComanda,
                    status = true
                };

                int idComanda_Cliente = new Comanda_ClienteModel().Inserir(comanda_cliente);

                Comanda_Pedido comanda_pedido = new Comanda_Pedido()
                {
                    id_comanda_cliente = idComanda_Cliente,
                    id_funcionario = idFunc
                };

                new Comanda_PedidoModel().Inserir(comanda_pedido);

                var alterarStatusComanda = new ComandaModel().Listar().Where(comanda => comanda.id == idComanda).FirstOrDefault();
                alterarStatusComanda.status = true;

                new ComandaModel().Alterar(alterarStatusComanda);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ActionResult ListarPedidos(string comandaID, string id_comanda_pedido = null)
        {
            List<Pedido> ltPedidos = new List<Pedido>();
            try
            {
                Comanda_Pedido comandaPedido = new Comanda_Pedido();

                if (id_comanda_pedido != null)
                {
                    comandaPedido.id = Convert.ToInt32(id_comanda_pedido);
                }
                else
                {
                    int idComanda = Convert.ToInt32(comandaID);

                    var comandaCliente = new Comanda_ClienteModel().Listar().Where(cCliente => cCliente.id_comanda == idComanda && cCliente.status == true).FirstOrDefault();

                    comandaPedido = new Comanda_PedidoModel().Listar().Where(comanda => comanda.id_comanda_cliente == comandaCliente.id).FirstOrDefault();
                }

                ltPedidos = new PedidoModel().Listar().Where(pedido => pedido.id_comanda_pedido == comandaPedido.id).ToList();
                if (ltPedidos.Count > 0)
                {
                    foreach (var pedido in ltPedidos)
                    {
                        pedido.total_pagar = (pedido.quantidade * pedido.valor);
                    }
                }
                else
                {
                    var pedido = new Pedido()
                    {
                        id_comanda_pedido = comandaPedido.id
                        
                    };

                    ltPedidos.Add(pedido);
                }

                return View("ListaPedidosFechamentoConta", ltPedidos);
            }
            catch (Exception e)
            {
                var erro = e.Message.ToString();
            }

            return View("ListaPedidosFechamentoConta", ltPedidos);
        }

        [HttpPost]
        public IActionResult FecharComanda(string comanda_Pedido_Id)
        {
            try
            {
                var comandaPedido = new Comanda_PedidoModel().Listar().Where(cPedido => cPedido.id == Convert.ToInt32(comanda_Pedido_Id));
                
                if(comandaPedido.Count() != 0)
                {
                    var comandaCliente = new Comanda_ClienteModel().Listar().Where(cCliente => cCliente.id == comandaPedido.FirstOrDefault().id_comanda_cliente).FirstOrDefault();
                    comandaCliente.status = false;

                    var comanda = new ComandaModel().Listar().Where(comanda => comanda.id == comandaCliente.id_comanda && comanda.status == true).FirstOrDefault();
                    comanda.status = false;

                    new ComandaModel().Alterar(comanda);
                    new Comanda_ClienteModel().Alterar(comandaCliente);

                    TempData["Sucesso"] = "Comanda fechada com sucesso!";
                }
            }

            catch (Exception e)
            {
                TempData["Falha"] = "Erro ao abrir a comanda: " + e.Message;
            }

            return RedirectToAction("Cliente");
        }

        public IActionResult FormNovaComanda()
        {
            try
            {
                var ltComandas = new ComandaModel().Listar().OrderBy(comanda => comanda.status).OrderBy(comanda => comanda.numero_comanda).ToList();
                
                return View("FormNovaComanda", ltComandas);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult InserirNovaComanda(Comanda comanda)
        {
            List<Comanda> ltComandas = new List<Comanda>();
            try
            {
                var inserirComanda = new ComandaModel().Inserir(comanda);

                ltComandas = new ComandaModel().Listar().ToList();
                TempData["Sucesso"] = "Comanda inserida na relação";
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("duplicate key value violates unique"))
                { 
                    TempData["Falha"] = "Numero de comanda ja foi cadastrado."; 
                }
            }

            ltComandas = new ComandaModel().Listar().ToList();

            return View("FormNovaComanda", ltComandas);
        }

        #endregion
    }
}