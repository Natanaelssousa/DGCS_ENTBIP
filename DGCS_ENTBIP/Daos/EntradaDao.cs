using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class EntradaDao : Conexao<Entrada, int>
    {
        private string StringConnectionfilial;


        public EntradaDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }
        public override void Atualizar(Entrada entity)
        {
            throw new NotImplementedException();
        }


        public Entrada BuscaRomaneioExistenteEntrada(string romaneio)
        {
            string sql = "select ROMANEIO_PRODUTO,NUMERO_NF_TRANSFERENCIA FROM LOJA_ENTRADAS where ROMANEIO_PRODUTO = '"+romaneio+"'";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                Entrada Entrada = null;
                Entrada EntradaVazia = new Entrada();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Entrada = new Entrada();
                            Entrada.romaneio_produto = reader["ROMANEIO_PRODUTO"].ToString();
                            Entrada.numero_nf_transferencia = reader["NUMERO_NF_TRANSFERENCIA"].ToString();

                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                if (Entrada == null)
                {
                    return EntradaVazia;
                }
                else
                {
                    return Entrada;
                }
            }
        }

        public string consultaSequenciaNotaEntrada()
        {
            string sql = "SELECT right('000000' + rtrim(NUMERO_NF+1),6) NUMERO_NF FROM SEQUENCIAL_NF_ENTRADA";

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
                    return "000000";
                }
                else
                {
                    return nf_sequencia;
                }
            }
        }

        public void AtualizaSequencial()
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE SEQUENCIAL_NF_ENTRADA SET NUMERO_NF = NUMERO_NF+1 ";

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
        public override List<Entrada> BuscaCompletaRetornoLista()
        {
            throw new NotImplementedException();
        }

        public override List<Entrada> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override Entrada BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override Entrada BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Entrada> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(Entrada entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(Entrada entity)
        {
           
            using (var conn = new SqlConnection(StringConnectionfilial))
            {

              

                string sql = "INSERT INTO [dbo].[LOJA_ENTRADAS] " +
             " ([ROMANEIO_PRODUTO] " +
              " ,[FILIAL] " +
              " ,[CODIGO_TAB_PRECO] " +
              " ,[TIPO_ENTRADA_SAIDA] " +
              " ,[FILIAL_ORIGEM] " +
              " ,[NUMERO_NF_TRANSFERENCIA] " +
              " ,[ROMANEIO_NF_SAIDA] " +
              " ,[FORNECEDOR] " +
              " ,[RESPONSAVEL] " +
              " ,[EMISSAO] " +
              " ,[DATA_SAIDA] " +
              " ,[DESC_TIPO_ENTRADA_SAIDA] " +
              " ,[OBS] " +
              " ,[ENTRADA_CONFERIDA] " +
              " ,[ENTRADA_SEM_PRODUTOS] " +
              " ,[QTDE_TOTAL] " +
              " ,[VALOR_TOTAL] " +
              " ,[FATOR_PRECO] " +
              " ,[STATUS_TRANSITO] " +
              " ,[QTDE_NAO_CONFERIDA] " +
              " ,[VALOR_NAO_CONFERIDO] " +
              " ,[ENTRADA_POR] " +
              " ,[CGC_FORNECEDOR] " +
              " ,[PEDIDO_STATUS_LOJA] " +
              " ,[DATA_BAIXA_PEDIDO] " +
              " ,[ENTRADA_ENCERRADA] " +
              " ,[DATA_PARA_TRANSFERENCIA] " +
              " ,[SERIE_NF_ENTRADA] " +
              " ,[ENTRADA_CANCELADA] " +
              " ,[LX_STATUS_ENTRADA] " +
              " ,[DATA_ENTRADA_CONFERIDA] " +
              " ,[CHAVE_NFE] " +
              " ,[ESPECIE_SERIE] " +
              " ,[UTILIZA_PRECO_ORIGEM]) " +
        " VALUES  " +
             " ( '" + entity.romaneio_produto + "',  " +
             " '" + entity.filial + "',  " +
             " '" + entity.codigo_tab_preco + "',  " +
             " '" + entity.tipo_entrada_saida + "',  " +
             " '" + entity.filial_origem + "',  " +
              "'" + entity.numero_nf_transferencia + "',  " +
              "'" + entity.romaneio_nf_saida + "', " +
              "'" + entity.fornecedor + "', " +
              "'" + entity.responsavel + "', " +
              "CONVERT(DATE,GETDATE()), " +
              "'" + Convert.ToDateTime(entity.data_saida.Trim()).ToString("yyyyMMdd") + "',  " +
              "'" + entity.desc_tipo_entrada_saida + "',  " +
              "'" + entity.obs + "', " +
              "'" + entity.entrada_conferida + "',  " +
              "'" + entity.entrada_sem_produtos + "', " +
              "'" + entity.qtde_total + "', " +
              "'" + entity.valor_total + "', " +
              "'" + entity.fator_preco + "',  " +
              "'" + entity.status_transito + "',  " +
              "'" + entity.qtde_nao_conferida + "', " +
              "'" + entity.valor_nao_conferido + "',  " +
              "'" + entity.entrada_por + "',  " +
              "'" + entity.cnpj + "', " +
              "'" + entity.pedido_status_loja + "', " +
              "null,  " +
              "'" + entity.entrada_encerrada + "',  " +
              "GETDATE(),  " +
              "'" + entity.serie_nf_entrada + "', " +
              "'" + entity.entrada_cancelada + "', " +
              "'" + entity.lx_status_entrada + "', " +
              "GETDATE()," +
              "'" + entity.chave_nfe + "', " +
              "'" + entity.especie_serie + "', " +
              "'" + entity.utiliza_preco_origem + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlTransaction objTrans = null;

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