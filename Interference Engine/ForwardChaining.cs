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
                                    printResult();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("FC Goal could not be found");                      
        }

        //assigns the Knowledge base list to appropriate lists for manipulation, for all Dictionary, list and queue. 
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

        //prints result
        public static void printResult() 
        {
            Console.Write("YES: ");
            Console.WriteLine(string.Join(", ", trueClausesList));
        }

        //Checks if goal is found in true clause list
        public static bool checkGoal() 
        {
            return trueClausesList.Contains(KnowledgeBase.Query);
        }

        //next 3 methods were used for testing purposes only
        //tests the output of the Dictionary
        public static void testDict() {
            foreach (KeyValuePair<string, int> pair in clauseCounts)
            {
                Console.WriteLine($"Entry: {pair.Key}, Letter Count: {pair.Value}");
            }    
        }

        //tests output of Queue
        public static void testQueue() {
            foreach(var c in trueClausesQueue)
            {
                Console.WriteLine("Queue:" + c);
            }
        }

        //Tests output of list
        public static void testList() {
            foreach(string c in trueClausesList)
            {
                Console.WriteLine(c + ",");
            }
        }



    }
}


/* https://builtin.com/artificial-intelligence/forward-chaining-vs-backward-chaining#:~:text=Forward%20chaining%20is%20a%20form,facts%20that%20support%20the%20goal.
i used this website to help me understand forward chaining. 

psuedocode for FC 

1. Apply value to clauses based on how many are needed to fulfil conditions --> e.g. a=>b, value assigned is 1, since only 1 symbol is required to prove b is true
2. Loop through and find out which clauses are known to be true, put them in a seperate list.
3. Put this list in a Queue, dequeue one, loop through clause dictionary and minus one from dictionary key value whenever one of its conditions are determined to be true from current true proposition symbol
4. If key value of clause becomes 0, if theres a symbol in clause which doesnt currently exist in list of true proposition symbols, add to list and enqueue to end of queue.
5. Check if goal symbol is in true proposition symbol list. If not, dequeue next symbol and repeat process until goal state reached or queue is empty. 

*/