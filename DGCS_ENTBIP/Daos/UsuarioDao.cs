using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class UsuarioDao : Conexao<Usuario, int>
    {
        public override void Atualizar(Usuario entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE USUARIO_DGCS SET NOME_COMPLETO = '"+entity.nome_completo+"', CPF = '"+entity.cpf+"', " +
                    "EMAIL = '"+entity.email+"', PERFIL_ACESSO = '"+entity.perfil_acesso+"', LOGIN_USUARIO = '"+entity.login_usuario+"'" +
                    ", SENHA = '"+entity.senha+"', SITUACAO = '"+entity.situacao+"', CODIGO_LOJA = '"+entity.codigo_loja+"'" +
                    " WHERE ID_USUARIO_DGCS = '"+entity.id_usuario_dgcs+"'";
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



        public void ResetDeSenhaCPF (Usuario entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE USUARIO_DGCS SET  SENHA = '" + entity.senha + "'" +
                    " WHERE CPF = '" + entity.cpf + "'";
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
        public  void ResetDeSenha (Usuario entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE USUARIO_DGCS SET  SENHA = '"+entity.senha+"'" +
                    " WHERE ID_USUARIO_DGCS = '" + entity.id_usuario_dgcs + "'";
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

        public List<Usuario> BuscaRetornoUsuariosPadrao(string codigo_loja, string nome)
        {
            string sql = "SELECT ID_USUARIO_DGCS, NOME_COMPLETO, CPF, EMAIL, PERFIL_ACESSO, LOGIN_USUARIO," +
                " SENHA, SITUACAO, DATA_CRIACAO, CODIGO_LOJA FROM USUARIO_DGCS WHERE" +
                " NOME_COMPLETO = '" + nome + "' and CODIGO_LOJA = '" + codigo_loja + "' ORDER BY DATA_CRIACAO";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Usuario> list = new List<Usuario>();
                Usuario usuario = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            usuario = new Usuario();
                            usuario.id_usuario_dgcs = reader["ID_USUARIO_DGCS"].ToString();
                            usuario.situacao = reader["SITUACAO"].ToString();
                            usuario.nome_completo = reader["NOME_COMPLETO"].ToString();
                            usuario.codigo_loja = reader["CODIGO_LOJA"].ToString();
                            usuario.cpf = reader["CPF"].ToString();
                            usuario.data_criacao = reader["DATA_CRIACAO"].ToString();
                            usuario.email = reader["EMAIL"].ToString();
                            usuario.perfil_acesso = reader["PERFIL_ACESSO"].ToString();
                            usuario.login_usuario = reader["LOGIN_USUARIO"].ToString();
                            usuario.senha = reader["SENHA"].ToString();



                            if (usuario.situacao == "1")
                            {
                                usuario.situacao = "Ativo";
                            }
                            else if (usuario.situacao == "0")
                            {
                                usuario.situacao = "Inativo";
                            }

                            list.Add(usuario);
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
        public List<Usuario> BuscaRetornoPalavrasChaves(string codigo_loja, string nome)
        {
            string sql = "SELECT ID_USUARIO_DGCS, NOME_COMPLETO, CPF, EMAIL, PERFIL_ACESSO, LOGIN_USUARIO," +
                " SENHA, SITUACAO, DATA_CRIACAO, CODIGO_LOJA FROM USUARIO_DGCS WHERE" +
                " NOME_COMPLETO LIKE('" + nome + "%') OR CODIGO_LOJA LIKE('" + codigo_loja + "%') ORDER BY DATA_CRIACAO";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Usuario> list = new List<Usuario>();
                Usuario usuario = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            usuario = new Usuario();
                            usuario.id_usuario_dgcs = reader["ID_USUARIO_DGCS"].ToString();
                            usuario.situacao = reader["SITUACAO"].ToString();
                            usuario.nome_completo = reader["NOME_COMPLETO"].ToString();
                            usuario.codigo_loja = reader["CODIGO_LOJA"].ToString();
                            usuario.cpf = reader["CPF"].ToString();
                            usuario.data_criacao = reader["DATA_CRIACAO"].ToString();
                            usuario.email = reader["EMAIL"].ToString();
                            usuario.perfil_acesso = reader["PERFIL_ACESSO"].ToString();
                            usuario.login_usuario = reader["LOGIN_USUARIO"].ToString();
                            usuario.senha = reader["SENHA"].ToString();



                            if (usuario.situacao == "1")
                            {
                                usuario.situacao = "Ativo";
                            }
                            else if (usuario.situacao == "0")
                            {
                                usuario.situacao = "Inativo";
                            }

                            list.Add(usuario);
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

        public  Usuario BuscaUsuarioLogin(string login, string senha)
        {
            string sql = "SELECT ID_USUARIO_DGCS,NOME_COMPLETO,CPF,EMAIL,PERFIL_ACESSO,LOGIN_USUARIO,SENHA," +
                "SITUACAO,DATA_CRIACAO,CODIGO_LOJA FROM USUARIO_DGCS WHERE LOGIN_USUARIO='"+login+"' AND SENHA='"+senha+"' ORDER BY DATA_CRIACAO";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                Usuario usuario = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            usuario = new Usuario();
                            usuario.id_usuario_dgcs = reader["ID_USUARIO_DGCS"].ToString();
                            usuario.situacao = reader["SITUACAO"].ToString();
                            usuario.nome_completo = reader["NOME_COMPLETO"].ToString();
                            usuario.codigo_loja = reader["CODIGO_LOJA"].ToString();
                            usuario.cpf = reader["CPF"].ToString();
                            usuario.data_criacao = reader["DATA_CRIACAO"].ToString();
                            usuario.email = reader["EMAIL"].ToString();
                            usuario.perfil_acesso = reader["PERFIL_ACESSO"].ToString();
                            usuario.login_usuario = reader["LOGIN_USUARIO"].ToString();
                            usuario.senha = reader["SENHA"].ToString();



                            if (usuario.situacao == "1")
                            {
                                usuario.situacao = "Ativo";
                            }
                            else if (usuario.situacao == "0")
                            {
                                usuario.situacao = "Inativo";
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
                return usuario;
            }
        }


        public Usuario BuscaUsuarioCPF(string cpf,string email)
        {
            string sql = "SELECT ID_USUARIO_DGCS,NOME_COMPLETO,CPF,EMAIL,PERFIL_ACESSO,LOGIN_USUARIO,SENHA," +
                "SITUACAO,DATA_CRIACAO,CODIGO_LOJA FROM USUARIO_DGCS WHERE CPF='" + cpf + "' and  EMAIL = '"+ email + "' ORDER BY DATA_CRIACAO";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                Usuario usuario = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            usuario = new Usuario();
                            usuario.id_usuario_dgcs = reader["ID_USUARIO_DGCS"].ToString();
                            usuario.situacao = reader["SITUACAO"].ToString();
                            usuario.nome_completo = reader["NOME_COMPLETO"].ToString();
                            usuario.codigo_loja = reader["CODIGO_LOJA"].ToString();
                            usuario.cpf = reader["CPF"].ToString();
                            usuario.data_criacao = reader["DATA_CRIACAO"].ToString();
                            usuario.email = reader["EMAIL"].ToString();
                            usuario.perfil_acesso = reader["PERFIL_ACESSO"].ToString();
                            usuario.login_usuario = reader["LOGIN_USUARIO"].ToString();
                            usuario.senha = reader["SENHA"].ToString();



                            if (usuario.situacao == "1")
                            {
                                usuario.situacao = "Ativo";
                            }
                            else if (usuario.situacao == "0")
                            {
                                usuario.situacao = "Inativo";
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
                return usuario;
            }
        }

        public  List<Usuario> BuscaCompletaRetornoListaCodigoLoja(string codigo )
        {
            string sql = "SELECT ID_USUARIO_DGCS,NOME_COMPLETO,CPF,EMAIL,PERFIL_ACESSO,LOGIN_USUARIO,SENHA," +
                "SITUACAO,DATA_CRIACAO,CODIGO_LOJA FROM USUARIO_DGCS where CODIGO_LOJA = '"+ codigo + "' and   PERFIL_ACESSO IN ('Operador','Gerente Loja') ORDER BY DATA_CRIACAO";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Usuario> list = new List<Usuario>();
                Usuario usuario = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            usuario = new Usuario();
                            usuario.id_usuario_dgcs = reader["ID_USUARIO_DGCS"].ToString();
                            usuario.situacao = reader["SITUACAO"].ToString();
                            usuario.nome_completo = reader["NOME_COMPLETO"].ToString();
                            usuario.codigo_loja = reader["CODIGO_LOJA"].ToString();
                            usuario.cpf = reader["CPF"].ToString();
                            usuario.data_criacao = reader["DATA_CRIACAO"].ToString();
                            usuario.email = reader["EMAIL"].ToString();
                            usuario.perfil_acesso = reader["PERFIL_ACESSO"].ToString();
                            usuario.login_usuario = reader["LOGIN_USUARIO"].ToString();
                            usuario.senha = reader["SENHA"].ToString();



                            if (usuario.situacao == "1")
                            {
                                usuario.situacao = "Ativo";
                            }
                            else if (usuario.situacao == "0")
                            {
                                usuario.situacao = "Inativo";
                            }

                            list.Add(usuario);
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
        public override List<Usuario> BuscaCompletaRetornoLista()
        {
            string sql = "SELECT ID_USUARIO_DGCS,NOME_COMPLETO,CPF,EMAIL,PERFIL_ACESSO,LOGIN_USUARIO,SENHA," +
                "SITUACAO,DATA_CRIACAO,CODIGO_LOJA FROM USUARIO_DGCS ORDER BY DATA_CRIACAO";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<Usuario> list = new List<Usuario>();
                Usuario usuario = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            usuario = new Usuario();
                            usuario.id_usuario_dgcs = reader["ID_USUARIO_DGCS"].ToString();
                            usuario.situacao = reader["SITUACAO"].ToString();
                            usuario.nome_completo = reader["NOME_COMPLETO"].ToString();
                            usuario.codigo_loja = reader["CODIGO_LOJA"].ToString();
                            usuario.cpf = reader["CPF"].ToString();
                            usuario.data_criacao = reader["DATA_CRIACAO"].ToString();
                            usuario.email = reader["EMAIL"].ToString();
                            usuario.perfil_acesso = reader["PERFIL_ACESSO"].ToString();
                            usuario.login_usuario = reader["LOGIN_USUARIO"].ToString();
                            usuario.senha = reader["SENHA"].ToString();



                            if (usuario.situacao == "1")
                            {
                                usuario.situacao = "Ativo";
                            }
                            else if (usuario.situacao == "0")
                            {
                                usuario.situacao = "Inativo";
                            }

                            list.Add(usuario);
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

        public override List<Usuario> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override Usuario BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override Usuario BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override void Remover(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(Usuario entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "INSERT INTO USUARIO_DGCS (NOME_COMPLETO,CPF,EMAIL,PERFIL_ACESSO,LOGIN_USUARIO," +
                    "SENHA,SITUACAO,CODIGO_LOJA) VALUES ('"+entity.nome_completo+"','"+entity.cpf+"','"+entity.email+"'" +
                    ",'"+entity.perfil_acesso+"','"+entity.login_usuario+"','"+entity.senha+"','"+entity.situacao+"','"+entity.codigo_loja+"')";
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

        public override List<Usuario> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }
    }
}