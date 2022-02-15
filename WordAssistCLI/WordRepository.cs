using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WordAssistCLI
{

    public class WordRepository
    {
        private string WordFilePath { get; set; }
        private int[] LetterCount { get; set; }
        public WordRepository()
        {
            this.WordFilePath =
                $@"{
                     Directory
                    .GetParent(Environment.CurrentDirectory)
                    .Parent
                    .Parent
                    .FullName
                }\WordFiles";
            this.LetterCount = new int[26];
        }
        public char FindQualifyingWords(out IEnumerable<string> guesses, IEnumerable<string> find, IEnumerable<string> exclude)
        {
            LetterCount = new int[26];
            guesses = FindQualifyingWords(find, exclude);
            return GetMostCommonLetter(guesses, find, exclude);
        }
        public IEnumerable<string> FindQualifyingWords(IEnumerable<string> find, IEnumerable<string> exclude)
            => this.FindQualifyingWords(IEnumberableToString(find), IEnumberableToString(exclude));

        public IEnumerable<string> FindQualifyingWords(string find, string exclude)
        {
            using (StreamReader reader = new StreamReader($@"{WordFilePath}\{find.Length}.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (AuthenticateWord(line, find, exclude))
                        yield return line;
                }
            }
        }
        private bool AuthenticateWord(string word, string find, string exclude)
        {
            if (exclude.Length != 0)
                if (ExcludeWordIfContains(word, exclude))
                    return false;
            for (int i = 0; i < find.Length; i++)
            {
                List<int> multiplesIndex;
                if (find[i] != ' ' &&
                   (word[i] != find[i] ||
                   (CheckContainsMultiples(word, i, out multiplesIndex) &&
                   CheckMultipleValid(multiplesIndex, find, word[i]))))
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckMultipleValid(List<int> indexs, string find, char multiple)
        {
            foreach (int index in indexs)
            {
                if (find[index] == multiple)
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckContainsMultiples(string word, int index, out List<int> multiples)
        {
            int count = 0;
            bool containsMultiples = false;
            multiples = new List<int>();
            foreach (char letter in word)
            {
                if (count != index && word[index] == letter)
                {
                    containsMultiples = true;
                    multiples.Add(count);
                }
                count++;
            }
            return containsMultiples;
        }
        private bool ExcludeWordIfContains(string word, string exclude)
        {
            foreach (char letter in exclude)
            {
                if (AuthenticateLetter(word, letter))
                {
                    return true;
                }

            }
            return false;
        }
        private bool AuthenticateLetter(string word, char letter)
        {
            foreach (char ch in word)
            {
                if (ch == letter)
                {
                    return true;
                }
            }
            return false;
        }
        private string IEnumberableToString(IEnumerable<string> iEnumberable)
        {
            string temp = "";
            foreach (string letter in iEnumberable)
            {
                temp += letter;
            }
            return temp;
        }

        char GetMostCommonLetter(IEnumerable<string> wordList, IEnumerable<string> find, IEnumerable<string> exclude)
            => GetMostCommonLetter(wordList, IEnumberableToString(find), IEnumberableToString(exclude));
        char GetMostCommonLetter(IEnumerable<string> wordList, string find, string exclude)
        {
            foreach (string word in wordList)
            {
                TallyWord(word);
            }
            int largest = 0;
            int index = 0;
            for (int i = 0; i < LetterCount.Length; i++)
            {
                if (LetterCount[i] > largest && !AuthenticateLetter(find, char.ToLower((char)(i + 65))))
                {
                    largest = LetterCount[i];
                    index = i;
                }
            }
            return (char)(index + 65);
        }
        void TallyWord(string word)
        {
            foreach (char letter in word)
            {
                LetterCount[char.ToUpper(letter) - 65]++;
            }
        }
    }
}

