﻿using System;  
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
                FileReader.ReadFile(filename);
                KnowledgeBase KB = new KnowledgeBase();
                //Clone the fileReaders clauses list to the knowledge base. 
                KB.HornClauses = new List<string>(FileReader.clauses);

                //test if the list got transfered properly
                Console.WriteLine(KB.HornClauses[0]);


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
            }
        }


    }
}