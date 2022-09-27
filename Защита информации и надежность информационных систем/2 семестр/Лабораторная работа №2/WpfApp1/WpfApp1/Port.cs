using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Port
    {
        private Dictionary<char, int> alphabetA = new Dictionary<char, int>(){
            { 'a', 0}, { 'ä', 1}, { 'b', 2}, { 'c', 3}, { 'd', 4}, { 'e', 5}, { 'f', 6}, { 'g', 7}, { 'h', 8}, { 'i', 9}, { 'j', 10}, { 'k', 11}, { 'l', 12},
            { 'm', 13}, { 'n', 14}, { 'o', 15}, { 'ö', 16}, { 'p', 17}, { 'q', 18}, { 'r', 19}, { 's', 20}, { 'ß', 21}, { 't', 22}, { 'u', 23}, { 'ü', 24}, { 'v', 25}, { 'w', 26},
            { 'x', 27},{ 'y', 28}, { 'z', 29}
        };

        Dictionary<int, char> alphabetB = new Dictionary<int, char>(){
            { 0, 'a'}, { 1, 'ä'}, { 2, 'b'}, { 3, 'c'}, { 4, 'd'}, { 5, 'e'}, { 6, 'f'}, { 7, 'g'}, { 8, 'h'}, { 9, 'i'}, { 10, 'j'}, { 11, 'k'}, { 12, 'l'},
            { 13, 'm'}, { 14, 'n'}, { 15, 'o'}, { 16, 'ö'}, { 17, 'p'}, { 18, 'q'}, { 19, 'r'}, { 20, 's'}, { 21, 'ß'}, { 22, 't'}, { 23, 'u'}, { 24, 'ü'}, { 25, 'v'}, { 26, 'w'},
            { 27, 'x'},{ 28, 'y'}, { 29,'z'}
        };

        public double[] GetSymbolFrequency(string text)
        {
            double[] freaquency = new double[30 * 30];
            int counter = 0;
            int[,] table = GetTable();
            string[] splitText = text.Split(' ');
            int z = 0;

            for(int i = 0; i < alphabetA.Count; i++)
            {
                counter = 0;
                for(int k = 0; k < alphabetA.Count; k++, z++)
                {
                    for(int j = 0; j < splitText.Length - 1; j++)
                    {
                        if(table[i, k] == Convert.ToInt32(splitText[j]))
                        {
                            counter++;
                        }
                    }
                    freaquency[z] = (double)counter / (splitText.Length - 1);
                }
            }
            return freaquency;
        }

        public string Encrypt(string text)
        {
            int[,] table = GetTable();
            string encryptString = "";
            if (text.Length % 2 != 0)
            {
                text += 'a';
            }
            for(int i = 0; i < text.Length; i++)
            {
                encryptString += table[alphabetA[text[i]], alphabetA[text[++i]]].ToString() + ' ';
            }
            return encryptString;
        }

        public string Decrypt(string text)
        {
            int j, k;
            int[,] table = GetTable();
            string decryptText = "";
            string[] splitText = text.Split(' ');
            for(int i = 0; i < splitText.Length - 1; i++)
            {
                (j, k) = GetTableIndexForValue(Convert.ToInt32(splitText[i]), table);
                decryptText += alphabetB[j];
                decryptText += alphabetB[k];
            }
            return decryptText;
        }

        private (int, int)GetTableIndexForValue(int value, int[,] table)
        {
            for(int i = 0; i < alphabetA.Count; i++)
            {
                for(int k = 0; k < alphabetA.Count; k++)
                {
                    if (table[i, k] == value)
                        return (i, k);
                }
            }
            return (-1, -1);
        }

        private int[,] GetTable()
        {
            int[,] table = new int[alphabetA.Count, alphabetA.Count];
            for(int i = 0, counter = 0; i < alphabetA.Count; i++)
            {
                for(int k = 0; k < alphabetA.Count; k++)
                {
                    table[i,k] = counter++;
                }
            }
            return table;
        }
    }
}
