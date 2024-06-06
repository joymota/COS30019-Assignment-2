using System;  
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace IEngine {

    public class ForwardChaining {



        public static Dictionary<string, int> clauseCounts = new Dictionary<string, int>();

        public static Queue<string> trueClausesQueue = new Queue<string>();

        public static List<string> trueClausesList = new List<string>();

        public static List<string> KB = KnowledgeBase.HornClauses;

        public static void Solve() {
            bool var = false;
            sortSymbols();

            while (trueClausesQueue.Count > 0 && var == false) {
                string propositionSymbol = trueClausesQueue.Dequeue();

                foreach (var entry in clauseCounts)
                {
                    if (entry.Key.Contains(propositionSymbol))
                    {
                        clauseCounts[entry.Key] = entry.Value - 1;

                        if (clauseCounts[entry.Key] == 0)
                        {
                            string[] temp = Regex.Split(entry.Key, @"[^a-z0-9]+");

                            foreach (string c in temp ) 
                            {
                                if(!trueClausesList.Contains(c))
                                {
                                    trueClausesQueue.Enqueue(c);
                                    trueClausesList.Add(c);
                                }
                            }
                        }
                    }
                }
                if (trueClausesList.Contains(KnowledgeBase.Query))
                {
                    foreach (string c in trueClausesList)
                    {
                        Console.WriteLine(c);
                        
                    }
                    var = true;
                }
            }

            
        }

        public static void sortSymbols()
        {
            foreach (string k in KB) 
            {
                if(k.Contains("=>")) 
                {
                    int letterCount = CountSymbols(k);

                    clauseCounts[k] = letterCount;
                } 
                else
                {
                    trueClausesQueue.Enqueue(k);
                    trueClausesList.Add(k);
                }
            } 
            //testList();
        }

        public static int CountSymbols(string clause) 
        {
            int count = -1;

            foreach (char c in clause) 
            {
                if (char.IsLetter(c))
                {
                    count++;
                }
            }
            return count;
        }

        public static void testList() {
            foreach (KeyValuePair<string, int> pair in clauseCounts)
            {
                Console.WriteLine($"Entry: {pair.Key}, Letter Count: {pair.Value}");
            }    
            while (trueClausesQueue.Count > 1)
            {
                string element = trueClausesQueue.Dequeue();
                Console.WriteLine($"Dequeued: {element}");
            }
        }
    }
}