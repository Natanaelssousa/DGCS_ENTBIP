using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class AuxEntradaDao : Conexao<AuxEntrada, int>
    {
        private string StringConnectionfilial;


        public AuxEntradaDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }


        public  void AtualizarRomaneio()
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE SEQUENCIAL_ROMANEIO SET ROMANEIO_PRODUTO = ROMANEIO_PRODUTO + 1 ";

                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }

        public void ZerarQtdeDivergentesRomaneio(string romaneio,string codigo_barra, string responsavel)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE AUX_ENTRADA SET QTDE_BIPADO = '0', DATA_HORA = getdate() WHERE ROMANEIO_PRODUTO = '" + romaneio + "' AND CODIGO_BARRA = '"+ codigo_barra + "' and RESPONSAVEL = '"+responsavel+"' ";

                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }

        public void AtualizaQtdeBipadoUltValida(string romaneio, string responsavel)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE AUX_ENTRADA SET QTDE_BIPADO = CASE WHEN QTDE_CONTADA3 > 0  THEN QTDE_CONTADA3 ELSE CASE WHEN" +
                    " QTDE_CONTADA2 > 0  THEN QTDE_CONTADA2 ELSE QTDE_CONTADA1  END END" +
                    "  WHERE ROMANEIO_PRODUTO = '"+ romaneio + "' AND RESPONSAVEL = '"+ responsavel + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }
        public void GuardaContagem1(string romaneio, string responsavel)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE AUX_ENTRADA SET QTDE_CONTADA1 = QTDE_BIPADO WHERE ROMANEIO_PRODUTO = '" + romaneio + "'  and RESPONSAVEL = '" + responsavel + "' ";

                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }

        public void GuardaContagem2(string romaneio, string responsavel)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE AUX_ENTRADA SET QTDE_CONTADA2 = QTDE_BIPADO WHERE ROMANEIO_PRODUTO = '" + romaneio + "'  and RESPONSAVEL = '" + responsavel + "' ";

                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }


        public void GuardaContagem3(string romaneio, string responsavel)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE AUX_ENTRADA SET QTDE_CONTADA3 = QTDE_BIPADO WHERE ROMANEIO_PRODUTO = '" + romaneio + "' AND  RESPONSAVEL = '" + responsavel + "' ";

                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }

        public string[] ConsultaNotaEmAbertoResponsavel(string responsavel)
        {
            string sql = "SELECT SUM(QTDE_BIPADO) QTDE_BIPADO,A.ROMANEIO_PRODUTO,A.RESPONSAVEL,A.IDS_NFS_SELECIONADAS," +
                "CASE WHEN ENTRADA_STATUS IS NULL THEN 'PENDENTE'WHEN ENTRADA_STATUS = '0' THEN 'PENDENTE' END ENTRADA_STATUS" +
                " FROM AUX_ENTRADA A LEFT JOIN AUX_ENTRADA_STATUS B ON (A.ROMANEIO_PRODUTO = B.ROMANEIO_PRODUTO) AND ( A.IDS_NFS_SELECIONADAS =B.IDS_NFS_SELECIONADAS) " +
                "WHERE A.RESPONSAVEL = '"+ responsavel + "' and (ENTRADA_STATUS is null or ENTRADA_STATUS = '0') " +
                " GROUP BY A.ROMANEIO_PRODUTO,A.RESPONSAVEL,A.IDS_NFS_SELECIONADAS,ENTRADA_STATUS";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                string[] notaEmAberto = new string[3];

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {

                            notaEmAberto[0] = reader["QTDE_BIPADO"].ToString();
                            notaEmAberto[1] = reader["ROMANEIO_PRODUTO"].ToString();
                            notaEmAberto[2] = reader["IDS_NFS_SELECIONADAS"].ToString();

                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return notaEmAberto;
            }
        }

        public bool ConsultaNotaEmBIpagemPorOUtroUsuario(string ids, string usuario)
        {
            string sql = "SELECT SUM(QTDE_BIPADO) QTDE_BIPADO,A.ROMANEIO_PRODUTO,A.RESPONSAVEL,A.IDS_NFS_SELECIONADAS," +
                "CASE WHEN ENTRADA_STATUS IS NULL THEN 'PENDENTE'WHEN ENTRADA_STATUS = '0' THEN 'PENDENTE' END ENTRADA_STATUS" +
                " FROM AUX_ENTRADA A LEFT JOIN AUX_ENTRADA_STATUS B ON (A.ROMANEIO_PRODUTO = B.ROMANEIO_PRODUTO) " +
                "WHERE A.IDS_NFS_SELECIONADAS like ('" + ids + "%') and (ENTRADA_STATUS is null or ENTRADA_STATUS = '0') and A.RESPONSAVEL <> '"+ usuario + "' " +
                " GROUP BY A.ROMANEIO_PRODUTO,A.RESPONSAVEL,A.IDS_NFS_SELECIONADAS,ENTRADA_STATUS";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                string notaEmAberto="";

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {

                            notaEmAberto = reader["IDS_NFS_SELECIONADAS"].ToString();

                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);

                if (notaEmAberto == "" )
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }

        public List<AuxEntrada> RetornaItensDIvergentesEmail(string romaneio)
        {
            string sql = "select b.NF_ENTRADA,A.ROMANEIO_PRODUTO,B.PRODUTO,B.COR_PRODUTO,QTDE_BIPADO,QTDE_TOTAL,A.PRECO1, " +
" B.TAMANHO,DESC_PRODUTO,FORNECEDOR,A.RESPONSAVEL,B.DESTINATARIO,GRUPO_PRODUTO,SUBGRUPO_PRODUTO,a.CODIGO_BARRA from(SELECT DISTINCT  SS.[value] as IDS_NFS,  " +
" CODIGO_BARRA,ROMANEIO_PRODUTO,QTDE_BIPADO,QTDE_TOTAL,PRECO1,FORNECEDOR,RESPONSAVEL,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3 from AUX_ENTRADA as E " +
" cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) A INNER JOIN PRODUTOS_BARRA_XML B ON " +
" (A.IDS_NFS = B.ID_XML_NFE) AND(A.CODIGO_BARRA = B.CODIGO_BARRA) where A.ROMANEIO_PRODUTO = '"+ romaneio + "' " +
" AND QTDE_BIPADO<> QTDE_TOTAL GROUP BY PRODUTO, COR_PRODUTO, A.ROMANEIO_PRODUTO, A.PRECO1, TAMANHO "+
" , DESC_PRODUTO, b.NF_ENTRADA,A.FORNECEDOR, A.RESPONSAVEL, B.DESTINATARIO, a.QTDE_BIPADO, a.QTDE_TOTAL, GRUPO_PRODUTO, SUBGRUPO_PRODUTO,a.CODIGO_BARRA";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                AuxEntrada produto = null;
                AuxEntrada produtoVazio = new AuxEntrada();
                List<AuxEntrada> list = new List<AuxEntrada>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new AuxEntrada();
                            produto.romaneio_produto = reader["ROMANEIO_PRODUTO"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();
                            produto.qtde_total = reader["QTDE_TOTAL"].ToString();
                            produto.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.destinatario = reader["DESTINATARIO"].ToString();
                            produto.grupo = reader["GRUPO_PRODUTO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO_PRODUTO"].ToString();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.ids_nfs_selecionadas = reader["NF_ENTRADA"].ToString();
                            produto.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            produto.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            produto.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();

                            list.Add(produto);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return list;
            }
        }

        public List<AuxEntrada> RetornaItensDIvergentesDetalhe(string romaneio)
        {
            string sql = "select  A.ROMANEIO_PRODUTO,B.PRODUTO,B.COR_PRODUTO,SUM((QTDE_TOTAL - QTDE_BIPADO)) QTDE_TOTAL" +
                ",A.PRECO1,B.TAMANHO,DESC_PRODUTO,FORNECEDOR,A.RESPONSAVEL,B.DESTINATARIO from (SELECT DISTINCT  SS.[value] " +
                "as IDS_NFS, CODIGO_BARRA,ROMANEIO_PRODUTO,QTDE_BIPADO,QTDE_TOTAL,PRECO1,FORNECEDOR,RESPONSAVEL from AUX_ENTRADA " +
                "as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) A INNER JOIN PRODUTOS_BARRA_XML B ON  " +
                "(A.IDS_NFS = B.ID_XML_NFE) AND (A.CODIGO_BARRA = B.CODIGO_BARRA) where A.ROMANEIO_PRODUTO = '" + romaneio + "'  " +
                "AND QTDE_BIPADO <> QTDE_TOTAL GROUP BY PRODUTO,COR_PRODUTO,A.ROMANEIO_PRODUTO,A.PRECO1,TAMANHO ,DESC_PRODUTO," +
                "A.FORNECEDOR,A.RESPONSAVEL,B.DESTINATARIO";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                AuxEntrada produto = null;
                AuxEntrada produtoVazio = new AuxEntrada();
                List<AuxEntrada> list = new List<AuxEntrada>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new AuxEntrada();
                            produto.romaneio_produto = reader["ROMANEIO_PRODUTO"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();
                            produto.qtde_total = reader["QTDE_TOTAL"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.destinatario = reader["DESTINATARIO"].ToString();


                            list.Add(produto);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return list;
            }
        }

        public List<AuxEntrada> RetornaItensSobra(string romaneio)
        {
            string sql = "select  A.ROMANEIO_PRODUTO,B.PRODUTO,B.COR_PRODUTO,SUM((QTDE_BIPADO - QTDE_TOTAL)) QTDE_TOTAL" +
                ",A.PRECO1,B.TAMANHO,DESC_PRODUTO,FORNECEDOR,A.RESPONSAVEL,B.DESTINATARIO from (SELECT DISTINCT  SS.[value] " +
                "as IDS_NFS, CODIGO_BARRA,ROMANEIO_PRODUTO,QTDE_BIPADO,QTDE_TOTAL,PRECO1,FORNECEDOR,RESPONSAVEL from AUX_ENTRADA " +
                "as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) A INNER JOIN PRODUTOS_BARRA_XML B ON  " +
                "(A.IDS_NFS = B.ID_XML_NFE) AND (A.CODIGO_BARRA = B.CODIGO_BARRA) where A.ROMANEIO_PRODUTO = '" + romaneio + "'  " +
                "AND QTDE_BIPADO > QTDE_TOTAL GROUP BY PRODUTO,COR_PRODUTO,A.ROMANEIO_PRODUTO,A.PRECO1,TAMANHO ,DESC_PRODUTO," +
                "A.FORNECEDOR,A.RESPONSAVEL,B.DESTINATARIO";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                AuxEntrada produto = null;
                AuxEntrada produtoVazio = new AuxEntrada();
                List<AuxEntrada> list = new List<AuxEntrada>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new AuxEntrada();
                            produto.romaneio_produto = reader["ROMANEIO_PRODUTO"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();
                            produto.qtde_total = reader["QTDE_TOTAL"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.destinatario = reader["DESTINATARIO"].ToString();


                            list.Add(produto);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return list;
            }
        }
        public List<AuxEntrada> RetornaItensRomaneioSaida(string romaneio, string responsavel)
        {
            string sql = "select  A.ROMANEIO_PRODUTO,B.PRODUTO,B.COR_PRODUTO,SUM((QTDE_TOTAL - QTDE_BIPADO)) QTDE_TOTAL" +
                ",A.PRECO1,B.TAMANHO,DESC_PRODUTO,FORNECEDOR,A.RESPONSAVEL,B.DESTINATARIO,B.CHAVE_NFE,B.NF_ENTRADA from (SELECT DISTINCT  SS.[value] " +
                "as IDS_NFS, CODIGO_BARRA,ROMANEIO_PRODUTO,QTDE_BIPADO,QTDE_TOTAL,PRECO1,FORNECEDOR,RESPONSAVEL from AUX_ENTRADA " +
                "as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) A INNER JOIN PRODUTOS_BARRA_XML B ON  " +
                "(A.IDS_NFS = B.ID_XML_NFE) AND (A.CODIGO_BARRA = B.CODIGO_BARRA) where A.ROMANEIO_PRODUTO = '"+ romaneio + "' AND A.RESPONSAVEL = '"+responsavel+"'  " +
                "AND QTDE_BIPADO < QTDE_TOTAL GROUP BY PRODUTO,COR_PRODUTO,A.ROMANEIO_PRODUTO,A.PRECO1,TAMANHO ,DESC_PRODUTO," +
                "A.FORNECEDOR,A.RESPONSAVEL,B.DESTINATARIO,B.CHAVE_NFE,B.NF_ENTRADA";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                AuxEntrada produto = null;
                AuxEntrada produtoVazio = new AuxEntrada();
                List<AuxEntrada> list = new List<AuxEntrada>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new AuxEntrada();
                            produto.romaneio_produto = reader["ROMANEIO_PRODUTO"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();
                            produto.qtde_total = reader["QTDE_TOTAL"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.destinatario = reader["DESTINATARIO"].ToString();
                            produto.chave_nfe = reader["CHAVE_NFE"].ToString();
                            produto.nota_fiscal = reader["NF_ENTRADA"].ToString();


                            list.Add(produto);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return list;
            }
        }


        public List<AuxEntrada>  ConsultaBipagem(string romaneio,string responsavel)
        {
            string sql = "SELECT A.QTDE_VEZES_BIPADO,A.IDS_NFS_SELECIONADAS,A.ROMANEIO_PRODUTO,A.PRODUTO,A.COR_PRODUTO,A.TAMANHO,A.CODIGO_BARRA,A.PRECO1,QTDE_TOTAL,QTDE_BIPADO," +
                "DESC_PRODUTO,FORNECEDOR,A.RESPONSAVEL,CASE WHEN ENTRADA_STATUS IS NULL THEN 'SEM FINALIZACAO' WHEN ENTRADA_STATUS = '0' THEN 'PENDENTE ENTRADA' " +
                "WHEN ENTRADA_STATUS = '1' THEN 'ENTRADA FEITA' END ENTRADA_STATUS,CHAVE_NFE,GRUPO,SUBGRUPO,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3,DATA_HORA FROM AUX_ENTRADA A LEFT JOIN AUX_ENTRADA_STATUS B  ON (A.ROMANEIO_PRODUTO = B.ROMANEIO_PRODUTO) AND (A.RESPONSAVEL = B.RESPONSAVEL) " +
                "WHERE A.ROMANEIO_PRODUTO = '" + romaneio + "' AND A.RESPONSAVEL = '"+ responsavel + "' order by DATA_HORA desc";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                AuxEntrada produto = null;
                AuxEntrada produtoVazio = new AuxEntrada();
                List<AuxEntrada> list = new List<AuxEntrada>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new AuxEntrada();
                            produto.romaneio_produto = reader["ROMANEIO_PRODUTO"].ToString();
                            produto.romaneio_produto = reader["ROMANEIO_PRODUTO"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();
                            produto.qtde_total = reader["QTDE_TOTAL"].ToString();
                            produto.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.entrada_status = reader["ENTRADA_STATUS"].ToString();
                            produto.chave_nfe = reader["CHAVE_NFE"].ToString();
                            produto.grupo = reader["GRUPO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO"].ToString();
                            produto.ids_nfs_selecionadas = reader["IDS_NFS_SELECIONADAS"].ToString();
                            produto.qtde_vezes_bipado = reader["QTDE_VEZES_BIPADO"].ToString();
                            produto.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            produto.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            produto.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();
                            produto.data_hora = reader["DATA_HORA"].ToString();
                            
                            list.Add(produto);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return list;
            }
        }

        public List<AuxEntrada> ConsultaBipagemZerada(string romaneio, string responsavel   )
        {
            string sql = "SELECT A.ROMANEIO_PRODUTO,A.PRODUTO,A.COR_PRODUTO,A.TAMANHO,A.CODIGO_BARRA,A.PRECO1,QTDE_TOTAL,QTDE_BIPADO," +
                "DESC_PRODUTO,FORNECEDOR,A.RESPONSAVEL,CASE WHEN ENTRADA_STATUS IS NULL THEN 'SEM FINALIZACAO' WHEN ENTRADA_STATUS = '0' THEN 'PENDENTE ENTRADA' " +
                "WHEN ENTRADA_STATUS = '1' THEN 'ENTRADA FEITA' END ENTRADA_STATUS,CHAVE_NFE,GRUPO,SUBGRUPO,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3 FROM AUX_ENTRADA A LEFT JOIN AUX_ENTRADA_STATUS B  ON (A.ROMANEIO_PRODUTO = B.ROMANEIO_PRODUTO) " +
                "WHERE A.ROMANEIO_PRODUTO = '" + romaneio + "'AND A.RESPONSAVEL = '"+responsavel+"' and QTDE_BIPADO = 0 order by DATA_HORA desc";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                AuxEntrada produto = null;
                AuxEntrada produtoVazio = new AuxEntrada();
                List<AuxEntrada> list = new List<AuxEntrada>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new AuxEntrada();
                            produto.romaneio_produto = reader["ROMANEIO_PRODUTO"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();
                            produto.qtde_total = reader["QTDE_TOTAL"].ToString();
                            produto.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.entrada_status = reader["ENTRADA_STATUS"].ToString();
                            produto.chave_nfe = reader["CHAVE_NFE"].ToString();
                            produto.grupo = reader["GRUPO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO"].ToString();
                            produto.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            produto.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            produto.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();

                            list.Add(produto);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return list;
            }
        }
        public AuxEntrada ConsultaNotaEmAbertoChave(string romaneio,string responsavel)
        {
            string sql = "SELECT A.ROMANEIO_PRODUTO,A.PRODUTO,A.COR_PRODUTO,A.TAMANHO,A.CODIGO_BARRA,A.PRECO1,QTDE_TOTAL,QTDE_BIPADO," +
                "DESC_PRODUTO,FORNECEDOR,A.RESPONSAVEL,CASE WHEN ENTRADA_STATUS IS NULL THEN 'SEM FINALIZACAO' WHEN ENTRADA_STATUS = '0' THEN 'PENDENTE ENTRADA' " +
                "WHEN ENTRADA_STATUS = '1' THEN 'ENTRADA FEITA' END ENTRADA_STATUS,CHAVE_NFE,GRUPO,SUBGRUPO,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3 FROM AUX_ENTRADA A LEFT JOIN AUX_ENTRADA_STATUS B  ON (A.ROMANEIO_PRODUTO = B.ROMANEIO_PRODUTO) " +
                "WHERE   A.ROMANEIO_PRODUTO = '" + romaneio + "' AND A.RESPONSAVEL = '"+ responsavel + "'";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                AuxEntrada produto = null;
                AuxEntrada produtoVazio = new AuxEntrada();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new AuxEntrada();
                            produto.romaneio_produto = reader["ROMANEIO_PRODUTO"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();
                            produto.qtde_total = reader["QTDE_TOTAL"].ToString();
                            produto.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.entrada_status = reader["ENTRADA_STATUS"].ToString();
                            produto.chave_nfe = reader["CHAVE_NFE"].ToString();
                            produto.grupo = reader["GRUPO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO"].ToString();
                            produto.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            produto.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            produto.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();
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


        public List<AuxEntrada> ConsultaQtdeItemDivergenteNotaEmAberto(string romaneio, string responsavel)
        {
            string sql = "SELECT QTDE_TOTAL,QTDE_BIPADO,CASE WHEN QTDE_TOTAL - QTDE_BIPADO = 0 THEN 'QTDE OK' ELSE 'QTDE DIVERGENTE' END BIPAGEM_ITEM,CODIGO_BARRA,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3 " +
                " FROM AUX_ENTRADA A  LEFT JOIN AUX_ENTRADA_STATUS B  ON (A.ROMANEIO_PRODUTO = B.ROMANEIO_PRODUTO) " +
                "WHERE  A.ROMANEIO_PRODUTO = '"+ romaneio + "' and a.responsavel ='"+responsavel+"' order by  QTDE_BIPADO desc";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                List<AuxEntrada> lista = new List<AuxEntrada>();
                AuxEntrada auxEntrada = null;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                             

                            if ( reader["BIPAGEM_ITEM"].ToString() == "QTDE DIVERGENTE")
                            {
                                auxEntrada = new AuxEntrada();
                                auxEntrada.codigo_barra = reader["CODIGO_BARRA"].ToString();
                                auxEntrada.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                                auxEntrada.qtde_total = reader["QTDE_TOTAL"].ToString();
                                auxEntrada.romaneio_produto = romaneio;
                                auxEntrada.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                                auxEntrada.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                                auxEntrada.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();
                                lista.Add(auxEntrada);

                            }
        
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
             
                    return lista;
               
            }
        }



        public AuxEntrada ConsultaRomaneioEmAberto(string romaneio, string responsavel)
        {
            string sql = "SELECT top(1) A.ROMANEIO_PRODUTO,A.PRODUTO,A.COR_PRODUTO,A.TAMANHO,A.CODIGO_BARRA,A.PRECO1,QTDE_TOTAL,QTDE_BIPADO," +
                "DESC_PRODUTO,FORNECEDOR,A.RESPONSAVEL,CASE WHEN ENTRADA_STATUS IS NULL THEN 'SEM FINALIZACAO' WHEN ENTRADA_STATUS = '0' THEN 'PENDENTE ENTRADA' " +
                "WHEN ENTRADA_STATUS = '1' THEN 'ENTRDA FEITA' END ENTRADA_STATUS,CHAVE_NFE,GRUPO,SUBGRUPO,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3 FROM AUX_ENTRADA A LEFT JOIN AUX_ENTRADA_STATUS B  ON (A.ROMANEIO_PRODUTO = B.ROMANEIO_PRODUTO) " +
                "WHERE  A.ROMANEIO_PRODUTO = '" + romaneio + "' and A.RESPONSAVEL = '"+responsavel+"' ";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                AuxEntrada produto = null;
                AuxEntrada produtoVazio = new AuxEntrada();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new AuxEntrada();
                            produto.romaneio_produto = reader["ROMANEIO_PRODUTO"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();
                            produto.qtde_total = reader["QTDE_TOTAL"].ToString();
                            produto.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.entrada_status = reader["ENTRADA_STATUS"].ToString();
                            produto.chave_nfe = reader["CHAVE_NFE"].ToString();
                            produto.grupo = reader["GRUPO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO"].ToString();
                            produto.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            produto.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            produto.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();
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

        public AuxEntrada ConsultaNotaEmAberto(string romaneio, string codigo_barra, string usuario)
        {
            string sql = "SELECT A.ROMANEIO_PRODUTO,A.PRODUTO,A.COR_PRODUTO,A.TAMANHO,A.CODIGO_BARRA,A.PRECO1,QTDE_TOTAL,QTDE_BIPADO," +
                "DESC_PRODUTO,FORNECEDOR,A.RESPONSAVEL,CASE WHEN ENTRADA_STATUS IS NULL THEN 'SEM FINALIZACAO' WHEN ENTRADA_STATUS = '0' THEN 'PENDENTE ENTRADA' " +
                "WHEN ENTRADA_STATUS = '1' THEN 'ENTRDA FEITA' END ENTRADA_STATUS,CHAVE_NFE,GRUPO,SUBGRUPO,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3 FROM AUX_ENTRADA A LEFT JOIN AUX_ENTRADA_STATUS B  ON (A.ROMANEIO_PRODUTO = B.ROMANEIO_PRODUTO) " +
                "WHERE A.CODIGO_BARRA = '"+ codigo_barra + "' AND A.ROMANEIO_PRODUTO = '"+ romaneio + "' and A.RESPONSAVEL = '"+ usuario + "'";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                AuxEntrada produto = null;
                AuxEntrada produtoVazio = new AuxEntrada();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new AuxEntrada();
                            produto.romaneio_produto = reader["ROMANEIO_PRODUTO"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();
                            produto.qtde_total = reader["QTDE_TOTAL"].ToString();
                            produto.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.entrada_status = reader["ENTRADA_STATUS"].ToString();
                            produto.chave_nfe = reader["CHAVE_NFE"].ToString();
                            produto.grupo = reader["GRUPO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO"].ToString();
                            produto.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            produto.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            produto.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();
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


        public bool VerificaSeChaveJaFoiBipado(string chave)
        {
            string sql = "SELECT CHAVE_NFE FROM AUX_ENTRADA WHERE CHAVE_NFE = '" + chave + "' ";

            string chave_nota = ""; 

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            chave_nota = reader["CHAVE_NFE"].ToString();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);

                if (chave_nota == "")
                {
                    return false;
                }
                else
                {
                    return true;
                }

               
            }
        }

        public string BuscaProximoRomaneio()
        {
            string sql = "SELECT right(replicate('0',6) +convert(VARCHAR, ROMANEIO_PRODUTO),6) as ROMANEIO_PRODUTO FROM SEQUENCIAL_ROMANEIO";

            string romaneio = ""; ;

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            romaneio = reader["ROMANEIO_PRODUTO"].ToString();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return romaneio;
            }
        }


        public string [] ConsultaAuxStatus(string responsavel)
        {
            string sql = "SELECT ROMANEIO_PRODUTO,QTDE_VEZES_BIPADO,RESPONSAVEL,ENTRADA_STATUS,IDS_NFS_SELECIONADAS from AUX_ENTRADA_STATUS WHERE RESPONSAVEL = '" + responsavel + "' and ENTRADA_STATUS = 0 ";

            string [] ent_status = new string [5] ;

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            ent_status[0] = reader["ROMANEIO_PRODUTO"].ToString();
                            ent_status[1] = reader["QTDE_VEZES_BIPADO"].ToString();
                            ent_status[2] = reader["RESPONSAVEL"].ToString();
                            ent_status[3] = reader["ENTRADA_STATUS"].ToString();
                            ent_status[3] = reader["IDS_NFS_SELECIONADAS"].ToString();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return ent_status;
            }
        }

        public string BuscaRomaneioEmAberto()
        {
            string sql = "SELECT right(replicate('0',6) +convert(VARCHAR, ROMANEIO_PRODUTO),6) as ROMANEIO_PRODUTO FROM SEQUENCIAL_ROMANEIO";

            string romaneio = ""; ;

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            romaneio = reader["ROMANEIO_PRODUTO"].ToString();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return romaneio;
            }
        }

        public override void Atualizar(AuxEntrada entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE AUX_ENTRADA SET QTDE_BIPADO ='" + entity.qtde_bipado + "',DATA_HORA =GETDATE() " +
                    " WHERE RESPONSAVEL = '" + entity.responsavel + "' AND ROMANEIO_PRODUTO = '"+entity.romaneio_produto+"'" +
                    "AND CODIGO_BARRA = '"+ entity .codigo_barra+ "'";
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }

        public override List<AuxEntrada> BuscaCompletaRetornoLista()
        {
            throw new NotImplementedException();
        }

        public override List<AuxEntrada> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override AuxEntrada BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override AuxEntrada BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<AuxEntrada> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(AuxEntrada entity)
        {
            throw new NotImplementedException();
        }

        public  void RemoverPor(string romaneio, string responsavel, string id_nfs)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {

                string sql = "DELETE FROM AUX_ENTRADA WHERE ROMANEIO_PRODUTO = '" + romaneio + "' and  RESPONSAVEL = '"+responsavel+"' " +
                    " DELETE FROM AUX_ENTRADA_STATUS WHERE ROMANEIO_PRODUTO = '" + romaneio + "' AND IDS_NFS_SELECIONADAS = '"+ id_nfs + "' ";

                SqlCommand cmd = new SqlCommand(sql, conn);


                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }


        public  void RemoverPorIdNfs(string id_nfs)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {

                string sql = "DELETE FROM AUX_ENTRADA WHERE IDS_NFS_SELECIONADAS like ('%" + id_nfs + "%')   " +
                    " DELETE FROM AUX_ENTRADA_STATUS WHERE IDS_NFS_SELECIONADAS like ('%" + id_nfs + "%')";

                SqlCommand cmd = new SqlCommand(sql, conn);


                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }


        public void AtualizarStatus(string romaneio_produto, string qtde_vezes_bipado, string entrada_status, string responsavel, string id_nfs_selecionadas,string status_bipagem)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE AUX_ENTRADA_STATUS  SET QTDE_VEZES_BIPADO = '"+ qtde_vezes_bipado + "', ENTRADA_STATUS = '"+ entrada_status + "' , STATUS_BIPAGEM = '" + status_bipagem + "'" +
                    "  WHERE ROMANEIO_PRODUTO = '"+romaneio_produto+"' AND RESPONSAVEL = '"+ responsavel + "' and IDS_NFS_SELECIONADAS = '"+ id_nfs_selecionadas.Replace("'", "") + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);


                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }

        public void SalvarStatus(string romaneio_produto, string qtde_vezes_bipado, string estrada_status, string responsavel, string id_nfs_selecionadas,string status_bipagem)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "INSERT INTO AUX_ENTRADA_STATUS (ROMANEIO_PRODUTO,QTDE_VEZES_BIPADO,ENTRADA_STATUS,IDS_NFS_SELECIONADAS,RESPONSAVEL,STATUS_BIPAGEM) " +
                    "values ('" + romaneio_produto + "','" + qtde_vezes_bipado + "','" + estrada_status + "','" + id_nfs_selecionadas.Replace("'", "") + "','" + responsavel + "','" + status_bipagem + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);


                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }
        public override void Salvar(AuxEntrada entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "INSERT INTO AUX_ENTRADA(IDS_NFS_SELECIONADAS,ROMANEIO_PRODUTO,PRODUTO,COR_PRODUTO,TAMANHO,CODIGO_BARRA,PRECO1," +
                    "QTDE_TOTAL,QTDE_BIPADO,DESC_PRODUTO,FORNECEDOR,RESPONSAVEL,QTDE_VEZES_BIPADO,CHAVE_NFE,GRUPO,SUBGRUPO) " +
                    "VALUES('" + entity.ids_nfs_selecionadas.Replace("'","") + "','" + entity.romaneio_produto + "', '" + entity.produto + "', '" + entity.cor_produto + "', '" + entity.tamanho + "'," +
                    " '" + entity.codigo_barra + "', '" + entity.preco1 + "', '" + entity.qtde_total + "', '" + entity.qtde_bipado + "'," +
                    " '" + entity.desc_produto + "', '" + entity.fornecedor + "', '" + entity.responsavel + "', '" + entity.qtde_vezes_bipado + "'," +
                    " '" + entity.chave_nfe + "', '" + entity.grupo + "', '" + entity.subgrupo + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);


                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }

        public string SalvarRetaguardatxt(AuxEntrada entity, ParametrosFtp parametrosFtp)
        {

            // Obtém a data e hora atual
            DateTime dataHoraAtual = Convert.ToDateTime(entity.data_hora);

            // Formata a data e hora no formato de banco de dados
            string dataHoraFormatada = dataHoraAtual.ToString("yyyy-MM-dd hh:mm:ss.mmm");

            string sql = "INSERT INTO AUX_ENTRADA(IDS_NFS_SELECIONADAS,NOTA_FISCAL,ROMANEIO_PRODUTO,PRODUTO,COR_PRODUTO,TAMANHO,CODIGO_BARRA,PRECO1," +
                 "QTDE_TOTAL,QTDE_BIPADO,DESC_PRODUTO,FORNECEDOR,RESPONSAVEL,QTDE_VEZES_BIPADO,CHAVE_NFE,GRUPO,SUBGRUPO,CNPJ,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3,DATA_HORA) " +
                 "VALUES('" + entity.ids_nfs_selecionadas.Replace("'", "") + "','" + entity.nota_fiscal + "','" + entity.romaneio_produto + "', '" + entity.produto + "', '" + entity.cor_produto + "', '" + entity.tamanho + "'," +
                 " '" + entity.codigo_barra + "', '" + entity.preco1 + "', '" + entity.qtde_total + "', '" + entity.qtde_bipado + "'," +
                 " '" + entity.desc_produto + "', '" + entity.fornecedor + "', '" + entity.responsavel + "', '" + entity.qtde_vezes_bipado + "'," +
                 " '" + entity.chave_nfe + "', '" + entity.grupo + "', '" + entity.subgrupo + "', '" + entity.cnpj + "', '" + entity.qtde_contada1 + "', '" + entity.qtde_contada2 + "', '" + entity.qtde_contada3 + "', '" + dataHoraFormatada + "')";

            return sql;

        }
        public  void SalvarRetaguarda(AuxEntrada entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "INSERT INTO AUX_ENTRADA(IDS_NFS_SELECIONADAS,NOTA_FISCAL,ROMANEIO_PRODUTO,PRODUTO,COR_PRODUTO,TAMANHO,CODIGO_BARRA,PRECO1," +
                    "QTDE_TOTAL,QTDE_BIPADO,DESC_PRODUTO,FORNECEDOR,RESPONSAVEL,QTDE_VEZES_BIPADO,CHAVE_NFE,GRUPO,SUBGRUPO,CNPJ,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3) " +
                    "VALUES('" + entity.ids_nfs_selecionadas.Replace("'", "") + "','" + entity.nota_fiscal + "','" + entity.romaneio_produto + "', '" + entity.produto + "', '" + entity.cor_produto + "', '" + entity.tamanho + "'," +
                    " '" + entity.codigo_barra + "', '" + entity.preco1 + "', '" + entity.qtde_total + "', '" + entity.qtde_bipado + "'," +
                    " '" + entity.desc_produto + "', '" + entity.fornecedor + "', '" + entity.responsavel + "', '" + entity.qtde_vezes_bipado + "'," +
                    " '" + entity.chave_nfe + "', '" + entity.grupo + "', '" + entity.subgrupo + "', '" + entity.cnpj + "', '" + entity.qtde_contada1 + "', '" + entity.qtde_contada2 + "', '" + entity.qtde_contada3 + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);


                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }
    }
}