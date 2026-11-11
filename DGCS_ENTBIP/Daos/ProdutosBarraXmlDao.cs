using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class ProdutosBarraXmlDao : Conexao<ProdutosBarraXml, int>
    {

        private string StringConnectionfilial;


        public ProdutosBarraXmlDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }

        public override void Atualizar(ProdutosBarraXml entity)
        {
            throw new NotImplementedException();
        }

        public override List<ProdutosBarraXml> BuscaCompletaRetornoLista()
        {
            throw new NotImplementedException();
        }

        public List<ProdutosBarraXml> BuscaNotasParaDarEntrada ( string id_xml_nfe)
        {
            string ids = id_xml_nfe;
         

            string sql = "select ID_XML_NFE,CNPJ,CHAVE_NFE,NOME_CLIFOR,NF_ENTRADA,SERIE_NF_ENTRADA,CODIGO_BARRA,PRODUTO," +
                "DESC_PRODUTO,COR_PRODUTO,DESC_COR_PRODUTO,GRUPO_PRODUTO,SUBGRUPO_PRODUTO,GRIFFE,TAMANHO,GRADE,UNIDADE," +
                "QTDE,VALOR_UN,VALOR_PROD_TOT,DATA_SAIDA ,DESTINATARIO from PRODUTOS_BARRA_XML WHERE ID_XML_NFE IN (" + ids+ ") ORDER BY ID_XML_NFE";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                ProdutosBarraXml produto = null;
                List<ProdutosBarraXml> list = new List<ProdutosBarraXml>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new ProdutosBarraXml();
                            produto.id_xml_nfe = reader["ID_XML_NFE"].ToString();
                            produto.chave_nfe = reader["CHAVE_NFE"].ToString();
                            produto.nome_clifor = reader["NOME_CLIFOR"].ToString();
                            produto.nf_entrada = reader["NF_ENTRADA"].ToString();
                            produto.serie_nf_entrada = reader["SERIE_NF_ENTRADA"].ToString();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.desc_cor_produto = reader["DESC_COR_PRODUTO"].ToString();
                            produto.grupo_produto = reader["GRUPO_PRODUTO"].ToString();
                            produto.subgrupo_produto = reader["SUBGRUPO_PRODUTO"].ToString();
                            produto.griffe = reader["GRIFFE"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.grade = reader["GRADE"].ToString();
                            produto.unidade = reader["UNIDADE"].ToString();
                            produto.qtde = reader["QTDE"].ToString();
                            produto.valor_un = reader["VALOR_UN"].ToString();
                            produto.cnpj = reader["CNPJ"].ToString();
                            produto.valor_prod_tot = reader["VALOR_PROD_TOT"].ToString();
                            produto.data_saida = reader["DATA_SAIDA"].ToString();
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

        public override List<ProdutosBarraXml> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override ProdutosBarraXml BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override ProdutosBarraXml BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<ProdutosBarraXml> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(ProdutosBarraXml entity)
        {
            throw new NotImplementedException();
        }


        public bool ExecutaSP_DGCS_PROCESSA_NFE_XML_LOJA(string codigo_loja)
        {
            DateTime data = DateTime.Today.AddDays(-7);
            string dataCorreta = data.ToString("yyyyMMdd");


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "EXEC SP_DGCS_PROCESSA_NFE_XML_LOJA '"+ codigo_loja + "', '"+ dataCorreta + "' ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                bool retorno = false;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    retorno = true;
                }
                catch (Exception e)
                {
                    retorno = false;
                    throw e;

                }


                conn.Close();
                SqlConnection.ClearPool(conn);
                return retorno;
            }
        }

        public bool ExecutaSP_DGCS_PROCESSOS()
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "exec SP_DGCS_PROCESSOS";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                bool retorno = false;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    retorno =  true;
                }
                catch (Exception e)
                {
                    retorno = false;
                    throw e;
                    
                }

            
                conn.Close();
                SqlConnection.ClearPool(conn);
                return retorno;
            }
        }


        public bool ExecutaSP_DGCS_PROCESSOS_retaguarda()
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "exec SP_DGCS_PROCESSOS ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                bool retorno = false;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    retorno = true;
                }
                catch (Exception e)
                {
                    retorno = false;
                    throw e;

                }


                conn.Close();
                SqlConnection.ClearPool(conn);
                return retorno;
            }
        }
        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(ProdutosBarraXml entity)
        {
            throw new NotImplementedException();
        }
    }
}