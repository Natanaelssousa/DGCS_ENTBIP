using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class ParametroFtpDao : Conexao<ParametrosFtp, int>
    {

        private string StringConnectionfilial;


        public ParametroFtpDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }


        public override void Atualizar(ParametrosFtp entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE PARAMETRO_FTP SET SERVIDOR = '" + entity.servidor + "', PASTA = '"+ entity.pasta + "' " +
                    ", USUARIO = '" + entity.usuario + "', SENHA = '" + entity.senha + "', STATUS = '" + entity.status + "'";
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

        public override List<ParametrosFtp> BuscaCompletaRetornoLista()
        {
            throw new NotImplementedException();
        }

        public ParametrosFtp BuscaFTPcadastrado()
        {
            string sql = "SELECT SERVIDOR,PASTA,USUARIO,SENHA,STATUS FROM PARAMETRO_FTP";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                ParametrosFtp ftp = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            ftp = new ParametrosFtp();
                            ftp.servidor = reader["SERVIDOR"].ToString();
                            ftp.pasta = reader["PASTA"].ToString();
                            ftp.usuario = reader["USUARIO"].ToString();
                            ftp.senha = reader["SENHA"].ToString();
                            ftp.status = reader["STATUS"].ToString();

                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return ftp;
            }
        }

        public override List<ParametrosFtp> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override ParametrosFtp BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override ParametrosFtp BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<ParametrosFtp> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(ParametrosFtp entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(ParametrosFtp entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "INSERT INTO PARAMETRO_FTP(SERVIDOR,PASTA,USUARIO,SENHA,STATUS) VALUES ('" + entity.servidor + "','" + entity.pasta + "'," +
                "'" + entity.usuario + "'" + ",'" + entity.senha + "','" + entity.status + "')";
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