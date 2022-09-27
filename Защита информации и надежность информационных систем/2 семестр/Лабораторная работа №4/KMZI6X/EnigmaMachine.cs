using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZI6X
{
    public class EnigmaMachine
    {
        private Dictionary<Char, Char> plugBoard;

        private Rotor[] rotors;
        private Rotor reflector;
        
        private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private const string rotorIconf = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
        private const string rotorIIconf = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
        private const string rotorIIIconf = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
        private const string rotorIVconf = "ESOVPZJAYQUIRHXLNFTGKDCMWB";
        private const string rotorVIIconf = "NZJHGRCXMYSWBOUFAIVLPEKQDT";
        private const string rotorBetaconf = "LEYJVCNIXWPBQMDRTAKZFGUHOS";      //7 Gamma 2
        private const string rotorGammaconf = "FSOKANUERHMBTIYCWLQPZXVGJD";

        
        private const string reflectorAconf = "EJMZALYXVBWFCRQUONTSPIKHGD";
        private const string reflectorBconf = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
        private const string reflectorCconf = "FVPJIAOYEDRZXWGCTKUQSBNMHL";
        private const string reflectorBDUNNconf = "ENKQAUYWJICOPBLMDXZVFTHRGS";
        private const string reflectorCDUNNconf = "RDOBJNTKVEHMLFCWZAXGYIPSUQ";

        private class Rotor
        {
            private int outerPosition;
            public char outerChar { get; set; }
            private string wiring;
            private char turnOver;
            public string name { get; }
            public char ring { get; set; }
            public int[] map { get; }
            public int[] revMap { get; }

            public Rotor(string w, char to, string n)
            {
                turnOver = to;
                outerPosition = 0;

                ring = 'A';
                name = n;

                map = new int[26];
                revMap = new int[26];

                setWiring(w);
            }

            public void setWiring(string newW)      //устан разводку для рефлекторов
            {
                wiring = newW;
                outerChar = wiring.ToCharArray()[outerPosition];
                for (int i = 0; i < 26; i++)
                {
                    int match = ((int)wiring.ToCharArray()[i]) - 65;
                    map[i] = (26 + match - i) % 26;
                    revMap[match] = (26 + i - match) % 26;
                }
            }

            public void setOuterPosition(int i)
            {
                outerPosition = i;
                outerChar = alphabet.ToCharArray()[outerPosition];
            }

            public int getOuterPosition()
            {
                return outerPosition;
            }

            public void setOuterChar(char c)
            {
                outerChar = c;
                outerPosition = alphabet.IndexOf(outerChar);
            }

            public void step()
            {
                outerPosition = (outerPosition + 2) % 26;               ///правый
                outerChar = alphabet.ToCharArray()[outerPosition];
            }
            public void step2()
            {
                outerPosition = (outerPosition + 2) % 26;               ///центр
                outerChar = alphabet.ToCharArray()[outerPosition];
            }
            public void step3()
            {
                outerPosition = (outerPosition + 1) % 26;               ///левый
                outerChar = alphabet.ToCharArray()[outerPosition];
            }
            public bool isInTurnOver()
            {
                return outerChar == turnOver;
            }
        }

        private void rotateRotors(Rotor[] r)
        {
            if (r.Length == 3)
            {
                r[2].step();
                r[1].step2();
                r[0].step3();
            }
        }

        private char rotorMap(char c, bool reverse)
        {
            int cPos = (int)c - 65;
            if (!reverse)
            {
                for (int i = rotors.Length - 1; i >= 0; i--)
                {
                    cPos = rotorValue(rotors[i], cPos, reverse);
                }
            }
            else
            {
                for (int i = 0; i < rotors.Length; i++)
                {
                    cPos = rotorValue(rotors[i], cPos, reverse);
                }
            }

            return alphabet.ToCharArray()[cPos];
        }
        private int rotorValue(Rotor r, int cPos, bool reverse)
        {
            int rPos = (int)r.ring - 65;
            int d;
            if (!reverse)
                d = r.map[(26 + cPos + r.getOuterPosition() - rPos) % 26];
            else
                d = r.revMap[(26 + cPos + r.getOuterPosition() - rPos) % 26];

            return (cPos + d) % 26;
        }
        private char reflectorMap(char c)
        {
            int cPos = (int)c - 65;
            cPos = (cPos + reflector.map[cPos]) % 26;
            return alphabet.ToCharArray()[cPos];
        }

        
        public EnigmaMachine()
        {
            plugBoard = new Dictionary<char, char>();
            //символы на кот. ротор поворачивается
            Rotor rVII = new Rotor(rotorVIIconf, 'Z', "VII");
            Rotor rGamma = new Rotor(rotorGammaconf, 'Z', "GAMMA");
            Rotor rII = new Rotor(rotorIIconf, 'Z', "II");

            rotors = new Rotor[] { rVII, rGamma, rII };
            reflector = new Rotor(reflectorBDUNNconf, ' ', "");
        }

        
        public void setReflector(char conf)
        {
            if (conf != 'A' && conf != 'B' && conf != 'C' && conf != 'D')
            {
                throw new ArgumentException("Invalid argument");
            }

            string wiring = "";
            switch (conf)
            {
                case 'A':
                    wiring = reflectorAconf;
                    break;
                case 'B':
                    wiring = reflectorBconf;
                    break;
                case 'C':
                    wiring = reflectorCconf;
                    break;
                case 'D':
                    wiring = reflectorBDUNNconf;
                    break;
            }
            reflector.setWiring(wiring);
        }

        public void setSettings(char[] rings, char[] grund)
        {
            if (rings.Length != rotors.Length || grund.Length != rotors.Length)
            {
                throw new ArgumentException("Invalid argument lengths");
            }

            for (int i = 0; i < rotors.Length; i++)
            {
                rotors[i].ring = Char.ToUpper(rings[i]);
                rotors[i].setOuterChar(Char.ToUpper(grund[i]));
            }
        }
                                            ///старт поз
        public void setSettings(char[] rings, char[] grund, string rotorOrder)
        {
            Rotor rI = null;
            Rotor rII = null;
            Rotor rIII = null;
            Rotor rVII = null;
            Rotor rBeta = new Rotor(rotorBetaconf, 'Z', "Beta");
            Rotor rGamma = new Rotor(rotorGammaconf, 'Z', "Gamma");


            for (int i = 0; i < rotors.Length; i++)                 /// Устан позицию до поворота
            {
                if (rotors[i].name == "VII")    rVII = rotors[i];
                if (rotors[i].name == "Gamma")  rGamma = rotors[i];
                if (rotors[i].name == "II")     rII = rotors[i];
            }
            string[] order = rotorOrder.Split('-');


            for (int i = 0; i < order.Length; i++)                  /// Новые позиции
            {
                if (order[i] == "I")    rotors[i] = rI;
                if (order[i] == "II")   rotors[i] = rII;
                if (order[i] == "III")  rotors[i] = rIII;
                if (order[i] == "VII")  rotors[i] = rVII;
                if (order[i] == "Gamma" || order[i] == "GAMMA") rotors[i] = rGamma;
                if (order[i] == "Beta"  || order[i] == "BETA")  rotors[i] = rBeta;
            }
            setSettings(rings, grund);
        }

        
        public void setSettings(char[] rings, char[] grund, string rotorOrder, char reflectorConf)
        {
            setReflector(reflectorConf);
            setSettings(rings, grund, rotorOrder);
        }


        
        public string runEnigma(string msg)
        {
            StringBuilder encryptedMessage = new StringBuilder();
            msg = msg.ToUpper();

            foreach (char c in msg)
            {
                encryptedMessage.Append(encryptChar(c));
            }
            return encryptedMessage.ToString();
        }



        private char encryptChar(char c)
        {
            rotateRotors(rotors);               ///устан позиции роторов до поворота
            if (plugBoard.ContainsKey(c))       ///надо с нашей клавы что-то вводить
            {
                c = plugBoard[c];
            }
            c = rotorMap(c, false);         ///проходим все значения в завис от boolean
            c = reflectorMap(c);
            c = rotorMap(c, true);          ///false - либо в одну сторону либо в другую

            if (plugBoard.ContainsKey(c))
            {
                c = plugBoard[c];
            }
            return c;
        }

        public void addPlug(char c, char cc)
        {
            if (Char.IsLetter(c) && Char.IsLetter(cc))
            {
                c = Char.ToUpper(c);
                cc = Char.ToUpper(cc);
                if (c != cc && !plugBoard.ContainsKey(c))
                {
                    plugBoard.Add(c, cc);
                    plugBoard.Add(cc, c);
                }
            }
            else
            {
                throw new ArgumentException("Invalid character");
            }
        }
    }
}

