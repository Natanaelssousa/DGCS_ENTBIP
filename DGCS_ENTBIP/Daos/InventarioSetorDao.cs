using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Daos
{
    public class InventarioSetorDao : Conexao<InventarioSetor, int>
    {

        private string StringConnectionfilial;

        public InventarioSetorDao(string servidor, string banco, string usuario, string senha)
        {
            StringConnectionfilial = @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + senha + ";timeout=30;app= Portal DGCS_ENTBIP";
        }



        public override void Atualizar(InventarioSetor entity)
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

        public override List<InventarioSetor> BuscaCompletaRetornoLista()
        {
            string sql = "";


            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 200000;
                List<InventarioSetor> list = new List<InventarioSetor>();
                InventarioSetor inventario = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            inventario = new InventarioSetor();
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

        public override List<InventarioSetor> BuscaPorDataRetornoLista(string data)
        {
            throw new NotImplementedException();
        }

        public override InventarioSetor BuscaPorDataRetornoObjeto(string data)
        {
            throw new NotImplementedException();
        }

        public override InventarioSetor BuscaPorIdRetornoObejto(int id)
        {
            throw new NotImplementedException();
        }

        public override List<InventarioSetor> BuscaRetornoPalavrasChaves(string palavra)
        {
            throw new NotImplementedException();
        }

        public override void Remover(InventarioSetor entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoverPorId(string id)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(InventarioSetor entity)
        {
            using (var conn = new SqlConnection(StringConnectionfilial))
            {
                string sql = "INSERT INTO INVENTARIO_SETOR( CODIGO_INVENTARIO,FILIAL,LOCAL,PRODUTO,COR_PRODUTO,QTDE_CONTAGEM,Q1" +
                    ",Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,Q23" +
                    ",Q24,Q25,Q26,Q27,Q28,Q29,Q30,Q31,Q32,Q33,Q34,Q35,Q36,Q37,Q38,Q39,Q40,Q41,Q42,Q43,Q44,Q45,Q46,Q47,Q48," +
                    "DATA_PARA_TRANSFERENCIA,ORDEM_ENTRADA) " +
                        "VALUES('" + entity.codigo_inventario + "','" + entity.filial + "','" + entity.local + "','" + entity.produto + "','" + entity.cor_produto + "'," +
                    "'" + entity.qtde_contagem + "','" + entity.q1 + "','" + entity.q2 + "','" + entity.q3 + "','" + entity.q4 + "','" + entity.q5 + "'," +
                    "'" + entity.q6 + "','" + entity.q7 + "','" + entity.q8 + "','" + entity.q9 + "','" + entity.q10 + "','" + entity.q11 + "'," +
                    "'" + entity.q12 + "','" + entity.q13 + "','" + entity.q14 + "','" + entity.q15 + "','" + entity.q16 + "','" + entity.q17 + "'," +
                    "'" + entity.q18 + "','" + entity.q19 + "','" + entity.q20 + "','" + entity.q21 + "','" + entity.q22 + "','" + entity.q23 + "'," +
                    "'" + entity.q24 + "','" + entity.q25 + "','" + entity.q26 + "','" + entity.q27 + "','" + entity.q28 + "'," +
                    "'" + entity.q29 + "','" + entity.q30 + "','" + entity.q31 + "','" + entity.q32 + "','" + entity.q33 + "','" + entity.q34 + "'," +
                    "'" + entity.q35 + "','" + entity.q36 + "','" + entity.q37 + "','" + entity.q38 + "','" + entity.q39 + "','" + entity.q40 + "'," +
                    "'" + entity.q41 + "','" + entity.q42 + "','" + entity.q43 + "','" + entity.q44 + "','" + entity.q45 + "','" + entity.q46 + "'," +
                    "'" + entity.q47 + "','" + entity.q48 + "',GETDATE(),'" + entity.ordem_entrada + "')";
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