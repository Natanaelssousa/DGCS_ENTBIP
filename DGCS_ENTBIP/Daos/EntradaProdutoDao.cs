using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class EntradaProdutoDao : Conexao<EntradaProduto, int>
    {
        private string StringConnectionfilial;


        public EntradaProdutoDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }
        public override void Atualizar(EntradaProduto entity)
        {
            throw new NotImplementedException();
        }

        public override List<EntradaProduto> BuscaCompletaRetornoLista()
        {
            throw new NotImplementedException();
        }

        public override List<EntradaProduto> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override EntradaProduto BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override EntradaProduto BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<EntradaProduto> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(EntradaProduto entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(EntradaProduto entity)
        {
          


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "INSERT INTO [dbo].[LOJA_ENTRADAS_PRODUTO] " +
        " ([FILIAL] " +
        " ,[ROMANEIO_PRODUTO] " +
        " ,[PRODUTO] " +
        " ,[COR_PRODUTO] " +
        " ,[QTDE_ENTRADA] " +
        " ,[PRECO1] " +
        " ,[PRECO2] " +
        " ,[PRECO3] " +
        " ,[PRECO4] " +
        " ,[VALOR] " +
        " ,[EN1] " +
        " ,[EN2] " +
        " ,[EN3] " +
        " ,[EN4] " +
        " ,[EN5] " +
        " ,[EN6] " +
        " ,[EN7] " +
        " ,[EN8] " +
        " ,[EN9] " +
        " ,[EN10] " +
        " ,[EN11] " +
        " ,[EN12] " +
        " ,[EN13] " +
        " ,[EN14] " +
        " ,[EN15] " +
        " ,[EN16] " +
        " ,[EN17] " +
        " ,[EN18] " +
        " ,[EN19] " +
        " ,[EN20] " +
        " ,[EN21] " +
        " ,[EN22] " +
        " ,[EN23] " +
        " ,[EN24] " +
        " ,[EN25] " +
        " ,[EN26] " +
        " ,[EN27] " +
        " ,[EN28] " +
        " ,[EN29] " +
        " ,[EN30] " +
        " ,[EN31] " +
        " ,[EN32] " +
        " ,[EN33] " +
        " ,[EN34] " +
        " ,[EN35] " +
        " ,[EN36] " +
        " ,[EN37] " +
        " ,[EN38] " +
        " ,[EN39] " +
        " ,[EN40] " +
        " ,[EN41] " +
        " ,[EN42] " +
        " ,[EN43] " +
        " ,[EN44] " +
        " ,[EN45] " +
        " ,[EN46] " +
        " ,[EN47] " +
        " ,[EN48] " +
        " ,[COR_SORTIDA_TROCADA] " +
        " ,[PEDIDO_COMPRA] " +
        " ,[PEDIDO] " +
        " ,[DATA_PARA_TRANSFERENCIA] " +
        " ,[LJ_PEDIDO_COMPRA] " +
        " ,[LJ_ITEM_PEDIDO] " +
        " ,[ENTREGA_PEDIDO]) " +
 " VALUES " +
      "  ('" + entity.filial + "',  " +
        " '" + entity.romaneio_produto + "',  " +
        " '" + entity.produto + "',  " +
        " '" + entity.cor_produto + "',  " +
        " '" + entity.qtde_entrada + "',  " +
        " '" + entity.preco1 + "',  " +
        " '" + entity.preco2 + "',  " +
        " '" + entity.preco3 + "',  " +
        " '" + entity.preco4 + "',  " +
        " '" + entity.valor + "',  " +
        " '" + entity.en1 + "',  " +
        " '" + entity.en2 + "',  " +
        " '" + entity.en3 + "',  " +
        " '" + entity.en4 + "',  " +
        " '" + entity.en5 + "',  " +
        " '" + entity.en6 + "',  " +
        " '" + entity.en7 + "',  " +
        " '" + entity.en8 + "',  " +
        " '" + entity.en9 + "',  " +
        " '" + entity.en10 + "',  " +
        " '" + entity.en11 + "',  " +
        " '" + entity.en12 + "',  " +
        " '" + entity.en13 + "',  " +
        " '" + entity.en14 + "',  " +
        " '" + entity.en15 + "',  " +
        " '" + entity.en16 + "',  " +
        " '" + entity.en17 + "',  " +
        " '" + entity.en18 + "',  " +
        " '" + entity.en19 + "',  " +
        " '" + entity.en20 + "',  " +
        "'" + entity.en21 + "',  " +
        "'" + entity.en22 + "',  " +
        " '" + entity.en23 + "',  " +
        " '" + entity.en24 + "',  " +
        " '" + entity.en25 + "',  " +
        " '" + entity.en26 + "',  " +
        " '" + entity.en27 + "',  " +
        " '" + entity.en28 + "',  " +
        "'" + entity.en29 + "',  " +
        "'" + entity.en30 + "',  " +
        "'" + entity.en31 + "',  " +
        "'" + entity.en32 + "',  " +
        "'" + entity.en33 + "',  " +
        "'" + entity.en34 + "',  " +
        "'" + entity.en35 + "',  " +
        "'" + entity.en36 + "',  " +
        "'" + entity.en37 + "',  " +
        "'" + entity.en38 + "',  " +
        "'" + entity.en39 + "',  " +
        "'" + entity.en40 + "',  " +
        "'" + entity.en41 + "',  " +
        "'" + entity.en42 + "',  " +
        "'" + entity.en43 + "',  " +
        "'" + entity.en44 + "',  " +
        "'" + entity.en45 + "',  " +
        "'" + entity.en46 + "',  " +
        "'" + entity.en47 + "',  " +
        "'" + entity.en48 + "',  " +
        "'" + entity.cor_sortida_trocada + "',  " +
        "'" + entity.pedido_compra + "',  " +
        " '" + entity.pedido + "',  " +
        " GETDATE(), " +
        " '" + entity.lj_pedido_compra + "',  " +
        "'" + entity.lj_item_pedido + "',  " +
        " GETDATE())  ";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlTransaction objTrans = null;

                try
                {

                    conn.Open();
                    ////--- Start local transaction ---
                    //objTrans = conn.BeginTransaction();

                    cmd.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    //objTrans.Rollback();
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }



        }
    }
}