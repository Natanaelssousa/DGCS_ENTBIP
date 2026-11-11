using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class InventarioProdutoDao : Conexao<InventarioProduto, int>
    {


        private string StringConnectionfilial;

        public InventarioProdutoDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }
        public override void Atualizar(InventarioProduto entity)
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

        public override List<InventarioProduto> BuscaCompletaRetornoLista()
        {
            string sql = "";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<InventarioProduto> list = new List<InventarioProduto>();
                InventarioProduto inventario = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            inventario = new InventarioProduto();
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

        public override List<InventarioProduto> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override InventarioProduto BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override InventarioProduto BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<InventarioProduto> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(InventarioProduto entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(InventarioProduto entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "INSERT INTO INVENTARIO_PRODUTO(CODIGO_INVENTARIO,FILIAL,PRODUTO,COR_PRODUTO,QTDE,EN1,EN2,EN3,EN4,EN5,EN6,EN7,EN8, " +
            "EN9,EN10,EN11,EN12,EN13,EN14,EN15,EN16,EN17,EN18,EN19,EN20,EN21,EN22,EN23,EN24,EN25,EN26,EN27,EN28,EN29,EN30,EN31,EN32,EN33,EN34,EN35, "+
            "EN36,EN37,EN38,EN39,EN40,EN41,EN42,EN43,EN44,EN45,EN46,EN47,EN48,DATA_PARA_TRANSFERENCIA) " +
                    "VALUES('"+entity.codigo_inventario+ "','" + entity.filial + "','" + entity.produto + "','" + entity.cor_produto + "'," +
                    "'" + entity.qtde + "','" + entity.en1 + "','" + entity.en2 + "','" + entity.en3 + "','" + entity.en4 + "','" + entity.en5 + "'," +
                    "'" + entity.en6 + "','" + entity.en7 + "','" + entity.en8 + "','" + entity.en9 + "','" + entity.en10 + "','" + entity.en11 + "'," +
                    "'" + entity.en12 + "','" + entity.en13 + "','" + entity.en14 + "','" + entity.en15 + "','" + entity.en16 + "','" + entity.en17 + "'," +
                    "'" + entity.en18 + "','" + entity.en19 + "','" + entity.en20 + "','" + entity.en21 + "','" + entity.en22 + "','" + entity.en23 + "'," +
                    "'" + entity.en24 + "','" + entity.en25 + "','" + entity.en26 + "','" + entity.en27 + "','" + entity.en28 + "'," +
                    "'" + entity.en29 + "','" + entity.en30 + "','" + entity.en31 + "','" + entity.en32 + "','" + entity.en33 + "','" + entity.en34 + "'," +
                    "'" + entity.en35 + "','" + entity.en36 + "','" + entity.en37 + "','" + entity.en38 + "','" + entity.en39 + "','" + entity.en40 + "'," +
                    "'" + entity.en41 + "','" + entity.en42 + "','" + entity.en43 + "','"+entity.en44+ "','" + entity.en45 + "','" + entity.en46 + "'," +
                    "'" + entity.en47 + "','" + entity.en48 + "',GETDATE())";
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