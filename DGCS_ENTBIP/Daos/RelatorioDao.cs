using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{


    public class RelatorioDao : Conexao<Relatorio, int>
    {

        private string StringConnectionfilial;


        public RelatorioDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }

        public List<Relatorio> BuscaRetornoNFsPorNotaRetaguarda(string nf)
        {
            string sql = "select ID_AUX_ENTRADA,IDS_NFS_SELECIONADAS,NOTA_FISCAL,ROMANEIO_PRODUTO,PRODUTO,COR_PRODUTO," +
"TAMANHO,CODIGO_BARRA,PRECO1,QTDE_TOTAL,QTDE_BIPADO,DESC_PRODUTO,GRUPO,SUBGRUPO,FORNECEDOR,RESPONSAVEL," +
"CHAVE_NFE,QTDE_VEZES_BIPADO,CNPJ,CONVERT(DATE,DATA_HORA)DATA_HORA,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3,(QTDE_BIPADO-QTDE_TOTAL)COMPLEMENTAR,CASE WHEN (QTDE_BIPADO - QTDE_TOTAL) > 0 THEN 'SOBRA' WHEN (QTDE_BIPADO - QTDE_TOTAL) < 0 THEN 'FALTA'ELSE 'OK'END DIVERGENCIA from AUX_ENTRADA  WHERE " +
"  NOTA_FISCAL like('" + nf + "%') ORDER BY DATA_HORA";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Relatorio> list = new List<Relatorio>();
                Relatorio nota = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            nota = new Relatorio();
                            nota.emissao = reader["DATA_HORA"].ToString();
                            nota.nf_entrada = reader["NOTA_FISCAL"].ToString();
                            nota.nome_clifor = reader["FORNECEDOR"].ToString();
                            nota.grupo_produto = reader["GRUPO"].ToString();
                            nota.subgrupo_produto = reader["SUBGRUPO"].ToString();
                            nota.produto = reader["PRODUTO"].ToString();
                            nota.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            nota.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            nota.qtde_total = reader["QTDE_TOTAL"].ToString();
                            nota.responsavel = reader["RESPONSAVEL"].ToString();
                            nota.cnpj = reader["CNPJ"].ToString();
                            nota.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            nota.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            nota.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();
                            nota.complementar = reader["COMPLEMENTAR"].ToString();
                            nota.divergencia = reader["DIVERGENCIA"].ToString();
                            nota.desc_produto = reader["DESC_PRODUTO"].ToString();
                            nota.custo = reader["PRECO1"].ToString();


                            list.Add(nota);
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
        public List<Relatorio> BuscaRetornoNFsPorNota(  string nf )
        {
            string sql = "select EMISSAO, b.NUMERO_NF_TRANSFERENCIA NF_ENTRADA,A.NOME_CLIFOR,A.SERIE_NF_ENTRADA,d.grupo GRUPO_PRODUTO,d.SUBGRUPO SUBGRUPO_PRODUTO,d.PRODUTO," +
                "  d.CODIGO_BARRA,D.QTDE_BIPADO,D.QTDE_TOTAL,D.RESPONSAVEL,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3 " +
                "from NFE_XML_LOJA A LEFT JOIN LOJA_ENTRADAS B ON(A.CHAVE_NFE = B.CHAVE_NFE) left join(SELECT DISTINCT " +
                " SS.[value] as IDS_NFS, RESPONSAVEL,  QTDE_BIPADO,QTDE_TOTAL,CODIGO_BARRA,QTDE_CONTADA1,QTDE_CONTADA2," +
                "QTDE_CONTADA3,grupo, SUBGRUPO, produto from AUX_ENTRADA as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) " +
                "D ON D.IDS_NFS = A.ID_XML_NFE LEFT JOIN(SELECT DISTINCT  SS.[value] as IDS_NFS, RESPONSAVEL,IDS_NFS_SELECIONADAS, " +
                "ENTRADA_STATUS from AUX_ENTRADA_STATUS as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) F " +
                "ON F.IDS_NFS = A.ID_XML_NFE WHERE b.NUMERO_NF_TRANSFERENCIA like('"+ nf + "%') and EMISSAO IS NOT NULL ORDER BY EMISSAO," +
                " ROMANEIO_PRODUTO";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Relatorio> list = new List<Relatorio>();
                Relatorio nota = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            nota = new Relatorio();
                            nota.emissao = reader["EMISSAO"].ToString();
                            nota.nf_entrada = reader["NF_ENTRADA"].ToString();
                            nota.nome_clifor = reader["NOME_CLIFOR"].ToString();
                            nota.serie_nfe_entrada = reader["SERIE_NF_ENTRADA"].ToString();
                            nota.grupo_produto = reader["GRUPO_PRODUTO"].ToString();
                            nota.subgrupo_produto = reader["SUBGRUPO_PRODUTO"].ToString();
                            nota.produto = reader["PRODUTO"].ToString();
                            nota.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            nota.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            nota.qtde_total = reader["QTDE_TOTAL"].ToString();
                            nota.responsavel = reader["RESPONSAVEL"].ToString();
                            nota.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            nota.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            nota.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();

                            list.Add(nota);
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

        public List<Relatorio> BuscaRetornoListaNFsPorCnpjRetaguarda(string cnpj)
        {
            string sql = "select ID_AUX_ENTRADA,IDS_NFS_SELECIONADAS,NOTA_FISCAL,ROMANEIO_PRODUTO,PRODUTO,COR_PRODUTO," +
"TAMANHO,CODIGO_BARRA,PRECO1,QTDE_TOTAL,QTDE_BIPADO,DESC_PRODUTO,GRUPO,SUBGRUPO,FORNECEDOR,RESPONSAVEL," +
"CHAVE_NFE,QTDE_VEZES_BIPADO,CNPJ,CONVERT(DATE,DATA_HORA)DATA_HORA,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3,(QTDE_BIPADO-QTDE_TOTAL)COMPLEMENTAR,CASE WHEN (QTDE_BIPADO - QTDE_TOTAL) > 0 THEN 'SOBRA' WHEN (QTDE_BIPADO - QTDE_TOTAL) < 0 THEN 'FALTA'ELSE 'OK'END DIVERGENCIA from AUX_ENTRADA  WHERE " +
"  CNPJ = '" + cnpj + "' ORDER BY DATA_HORA";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Relatorio> list = new List<Relatorio>();
                Relatorio nota = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            nota = new Relatorio();
                            nota.emissao = reader["DATA_HORA"].ToString();
                            nota.nf_entrada = reader["NOTA_FISCAL"].ToString();
                            nota.nome_clifor = reader["FORNECEDOR"].ToString();
                            nota.grupo_produto = reader["GRUPO"].ToString();
                            nota.subgrupo_produto = reader["SUBGRUPO"].ToString();
                            nota.produto = reader["PRODUTO"].ToString();
                            nota.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            nota.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            nota.qtde_total = reader["QTDE_TOTAL"].ToString();
                            nota.responsavel = reader["RESPONSAVEL"].ToString();
                            nota.cnpj = reader["CNPJ"].ToString();
                            nota.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            nota.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            nota.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();
                            nota.complementar = reader["COMPLEMENTAR"].ToString();
                            nota.divergencia = reader["DIVERGENCIA"].ToString();
                            nota.desc_produto = reader["DESC_PRODUTO"].ToString();
                            nota.custo = reader["PRECO1"].ToString();

                            list.Add(nota);
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
        public List<Relatorio> BuscaRetornoListaNFsPorNfCnpjRetaguarda( string nf, string cnpj)
        {
            string sql = "select ID_AUX_ENTRADA,IDS_NFS_SELECIONADAS,NOTA_FISCAL,ROMANEIO_PRODUTO,PRODUTO,COR_PRODUTO," +
"TAMANHO,CODIGO_BARRA,PRECO1,QTDE_TOTAL,QTDE_BIPADO,DESC_PRODUTO,GRUPO,SUBGRUPO,FORNECEDOR,RESPONSAVEL," +
"CHAVE_NFE,QTDE_VEZES_BIPADO,CNPJ,CONVERT(DATE,DATA_HORA)DATA_HORA,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3,(QTDE_BIPADO-QTDE_TOTAL)COMPLEMENTAR,CASE WHEN (QTDE_BIPADO - QTDE_TOTAL) > 0 THEN 'SOBRA' WHEN (QTDE_BIPADO - QTDE_TOTAL) < 0 THEN 'FALTA'ELSE 'OK'END DIVERGENCIA from AUX_ENTRADA  WHERE " +
"  CNPJ = '" + cnpj + "' and NOTA_FISCAL like('" + nf + "%') ORDER BY DATA_HORA";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Relatorio> list = new List<Relatorio>();
                Relatorio nota = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            nota = new Relatorio();
                            nota.emissao = reader["DATA_HORA"].ToString();
                            nota.nf_entrada = reader["NOTA_FISCAL"].ToString();
                            nota.nome_clifor = reader["FORNECEDOR"].ToString();
                            nota.grupo_produto = reader["GRUPO"].ToString();
                            nota.subgrupo_produto = reader["SUBGRUPO"].ToString();
                            nota.produto = reader["PRODUTO"].ToString();
                            nota.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            nota.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            nota.qtde_total = reader["QTDE_TOTAL"].ToString();
                            nota.responsavel = reader["RESPONSAVEL"].ToString();
                            nota.cnpj = reader["CNPJ"].ToString();
                            nota.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            nota.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            nota.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();
                            nota.complementar = reader["COMPLEMENTAR"].ToString();
                            nota.divergencia = reader["DIVERGENCIA"].ToString();
                            nota.desc_produto = reader["DESC_PRODUTO"].ToString();
                            nota.custo = reader["PRECO1"].ToString();

                            list.Add(nota);
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
        public List<Relatorio> BuscaRetornoListaNFsPorDataCnpjRetaguarda(string data_inicio, string data_fim, string cnpj)
        {
            string sql = "select ID_AUX_ENTRADA,IDS_NFS_SELECIONADAS,NOTA_FISCAL,ROMANEIO_PRODUTO,PRODUTO,COR_PRODUTO," +
      "TAMANHO,CODIGO_BARRA,PRECO1,QTDE_TOTAL,QTDE_BIPADO,DESC_PRODUTO,GRUPO,SUBGRUPO,FORNECEDOR,RESPONSAVEL," +
      "CHAVE_NFE,QTDE_VEZES_BIPADO,CNPJ,CONVERT(DATE,DATA_HORA)DATA_HORA,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3,(QTDE_BIPADO-QTDE_TOTAL)COMPLEMENTAR,CASE WHEN (QTDE_BIPADO - QTDE_TOTAL) > 0 THEN 'SOBRA' WHEN (QTDE_BIPADO - QTDE_TOTAL) < 0 THEN 'FALTA'ELSE 'OK'END DIVERGENCIA from AUX_ENTRADA  WHERE " +
         " Convert(date,DATA_HORA)  >= '" + data_inicio + "' and  Convert(date,DATA_HORA)  <= '" + data_fim + "' and  CNPJ = '"+cnpj+"' ORDER BY DATA_HORA";

            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Relatorio> list = new List<Relatorio>();
                Relatorio nota = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            nota = new Relatorio();
                            nota.emissao = reader["DATA_HORA"].ToString();
                            nota.nf_entrada = reader["NOTA_FISCAL"].ToString();
                            nota.nome_clifor = reader["FORNECEDOR"].ToString();
                            nota.grupo_produto = reader["GRUPO"].ToString();
                            nota.subgrupo_produto = reader["SUBGRUPO"].ToString();
                            nota.produto = reader["PRODUTO"].ToString();
                            nota.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            nota.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            nota.qtde_total = reader["QTDE_TOTAL"].ToString();
                            nota.responsavel = reader["RESPONSAVEL"].ToString();
                            nota.cnpj = reader["CNPJ"].ToString();
                            nota.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            nota.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            nota.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();
                            nota.complementar = reader["COMPLEMENTAR"].ToString();
                            nota.divergencia = reader["DIVERGENCIA"].ToString();
                            nota.desc_produto = reader["DESC_PRODUTO"].ToString();
                            nota.custo = reader["PRECO1"].ToString();

                            list.Add(nota);
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
        public List<Relatorio> BuscaRetornoListaNFsPorDataRetaguarda(string data_inicio, string data_fim)
        {
            string sql = "select ID_AUX_ENTRADA,IDS_NFS_SELECIONADAS,NOTA_FISCAL,ROMANEIO_PRODUTO,PRODUTO,COR_PRODUTO," +
     "TAMANHO,CODIGO_BARRA,PRECO1,QTDE_TOTAL,QTDE_BIPADO,DESC_PRODUTO,GRUPO,SUBGRUPO,FORNECEDOR,RESPONSAVEL," +
     "CHAVE_NFE,QTDE_VEZES_BIPADO,CNPJ,CONVERT(DATE,DATA_HORA)DATA_HORA,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3,(QTDE_BIPADO-QTDE_TOTAL)COMPLEMENTAR,CASE WHEN (QTDE_BIPADO - QTDE_TOTAL) > 0 THEN 'SOBRA' WHEN (QTDE_BIPADO - QTDE_TOTAL) < 0 THEN 'FALTA'ELSE 'OK'END DIVERGENCIA from AUX_ENTRADA  WHERE " +
        " Convert(date,DATA_HORA)  >= '"+ data_inicio + "' and  Convert(date,DATA_HORA)  <= '" + data_fim + "'  ORDER BY DATA_HORA";

            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Relatorio> list = new List<Relatorio>();
                Relatorio nota = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            nota = new Relatorio();
                            nota.emissao = reader["DATA_HORA"].ToString();
                            nota.nf_entrada = reader["NOTA_FISCAL"].ToString();
                            nota.nome_clifor = reader["FORNECEDOR"].ToString();
                            nota.grupo_produto = reader["GRUPO"].ToString();
                            nota.subgrupo_produto = reader["SUBGRUPO"].ToString();
                            nota.produto = reader["PRODUTO"].ToString();
                            nota.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            nota.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            nota.qtde_total = reader["QTDE_TOTAL"].ToString();
                            nota.responsavel = reader["RESPONSAVEL"].ToString();
                            nota.cnpj = reader["CNPJ"].ToString();
                            nota.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            nota.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            nota.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();
                            nota.complementar = reader["COMPLEMENTAR"].ToString();
                            nota.divergencia = reader["DIVERGENCIA"].ToString();
                            nota.desc_produto = reader["DESC_PRODUTO"].ToString();
                            nota.custo = reader["PRECO1"].ToString();

                            list.Add(nota);
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
        public List<Relatorio> BuscaRetornoListaNFsPorData(string data_inicio , string data_fim )
        {
            string sql = "select EMISSAO,b.NUMERO_NF_TRANSFERENCIA NF_ENTRADA,A.NOME_CLIFOR,A.SERIE_NF_ENTRADA,d.grupo GRUPO_PRODUTO,d.SUBGRUPO SUBGRUPO_PRODUTO,d.PRODUTO, " +
                " d.CODIGO_BARRA,D.QTDE_BIPADO,D.QTDE_TOTAL,D.RESPONSAVEL,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3 " +
                " from NFE_XML_LOJA A LEFT JOIN LOJA_ENTRADAS B ON(A.CHAVE_NFE = B.CHAVE_NFE) left join(SELECT DISTINCT  SS.[value] " +
                "as IDS_NFS, RESPONSAVEL,  QTDE_BIPADO,QTDE_TOTAL,CODIGO_BARRA,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3,grupo, " +
                "SUBGRUPO, produto  from AUX_ENTRADA as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) D  ON " +
                "D.IDS_NFS = A.ID_XML_NFE LEFT JOIN(SELECT DISTINCT  SS.[value] as IDS_NFS, RESPONSAVEL,IDS_NFS_SELECIONADAS,ENTRADA_STATUS  " +
                "from AUX_ENTRADA_STATUS as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) F ON F.IDS_NFS = A.ID_XML_NFE " +
                "WHERE EMISSAO >='"+ data_inicio + "' and EMISSAO <= '"+ data_fim + "' ORDER BY EMISSAO,ROMANEIO_PRODUTO";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Relatorio> list = new List<Relatorio>();
                Relatorio nota = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            nota = new Relatorio();
                            nota.emissao = reader["EMISSAO"].ToString();
                            nota.nf_entrada = reader["NF_ENTRADA"].ToString();
                            nota.nome_clifor = reader["NOME_CLIFOR"].ToString();
                            nota.serie_nfe_entrada = reader["SERIE_NF_ENTRADA"].ToString();
                            nota.grupo_produto = reader["GRUPO_PRODUTO"].ToString();
                            nota.subgrupo_produto = reader["SUBGRUPO_PRODUTO"].ToString();
                            nota.produto = reader["PRODUTO"].ToString();
                            nota.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            nota.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            nota.qtde_total = reader["QTDE_TOTAL"].ToString();
                            nota.responsavel = reader["RESPONSAVEL"].ToString();
                            nota.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            nota.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            nota.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();

                            list.Add(nota);
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

        public List<Relatorio> BuscaRetornoNFsDiaAtualRetaguarda()
        {
            string sql = "select ID_AUX_ENTRADA,IDS_NFS_SELECIONADAS,NOTA_FISCAL,ROMANEIO_PRODUTO,PRODUTO,COR_PRODUTO," +
         "TAMANHO,CODIGO_BARRA,PRECO1,QTDE_TOTAL,QTDE_BIPADO,DESC_PRODUTO,GRUPO,SUBGRUPO,FORNECEDOR,RESPONSAVEL," +
         "CHAVE_NFE,QTDE_VEZES_BIPADO,CNPJ,CONVERT(DATE,DATA_HORA)DATA_HORA,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3,(QTDE_BIPADO-QTDE_TOTAL)COMPLEMENTAR,CASE WHEN (QTDE_BIPADO - QTDE_TOTAL) > 0 THEN 'SOBRA' WHEN (QTDE_BIPADO - QTDE_TOTAL) < 0 THEN 'FALTA'ELSE 'OK'END DIVERGENCIA from AUX_ENTRADA  WHERE " +
            " Convert(date,DATA_HORA)  >= Convert(date,getdate())  ORDER BY DATA_HORA";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Relatorio> list = new List<Relatorio>();
                Relatorio nota = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            nota = new Relatorio();
                            nota.emissao = reader["DATA_HORA"].ToString();
                            nota.nf_entrada = reader["NOTA_FISCAL"].ToString();
                            nota.nome_clifor = reader["FORNECEDOR"].ToString();
                            nota.grupo_produto = reader["GRUPO"].ToString();
                            nota.subgrupo_produto = reader["SUBGRUPO"].ToString();
                            nota.produto = reader["PRODUTO"].ToString();
                            nota.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            nota.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            nota.qtde_total = reader["QTDE_TOTAL"].ToString();
                            nota.responsavel = reader["RESPONSAVEL"].ToString();
                            nota.cnpj = reader["CNPJ"].ToString();
                            nota.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            nota.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            nota.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();
                            nota.complementar = reader["COMPLEMENTAR"].ToString();
                            nota.divergencia = reader["DIVERGENCIA"].ToString();
                            nota.desc_produto = reader["DESC_PRODUTO"].ToString();
                            nota.custo = reader["PRECO1"].ToString();

                            list.Add(nota);
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
        public List<Relatorio> BuscaRetornoNFsDiaAtual()
        {
            string sql = "select EMISSAO,b.NUMERO_NF_TRANSFERENCIA NF_ENTRADA,A.NOME_CLIFOR,A.SERIE_NF_ENTRADA,d.grupo GRUPO_PRODUTO,d.SUBGRUPO SUBGRUPO_PRODUTO,d.PRODUTO,  " +
                "d.CODIGO_BARRA,D.QTDE_BIPADO,D.QTDE_TOTAL,D.RESPONSAVEL,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3  " +
                "from NFE_XML_LOJA A LEFT JOIN LOJA_ENTRADAS B ON(A.CHAVE_NFE = B.CHAVE_NFE) left join(SELECT DISTINCT  " +
                "SS.[value] as IDS_NFS, RESPONSAVEL,  QTDE_BIPADO,QTDE_TOTAL,CODIGO_BARRA,QTDE_CONTADA1,QTDE_CONTADA2,QTDE_CONTADA3, " +
                "grupo, SUBGRUPO, produto  from AUX_ENTRADA as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) D " +
                " ON D.IDS_NFS = A.ID_XML_NFE LEFT JOIN(SELECT DISTINCT  SS.[value] as IDS_NFS, RESPONSAVEL,IDS_NFS_SELECIONADAS, " +
                "ENTRADA_STATUS  from AUX_ENTRADA_STATUS as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) F " +
                "ON F.IDS_NFS = A.ID_XML_NFE WHERE EMISSAO >= CONVERT(DATE,GETDATE())  ORDER BY EMISSAO,ROMANEIO_PRODUTO";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Relatorio> list = new List<Relatorio>();
                Relatorio nota = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            nota = new Relatorio();
                            nota.emissao = reader["EMISSAO"].ToString();
                            nota.nf_entrada = reader["NF_ENTRADA"].ToString();
                            nota.nome_clifor = reader["NOME_CLIFOR"].ToString();
                            nota.serie_nfe_entrada = reader["SERIE_NF_ENTRADA"].ToString();
                            nota.grupo_produto = reader["GRUPO_PRODUTO"].ToString();
                            nota.subgrupo_produto = reader["SUBGRUPO_PRODUTO"].ToString();
                            nota.produto = reader["PRODUTO"].ToString();
                            nota.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            nota.qtde_bipado = reader["QTDE_BIPADO"].ToString();
                            nota.qtde_total = reader["QTDE_TOTAL"].ToString();
                            nota.responsavel = reader["RESPONSAVEL"].ToString();
                            nota.qtde_contada1 = reader["QTDE_CONTADA1"].ToString();
                            nota.qtde_contada2 = reader["QTDE_CONTADA2"].ToString();
                            nota.qtde_contada3 = reader["QTDE_CONTADA3"].ToString();

                            list.Add(nota);
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

        public override void Atualizar(Relatorio entity)
        {
            throw new NotImplementedException();
        }

        public override List<Relatorio> BuscaCompletaRetornoLista()
        {
            throw new NotImplementedException();
        }

        public override List<Relatorio> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override Relatorio BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override Relatorio BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Relatorio> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(Relatorio entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(Relatorio entity)
        {
            throw new NotImplementedException();
        }
    }
}