using System;  
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace IEngine {

    class Program {

        static void Main(string[] args) { 
            string filename = args[1];
            
            if (FileReader.doesFileExist(filename) == false) {
                Console.WriteLine(filename + " cant be found, please try again.");
            } else {
                KnowledgeBase.ReadFile(filename);                

                switch (args[0].ToLower()) {
                    case "tt": {
                        TruthTable.Generate(KnowledgeBase.HornClauses, KnowledgeBase.Query);
                        
                        break;
                    }

                    case "fc": {
                        ForwardChaining.Solve();
                        break;
                    }

                    case "bc": {
                        BackwardChaining.Solve();
                        break;
                    }

                    default: {
                        Console.WriteLine("Invalid method specified");
                        break;
                    }
                }
                
            }
        }


    }
}