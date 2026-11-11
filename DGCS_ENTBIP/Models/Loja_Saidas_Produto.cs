using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class Loja_Saidas_Produto
    {

        public Loja_Saidas_Produto(string vlr)
        {
            en1 = vlr;
            en2 = vlr;
            en3 = vlr;
            en4 = vlr;
            en5 = vlr;
            en6 = vlr;
            en7 = vlr;
            en8 = vlr;
            en9 = vlr;
            en10 = vlr;
            en11 = vlr;
            en12 = vlr;
            en13 = vlr;
            en14 = vlr;
            en15 = vlr;
            en16 = vlr;
            en17 = vlr;
            en18 = vlr;
            en19 = vlr;
            en20 = vlr;
            en21 = vlr;
            en22 = vlr;
            en23 = vlr;
            en24 = vlr;
            en25 = vlr;
            en26 = vlr;
            en27 = vlr;
            en28 = vlr;
            en29 = vlr;
            en30 = vlr;
            en31 = vlr;
            en32 = vlr;
            en33 = vlr;
            en34 = vlr;
            en35 = vlr;
            en36 = vlr;
            en37 = vlr;
            en38 = vlr;
            en39 = vlr;
            en40 = vlr;
            en41 = vlr;
            en42 = vlr;
            en43 = vlr;
            en44 = vlr;
            en45 = vlr;
            en46 = vlr;
            en47 = vlr;
            en48 = vlr;
        }

        public Loja_Saidas_Produto()
        {
            en1 = "en1";
            en2 = "en2";
            en3 = "en3";
            en4 = "en4";
            en5 = "en5";
            en6 = "en6";
            en7 = "en7";
            en8 = "en8";
            en9 = "en9";
            en10 = "en10";
            en11 = "en11";
            en12 = "en12";
            en13 = "en13";
            en14 = "en14";
            en15 = "en15";
            en16 = "en16";
            en17 = "en17";
            en18 = "en18";
            en19 = "en19";
            en20 = "en20";
            en21 = "en21";
            en22 = "en22";
            en23 = "en23";
            en24 = "en24";
            en25 = "en25";
            en26 = "en26";
            en27 = "en27";
            en28 = "en28";
            en29 = "en29";
            en30 = "en30";
            en31 = "en31";
            en32 = "en32";
            en33 = "en33";
            en34 = "en34";
            en35 = "en35";
            en36 = "en36";
            en37 = "en37";
            en38 = "en38";
            en39 = "en39";
            en40 = "en40";
            en41 = "en41";
            en42 = "en42";
            en43 = "en43";
            en44 = "en44";
            en45 = "en45";
            en46 = "en46";
            en47 = "en47";
            en48 = "en48";

        }

        public string[] retornaVetor()
        {

            string[] vetor = new string[48];

            vetor[0] = en1;
            vetor[1] = en2;
            vetor[2] = en3;
            vetor[3] = en4;
            vetor[4] = en5;
            vetor[5] = en6;
            vetor[6] = en7;
            vetor[7] = en8;
            vetor[8] = en9;
            vetor[9] = en10;
            vetor[10] = en11;
            vetor[11] = en12;
            vetor[12] = en13;
            vetor[13] = en14;
            vetor[14] = en15;
            vetor[15] = en16;
            vetor[16] = en17;
            vetor[17] = en18;
            vetor[18] = en19;
            vetor[19] = en20;
            vetor[20] = en21;
            vetor[21] = en22;
            vetor[22] = en23;
            vetor[23] = en24;
            vetor[24] = en25;
            vetor[25] = en26;
            vetor[26] = en27;
            vetor[27] = en28;
            vetor[28] = en29;
            vetor[29] = en30;
            vetor[30] = en31;
            vetor[31] = en32;
            vetor[32] = en33;
            vetor[33] = en34;
            vetor[34] = en35;
            vetor[35] = en36;
            vetor[36] = en37;
            vetor[37] = en38;
            vetor[38] = en39;
            vetor[39] = en40;
            vetor[40] = en41;
            vetor[41] = en42;
            vetor[42] = en43;
            vetor[43] = en44;
            vetor[44] = en45;
            vetor[45] = en46;
            vetor[46] = en47;
            vetor[47] = en48;

            return vetor;

        }


        public Loja_Saidas_Produto retornaObjeto(string[] vetor)
        {
            Loja_Saidas_Produto objeto = new Loja_Saidas_Produto();

            objeto.en1 = vetor[0];
            objeto.en2 = vetor[1];
            objeto.en3 = vetor[2];
            objeto.en4 = vetor[3];
            objeto.en5 = vetor[4];
            objeto.en6 = vetor[5];
            objeto.en7 = vetor[6];
            objeto.en8 = vetor[7];
            objeto.en9 = vetor[8];
            objeto.en10 = vetor[9];
            objeto.en11 = vetor[10];
            objeto.en12 = vetor[11];
            objeto.en13 = vetor[12];
            objeto.en14 = vetor[13];
            objeto.en15 = vetor[14];
            objeto.en16 = vetor[15];
            objeto.en17 = vetor[16];
            objeto.en18 = vetor[17];
            objeto.en19 = vetor[18];
            objeto.en20 = vetor[19];
            objeto.en21 = vetor[20];
            objeto.en22 = vetor[21];
            objeto.en23 = vetor[22];
            objeto.en24 = vetor[23];
            objeto.en25 = vetor[24];
            objeto.en26 = vetor[25];
            objeto.en27 = vetor[26];
            objeto.en28 = vetor[27];
            objeto.en29 = vetor[28];
            objeto.en30 = vetor[29];
            objeto.en31 = vetor[30];
            objeto.en32 = vetor[31];
            objeto.en33 = vetor[32];
            objeto.en34 = vetor[33];
            objeto.en35 = vetor[34];
            objeto.en36 = vetor[35];
            objeto.en37 = vetor[36];
            objeto.en38 = vetor[37];
            objeto.en39 = vetor[38];
            objeto.en40 = vetor[39];
            objeto.en41 = vetor[40];
            objeto.en42 = vetor[41];
            objeto.en43 = vetor[42];
            objeto.en44 = vetor[43];
            objeto.en45 = vetor[44];
            objeto.en46 = vetor[45];
            objeto.en47 = vetor[46];
            objeto.en48 = vetor[47];

            return objeto;

        }

        public string romaneio_produto { get; set; }
        public string filial { get; set; }
        public string produto { get; set; }
        public string cor_produto { get; set; }
        public string en1 { get; set; }
        public string en2 { get; set; }
        public string en3 { get; set; }
        public string en4 { get; set; }
        public string en5 { get; set; }
        public string en6 { get; set; }
        public string en7 { get; set; }
        public string en8 { get; set; }
        public string en9 { get; set; }
        public string en10 { get; set; }
        public string en11 { get; set; }
        public string en12 { get; set; }
        public string en13 { get; set; }
        public string en14 { get; set; }
        public string en15 { get; set; }
        public string en16 { get; set; }
        public string en17 { get; set; }
        public string en18 { get; set; }
        public string en19 { get; set; }
        public string en20 { get; set; }
        public string en21 { get; set; }
        public string en22 { get; set; }
        public string en23 { get; set; }
        public string en24 { get; set; }
        public string en25 { get; set; }
        public string en26 { get; set; }
        public string en27 { get; set; }
        public string en28 { get; set; }
        public string en29 { get; set; }
        public string en30 { get; set; }
        public string en31 { get; set; }
        public string en32 { get; set; }
        public string en33 { get; set; }
        public string en34 { get; set; }
        public string en35 { get; set; }
        public string en36 { get; set; }
        public string en37 { get; set; }
        public string en38 { get; set; }
        public string en39 { get; set; }
        public string en40 { get; set; }
        public string en41 { get; set; }
        public string en42 { get; set; }
        public string en43 { get; set; }
        public string en44 { get; set; }
        public string en45 { get; set; }
        public string en46 { get; set; }
        public string en47 { get; set; }
        public string en48 { get; set; }
        public string timestamp { get; set; }
        public string valor { get; set; }
        public string preco4 { get; set; }
        public string preco3 { get; set; }
        public string preco2 { get; set; }
        public string preco1 { get; set; }
        public string qtde_saida { get; set; }
        public string data_para_transferencia { get; set; }

    }

}