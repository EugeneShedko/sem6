using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class MultiplePermutation
    {
        private string _firstKey = "shedko";
        private string _secondKey = "jewgeni";
        private Dictionary<char, int> _alphabet = new Dictionary<char, int>(){
            { 'a', 0}, { 'ä', 1}, { 'b', 2}, { 'c', 3}, { 'd', 4}, { 'e', 5}, { 'f', 6}, { 'g', 7}, { 'h', 8}, { 'i', 9}, { 'j', 10}, { 'k', 11}, { 'l', 12},
            { 'm', 13}, { 'n', 14}, { 'o', 15}, { 'ö', 16}, { 'p', 17}, { 'q', 18}, { 'r', 19}, { 's', 20}, { 'ß', 21}, { 't', 22}, { 'u', 23}, { 'ü', 24}, { 'v', 25}, { 'w', 26},
            { 'x', 27},{ 'y', 28}, { 'z', 29}
        };

        private char[] _apha = {'a','ä','b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'ö', 'p', 'q', 'r', 's', 'ß', 't', 'u', 'ü', 'v', 'w', 'x', 'y', 'z'};
        public double[] GetSymbolProbability(string text)
        {
            int counter;
            double[] probability = new double[_apha.Length];
            for(int i = 0; i < _apha.Length; i++)
            {
                counter = 0;
                for(int j = 0; j < text.Length; j++)
                {
                    if(_apha[i] == text[j])
                    {
                        counter++;
                    }
                }
                probability[i] = (double)counter / text.Length;
            }
            return probability;
        }
        public string DecryptText(string text)
        {
            int start = 0;
            int length = _firstKey.Length * _secondKey.Length;
            string tempstr;
            string decryptText = "";
            int[] firstKeyIndex = GetKeyIndex(_firstKey);
            int[] secondKeyIndex = GetKeyIndex(_secondKey);
            int tablesNumber = (text.Length % (_firstKey.Length * _secondKey.Length) == 0) ? text.Length / (_firstKey.Length * _secondKey.Length) : text.Length / (_firstKey.Length * _secondKey.Length) + 1;
            for (int i = 0; i < tablesNumber; i++)
            {
                tempstr = text.Substring(start, length);
                decryptText += ReadDecryptTextFromTable(SortDecryptArrayByRows(SortDecryptArrayByColumn(GetDecryptTable(_firstKey.Length, _secondKey.Length, tempstr), secondKeyIndex), firstKeyIndex));
                start += _firstKey.Length * _secondKey.Length;
                if (start + _firstKey.Length * _secondKey.Length > text.Length)
                {
                    length = text.Length - start;
                }
            }
            return decryptText;
        }
        public string EncryptText(string text)
        {
            int start = 0;
            string encryptText = "";
            string tempstr;
            int length = _firstKey.Length * _secondKey.Length;
            int[] firstKeyIndex = GetKeyIndex(_firstKey);
            int[] secondKeyIndex = GetKeyIndex(_secondKey);
            int tablesNumber = (text.Length % (_firstKey.Length * _secondKey.Length) == 0) ? text.Length / (_firstKey.Length * _secondKey.Length) : text.Length / (_firstKey.Length * _secondKey.Length) + 1;
            for(int i = 0; i < tablesNumber; i++)
            {
                tempstr = text.Substring(start, length);
                encryptText += ReadEncryptTextFromTable(SortArrayByColumn(SortArrayByRows(GetEncryptTable(_firstKey.Length, _secondKey.Length, tempstr) , firstKeyIndex), secondKeyIndex));
                start += _firstKey.Length * _secondKey.Length;
                if(start + _firstKey.Length * _secondKey.Length > text.Length)
                {
                    length = text.Length - start;
                }
            }
            return encryptText;
        }

        private string ReadEncryptTextFromTable(char[,] table)
        {
            string encryptText = "";
            int rowNumber = table.GetUpperBound(0) + 1;
            int columnNumber = table.GetUpperBound(1) + 1;
            for(int i = 0; i < columnNumber; i++)
            {
                for(int j = 0; j < rowNumber; j++)
                {
                    encryptText += table[j, i];
                }
            }
            return encryptText;
        }

        private string ReadDecryptTextFromTable(char[,] table)
        {
            string encryptText = "";
            int rowNumber = table.GetUpperBound(0) + 1;
            int columnNumber = table.GetUpperBound(1) + 1;
            for (int i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < columnNumber; j++)
                {
                    encryptText += table[i, j];
                }
            }
            return encryptText;
        }
        private char[,] SortArrayByColumn(char[,] array, int[] keyIndex)
        {
            char[,] sortedArrayByColumn = new char[array.GetUpperBound(0) + 1, array.GetUpperBound(1) + 1];
            int rowNumber = array.GetUpperBound(0) + 1;
            int columnNumber = array.GetUpperBound(1) + 1;

            for (int i = 0; i < columnNumber; i++)
            {
                for (int j = 0; j < rowNumber; j++)
                {
                  
                    sortedArrayByColumn[j, keyIndex[i] - 1] = array[j, i];
                }
            }
            return sortedArrayByColumn;
        }

        private char[,] SortDecryptArrayByColumn(char[,] array,  int[] keyIndex)
        {
            char[,] sortedDecryptArrayByColumn = new char[array.GetUpperBound(0) + 1, array.GetUpperBound(1) + 1];
            int rowNumber = array.GetUpperBound(0) + 1;
            int columnNumber = array.GetUpperBound(1) + 1;

            for (int i = 0; i < columnNumber; i++)
            {
                for (int j = 0; j < rowNumber; j++)
                {

                    sortedDecryptArrayByColumn[j, i] = array[j, keyIndex[i] - 1];
                }
            }
            return sortedDecryptArrayByColumn;
        }
       
        private char[,] SortArrayByRows(char[,] array, int[] keyIndex)
        {
            char[,] sortedArrayByRows = new char[array.GetUpperBound(0) + 1, array.GetUpperBound(1) + 1];
            int rowNumber = array.GetUpperBound(0) + 1;
            int columnNumber = array.GetUpperBound(1) + 1;
            
            for(int i = 0; i < rowNumber; i++)
            {
                for(int j = 0; j < columnNumber; j++)
                {
                    sortedArrayByRows[keyIndex[i] - 1, j] = array[i, j]; 
                }
            }
            return sortedArrayByRows;
        }

        private char[,] SortDecryptArrayByRows(char[,] array, int[] keyIndex)
        {
            char[,] sortedArrayByRows = new char[array.GetUpperBound(0) + 1, array.GetUpperBound(1) + 1];
            int rowNumber = array.GetUpperBound(0) + 1;
            int columnNumber = array.GetUpperBound(1) + 1;

            for (int i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < columnNumber; j++)
                {
                    sortedArrayByRows[i, j] = array[keyIndex[i] - 1, j];
                }
            }
            return sortedArrayByRows;
        }

        private int[] GetKeyIndex(string key)
        {
            int counter;
            int[] keyIndexInAlphabet = new int[key.Length];
            int[] keyOrder = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                keyIndexInAlphabet[i] = _alphabet[key[i]];
            }
            for(int i = 0; i < keyIndexInAlphabet.Length; i++)
            {
                counter = 1;
                for(int j = 0; j < keyIndexInAlphabet.Length; j++)
                {
                    if((keyIndexInAlphabet[i] > keyIndexInAlphabet[j]) && i != j)
                    {
                        counter++;
                    }
                }
                keyOrder[i] = counter;
            }
            for(int i = 0; i < keyOrder.Length; i++)
            {
                for(int j = 0; j < keyOrder.Length; j++)
                {
                    if(keyOrder[i] == keyOrder[j] && i != j)
                    {
                        keyOrder[j]++;
                    }
                }
            }
            return keyOrder;
        }

        private char[,] GetDecryptTable(int firstKeyLength, int secondKeyLength, string text)
        {
            char[,] decryptTable = new char[firstKeyLength, secondKeyLength];
            int decryptTableRowNumber = decryptTable.GetUpperBound(0) + 1;
            int decryptTableColumnNumber = decryptTable.GetUpperBound(1) + 1;
            for (int i = 0, z = 0; i < decryptTableColumnNumber; i++)
            {
                for (int j = 0; j < decryptTableRowNumber; j++)
                {
                    if (z < text.Length)
                    {
                        decryptTable[j, i] = text[z++];
                    }
                    else
                    {
                        decryptTable[j, i] = ' ';
                    }
                }
            }
            return decryptTable;


        }
        private char[,] GetEncryptTable(int firstKeyLength, int secondKeyLength, string text)
        {
            char[,] encryptTable = new char[firstKeyLength, secondKeyLength];
            int encryptTableRowNumber = encryptTable.GetUpperBound(0) + 1;
            int encryptTableColumnNumber = encryptTable.GetUpperBound(1) + 1;
            for(int i = 0, z = 0; i < encryptTableRowNumber; i++)
            {
                for(int j = 0; j < encryptTableColumnNumber; j++)
                {
                    if (z < text.Length)
                    {
                        encryptTable[i, j] = text[z++];
                    }
                    else
                    {
                        encryptTable[i, j] = ' ';
                    }
                }    
            }
            return encryptTable;
        }
    }
}
