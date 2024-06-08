using System;  
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEngine
{
    public class BackwardChaining
    {
        public static List<string> KB = KnowledgeBase.HornClauses;
        public static string query = KnowledgeBase.Query;

        // Solves the inference engine using backward chaining.
        public static void Solve()
        {
            HashSet<string> visited = new HashSet<string>();
            bool result = BackwardChain(query, visited);

            if (result)
            {
                Console.WriteLine("YES: " + string.Join(", ", visited));
            }
            else
            {
                Console.WriteLine("BC Goal could not be found");
            }
        }

        // The backward chaining algorithm.
        public static bool BackwardChain(string goal, HashSet<string> visited)
        {
            // If the goal is already a known fact
            if (KB.Contains(goal))
            {
                visited.Add(goal);
                return true;
            }

            // Find all rules that have the goal as their conclusion
            List<string> rules = new List<string>();
            foreach (string clause in KB)
            {
                if (clause.Contains("=>"))
                {
                    string[] parts = clause.Split("=>");
                    string conclusion = parts[1];
                    if (conclusion == goal)
                    {
                        rules.Add(clause);
                    }
                }
            }

            foreach (var rule in rules)
            {
                // Get the premises of the rule
                string[] parts = rule.Split("=>");
                string[] premises = parts[0].Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries); // these 2 lines = a & b => c turns into ["a", "b"] ["c"], ["a", "b"] would be the premise

                bool allPremisesTrue = true;
                foreach (var premise in premises)
                {   
                    //recursion - loop through method again until either true or false
                    if (!visited.Contains(premise) && !BackwardChain(premise, visited)) //if the premise has not already been visited OR if the method can not prove the premise is true
                    {
                        allPremisesTrue = false;
                        break;
                    }
                }

                if (allPremisesTrue)
                {
                    visited.Add(goal);
                    return true;
                }
            }

        return false; //if break
        }
    }

}


/*

    1. if goal is in KB as a fact, return true
    2. Find all rules which have the goal as their conclusion.
    3. Find the premises of those

    Step 1: Define the knowledge base as per the requirements. 
    Step 2: Set the goal or conclusion to begin the backward chaining process. 
    Step 3: Start and iterate the functions. Check the facts to match the goal. Terminate the process if successful; else, iterate over the rules in the knowledge base. Search to match the conclusion with a current goal. Like forward chaining, recursively invoke the backward chaining process for the rule conditions if there is a match. Repeat the previous process until you reach the endpoint. 
    Step 4: Determine the termination condition for the backward chaining process that can be any specific criteria. Output the final result after reaching the endpoint. 

    https://www.simplilearn.com/tutorials/artificial-intelligence-tutorial/forward-and-backward-chaining-in-ai

    1. Start with initial goal
    2. Finds rule with conclusion is goal
    3. Extract premises of matching rule
    4. Prove premise is true, premise becomes new goal
    5. Repeat until final premise is known to be true, or last premise in chain is false.
*/