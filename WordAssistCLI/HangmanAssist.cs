using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordAssistCLI
{
    public class HangmanAssist
    {
        WordRepository Repository { get; set; }
        public char[] Find { get; private set; }
        public HangmanAssist()
        {
            Repository = new WordRepository();
            Find = new char[0];
        }
        public IEnumerable<string> find()
        {
            string temp = "";
            foreach(char letter in Find)
            {
                temp += " ";
            }
            return Repository.FindQualifyingWords(temp, "");
        }
        public bool TrySetLength(int length)
        {
            if(length < 32 && length > 1)
            {
                Find = new char[length];
                return true;
            }
            return false;
        }
    }
}
