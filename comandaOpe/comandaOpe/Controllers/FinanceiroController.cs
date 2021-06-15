using comandaOpe.Data;
using comandaOpe.Data.Models;
using comandaOpe.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace comandaOpe.Controllers
{
    public class FinanceiroController : Controller
    {
        public IActionResult Financeiro()
        {
            return View("Financeiro");
        }

        #region comandas

        public IActionResult RelatorioClienteMes()
        {
            try
            {
                var anoMesRelatorio = DateTime.Now.AddMonths(-1);
                var dataInicio = new DateTime(anoMesRelatorio.Year, anoMesRelatorio.Month, anoMesRelatorio.Day, 0, 0, 0);

                List<ClienteRelatorio> ltClienteRelatorios = new List<ClienteRelatorio>();
                
                var comandaFechadas = new Comanda_ClienteModel().Listar().Where(comanda => comanda.status != true && comanda.criado >= dataInicio)
                                                                    .OrderBy(cliente => cliente.id_cliente);

                foreach (var cFechada in comandaFechadas)
                {
                    var somaPedido = 0.00;
                    var quantidadePedidos = 0;

                    var comandaPedido = new Comanda_PedidoModel().Listar().Where(comanda => comanda.id_comanda_cliente == cFechada.id).FirstOrDefault();

                    var ltPedidos = new PedidoModel().Listar().Where(pedido => pedido.id_comanda_pedido == comandaPedido.id);
                    
                    foreach (var pedido in ltPedidos)
                    {
                        somaPedido += pedido.quantidade * pedido.valor;
                    }

                    var cliente = new ClienteModel().Buscar(cFechada.id_cliente);
                    var comanda = new ComandaModel().Buscar(cFechada.id_comanda);

                    ClienteRelatorio clienteRelatorio = new ClienteRelatorio()
                    {
                        nome_cliente = cliente.nome,
                        comanda = comanda.numero_comanda,
                        quantidade_pedidos = ltPedidos.Count(),
                        valor_gasto = somaPedido
                    };

                    ltClienteRelatorios.Add(clienteRelatorio);

                }

                return View("ClienteFinanceiro", ltClienteRelatorios);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public IActionResult RelatorioClienteSemana()
        {
            try
            {
                var anoMesRelatorio = DateTime.Now.AddDays(-7);
                var dataInicio = new DateTime(anoMesRelatorio.Year, anoMesRelatorio.Month, anoMesRelatorio.Day, 0, 0, 0);

                List<ClienteRelatorio> ltClienteRelatorios = new List<ClienteRelatorio>();

                var comandaFechadas = new Comanda_ClienteModel().Listar().Where(comanda => comanda.status != true && comanda.criado >= dataInicio)
                                                                    .OrderBy(cliente => cliente.id_cliente);

                foreach (var cFechada in comandaFechadas)
                {
                    var somaPedido = 0.00;
                    
                    var comandaPedido = new Comanda_PedidoModel().Listar().Where(comanda => comanda.id_comanda_cliente == cFechada.id).FirstOrDefault();

                    var ltPedidos = new PedidoModel().Listar().Where(pedido => pedido.id_comanda_pedido == comandaPedido.id);

                    foreach (var pedido in ltPedidos)
                    {
                        somaPedido += pedido.quantidade * pedido.valor;
                    }

                    var cliente = new ClienteModel().Buscar(cFechada.id_cliente);
                    var comanda = new ComandaModel().Buscar(cFechada.id_comanda);

                    ClienteRelatorio clienteRelatorio = new ClienteRelatorio()
                    {
                        nome_cliente = cliente.nome,
                        comanda = comanda.numero_comanda,
                        quantidade_pedidos = ltPedidos.Count(),
                        valor_gasto = somaPedido
                    };

                    ltClienteRelatorios.Add(clienteRelatorio);

                }

                return View("ClienteFinanceiro", ltClienteRelatorios);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public IActionResult RelatorioClienteDia()
        {
            try
            {
                var anoMesRelatorio = DateTime.Now.AddDays(-1);
                var dataInicio = new DateTime(anoMesRelatorio.Year, anoMesRelatorio.Month, anoMesRelatorio.Day, 0, 0, 0);

                List<ClienteRelatorio> ltClienteRelatorios = new List<ClienteRelatorio>();

                var comandaFechadas = new Comanda_ClienteModel().Listar().Where(comanda => comanda.status != true && comanda.criado >= dataInicio)
                                                                    .OrderBy(cliente => cliente.id_cliente);

                foreach (var cFechada in comandaFechadas)
                {
                    var somaPedido = 0.00;
                    var quantidadePedidos = 0;

                    var comandaPedido = new Comanda_PedidoModel().Listar().Where(comanda => comanda.id_comanda_cliente == cFechada.id).FirstOrDefault();

                    var ltPedidos = new PedidoModel().Listar().Where(pedido => pedido.id_comanda_pedido == comandaPedido.id);

                    foreach (var pedido in ltPedidos)
                    {
                        somaPedido += pedido.quantidade * pedido.valor;
                    }

                    var cliente = new ClienteModel().Buscar(cFechada.id_cliente);
                    var comanda = new ComandaModel().Buscar(cFechada.id_comanda);

                    ClienteRelatorio clienteRelatorio = new ClienteRelatorio()
                    {   
                        nome_cliente = cliente.nome,
                        comanda = comanda.numero_comanda,
                        quantidade_pedidos = ltPedidos.Count(),
                        valor_gasto = somaPedido
                    };

                    ltClienteRelatorios.Add(clienteRelatorio);

                }

                return View("ClienteFinanceiro", ltClienteRelatorios);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        #endregion

        #region Pedidos

        public IActionResult ListarPedidosFrequentesMes()
        {
            try
            {
                var anoMesRelatorio = DateTime.Now.AddMonths(-1);
                var dataInicio = new DateTime(anoMesRelatorio.Year, anoMesRelatorio.Month, anoMesRelatorio.Day, 0, 0, 0);
                
                var ltPedidos = new PedidoModel().Listar().Where(pedido => pedido.criado >= dataInicio).ToList();

                var gpPedidos = ltPedidos.GroupBy(c => c.descricao_produto).ToList();

                return View("ProdutoFinanceiro", gpPedidos);
            }
            catch (Exception e)
            {

                throw;
            }

        }
        public IActionResult ListarPedidosFrequentesSemana()
        {
            try
            {
                var anoMesRelatorio = DateTime.Now.AddDays(-7);
                var dataInicio = new DateTime(anoMesRelatorio.Year, anoMesRelatorio.Month, anoMesRelatorio.Day, 0, 0, 0);

                var ltPedidos = new PedidoModel().Listar().Where(pedido => pedido.criado >= dataInicio).ToList();

                var gpPedidos = ltPedidos.GroupBy(c => c.descricao_produto).ToList();

                return View("ProdutoFinanceiro", gpPedidos);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public IActionResult ListarPedidosFrequentesDia()
        {
            try
            {
                var anoMesRelatorio = DateTime.Now.AddDays(-1);
                var dataInicio = new DateTime(anoMesRelatorio.Year, anoMesRelatorio.Month, anoMesRelatorio.Day, 0, 0, 0);
                
                var ltPedidos = new PedidoModel().Listar().Where(pedido => pedido.criado >= dataInicio).ToList();

                var gpPedidos = ltPedidos.GroupBy(c => c.descricao_produto).ToList();

                return View("ProdutoFinanceiro", gpPedidos);
            }
            catch (Exception e)
            {

                throw;
            }

        }

        #endregion
    }
}
