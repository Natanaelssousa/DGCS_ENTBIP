using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class RegistroAmostrasDao : Conexao<RegistroAmostras, int>
    {
        private string StringConnectionfilial;


        public RegistroAmostrasDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }
        public override void Atualizar(RegistroAmostras entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE REGISTRO_AMOSTRAS SET QTDE ='" + entity.qtde + "', DATA_CADASTRO =GETDATE() " +
                    " WHERE RESPONSAVEL = '" + entity.responsavel + "' AND CPF_CLIENTE = '" + entity.cpf_cliente + "'" +
                    " AND PRODUTO = '" + entity.produto + "'";
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

        public void AtualizarStatus(RegistroAmostras entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "UPDATE REGISTRO_AMOSTRAS SET FINALIZADO ='1', DATA_CADASTRO =GETDATE() " +
                    " WHERE RESPONSAVEL = '" + entity.responsavel + "' " +
                    " AND FINALIZADO = '0'";
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

        public RegistroAmostras ConsultaRegistroItem(string responsavel, string prod,string cpf)
        {
            string sql = "select COD_FILIAL,ID_REGISTROS, DATA_CADASTRO, CPF_CLIENTE, PRODUTO, QTDE, RESPONSAVEL," +
                "FINALIZADO from REGISTRO_AMOSTRAS where RESPONSAVEL = '" + responsavel + "' and FINALIZADO = '0' and PRODUTO = '" + prod + "' and CPF_CLIENTE = '"+ cpf + "' ";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                RegistroAmostras produto = null;
                RegistroAmostras produtoVazio = new RegistroAmostras();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new RegistroAmostras();
                            produto.id_registros = reader["ID_REGISTROS"].ToString();
                            produto.data_cadastro = reader["DATA_CADASTRO"].ToString();
                            produto.cpf_cliente = reader["CPF_CLIENTE"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.qtde = reader["QTDE"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.finalizado = reader["FINALIZADO"].ToString();
                            produto.cod_filial = reader["COD_FILIAL"].ToString();

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
        public List<RegistroAmostras> ConsultaRegistroEmAberto(string responsavel, string nome_server)
        {
            string sql = "select COD_FILIAL,ID_REGISTROS, DATA_CADASTRO, CPF_CLIENTE, PRODUTO, QTDE, RESPONSAVEL," +
                "FINALIZADO from REGISTRO_AMOSTRAS where RESPONSAVEL = '" + responsavel + "' and FINALIZADO = '0' ";

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<RegistroAmostras> list = new List<RegistroAmostras>();
                RegistroAmostras produto = null;
                RegistroAmostras produtoVazio = new RegistroAmostras();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new RegistroAmostras();
                            produto.id_registros = reader["ID_REGISTROS"].ToString();
                            produto.data_cadastro = reader["DATA_CADASTRO"].ToString();
                            produto.cpf_cliente = reader["CPF_CLIENTE"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.qtde = reader["QTDE"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.finalizado = reader["FINALIZADO"].ToString();
                            produto.cod_filial = reader["COD_FILIAL"].ToString();
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

        public override List<RegistroAmostras> BuscaCompletaRetornoLista()
        {
            string sql = "select COD_FILIAL,ID_REGISTROS, DATA_CADASTRO, CPF_CLIENTE, PRODUTO, QTDE, RESPONSAVEL," +
                "FINALIZADO from REGISTRO_AMOSTRAS where CONVERT(DATE,DATA_CADASTRO) >= CONVERT(DATE,GETDATE()) and FINALIZADO = '1' ";

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<RegistroAmostras> list = new List<RegistroAmostras>();
                RegistroAmostras produto = null;
                RegistroAmostras produtoVazio = new RegistroAmostras();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new RegistroAmostras();
                            produto.id_registros = reader["ID_REGISTROS"].ToString();
                            produto.data_cadastro = reader["DATA_CADASTRO"].ToString();
                            produto.cpf_cliente = reader["CPF_CLIENTE"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.qtde = reader["QTDE"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.finalizado = reader["FINALIZADO"].ToString();
                            produto.cod_filial = reader["COD_FILIAL"].ToString();

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
        public  List<RegistroAmostras> BuscaRetornoListaNFsPorData(string dataInicio, string dataFim)
        {
            string sql = "select COD_FILIAL,ID_REGISTROS, DATA_CADASTRO, CPF_CLIENTE, PRODUTO, QTDE, RESPONSAVEL," +
                "FINALIZADO from REGISTRO_AMOSTRAS where " +
                "CONVERT(DATE,DATA_CADASTRO) >= '"+ dataInicio + "' and CONVERT(DATE,DATA_CADASTRO) <= '" + dataFim + "' and FINALIZADO = '1' ";

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<RegistroAmostras> list = new List<RegistroAmostras>();
                RegistroAmostras produto = null;
                RegistroAmostras produtoVazio = new RegistroAmostras();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new RegistroAmostras();
                            produto.id_registros = reader["ID_REGISTROS"].ToString();
                            produto.data_cadastro = reader["DATA_CADASTRO"].ToString();
                            produto.cpf_cliente = reader["CPF_CLIENTE"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.qtde = reader["QTDE"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.finalizado = reader["FINALIZADO"].ToString();
                            produto.cod_filial = reader["COD_FILIAL"].ToString();

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

        public List<RegistroAmostras> BuscaRetornoListaNFsPorProduto(string prod)
        {
            string sql = "select COD_FILIAL,ID_REGISTROS, DATA_CADASTRO, CPF_CLIENTE, PRODUTO, QTDE, RESPONSAVEL," +
                "FINALIZADO from REGISTRO_AMOSTRAS where  PRODUTO = '" + prod + "' and FINALIZADO = '1' ";

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<RegistroAmostras> list = new List<RegistroAmostras>();
                RegistroAmostras produto = null;
                RegistroAmostras produtoVazio = new RegistroAmostras();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new RegistroAmostras();
                            produto.id_registros = reader["ID_REGISTROS"].ToString();
                            produto.data_cadastro = reader["DATA_CADASTRO"].ToString();
                            produto.cpf_cliente = reader["CPF_CLIENTE"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.qtde = reader["QTDE"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.finalizado = reader["FINALIZADO"].ToString();
                            produto.cod_filial = reader["COD_FILIAL"].ToString();

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
        public List<RegistroAmostras> BuscaRetornoListaNFsPorCpf( string cpf)
        {
            string sql = "select COD_FILIAL,ID_REGISTROS, DATA_CADASTRO, CPF_CLIENTE, PRODUTO, QTDE, RESPONSAVEL," +
                "FINALIZADO from REGISTRO_AMOSTRAS where  CPF_CLIENTE = '" + cpf + "' and FINALIZADO = '1' ";

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<RegistroAmostras> list = new List<RegistroAmostras>();
                RegistroAmostras produto = null;
                RegistroAmostras produtoVazio = new RegistroAmostras();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new RegistroAmostras();
                            produto.id_registros = reader["ID_REGISTROS"].ToString();
                            produto.data_cadastro = reader["DATA_CADASTRO"].ToString();
                            produto.cpf_cliente = reader["CPF_CLIENTE"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.qtde = reader["QTDE"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.finalizado = reader["FINALIZADO"].ToString();
                            produto.cod_filial = reader["COD_FILIAL"].ToString();

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

        public List<RegistroAmostras> BuscaRetornoListaNFsPorDataCpf(string dataInicio, string dataFim, string cpf)
        {
            string sql = "select COD_FILIAL,ID_REGISTROS, DATA_CADASTRO, CPF_CLIENTE, PRODUTO, QTDE, RESPONSAVEL," +
                "FINALIZADO from REGISTRO_AMOSTRAS where " +
                "CONVERT(DATE,DATA_CADASTRO) >= '" + dataInicio + "' and CONVERT(DATE,DATA_CADASTRO) <= '" + dataFim + "' " +
                "and CPF_CLIENTE <= '" + cpf + "' and FINALIZADO = '1' ";

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<RegistroAmostras> list = new List<RegistroAmostras>();
                RegistroAmostras produto = null;
                RegistroAmostras produtoVazio = new RegistroAmostras();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new RegistroAmostras();
                            produto.id_registros = reader["ID_REGISTROS"].ToString();
                            produto.data_cadastro = reader["DATA_CADASTRO"].ToString();
                            produto.cpf_cliente = reader["CPF_CLIENTE"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.qtde = reader["QTDE"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.finalizado = reader["FINALIZADO"].ToString();
                            produto.cod_filial = reader["COD_FILIAL"].ToString();

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

        public List<RegistroAmostras> BuscaRetornoListaNFsPorDataProduto(string dataInicio, string dataFim, string prod)
        {
            string sql = "select COD_FILIAL,ID_REGISTROS, DATA_CADASTRO, CPF_CLIENTE, PRODUTO, QTDE, RESPONSAVEL," +
                "FINALIZADO from REGISTRO_AMOSTRAS where " +
                "CONVERT(DATE,DATA_CADASTRO) >= '" + dataInicio + "' and CONVERT(DATE,DATA_CADASTRO) <= '" + dataFim + "' " +
                "and PRODUTO <= '" + prod + "' and FINALIZADO = '1' ";

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<RegistroAmostras> list = new List<RegistroAmostras>();
                RegistroAmostras produto = null;
                RegistroAmostras produtoVazio = new RegistroAmostras();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new RegistroAmostras();
                            produto.id_registros = reader["ID_REGISTROS"].ToString();
                            produto.data_cadastro = reader["DATA_CADASTRO"].ToString();
                            produto.cpf_cliente = reader["CPF_CLIENTE"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.qtde = reader["QTDE"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.finalizado = reader["FINALIZADO"].ToString();
                            produto.cod_filial = reader["COD_FILIAL"].ToString();
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

        public List<RegistroAmostras> BuscaRetornoListaNFsPorDataProdutoCPF(string dataInicio, string dataFim, string prod, string cpf)
        {
            string sql = "select ID_REGISTROS, DATA_CADASTRO, CPF_CLIENTE, PRODUTO, QTDE, RESPONSAVEL," +
                "FINALIZADO,COD_FILIAL, from REGISTRO_AMOSTRAS where " +
                "CONVERT(DATE,DATA_CADASTRO) >= '" + dataInicio + "' and CONVERT(DATE,DATA_CADASTRO) <= '" + dataFim + "' " +
                "and PRODUTO <= '" + prod + "' and FINALIZADO = '1' and CPF_CLIENTE <= '" + cpf + "'  ";

            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<RegistroAmostras> list = new List<RegistroAmostras>();
                RegistroAmostras produto = null;
                RegistroAmostras produtoVazio = new RegistroAmostras();

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new RegistroAmostras();
                            produto.id_registros = reader["ID_REGISTROS"].ToString();
                            produto.data_cadastro = reader["DATA_CADASTRO"].ToString();
                            produto.cpf_cliente = reader["CPF_CLIENTE"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.qtde = reader["QTDE"].ToString();
                            produto.responsavel = reader["RESPONSAVEL"].ToString();
                            produto.finalizado = reader["FINALIZADO"].ToString();
                            produto.cod_filial = reader["COD_FILIAL"].ToString();
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

        public override List<RegistroAmostras> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override RegistroAmostras BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override RegistroAmostras BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<RegistroAmostras> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(RegistroAmostras entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {

                string sql = "DELETE FROM REGISTRO_AMOSTRAS WHERE RESPONSAVEL = '" + entity.responsavel + "' " +
                    "and  FINALIZADO = '0' ";

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

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public string SalvarRetaguardatxt(RegistroAmostras entity, ParametrosFtp parametrosFtp)
        {
            // Obtém a data e hora atual
            DateTime dataHoras = Convert.ToDateTime(entity.data_cadastro);

            // Formata a data e hora no formato de banco de dados
            string dataHora= dataHoras.ToString("yyyy-MM-dd hh:mm:ss.mmm");


            string sql = "INSERT INTO REGISTRO_AMOSTRAS(COD_FILIAL,QTDE,DATA_CADASTRO,CPF_CLIENTE,PRODUTO,RESPONSAVEL,FINALIZADO) " +
               "VALUES('" + entity.cod_filial + "','" + entity.qtde + "','" + dataHora + "','" + entity.cpf_cliente + "'," +
               "'" + entity.produto + "','" + entity.responsavel + "','1')";

            // Obtém a data e hora atual
            DateTime dataHoraAtual = DateTime.Now;

            // Formata a data e hora no formato de banco de dados
            string dataHoraFormatada = dataHoraAtual.ToString("yyyyMMddHHmmss");
            string nomeArquivo = "AmostraProd" + entity.cod_filial + "_" + dataHoraFormatada + ".txt";
            string caminhoPasta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", "Content", "Write");
            string caminhoArquivo = Path.Combine(caminhoPasta, nomeArquivo);

            //// Escrever cada item da lista no arquivo
            //using (StreamWriter writer = new StreamWriter(caminhoArquivo))
            //{

            //    writer.WriteLine(sql);

            //}


            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());
            parametrosFtp.senha = cripSenha.DescriptografarSenhaBanco(parametrosFtp.senha);

            FtpClient ftpClient = new FtpClient("ftp://" + parametrosFtp.servidor + "/", "" + parametrosFtp.usuario + "", "" + parametrosFtp.senha + "");


            if (parametrosFtp.status != "0")
            {
                if (ftpClient.TestConnection())
                {
                    // A conexão é bem-sucedida, você pode prosseguir com as operações FTP
                    string localFilePath = caminhoArquivo;
                    string remoteFilePath = @"/" + parametrosFtp.pasta + "/" + nomeArquivo;
                    ftpClient.UploadFile(localFilePath, remoteFilePath);
                }
                else
                {
                    Console.WriteLine("erro ao gravar no diretorio");
                }

                Console.WriteLine($"Arquivo gerado em: {caminhoArquivo}");
            }
            return sql;

        }

        public override void Salvar(RegistroAmostras entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "INSERT INTO REGISTRO_AMOSTRAS(COD_FILIAL,QTDE,DATA_CADASTRO,CPF_CLIENTE,PRODUTO,RESPONSAVEL) " +
                    "VALUES('" + entity.cod_filial + "','" + entity.qtde + "',getdate(),'" + entity.cpf_cliente + "'," +
                    "'" + entity.produto + "','" + entity.responsavel + "')";
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