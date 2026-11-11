using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class PerfilAcessoDao : Conexao<PerfilAcesso, int>
    {
        public override void Atualizar(PerfilAcesso entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE PERFIS_ACESSO SET TELA_NOTAS_PENDENTES = '" + entity.tela_notas_pendentes + "', TELA_CADASTRO_SERVIDOR = '" + entity.tela_cadastro_servidor + "', " +
                    "TELA_CADASTRO_USUARIO = '" + entity.tela_cadastro_usuario + "', TELA_PARAMETROS = '" + entity.tela_parametros + "', TELA_RELATORIO = '" + entity.tela_relatorio + "'" +
                    ", TELA_EMAILS = '" + entity.tela_emails + "', TELA_PERFIS_ACESSO = '" + entity.tela_perfis_acesso + "'" +
                    ", TELA_RELATORIO_ABASTECIMENTO = '" + entity.tela_relatorio_abastecimento + "'," +
                    " TELA_RELATORIO_LOG = '" + entity.tela_relatorio_log + "', TELA_PEDIDO_CONSUMIVEIS = '" + entity.tela_pedidos_consumiveis + "'" +
                    ", TELA_INVENTARIO_CICLICO = '" + entity.tela_inventario_ciclico + "', TELA_PARAMETRO_FTP = '" + entity.tela_parametro_ftp + "'" +
                    ", TELA_REGISTRO_AMOSTRA = '" + entity.tela_registro_amostra + "', TELA_ESCALA = '" + entity.tela_escala + "'" +
                    " WHERE ID_PERFIL = '" + entity.id_perfil + "'";
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



        public override List<PerfilAcesso> BuscaCompletaRetornoLista()
        {
            string sql = "SELECT ID_PERFIL,PERFIL,TELA_NOTAS_PENDENTES,TELA_CADASTRO_SERVIDOR," +
                "TELA_CADASTRO_USUARIO,TELA_PARAMETROS,TELA_RELATORIO,TELA_EMAILS,TELA_PERFIS_ACESSO, " +
                "TELA_RELATORIO_ABASTECIMENTO,TELA_RELATORIO_LOG,TELA_PEDIDO_CONSUMIVEIS," +
                "TELA_INVENTARIO_CICLICO,TELA_PARAMETRO_FTP,TELA_REGISTRO_AMOSTRA,TELA_ESCALA FROM PERFIS_ACESSO";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<PerfilAcesso> list = new List<PerfilAcesso>();
                PerfilAcesso perfil = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            perfil = new PerfilAcesso();
                            perfil.id_perfil = reader["ID_PERFIL"].ToString();
                            perfil.perfil = reader["PERFIL"].ToString();
                            perfil.tela_notas_pendentes = reader["TELA_NOTAS_PENDENTES"].ToString();
                            perfil.tela_cadastro_servidor = reader["TELA_CADASTRO_SERVIDOR"].ToString();
                            perfil.tela_cadastro_usuario = reader["TELA_CADASTRO_USUARIO"].ToString();
                            perfil.tela_parametros = reader["TELA_PARAMETROS"].ToString();
                            perfil.tela_relatorio = reader["TELA_RELATORIO"].ToString();
                            perfil.tela_emails = reader["TELA_EMAILS"].ToString();
                            perfil.tela_perfis_acesso = reader["TELA_PERFIS_ACESSO"].ToString();
                            perfil.tela_relatorio_abastecimento = reader["TELA_RELATORIO_ABASTECIMENTO"].ToString();
                            perfil.tela_relatorio_log = reader["TELA_RELATORIO_LOG"].ToString();
                            perfil.tela_pedidos_consumiveis = reader["TELA_PEDIDO_CONSUMIVEIS"].ToString();
                            perfil.tela_inventario_ciclico = reader["TELA_INVENTARIO_CICLICO"].ToString();
                            perfil.tela_parametro_ftp = reader["TELA_PARAMETRO_FTP"].ToString();
                            perfil.tela_registro_amostra = reader["TELA_REGISTRO_AMOSTRA"].ToString();
                            perfil.tela_escala = reader["TELA_ESCALA"].ToString();

                            list.Add(perfil);
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

        public override List<PerfilAcesso> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override PerfilAcesso BuscaPorDataRetornoObjeto(string data)
        {
            string sql = "SELECT ID_PERFIL,PERFIL,TELA_NOTAS_PENDENTES,TELA_CADASTRO_SERVIDOR," +
                   "TELA_CADASTRO_USUARIO,TELA_PARAMETROS,TELA_RELATORIO,TELA_EMAILS,TELA_PERFIS_ACESSO, " +
                   "TELA_RELATORIO_ABASTECIMENTO,TELA_RELATORIO_LOG,TELA_PEDIDO_CONSUMIVEIS,TELA_INVENTARIO_CICLICO," +
                   "TELA_PARAMETRO_FTP,TELA_REGISTRO_AMOSTRA,TELA_ESCALA FROM PERFIS_ACESSO WHERE " +
             " PERFIL = '" + data + "'";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                PerfilAcesso perfil = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            perfil = new PerfilAcesso();
                            perfil.id_perfil = reader["ID_PERFIL"].ToString();
                            perfil.perfil = reader["PERFIL"].ToString();
                            perfil.tela_notas_pendentes = reader["TELA_NOTAS_PENDENTES"].ToString();
                            perfil.tela_cadastro_servidor = reader["TELA_CADASTRO_SERVIDOR"].ToString();
                            perfil.tela_cadastro_usuario = reader["TELA_CADASTRO_USUARIO"].ToString();
                            perfil.tela_parametros = reader["TELA_PARAMETROS"].ToString();
                            perfil.tela_relatorio = reader["TELA_RELATORIO"].ToString();
                            perfil.tela_emails = reader["TELA_EMAILS"].ToString();
                            perfil.tela_perfis_acesso = reader["TELA_PERFIS_ACESSO"].ToString();
                            perfil.tela_relatorio_abastecimento = reader["TELA_RELATORIO_ABASTECIMENTO"].ToString();
                            perfil.tela_relatorio_log = reader["TELA_RELATORIO_LOG"].ToString();
                            perfil.tela_pedidos_consumiveis = reader["TELA_PEDIDO_CONSUMIVEIS"].ToString();
                            perfil.tela_inventario_ciclico = reader["TELA_INVENTARIO_CICLICO"].ToString();
                            perfil.tela_parametro_ftp = reader["TELA_PARAMETRO_FTP"].ToString();
                            perfil.tela_registro_amostra = reader["TELA_REGISTRO_AMOSTRA"].ToString();
                            perfil.tela_escala = reader["TELA_ESCALA"].ToString();

                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return perfil;
            }
        }

        public override PerfilAcesso BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<PerfilAcesso> BuscaRetornoPalavrasChaves(string palavra)
        {
            string sql = "SELECT ID_PERFIL,PERFIL,TELA_NOTAS_PENDENTES,TELA_CADASTRO_SERVIDOR," +
                 "TELA_CADASTRO_USUARIO,TELA_PARAMETROS,TELA_RELATORIO,TELA_EMAILS,TELA_PERFIS_ACESSO," +
                 "TELA_RELATORIO_ABASTECIMENTO,TELA_RELATORIO_LOG,TELA_PEDIDO_CONSUMIVEIS,TELA_INVENTARIO_CICLICO," +
                 "TELA_PARAMETRO_FTP,TELA_REGISTRO_AMOSTRA,TELA_ESCALA FROM PERFIS_ACESSO WHERE " +
           " PERFIL LIKE('" + palavra + "%') ";


            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<PerfilAcesso> list = new List<PerfilAcesso>();
                PerfilAcesso perfil = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            perfil = new PerfilAcesso();
                            perfil.id_perfil = reader["ID_PERFIL"].ToString();
                            perfil.perfil = reader["PERFIL"].ToString();
                            perfil.tela_notas_pendentes = reader["TELA_NOTAS_PENDENTES"].ToString();
                            perfil.tela_cadastro_servidor = reader["TELA_CADASTRO_SERVIDOR"].ToString();
                            perfil.tela_cadastro_usuario = reader["TELA_CADASTRO_USUARIO"].ToString();
                            perfil.tela_parametros = reader["TELA_PARAMETROS"].ToString();
                            perfil.tela_relatorio = reader["TELA_RELATORIO"].ToString();
                            perfil.tela_emails = reader["TELA_EMAILS"].ToString();
                            perfil.tela_perfis_acesso = reader["TELA_PERFIS_ACESSO"].ToString();
                            perfil.tela_relatorio_abastecimento = reader["TELA_RELATORIO_ABASTECIMENTO"].ToString();
                            perfil.tela_relatorio_log = reader["TELA_RELATORIO_LOG"].ToString();
                            perfil.tela_pedidos_consumiveis = reader["TELA_PEDIDO_CONSUMIVEIS"].ToString();
                            perfil.tela_inventario_ciclico = reader["TELA_INVENTARIO_CICLICO"].ToString();
                            perfil.tela_parametro_ftp = reader["TELA_PARAMETRO_FTP"].ToString();
                            perfil.tela_registro_amostra = reader["TELA_REGISTRO_AMOSTRA"].ToString();
                            perfil.tela_escala = reader["TELA_ESCALA"].ToString();

                            list.Add(perfil);
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

        public override void Remover(PerfilAcesso entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(PerfilAcesso entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "INSERT INTO PERFIS_ACESSO(PERFIL, TELA_NOTAS_PENDENTES, TELA_CADASTRO_SERVIDOR, TELA_CADASTRO_USUARIO, " +
                "TELA_PARAMETROS, TELA_RELATORIO, TELA_EMAILS, TELA_PERFIS_ACESSO,TELA_RELATORIO_ABASTECIMENTO,TELA_RELATORIO_LOG," +
                "TELA_PEDIDO_CONSUMIVEIS,TELA_INVENTARIO_CICLICO,TELA_PARAMETRO_FTP,TELA_REGISTRO_AMOSTRA,TELA_ESCALA) VALUES ('" + entity.perfil + "','" + entity.tela_notas_pendentes + "'," +
                "'" + entity.tela_cadastro_servidor + "'" +",'" + entity.tela_cadastro_usuario + "','" + entity.tela_parametros + "','" + 
                entity.tela_relatorio + "','" + entity.tela_emails + "','" + entity.tela_perfis_acesso + "','" + entity.tela_relatorio_abastecimento + "'" +
                ",'" + entity.tela_relatorio_log + "','" + entity.tela_pedidos_consumiveis + "','" + entity.tela_inventario_ciclico + "'" +
                ",'" + entity.tela_parametro_ftp + "','" + entity.tela_registro_amostra + "','" + entity.tela_escala + "')";
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