using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class ProdutoDao : Conexao<Produto, int>
    {
        private string StringConnectionfilial;
        public ProdutoDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }

        public override void Atualizar(Produto entity)
        {
            throw new NotImplementedException();
        }

        public override List<Produto> BuscaCompletaRetornoLista()
        {
            throw new NotImplementedException();
        }

        public override List<Produto> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public List<Produto> BuscaProdutoTodosProdutosNotasSemBipar(string[] id_xml_nfe)
        {
            string ids = "";
            for (int i = 0; i < id_xml_nfe.Count(); i++)
            {
                if (i + 1 == 1 && i + 1 != id_xml_nfe.Count())
                {
                    ids = "'" + id_xml_nfe[i] + "',";
                }
                else if (i + 1 == 1 && i + 1 == id_xml_nfe.Count())
                {
                    ids = "'" + id_xml_nfe[i] + "'";
                }
                else if (i + 1 == id_xml_nfe.Count())
                {
                    ids += "'" + id_xml_nfe[i] + "'";
                }
                else
                {
                    ids += "'" + id_xml_nfe[i] + "',";
                }

            }

            string sql = "select A.NF_ENTRADA,A.CODIGO_BARRA,A.PRODUTO,A.COR_PRODUTO,A.DESC_PRODUTO,SUM(CONVERT(INT,QTDE)) QTDE_TOTAL, " +
            " VALOR_UN PRECO1, NOME_CLIFOR FORNECEDOR,A.TAMANHO,A.CHAVE_NFE,GRUPO_PRODUTO, "+
            " SUBGRUPO_PRODUTO from PRODUTOS_BARRA_XML A left join(SELECT DISTINCT  SS.[value] as IDS_NFS, RESPONSAVEL, " +
            " IDS_NFS_SELECIONADAS from AUX_ENTRADA as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) D " +
            " ON A.ID_XML_NFE = D.IDS_NFS LEFT JOIN AUX_ENTRADA B ON(B.IDS_NFS_SELECIONADAS = D.IDS_NFS_SELECIONADAS) " +
            " AND(A.CODIGO_BARRA = B.CODIGO_BARRA) " +
            " where convert(varchar,A.ID_XML_NFE) in (" + ids + ") AND B.CODIGO_BARRA IS NULL " +
            " GROUP BY A.NF_ENTRADA,A.CODIGO_BARRA,A.PRODUTO,A.COR_PRODUTO,A.DESC_PRODUTO, " +
            " VALOR_UN,NOME_CLIFOR,A.TAMANHO,A.CHAVE_NFE,GRUPO_PRODUTO,SUBGRUPO_PRODUTO ";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                Produto produto = null;
                Produto produtoVazio = new Produto();
                List<Produto> lista = new List<Produto>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new Produto();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.qtde_total = reader["QTDE_TOTAL"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.chave_nfe = reader["CHAVE_NFE"].ToString();
                            produto.grupo = reader["GRUPO_PRODUTO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO_PRODUTO"].ToString();
                            produto.nota_fiscal = reader["NF_ENTRADA"].ToString();
                            produto.ids_nfs_selecionadas = ids;

                            lista.Add(produto);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);

                if (produto == null)
                {
                    return lista;
                }
                else
                {
                    return lista;
                }


            }
        }
        public List<Produto> BuscaProdutoNotasSelecionadasChave(string chave, string[] id_xml_nfe)
        {
            string ids = "";
            for (int i = 0; i < id_xml_nfe.Count(); i++)
            {
                if (i + 1 == 1 && i + 1 != id_xml_nfe.Count())
                {
                    ids = "'" + id_xml_nfe[i] + "',";
                }
                else if (i + 1 == 1 && i + 1 == id_xml_nfe.Count())
                {
                    ids = "'" + id_xml_nfe[i] + "'";
                }
                else if (i + 1 == id_xml_nfe.Count())
                {
                    ids += "'" + id_xml_nfe[i] + "'";
                }
                else
                {
                    ids += "'" + id_xml_nfe[i] + "',";
                }

            }

            string sql = "select CODIGO_BARRA,PRODUTO,COR_PRODUTO,DESC_PRODUTO,SUM(CONVERT(INT,QTDE)) QTDE_TOTAL," +
                " VALOR_UN PRECO1,NOME_CLIFOR FORNECEDOR,TAMANHO,CHAVE_NFE,GRUPO_PRODUTO,SUBGRUPO_PRODUTO from PRODUTOS_BARRA_XML where ID_XML_NFE in (" + ids + ") " +
                " AND CHAVE_NFE = '" + chave + "' GROUP BY CODIGO_BARRA,PRODUTO,COR_PRODUTO,DESC_PRODUTO," +
                " VALOR_UN,NOME_CLIFOR,TAMANHO,CHAVE_NFE,GRUPO_PRODUTO,SUBGRUPO_PRODUTO  ";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                Produto produto = null;
                Produto produtoVazio = new Produto();
                List<Produto> lista = new List<Produto>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new Produto();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.qtde_total = reader["QTDE_TOTAL"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.chave_nfe = reader["CHAVE_NFE"].ToString();
                            produto.grupo = reader["GRUPO_PRODUTO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO_PRODUTO"].ToString();
                            produto.ids_nfs_selecionadas = ids;

                            lista.Add(produto);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);

                if (produto == null)
                {
                    return lista;
                }
                else
                {
                    return lista;
                }


            }
        }


        public Produto BuscaProdutoBancoLoja(string cod_barra,string banco_loja,string codigo_loja)
        {
          

            string sql = "select (SELECT FILIAL FROM " + banco_loja + ".dbo.FILIAIS where COD_FILIAL ='"+ codigo_loja + "')FILIAL, A.PRODUTO,A.COR_PRODUTO,B.DESC_COR_PRODUTO,TAMANHO,CODIGO_BARRA,A.GRADE,C.DESC_PRODUTO," +
                "C.GRUPO_PRODUTO,C.SUBGRUPO_PRODUTO,C.GRIFFE,D.PRECO1  from " + banco_loja + ".dbo.PRODUTOS_BARRA A INNER JOIN" +
                " "+ banco_loja + ".dbo.PRODUTO_CORES B ON (A.PRODUTO = B.PRODUTO) AND  (A.COR_PRODUTO = B.COR_PRODUTO) " +
                "INNER JOIN "+ banco_loja + ".dbo.PRODUTOS C ON (A.PRODUTO = C.PRODUTO)  INNER JOIN  " + banco_loja + ".dbo.PRODUTOS_PRECOS D ON (A.PRODUTO = D.PRODUTO) " +
                " where (CODIGO_BARRA in ('"+ cod_barra + "') OR A.PRODUTO IN ('" + cod_barra + "')) AND CODIGO_TAB_PRECO = '00' ";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                Produto produto = null;
                Produto produtoVazio = new Produto();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new Produto();
                            produto.filial = reader["FILIAL"].ToString();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.fornecedor = reader["GRIFFE"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.grupo = reader["GRUPO_PRODUTO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO_PRODUTO"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();


                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);

                if (produto == null)
                {
                    return produtoVazio;
                }
                else
                {
                    return produto;
                }


            }
        }




        public Produto BuscaProdutoNotasSelecionadas(string cod_barra, string[] id_xml_nfe)
        {
            string ids = "";
            for (int i = 0; i < id_xml_nfe.Count(); i++)
            {
                if (i + 1 == 1 && i + 1 != id_xml_nfe.Count())
                {
                    ids = "'" + id_xml_nfe[i] + "',";
                }
                else if (i + 1 == 1 && i + 1 == id_xml_nfe.Count())
                {
                    ids = "'" + id_xml_nfe[i] + "'";
                }
                else if (i + 1 == id_xml_nfe.Count())
                {
                    ids += "'" + id_xml_nfe[i] + "'";
                }
                else
                {
                    ids += "'" + id_xml_nfe[i] + "',";
                }

            }

            string sql = "select CODIGO_BARRA,PRODUTO,COR_PRODUTO,DESC_PRODUTO,SUM(CONVERT(INT,QTDE)) QTDE_TOTAL," +
                " VALOR_UN PRECO1,NOME_CLIFOR FORNECEDOR,TAMANHO,GRUPO_PRODUTO,SUBGRUPO_PRODUTO from PRODUTOS_BARRA_XML where ID_XML_NFE in (" + ids + ") " +
                " AND CODIGO_BARRA = '" + cod_barra + "' GROUP BY CODIGO_BARRA,PRODUTO,COR_PRODUTO,DESC_PRODUTO," +
                " VALOR_UN,NOME_CLIFOR,TAMANHO,GRUPO_PRODUTO,SUBGRUPO_PRODUTO  ";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                Produto produto = null;
                Produto produtoVazio = new Produto();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new Produto();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.qtde_total = reader["QTDE_TOTAL"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.ids_nfs_selecionadas = ids;
                            produto.grupo = reader["GRUPO_PRODUTO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO_PRODUTO"].ToString();


                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);

                if(produto == null)
                {
                    return produtoVazio;
                }
                else
                {
                    return produto;
                }

               
            }
        }

        public override Produto BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override Produto BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Produto> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(Produto entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(Produto entity)
        {
            throw new NotImplementedException();
        }
    }
}