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
    public class AuxEntradaLoteDao : Conexao<AuxEntradaLote, int>
    {
        private string StringConnectionfilial;

        public AuxEntradaLoteDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }


        public override void Atualizar(AuxEntradaLote entity)
        {
            throw new NotImplementedException();
        }

        public override List<AuxEntradaLote> BuscaCompletaRetornoLista()
        {
            throw new NotImplementedException();
        }

        public List<AuxEntradaLote> ConsultaLoteAmais(string romaneio, string responsavel, string[] id_xml_nfe)
        {
            string ids = "";
            for (int i = 0; i < id_xml_nfe.Count(); i++)
            {
                if (i + 1 == 1 && i + 1 != id_xml_nfe.Count())
                {
                    ids = "'" + id_xml_nfe[i] + "',";
                }
                else if (i + 1 == 1 && i + 1 == id_xml_nfe.Count())
                {
                    ids = "'" + id_xml_nfe[i] + "'";
                }
                else if (i + 1 == id_xml_nfe.Count())
                {
                    ids += "'" + id_xml_nfe[i] + "'";
                }
                else
                {
                    ids += "'" + id_xml_nfe[i] + "',";
                }

            }

            string sql = "SELECT A.IDS_NFS_SELECIONADAS,A.ROMANEIO_PRODUTO, A.PRODUTO" +
                ",A.COR_PRODUTO,A.DESC_PRODUTO,A.TAMANHO,A.CODIGO_BARRA,A.PRECO1,A.GRUPO,A.SUBGRUPO, " +
                "QTDE_BIPADO,B.CHAVE_NFE,B.xped XPED,DESTINATARIO FILIAL FROM (SELECT A.ID_AUX_ENTRADA,A.IDS_NFS_SELECIONADAS," +
                "A.ROMANEIO_PRODUTO, A.PRODUTO,A.COR_PRODUTO,A.DESC_PRODUTO,A.TAMANHO,A.CODIGO_BARRA,A.PRECO1," +
                "A.GRUPO,A.SUBGRUPO, 1 AS QTDE_BIPADO FROM AUX_ENTRADA A CROSS APPLY ( SELECT TOP ((A.QTDE_BIPADO)  - QTDE_TOTAL) ROW_NUMBER()" +
                " OVER (ORDER BY (SELECT NULL)) AS rn  FROM master.dbo.spt_values) AS Numbers WHERE Numbers.rn > 0 AND " +
                "QTDE_BIPADO > QTDE_TOTAL AND  ROMANEIO_PRODUTO IN ('"+romaneio+"') AND RESPONSAVEL = '"+responsavel+"' ) A " +
                " LEFT JOIN (SELECT * FROM PRODUTOS_BARRA_XML" +
                " WHERE ID_XML_NFE IN ("+ ids + ") ) B ON (A.CODIGO_BARRA = B.CODIGO_BARRA)";



            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;

                AuxEntradaLote produto = null;
                AuxEntradaLote produtoVazio = new AuxEntradaLote();
                List<AuxEntradaLote> list = new List<AuxEntradaLote>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            produto = new AuxEntradaLote();
                            produto.ids_nfs_selecionadas = reader["IDS_NFS_SELECIONADAS"].ToString();
                            produto.romaneio_produto = reader["ROMANEIO_PRODUTO"].ToString();
                            produto.produto = reader["PRODUTO"].ToString();
                            produto.cor_produto = reader["COR_PRODUTO"].ToString();
                            produto.desc_produto = reader["DESC_PRODUTO"].ToString();
                            produto.tamanho = reader["TAMANHO"].ToString();
                            produto.codigo_barra = reader["CODIGO_BARRA"].ToString();
                            produto.preco1 = reader["PRECO1"].ToString();
                            produto.grupo = reader["GRUPO"].ToString();
                            produto.subgrupo = reader["SUBGRUPO"].ToString();
                            produto.quantidade = reader["QTDE_BIPADO"].ToString();
                            produto.xped = reader["XPED"].ToString();
                            produto.chave_nfe = reader["CHAVE_NFE"].ToString();
                            produto.filial = reader["filial"].ToString();



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

        public override AuxEntradaLote BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override AuxEntradaLote BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<AuxEntradaLote> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(AuxEntradaLote entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(AuxEntradaLote entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "INSERT INTO AUX_ENTRADA_LOTE(IDS_NFS_SELECIONADAS,ROMANEIO_PRODUTO,FILIAL,CODIGO_LOJA,PRODUTO" +
                    ",DESC_PRODUTO,COR_PRODUTO,TAMANHO,CODIGO_BARRA,PRECO1,GRUPO,SUBGRUPO,LOTE,QUANTIDADE,XPED,CHAVE_NFE) " +
                    "VALUES('" + entity.ids_nfs_selecionadas.Replace("'", "") + "','" + entity.romaneio_produto + "', '" + entity.filial + "', '" + entity.codigo_loja + "', '" + entity.produto + "'," +
                    " '" + entity.desc_produto + "', '" + entity.cor_produto + "', '" + entity.tamanho + "', '" + entity.codigo_barra + "'," +
                    " '" + entity.preco1 + "', '" + entity.grupo + "', '" + entity.subgrupo + "', '" + entity.lote + "'," +
                    " '" + entity.quantidade + "', '" + entity.xped + "', '" + entity.chave_nfe + "')";
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

        public string SalvarRetaguardaftptxt(AuxEntradaLote entity, ParametrosFtp parametrosFtp)
        {

            string sql = "INSERT INTO AUX_ENTRADA_LOTE(IDS_NFS_SELECIONADAS,ROMANEIO_PRODUTO,FILIAL,CODIGO_LOJA,PRODUTO" +
                         ",DESC_PRODUTO,COR_PRODUTO,TAMANHO,CODIGO_BARRA,PRECO1,GRUPO,SUBGRUPO,LOTE,QUANTIDADE,XPED,CHAVE_NFE) " +
                         "VALUES('" + entity.ids_nfs_selecionadas.Replace("'", "") + "','" + entity.romaneio_produto + "', '" + entity.filial + "', '" + entity.codigo_loja + "', '" + entity.produto + "'," +
                         " '" + entity.desc_produto + "', '" + entity.cor_produto + "', '" + entity.tamanho + "', '" + entity.codigo_barra + "'," +
                         " '" + entity.preco1 + "', '" + entity.grupo + "', '" + entity.subgrupo + "', '" + entity.lote + "'," +
                         " '" + entity.quantidade + "', '" + entity.xped + "', '" + entity.chave_nfe + "')";



            string nomeArquivo = "LtBipChnfe" + entity.chave_nfe + "Lote"+ entity.lote + ".txt";
            string caminhoPasta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", "Content", "Write");
            string caminhoArquivo = Path.Combine(caminhoPasta, nomeArquivo);

            // Escrever cada item da lista no arquivo
            using (StreamWriter writer = new StreamWriter(caminhoArquivo))
            {

                writer.WriteLine(sql);

            }
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());
            parametrosFtp.senha = cripSenha.DescriptografarSenhaBanco(parametrosFtp.senha);

            FtpClient ftpClient = new FtpClient("ftp://"+ parametrosFtp.servidor + "/", ""+ parametrosFtp.usuario + "", ""+ parametrosFtp.senha + "");

            if(parametrosFtp.status != "0")
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

        public override List<AuxEntradaLote> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }
    }
}