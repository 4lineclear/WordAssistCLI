using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordAssistCLI
{
    public class WordAssist
    {
        string Input { get; set; }
        private Process CurrentProcess { get; set; }
        private KeyMatcher Matcher { get; }
        private HangmanAssist Hangman { get; }
        public WordAssist()
        {
            this.Input = "";
            this.Matcher = new KeyMatcher("Input Unrecognized");
            this.CurrentProcess = new Process("", "", StartingMessage);

            Hangman = new HangmanAssist();

            ProcessMatcher actionMatcher = new ProcessMatcher("/", "Action Unrecognized");
            ProcessMatcher hangmanMatcher = new ProcessMatcher("/", "Hangman Action Unrecognized");
            ProcessMatcher commandMatcher = new ProcessMatcher("!", "Command Unrecognized");

            hangmanMatcher.AddProcess("back","returns to main menu", () =>
            {
                Matcher.SetNewProcessMatcher("/", actionMatcher);
                Console.Clear();
                Console.WriteLine("Returned to main menu");
            });
            hangmanMatcher.AddProcess("startfind","starts the finding of a word", () =>
            {
                Console.Write("Word Length: ");
                int output = 0;
                while(!Int32.TryParse(Console.ReadLine(), out output) || !Hangman.TrySetLength(output))
                {
                    Console.WriteLine("Invalid input! Please enter a integer within 2 and 31");
                }
                Console.WriteLine(Hangman.Find.Length);
                hangmanMatcher.RemoveProcess("startfind");
                hangmanMatcher.AddProcess("find", "prints list of possible results", () =>
                {
                    IEnumerable<string> guesses = Hangman.find();
                    foreach(string word in guesses)
                    {
                        Console.WriteLine(word);
                    }
                });
            });

            actionMatcher.AddProcess("hangman", "starts hangman", () => 
            {
                Matcher.SetNewProcessMatcher("/", hangmanMatcher);
                Console.Clear();
                Console.WriteLine("Hangman started");
            });


            commandMatcher.AddProcess("clear", "clears the console", () => Console.Clear());
            commandMatcher.AddProcess("commands", "prints list of commands", () => Console.WriteLine($"Commands:\n{commandMatcher.GetProcessInputs}"));
            commandMatcher.AddProcess("actions", "prints list of usable actions", () => Console.WriteLine($"Actions:\n{Matcher.GetProcessMatcher("/").GetProcessInputs}"));
            commandMatcher.AddProcess("quit", "ends word assist", ()=>{ });

            this.Matcher.AddProcessMatcher(actionMatcher);
            this.Matcher.AddProcessMatcher(commandMatcher);
        }
        public void StartAssist()
        {
            while (Input != "!quit")
            {
                this.CurrentProcess.Response();
                this.Input = Console.ReadLine().Trim().ToLower();
                this.CurrentProcess = Matcher.GetProcess(Input);
            }
            Console.WriteLine("goodbye");
        }
        public void StartingMessage()
        {
            Console.WriteLine(  "Welcome To Word Assist! The very best word game tool around\n" +
                                "===========================\n" +
                                "   Press Enter to Start    \n" +
                                "===========================");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.WriteLine();
            }
            Console.Clear();
        }
    }
}
