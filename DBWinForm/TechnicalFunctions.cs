using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBWinForm
{
    class TechnicalFunctions
    {
        public string[] Split(string splitThis, char separatingChar)
        {
            string[] splittedString= splitThis.Split(separatingChar).ToArray();
            //string[] splittedString = splitThis.(key => key.Split(' '));
            //string charsArray = new String(splitThis.ToCharArray().Where(c => Char.IsLetter(c)).ToArray());
            return splittedString;
        }
    }
}
