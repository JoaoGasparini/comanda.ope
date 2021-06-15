using comandaOpe.Data.Models;
using System;
using System.Linq;

namespace comandaOpe.Data
{
    public class BaseModel<T> where T : BaseEntitie
    {
        public static DbSistemaComandaContext dbSistemaComandaContext;

        public BaseModel()
        {
            dbSistemaComandaContext = new DbSistemaComandaContext();
        }

        public virtual int Inserir(T entitie)
        {
            int id = 0;
            try
            {
                dbSistemaComandaContext.Entry(entitie).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                dbSistemaComandaContext.SaveChanges();

                id = entitie.id;
            }
            catch (Exception e)
            {
                throw e;
            }

            return id;
        }

        public int Alterar(T entitie)
        {
            int id = 0;
            try
            {
                dbSistemaComandaContext.Entry(entitie).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                id = dbSistemaComandaContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;

            }
            return id;
        }

        public IQueryable<T> Listar()
        {
            try
            {
                return dbSistemaComandaContext.Set<T>();

            }
            catch (Exception e)
            {
                throw e;

            }
        }
        public T Buscar(int id)
        {
            try
            {
                var listar = Listar();
                return listar.Where(item => item.id == id).FirstOrDefault();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public bool Remover(int id)
        {
            try
            {
                var buscar = Listar().Where(item => item.id == id).FirstOrDefault();
                dbSistemaComandaContext.Remove<T>(buscar);

                var idRetorno = dbSistemaComandaContext.SaveChanges();

                var retorno = true;
                return retorno;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
