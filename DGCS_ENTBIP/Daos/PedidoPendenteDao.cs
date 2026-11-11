using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class PedidoPendenteDao : Conexao<PedidoPendente, int>
    {

        private string StringConnectionfilial;


        public PedidoPendenteDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }
        public override void Atualizar(PedidoPendente entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE PEDIDO_PENDENTES SET STATUS = '1' WHERE CHAVE_NFE = '"+entity.chave_nfe+"'";

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

        public override List<PedidoPendente> BuscaCompletaRetornoLista()
        {
            string sql = "SELECT * FROM PEDIDO_PENDENTES WHERE STATUS = '0'";

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                PedidoPendente pedido = null;
                PedidoPendente pedidoVazio = new PedidoPendente();
                List<PedidoPendente> list = new List<PedidoPendente>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            pedido = new PedidoPendente();
                            pedido.id = reader["ID"].ToString();
                            pedido.pedido = reader["PEDIDO"].ToString();
                            pedido.filial = reader["FILIAL"].ToString();
                            pedido.nome_clifor = reader["NOME_CLIFOR"].ToString();
                            pedido.nf_entrada = reader["NF_ENTRADA"].ToString();
                            pedido.serie_nf_entrada = reader["SERIE_NF_ENTRADA"].ToString();
                            pedido.chave_nfe = reader["CHAVE_NFE"].ToString();
                            pedido.ilha = reader["ILHA"].ToString();
                            pedido.desc_ilha = reader["DESC_ILHA"].ToString();
                            pedido.status = reader["STATUS"].ToString();


                            list.Add(pedido);
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

        public override List<PedidoPendente> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override PedidoPendente BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override PedidoPendente BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<PedidoPendente> BuscaRetornoPalavrasChaves(string palavra)
        {
            string sql = "SELECT * FROM PEDIDO_PENDENTES WHERE (NOME_CLIFOR LIKE '"+ palavra + "%' OR NF_ENTRADA LIKE '" + palavra + "%')";

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                PedidoPendente pedido = null;
                PedidoPendente pedidoVazio = new PedidoPendente();
                List<PedidoPendente> list = new List<PedidoPendente>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            pedido = new PedidoPendente();
                            pedido.id = reader["ID"].ToString();
                            pedido.pedido = reader["PEDIDO"].ToString();
                            pedido.filial = reader["FILIAL"].ToString();
                            pedido.nome_clifor = reader["NOME_CLIFOR"].ToString();
                            pedido.nf_entrada = reader["NF_ENTRADA"].ToString();
                            pedido.serie_nf_entrada = reader["SERIE_NF_ENTRADA"].ToString();
                            pedido.chave_nfe = reader["CHAVE_NFE"].ToString();
                            pedido.ilha = reader["ILHA"].ToString();
                            pedido.desc_ilha = reader["DESC_ILHA"].ToString();
                            pedido.status = reader["STATUS"].ToString();


                            list.Add(pedido);
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

        public override void Remover(PedidoPendente entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(PedidoPendente entity)
        {
            throw new NotImplementedException();
        }
    }
}