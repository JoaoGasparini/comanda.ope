﻿using comandaOpe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace comandaOpe.Data
{
    public class DbSistemaComandaContext : DbContext
    {
        private string connectionString;

        public DbSistemaComandaContext() : base()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();
            connectionString = configuration.GetConnectionString("ConnectionString").ToString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(connectionString);
        }

        //TABELAS DE MAPEAMENTO

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Cozinha> Cozinha { get; set; }
        public DbSet<Comanda> Comanda { get; set; }
        public DbSet<Comanda_Pedido> Comanda_Pedido { get; set; }
        public DbSet<Comanda_Cliente> Comanda_Cliente { get; set; }

    }
}
