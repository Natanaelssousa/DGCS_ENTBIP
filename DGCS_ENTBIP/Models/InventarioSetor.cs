using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class InventarioSetor
    {
        public InventarioSetor(string vlr)
        {
            q1 = vlr;
            q2 = vlr;
            q3 = vlr;
            q4 = vlr;
            q5 = vlr;
            q6 = vlr;
            q7 = vlr;
            q8 = vlr;
            q9 = vlr;
            q10 = vlr;
            q11 = vlr;
            q12 = vlr;
            q13 = vlr;
            q14 = vlr;
            q15 = vlr;
            q16 = vlr;
            q17 = vlr;
            q18 = vlr;
            q19 = vlr;
            q20 = vlr;
            q21 = vlr;
            q22 = vlr;
            q23 = vlr;
            q24 = vlr;
            q25 = vlr;
            q26 = vlr;
            q27 = vlr;
            q28 = vlr;
            q29 = vlr;
            q30 = vlr;
            q31 = vlr;
            q32 = vlr;
            q33 = vlr;
            q34 = vlr;
            q35 = vlr;
            q36 = vlr;
            q37 = vlr;
            q38 = vlr;
            q39 = vlr;
            q40 = vlr;
            q41 = vlr;
            q42 = vlr;
            q43 = vlr;
            q44 = vlr;
            q45 = vlr;
            q46 = vlr;
            q47 = vlr;
            q48 = vlr;
        }

        public InventarioSetor()
        {
            q1 = "q1";
            q2 = "q2";
            q3 = "q3";
            q4 = "q4";
            q5 = "q5";
            q6 = "q6";
            q7 = "q7";
            q8 = "q8";
            q9 = "q9";
            q10 = "q10";
            q11 = "q11";
            q12 = "q12";
            q13 = "q13";
            q14 = "q14";
            q15 = "q15";
            q16 = "q16";
            q17 = "q17";
            q18 = "q18";
            q19 = "q19";
            q20 = "q20";
            q21 = "q21";
            q22 = "q22";
            q23 = "q23";
            q24 = "q24";
            q25 = "q25";
            q26 = "q26";
            q27 = "q27";
            q28 = "q28";
            q29 = "q29";
            q30 = "q30";
            q31 = "q31";
            q32 = "q32";
            q33 = "q33";
            q34 = "q34";
            q35 = "q35";
            q36 = "q36";
            q37 = "q37";
            q38 = "q38";
            q39 = "q39";
            q40 = "q40";
            q41 = "q41";
            q42 = "q42";
            q43 = "q43";
            q44 = "q44";
            q45 = "q45";
            q46 = "q46";
            q47 = "q47";
            q48 = "q48";

        }

        public string[] retornaVetor()
        {

            string[] vetor = new string[48];

            vetor[0] = q1;
            vetor[1] = q2;
            vetor[2] = q3;
            vetor[3] = q4;
            vetor[4] = q5;
            vetor[5] = q6;
            vetor[6] = q7;
            vetor[7] = q8;
            vetor[8] = q9;
            vetor[9] = q10;
            vetor[10] = q11;
            vetor[11] = q12;
            vetor[12] = q13;
            vetor[13] = q14;
            vetor[14] = q15;
            vetor[15] = q16;
            vetor[16] = q17;
            vetor[17] = q18;
            vetor[18] = q19;
            vetor[19] = q20;
            vetor[20] = q21;
            vetor[21] = q22;
            vetor[22] = q23;
            vetor[23] = q24;
            vetor[24] = q25;
            vetor[25] = q26;
            vetor[26] = q27;
            vetor[27] = q28;
            vetor[28] = q29;
            vetor[29] = q30;
            vetor[30] = q31;
            vetor[31] = q32;
            vetor[32] = q33;
            vetor[33] = q34;
            vetor[34] = q35;
            vetor[35] = q36;
            vetor[36] = q37;
            vetor[37] = q38;
            vetor[38] = q39;
            vetor[39] = q40;
            vetor[40] = q41;
            vetor[41] = q42;
            vetor[42] = q43;
            vetor[43] = q44;
            vetor[44] = q45;
            vetor[45] = q46;
            vetor[46] = q47;
            vetor[47] = q48;

            return vetor;

        }


        public InventarioSetor retornaObjeto(string[] vetor)
        {
            InventarioSetor objeto = new InventarioSetor();

            objeto.q1 = vetor[0];
            objeto.q2 = vetor[1];
            objeto.q3 = vetor[2];
            objeto.q4 = vetor[3];
            objeto.q5 = vetor[4];
            objeto.q6 = vetor[5];
            objeto.q7 = vetor[6];
            objeto.q8 = vetor[7];
            objeto.q9 = vetor[8];
            objeto.q10 = vetor[9];
            objeto.q11 = vetor[10];
            objeto.q12 = vetor[11];
            objeto.q13 = vetor[12];
            objeto.q14 = vetor[13];
            objeto.q15 = vetor[14];
            objeto.q16 = vetor[15];
            objeto.q17 = vetor[16];
            objeto.q18 = vetor[17];
            objeto.q19 = vetor[18];
            objeto.q20 = vetor[19];
            objeto.q21 = vetor[20];
            objeto.q22 = vetor[21];
            objeto.q23 = vetor[22];
            objeto.q24 = vetor[23];
            objeto.q25 = vetor[24];
            objeto.q26 = vetor[25];
            objeto.q27 = vetor[26];
            objeto.q28 = vetor[27];
            objeto.q29 = vetor[28];
            objeto.q30 = vetor[29];
            objeto.q31 = vetor[30];
            objeto.q32 = vetor[31];
            objeto.q33 = vetor[32];
            objeto.q34 = vetor[33];
            objeto.q35 = vetor[34];
            objeto.q36 = vetor[35];
            objeto.q37 = vetor[36];
            objeto.q38 = vetor[37];
            objeto.q39 = vetor[38];
            objeto.q40 = vetor[39];
            objeto.q41 = vetor[40];
            objeto.q42 = vetor[41];
            objeto.q43 = vetor[42];
            objeto.q44 = vetor[43];
            objeto.q45 = vetor[44];
            objeto.q46 = vetor[45];
            objeto.q47 = vetor[46];
            objeto.q48 = vetor[47];

            return objeto;

        }
        public string codigo_inventario { get; set; }
        public string filial { get; set; }
        public string local { get; set; }
        public string produto { get; set; }
        public string cor_produto { get; set; }
        public string qtde_contagem { get; set; }
        public string q1 { get; set; }
        public string q2 { get; set; }
        public string q3 { get; set; }
        public string q4 { get; set; }
        public string q5 { get; set; }
        public string q6 { get; set; }
        public string q7 { get; set; }
        public string q8 { get; set; }
        public string q9 { get; set; }
        public string q10 { get; set; }
        public string q11 { get; set; }
        public string q12 { get; set; }
        public string q13 { get; set; }
        public string q14 { get; set; }
        public string q15 { get; set; }
        public string q16 { get; set; }
        public string q17 { get; set; }
        public string q18 { get; set; }
        public string q19 { get; set; }
        public string q20 { get; set; }
        public string q21 { get; set; }
        public string q22 { get; set; }
        public string q23 { get; set; }
        public string q24 { get; set; }
        public string q25 { get; set; }
        public string q26 { get; set; }
        public string q27 { get; set; }
        public string q28 { get; set; }
        public string q29 { get; set; }
        public string q30 { get; set; }
        public string q31 { get; set; }
        public string q32 { get; set; }
        public string q33 { get; set; }
        public string q34 { get; set; }
        public string q35 { get; set; }
        public string q36 { get; set; }
        public string q37 { get; set; }
        public string q38 { get; set; }
        public string q39 { get; set; }
        public string q40 { get; set; }
        public string q41 { get; set; }
        public string q42 { get; set; }
        public string q43 { get; set; }
        public string q44 { get; set; }
        public string q45 { get; set; }
        public string q46 { get; set; }
        public string q47 { get; set; }
        public string q48 { get; set; }
        public string data_para_transferencia { get; set; }
        public string ordem_entrada { get; set; }

    }
}