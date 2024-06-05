using System;  
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Dynamic;

namespace IEngine {

    public class KnowledgeBase { 

        private List<string> hornClauses = new List<string>(); 

        public KnowledgeBase()  {
            
        }

        public List<string> HornClauses
        {
            get
            {
                return hornClauses;
            }
            set
            {
                hornClauses = value;
            }
        }


    }
}