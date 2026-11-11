using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class InventarioConfiguracaoDao : Conexao<InventarioConfiguracao, int>
    {


        private string StringConnectionfilial;

        public  InventarioConfiguracaoDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }
        public override void Atualizar(InventarioConfiguracao entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "";
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

        public override List<InventarioConfiguracao> BuscaCompletaRetornoLista()
        {
            string sql = "";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<InventarioConfiguracao> list = new List<InventarioConfiguracao>();
                InventarioConfiguracao inventario = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            inventario = new InventarioConfiguracao();
                            //servidor.id_servidores_dgcs = reader["ID_SERVIDORES_DGCS"].ToString();
                            //servidor.situacao = reader["SITUACAO"].ToString();
                            //servidor.nome_servidor = reader["NOME_SERVIDOR"].ToString();
                            //servidor.codigo_loja = reader["CODIGO_LOJA"].ToString();
                            //servidor.ultima_edicao = reader["ULTIMA_EDICAO"].ToString();




                            list.Add(inventario);
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

        public override List<InventarioConfiguracao> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override InventarioConfiguracao BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override InventarioConfiguracao BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<InventarioConfiguracao> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(InventarioConfiguracao entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(InventarioConfiguracao entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "INSERT INTO INVENTARIO_CONFIGURACAO(CODIGO_INVENTARIO,FILIAL,LOCAL,NOME_SETOR,INICIADO,ENCERRADO,QTDE_CONTADA," +
                    "QTDE_CONTAGENS,ULTIMA_QTDE_CONTADA,NUMERO_CONTAGEM) " +
                    "VALUES('"+ entity.codigo_inventario + "','" + entity.filial + "','" + entity.local + "','" + entity.nome_setor + "',DATEADD(MI,-10,GETDATE())" +
                    ",GETDATE(),'" + entity.qtde_contada + "','" + entity.qtde_contagens + "','" + entity.ultima_qtde_contada + "','" + entity.numero_contagem + "')";
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