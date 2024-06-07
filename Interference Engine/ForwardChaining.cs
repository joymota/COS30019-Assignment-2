using System;  
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IEngine {

    public class ForwardChaining {

        public static Dictionary<string, int> clauseCounts = new Dictionary<string, int>();

        public static Queue<string> trueClausesQueue = new Queue<string>();

        public static List<string> trueClausesList = new List<string>();

        public static List<string> KB = KnowledgeBase.HornClauses;

        //Solves the inference engine using forward chaining.
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

                                if(checkGoal())
                                {
                                    var = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if(var == true)
            {
                printResult();
            }
            else
            {
                Console.WriteLine("Goal could not be found");
            }
                       
        }

        //assigns the Knowledge base list to appropriate lists for manipulation
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
            //testQueue();
            //testDict();
        }

        //assists in assigning a number to the dictionary entry based off how many symbols each clause has
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

        //tests the output of the queue for testing purposes
        public static void testDict() {
            foreach (KeyValuePair<string, int> pair in clauseCounts)
            {
                Console.WriteLine($"Entry: {pair.Key}, Letter Count: {pair.Value}");
            }    
        }

        public static void testQueue() {
            foreach(var c in trueClausesQueue)
            {
                Console.WriteLine("Queue:" + c);
            }
        }

        public static void testList() {
            foreach(string c in trueClausesList)
            {
                Console.WriteLine(c + ",");
            }
        }

        public static bool checkGoal() {
            if (trueClausesList.Contains(KnowledgeBase.Query))
                {
                    return true;
                }
            return false;
        }

        public static void printResult() 
        {
            Console.Write("YES: ");
            foreach (string c in trueClausesList)
            {
                Console.Write(c + ",");
            } 
        }
    }
}


/* https://builtin.com/artificial-intelligence/forward-chaining-vs-backward-chaining#:~:text=Forward%20chaining%20is%20a%20form,facts%20that%20support%20the%20goal.

psuedocode for FC will follow here soon */