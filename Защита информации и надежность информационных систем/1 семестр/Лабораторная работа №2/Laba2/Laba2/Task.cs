using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    static class Task
    {
        public static string TextReader(string path)
        {
            string str;
            using(StreamReader reader = new StreamReader(path))
            {
                str = reader.ReadLine();
            }
            return str;
        }
        public static double Entropiya(string message)
        {
            double entropiyaValue = 0;
            List<double> lettersProbability = Probability(GetSymbols(message));
            for(int i=0; i < lettersProbability.Count;i++)
            {
                entropiyaValue += lettersProbability[i] * Math.Log(lettersProbability[i], 2);
            }
            return -entropiyaValue;
        }
        public static int Quantity(char letter, string message)
        {
            int counter = 0;
            for(int i=0; i < message.Length; i++)
            {
                if(message[i] == letter)
                {
                    counter++;
                }
            }
            return counter;
        }
        public static Dictionary<char, int> GetSymbols(string message)
        {
            Dictionary<char, int> letters = new Dictionary<char, int>();
            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] != ' ')
                {
                    letters[message[i]] = Quantity(message[i], message);
                }
            }
            return letters;
        }
        public static List<double> Probability(Dictionary<char,int> letters)
        {
            List<double> lettersProbability = new List<double>();
            for(int i = 0; i < letters.Values.Count; i++)
            {
                lettersProbability.Add((double)letters.Values.ToArray()[i] / letters.Values.Sum());
            }
        
            return lettersProbability;
        }
        public static string ConvertToAscii(string str)
        {
            string asciStr = null;
            for(int i=0; i <str.Length;i++)
            {
                asciStr += Convert.ToString(str[i], 2);
            }
            return asciStr;
        }
        public static double AmountOfInformation(string message)
        {
            return Entropiya(message) * GetSymbols(message).Values.Sum();
        }

        public static double AmountOfInformationAscii(string message)
        {
            return Entropiya(ConvertToAscii(message)) * GetSymbols(message).Values.Sum() * 8;   
        
        }
        public static double AmountOfInformationWithProbability(double probability, string message)
        {
            double effectentropy = Entropiya(message) - GetConditionalEntropy(probability);
            return GetSymbols(message).Values.Sum() * effectentropy;

        }
        public static double AmountOfInformationWithProbabilityAscii(double probability, string message)
        { 
            double effectentropy = Entropiya(ConvertToAscii(message)) - GetConditionalEntropy(probability);
            return GetSymbols(message).Values.Sum() * effectentropy;

        }
        public static double GetConditionalEntropy(double probability)
        {
            return -probability * Math.Log(probability, 2) - (1 - probability) * Math.Log(1 - probability, 2);
        }
    }
}
