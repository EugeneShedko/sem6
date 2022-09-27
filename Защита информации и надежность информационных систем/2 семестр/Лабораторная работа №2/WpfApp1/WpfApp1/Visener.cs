using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Visener
    {
        private string initialKey = "shedko";
        Dictionary<char, int> alphabetA = new Dictionary<char, int>(){
            { 'a', 0}, { 'ä', 1}, { 'b', 2}, { 'c', 3}, { 'd', 4}, { 'e', 5}, { 'f', 6}, { 'g', 7}, { 'h', 8}, { 'i', 9}, { 'j', 10}, { 'k', 11}, { 'l', 12},
            { 'm', 13}, { 'n', 14}, { 'o', 15}, { 'ö', 16}, { 'p', 17}, { 'q', 18}, { 'r', 19}, { 's', 20}, { 'ß', 21}, { 't', 22}, { 'u', 23}, { 'ü', 24}, { 'v', 25}, { 'w', 26},
            { 'x', 27},{ 'y', 28}, { 'z', 29}
        };

        Dictionary<int, char> alphabetB = new Dictionary<int, char>(){
            { 0, 'a'}, { 1, 'ä'}, { 2, 'b'}, { 3, 'c'}, { 4, 'd'}, { 5, 'e'}, { 6, 'f'}, { 7, 'g'}, { 8, 'h'}, { 9, 'i'}, { 10, 'j'}, { 11, 'k'}, { 12, 'l'},
            { 13, 'm'}, { 14, 'n'}, { 15, 'o'}, { 16, 'ö'}, { 17, 'p'}, { 18, 'q'}, { 19, 'r'}, { 20, 's'}, { 21, 'ß'}, { 22, 't'}, { 23, 'u'}, { 24, 'ü'}, { 25, 'v'}, { 26, 'w'},
            { 27, 'x'},{ 28, 'y'}, { 29,'z'}
        };

        private string CreateFullKey(int textLength)
        {
            string fullKey = initialKey;
            for(int i = 0, k = 0; i < textLength; i++)
            {
                if(k == initialKey.Length)
                {
                    k = 0;
                }
                fullKey += initialKey[k++];
            }
            return fullKey;
        }

        //public ObservableCollection<Veroyat> GetSymbolFrequency(string text)
        public double[] GetSymbolFrequency(string text)
        {
            int counter = 0;
            double[] frequency = new double[30];
            //ObservableCollection<Veroyat> ver = new ObservableCollection<Veroyat>();
            for (int i = 0; i < alphabetB.Count; i++)
            {
                counter = 0;
                for(int k = 0; k < text.Length; k++)
                {
                    if (alphabetB[i] == text[k])
                    {
                        counter++;
                    }
                }
                frequency[i] = (double)counter / text.Length;
                //ver.Add(new Veroyat(frequency[i] , alphabetB[i].ToString()));
            }
            return frequency;
        }

        public string Encrypt(string text)
        {
            GetSymbolFrequency(text);
            string encryptText = "";
            string key = CreateFullKey(text.Length);
            for(int i = 0; i < text.Length; i++)
            {
                encryptText += alphabetB[(alphabetA[text[i]] + alphabetA[key[i]]) % alphabetA.Count];
            }
            return encryptText;
        }

        public string Decrypt(string text)
        {
            int x;
            string decryptText = "";
            string key = CreateFullKey(text.Length);
            for(int i = 0; i < text.Length; i++)
            {
                x = alphabetA[text[i]] - alphabetA[key[i]];
                decryptText += alphabetB[(x  >= 0) ? x % alphabetA.Count:alphabetA.Count + x];
            }
            return decryptText;
        }
    }
}
