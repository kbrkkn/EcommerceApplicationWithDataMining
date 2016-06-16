using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebUI.DataMining
{
    public class Rule
    {
        public int[] antecedent;//1 VE 3 BİRLİKTE ALINDIYSA
        public int[] consequent;//2 DE ALINMIŞTIR GİBİ
        public double confidence;
        //antecedent=>consequent
        public Rule(int[] antecedent, int[] consequent, double confidence)
        {
            this.antecedent = new int[antecedent.Length];
            Array.Copy(antecedent, this.antecedent, antecedent.Length);
            this.consequent = new int[consequent.Length];
            Array.Copy(consequent, this.consequent, consequent.Length);
            this.confidence = confidence;
        }


    }
}