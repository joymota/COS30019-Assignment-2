using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IEngine
{
    public static class TruthTable
    {
        public static void Generate(List<string> kb, string query)
        {
            List<string> symbols = GetSymbols(kb, query);
            List<List<bool>> models = GenerateModels(symbols);
            int numModelsForQuery = EvaluateKB(kb, query, symbols, models);

            if (numModelsForQuery > 0)
            {
                Console.WriteLine("YES: " + numModelsForQuery);
            }
            else
            {
                Console.WriteLine("NO");
            }
        }

        private static List<string> GetSymbols(List<string> kb, string query)
        {
            HashSet<string> symbolsSet = new HashSet<string>();

            // Extract symbols from the knowledge base
            foreach (string clause in kb)
            {
                string[] parts = clause.Split(new[] { ' ', '&', '|', '=', '>', '!', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (!part.Equals(query) && !part.Equals("=>") && !part.Equals("<=>"))
                    {
                        symbolsSet.Add(part);
                    }
                }
            }

            // Extract symbols from the query
            string[] queryParts = query.Split(new[] { ' ', '&', '|', '=', '>', '!', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in queryParts)
            {
                symbolsSet.Add(part);
            }

            return symbolsSet.ToList();
        }

        private static List<List<bool>> GenerateModels(List<string> symbols)
        {
            int numSymbols = symbols.Count;
            int numModels = (int)Math.Pow(2, numSymbols);
            List<List<bool>> models = new List<List<bool>>();

            // Generate all possible combinations of true/false for symbols
            for (int i = 0; i < numModels; i++)
            {
                List<bool> model = new List<bool>();
                for (int j = 0; j < numSymbols; j++)
                {
                    model.Add(((i >> j) & 1) == 1);
                }
                models.Add(model);
            }

            return models;
        }

        private static int EvaluateKB(List<string> kb, string query, List<string> symbols, List<List<bool>> models)
        {
            Console.WriteLine("Truth Table:");

            // Print column headers (symbols)
            foreach (string symbol in symbols)
            {
                Console.Write(symbol.PadRight(5));
            }
            Console.WriteLine("| " + query);

            int numModelsForQuery = 0;

            // Evaluate each model
            foreach (List<bool> model in models)
            {
                
                // Print model values
                foreach (bool value in model)
                {
                    Console.Write(value.ToString().PadRight(5));
                }
    
                // Evaluate the query using the model
                

                bool result = EvaluateClause(query, symbols, model);
                Console.WriteLine("| " + result);

                // If the query is true, increment the count of models
                if (result)
                {
                    numModelsForQuery++;
                    Console.WriteLine("Model: " + string.Join(", ", model.Select(b => b.ToString())));
                }
            }

            return numModelsForQuery;
        }

        private static bool EvaluateClause(string clause, List<string> symbols, List<bool> model)
        {
            // Replace symbols with their corresponding truth values
            for (int i = 0; i < symbols.Count; i++)
            {
                clause = clause.Replace(symbols[i], model[i].ToString());
            }

            // Evaluate the expression
            return bool.Parse(new DataTable().Compute(clause, "").ToString());
        }
    }
}
