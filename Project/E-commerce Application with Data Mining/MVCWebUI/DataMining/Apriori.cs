using Ecommerce.DAL.Concrete.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebUI.DataMining
{
    public class Apriori
    {
        
       public static List<ItemSet> GetFrequentItemSets(int N, List<int[]> transactions, int minSupportPct, int minItemSetLength)
        {
            // create a List of frequent ItemSet objects that are in transactions
            // frequent means occurs in minSupportPct percent of transactions
            // N is total number of items
            // uses a variation of the Apriori algorithm

            int minSupportCount = minSupportPct;

            Dictionary<int, bool> frequentDict = new Dictionary<int, bool>(); // key = int representation of an ItemSet, val = is in List of frequent ItemSet objects
            List<ItemSet> frequentList = new List<ItemSet>(); // item set objects that meet minimum count (in transactions) requirement 
            List<int> validItems = new List<int>(); // inidividual items/values at any given point in time to be used to construct new ItemSet (which may or may not meet threshhold count)

            // get counts of all individual items
            int[] counts = new int[N]; // index is the item/value, cell content is the count
            for (int i = 0; i < transactions.Count; ++i)
            {
                for (int j = 0; j < transactions[i].Length; ++j)
                {
                    int v = transactions[i][j];
                    ++counts[v];
                }
            }
            // for those items that meet support threshold, add to valid list, frequent list, frequent dict
            for (int i = 0; i < counts.Length; ++i)
            {
                if (counts[i] >= minSupportCount) // frequent item
                {
                    validItems.Add(i); // i is the item/value
                    int[] d = new int[1]; // the ItemSet ctor wants an array
                    d[0] = i;
                    ItemSet ci = new ItemSet(N, d, 1); // an ItemSet with size 1, ct 1
                    frequentList.Add(ci); // it's frequent
                    frequentDict.Add(ci.hashValue, true); // 
                } // else skip this item
            }

            bool done = false; // done if no new frequent item-sets found
            for (int k = 2; done == false; ++k) // construct all size  k = 2, 3, 4, . .  frequent item-sets
            {
                done = true; // assume no new item-sets will be created
                int numFreq = frequentList.Count; // List size modified so store first

                for (int i = 0; i < numFreq; ++i) // use existing frequent item-sets to create new freq item-sets with size+1
                {
                    if (frequentList[i].k != k - 1) continue; // only use those ItemSet objects with size 1 less than new ones being created

                    for (int j = 0; j < validItems.Count; ++j)
                    {
                        int[] newData = new int[k]; // data for a new item-set

                        for (int p = 0; p < k - 1; ++p)
                            newData[p] = frequentList[i].data[p]; // old data in

                        if (validItems[j] <= newData[k - 2]) continue; // because item-values are in order we can skip sometimes

                        newData[k - 1] = validItems[j]; // new item-value
                        ItemSet ci = new ItemSet(N, newData, -1); // ct to be determined

                        if (frequentDict.ContainsKey(ci.hashValue) == true) // this new ItemSet has already been added
                            continue;
                        int ct = CountTimesInTransactions(ci, transactions); // how many times is the new ItemSet in the transactuions?
                        if (ct >= minSupportCount) // we have a winner!
                        {
                            ci.ct = ct; // now we know the ct
                            frequentList.Add(ci);
                            frequentDict.Add(ci.hashValue, true);
                            done = false; // a new item-set was created, so we're not done
                        }
                    } // j
                } // i

                // update valid items -- quite subtle
                validItems.Clear();
                Dictionary<int, bool> validDict = new Dictionary<int, bool>(); // track new list of valid items
                for (int idx = 0; idx < frequentList.Count; ++idx)
                {
                    if (frequentList[idx].k != k) continue; // only looking at the just-created item-sets
                    for (int j = 0; j < frequentList[idx].data.Length; ++j)
                    {
                        int v = frequentList[idx].data[j]; // item
                        if (validDict.ContainsKey(v) == false)
                        {
                            //Console.WriteLine("adding " + v + " to valid items list");
                            validItems.Add(v);
                            validDict.Add(v, true);
                        }
                    }
                }
                validItems.Sort(); // keep valid item-values ordered so item-sets will always be ordered
            } // next k

            // transfer to return result, filtering by minItemSetCount
            List<ItemSet> result = new List<ItemSet>();
            List<int> lengths = new List<int>();

            for (int i = 0; i < frequentList.Count; ++i)
            {
                lengths.Add(frequentList[i].k);
                lengths.Sort();

            }
            for (int i = 0; i < frequentList.Count; ++i)
            {
                if (frequentList[i].k == lengths[lengths.Count - 1])
                    result.Add(new ItemSet(frequentList[i].N, frequentList[i].data, frequentList[i].ct));
            }

            return result;
        }

        private static int CountTimesInTransactions(ItemSet itemSet, List<int[]> transactions)
        {
            // number of times itemSet occurs in transactions
            int ct = 0;
            for (int i = 0; i < transactions.Count; ++i)
            {
                if (itemSet.IsSubsetOf(transactions[i])== true)
                    ++ct;
            }
            return ct;
        }
    public  static List<Rule> GetHighConfRules(List<ItemSet> freqItemSets, List<int[]> trans, double minConfidencePct)
        {
            // generate candidate rules from freqItemSets, save rules that meet min confidence against transactions
            List<Rule> result = new List<Rule>();

            Dictionary<int[], int> itemSetCountDict = new Dictionary<int[], int>(); // count of item sets

            for (int i = 0; i < freqItemSets.Count; ++i) // each freq item-set generates multiple candidate rules
            {
                int[] currItemSet = freqItemSets[i].data; // for clarity only
                int ctItemSet = CountInTrans(currItemSet, trans, itemSetCountDict); // needed for each candidate rule

                for (int len = 1; len <= currItemSet.Length - 1; ++len) // antecedent len = 1, 2, 3, . .
                {
                    int[] c = NewCombination(len); // a mathematical combination

                    while (c != null) // each combination makes a candidate rule
                    {
                        int[] ante = MakeAntecedent(currItemSet, c);
                        int[] cons = MakeConsequent(currItemSet, c); // could defer this until known if needed

                        int ctAntecendent = CountInTrans(ante, trans, itemSetCountDict); // use lookup if possible 
                        double confidence = (ctItemSet * 1.0) / ctAntecendent;

                        if (confidence >= minConfidencePct) // we have a winner!
                        {
                            Rule r = new Rule(ante, cons, confidence);
                            result.Add(r); // if freq item-sets are distinct, no dup rules ever created
                        }
                        c = NextCombination(c, currItemSet.Length);
                    } // while each combination
                } // len each possible antecedent for curr item-set
            } // i each freq item-set

            return result;
        } // GetHighConfRules

        static int[] NewCombination(int k)
        {
            // if k = 3, return is (0 1 2). n is external somewhere
            int[] result = new int[k];
            for (int i = 0; i < result.Length; ++i)
                result[i] = i;
            return result;
        }

        static int[] NextCombination(int[] comb, int n)
        {
            // if n = 5, combination = (0 3 4) next is (1 2 3)
            // if n = 5, combination = (3 4 5) next is null
            int[] result = new int[comb.Length];
            int k = comb.Length;

            if (comb[0] == n - k) return null;
            Array.Copy(comb, result, comb.Length);
            int i = k - 1;
            while (i > 0 && result[i] == n - k + i)
                --i;
            ++result[i];
            for (int j = i; j < k - 1; ++j)
                result[j + 1] = result[j] + 1;
            return result;
        }

        static int[] MakeAntecedent(int[] itemSet, int[] comb)
        {
            // if item-set = (1 3 4 6 8) and combination = (0 2) 
            // then antecedent = (1 4)
            int[] result = new int[comb.Length];
            for (int i = 0; i < comb.Length; ++i)
            {
                int idx = comb[i];
                result[i] = itemSet[idx];
            }
            return result;
        }

        static int[] MakeConsequent(int[] itemSet, int[] comb)
        {
            // if item-set = (1 3 4 6 8) and combination = (0 2) 
            // then consequent = (3 6 8)
            int[] result = new int[itemSet.Length - comb.Length];
            int j = 0; // ptr into combination
            int p = 0; // ptr into result
            for (int i = 0; i < itemSet.Length; ++i)
            {
                if (j < comb.Length && i == comb[j]) // we are at an antecedent
                    ++j; // so continue
                else
                    result[p++] = itemSet[i]; // at a consequent so add it
            }
            return result;
        }

        static int CountInTrans(int[] itemSet, List<int[]> trans, Dictionary<int[], int> countDict)
        {
            // number of times itemSet occurs in transactions, using a lookup dict
            if (countDict.ContainsKey(itemSet) == true)
                return countDict[itemSet]; // use already computed count

            int ct = 0;
            for (int i = 0; i < trans.Count; ++i)
                if (IsSubsetOf(itemSet, trans[i]) == true)
                    ++ct;
            countDict.Add(itemSet, ct);
            return ct;
        }

        static bool IsSubsetOf(int[] itemSet, int[] trans)
        {
            // 'trans' is an ordered transaction like [0 1 4 5 8]
            int foundIdx = -1;
            for (int j = 0; j < itemSet.Length; ++j)
            {
                foundIdx = IndexOf(trans, itemSet[j], foundIdx + 1);
                if (foundIdx == -1) return false;
            }
            return true;
        }

        static int IndexOf(int[] array, int item, int startIdx)
        {
            for (int i = startIdx; i < array.Length; ++i)
            {
                if (i > item) return -1; // i is past where the target could possibly be
                if (array[i] == item) return i;
            }
            return -1;
        }

    } // program class

   
}