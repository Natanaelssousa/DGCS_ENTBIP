using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class LogRelatorioDao : Conexao<LogRelatorio, int>
    {
        public override void Atualizar(LogRelatorio entity)
        {
            throw new NotImplementedException();
        }

        public override List<LogRelatorio> BuscaCompletaRetornoLista()
        {
            string sql = "SELECT USUARIO,CNPJ,PERIODO_INICIO,PERIODO_FIM,NOTA_FISCAL,DATA_CONSULTA FROM LOG_RELATORIO WHERE DATA_CONSULTA >= GETDATE()-1 ";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<LogRelatorio> list = new List<LogRelatorio>();
                LogRelatorio log = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            log = new LogRelatorio();
                            log.usuario = reader["USUARIO"].ToString();
                            log.cnpj = reader["CNPJ"].ToString();
                            log.periodo_inicio = reader["PERIODO_INICIO"].ToString();
                            log.periodo_fim = reader["PERIODO_FIM"].ToString();
                            log.nota_fiscal = reader["NOTA_FISCAL"].ToString();
                            log.data_consulta = reader["DATA_CONSULTA"].ToString();


                            list.Add(log);
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

        public List<LogRelatorio> BuscaRetornoListaNFsPorDataRetaguarda(string data_inicio, string data_fim)
        {
            string sql = "SELECT USUARIO,CNPJ,PERIODO_INICIO,PERIODO_FIM,NOTA_FISCAL,DATA_CONSULTA FROM LOG_RELATORIO WHERE " +
            " CONVERT(DATE,DATA_CONSULTA) between '" + data_inicio + " ' and  '" + data_fim + "' ORDER BY DATA_CONSULTA";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<LogRelatorio> list = new List<LogRelatorio>();
                LogRelatorio log = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            log = new LogRelatorio();
                            log.usuario = reader["USUARIO"].ToString();
                            log.cnpj = reader["CNPJ"].ToString();
                            log.periodo_inicio = reader["PERIODO_INICIO"].ToString();
                            log.periodo_fim = reader["PERIODO_FIM"].ToString();
                            log.nota_fiscal = reader["NOTA_FISCAL"].ToString();
                            log.data_consulta = reader["DATA_CONSULTA"].ToString();

                            list.Add(log);
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

        public List<LogRelatorio> BuscaRetornoNFsPorUsuarioRetaguarda(string usuario)
        {
            string sql = "SELECT USUARIO,CNPJ,PERIODO_INICIO,PERIODO_FIM,NOTA_FISCAL,DATA_CONSULTA FROM LOG_RELATORIO WHERE " +
            " USUARIO = '" + usuario + " '  ORDER BY DATA_CONSULTA";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<LogRelatorio> list = new List<LogRelatorio>();
                LogRelatorio log = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            log = new LogRelatorio();
                            log.usuario = reader["USUARIO"].ToString();
                            log.cnpj = reader["CNPJ"].ToString();
                            log.periodo_inicio = reader["PERIODO_INICIO"].ToString();
                            log.periodo_fim = reader["PERIODO_FIM"].ToString();
                            log.nota_fiscal = reader["NOTA_FISCAL"].ToString();
                            log.data_consulta = reader["DATA_CONSULTA"].ToString();

                            list.Add(log);
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


        public List<LogRelatorio> BuscaRetornoListaNFsPorDataCnpjRetaguarda(string emissao_inicio,string emissao_fim, string cnpj)
        {
            string sql = "SELECT USUARIO,CNPJ,PERIODO_INICIO,PERIODO_FIM,NOTA_FISCAL,DATA_CONSULTA FROM LOG_RELATORIO WHERE " +
            "  CONVERT(DATE,DATA_CONSULTA) between '" + emissao_inicio + " ' and  '" + emissao_fim + "' and CNPJ = '"+cnpj+"'  ORDER BY DATA_CONSULTA";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<LogRelatorio> list = new List<LogRelatorio>();
                LogRelatorio log = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            log = new LogRelatorio();
                            log.usuario = reader["USUARIO"].ToString();
                            log.cnpj = reader["CNPJ"].ToString();
                            log.periodo_inicio = reader["PERIODO_INICIO"].ToString();
                            log.periodo_fim = reader["PERIODO_FIM"].ToString();
                            log.nota_fiscal = reader["NOTA_FISCAL"].ToString();
                            log.data_consulta = reader["DATA_CONSULTA"].ToString();

                            list.Add(log);
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

        public List<LogRelatorio> BuscaRetornoListaNFsPorUsuarioCnpjRetaguarda(string usuario, string cnpj)
        {
            string sql = "SELECT USUARIO,CNPJ,PERIODO_INICIO,PERIODO_FIM,NOTA_FISCAL,DATA_CONSULTA FROM LOG_RELATORIO WHERE " +
            " USUARIO = '"+usuario+"' and CNPJ = '" + cnpj + "'  ORDER BY DATA_CONSULTA";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<LogRelatorio> list = new List<LogRelatorio>();
                LogRelatorio log = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            log = new LogRelatorio();
                            log.usuario = reader["USUARIO"].ToString();
                            log.cnpj = reader["CNPJ"].ToString();
                            log.periodo_inicio = reader["PERIODO_INICIO"].ToString();
                            log.periodo_fim = reader["PERIODO_FIM"].ToString();
                            log.nota_fiscal = reader["NOTA_FISCAL"].ToString();
                            log.data_consulta = reader["DATA_CONSULTA"].ToString();

                            list.Add(log);
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



        public List<LogRelatorio> BuscaRetornoListaNFsPorCnpjRetaguarda( string cnpj)
        {
            string sql = "SELECT USUARIO,CNPJ,PERIODO_INICIO,PERIODO_FIM,NOTA_FISCAL,DATA_CONSULTA FROM LOG_RELATORIO WHERE " +
            "  CNPJ = '" + cnpj + "'  ORDER BY DATA_CONSULTA";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<LogRelatorio> list = new List<LogRelatorio>();
                LogRelatorio log = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            log = new LogRelatorio();
                            log.usuario = reader["USUARIO"].ToString();
                            log.cnpj = reader["CNPJ"].ToString();
                            log.periodo_inicio = reader["PERIODO_INICIO"].ToString();
                            log.periodo_fim = reader["PERIODO_FIM"].ToString();
                            log.nota_fiscal = reader["NOTA_FISCAL"].ToString();
                            log.data_consulta = reader["DATA_CONSULTA"].ToString();

                            list.Add(log);
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

        public override List<LogRelatorio> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override LogRelatorio BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override LogRelatorio BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<LogRelatorio> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(LogRelatorio entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(LogRelatorio entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "INSERT INTO LOG_RELATORIO (USUARIO,CNPJ,PERIODO_INICIO,PERIODO_FIM,NOTA_FISCAL) " +
                    "VALUES ('" + entity.usuario + "','" + entity.cnpj + "'," +
                "'" + entity.periodo_inicio + "'" + ",'" + entity.periodo_fim + "','" + entity.nota_fiscal + "')";
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