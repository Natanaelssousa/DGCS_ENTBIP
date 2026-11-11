using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public abstract class Conexao<TEntity, TKey>
      where TEntity : class
    {
        protected string StringConnection = ConfigurationManager.ConnectionStrings["DatabaseCrud"].ConnectionString;

        public abstract List<TEntity> BuscaCompletaRetornoLista();
        public abstract List<TEntity> BuscaPorDataRetornoLista(String data);

        public abstract List<TEntity> BuscaRetornoPalavrasChaves(String palavra);
        public abstract TEntity BuscaPorDataRetornoObjeto(String data);
        public abstract TEntity BuscaPorIdRetornoObejto(TKey id);
        public abstract void Salvar(TEntity entity);
        public abstract void Atualizar(TEntity entity);
        public abstract void Remover(TEntity entity);
        public abstract void RemoverPorId(String id);
    }
}