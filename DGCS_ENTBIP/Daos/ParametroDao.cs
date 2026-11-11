using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class ParametroDao : Conexao<Parametro, int>
    {
        public override void Atualizar(Parametro entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE PARAMETROS_DGCS SET DATA_INICIO ='" + entity.data_inicio + "'," +
                    " DATA_FIM ='" + entity.data_fim + "', STATUS ='" + entity.status + "',VALOR ='" + entity.valor + "'," +
                    " RESPONSAVEL_ALTERACAO = '" + entity.responsavel_alteracao + "', DATA_ALTERACAO = GETDATE() " +
                    " WHERE ID_PARAMETRO_DGCS = '" + entity.id_parametro_dgcs + "' ";
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

        public  Parametro BuscaParametro(string nome_parametro)
        {
            string sql = "SELECT* FROM PARAMETROS_DGCS WHERE NOME_PARAMETRO = '"+ nome_parametro + "' ";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Parametro> list = new List<Parametro>();
                Parametro parametro = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            parametro = new Parametro();
                            parametro.id_parametro_dgcs = reader["ID_PARAMETRO_DGCS"].ToString();
                            parametro.nome_parametro = reader["NOME_PARAMETRO"].ToString();
                            parametro.descricao = reader["DESCRICAO"].ToString();
                            parametro.status = reader["STATUS"].ToString();
                            parametro.responsavel_alteracao = reader["RESPONSAVEL_ALTERACAO"].ToString();
                            parametro.data_alteracao = reader["DATA_ALTERACAO"].ToString();
                            parametro.data_criacao = reader["DATA_CRIACAO"].ToString();
                            parametro.data_inicio = reader["DATA_INICIO"].ToString();
                            parametro.data_fim = reader["DATA_FIM"].ToString();
                            parametro.valor = reader["VALOR"].ToString();


                            if (parametro.status == "1")
                            {
                                parametro.status = "Ativo";
                            }
                            else if (parametro.status == "0")
                            {
                                parametro.status = "Inativo";
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
                return parametro;
            }
        }


        public override List<Parametro> BuscaRetornoPalavrasChaves(string nome_parametro)
        {
            string sql = "SELECT* FROM PARAMETROS_DGCS WHERE (NOME_PARAMETRO LIKE ('" + nome_parametro + "%')  or NOME_PARAMETRO LIKE ('%" + nome_parametro + "')) ORDER BY DATA_CRIACAO";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Parametro> list = new List<Parametro>();
                Parametro parametro = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            parametro = new Parametro();
                            parametro.id_parametro_dgcs = reader["ID_PARAMETRO_DGCS"].ToString();
                            parametro.nome_parametro = reader["NOME_PARAMETRO"].ToString();
                            parametro.descricao = reader["DESCRICAO"].ToString();
                            parametro.status = reader["STATUS"].ToString();
                            parametro.responsavel_alteracao = reader["RESPONSAVEL_ALTERACAO"].ToString();
                            parametro.data_alteracao = reader["DATA_ALTERACAO"].ToString();
                            parametro.data_criacao = reader["DATA_CRIACAO"].ToString();
                            parametro.data_inicio = reader["DATA_INICIO"].ToString();
                            parametro.data_fim = reader["DATA_FIM"].ToString();
                            parametro.valor = reader["VALOR"].ToString();


                            if (parametro.status == "1")
                            {
                                parametro.status = "Ativo";
                            }
                            else if (parametro.status == "0")
                            {
                                parametro.status = "Inativo";
                            }

                            list.Add(parametro);
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

        public override List<Parametro> BuscaCompletaRetornoLista()
        {
            string sql = "SELECT * FROM PARAMETROS_DGCS  ORDER BY DATA_CRIACAO";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Parametro> list = new List<Parametro>();
                Parametro parametro = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            parametro = new Parametro();
                            parametro.id_parametro_dgcs = reader["ID_PARAMETRO_DGCS"].ToString();
                            parametro.nome_parametro = reader["NOME_PARAMETRO"].ToString();
                            parametro.descricao = reader["DESCRICAO"].ToString();
                            parametro.status = reader["STATUS"].ToString();
                            parametro.responsavel_alteracao = reader["RESPONSAVEL_ALTERACAO"].ToString();
                            parametro.data_alteracao = reader["DATA_ALTERACAO"].ToString();
                            parametro.data_criacao = reader["DATA_CRIACAO"].ToString();
                            parametro.data_inicio = reader["DATA_INICIO"].ToString();
                            parametro.data_fim = reader["DATA_FIM"].ToString();
                            parametro.valor = reader["VALOR"].ToString();



                            if (parametro.status == "1")
                            {
                                parametro.status = "Ativo";
                            }
                            else if (parametro.status == "0")
                            {
                                parametro.status = "Inativo";
                            }

                            list.Add(parametro);
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

        public override List<Parametro> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override Parametro BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override Parametro BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override void Remover(Parametro entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(Parametro entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "INSERT INTO PARAMETROS_DGCS( NOME_PARAMETRO, DESCRICAO, STATUS,RESPONSAVEL_ALTERACAO,DATA_ALTERACAO,DATA_INICIO,DATA_FIM,VALOR) " +
                    "VALUES('" + entity.nome_parametro + "', '" + entity.descricao + "', '" + entity.status + "', '" + entity.responsavel_alteracao + "', '" + entity.data_alteracao + "', '" + entity.data_inicio + "', '" + entity.data_fim + "', '" + entity.valor + "')";
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