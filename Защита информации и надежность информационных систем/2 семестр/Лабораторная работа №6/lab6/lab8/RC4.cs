using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8
{
    public class RC4
    {
    
        byte[] S = new byte[256]; ///массив-перестановка, сод. все байты от 0x00 до 0xFF
        int x = 0;      ///переменные счетчики
        int y = 0;


        public RC4(byte[] key)
        {
            /// Алгоритм ключевого расписания (Key-Scheduling Algorithm)
            /// для нач. иниц S ключом
            
            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % key.Length]) % 256;
                S.Swap(i, j);    ///поменять местами        
            }
        }


        ///для к. байта массива исх. данных запраш. байт ключа
        ///и объед их при пом xor (^)
        public byte[] Encode(byte[] dataB, int size)
        {
            byte[] data = dataB.Take(size).ToArray();
            byte[] cipher = new byte[data.Length];

            for (int m = 0; m < data.Length; m++)
            {
                cipher[m] = (byte)(data[m] ^ keyItem());
            }
            return cipher;
        }


        /// При к. вызове отдает след. байт ключ.потока
        /// кот. мы будем объед xor'ом с байтом исх. данных
        /// Генератор ПСП
        private byte keyItem()
        {
            x = (x + 1) % 256;
            y = (y + S[x]) % 256;

            S.Swap(x, y);

            return S[(S[x] + S[y]) % 256];
        }
    }

    static class SwapExt
    {
        public static void Swap<T>(this T[] array, int index1, int index2)
        {
            T temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }
}

