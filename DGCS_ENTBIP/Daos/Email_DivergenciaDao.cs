using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class Email_DivergenciaDao : Conexao<Email_Divergencia, int>
    {


        public override void Atualizar(Email_Divergencia entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE EMAILS_DIVERGENCIA SET NOME = '"+ entity.nome + "' , EMAIL = '"+entity.email+"' WHERE ID_EMAILS = '" + entity.id_emails + "' ";

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


        public  void Atualizar_Email_Envio(Email_Divergencia entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE EMAIL_DIVERGENCIA_ENVIO SET NOME = '" + entity.nome + "' , EMAIL = '" + entity.email + "'" +
                    " , SENHA = '" + entity.senha + "' , SERVIDOREMAIL = '" + entity.servidor_email + "' , HOSTENVIO = '" + entity.host_envio + "' WHERE ID_EMAILS = '" + entity.id_emails + "' ";

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



        public  List<Email_Divergencia> BuscaEmailGrupo(string grupo)
        {
            string sql = "SELECT  ID_EMAILS,NOME,EMAIL FROM EMAILS_DIVERGENCIA WHERE NOME = '"+ grupo + "' ";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                Email_Divergencia email = null;
                List<Email_Divergencia> list = new List<Email_Divergencia>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            email = new Email_Divergencia();
                            email.id_emails = reader["ID_EMAILS"].ToString();
                            email.nome = reader["NOME"].ToString();
                            email.email = reader["EMAIL"].ToString();

                            list.Add(email);
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
        public override List<Email_Divergencia> BuscaCompletaRetornoLista()
        {
            string sql = "SELECT  ID_EMAILS,NOME,EMAIL FROM EMAILS_DIVERGENCIA ";

            
            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                Email_Divergencia email = null;
                List<Email_Divergencia> list = new List<Email_Divergencia>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            email = new Email_Divergencia();
                            email.id_emails = reader["ID_EMAILS"].ToString();
                            email.nome = reader["NOME"].ToString();
                            email.email = reader["EMAIL"].ToString();

                            list.Add(email);
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


        public  List<Email_Divergencia> BuscaCompletaRetornoListaEmailEnvio()
        {
            string sql = "SELECT  * FROM EMAIL_DIVERGENCIA_ENVIO ";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                Email_Divergencia email = null;
                List<Email_Divergencia> list = new List<Email_Divergencia>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            email = new Email_Divergencia();
                            email.id_emails = reader["ID_EMAILS"].ToString();
                            email.nome = reader["NOME"].ToString();
                            email.email = reader["EMAIL"].ToString();
                            email.senha = reader["SENHA"].ToString();
                            email.servidor_email = reader["SERVIDOREMAIL"].ToString();
                            email.host_envio = reader["HOSTENVIO"].ToString();

                            list.Add(email);
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

        public Email_Divergencia BuscaEmailEnvio()
        {
            string sql = "SELECT  * FROM EMAIL_DIVERGENCIA_ENVIO ";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                Email_Divergencia email = null;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            email = new Email_Divergencia();
                            email.id_emails = reader["ID_EMAILS"].ToString();
                            email.nome = reader["NOME"].ToString();
                            email.email = reader["EMAIL"].ToString();
                            email.senha = reader["SENHA"].ToString();
                            email.servidor_email = reader["SERVIDOREMAIL"].ToString();
                            email.host_envio = reader["HOSTENVIO"].ToString();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return email;
            }
        }
        public override List<Email_Divergencia> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override Email_Divergencia BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override Email_Divergencia BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Email_Divergencia> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(Email_Divergencia entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public void Salvar_email_Envio(Email_Divergencia entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = @"INSERT INTO EMAIL_DIVERGENCIA_ENVIO" +
           "(NOME,EMAIL,SENHA,SERVIDOREMAIL,HOSTENVIO)" +
          " VALUES" +
          " ('" + entity.nome + "', '" + entity.email + "', '" + entity.senha + "', '" + entity.servidor_email + "', '" + entity.host_envio + "')";



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

        public override void Salvar(Email_Divergencia entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = @"INSERT INTO EMAILS_DIVERGENCIA" +
           "(NOME, EMAIL)" +
          " VALUES" +
          " ('" + entity.nome + "', '" + entity.email + "')";



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