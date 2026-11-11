using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class NotaPendenteDao : Conexao<NotaPendente, int>
    {

        private string StringConnectionfilial;


        public NotaPendenteDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }


        public override void Atualizar(NotaPendente entity)
        {
            throw new NotImplementedException();
        }


        public int BuscaQtdeTotalNota(string id_xml_nfe)
        {
            string sql = "select sum(convert(int,qtde)) qtde_total from PRODUTOS_BARRA_XML where id_xml_nfe = '" + id_xml_nfe + "'";

            int qtdeTotal = 0;

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
                            qtdeTotal = Convert.ToInt32(reader["qtde_total"].ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return qtdeTotal;
            }
        }

        public List<NotaPendente> BuscaCompletaRetornoListaNFs()
        {
            string sql = "select distinct C.DATA_AUTORIZACAO_NFE,C.NF_ENTRADA,A.NOME_CLIFOR,A.SERIE_NF_ENTRADA,A.CHAVE_NFE,A.ID_XML_NFE " +
                ", SUM(convert(int, A.QTDE))QTDE,CASE WHEN D.RESPONSAVEL IS NULL AND F.RESPONSAVEL IS NULL  THEN 'Pendente' ELSE 'Em andamento' " +
                "END STATUS,CASE WHEN D.RESPONSAVEL IS NULL AND F.RESPONSAVEL IS NULL THEN 'Pendente' ELSE CASE WHEN D.RESPONSAVEL IS NULL THEN " +
                "F.RESPONSAVEL ELSE D.RESPONSAVEL END END RESPONSAVEL,CASE WHEN A.XPED IS NULL THEN '0' ELSE A.XPED END XPED from PRODUTOS_BARRA_XML A LEFT JOIN LOJA_ENTRADAS B ON  (A.CHAVE_NFE = B.CHAVE_NFE)" +
                " inner join  NFE_XML_LOJA C ON(A.ID_XML_NFE = C.ID_XML_NFE) left join(SELECT DISTINCT  SS.[value] as IDS_NFS, RESPONSAVEL," +
                "IDS_NFS_SELECIONADAS from AUX_ENTRADA as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) D " +
                "ON D.IDS_NFS = A.ID_XML_NFE LEFT JOIN (SELECT DISTINCT  SS.[value] as IDS_NFS, RESPONSAVEL,IDS_NFS_SELECIONADAS,ENTRADA_STATUS " +
                "from AUX_ENTRADA_STATUS as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) F  ON F.IDS_NFS = A.ID_XML_NFE WHERE " +
                " (ENTRADA_STATUS IS NULL or ENTRADA_STATUS = 0)  group by  C.NF_ENTRADA,A.NOME_CLIFOR,A.SERIE_NF_ENTRADA,A.CHAVE_NFE,A.ID_XML_NFE,D.RESPONSAVEL,ENTRADA_STATUS," +
                "F.RESPONSAVEL,C.DATA_AUTORIZACAO_NFE,A.XPED ORDER BY C.DATA_AUTORIZACAO_NFE";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<NotaPendente> list = new List<NotaPendente>();
                NotaPendente nota = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            nota = new NotaPendente();
                            nota.nf_entrada = reader["NF_ENTRADA"].ToString();
                            nota.nome_clifor = reader["NOME_CLIFOR"].ToString();
                            nota.serie_nf_entrada = reader["SERIE_NF_ENTRADA"].ToString();
                            nota.chave_nfe = reader["CHAVE_NFE"].ToString();
                            nota.id_xml_nfe = reader["ID_XML_NFE"].ToString();
                            nota.qtde = reader["qtde"].ToString();
                            nota.status = reader["STATUS"].ToString();
                            nota.responsavel = reader["RESPONSAVEL"].ToString();
                            nota.xped = reader["XPED"].ToString();
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

        public override List<NotaPendente> BuscaCompletaRetornoLista()
        {
            throw new NotImplementedException();
        }

        public override List<NotaPendente> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override NotaPendente BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override NotaPendente BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<NotaPendente> BuscaRetornoPalavrasChaves(string palavra)
        {

            string sql = "select distinct C.DATA_AUTORIZACAO_NFE,C.NF_ENTRADA,A.NOME_CLIFOR,A.SERIE_NF_ENTRADA,A.CHAVE_NFE,A.ID_XML_NFE " +
                ", SUM(convert(int, A.QTDE))QTDE,CASE WHEN D.RESPONSAVEL IS NULL AND F.RESPONSAVEL IS NULL  THEN 'Pendente' ELSE 'Em andamento' " +
                "END STATUS,CASE WHEN D.RESPONSAVEL IS NULL AND F.RESPONSAVEL IS NULL THEN 'Pendente' ELSE CASE WHEN D.RESPONSAVEL IS NULL THEN " +
                "F.RESPONSAVEL ELSE D.RESPONSAVEL END END RESPONSAVEL,CASE WHEN A.XPED IS NULL THEN '0' ELSE A.XPED END XPED from PRODUTOS_BARRA_XML A LEFT JOIN LOJA_ENTRADAS B ON  (A.CHAVE_NFE = B.CHAVE_NFE)" +
                " inner join  NFE_XML_LOJA C ON(A.ID_XML_NFE = C.ID_XML_NFE) left join(SELECT DISTINCT  SS.[value] as IDS_NFS, RESPONSAVEL," +
                "IDS_NFS_SELECIONADAS from AUX_ENTRADA as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) D " +
                "ON D.IDS_NFS = A.ID_XML_NFE LEFT JOIN (SELECT DISTINCT  SS.[value] as IDS_NFS, RESPONSAVEL,IDS_NFS_SELECIONADAS,ENTRADA_STATUS " +
                "from AUX_ENTRADA_STATUS as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) F  ON F.IDS_NFS = A.ID_XML_NFE WHERE " +
                 " EMISSAO IS NULL   and   (C.NF_ENTRADA like ('" + palavra + "%') or  A.NOME_CLIFOR like('" + palavra + "%'))  " +
                " group by  C.NF_ENTRADA,A.NOME_CLIFOR,A.SERIE_NF_ENTRADA,A.CHAVE_NFE,A.ID_XML_NFE,D.RESPONSAVEL,ENTRADA_STATUS," +
                "F.RESPONSAVEL,C.DATA_AUTORIZACAO_NFE,A.XPED ORDER BY C.DATA_AUTORIZACAO_NFE";

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<NotaPendente> list = new List<NotaPendente>();
                NotaPendente nota = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            nota = new NotaPendente();
                            nota.nf_entrada = reader["NF_ENTRADA"].ToString();
                            nota.nome_clifor = reader["NOME_CLIFOR"].ToString();
                            nota.serie_nf_entrada = reader["SERIE_NF_ENTRADA"].ToString();
                            nota.chave_nfe = reader["CHAVE_NFE"].ToString();
                            nota.id_xml_nfe = reader["ID_XML_NFE"].ToString();
                            nota.qtde = reader["qtde"].ToString();
                            nota.status = reader["STATUS"].ToString();
                            nota.responsavel = reader["RESPONSAVEL"].ToString();
                            nota.xped = reader["XPED"].ToString();


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


        public  NotaPendente BuscaNotaClicada(string palavra)
        {
            string sql = "select distinct C.DATA_AUTORIZACAO_NFE,C.NF_ENTRADA,A.NOME_CLIFOR,A.SERIE_NF_ENTRADA,A.CHAVE_NFE,A.ID_XML_NFE " +
     ", SUM(convert(int, A.QTDE))QTDE,CASE WHEN D.RESPONSAVEL IS NULL " +
     "  THEN 'Pendente' ELSE 'Em andamento' END STATUS,CASE WHEN D.RESPONSAVEL IS NULL THEN 'Pendente' ELSE D.RESPONSAVEL END RESPONSAVEL,CASE WHEN A.XPED IS NULL THEN '0' ELSE A.XPED END XPED " +
     " from PRODUTOS_BARRA_XML A LEFT JOIN LOJA_ENTRADAS B ON " +
     " (A.CHAVE_NFE = B.CHAVE_NFE) inner join  NFE_XML_LOJA C ON(A.ID_XML_NFE = C.ID_XML_NFE) " +
     "left join(SELECT DISTINCT  SS.[value] as IDS_NFS, RESPONSAVEL " +
     "from AUX_ENTRADA as E cross apply String_Split(E.IDS_NFS_SELECIONADAS, ',') as SS ) D ON " +
     "D.IDS_NFS = A.ID_XML_NFE " +
     "WHERE EMISSAO IS NULL   and   (C.NF_ENTRADA like ('" + palavra + "%'))  " +
     "group by  C.NF_ENTRADA,A.NOME_CLIFOR,A.SERIE_NF_ENTRADA,A.CHAVE_NFE,A.ID_XML_NFE,D.RESPONSAVEL,C.DATA_AUTORIZACAO_NFE,A.XPED ORDER BY C.DATA_AUTORIZACAO_NFE";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<NotaPendente> list = new List<NotaPendente>();
                NotaPendente nota = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            nota = new NotaPendente();
                            nota.nf_entrada = reader["NF_ENTRADA"].ToString();
                            nota.nome_clifor = reader["NOME_CLIFOR"].ToString();
                            nota.serie_nf_entrada = reader["SERIE_NF_ENTRADA"].ToString();
                            nota.chave_nfe = reader["CHAVE_NFE"].ToString();
                            nota.id_xml_nfe = reader["ID_XML_NFE"].ToString();
                            nota.qtde = reader["qtde"].ToString();
                            nota.status = reader["STATUS"].ToString();
                            nota.responsavel = reader["RESPONSAVEL"].ToString();
                            nota.xped = reader["XPED"].ToString();


                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return nota;
            }
        }

        public override void Remover(NotaPendente entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(NotaPendente entity)
        {
            throw new NotImplementedException();
        }
    }
}