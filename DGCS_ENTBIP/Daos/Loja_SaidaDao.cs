using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class Loja_SaidaDao : Conexao<Loja_Saidas, int>
    {

        private string StringConnectionfilial;


        public Loja_SaidaDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }

        public string  consultaSequenciaNotaSaida()
        {
            string sql = "SELECT right('000000000' + rtrim(NUMERO_NF+1),9) NUMERO_NF FROM SEQUENCIAL_NF_SAIDA";

            string nf_sequencia = "";

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

                            nf_sequencia = reader["NUMERO_NF"].ToString();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                if (nf_sequencia == "")
                {
                    return "000000000";
                }
                else
                {
                    return nf_sequencia;
                }
            }
        }
        public override void Atualizar(Loja_Saidas entity)
        {
            throw new NotImplementedException();
        }

        public override List<Loja_Saidas> BuscaCompletaRetornoLista()
        {
            throw new NotImplementedException();
        }

        public override List<Loja_Saidas> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override Loja_Saidas BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override Loja_Saidas BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Loja_Saidas> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(Loja_Saidas entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        private void AtualizaSequencial()
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE SEQUENCIAL_NF_SAIDA SET NUMERO_NF = NUMERO_NF+1 ";

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


        public void SalvarNfCh(Loja_Saidas entity, string chaveEntrada, string nfEntrada)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = @"INSERT INTO LOJA_SAIDAS" +
           "(ROMANEIO_PRODUTO, FILIAL, CODIGO_TAB_PRECO, TIPO_ENTRADA_SAIDA, FILIAL_DESTINO" +
          " , NUMERO_NF_TRANSFERENCIA, SERIE_NF, RESPONSAVEL, INDICA_DEVOLUCAO, FORNECEDOR_DEVOLUCAO" +
          " , EMISSAO, OBS, QTDE_TOTAL, VALOR_TOTAL, FATOR_PRECO, STATUS_TRANSITO, DATA_PARA_TRANSFERENCIA" +
          " , SAIDA_ENCERRADA, SAIDA_CANCELADA, SUGESTAO_TRANSFERENCIA, LX_STATUS_SAIDA)" +
          " VALUES" +
          " ('" + entity.romaneio_produto + "', '" + entity.filial + "', '" + entity.codigo_tab_preco + "', '" + entity.tipo_entrada_saida + "', '" + entity.filial_destino + "'" +
          " , '', '" + entity.serie_nf + "', '" + entity.responsavel + "', '0', NULL," +
          "CONVERT(DATE, GETDATE()), 'Dados nota de entrada Nf entrada: "+nfEntrada+" Chave nfe entrada: " + chaveEntrada + "', '" + entity.qtde_total + "', '" + entity.valor_total.Replace(',', '.') + "', '0.00', NULL, GETDATE()," +
           "'0', '0', NULL, '1')";



                SqlCommand cmd = new SqlCommand(sql, conn);


                try
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();
                    AtualizaSequencial();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }
        public override void Salvar(Loja_Saidas entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = @"INSERT INTO LOJA_SAIDAS" +
           "(ROMANEIO_PRODUTO, FILIAL, CODIGO_TAB_PRECO, TIPO_ENTRADA_SAIDA, FILIAL_DESTINO" +
          " , NUMERO_NF_TRANSFERENCIA, SERIE_NF, RESPONSAVEL, INDICA_DEVOLUCAO, FORNECEDOR_DEVOLUCAO" +
          " , EMISSAO, OBS, QTDE_TOTAL, VALOR_TOTAL, FATOR_PRECO, STATUS_TRANSITO, DATA_PARA_TRANSFERENCIA" +
          " , SAIDA_ENCERRADA, SAIDA_CANCELADA, SUGESTAO_TRANSFERENCIA, LX_STATUS_SAIDA)" +
          " VALUES" +
          " ('" + entity.romaneio_produto + "', '" + entity.filial + "', '"+ entity.codigo_tab_preco + "', '"+entity.tipo_entrada_saida+"', '" + entity.filial_destino + "'" +
          " , '', '" + entity.serie_nf + "', '" + entity.responsavel + "', '0', NULL," +
          "CONVERT(DATE, GETDATE()), NULL, '" + entity.qtde_total + "', '" + entity.valor_total.Replace(',', '.') + "', '0.00', NULL, GETDATE()," +
           "'0', '0', NULL, '1')";



                SqlCommand cmd = new SqlCommand(sql, conn);


                try
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();
                    AtualizaSequencial();
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
            }
        }
    }
}