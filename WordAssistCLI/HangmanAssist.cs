using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordAssistCLI
{
    public class HangmanAssist
    {
        WordAssist Assist { get; }
        private char[] CorrectLetters { get; set; }
        private string StringCL
        {
            get
            {
                string temp = "";
                foreach (char letter in CorrectLetters)
                {
                    temp += letter;
                }
                return temp;
            }
         }
        private string[] SplitInput => this.Assist.Input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        public HangmanAssist(WordAssist assist)
        {
            this.Assist = assist;
            this.CorrectLetters = new char[0];
        }
        public void SetLen()
        {
            if (SplitInput.Length != 2 || !Int32.TryParse(SplitInput[1], out int len) || len > 31 || len < 2)
            {
                Console.WriteLine("Please format as: \"/setlen {a}\" where a is an integer between 2 - 31");
                return;
            }
            char[] temp = new char[len];
            for (int i = 0; i < len && i < CorrectLetters.Length; i++)
            {
                temp[i] = this.CorrectLetters[i];
            }
            this.CorrectLetters = temp;
            Console.WriteLine($"The length of the word is now: {len}");
        }
        public void SetCL()
        {
            if (SplitInput.Length == 2 && 
                SplitInput[1].Length == CorrectLetters.Length && 
                SplitInput[1].All(Char.IsLetter))
            {
                for (int i = 0; i < CorrectLetters.Length; i++)
                {
                    this.CorrectLetters[i] = SplitInput[1][i];
                }
                Console.WriteLine(StringCL);
            }
            else if (
                SplitInput.Length == 3 && 
                SplitInput[2].Length == 1 && 
                SplitInput[2].All(Char.IsLetter) && 
                Int32.TryParse(SplitInput[1], out int pos) && 
                pos > 0 && 
                pos < CorrectLetters.Length + 1)
            {
                this.CorrectLetters[pos-1] = SplitInput[2][0];
                Console.WriteLine(StringCL);
            }
            else
            {
                Console.WriteLine("Fuck you");
            }
        }
        public void SetIL()
        {
            
        }
        public void AddCL()
        {

        }
        public void AddIL()
        {

        }
        public void RMCL()
        {

        }
        public void RMIL()
        {

        }
        public void ListCL()
        {

        }
        public void ListIL()
        {

        }
        public void ListAG()
        {

        }
        public void ListBG()
        {

        }
        public void NumG()
        {

        }
        public void ClearCl()
        {

        }
        public void ClearIL()
        {

        }
        public void ClearAll()
        {

        }

        public void ListAll()
        {

        }
    }
}
