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
            string filename = @"C:\Users\Joshua\Desktop\University\2024\COS30019\Assignment 2\Interference Engine\test_HornKB.txt";
            
            if (FileReader.doesFileExist(filename) == false) {
                Console.WriteLine(filename + "cant be found, please try again.");
            } else {
                KnowledgeBase.ReadFile(filename);

                ForwardChaining.Solve();
                BackwardChaining.Solve();

/*
                switch (args[0].ToLower()) {
                    case "tt": {
                        
                        break;
                    }

                    case "fc": {

                        break;
                    }

                    case "bc": {

                        break;
                    }
                }
                */ 
            }
        }


    }
}