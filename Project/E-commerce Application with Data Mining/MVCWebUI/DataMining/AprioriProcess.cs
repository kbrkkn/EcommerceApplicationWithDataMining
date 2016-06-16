using Ecommerce.DAL.Concrete.EntityFramework;
using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebUI.DataMining
{
    public static class AprioriProcess
    {
        public static ApplicationDbContext db = new ApplicationDbContext();
        public static List<int[]> CreateTransactions()
        {
            //PRODUCT TABLE'INDAKİ TÜM RECORDLARI TUTMAK İÇİN
            List<int> allproducts = ListAllProducts();

            //orderids={1,2,3}
            List<int> orderids = new List<int>();//orderidleri tutmak için
            orderids = db.Order.Select(x => x.OrderId).ToList();

            //transactions={[10,20],[15,12,13],[5,6,12]}
           // List<int[]> transactions = new List<int[]>(); //transaction database görevi görüyor.
            int[] product;

            //transactionindex listinde ürünid leri değil de,o ürünlerin allproducts listesindeki pozisyonlarıyla tutulması sağlanır.
            List<int[]> transactionindex = new List<int[]>();

            List<int> yy;
            foreach (var orderid in orderids)
            {   //her orderid için,ürünleri koymak için yeni array oluştur.
                //mesela orderid si 1 olan ürünleri bir arrayde topla.
                //transaction database ini listede oluşturuyoruz
                product = new int[] { };
                yy = new List<int>();

                var orderdetails = db.OrderDetail.Where(c => c.OrderId == orderid).ToList();//orderdetail tablosundan
                //orderid leri aynı olanları bir araya topla.

                //productarraye,yukarıdakilerin product id sini koy
                product = orderdetails.Select(v => v.ProductId).ToArray();

                foreach (var p in product)
                {
                    int y = allproducts.IndexOf(p);//productidlerin allproducts listesindeki indexleri bulunur.
                    yy.Add(y);
                }
              //  transactions.Add(product);
                transactionindex.Add(yy.ToArray());//productidlerin allproducts listesindeki indexlerini tutar.
            }
            return transactionindex;
          
        }

        public static List<int> ListAllProducts()
        {
            List<int> allproducts = db.Product.Select(x => x.Product_Id).ToList();
            int N = allproducts.Count;
            return allproducts;
        }
        //id=detaylarını görmek için seçtiğimiz ürünün product id sidir.
        //allproducts=tüm ürünler
        //N tüm ürünlerin toplam sayısı
        //transactionindex mesela:{[0,1,2],[2,3]} bu listedeki her bir array transaction row;arrayin elemanları ise alınan
        //product ın allproducts listesindeki indexini temsil eder.
     public static List<AdvicedProduct> DoAppriori(List<int> productids, List<int> allproducts, int N, List<int[]> transactionindex)
        {
            int minSupportPct = 2; //minsupport=2
            int minItemSetLength = 2;//level k=2
            double minConPct = 0.60;//min confidence %60
            //TRANSACTIONINDEX LİSTİNDEKİ FREQUENTITEMLERİ BULMAK İÇİN.
            List<ItemSet> frequentItemSets = Apriori.GetFrequentItemSets(N, transactionindex, minSupportPct, minItemSetLength);
            //ASSOCIATION RULE ÇIKARMAK İÇİN
            List<MVCWebUI.DataMining.Rule> goodRules = Apriori.GetHighConfRules(frequentItemSets, transactionindex, minConPct);

            //Rule:antecedent[]=>consequent[] ex:1,2=>3 
            List<int> consquents = new List<int>();
            List<int> Product_Ids = new List<int>();
            foreach (var id in productids) {
                int Product_Id = allproducts.IndexOf(id);//seçilen productın id sinin indexini allproductstan bulur.
                Product_Ids.Add(Product_Id);
            }
            var advices = new List<AdvicedProduct>();//öneri productlar için modelview classından liste(view'de tanımlamak için oluşturuldu.)
            
//çıkarılan association rule larının her birine bakar
          //  foreach (var rule in goodRules)
            for (int i = 0; i < goodRules.Count;++i )
            {
                var a = goodRules[i].antecedent.SequenceEqual(Product_Ids);
                if (a == true)
                {
                    AddatoAdvices(allproducts, goodRules, advices, i);

                }
               //ürün bir adet ise ve antecedent bir boyutlu ise ve bunlar birbirine eşitse,önerileri getir.
           /*     if ((goodRules[i].antecedent[0] == Product_Ids[0]) && (goodRules[i].antecedent.Length == 1 && Product_Ids.Count==1))
                {
                    AddatoAdvices(allproducts, goodRules, advices, i);

                }
 /*if ((goodRules[i].antecedent.Length > 1 && Product_Ids.Count > 1) && (goodRules[i].antecedent.Length == Product_Ids.Count))
                {
                    var a = goodRules[i].antecedent.SequenceEqual(Product_Ids);
                    if (a == true)
                    {
                        AddatoAdvices(allproducts, goodRules, advices, i);

                    }
  }*/
            }
            return advices;
        }

     private static void AddatoAdvices(List<int> allproducts, List<MVCWebUI.DataMining.Rule> goodRules, List<AdvicedProduct> advices, int i)
     {
         for (int a = 0; a < goodRules[i].consequent.Length; a++)//seçtiğimiz ürün için tüm önerileri sun.
         {
             //consequent bize productın allproducts'taki indexini söylediği için,gidip oradan product id yi bulduk.
             int adviceid = allproducts[goodRules[i].consequent[a]];
             Product advicedproduct = db.Product.Find(adviceid);//product tablosundan bu id ye göre ürünümüzü bulduk
             advices.Add(new AdvicedProduct
             {
                 Name = advicedproduct.Product_Description,
                 advicedImage = db.Image.Find(adviceid),
                 FileName = db.Image.Find(adviceid).FileName,
                 File = db.Image.Find(adviceid).File,
                 ImageData = db.Image.Find(adviceid).ImageData,
                 Product_Id = adviceid

             });//önerilere ekledik.

         }
     }

     

       

    }
}