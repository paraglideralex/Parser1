using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Parse1
{
    class TConverter
    {
        public void ConvertFromList (List <string> list)
        {
            StreamWriter read = new StreamWriter("Read.txt");
            foreach (string s in list)
                read.Write(s);
        }

        public void ConvertFromString(string list)
        {
            StreamWriter read = new StreamWriter("RarePoints.txt");
            foreach (char s in list)
                read.Write(s);
        }


    }
}
