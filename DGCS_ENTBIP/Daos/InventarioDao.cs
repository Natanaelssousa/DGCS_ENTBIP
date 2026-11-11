using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class InventarioDao : Conexao<Inventario, int>
    {


        private string StringConnectionfilial;

        public  InventarioDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }

    
        public override void Atualizar(Inventario entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "";
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

        public List<InventarioRelatorio> BuscaRetornoListaNFsPorDataInventario(string dataIni, string dataF, string codigoInventario)
        {
            DateTime datai = Convert.ToDateTime(dataIni);
            string dataInicio = datai.ToString("yyyyMMdd");
            DateTime dataf = Convert.ToDateTime(dataF);
            string dataFim = dataf.ToString("yyyyMMdd");

            string sql = "exec SP_DGCS_INVENTARIO_RELATORIO '" + dataInicio + "','" + dataFim + "','"+ codigoInventario + "'";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<InventarioRelatorio> list = new List<InventarioRelatorio>();
                InventarioRelatorio inventario = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            inventario = new InventarioRelatorio();
                            inventario.codigo_inventario = reader["CODIGO_INVENTARIO"].ToString();
                            inventario.filial = reader["FILIAL"].ToString();
                            inventario.desc_inventario = reader["DESC_INVENTARIO"].ToString();
                            inventario.emissao = reader["EMISSAO"].ToString();
                            inventario.produto = reader["PRODUTO"].ToString();
                            inventario.desc_prod_nf = reader["DESC_PROD_NF"].ToString();
                            inventario.grupo_produto = reader["GRUPO_PRODUTO"].ToString();
                            inventario.subgrupo_produto = reader["SUBGRUPO_PRODUTO"].ToString();
                            inventario.qtde = reader["QTDE"].ToString();
                            inventario.responsavel = reader["RESPONSAVEL"].ToString();


                            list.Add(inventario);
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


        public List<InventarioRelatorio> BuscaRetornoDataAtual()
        {

            DateTime datai = DateTime.Now;
            string dataInicio = datai.ToString("yyyyMMdd");
            DateTime dataf = DateTime.Now;
            string dataFim = dataf.ToString("yyyyMMdd");
            string sql = "exec SP_DGCS_INVENTARIO_RELATORIO '" + dataInicio + "','" + dataFim + "',''";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<InventarioRelatorio> list = new List<InventarioRelatorio>();
                InventarioRelatorio inventario = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            inventario = new InventarioRelatorio();
                            inventario.codigo_inventario = reader["CODIGO_INVENTARIO"].ToString();
                            inventario.filial = reader["FILIAL"].ToString();
                            inventario.desc_inventario = reader["DESC_INVENTARIO"].ToString();
                            inventario.emissao = reader["EMISSAO"].ToString();
                            inventario.produto = reader["PRODUTO"].ToString();
                            inventario.desc_prod_nf = reader["DESC_PROD_NF"].ToString();
                            inventario.grupo_produto = reader["GRUPO_PRODUTO"].ToString();
                            inventario.subgrupo_produto = reader["SUBGRUPO_PRODUTO"].ToString();
                            inventario.qtde = reader["QTDE"].ToString();
                            inventario.responsavel = reader["RESPONSAVEL"].ToString();


                            list.Add(inventario);
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
        public  List<InventarioRelatorio> BuscaRetornoListaPorData(string dataIni, string dataF)
        {
            DateTime datai = Convert.ToDateTime(dataIni);
            string dataInicio = datai.ToString("yyyyMMdd");
            DateTime dataf = Convert.ToDateTime(dataF);
            string dataFim = dataf.ToString("yyyyMMdd");

            string sql = "exec SP_DGCS_INVENTARIO_RELATORIO '"+ dataInicio + "','"+ dataFim + "',''";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<InventarioRelatorio> list = new List<InventarioRelatorio>();
                InventarioRelatorio inventario = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            inventario = new InventarioRelatorio();
                            inventario.codigo_inventario = reader["CODIGO_INVENTARIO"].ToString();
                            inventario.filial = reader["FILIAL"].ToString();
                            inventario.desc_inventario = reader["DESC_INVENTARIO"].ToString();
                            inventario.emissao = reader["EMISSAO"].ToString();
                            inventario.produto = reader["PRODUTO"].ToString();
                            inventario.desc_prod_nf = reader["DESC_PROD_NF"].ToString();
                            inventario.grupo_produto = reader["GRUPO_PRODUTO"].ToString();
                            inventario.subgrupo_produto = reader["SUBGRUPO_PRODUTO"].ToString();
                            inventario.qtde = reader["QTDE"].ToString();
                            inventario.responsavel = reader["RESPONSAVEL"].ToString();


                            list.Add(inventario);
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


        public override List<Inventario> BuscaCompletaRetornoLista()
        {
            string sql = "SELECT * FROM INVENTARIO WHERE EMISSAO >= convert(date,GETDATE());";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Inventario> list = new List<Inventario>();
                Inventario inventario = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            inventario = new Inventario();
                            inventario.codigo_inventario = reader["CODIGO_INVENTARIO"].ToString();
                            inventario.filial = reader["FILIAL"].ToString();
                            inventario.desc_inventario = reader["DESC_INVENTARIO"].ToString();
                            inventario.responsavel = reader["RESPONSAVEL"].ToString();
                            inventario.emissao = reader["EMISSAO"].ToString();




                            list.Add(inventario);
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

        public override List<Inventario> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override Inventario BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override Inventario BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Inventario> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(Inventario entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(Inventario entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "INSERT INTO INVENTARIO (CODIGO_INVENTARIO,FILIAL,DESC_INVENTARIO,RESPONSAVEL,EMISSAO,ARQUIVO_CONTAGEM,QTDE_TOTAL," +
                    "TODOS_PRODUTOS,VALIDO_E_FINALIZADO,OBS,DATA_PARA_TRANSFERENCIA,ESTOQUE_AJUSTADO,DATA_AJUSTE,RESPONSAVEL_AJUSTE" +
                    ",DATA_SALDO,LX_STATUS_INVENTARIO,DATA_FINALIZACAO) " +
                    "VALUES('"+ entity.codigo_inventario + "','" + entity.filial + "','" + entity.desc_inventario + "','" + entity.responsavel + "'" +
                    ",CONVERT(date, GETDATE()),'" + entity.arquivo_contagem + "','" + entity.qtde_total + "','" + entity.todos_produtos + "'" +
                    ",'" + entity.valido_e_finalizado + "','" + entity.obs + "',GETDATE(),'" + entity.estoque_ajustado + "'" +
                    ",null,'" + entity.responsavel_ajuste + "',null,'" + entity.lx_status_inventario + "'" +
                    ",CONVERT(date, GETDATE()))";
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
    }
}