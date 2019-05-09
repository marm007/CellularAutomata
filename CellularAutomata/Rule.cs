using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    class Rule
    {
        int[] tab = new int[8];

        public Rule(int ruleInt)
        {
            string ruleBinary = Convert.ToString(ruleInt, 2);

            string ruleString = "";

            for (int i = 0; i < 8 - ruleBinary.Length; i++)
                ruleString += "0";
            ruleString += ruleBinary;
            ruleString = Reverse(ruleString);

            tab[0] = ruleString[0] - '0';
            tab[1] = ruleString[1] - '0';
            tab[2] = ruleString[2] - '0';
            tab[3] = ruleString[3] - '0';
            tab[4] = ruleString[4] - '0';
            tab[5] = ruleString[5] - '0';
            tab[6] = ruleString[6] - '0';
            tab[7] = ruleString[7] - '0';
        }

        public int[] Tab { get => tab; set => tab = value; }

        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}
