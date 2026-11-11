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
    public class ComprasPedidoDao : Conexao<ComprasPedido, int>
    {
        public override void Atualizar(ComprasPedido entity)
        {
            throw new NotImplementedException();
        }

        public override List<ComprasPedido> BuscaCompletaRetornoLista()
        {
            throw new NotImplementedException();
        }

        public override List<ComprasPedido> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override ComprasPedido BuscaPorDataRetornoObjeto(string data)
        {
            string sql = "select * from COMPRAS_PEDIDO WHERE CHAVE_NFE = '" + data + "' ";



            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                ComprasPedido produto = null;
                ComprasPedido produtoVazio = new ComprasPedido();
                List<ComprasPedido> list = new List<ComprasPedido>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new ComprasPedido();
                            produto.chave_nfe = reader["chave_nfe"].ToString();
                            produto.nf_entrada = reader["nf_entrada"].ToString();
                            produto.cnpj = reader["cnpj"].ToString();


                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                conn.Close();
                SqlConnection.ClearPool(conn);
                return produto;
            }
        }

        public override ComprasPedido BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<ComprasPedido> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(ComprasPedido entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(ComprasPedido entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "INSERT INTO COMPRAS_PEDIDO (chave_nfe,nf_entrada,serie_nf_entrada,cnpj,usuario,loja) " +
                    "VALUES('" + entity.chave_nfe + "', '" + entity.nf_entrada + "', '" + entity.serie_nf_entrada + "'," +
                    " '" + entity.cnpj + "', '" + entity.usuario + "', '" + entity.loja + "')";
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


        public void Salvartxt(ComprasPedido entity, ParametrosFtp parametrosFtp)
        {

            string sql = "INSERT INTO COMPRAS_PEDIDO (chave_nfe,nf_entrada,serie_nf_entrada,cnpj,usuario,loja) " +
                "VALUES('" + entity.chave_nfe + "', '" + entity.nf_entrada + "', '" + entity.serie_nf_entrada + "'," +
                " '" + entity.cnpj + "', '" + entity.usuario + "', '" + entity.loja + "')";



            string nomeArquivo = "PedCsmChave" + entity.chave_nfe + ".txt";
            string caminhoPasta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", "Content", "Write");
            string caminhoArquivo = Path.Combine(caminhoPasta, nomeArquivo);

            // Escrever cada item da lista no arquivo
            using (StreamWriter writer = new StreamWriter(caminhoArquivo))
            {

                writer.WriteLine(sql);

            }

            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());
            parametrosFtp.senha = cripSenha.DescriptografarSenhaBanco(parametrosFtp.senha);

            FtpClient ftpClient = new FtpClient("ftp://" + parametrosFtp.servidor + "/", "" + parametrosFtp.usuario + "", "" + parametrosFtp.senha + "");

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

            if (ftpClient.TestConnection())
            {
                // A conexão é bem-sucedida, você pode prosseguir com as operações FTP
                string localFilePath = caminhoArquivo;
                string remoteFilePath = @"/ENTBIP/" + nomeArquivo;
                ftpClient.UploadFile(localFilePath, remoteFilePath);
            }
            else
            {
                Console.WriteLine("erro ao gravar no diretorio");
            }

            Console.WriteLine($"Arquivo gerado em: {caminhoArquivo}");


        }
    }
}