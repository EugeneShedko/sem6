using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laba2;

namespace Base64
{
    static class Operation
    {
        public static double Redundancy(double shenon, double hartley)
        {
            return ((hartley - shenon) / hartley) * 100;
        }
        public static string XOR(string str1, string str2)
        {
            string resultXOR = null;
            for(int i = 0; i < str1.Length;i++)
            {
                if(str1[i] == str2[i])
                {
                    resultXOR += "0";
                }
                else
                {
                    resultXOR += "1";
                }
            }
            return resultXOR;

        }
    }
}
