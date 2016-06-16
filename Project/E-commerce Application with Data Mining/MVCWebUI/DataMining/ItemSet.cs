using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebUI.DataMining
{
    public class ItemSet
    {
        public int N; //PRODUCT TABLOSUNDA BULUNAN PRODUCTLARIN TOPLAM SAYISIDIR

        public int k; // FREQUENT ITEMLERİ BULURKEN KULLANILACAK SAYIDIR.YANİ TEKLİ ELEMANLARDAN SIK GEÇENLERİ BUL,
        //2 Lİ KOMBİNLERDEN SIK GEÇENLERİ,3 LÜ OLANLARDAN... GİBİ.FREQUENT İTEM BULUNMAYANA KADAR DEVAM EDİLİR.

        public int[] data; //BİRLİKTE ALINAN ÜRÜNLARİ TUTMAK İÇİN,MESELA:[0 2 5] GİBİ

        public int hashValue; //BİRLİKTE GEÇEN ÜRÜNLER ÜZERİNDE DAHA KOLAY İŞLEM YAPABİLMEK İÇİN ŞU ŞEKİLDE 
        //HASHLİYORUZ:"0 2 5" -> 520

      public int ct;// TRANSACTION LİSTESİNDE İTEMSET LERİN KAÇ KERE GEÇTİĞİNİ TUTMAK İÇİN MESELA (1,2) İTEMSETİ 3 DEFA GEÇMİŞ GİBİ

        public ItemSet(int N, int[] items, int ct)
        {
            this.N = N;
            this.k = items.Length;
            this.data = new int[this.k];
            Array.Copy(items, this.data, items.Length);
            this.hashValue = ComputeHashValue(items);
            this.ct = ct;
        }

        private static int ComputeHashValue(int[] data)//ITEMSET'İ TEK BİR İNTEGER GİBİ TUTMAK İÇİN TERSTEN BİRLEŞTİRME YAPIYORUZ
            //ITEMSET (0,2,5) İSE 520 OLARAK HASHLEME YAPILIYOR.
        {
            int value = 0;
            int multiplier = 1;
            for (int i = 0; i < data.Length; ++i) 
            {
                value = value + (data[i] * multiplier);
                multiplier = multiplier * 10;
            }
            return value;
        }



        public bool IsSubsetOf(int[] trans)
            //Method IsSubsetOf returns true if the item-set object is a subset of a transaction:

        {
            // TRANS ARRAYİ SIRALI HALDEDİR.
            int foundIdx = -1;
            for (int j = 0; j < this.data.Length; ++j)
            {
                foundIdx = IndexOf(trans, this.data[j], foundIdx + 1);
                if (foundIdx == -1) return false;
            }
            return true;
        }
      
        //  Method IndexOf also takes advantage of ordering.
        private static int IndexOf(int[] array, int item, int startIdx)
        {
            for (int i = startIdx; i < array.Length; ++i)
            {
                if (i > item) return -1; // i is past where the target could possibly be
                if (array[i] == item) return i;
            }
            return -1;
        }


    }
}