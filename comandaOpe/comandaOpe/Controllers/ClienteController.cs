using comandaOpe.Data;
using comandaOpe.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace comandaOpe.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Cliente()
        {
            return View("Cliente");
        }
        public ViewResult FormCliente()
        {
            var ltComandasSemUso = new ComandaModel().Listar().Where(comanda => comanda.status == false).ToList();

            if (ltComandasSemUso == null)
            {
                TempData["comandaIndisponiveis"] = "Todas as comandas estão em uso.";
                return View("CadastrarCliente", ltComandasSemUso);
            }

            return View("CadastrarCliente", ltComandasSemUso);
        }

        public ViewResult CadastrarCliente(Cliente cliente, string numero_comanda)
        {
            try
            {
                var pesquisaCpf = new ClienteModel().Listar().Where(cli => cliente.cpf == cli.cpf).FirstOrDefault();

                if (pesquisaCpf == null)
                {
                    int idCliente = new ClienteModel().Inserir(cliente);

                    AbrirComanda(idCliente, numero_comanda, 1);

                    return View("Cliente");
                }
                else
                {
                    AbrirComanda(pesquisaCpf.id, numero_comanda, 1);
                    return View("AbrirComanda");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region COMANDA

        public IActionResult FormComanda()
        {
            var ltComandasSemUso = new ComandaModel().Listar().Where(comanda => comanda.status == false).ToList();

            return View("AbrirComanda", ltComandasSemUso);
        }

        [HttpPost]
        public IActionResult PegaCliente_AbrirComanda(string cpf, string numero_comanda, int idFunc)
        {
            try
            {
                var cliente = new ClienteModel().Listar().Where(cliente => cliente.cpf == cpf);

                if (cliente != null)
                {
                    var idCliente = cliente.FirstOrDefault().id;

                    AbrirComanda(idCliente, numero_comanda, 1);
                }
                else
                {
                    TempData["cpfNaoCadastrado"] = "CPF não encontrado, por favor cadastre para abrir uma comanda";
                    return View();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return View("Cliente");
        }

        public void AbrirComanda(int idCliente, string numeroComanda, int idFunc)
        {
            try
            {
                Comanda_Cliente comanda_cliente = new Comanda_Cliente()
                {
                    id_cliente = idCliente,
                    id_comanda = Convert.ToInt32(numeroComanda),
                    status = true
                };

                int idComanda_Cliente = new Comanda_ClienteModel().Inserir(comanda_cliente);

                Comanda_Pedido comanda_pedido = new Comanda_Pedido()
                {
                    id_comanda_cliente = idComanda_Cliente,
                    id_funcionario = idFunc
                };

                new Comanda_PedidoModel().Inserir(comanda_pedido);

                var alterarStatusComanda = new ComandaModel().Listar().Where(comanda => comanda.numero_comanda == Convert.ToInt32(numeroComanda)).FirstOrDefault();
                alterarStatusComanda.status = true;

                new ComandaModel().Alterar(alterarStatusComanda);
            }


            catch (Exception e)
            {

            }
        }
        public IActionResult ListaPedidosFechamentoConta()
        {
            return View("ListaPedidosFechamentoConta");
        }

        public ActionResult ListarPedidos(string numero_comanda)
        {
            List<Pedido> ltPedidos = new List<Pedido>();
            try
            {
                var comanda = new ComandaModel().Listar().Where(comanda => comanda.numero_comanda == Convert.ToInt32(numero_comanda) && comanda.status == true).FirstOrDefault();

                var comandaCliente = new Comanda_ClienteModel().Listar().Where(cCliente => cCliente.id_comanda == comanda.id && cCliente.status == true).FirstOrDefault();

                new ComandaModel().Alterar(comanda);
                new Comanda_ClienteModel().Alterar(comandaCliente);

                var comandaPedido = new Comanda_PedidoModel().Listar().Where(comanda => comanda.id_comanda_cliente == comandaCliente.id).FirstOrDefault();

                ltPedidos = new PedidoModel().Listar().Where(pedido => pedido.id_comanda_pedido == comandaPedido.id).ToList();
                foreach (var pedido in ltPedidos)
                {
                    pedido.total_pagar = (pedido.quantidade * pedido.valor);
                }
            }
            catch (Exception e)
            { }

            return View("ListaPedidosFechamentoConta", ltPedidos);
        }

        [HttpPost]
        public IActionResult FecharComanda(string numero_comanda)
        {
            try
            {
                string valor = Response.ContentType;

                int numeroComanda = Convert.ToInt32(numero_comanda);

                var comanda = new ComandaModel().Listar().Where(comanda => comanda.numero_comanda == numeroComanda && comanda.status == true).FirstOrDefault();
                comanda.status = false;

                var comandaCliente = new Comanda_ClienteModel().Listar().Where(cCliente => cCliente.id_comanda == comanda.id && cCliente.status == true).FirstOrDefault();
                comandaCliente.status = false;

                new ComandaModel().Alterar(comanda);
                new Comanda_ClienteModel().Alterar(comandaCliente);

                TempData["Comanda Fechada"] = "Comanda fechada !";

            }
            catch (Exception e)
            {

                throw e;
            }

            return View();
        }
        public IActionResult FormNovaComanda()
        {
            try
            {
                var ltComandas = new ComandaModel().Listar().ToList();

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

                return View("FormNovaComanda", ltComandas);
            }
            catch (Exception e)
            {
                var erro = e.InnerException.ToString();

                if (erro.Contains("duplicate key value violates unique"))
                {
                    TempData["Falhou"] = "Numero de comanda ja foi cadastrado.";

                };
            }
            ltComandas = new ComandaModel().Listar().ToList();

            return View("FormNovaComanda", ltComandas);
        }
    }

    #endregion
}