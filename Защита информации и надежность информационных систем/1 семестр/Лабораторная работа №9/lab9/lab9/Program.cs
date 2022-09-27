using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string message = "Shedko Eugene";
            List<string> letters = new List<string>();
            List<int> frequencies = new List<int>();
            List<double> probabilities = new List<double>();

            CalculateFrequencyAndProbabilitiyForEachLetter(message, letters, frequencies, probabilities);

            string[] massOfLetters = new string[letters.Count];
            int[] massOfFrequencies = new int[letters.Count];
            double[] massOfProbabilities = new double[letters.Count];
            string[] letterBits = new string[letters.Count];

            FillAllMassesFromLists(massOfLetters, massOfFrequencies, massOfProbabilities, letters, frequencies, probabilities);
            OutputAllMass(massOfLetters, massOfFrequencies, massOfProbabilities);

            SortMassesOfLettersAndProbabilities(massOfLetters, massOfProbabilities);

            double sumOfProbabilities = CalculateSumOfProbabilities(massOfProbabilities);
            Console.WriteLine("Sum of probabilities = " + sumOfProbabilities);


            Console.WriteLine("\n\nShannon-Fano method");
            Fano(0, massOfLetters.Length - 1, massOfProbabilities, letterBits);

            Console.WriteLine("\nLetter   Bits");
            for (int i = 0; i < letterBits.Length; i++)
            {
                Console.WriteLine($"  {massOfLetters[i]}      {letterBits[i]}");
            }

            string encodedMessage = EncodeMessage(message, massOfLetters, letterBits);
            Console.WriteLine($"\nEncoded message: {encodedMessage}\n");

            string decodedMessage = DecodeMessage(encodedMessage, massOfLetters, letterBits);
            Console.WriteLine($"Decoded message: {decodedMessage}");


            Console.WriteLine("\n\nHuffman method");
            HuffmanTree huffmanTree = new HuffmanTree();

            // Build the Huffman tree
            huffmanTree.Build(message);

            // Encode
            BitArray encoded = huffmanTree.Encode(message);
            Console.Write("Encoded message: ");
            foreach (bool bit in encoded)
            {
                Console.Write((bit ? 1 : 0) + "");
            }
            Console.WriteLine();

            // Decode
            string decoded = huffmanTree.Decode(encoded);
            Console.WriteLine("\nDecoded message: " + decoded);


            Console.WriteLine("\n\nASCII");
            string asciiEncoded = EncodingToBytes(message);
            Console.WriteLine($"ASCII encodeing: {asciiEncoded}");

            Console.WriteLine("\n\nRESULT");
            Console.WriteLine($"Shannon-Fano lenght: {encodedMessage.Length}");
            Console.WriteLine($"Huffman lenght: {encoded.Count}");
            Console.WriteLine($"ASCII length: {asciiEncoded.Length}");

            Console.ReadLine();
        }

        static void CalculateFrequencyAndProbabilitiyForEachLetter(string message, List<string> letters,
            List<int> frequencies, List<double> probabilities)
        {
            string englishAlphabetRepeat = "";
            for (int i = 0; i < message.Length; i++)
            {
                int repeats = 0;
                bool repeatchar = false;
                bool theSameLetter = false;

                if (!theSameLetter)
                {

                    for (int j = 0; j < message.Length; j++)
                    {
                        if (message[i] == message[j])
                        {
                            repeatchar = true;
                            repeats++;
                        }
                    }
                }
                else
                {
                    continue;
                }

                for (int k = 0; k < englishAlphabetRepeat.Length; k++)
                {
                    if (message[i] == englishAlphabetRepeat[k])
                    {
                        theSameLetter = true;
                        break;
                    }
                }

                if (repeatchar)
                {
                    englishAlphabetRepeat = englishAlphabetRepeat + message[i];
                }


                if (!theSameLetter)
                {
                    //Console.WriteLine("Symbol " + "'" + message[i] + "'" + " repeats " + repeats + " time(s) ");
                    //Console.WriteLine("Symbol's " + "'" + message[i] + "'" + " probability is " + (double)repeats / (double)message.Length);
                    string temp = "";
                    temp = temp + message[i];
                    letters.Add(temp);
                    frequencies.Add(repeats);
                    probabilities.Add((double)repeats / (double)message.Length);
                }
            }
            //Console.WriteLine(englishAlphabetRepeat);
        }

        static void FillAllMassesFromLists(string[] massOfLetters, int[] massOfFrequencies, double[] massOfProbabilities, List<string> letters,
            List<int> frequencies, List<double> probabilities)
        {
            for (int i = 0; i < massOfLetters.Length; i++)
            {
                massOfLetters[i] = letters[i];
                massOfFrequencies[i] = frequencies[i];
                massOfProbabilities[i] = probabilities[i];
            }
        }

        static void OutputAllMass(string[] massOfLetters, int[] massOfFrequencies, double[] massOfProbabilities)
        {
            Console.WriteLine("   Letter          Frequency          probability");
            for (int i = 0; i < massOfLetters.Length; i++)
            {
                Console.WriteLine($"    '{massOfLetters[i]}'                {massOfFrequencies[i]}           {massOfProbabilities[i]}");
            }
        }

        static void SortMassesOfLettersAndProbabilities(string[] massOfLetters, double[] massOfProbabilities)
        {
            Console.WriteLine();
            Console.WriteLine("Sorted table:");
            Array.Sort(massOfProbabilities, massOfLetters);
            Array.Reverse(massOfProbabilities);
            Array.Reverse(massOfLetters);
            for (int i = 0; i < massOfProbabilities.Length; i++)
            {
                Console.Write(massOfLetters[i] + "  -  " + massOfProbabilities[i] + "\n");
            }
            Console.WriteLine();
        }

        static double CalculateSumOfProbabilities(double[] massOfProbabilities)
        {
            double sum = 0;
            for (int i = 0; i < massOfProbabilities.Length; i++)
            {
                sum += massOfProbabilities[i];
            }
            return sum;
        }

        static int Delenie_Posledovatelnosty(int L, int R, double[] massOfProbabilities)
        {
            int m;
            double schet1 = 0;
            double schet2;
            for (int i = L; i <= R - 1; i++)
            {
                schet1 = schet1 + massOfProbabilities[i];
            }

            schet2 = massOfProbabilities[R];
            m = R;
            while (schet1 >= schet2)
            {
                m = m - 1;
                schet1 = schet1 - massOfProbabilities[m];
                schet2 = schet2 + massOfProbabilities[m];
            }
            return m;
        }

        static void Fano(int L, int R, double[] massOfProbabilities, string[] letterBits)
        {
            int n;

            if (L < R)
            {

                n = Delenie_Posledovatelnosty(L, R, massOfProbabilities);
                //Console.WriteLine(n);
                for (int i = L; i <= R; i++)
                {
                    if (i <= n)
                    {
                        letterBits[i] += Convert.ToByte(1);
                    }
                    else
                    {
                        letterBits[i] += Convert.ToByte(0);
                    }
                }



                Fano(L, n, massOfProbabilities, letterBits);

                Fano(n + 1, R, massOfProbabilities, letterBits);

            }


        }

        static string EncodeMessage(string message, string[] massOfLetters, string[] letterBits)
        {
            string encodedMessage = "";
            for (int i = 0; i < message.Length; i++)
            {
                string temp = "";
                temp += message[i];
                for (int j = 0; j < massOfLetters.Length; j++)
                {
                    if (temp == massOfLetters[j])
                    {
                        encodedMessage += letterBits[j];
                        break;
                    }
                }
            }
            return encodedMessage;
        }

        static string DecodeMessage(string encodedMessage, string[] massOfLetters, string[] letterBits)
        {
            string decodedMessage = "";
            int lmin = 2;
            for (int i = 0; i < encodedMessage.Length;)
            {
                string temp = "";
                for (int k = i; k < i + lmin; k++)
                {
                    if (k < encodedMessage.Length)
                    {
                        temp += encodedMessage[k];
                    }
                    else
                    {
                        break;
                    }
                }

                bool doYouCatchMyDrift = false;
                for (int j = 0; j < letterBits.Length; j++)
                {
                    if (temp == letterBits[j])
                    {
                        decodedMessage += massOfLetters[j];
                        doYouCatchMyDrift = true;
                        break;
                    }
                }
                if (!doYouCatchMyDrift)
                {
                    lmin++;
                }
                else
                {
                    i += lmin;
                    lmin = 2;
                }
            }
            return decodedMessage;
        }

        //Перевод в биты
        public static string EncodingToBytes(string message)
        {
            var enc1251 = Encoding.GetEncoding(1251);

            //if (string.IsNullOrEmpty(message))

            string bin = string.Empty;
            var txt1251 = enc1251.GetBytes(message.ToCharArray());
            foreach (var ch in txt1251)
            {
                //Console.Write($"{ch} ");
                bin += Convert.ToString(ch, 2);
            }
            //Console.WriteLine($"\n{bin}");
            return bin;
        }


        public class Node
        {
            public char Symbol { get; set; }
            public int Frequency { get; set; }
            public Node Right { get; set; }
            public Node Left { get; set; }

            public List<bool> Traverse(char symbol, List<bool> data)
            {
                // Leaf
                if (Right == null && Left == null)
                {
                    if (symbol.Equals(this.Symbol))
                    {
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    List<bool> left = null;
                    List<bool> right = null;

                    if (Left != null)
                    {
                        List<bool> leftPath = new List<bool>();
                        leftPath.AddRange(data);
                        leftPath.Add(false);

                        left = Left.Traverse(symbol, leftPath);
                    }

                    if (Right != null)
                    {
                        List<bool> rightPath = new List<bool>();
                        rightPath.AddRange(data);
                        rightPath.Add(true);
                        right = Right.Traverse(symbol, rightPath);
                    }

                    if (left != null)
                    {
                        return left;
                    }
                    else
                    {
                        return right;
                    }
                }
            }
        }

        public class HuffmanTree
        {
            private List<Node> nodes = new List<Node>();
            public Node Root { get; set; }
            public Dictionary<char, int> Frequencies = new Dictionary<char, int>();

            public void Build(string source)
            {
                for (int i = 0; i < source.Length; i++)
                {
                    if (!Frequencies.ContainsKey(source[i]))
                    {
                        Frequencies.Add(source[i], 0);
                    }

                    Frequencies[source[i]]++;
                }

                foreach (KeyValuePair<char, int> symbol in Frequencies)
                {
                    nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
                }

                while (nodes.Count > 1)
                {
                    List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

                    if (orderedNodes.Count >= 2)
                    {
                        // Take first two items
                        List<Node> taken = orderedNodes.Take(2).ToList<Node>();

                        // Create a parent node by combining the frequencies
                        Node parent = new Node()
                        {
                            Symbol = '*',
                            Frequency = taken[0].Frequency + taken[1].Frequency,
                            Left = taken[0],
                            Right = taken[1]
                        };

                        nodes.Remove(taken[0]);
                        nodes.Remove(taken[1]);
                        nodes.Add(parent);
                    }

                    this.Root = nodes.FirstOrDefault();

                }

            }

            public BitArray Encode(string source)
            {
                List<bool> encodedSource = new List<bool>();

                for (int i = 0; i < source.Length; i++)
                {
                    List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>());
                    encodedSource.AddRange(encodedSymbol);
                }

                BitArray bits = new BitArray(encodedSource.ToArray());

                return bits;
            }

            public string Decode(BitArray bits)
            {
                Node current = this.Root;
                string decoded = "";

                foreach (bool bit in bits)
                {
                    if (bit)
                    {
                        if (current.Right != null)
                        {
                            current = current.Right;
                        }
                    }
                    else
                    {
                        if (current.Left != null)
                        {
                            current = current.Left;
                        }
                    }

                    if (IsLeaf(current))
                    {
                        decoded += current.Symbol;
                        current = this.Root;
                    }
                }

                return decoded;
            }

            public bool IsLeaf(Node node)
            {
                return (node.Left == null && node.Right == null);
            }

        }
    }
}
