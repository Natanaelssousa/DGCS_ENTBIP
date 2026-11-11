using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class AuxInventarioDao : Conexao<AuxInventario, int>
    {

        private string StringConnectionfilial;


        public AuxInventarioDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }

        public string BuscaProximoRomaneio()
        {
            string sql = "SELECT right(replicate('0',5) +convert(VARCHAR, ROMANEIO_INVENTARIO),5) as ROMANEIO_INVENTARIO FROM SEQUENCIAL_INVENTARIO";

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
                            romaneio = reader["ROMANEIO_INVENTARIO"].ToString();
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



        public void ExecutaSP_DGCS_PROCESSOS()
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "EXEC SP_DGCS_PROCESSOS";

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

        public void AtualizarRomaneio()
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE SEQUENCIAL_INVENTARIO SET ROMANEIO_INVENTARIO = ROMANEIO_INVENTARIO + 1 ";

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
        public override void Atualizar(AuxInventario entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE AUX_INVENTARIO SET QTDE_IMPORTADO ='" + entity.qtde_importado + "',DATA_HORA =GETDATE() " +
                    " WHERE RESPONSAVEL = '" + entity.responsavel + "' AND ROMANEIO_INVENTARIO = '" + entity.romaneio_inventario + "'" +
                    "AND (CODIGO_BARRA = '" + entity.codigo_barra + "' OR PRODUTO  = '" + entity.codigo_barra + "') ";
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



        public  void AtualizarStatus(string responsavel,string romaneio)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE AUX_INVENTARIO SET STATUS_INVENTARIO ='FINALIZADO' " +
                    " WHERE RESPONSAVEL = '" + responsavel + "' AND ROMANEIO_INVENTARIO = '" + romaneio + "'" +
                    "";
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

        public void RemoverPor(string romaneio, string responsavel)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {

                string sql = "DELETE FROM AUX_INVENTARIO WHERE ROMANEIO_INVENTARIO = '" + romaneio + "' and  RESPONSAVEL = '" + responsavel + "' ";

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

        public void RemoverPorItem(string romaneio, string responsavel, string codigo_barra)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {

                string sql = "DELETE FROM AUX_INVENTARIO WHERE ROMANEIO_INVENTARIO = '" + romaneio + "' and  RESPONSAVEL = '" + responsavel + "' AND CODIGO_BARRA = '"+codigo_barra+"' ";

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

        public override List<AuxInventario> BuscaCompletaRetornoLista()
        {
            throw new NotImplementedException();
        }

        public override List<AuxInventario> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override AuxInventario BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override AuxInventario BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<AuxInventario> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(AuxInventario entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }


        public AuxInventario ConsultaRomaneioEmAberto(string romaneio, string responsavel)
        {
            string sql = "SELECT top(1) ID_AUX_INVENTARIO,ROMANEIO_INVENTARIO,PRODUTO,COR_PRODUTO,TAMANHO,CODIGO_BARRA,QTDE_IMPORTADO,DESC_PRODUTO, " +
            "GRUPO,SUBGRUPO,FORNECEDOR,RESPONSAVEL,QTDE_VEZES_IMPORTADO,DATA_HORA,TIPO_INVENTARIO,STATUS_INVENTARIO,FILIAL FROM AUX_INVENTARIO  " +
                   "WHERE  ROMANEIO_INVENTARIO = '" + romaneio + "' and RESPONSAVEL = '" + responsavel + "'   AND STATUS_INVENTARIO = 'EM ABERTO'";





            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                AuxInventario produto = null;
                AuxInventario produtoVazio = new AuxInventario();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new AuxInventario();
                            produto.id_aux_inventario = reader["ID_AUX_INVENTARIO"].ToString();
                            produto.romaneio_inventario = reader["ROMANEIO_INVENTARIO"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.qtde_importado = reader["QTDE_IMPORTADO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.grupo = reader["GRUPO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO"].ToString();
                            produto.qtde_vezes_importado = reader["QTDE_VEZES_IMPORTADO"].ToString();
                            produto.tipo_inventario = reader["TIPO_INVENTARIO"].ToString();
                            produto.status_inventario = reader["STATUS_INVENTARIO"].ToString();
                            produto.filial = reader["FILIAL"].ToString();
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

        public AuxInventario ConsultaNotaEmAberto(string romaneio, string codigo_barra, string usuario)
        {
            string sql = "SELECT ID_AUX_INVENTARIO,ROMANEIO_INVENTARIO,PRODUTO,COR_PRODUTO,TAMANHO,CODIGO_BARRA,QTDE_IMPORTADO,DESC_PRODUTO, " + 
              "GRUPO,SUBGRUPO,FORNECEDOR,RESPONSAVEL,QTDE_VEZES_IMPORTADO,DATA_HORA,TIPO_INVENTARIO,STATUS_INVENTARIO,FILIAL FROM AUX_INVENTARIO  " +
                     "WHERE (CODIGO_BARRA = '" + codigo_barra + "' OR PRODUTO = '"+codigo_barra+"') AND ROMANEIO_INVENTARIO = '" + romaneio + "' and RESPONSAVEL = '" + usuario + "'   AND STATUS_INVENTARIO = 'EM ABERTO'";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                AuxInventario produto = null;
                AuxInventario produtoVazio = new AuxInventario();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new AuxInventario();
                            produto.id_aux_inventario = reader["ID_AUX_INVENTARIO"].ToString();
                            produto.romaneio_inventario = reader["ROMANEIO_INVENTARIO"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.qtde_importado = reader["QTDE_IMPORTADO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.grupo = reader["GRUPO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO"].ToString();
                            produto.qtde_vezes_importado = reader["QTDE_VEZES_IMPORTADO"].ToString();
                            produto.tipo_inventario = reader["TIPO_INVENTARIO"].ToString();
                            produto.status_inventario = reader["STATUS_INVENTARIO"].ToString();
                            produto.filial = reader["FILIAL"].ToString();
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


        public string ConsultaTotalEans(string responsavel)
        {
            string sql = "SELECT  count(CODIGO_BARRA) TOTAL_EANS FROM AUX_INVENTARIO  " +
                "WHERE RESPONSAVEL = '" + responsavel + "' AND STATUS_INVENTARIO = 'EM ABERTO'";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                string total = "0";
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {

                           total = reader["TOTAL_EANS"].ToString();
                           
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return total;
            }
        }


        public List<AuxInventario> ConsultaBipagem( string responsavel)
        {
            string sql = "SELECT ID_AUX_INVENTARIO,ROMANEIO_INVENTARIO,PRODUTO,COR_PRODUTO,TAMANHO,CODIGO_BARRA,QTDE_IMPORTADO,DESC_PRODUTO, "+
         "GRUPO,SUBGRUPO,FORNECEDOR,RESPONSAVEL,QTDE_VEZES_IMPORTADO,DATA_HORA,TIPO_INVENTARIO,STATUS_INVENTARIO,FILIAL FROM AUX_INVENTARIO  " +
                "WHERE RESPONSAVEL = '" + responsavel + "' AND STATUS_INVENTARIO = 'EM ABERTO' order by DATA_HORA desc";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                AuxInventario produto = null;
                AuxInventario produtoVazio = new AuxInventario();
                List<AuxInventario> list = new List<AuxInventario>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new AuxInventario();
                            produto.id_aux_inventario = reader["ID_AUX_INVENTARIO"].ToString();
                            produto.romaneio_inventario = reader["ROMANEIO_INVENTARIO"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.qtde_importado = reader["QTDE_IMPORTADO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.fornecedor = reader["FORNECEDOR"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.grupo = reader["GRUPO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO"].ToString();
                            produto.qtde_vezes_importado = reader["QTDE_VEZES_IMPORTADO"].ToString();
                            produto.tipo_inventario = reader["TIPO_INVENTARIO"].ToString();
                            produto.status_inventario = reader["STATUS_INVENTARIO"].ToString();
                            produto.filial = reader["FILIAL"].ToString();
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

        public override void Salvar(AuxInventario entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "INSERT INTO AUX_INVENTARIO(ROMANEIO_INVENTARIO,PRODUTO,COR_PRODUTO,TAMANHO,CODIGO_BARRA," +
                    "QTDE_IMPORTADO, DESC_PRODUTO,GRUPO,SUBGRUPO,FORNECEDOR,RESPONSAVEL,QTDE_VEZES_IMPORTADO,TIPO_INVENTARIO,STATUS_INVENTARIO,FILIAL)" +
                    " VALUES( '" + entity.romaneio_inventario + "', '" + entity.produto + "'," +
                    " '" + entity.cor_produto + "', '" + entity.tamanho + "', '" + entity.codigo_barra + "', '" + entity.qtde_importado + "', " +
                    "'" + entity.desc_produto + "', '" + entity.grupo + "', '" + entity.subgrupo + "', '" + entity.fornecedor + "'," +
                    " '" + entity.responsavel + "','" + entity.qtde_vezes_importado + "'," +
                    " '" + entity.tipo_inventario + "', '" + entity.status_inventario + "', '" + entity.filial + "')";

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