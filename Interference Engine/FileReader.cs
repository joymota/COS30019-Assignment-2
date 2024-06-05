using System;  
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace IEngine {

    public static class FileReader {

        public static string[] draftList {get; set;}  
        public static string Query {get; set;}  
        public static List<string> clauses = new List<string>(); 

        public static void ReadFile(string filename) {
            //Read the file into temporary list
            draftList = File.ReadAllLines(filename);

            //Remove whitespaces and split TELL from temporary list by ; to get horn clauses
            draftList[1] = draftList[1].Replace(" ", "");
            clauses = Regex.Split(draftList[1], ";").ToList();

            //Store query from temporary list
            Query = draftList[3].Replace(" ", "");

            Console.WriteLine(clauses[2]);
            Console.WriteLine(Query);
        }

        //check if file exists
        public static bool doesFileExist(string filename) {
            bool exist = false;
            if (File.Exists(filename)) {
                exist = true;
            }
            return exist;
        }
    }
}