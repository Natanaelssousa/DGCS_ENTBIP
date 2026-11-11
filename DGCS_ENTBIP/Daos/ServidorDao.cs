using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class ServidorDao : Conexao<Servidor, int>
    {

        private string StringConnectionfilial;

        public void ServidorDaoInicializar(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }
        public ServidorDao()
        {
        }


        public override void Atualizar(Servidor entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE SERVIDORES_DGCS SET NOME_SERVIDOR ='" + entity.nome_servidor + "'," +
                    " CODIGO_LOJA = '" + entity.codigo_loja + "', SITUACAO = '" + entity.situacao + "'," +
                    " USUARIO_BANCO = '" + entity.usuario_banco + "', SENHA_BANCO = '" + entity.senha_banco + "'," +
                    " NOME_DB = '" + entity.nome_db + "', NOME_DB_APP = '" + entity.nome_db_app + "',CNPJ = '" + entity.cnpj + "'," +
                    " PARAMETRO_ATIVO = '" + entity.parametro_ativo + "',PARAMETRO_CICLICO = '" + entity.parametro_ciclico + "', ULTIMA_EDICAO = '" + entity.ultima_edicao + "', TOKEN = '" + entity.token + "'" +
                    " WHERE ID_SERVIDORES_DGCS = '" + entity.id_servidores_dgcs+"' ";
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



        public void AtualizarNaLojaParametros(Servidor entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE SERVIDORES_DGCS SET "+
                    " PARAMETRO_ATIVO = '" + entity.parametro_ativo + "',PARAMETRO_CICLICO = '" + entity.parametro_ciclico + "'" +
                    " WHERE  NOME_SERVIDOR ='" + entity.nome_servidor + "' ";
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

        public  void AtualizarNaLoja(Servidor entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE SERVIDORES_DGCS SET NOME_SERVIDOR ='" + entity.nome_servidor + "'," +
                    " CODIGO_LOJA = '" + entity.codigo_loja + "', SITUACAO = '" + entity.situacao + "'," +
                    " USUARIO_BANCO = '" + entity.usuario_banco + "', SENHA_BANCO = '" + entity.senha_banco + "'," +
                    " NOME_DB = '" + entity.nome_db + "', NOME_DB_APP = '" + entity.nome_db_app + "',CNPJ = '" + entity.cnpj + "'," +
                    " PARAMETRO_ATIVO = '" + entity.parametro_ativo + "',PARAMETRO_CICLICO = '" + entity.parametro_ciclico + "', ULTIMA_EDICAO = '" + entity.ultima_edicao + "', TOKEN = '" + entity.token + "'" +
                    " WHERE  NOME_SERVIDOR ='" + entity.nome_servidor + "' ";
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

        public  void AtualizarPorRegiao(string parametro, string regiao, string estado, string parametro_ciclico)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "";
                if (estado != " Todos")
                {
                    sql = "UPDATE SERVIDORES_DGCS SET  PARAMETRO_ATIVO = '" + parametro + "',PARAMETRO_CICLICO = '" + parametro_ciclico + "'  " +
                               " WHERE REGIAO = '" + regiao + "' AND ESTADO = '" + estado + "' ";
                }
                else
                {
                    sql = "UPDATE SERVIDORES_DGCS SET  PARAMETRO_ATIVO = '" + parametro + "',PARAMETRO_CICLICO = '" + parametro_ciclico + "'   " +
                              " WHERE REGIAO = '" + regiao + "' ";
                }
        



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



        public List<Servidor> BuscaServidores(string regiao, string estado)
        {
            string sql = "";
            if (estado != " Todos")
            {
                sql = "SELECT * FROM SERVIDORES_DGCS WHERE REGIAO = '"+ regiao + "' AND ESTADO = '"+ estado + "'"; 
            }
            else
            {
                sql = "SELECT * FROM SERVIDORES_DGCS WHERE REGIAO = '" + regiao + "'";
            }
       


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Servidor> list = new List<Servidor>();
                Servidor servidor = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            servidor = new Servidor();
                            servidor.id_servidores_dgcs = reader["ID_SERVIDORES_DGCS"].ToString();
                            servidor.situacao = reader["SITUACAO"].ToString();
                            servidor.nome_servidor = reader["NOME_SERVIDOR"].ToString();
                            servidor.codigo_loja = reader["CODIGO_LOJA"].ToString();
                            servidor.ultima_edicao = reader["ULTIMA_EDICAO"].ToString();
                            servidor.data_criacao = reader["DATA_CRIACAO"].ToString();
                            servidor.usuario_banco = reader["USUARIO_BANCO"].ToString();
                            servidor.senha_banco = reader["SENHA_BANCO"].ToString();
                            servidor.nome_db = reader["NOME_DB"].ToString();
                            servidor.nome_db_app = reader["NOME_DB_APP"].ToString();
                            servidor.parametro_ativo = reader["PARAMETRO_ATIVO"].ToString();
                            servidor.regiao = reader["REGIAO"].ToString();
                            servidor.estado = reader["ESTADO"].ToString();
                            servidor.cnpj = reader["CNPJ"].ToString();
                            servidor.token = reader["TOKEN"].ToString();
                            servidor.parametro_ciclico = reader["PARAMETRO_CICLICO"].ToString();

                            if (servidor.situacao == "1")
                            {
                                servidor.situacao = "Ativo";
                            }
                            else if (servidor.situacao == "0")
                            {
                                servidor.situacao = "Inativo";
                            }

                            list.Add(servidor);
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


        public  List<Servidor> BuscaRetornoPalavrasChaves(string codigo_loja, string nome_servidor)
        {
            string sql = "SELECT* FROM SERVIDORES_DGCS WHERE (NOME_SERVIDOR LIKE ('"+ nome_servidor + "%') OR CODIGO_LOJA LIKE ('"+ codigo_loja + "%')) ORDER BY DATA_CRIACAO";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Servidor> list = new List<Servidor>();
                Servidor servidor = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            servidor = new Servidor();
                            servidor.id_servidores_dgcs = reader["ID_SERVIDORES_DGCS"].ToString();
                            servidor.situacao = reader["SITUACAO"].ToString();
                            servidor.nome_servidor = reader["NOME_SERVIDOR"].ToString();
                            servidor.codigo_loja = reader["CODIGO_LOJA"].ToString();
                            servidor.ultima_edicao = reader["ULTIMA_EDICAO"].ToString();
                            servidor.data_criacao = reader["DATA_CRIACAO"].ToString();
                            servidor.usuario_banco = reader["USUARIO_BANCO"].ToString();
                            servidor.senha_banco = reader["SENHA_BANCO"].ToString();
                            servidor.nome_db = reader["NOME_DB"].ToString();
                            servidor.nome_db_app = reader["NOME_DB_APP"].ToString();
                            servidor.parametro_ativo = reader["PARAMETRO_ATIVO"].ToString();
                            servidor.regiao = reader["REGIAO"].ToString();
                            servidor.estado = reader["ESTADO"].ToString();
                            servidor.cnpj = reader["CNPJ"].ToString();
                            servidor.token = reader["TOKEN"].ToString();
                            servidor.parametro_ciclico = reader["PARAMETRO_CICLICO"].ToString();

                            if (servidor.situacao == "1")
                            {
                                servidor.situacao = "Ativo";
                            }
                            else if (servidor.situacao == "0")
                            {
                                servidor.situacao = "Inativo";
                            }

                            list.Add(servidor);
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
        public override List<Servidor> BuscaCompletaRetornoLista()
        {
            string sql = "SELECT* FROM SERVIDORES_DGCS ORDER BY DATA_CRIACAO";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Servidor> list = new List<Servidor>();
                Servidor servidor = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            servidor = new Servidor();
                            servidor.id_servidores_dgcs = reader["ID_SERVIDORES_DGCS"].ToString();
                            servidor.situacao = reader["SITUACAO"].ToString();
                            servidor.nome_servidor = reader["NOME_SERVIDOR"].ToString();
                            servidor.codigo_loja = reader["CODIGO_LOJA"].ToString();
                            servidor.ultima_edicao = reader["ULTIMA_EDICAO"].ToString();
                            servidor.data_criacao = reader["DATA_CRIACAO"].ToString();
                            servidor.usuario_banco = reader["USUARIO_BANCO"].ToString();
                            servidor.senha_banco = reader["SENHA_BANCO"].ToString();
                            servidor.nome_db = reader["NOME_DB"].ToString();
                            servidor.nome_db_app = reader["NOME_DB_APP"].ToString();
                            servidor.parametro_ativo = reader["PARAMETRO_ATIVO"].ToString();
                            servidor.regiao = reader["REGIAO"].ToString();
                            servidor.estado = reader["ESTADO"].ToString();
                            servidor.cnpj = reader["CNPJ"].ToString();
                            servidor.token = reader["TOKEN"].ToString();
                            servidor.parametro_ciclico = reader["PARAMETRO_CICLICO"].ToString();

                            if (servidor.situacao == "1")
                            {
                                servidor.situacao = "Ativo";
                            }
                            else if (servidor.situacao == "0")
                            {
                                servidor.situacao = "Inativo";
                            }

                            list.Add(servidor);
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

        public override List<Servidor> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override Servidor BuscaPorDataRetornoObjeto(string data)
        {
            string sql = "SELECT* FROM SERVIDORES_DGCS WHERE  CODIGO_LOJA ='" + data + "'";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Servidor> list = new List<Servidor>();
                Servidor servidor = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            servidor = new Servidor();
                            servidor.id_servidores_dgcs = reader["ID_SERVIDORES_DGCS"].ToString();
                            servidor.situacao = reader["SITUACAO"].ToString();
                            servidor.nome_servidor = reader["NOME_SERVIDOR"].ToString();
                            servidor.codigo_loja = reader["CODIGO_LOJA"].ToString();
                            servidor.ultima_edicao = reader["ULTIMA_EDICAO"].ToString();
                            servidor.data_criacao = reader["DATA_CRIACAO"].ToString();
                            servidor.usuario_banco = reader["USUARIO_BANCO"].ToString();
                            servidor.senha_banco = reader["SENHA_BANCO"].ToString();
                            servidor.nome_db = reader["NOME_DB"].ToString();
                            servidor.nome_db_app = reader["NOME_DB_APP"].ToString();
                            servidor.parametro_ativo = reader["PARAMETRO_ATIVO"].ToString();
                            servidor.regiao = reader["REGIAO"].ToString();
                            servidor.estado = reader["ESTADO"].ToString();
                            servidor.cnpj = reader["CNPJ"].ToString();
                            servidor.token = reader["TOKEN"].ToString();
                            servidor.parametro_ciclico = reader["PARAMETRO_CICLICO"].ToString();


                            if (servidor.situacao == "1")
                            {
                                servidor.situacao = "Ativo";
                            }
                            else if (servidor.situacao == "0")
                            {
                                servidor.situacao = "Inativo";
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
                return servidor;
            }
        }

        public override Servidor BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override void Remover(Servidor entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            using (var conn = new SqlConnection(StringConnection))
            {

                string sql = "DELETE FROM SERVIDORES_DGCS WHERE NOME_SERVIDOR = '"+id+"'";

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


        public  void RemoverServidorLoja(string id, string servidor,string banco,string usuario,string senha)
        {

          string  StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {

                string sql = "DELETE FROM SERVIDORES_DGCS WHERE NOME_SERVIDOR = '" + id + "'";

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

        public override void Salvar(Servidor entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "INSERT INTO SERVIDORES_DGCS(SITUACAO, NOME_SERVIDOR, CODIGO_LOJA, ULTIMA_EDICAO,USUARIO_BANCO,SENHA_BANCO," +
                    "NOME_DB,NOME_DB_APP,PARAMETRO_ATIVO,REGIAO,ESTADO,CNPJ,TOKEN,PARAMETRO_CICLICO) " +
                    "VALUES('1', '"+entity.nome_servidor+"', '"+entity.codigo_loja+"', '"+entity.ultima_edicao + "', '" + entity.usuario_banco + "'," +
                    " '" + entity.senha_banco + "', '" + entity.nome_db + "', '" + entity.nome_db_app + "', '" + entity.parametro_ativo + "'," +
                    " '" + entity.regiao + "', '" + entity.estado + "', '" + entity.cnpj + "', '" + entity.token + "', '" + entity.parametro_ciclico + "')";
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

        public  void SalvarNaLoja(Servidor entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "INSERT INTO SERVIDORES_DGCS(SITUACAO, NOME_SERVIDOR, CODIGO_LOJA, ULTIMA_EDICAO,USUARIO_BANCO,SENHA_BANCO," +
                    "NOME_DB,NOME_DB_APP,PARAMETRO_ATIVO,REGIAO,ESTADO,CNPJ,TOKEN,PARAMETRO_CICLICO) " +
                    "VALUES('1', '" + entity.nome_servidor + "', '" + entity.codigo_loja + "', '" + entity.ultima_edicao + "', '" + entity.usuario_banco + "'," +
                    " '" + entity.senha_banco + "', '" + entity.nome_db + "', '" + entity.nome_db_app + "', '" + entity.parametro_ativo + "'," +
                    " '" + entity.regiao + "', '" + entity.estado + "', '" + entity.cnpj + "', '" + entity.token + "', '" + entity.parametro_ciclico + "')";
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
        public override List<Servidor> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }
    }
}