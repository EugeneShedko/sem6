using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZI6X
{
    public class EnigmaSettings
    {
        public char[] rings { get; set; }
        public char[] grund { get; set; }
        public string order { get; set; }
        public char reflector { get; set; }
        public List<string> plugs = new List<string>();

        public EnigmaSettings()
        {
            setDefault();
        }

        public void setDefault()
        {
            rings = new char[] { 'A', 'A', 'A' };
            grund = new char[] { 'A', 'A', 'A' };
            order = "VII-Gamma-II";    
            reflector = 'D';
            plugs.Clear();
        }
    }
}
